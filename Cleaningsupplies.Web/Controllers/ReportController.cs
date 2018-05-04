using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CleaningSupplies.Database.Models;
using Microsoft.AspNet.Identity;

namespace Cleaningsupplies.Web
{
    public class ReportController : Controller
    {
        public ActionResult Report()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

    }
}