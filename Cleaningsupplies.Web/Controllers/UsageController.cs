using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cleaningsupplies.Business.Classes;
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
            //check for temp data message passed
            if (TempData.ContainsKey("message") && TempData["message"] != null)
            {
                ViewBag.message = TempData["message"].ToString();
            }
            else
            {
                ViewBag.message = "";
            }

            // create Usage View Model
            List<UsageVM> ListVM = new List<UsageVM>();

            // query Master creating Master List
            var masters = (from m in db.Master select m).ToList();

            // for each Master in the List:
            //  - create a Usage View Model object
            //  - populate this object with data from Master
            //  - summarize usage quantity modified 
            //  - add Usage View Model to List
            foreach (Master m in masters)
            {
                UsageVM vm = new UsageVM
                {
                    ID = m.ID,
                    Description = m.Description,
                    QuantityInStock = QueryMethods.GetProdInvSum(m.ID),
                    Quantity_modified = 0
                };

                ListVM.Add(vm);
            }

            return View(ListVM);

        }

        public JsonResult JsonUpdate(UsageVM model)
        {
            string message = null;
            TempData["message"] = null;

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

            //if (model.Quantity_modified > 0)
            //{
            //    message = "Update Successful - Qty of " + model.Quantity_modified + " Added";
            //}
            //else if (model.Quantity_modified < 0)
            //{
            //    message = "Update Successful - Qty of " + model.Quantity_modified * -1 + " Removed";
            //}

            ////compute quantity on hand. Alert user if qty on hand equals or less than alert qty
            //int qtyOnHand = QueryMethods.GetProdInvSum(m.ID);

            //if (qtyOnHand <= master.MinThreshold
            //    message = "Update Successful & Alert ThreshHold Met";

            if (message != null)
            {
                return Json(message, JsonRequestBehavior.AllowGet);
            }
            else
            {
                TempData["message"] = "Update Successful";
                return Json(new
                {
                    redirectUrl = Url.Action("Index", "Usage"),
                    isRedirect = true
                });
            }
        }
    }
}