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
    public class MasterController : Controller
    {


        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Master
        public ActionResult Index()
        {
            return View(db.Master.ToList());
        }

        // GET: Master/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Master master = db.Master.Find(id);
            if (master == null)
            {
                return HttpNotFound();
            }
            return View(master);
        }

        // GET: Master/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Master/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Description,QuantityInStock,IsDeleted,CreatedByDateTime,ModifiedByDatetime")] Master master)
        {
            ModelState["CreatedById"].Errors.Clear();
            if (ModelState.IsValid)
            {
                 master.CreatedById = db.Users.Find(User.Identity.GetUserId());  //Requires "using Microsoft.AspNet.Identity;"
                master.ModifiedById = db.Users.Find(User.Identity.GetUserId());
                 master.CreatedByDateTime = DateTimeOffset.UtcNow;
                master.ModifiedByDatetime = DateTimeOffset.UtcNow;
                db.Master.Add(master);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(master);
        }

        // GET: Master/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Master master = db.Master.Find(id);
            if (master == null)
            {
                return HttpNotFound();
            }
            return View(master);
        }

        // POST: Master/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Description,QuantityInStock,IsDeleted,CreatedById,CreatedByDateTime,ModifiedByDatetime")] Master master)
        {
            ModelState["CreatedById"].Errors.Clear();

            if (ModelState.IsValid)
            {
                master.CreatedById = db.Users.Find(User.Identity.GetUserId());  //Requires "using Microsoft.AspNet.Identity;"
                master.ModifiedById = db.Users.Find(User.Identity.GetUserId());
                master.ModifiedByDatetime = DateTimeOffset.UtcNow;
                
                db.Entry(master).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(master);
        }

        // GET: Master/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Master master = db.Master.Find(id);
            if (master == null)
            {
                return HttpNotFound();
            }
            return View(master);
        }

        // POST: Master/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Master master = db.Master.Find(id);
            db.Master.Remove(master);
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
