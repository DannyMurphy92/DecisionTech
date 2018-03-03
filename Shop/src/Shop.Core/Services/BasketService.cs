using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Core.Models;
using Shop.Core.Services.Interfaces;

namespace Shop.Core.Services
{
    public class BasketService : IBasketService
    {
        private readonly IProductRepo productRepo;

        public BasketService(IProductRepo productRepo)
        {
            this.productRepo = productRepo;
        }

        public void AddProductToBasket(string name, int quantity)
        {
            if (quantity > 0)
            {
                var product = productRepo.GetProductByName(name);
                if (product != null)
                {
                    var basket = Basket.Instance();
                    basket.AddItem(product, quantity);
                }
            }
        }
    }
}
