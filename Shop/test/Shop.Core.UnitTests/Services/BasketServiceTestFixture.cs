using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoMoq;
using Moq;
using NUnit.Framework;
using Shop.Core.Models;
using Shop.Core.Services;
using Shop.Core.Services.Interfaces;

namespace Shop.Core.UnitTests.Services
{
    [TestFixture]
    public class BasketServiceTestFixture
    {
        private IFixture fixture;

        private Mock<IProductRepo> productRepoMock;
        private Product product;
        private Basket basket;

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

            productRepoMock.Setup(p => p.GetProductByName(It.IsAny<string>()))
                .Returns(() => product);

            basket = new Basket();
        }

        [TearDown]
        public void Teardown()
        {
            productRepoMock.Reset();
        }

        [Test]
        public void AddProductToBasket_WhenInvoked_CallsProductRepo()
        {
            // Arrange
            var productName = fixture.Create<string>();
            var subject = fixture.Create<BasketService>();

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
            var subject = fixture.Create<BasketService>();

            // Act
            subject.AddProductToBasket(basket, fixture.Create<string>(), quantity);

            // Assert
            Assert.IsTrue(basket.Items.Any(i => i.Product.Name == product.Name && i.Quantity == quantity));
        }


        [Test]
        public void AddPRoductToBasket_WhenProductDoesNotExist_NothingIsAddedToTheBasket()
        {
            // Arrange & Act & Assert
            var productName = product.Name;
            product = null;
            AssertBasketDoesNotUpdate(productName, 10);
        }

        [Test]
        public void AddProductToBasket_WhenQuantityIsZero_NothingIsAddedToTheBasket()
        {
            // Arrange & Act & Assert
            AssertBasketDoesNotUpdate(product.Name, 0);
        }

        [Test]
        public void AddProductToBasket_WhenQuantityIsNegative_NothingIsAddedToTheBasket()
        {
            // Arrange
            AssertBasketDoesNotUpdate(product.Name, -1);
        }

        private void AssertBasketDoesNotUpdate(string productName, int quantity)
        {
            // Arrange
            var initItemCount = basket.Items.Count;
            var initProductCount = basket.Items.FirstOrDefault(i => i.Product.Name == productName)?.Quantity;
            var subject = fixture.Create<BasketService>();

            // Act
            subject.AddProductToBasket(basket, fixture.Create<string>(), quantity);

            // Assert
            var finalProductCount = basket.Items.FirstOrDefault(i => i.Product.Name == productName)?.Quantity;

            Assert.AreEqual(initItemCount, basket.Items.Count());
            Assert.AreEqual(initProductCount, finalProductCount);
        }
    }
}
