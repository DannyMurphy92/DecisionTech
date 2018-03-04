using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Core.Models;
using Shop.Core.Services.Interfaces;

namespace Shop.Core.Services
{
    public class ProductRepo : IProductRepo
    {
        //Could be replaced with datastore, CSV etc.
        private readonly IList<Product> products;

        public ProductRepo(IList<Product> products)
        {
            this.products = products;
        }

        public Product GetProductByName(string name)
        {
            return products.FirstOrDefault(p => p.Name.ToLower() == name.ToLower().Trim());
        }
    }
}
