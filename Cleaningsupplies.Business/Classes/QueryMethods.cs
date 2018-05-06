using CleaningSupplies.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cleaningsupplies.Business.Classes
{
    public class QueryMethods
    {
        public static int GetProdInvSum(int id)
        {
            ApplicationDbContext db = new ApplicationDbContext();

            // get the sum for the quantity in the Usage table using the master id
            var results = (from p in db.Master
                           where (p.ID == id)
                           join pinv in db.Usage on p.ID equals pinv.GetMasterT.ID
                           orderby p.Description
                           select pinv).ToList().Select(pinv => pinv.Quantity_modified).Sum();

            return results;
        }

        // Get: All Masters 
        public static List<Master> GetAllMasters()
        {
            ApplicationDbContext db = new ApplicationDbContext();

            var result = db.Master;

            return result.ToList();
        }

        // Get: Master by ID
        public static Master GetMaster(int id)
        {
            ApplicationDbContext db = new ApplicationDbContext();

            var result = db.Master.Where(x => x.ID == id).FirstOrDefault();

            return result;
        }
    }
}