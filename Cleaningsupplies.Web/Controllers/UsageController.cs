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

namespace Cleaningsupplies.Web.Controllers
{
    [Authorize]

    public class UsageController : Controller
    {


        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Usage
        public ActionResult Index()
        {
            var result = from m in db.Master
            select m;

            return View(result.ToList());

            //return View(db.Usage.ToList());
        }

        // GET: Usage/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usage Usage = db.Usage.Find(id);
            if (Usage == null)
            {
                return HttpNotFound();
            }
            return View(Usage);
        }

        // GET: Usage/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Usage/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create([Bind(Include = "ID,Item,Quantity_modified,CreatedByDateTime,ModifiedByDatetime")] Usage Usage)
        {
            ModelState["CreatedById"].Errors.Clear();
            if (ModelState.IsValid)
            {
                Usage.CreatedById = db.Users.Find(User.Identity.GetUserId());  //Requires "using Microsoft.AspNet.Identity;"
                Usage.ModifiedById = db.Users.Find(User.Identity.GetUserId());
                Usage.CreatedByDateTime = DateTime.UtcNow;
                Usage.ModifiedByDatetime = DateTime.UtcNow;
                db.Usage.Add(Usage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(Usage);
        }

        // GET: Usage/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usage Usage = db.Usage.Find(id);
            if (Usage == null)
            {
                return HttpNotFound();
            }
            return View(Usage);
        }

        // POST: Usage/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Description,QuantityInStock,IsDeleted,CreatedById,CreatedByDateTime,ModifiedByDatetime")] Usage Usage)
        {
            ModelState["CreatedById"].Errors.Clear();

            if (ModelState.IsValid)
            {
                Usage.CreatedById = db.Users.Find(User.Identity.GetUserId());  //Requires "using Microsoft.AspNet.Identity;"
                Usage.ModifiedById = db.Users.Find(User.Identity.GetUserId());
                Usage.ModifiedByDatetime = DateTime.UtcNow;

                db.Entry(Usage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(Usage);
        }

        // GET: Usage/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usage Usage = db.Usage.Find(id);
            if (Usage == null)
            {
                return HttpNotFound();
            }
            return View(Usage);
        }

        // POST: Usage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Usage Usage = db.Usage.Find(id);
            db.Usage.Remove(Usage);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}