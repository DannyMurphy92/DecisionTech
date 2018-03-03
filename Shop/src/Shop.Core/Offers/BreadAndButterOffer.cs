using System;
using System.Linq;
using Shop.Core.Models;
using Shop.Core.Offers.Interfaces;

namespace Shop.Core.Offers
{
    public class BreadAndButterOffer : IOffer
    {
        private const string Bread = "bread";
        private const string Butter = "butter";
        private const int OfferButterQty = 2;
        private const int OfferBreadQty = 1;
        private const double PcValBreadOffer = 0.5;

        public double ApplyDiscount(Basket basket, double currentTotal)
        {
            var result = currentTotal;
            var breadBasket = basket.Items.FirstOrDefault(i => i.Product.Name.ToLower().Trim() == Bread);
            var butterBasket = basket.Items.FirstOrDefault(i => i.Product.Name.ToLower().Trim() == Butter);
            if (breadBasket != null && butterBasket != null && 
                breadBasket.Quantity >= OfferBreadQty && butterBasket.Quantity >= OfferButterQty)
            {
                var breadPrice = breadBasket.Product.Price;
                var butterPrice = butterBasket.Product.Price;

                var numBreadApplications = breadBasket.Quantity / OfferBreadQty;
                var numButterApplications = butterBasket.Quantity / OfferButterQty;

                var numApplications = (numButterApplications < numBreadApplications)
                    ? numButterApplications
                    : numBreadApplications;

                var origPrice = breadPrice * OfferBreadQty * numApplications;
                var newPrice = breadPrice * OfferBreadQty * numApplications * PcValBreadOffer;

                result = result - origPrice + newPrice;
            }

            return result;
        }
    }
}
