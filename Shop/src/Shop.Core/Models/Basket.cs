using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Core.Models
{
    public class Basket
    {

        public IList<BasketItem> Items { get; }


        public Basket()
        {
            Items = new List<BasketItem>();
        }

        public void AddItem(Product product, int quantity)
        {
            var existingItem = Items.FirstOrDefault(i => i.Product.Name.ToLower().Trim() == product.Name.ToLower().Trim());
            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                Items.Add(
                    new BasketItem
                    {
                        Product = product,
                        Quantity = quantity
                    }
                );
            }
        }
    }
}
