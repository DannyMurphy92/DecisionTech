using System.Collections.Generic;
using Shop.Core.Models;
using Shop.Core.Offers.Interfaces;
using Shop.Core.Services.Interfaces;

namespace Shop.Core.Services
{
    public class BasketService : IBasketService
    {
        private readonly IProductRepo productRepo;
        private readonly IList<IOffer> offers;

        public BasketService(IProductRepo productRepo, IList<IOffer> offers)
        {
            this.productRepo = productRepo;
            this.offers = offers;
        }

        public bool AddProductToBasket(Basket basket, string name, int quantity)
        {
            if (quantity > 0)
            {
                var product = productRepo.GetProductByName(name);
                if (product != null)
                {
                    basket.AddItem(product, quantity);

                    return true;
                }
            }

            return false;
        }

        public double CalculateTotal(Basket basket)
        {
            var result = CalculateTotalWithoutOffers(basket);

            foreach (var offer in offers)
            {
                result = offer.ApplyDiscount(basket, result);
            }

            return result;
        }

        private double CalculateTotalWithoutOffers(Basket basket)
        {
            var result = 0d;

            foreach (var item in basket.Items)
            {
                result += item.Quantity * item.Product.Price;
            }

            return result;
        }
    }
}
