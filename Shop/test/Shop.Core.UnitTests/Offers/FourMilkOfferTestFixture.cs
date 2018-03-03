using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoMoq;
using NUnit.Framework;
using Shop.Core.Models;
using Shop.Core.Offers;

namespace Shop.Core.UnitTests.Offers
{
    [TestFixture]
    public class FourMilkOfferTestFixture
    {
        private IFixture fixture;

        [OneTimeSetUp]
        public void OneTimeSetp()
        {
            fixture = new Fixture().Customize(new AutoConfiguredMoqCustomization());
        }

        [TestCase(0, 1.15, 100, 100)]
        [TestCase(3, 1.15, 100, 100)]
        [TestCase(4, 1.15, 4.60, 3.45)]
        [TestCase(9, 1.15, 10.35, 8.05)]
        public void ApplyDiscount_WhenThereIsXAmountOfMilkInBasket_TotalIsCalculatedCorrectly(int qtyMilk, double priceMilk, double initTotal, double expected)
        {
            // Arrange
            var milk = fixture.Build<Product>()
                .With(p => p.Name, "Milk")
                .With(p => p.Price, priceMilk)
                .Create();
            var basket = new Basket();
            basket.AddItem(milk, qtyMilk);

            var subject = new FourMilkOffer();

            // Act
            var result = Math.Round(subject.ApplyDiscount(basket, initTotal), 2);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ApplyDiscount_WhenThereIsNoMilkInBasket_TotalRemainsUnchanged()
        {
            // Arrange
            var initTotal = 100;
            var product = fixture.Create<Product>();
            var basket = new Basket();
            basket.AddItem(product, 10);

            var subject = new FourMilkOffer();

            // Act
            var result = subject.ApplyDiscount(basket, initTotal);

            // Assert
            Assert.AreEqual(initTotal, result);
        }
    }
}
