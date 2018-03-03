using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoMoq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Shop.Core.Models;
using Shop.Core.Offers;

namespace Shop.Core.UnitTests.Offers
{
    [TestFixture]
    public class BreadAndButterOfferTestFixture
    {
        private IFixture fixture;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            fixture = new Fixture().Customize(new AutoConfiguredMoqCustomization());
        }

        [TestCase(0, 0, 0.8, 1, 100, 100)]
        [TestCase(2, 1, 0.8, 1, 2.6, 2.1)]
        [TestCase(4, 1, 0.8, 1, 4.2, 3.7)]
        [TestCase(2, 2, 0.8, 1, 3.6, 3.1)]
        [TestCase(1, 1, 0.8, 1, 3.6, 3.6)]
        [TestCase(2, 0, 0.8, 1, 3.6, 3.6)]
        public void ApplyDiscount_WhenBasketHasXBreadAndYButter_AppliesDiscountCorrectly(int butterQty, int breadQty, double butterPrice, double breadPrice, double initTotal, double expectedTotal)
        {
            // Arrange
            var butter = fixture.Build<Product>()
                .With(p => p.Name, "Butter")
                .With(p => p.Price, butterPrice)
                .Create();
            var bread = fixture.Build<Product>()
                .With(p => p.Name, "Bread")
                .With(p => p.Price, breadPrice)
                .Create();
            var basket = new Basket();
            basket.AddItem(butter, butterQty);
            basket.AddItem(bread, breadQty);

            var subject = fixture.Create<BreadAndButterOffer>();

            // Act
            var result = Math.Round(subject.ApplyDiscount(basket, initTotal), 2);

            // Assert
            Assert.AreEqual(expectedTotal, result);
        }
    }
}
