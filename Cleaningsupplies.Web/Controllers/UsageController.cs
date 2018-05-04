using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cleaningsupplies.Web.Models;
using CleaningSupplies.Database.Models;
using Microsoft.AspNet.Identity;

namespace Cleaningsupplies.Web.Controllers
{
    [Authorize]

    public class UsageController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        //
        // GET: /CleaningUsage/

        public ActionResult Index()
        {
            //List<UsageModel> items = UsageModel.GetItems();

            // query product (master)
            List<UsageVM> ListVM = new List<UsageVM>();

            var masters = (from m in db.Master select m).ToList();

            foreach (Master m in masters)
            {
                UsageVM vm = new UsageVM
                {
                    ID = m.ID,
                    Description = m.Description,
                    QuantityInStock = SumProdInvQty.GetSum(m.ID),
                    Quantity_modified = 0
                };

                ListVM.Add(vm);
            }

            return View(ListVM);

        }

        public JsonResult JsonUpdate(UsageVM model)
        {
            string message = "Update Successful";

            if (model.Quantity_modified == 0)
            {
                message = "No Update - Qty is Zero";
                return Json(message, JsonRequestBehavior.AllowGet);
            }

            var userID = db.Users.Find(User.Identity.GetUserId());

            Master master = db.Master.Find(model.ID);

            if (master == null)
            {
                message = "Update Failed";

                return Json(message, JsonRequestBehavior.AllowGet);
            }

            Usage usage = new Usage
            {
                Item = model.Description,
                Quantity_modified = model.Quantity_modified,
                CreatedByDateTime = DateTime.Now,
                CreatedById = userID,
                ModifiedByDatetime = DateTime.Now,
                ModifiedById = userID,
                GetMasterT = db.Master.Find(model.ID)
            };

            db.Entry(usage).State = EntityState.Added;

            db.SaveChanges();

            if (model.Quantity_modified > 0)
            {
                message = "Update Successful - Qty of " + model.Quantity_modified + " Added";
            }
            else if (model.Quantity_modified < 0)
            {
                message = "Update Successful - Qty of " + model.Quantity_modified + " Removed";
            }


            return Json(message, JsonRequestBehavior.AllowGet);

        }

    }
}