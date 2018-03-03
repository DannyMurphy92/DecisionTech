using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Core.Models
{
    public class Basket
    {
        private static Basket instance;

        private Basket()
        {}

        public static Basket Instance()
        {
            return instance ?? (instance = new Basket());
        }
    }
}
