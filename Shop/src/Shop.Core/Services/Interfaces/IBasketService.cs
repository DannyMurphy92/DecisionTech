using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Core.Services.Interfaces
{
    public interface IBasketService
    {
        void AddProductToBasket(string name, int quantity);
    }
}
