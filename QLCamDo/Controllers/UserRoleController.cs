using QLCamDo.Bases;
using QLCamDo.Models;
using QLCamDo.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace QLCamDo.Controllers
{
    public class UserRoleController : BaseController
    {
        private UserRoleRepository _repository = new UserRoleRepository();       
        // GET: UserRole
        public ActionResult Index()
        {
            InitPermission(Request);
            InitMessage("Vai trò người dùng");
            return View(_repository.GetAll());
        }

        public ActionResult Create()
        {
            InitPermission(Request);
            return View();
        }
        [HttpPost]
        public ActionResult Create(UserRole entity)
        {
            if (entity == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            entity.CreatedDate = DateTime.Now;
            entity.LastModified = DateTime.Now;

            if (_repository.Insert(entity))
            {
                Session[GlobalConst.SessionMessage] = (int)HtmlUtility.ActionType.Created;
                return RedirectToAction("Index");
            }
            ViewBag.Message = HtmlUtility.BuildMessageTemplate("Thêm mới vài trò người dùng không thành công. Vui lòng thử lại.", HtmlUtility.MessageType.Error);

            return View();
        }
        public ActionResult Edit(int? id)
        {
            InitPermission(Request);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(_repository.GetById((int)id));
        }
        [HttpPost]
        public ActionResult Edit(UserRole entity)
        {
            if (entity == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            entity.LastModified = DateTime.Now;
            if (_repository.Edit(entity))
            {
                Session[GlobalConst.SessionMessage] = (int)HtmlUtility.ActionType.Updated;
                return RedirectToAction("Index");
            }
            ViewBag.Message = HtmlUtility.BuildMessageTemplate("Cập nhật vai trò người dùng không thành công. Vui lòng thử lại.", HtmlUtility.MessageType.Error);

            return View();
        }
    }
}