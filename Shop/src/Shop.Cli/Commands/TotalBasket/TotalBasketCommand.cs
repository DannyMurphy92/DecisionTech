using System;
using Shop.Core.Models;
using Shop.Core.Services.Interfaces;

namespace Shop.Cli.Commands.TotalBasket
{
    public class TotalBasketCommand
    {
        private readonly IBasketService basketService;

        public TotalBasketCommand(IBasketService basketService)
        {
            this.basketService = basketService;
        }

        public int Execute(TotalBasketOptions options, Basket basket)
        {
            var total = basketService.CalculateTotal(basket);

            Console.WriteLine($"Your baskets total is £{total}");

            return 0;
        }
    }
}
