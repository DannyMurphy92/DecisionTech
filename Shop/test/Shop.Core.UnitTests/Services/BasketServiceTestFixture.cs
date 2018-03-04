using System;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using AutoFixture.AutoMoq;
using Moq;
using NUnit.Framework;
using Shop.Core.Models;
using Shop.Core.Offers.Interfaces;
using Shop.Core.Services;
using Shop.Core.Services.Interfaces;

namespace Shop.Core.UnitTests.Services
{
    [TestFixture]
    public class BasketServiceTestFixture
    {
        private IFixture fixture;

        private Mock<IProductRepo> productRepoMock;
        private IList<Mock<IOffer>> offersMock;
        private Product product;
        private Basket basket;
        private IList<double> resultTotals;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            fixture = new Fixture().Customize(new AutoConfiguredMoqCustomization());

            productRepoMock = fixture.Freeze<Mock<IProductRepo>>();
        }

        [SetUp]
        public void Setup()
        {
            product = fixture.Create<Product>();
            offersMock = fixture.Freeze<IList<Mock<IOffer>>>();
            productRepoMock.Setup(p => p.GetProductByName(It.IsAny<string>()))
                .Returns(() => product);


            resultTotals = fixture.CreateMany<double>(offersMock.Count).ToList();
            for (int i = 0; i < resultTotals.Count; i++)
            {
                offersMock[i].Setup(o => o.ApplyDiscount(It.IsAny<Basket>(), It.IsAny<double>()))
                    .Returns(resultTotals[i]);
            }
            
            basket = fixture.Create<Basket>();
            basket.AddItem(fixture.Create<Product>(), fixture.Create<int>());
            basket.AddItem(fixture.Create<Product>(), fixture.Create<int>());
            basket.AddItem(fixture.Create<Product>(), fixture.Create<int>());
        }

        [TearDown]
        public void Teardown()
        {
            productRepoMock.Reset();

            foreach (var offer in offersMock)
            {
                offer.Reset();
            }
        }

        [Test]
        public void AddProductToBasket_WhenInvoked_CallsProductRepo()
        {
            // Arrange
            var productName = fixture.Create<string>();
            var subject = CreateBasketService();

            // Act
            subject.AddProductToBasket(basket, productName, 1);

            // Assert
            productRepoMock.Verify(p => p.GetProductByName(productName), Times.Once);
        }

        [Test]
        public void AddProductToBasket_WhenProductExists_AddsProductToBasket()
        {
            // Arrange
            var quantity = 1;
            var subject = CreateBasketService();

            // Act
            var result = subject.AddProductToBasket(basket, fixture.Create<string>(), quantity);

            // Assert
            Assert.IsTrue(result);
            Assert.IsTrue(basket.Items.Any(i => i.Product.Name == product.Name && i.Quantity == quantity));
        }
        
        [TestCase(false, -1)]
        [TestCase(false, 0)]
        [TestCase(true, 10)]
        public void AddProductToBasket_WhenInvokedWithInvalidArguments_ReturnsFalseNothingIsAddedToTheBasket(bool productIsNull, int quantity)
        {
            // Arrange
            var productName = product.Name;
            product = productIsNull ? null : product;

            var initItemCount = basket.Items.Count;
            var initProductCount = basket.Items.FirstOrDefault(i => i.Product.Name == productName)?.Quantity;
            var subject = CreateBasketService();

            // Act
            var result = subject.AddProductToBasket(basket, fixture.Create<string>(), quantity);

            // Assert
            var finalProductCount = basket.Items.FirstOrDefault(i => i.Product.Name == productName)?.Quantity;

            Assert.IsFalse(result);
            Assert.AreEqual(initItemCount, basket.Items.Count());
            Assert.AreEqual(initProductCount, finalProductCount);
        }

        [Test]
        public void CalculateTotal_WhenInvoked_UpdatesTotalToResultFromOffers()
        {
            // Arrange
            var initTotal = CalculateTotalWithoutOffers();

            var subject = CreateBasketService();

            // Act
            var result = subject.CalculateTotal(basket);

            // Assert
            for (int i = 0; i < resultTotals.Count(); i++)
            {
                if (i > 0)
                {
                    initTotal = resultTotals[i - 1];
                }
                offersMock[i].Verify(o => o.ApplyDiscount(basket, initTotal), Times.Once);
            }
            Assert.AreEqual(resultTotals[resultTotals.Count - 1], result);
        }

        [Test]
        public void CalculateTotal_IfNoOffers_ReturnsRegularBasketTotal()
        {
            // Arrange
            offersMock = new List<Mock<IOffer>>();
            var expected = Math.Round(CalculateTotalWithoutOffers(),2);

            var subject = CreateBasketService();

            // Act
            var resut = Math.Round(subject.CalculateTotal(basket), 2);

            // Assert
            Assert.AreEqual(expected, resut);
        }

        private double CalculateTotalWithoutOffers()
        {
            var result = 0d;

            foreach (var item in basket.Items)
            {
                result += item.Quantity * item.Product.Price;
            }

            return result;
        }

        //Needed because of issue injecting list of mock IOffers
        private BasketService CreateBasketService()
        {
            return new BasketService(productRepoMock.Object, offersMock.Select(o => o.Object).ToList());
        }
    }
}
