using System.Linq;
using Shop.Core.Models;

namespace Shop.Core.Offers
{
    public class BreadAndButterOffer : OfferBase
    {
        private const string Bread = "bread";
        private const string Butter = "butter";
        private const int OfferButterQty = 2;
        private const int OfferBreadQty = 1;
        private const double PcValBreadOffer = 0.5;

        protected override bool OfferCanBeApplied(Basket basket)
        {
            var breadBasket = GetBreadFromBasket(basket);
            var butterBasket = GetButterFromBasket(basket);

            return (breadBasket != null && butterBasket != null &&
                    breadBasket.Quantity >= OfferBreadQty && butterBasket.Quantity >= OfferButterQty);
        }

        protected override int NumberOfTimesOfferCanBeApplied(Basket basket)
        {
            var breadBasket = GetBreadFromBasket(basket);
            var butterBasket = GetButterFromBasket(basket);

            var numBreadApplications = breadBasket.Quantity / OfferBreadQty;
            var numButterApplications = butterBasket.Quantity / OfferButterQty;

            return (numButterApplications < numBreadApplications)
                ? numButterApplications
                : numBreadApplications;

        }

        protected override double OriginalPriceOfAffectedProductsBeforeOffer(Basket basket)
        {
            var breadBasket = GetBreadFromBasket(basket);

            return breadBasket.Product.Price * OfferBreadQty;
        }

        protected override double PriceOfAffectedProductsAfterOfferApplied(Basket basket)
        {
            var breadBasket = GetBreadFromBasket(basket);

            return breadBasket.Product.Price * OfferBreadQty * PcValBreadOffer;
        }

        private BasketItem GetBreadFromBasket(Basket basket)
        {
            return basket.Items.FirstOrDefault(i => i.Product.Name.ToLower().Trim() == Bread);
        }

        private BasketItem GetButterFromBasket(Basket basket)
        {
            return basket.Items.FirstOrDefault(i => i.Product.Name.ToLower().Trim() == Butter);
        }
    }
}
