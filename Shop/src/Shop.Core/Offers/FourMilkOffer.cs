using System.Linq;
using Shop.Core.Models;

namespace Shop.Core.Offers
{
    public class FourMilkOffer : OfferBase
    {
        private const string Milk = "milk";
        private const int OfferQty = 4;
        private const int EquivQty = 3;
        
        protected override bool OfferCanBeApplied(Basket basket)
        {
            var milkBasket = GetMilkItemFromBasket(basket);

            return milkBasket != null && milkBasket.Quantity >= OfferQty;
        }

        protected override int NumberOfTimesOfferCanBeApplied(Basket basket)
        {
            var milkBasket = GetMilkItemFromBasket(basket);

            return milkBasket.Quantity / OfferQty;
        }

        protected override double OriginalPriceOfAffectedProductsBeforeOffer(Basket basket)
        {
            var milkBasket = GetMilkItemFromBasket(basket);

            return milkBasket.Product.Price * OfferQty;
        }

        protected override double PriceOfAffectedProductsAfterOfferApplied(Basket basket)
        {
            var milkBasket = GetMilkItemFromBasket(basket);

            return milkBasket.Product.Price * EquivQty;
        }

        private BasketItem GetMilkItemFromBasket(Basket basket)
        {
            return basket.Items.FirstOrDefault(i => i.Product.Name.ToLower().Trim() == Milk);
        }
    }
}
