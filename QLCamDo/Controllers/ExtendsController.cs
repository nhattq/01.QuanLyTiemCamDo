using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QLCamDo.Models;

namespace QLCamDo.Controllers
{
    public class ExtendsController : Controller
    {
        private CamDoEntities db = new CamDoEntities();    

        public ActionResult Index(int? id)
        {
            var extends = db.Extends.Include(e => e.Contract);
            if (id != null)
                extends = extends.Where(o => o.ContractId == id);

            return View(extends.ToList());
        }

        // GET: Extends/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Extend extend = db.Extends.Find(id);
            if (extend == null)
            {
                return HttpNotFound();
            }
            return View(extend);
        }

        // GET: Extends/Create
        public ActionResult Create(string id)
        {
            ViewBag.ContractId = new SelectList(db.Contracts, "Id", "Code", id);
            return View();
        }

        // POST: Extends/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ContractId,CreatedDate,ExpiredDate,Times")] Extend extend)
        {
            extend.CreatedDate = DateTime.Now;
            extend.Times = db.Extends.Count(o => o.ContractId == extend.ContractId) + 1;
            if (ModelState.IsValid)
            {
                db.Extends.Add(extend);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ContractId = new SelectList(db.Contracts, "Id", "Code", extend.ContractId);
            return View(extend);
        }

        // GET: Extends/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Extend extend = db.Extends.Find(id);
            if (extend == null)
            {
                return HttpNotFound();
            }
            ViewBag.ContractId = new SelectList(db.Contracts, "Id", "Code", extend.ContractId);
            return View(extend);
        }

        // POST: Extends/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ContractId,CreatedDate,ExpiredDate,Times")] Extend extend)
        {
            if (ModelState.IsValid)
            {
                db.Entry(extend).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ContractId = new SelectList(db.Contracts, "Id", "Code", extend.ContractId);
            return View(extend);
        }

        // GET: Extends/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Extend extend = db.Extends.Find(id);
            if (extend == null)
            {
                return HttpNotFound();
            }
            return View(extend);
        }

        // POST: Extends/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Extend extend = db.Extends.Find(id);
            db.Extends.Remove(extend);
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
