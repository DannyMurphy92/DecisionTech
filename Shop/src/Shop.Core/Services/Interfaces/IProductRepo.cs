using System.Collections.Generic;
using Shop.Core.Models;

namespace Shop.Core.Services.Interfaces
{
    public interface IProductRepo
    {
        Product GetProductByName(string name);

        IList<Product> GetAll();
    }
}
