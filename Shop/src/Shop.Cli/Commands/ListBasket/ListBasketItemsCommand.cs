using System;
using Shop.Core.Models;

namespace Shop.Cli.Commands.ListBasket
{
    public class ListBasketItemsCommand
    {
        public int Execute(ListBasketItemsOptions options, Basket basket)
        {
            var total = 0d;
            foreach (var item in basket.Items)
            {
                Console.WriteLine($"{item.Quantity} \t {item.Product.Name} @ £{item.Product.Price}");

                total += item.Quantity * item.Product.Price;
            }

            Console.WriteLine($"The total before offers are applied is: £{total}");

            return 0;
        }
    }
}
