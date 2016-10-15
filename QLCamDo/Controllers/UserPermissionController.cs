using QLCamDo.Bases;
using QLCamDo.Models;
using QLCamDo.Utilities;
using QLCamDo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace QLCamDo.Controllers
{
    public class UserPermissionController : BaseController
    {
        UserPermissionRepository _repository = new UserPermissionRepository();
        UserModuleRepository _userModuleRepository = new UserModuleRepository();
        UserRoleRepository _roleRepository = new UserRoleRepository();
        private List<UserPermissionViewModel> lstPermission = null;
        int i = 0;
        public List<UserPermissionViewModel> GetAll(int roleId, int parentId)
        {
            if (lstPermission == null)
                lstPermission = new List<UserPermissionViewModel>();
            string pref = string.Empty;
            var lstParent = _userModuleRepository.GetAll(parentId);
            if (lstParent != null)
            {
                foreach (var obj in lstParent)
                {
                    UserPermission userPermission = _repository.GetById((int)roleId, obj.Id);

                    //for (int j = 0; j < i; j++)
                    pref += parentId == int.MinValue ? string.Empty : "--";
                    lstPermission.Add(new UserPermissionViewModel { RoleId = (int)roleId, Access = userPermission == null ? false : userPermission.Access, ModuleId = obj.Id, ModuleName = pref + " " + obj.Name });
                    GetAll(roleId, obj.Id);
                    pref = string.Empty;
                    i++;
                }
            }
            return lstPermission;
        }
        // GET: UserPermission
        public ActionResult Index(int? roleId)
        {
            InitPermission(Request);
            //InitMessage("permission");

            if (roleId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.Role = (int)roleId;
            ViewBag.RoleName = _roleRepository.GetById((int)roleId).RoleName;
            return View(GetAll((int)roleId, int.MinValue));
        }

        [HttpPost]
        public JsonResult Update()
        {
            string func = Request.Form["fn"];
            switch (func.ToLower())
            {
                case "update-access-all":
                    string role = Request.Form["role"];
                    string ids = Request.Form["id"];
                    string[] idItems = ids.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var obj in idItems)
                    {
                        int moduleId = int.Parse(obj.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries)[0]);
                        bool isAccess = obj.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries)[1] == "1";
                        _repository.Update(int.Parse(role), moduleId, isAccess);
                    }
                    //Session[GlobalConst.SessionMessage] = HtmlUtility.ActionType.Updated;
                    return Json(new { message = HtmlUtility.BuildMessageTemplate("Update permission success", HtmlUtility.MessageType.Success) });
            }
            return Json(new object { });
        }
    }
}