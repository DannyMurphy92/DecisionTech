using Shop.Core.Models;

namespace Shop.Core.Services.Interfaces
{
    public interface IBasketService
    {
        bool AddProductToBasket(Basket basket, string name, int quantity);

        double CalculateTotal(Basket basket);
    }
}
