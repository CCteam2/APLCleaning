using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cleaningsupplies.Web.Models
{
    public class UpdateUsage
    {
        public int ID { get; set; }
        public string Description { get; set; }
        //public int QuantityInStock { get; set; }
        public int Quantity_modified { get; set; }
    }
}