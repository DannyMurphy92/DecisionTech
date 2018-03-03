using Shop.Core.Models;
using Shop.Core.Offers.Interfaces;

namespace Shop.Core.Offers
{
    public abstract class OfferBase : IOffer
    {
        public double ApplyDiscount(Basket basket, double currentTotal)
        {
            var result = currentTotal;
            if (OfferCanBeApplied(basket))
            {
                var numApplications = NumberOfTimesOfferCanBeApplied(basket);

                var origPrice = OriginalPriceOfAffectedProductsBeforeOffer(basket) * numApplications;
                var newPrice = PriceOfAffectedProductsAfterOfferApplied(basket) * numApplications;
               
                result = result - origPrice + newPrice;
            }

            return result;
        }

        protected abstract bool OfferCanBeApplied(Basket basket);

        protected abstract  int NumberOfTimesOfferCanBeApplied(Basket basket);

        protected abstract double OriginalPriceOfAffectedProductsBeforeOffer(Basket basket);

        protected abstract double PriceOfAffectedProductsAfterOfferApplied(Basket basket);
    }
}
