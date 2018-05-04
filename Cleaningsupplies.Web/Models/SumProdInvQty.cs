using CleaningSupplies.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cleaningsupplies.Web.Models
{
    public class SumProdInvQty
    {
        public static int GetSum(int id)
        {
            ApplicationDbContext db = new ApplicationDbContext();

            // get the sum for the quantity in Usage table
            var results = (from p in db.Master
                           where (p.ID == id)
                           join pinv in db.Usage on p.ID equals pinv.GetMasterT.ID
                           orderby p.Description
                           select pinv).ToList().Select(pinv => pinv.Quantity_modified).Sum();

            return results;
        }
    }
}