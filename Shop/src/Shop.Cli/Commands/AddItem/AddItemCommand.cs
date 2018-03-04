using System;
using Shop.Core.Models;
using Shop.Core.Services.Interfaces;

namespace Shop.Cli.Commands.AddItem
{
    public class AddItemCommand
    {
        private readonly IBasketService basketService;

        public AddItemCommand(IBasketService basketService)
        {
            this.basketService = basketService;
        }

        public int Execute(AddItemOptions options, Basket basket)
        {
            var result = basketService.AddProductToBasket(basket, options.ItemName, options.Quantity);

            if (result)
            {
                Console.WriteLine($"Successfully added {options.Quantity} {options.ItemName} to the basket");
            }
            else
            {

                Console.WriteLine($"Could not add {options.Quantity} {options.ItemName} to the basket, please ensure it is a valid input");
            }
            return 0;
        }
    }
}
