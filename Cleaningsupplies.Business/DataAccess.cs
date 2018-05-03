using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleaningSupplies.Database.Models;

namespace Cleaningsupplies.Business
{
    public class DataAccess
    {
        // Get: All Masters 
        public List<Master> GetAllMasters()
        {
            var context = new ApplicationDbContext();
            var result = from m in context.Master
                            select m;

            return result.ToList();
        }

        // Get: Master
        public Master GetMaster(int mastID)
        {
            var context = new ApplicationDbContext();
            var result = context.Master.Where(x => x.ID == mastID).FirstOrDefault();

            return result;
        }
    }
}
