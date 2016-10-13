using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Reflection;
using System.Web.Routing;
using QLCamDo.Utilities;
using QLCamDo.Models;

namespace QLCamDo.Bases
{
    public class BaseController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        ///  
        public void InitMessage(string type)
        {
            try
            {
                int status = Session[GlobalConst.SessionMessage] == null ? 0 : (int)Session[GlobalConst.SessionMessage];
                switch (status)
                {
                    case 1:
                        ViewBag.Message = HtmlUtility.BuildMessageTemplate(string.Format("Thêm mới {0} thành công", type), HtmlUtility.MessageType.Success);
                        break;
                    case 2:
                        ViewBag.Message = HtmlUtility.BuildMessageTemplate(string.Format("Cập nhật {0} thành công", type), HtmlUtility.MessageType.Success);
                        break;
                    case 3:
                        ViewBag.Message = HtmlUtility.BuildMessageTemplate(string.Format("Xóa {0} thành công", type), HtmlUtility.MessageType.Success);
                        break;
                    case 4:
                        ViewBag.Message = HtmlUtility.BuildMessageTemplate(string.Format("Nhập {0} thành công", type), HtmlUtility.MessageType.Success);
                        break;
                    case 5:
                        ViewBag.Message = HtmlUtility.BuildMessageTemplate(string.Format("Nhập {0} không thành công. Vui lòng kiểm tra lại file excel", type), HtmlUtility.MessageType.Error);
                        break;
                }
                Session[GlobalConst.SessionMessage] = null;
            }
            catch { }
        }

        public User CurrentUser
        {
            get
            {
                if (Session[GlobalConst.OAuthSession] == null)
                    RedirectToAction("Index", "Home");
                return (User)Session[GlobalConst.OAuthSession];
            }
        }

        UserModuleRepository _moduleRepository = new UserModuleRepository();
        UserPermissionRepository _permissionRepository = new UserPermissionRepository();
        public void InitPermission(HttpRequestBase request)
        {
            if (CurrentUser == null || CurrentUser.RoleId == 1)
                return;

            string actionName = (string)request.RequestContext.RouteData.Values["action"];
            string controllerName = (string)request.RequestContext.RouteData.Values["controller"];

            if (string.IsNullOrEmpty(actionName) || string.IsNullOrEmpty(controllerName))
            {
                Response.Redirect("/User/NoAccess");
            }

            UserModule objModule = _moduleRepository.Get(controllerName, actionName);
            if (objModule == null)
            {
                Response.Redirect("/User/NoAccess");
            }

            bool isAccessModule = _permissionRepository.Get(CurrentUser.RoleId, objModule.Id);
            if (!isAccessModule)
                Response.Redirect("/User/NoAccess");
        }
        protected override void Dispose(bool disposing)
        {

        }
    }
}