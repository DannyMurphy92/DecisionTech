using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Core.Services.Interfaces;

namespace Shop.Cli.Commands.ListItems
{
    public class ListItemsCommand
    {
        private readonly IProductRepo productRepo;

        public ListItemsCommand(IProductRepo productRepo)
        {
            this.productRepo = productRepo;
        }

        public int Execute(ListItemsOptions options)
        {
            var products = productRepo.GetAll();

            foreach (var prod in products)
            {
                Console.WriteLine($"{prod.Name} \t £{prod.Price}");
            }

            return 0;
        }
    }
}
