using QLCamDo.Bases;
using QLCamDo.Models;
using QLCamDo.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLCamDo.Controllers
{
    public class UserModuleController : BaseController
    {
        UserModuleRepository _repository = new UserModuleRepository();
        UserControllerRepository _userControllerRepository = new UserControllerRepository();
        // GET: UserModule

        public List<UserModule> GetAll(int parentId)
        {
            List<UserModule> lst = new List<UserModule>();
            var lstParent = _repository.GetAll(parentId);
            if (lstParent != null)
            {
                int i = 0;
                foreach (var obj in lstParent)
                {
                    i++;
                    lst.Add(obj);
                    var lstChildren = GetAll(obj.Id);
                    if (lstChildren != null)
                    {
                        foreach (var objChildren in lstChildren)
                        {
                            string pref = string.Empty;                             
                                pref += "--";
                            objChildren.Name = pref + " " + objChildren.Name;
                            lst.Add(objChildren); 
                        }
                    }
                }
            }
            return lst;
        }
        public ActionResult Index()
        {
            InitPermission(Request);
            InitMessage("Chức năng hệ thống");
            return View(GetAll(int.MinValue));
        }

        public ActionResult Create()
        {
            InitPermission(Request);

            List<UserModule> lst = GetAll(int.MinValue);
            lst.Insert(0, new UserModule { Id = int.MinValue, Name = "Root" });
            var lstController = _userControllerRepository.GetControlles();
            ViewBag.Controller = new SelectList(lstController, "Name", "Name");
            ViewBag.Module = new SelectList(lst, "Id", "Name");
            ViewBag.Action = new SelectList(_userControllerRepository.GetActions(lstController[0].Name), "Action", "Action");
            return View();
        }
        [HttpPost]
        public ActionResult Create(UserModule entity)
        {
            List<UserModule> lst = GetAll(int.MinValue);
            lst.Insert(0, new UserModule { Id = int.MinValue, Name = "Root" });

            ViewBag.Module = new SelectList(lst, "Id", "Name");
            var lstController = _userControllerRepository.GetControlles();
            ViewBag.Controller = new SelectList(lstController, "Name", "Name");
            ViewBag.Action = new SelectList(_userControllerRepository.GetActions(lstController[0].Name), "Action", "Action");

            string controller = Request.Form["Controller"];
            string action = Request.Form["Action"];
            string module = Request.Form["Module"];
            string icon = Request.Form["Icon"];
            bool isDisplayMenu = Request.Form["IsDisplayMenu"] == "on";

            if (string.IsNullOrEmpty(entity.Name))
            {
                ViewBag.Message = HtmlUtility.BuildMessageTemplate("Vui lòng nhập tên chức năng", HtmlUtility.MessageType.Warning);
                return View(entity);
            }

            UserModule obj = new UserModule();
            obj.Name = entity.Name;
            obj.Order = entity.Order;
            obj.Action = action;
            obj.Controller = controller;
            obj.ParentId = int.Parse(module);
            obj.DisplayMenu = isDisplayMenu;
            obj.CreatedDate = DateTime.Now;
            obj.Icon = icon;
            int result = _repository.Insert(obj);
            if (result == 1)
            {
                ViewBag.Message = HtmlUtility.BuildMessageTemplate("Chức năng đã tồn tại", HtmlUtility.MessageType.Warning);
                return View(entity);
            }

            if (result == 0)
            {
                Session[GlobalConst.SessionMessage] = (int)HtmlUtility.ActionType.Created;
                return RedirectToAction("Index");
            }
            ViewBag.Message = HtmlUtility.BuildMessageTemplate("Thêm mới chức năng không thành công", HtmlUtility.MessageType.Error);
            return View(entity);
        }

        public ActionResult Edit(int? id)
        {
            InitPermission(Request);
            List<UserModule> lst = GetAll(int.MinValue);

            UserModule objUsermodule = _repository.GetById((int)id);
            if (objUsermodule == null)
            {
                return HttpNotFound();
            }

            lst.Insert(0, new UserModule { Id = int.MinValue, Name = "Root" });

            var lstController = _userControllerRepository.GetControlles();
            ViewBag.Controller = new SelectList(lstController, "Name", "Name", objUsermodule.Controller);
            ViewBag.Module = new SelectList(lst, "Id", "Name", objUsermodule.ParentId);
            //ViewBag.Action = new SelectList(_userControllerRepository.GetActions(lstController[0].Name), "Action", "Action");
            return View(objUsermodule);
        }

        [HttpPost]
        public ActionResult Edit(UserModule entity)
        {
            List<UserModule> lst = GetAll(int.MinValue);

            lst.Insert(0, new UserModule { Id = int.MinValue, Name = "Root" });

            ViewBag.Module = new SelectList(lst, "Id", "Name");
            var lstController = _userControllerRepository.GetControlles();
            ViewBag.Controller = new SelectList(lstController, "Name", "Name");
            ViewBag.Action = new SelectList(_userControllerRepository.GetActions(lstController[0].Name), "Action", "Action");

            string controller = Request.Form["Controller"];
            string action = Request.Form["Action"];
            string module = Request.Form["Module"];
            bool isDisplayMenu = Request.Form["IsDisplayMenu"] == "on";
            string icon = Request.Form["Icon"];

            if (string.IsNullOrEmpty(entity.Name))
            {
                ViewBag.Message = HtmlUtility.BuildMessageTemplate("Vui lòng nhập tên chức năng", HtmlUtility.MessageType.Warning);
                return View(entity);
            }

            UserModule obj = new UserModule();
            obj.Id = entity.Id;
            obj.Name = entity.Name;
            obj.Order = entity.Order;
            obj.Action = action;
            obj.Controller = controller;
            obj.ParentId = int.Parse(module);
            obj.DisplayMenu = isDisplayMenu;
            obj.Icon = icon;
            bool result = _repository.Update(obj);
            if (result)
            {
                Session[GlobalConst.SessionMessage] = (int)HtmlUtility.ActionType.Updated;
                return RedirectToAction("Index");
            }
            ViewBag.Message = HtmlUtility.BuildMessageTemplate("Cập nhật chức năng không thành công", HtmlUtility.MessageType.Error);
            return View(entity);
        }


        [HttpPost]
        public JsonResult Delete()
        {
            try
            {
                string id = Request.Form["id"];
                if (string.IsNullOrEmpty(id))
                    return Json(new { status = 1, result = HtmlUtility.BuildMessageTemplate("Có lỗi xãy ra. Vui lòng thử lại sau.", HtmlUtility.MessageType.Error) });

                int Id = int.MinValue;

                if (!int.TryParse(id, out Id))
                {
                    return Json(new { status = 1, result = HtmlUtility.BuildMessageTemplate("Có lỗi xãy ra. Vui lòng thử lại sau.", HtmlUtility.MessageType.Error) });

                }
                _repository.Delete(Id);

                Session[GlobalConst.SessionMessage] = (int)HtmlUtility.ActionType.Deleted;

                return Json(new { status = 0, result = HtmlUtility.BuildMessageTemplate("Xóa chức năng thành công.", HtmlUtility.MessageType.Success) });

            }
            catch (Exception ex)
            {
                return Json(new { status = 1, result = HtmlUtility.BuildMessageTemplate("Delete module fail: " + ex.Message, HtmlUtility.MessageType.Error) });

            }
        }

        public JsonResult GetAction()
        {
            string controller = Request.Form["id"];
            List<Models.UserController> lstAction = _userControllerRepository.GetActions(controller);
            string html = string.Empty;
            if (lstAction != null)
            {
                foreach (var obj in lstAction)
                    html += "<option value=" + obj.Action + ">" + obj.Action + "</option>";
            }
            return Json(new { html = html });
        }

    }
}