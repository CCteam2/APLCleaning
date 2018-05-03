using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cleaningsupplies.Web.Models
{
    public class UsageViewModel
    {
        public int ID { get; set; }
        public string Description { get; set; }
        //public int QuantityInStock { get; set; }
        public int Quantity_modified { get; set; }

        public static List<UsageViewModel> GetItems()
        {
            List<UsageViewModel> items = new List<UsageViewModel>()
            {
                //new UsageViewModel (){ ID=1, Description="Clorox Bleach", QuantityInStock=10},
                //new UsageViewModel (){ ID=2, Description="409 All Purpose Cleaner", QuantityInStock=5},
                //new UsageViewModel (){ ID=3, Description="Clorox Gel", QuantityInStock=20},
                //new UsageViewModel (){ ID=4, Description="Bounty Paper Towels", QuantityInStock=25},
                //new UsageViewModel (){ ID=5, Description="Comet Cleanser", QuantityInStock=6},
            };
            return items;
        }
    }
}