using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Core.Models;

namespace Shop.Core.Offers.Interfaces
{
    public interface IOffer
    {
        double ApplyDiscount(Basket basket, double currentTotal);
    }
}
