using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Shop.Core.Models;
using Shop.Core.Offers;

namespace Shop.Core.UnitTests.Offers
{
    [TestFixture]
    public class OfferBaseTestFixture
    {
        private Basket basket;

        [SetUp]
        public void Setup()
        {
            basket = new Basket();
        }

        [Test]
        public void ApplyOffer_WhenOfferCannotBeApplied_ReturnsInitialTotal()
        {
            // Arrange
            var initTotal = 100d;
            var subject = new OfferBaseTest(false, 0, 10, 5);

            // Act
            var result = subject.ApplyDiscount(basket, initTotal);

            // Assert
            Assert.AreEqual(initTotal, result);
        }


        [TestCase(1, 10, 5, 20, 15)]
        [TestCase(2, 10, 5, 20, 10)]
        [TestCase(0, 10, 5, 20, 20)]
        public void ApplyOffer_WhenCanBeApplied_AppliesOfferCorrectly(int numApps, double origPrice, double offerPrice, double initTotal, double expected)
        {
            // Arrange
            var subject = new OfferBaseTest(true, numApps, origPrice, offerPrice);

            // Act
            var result = subject.ApplyDiscount(basket, initTotal);

            // Assert
            Assert.AreEqual(expected, result);
        }
    }

    public class OfferBaseTest : OfferBase
    {
        private readonly bool canBeApplied;
        private readonly int numAppliedcations;
        private readonly double origPrice;
        private readonly double offerPrice;

        public OfferBaseTest(
            bool canBeApplied, 
            int numAppliedcations, 
            double origPrice, 
            double offerPrice)
        {
            this.canBeApplied = canBeApplied;
            this.numAppliedcations = numAppliedcations;
            this.origPrice = origPrice;
            this.offerPrice = offerPrice;
        }


        protected override bool OfferCanBeApplied(Basket basket)
        {
            return canBeApplied;
        }

        protected override int NumberOfTimesOfferCanBeApplied(Basket basket)
        {
            return numAppliedcations;
        }

        protected override double OriginalPriceOfAffectedProductsBeforeOffer(Basket basket)
        {
            return origPrice;
        }

        protected override double PriceOfAffectedProductsAfterOfferApplied(Basket basket)
        {
            return offerPrice;
        }
    }
}
