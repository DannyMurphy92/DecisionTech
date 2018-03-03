using System.Linq;
using Shop.Core.Models;
using Shop.Core.Offers.Interfaces;

namespace Shop.Core.Offers
{
    public class FourMilkOffer : IOffer
    {
        private const string Milk = "milk";
        private const int OfferQty = 4;
        private const int EquivQty = 3;

        public double ApplyDiscount(Basket basket, double currentTotal)
        {
            var result = currentTotal;
            var milkBasket = basket.Items.FirstOrDefault(i => i.Product.Name.ToLower().Trim() == Milk);
            if (milkBasket != null && milkBasket.Quantity >= OfferQty)
            {
                var milkPrice = milkBasket.Product.Price;
                int numApplications = milkBasket.Quantity / OfferQty;

                var origPrice = milkPrice * OfferQty * numApplications;
                var newPrice = milkPrice * EquivQty * numApplications;

                result = result - origPrice + newPrice;
            }

            return result;
        }
    }
}
