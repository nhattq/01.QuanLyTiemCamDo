using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QLCamDo.Models;
using QLCamDo.Bases;
using QLCamDo.Utilities;

namespace QLCamDo.Controllers
{
    public class ContractsController : BaseController
    {
        private CamDoEntities db = new CamDoEntities();

        // GET: Contracts
        public ActionResult Index()
        {
            InitPermission(Request);
            InitMessage("hợp đồng");
            var contracts = db.Contracts.Include(c => c.Customer).Include(c => c.User).Include(c => c.User1);
            return View(contracts.ToList());
        }

        // GET: Contracts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contract contract = db.Contracts.Find(id);
            if (contract == null)
            {
                return HttpNotFound();
            }
            return View(contract);
        }

        // GET: Contracts/Create
        public ActionResult Create()
        {
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name");
            return View();
        }

        // POST: Contracts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Code,CreatedDate,Description,Commodity,CustomerId,Status,UpdatedDate,CreatedBy,UpdatedBy")] Contract contract)
        {
            contract.Code = DateTime.Now.ToString("HHmmddMMyyyy");
            contract.CreatedBy = CurrentUser.Id;
            contract.CreatedDate = DateTime.Now;
            contract.Status = 1;//Đang hiệu lực
            if (ModelState.IsValid)
            {
                db.Contracts.Add(contract);
                db.SaveChanges();
                Session[Utilities.GlobalConst.SessionMessage] = (int)Utilities.HtmlUtility.ActionType.Created;
                return RedirectToAction("Index");
            }
            ViewBag.Message = HtmlUtility.BuildMessageTemplate("Thêm mới hợp đồng không thành công", HtmlUtility.MessageType.Error);
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name", contract.CustomerId);
            return View(contract);
        }

        // GET: Contracts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contract contract = db.Contracts.Include(c => c.Customer).Include(c => c.User).Include(c => c.User1).FirstOrDefault(o => o.Id == id);
            if (contract == null)
            {
                return HttpNotFound();
            }
            ViewBag.UpdatedBy = new SelectList(db.Users, "Id", "Fullname", contract.UpdatedBy);

            return View(contract);
        }

        // POST: Contracts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Contract obj)
        {
            Contract contract = db.Contracts.Include(c => c.Customer)
                .Include(c => c.User)
                .Include(c => c.User1)
                .FirstOrDefault(o => o.Id == obj.Id);

            contract.UpdatedBy = CurrentUser.Id;
            contract.UpdatedDate = DateTime.Now;
            contract.Status = obj.Status;
            contract.Description = obj.Description;
            contract.Commodity = obj.Commodity;

            if (ModelState.IsValid)
            {
                db.Entry(contract).State = EntityState.Modified;
                db.SaveChanges();
                Session[Utilities.GlobalConst.SessionMessage] = (int)Utilities.HtmlUtility.ActionType.Updated;
                return RedirectToAction("Index");
            }
            ViewBag.Message = HtmlUtility.BuildMessageTemplate("Cập nhật hợp đồng không thành công", HtmlUtility.MessageType.Error);
            return View(contract);
        }

        // GET: Contracts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contract contract = db.Contracts.Find(id);
            if (contract == null)
            {
                return HttpNotFound();
            }
            return View(contract);
        }

        // POST: Contracts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Contract contract = db.Contracts.Find(id);
            db.Contracts.Remove(contract);
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
