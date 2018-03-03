using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Core.Models;
using Shop.Core.Offers.Interfaces;

namespace Shop.Core.Offers
{
    public class FourMilkOffer : IOffer
    {
        private const string Milk = "milk";
        private const int OffcerQty = 4;

        public double ApplyDiscount(Basket basket, double currentTotal)
        {
            var result = currentTotal;
            var milk = basket.Items.FirstOrDefault(i => i.Product.Name.ToLower().Trim() == Milk);
            if (milk != null && milk.Quantity > 0)
            {
                var milkPrice = milk.Product.Price;
                int numApplications = milk.Quantity / OffcerQty;

                var origPrice = milkPrice * OffcerQty * numApplications;
                var newPrice = milkPrice * 3 * numApplications;

                result = result - origPrice + newPrice;
            }

            return result;
        }
    }
}
