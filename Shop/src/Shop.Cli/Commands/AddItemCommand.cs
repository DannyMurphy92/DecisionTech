using Shop.Core.Models;
using Shop.Core.Services.Interfaces;

namespace Shop.Cli.Commands
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
            basketService.AddProductToBasket(basket, options.ItemName, options.Quantity);

            return 0;
        }
    }
}
