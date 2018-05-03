using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cleaningsupplies.Web.Models;

namespace EditableWebgrid.Controllers
{
    [Authorize]

    public class UsageController : Controller
    {
        //
        // GET: /CleaningUsage/

        public ActionResult Index()
        {
            List<UsageModel> items = UsageModel.GetItems();

            return View(items);

        }

        public JsonResult IncreaseQty(UsageModel model)
        {
            // Update model to your db
            //  foreach (var item in model) {
            //      model.ID = item.ID;
            //      model.Description = item.Description;
            //      model.QtyAvailable = item.QtyAvailable;
            //      model.UserQty = item.UserQty;
            //  }

            string message = "Success - Increase Quantity - Save Usage";
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DecreaseQty(UsageModel model)
        {
            // Update model to your db
            //  foreach (var item in model) {
            //      model.ID = item.ID;
            //      model.Description = item.Description;
            //      model.QtyAvailable = item.QtyAvailable;
            //      model.UserQty = item.UserQty * -1;
            //  }

            string message = "Success - Decrease Quantity - Usage";
            return Json(message, JsonRequestBehavior.AllowGet);
        }

    }
}