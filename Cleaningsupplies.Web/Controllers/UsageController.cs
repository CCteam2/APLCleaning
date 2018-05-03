﻿using System;
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

            var items = (from m in db.Master select m).ToList();

            return View(items);

        }

        public JsonResult JsonUpdate(UpdateUsage model)
        {
            string message = "Update Successful";

            if (model.Quantity_modified == 0)
            {
                message = "No Update - Quantity is Zero";
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

            if (model.Quantity_modified < 0)
            {
                message = "Update Successful - Quantity of " + model.Quantity_modified + "Added";
            }
            else if (model.Quantity_modified > 0)
            {
                message = "Update Successful - Quantity of " + model.Quantity_modified + "Removed";
            }

            ////compute quantity at hand. Alert user if qty at hand equals or less than alert qty
            //int qtyOnHand = QueryMethods.SumProductInventoryQuantityForId(model.ID);

            //if (qtyOnHand <= model.AlertThreshHold)
            //    message = "Update Successful & Alert ThreshHold Met";
            //else
            //if (model.Quantity_modified < 0) 
            //    message = "Update Successful - Quantity of " + model.Quantity_modified + "Added";
            //else
            //if (model.Quantity_modified > 0)
            //    message = "Update Successful - Quantity of " + model.Quantity_modified + "Removed";

            return Json(message, JsonRequestBehavior.AllowGet);

        }

    }
}