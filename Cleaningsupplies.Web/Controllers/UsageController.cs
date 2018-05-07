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
                ViewBag.parm = TempData["parm"].ToString();
            }
            else
            {
                ViewBag.message = null;
                ViewBag.parm = null;
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
            string parm = null;

            TempData["message"] = null;
            TempData["parm"] = null;

            if (model.Quantity_modified == 0)
            {
                message = "No Update: Qty is Zero";
                return Json(message, JsonRequestBehavior.AllowGet);
            }

            Master master = db.Master.Find(model.ID);

            if (master == null)
            {
                message = "Update Failed: Contact System Administrator";

                return Json(message, JsonRequestBehavior.AllowGet);
            }

            //compute the quantity on hand before update
            //if qty on hand + qty modified is less than 0, qty will go below zero
            int qtyOnHand = QueryMethods.GetProdInvSum(master.ID);

            if (qtyOnHand + model.Quantity_modified < 0)
            {
                message = "No Update: Qty In Stock Will Go Below Zero";

                return Json(message, JsonRequestBehavior.AllowGet);
            }

            var userID = db.Users.Find(User.Identity.GetUserId());

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

            //compute the quantity on hand after update
            //if qty on hand is less than or equal to threshold, alert user
            qtyOnHand = QueryMethods.GetProdInvSum(master.ID);
            if (qtyOnHand <= master.MinimumValue)
            {
                message = "Update Successful";
                parm = "Alert Threshold Met";
            }
            else
            {
                message = "Update Successful";
                parm = "";
            }

            TempData["message"] = message;
            TempData["parm"] = parm;
            return Json(new
            {
                redirectUrl = Url.Action("Index", "Usage"),
                isRedirect = true
            });
        }
    }
}