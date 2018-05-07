using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CleaningSupplies.Database.Models;

namespace Cleaningsupplies.Web.Controllers
{
    [Authorize]

    public class ReportController : Controller


    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Report
        public ActionResult Index()
        {
            return View(db.Usage.ToList());
        }

        // GET: Report/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usage usage = db.Usage.Find(id);
            if (usage == null)
            {
                return HttpNotFound();
            }
            return View(usage);
        }

        // GET: Report/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Report/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Item,Quantity_modified,CreatedByDateTime,ModifiedByDatetime")] Usage usage)
        {
            if (ModelState.IsValid)
            {
                db.Usage.Add(usage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(usage);
        }

        // GET: Report/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usage usage = db.Usage.Find(id);
            if (usage == null)
            {
                return HttpNotFound();
            }
            return View(usage);
        }

        // POST: Report/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Item,Quantity_modified,CreatedByDateTime,ModifiedByDatetime")] Usage usage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(usage);
        }

        // GET: Report/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usage usage = db.Usage.Find(id);
            if (usage == null)
            {
                return HttpNotFound();
            }
            return View(usage);
        }

        // POST: Report/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Usage usage = db.Usage.Find(id);
            db.Usage.Remove(usage);
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
