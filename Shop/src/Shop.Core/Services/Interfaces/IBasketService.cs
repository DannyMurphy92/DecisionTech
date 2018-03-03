using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Core.Models;

namespace Shop.Core.Services.Interfaces
{
    public interface IBasketService
    {
        void AddProductToBasket(Basket basket, string name, int quantity);
    }
}
