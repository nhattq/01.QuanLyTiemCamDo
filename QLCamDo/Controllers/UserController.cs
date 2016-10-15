using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Net;
using QLCamDo.Bases;
using QLCamDo.Utilities;
using QLCamDo.Models;
using QLCamDo.Helpers;
using QLCamDo.ViewModels;

namespace QLCamDo.Controllers
{
    public class UserController : BaseController
    {
        UserRepository _userRepository = new UserRepository();
        public ActionResult Index()
        {
            InitPermission(Request);
            InitMessage("người dùng");
            return View(_userRepository.GetAll());
        }

        public ActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordFormModel entity)
        {
            if (CurrentUser == null)
                return RedirectToAction("Login", "User");

            if (entity.ConfirmNewPassword != entity.NewPassword)
            {
                ViewBag.Message = HtmlUtility.BuildMessageTemplate("Mật khẩu xác nhận không chính xác", HtmlUtility.MessageType.Warning);
                return View(entity);
            }

            EncryptHelper encrypt = new EncryptHelper();

            if (encrypt.Encrypt(entity.OldPassword) != CurrentUser.Password)
            {
                ViewBag.Message = HtmlUtility.BuildMessageTemplate("Mật khẩu hiện tại không chính xác", HtmlUtility.MessageType.Warning);
                return View(entity);
            }
            User objUser = _userRepository.GetById(CurrentUser.Id);
            objUser.Password = encrypt.Encrypt(entity.NewPassword);

            if (_userRepository.Update(objUser))
            {
                ViewBag.Message = HtmlUtility.BuildMessageTemplate("Cập nhật mật khẩu thành công", HtmlUtility.MessageType.Success);
                return View(entity);
            }
            ViewBag.Message = HtmlUtility.BuildMessageTemplate("Cập nhật mật khẩu không thành công", HtmlUtility.MessageType.Error);
            return View(entity);
        }

        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginFormModel entity)
        {
            if (entity == null)
            {
                ViewBag.Message = HtmlUtility.BuildMessageTemplate("Vui lòng nhập đầy đủ thông tin.", HtmlUtility.MessageType.Warning);
                return View(entity);
            }
            User objUser = _userRepository.Get(entity.Username, new EncryptHelper().Encrypt(entity.Password));
            if (objUser == null)
            {
                ViewBag.Message = HtmlUtility.BuildMessageTemplate("Tên đăng nhập hoặc mật khẩu không chính xác.", HtmlUtility.MessageType.Error);
                return View(entity);
            }

            if (!objUser.Active)
            {
                ViewBag.Message = HtmlUtility.BuildMessageTemplate("Tài khoản của bạn đang bị khóa.", HtmlUtility.MessageType.Error);
                return View(entity);
            }

            Session[GlobalConst.OAuthSession] = objUser;
            return RedirectToAction("Dashboard", "User");
        }

        public ActionResult Create()
        {
            InitPermission(Request);
            IEnumerable<UserRole> lstRoles = new UserRoleRepository().GetAll();
            ViewBag.RoleId = new SelectList(lstRoles, "Id", "RoleName");
            return View();
        }
        [HttpPost]
        public ActionResult Create(ViewModels.CreateUserFormModel entity)
        {
            IEnumerable<UserRole> lstRoles = new UserRoleRepository().GetAll();
            ViewBag.RoleId = new SelectList(lstRoles, "Id", "RoleName");

            if (_userRepository.GetByUsername(entity.Username) != null)
            {
                ViewBag.Message = HtmlUtility.BuildMessageTemplate("Tên đăng nhập đã tồn tại. Vui lòng chọn tên đăng nhập khác.", HtmlUtility.MessageType.Warning);
                return View(entity);
            }
            User obj = new User();

            obj.Password = new Helpers.EncryptHelper().Encrypt(entity.Password);
            obj.Username = entity.Username;
            obj.Fullname = entity.Fullname;
            obj.RoleId = entity.RoleId;
            obj.Active = entity.Active;
            obj.CreatedDate = DateTime.Now;
            obj.LastVisit = DateTime.Now;

            if (_userRepository.Insert(ref obj))
            {
                Session[Utilities.GlobalConst.SessionMessage] = (int)HtmlUtility.ActionType.Created;
                return RedirectToAction("Index");
            }
            else
                ViewBag.Message = HtmlUtility.BuildMessageTemplate("Thêm mới người dùng không thành công.", HtmlUtility.MessageType.Error);
            return View(entity);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewModels.CreateUserFormModel obj = new ViewModels.CreateUserFormModel();
            User objUser = _userRepository.GetById((int)id);
            obj.Active = objUser.Active;
            obj.ConfirmPassword = new Helpers.EncryptHelper().Decrypt(objUser.Password);
            obj.Password = new Helpers.EncryptHelper().Decrypt(objUser.Password);
            obj.Username = objUser.Username;
            obj.Fullname = objUser.Fullname;
            obj.RoleId = objUser.RoleId;
            obj.Id = objUser.Id;

            IEnumerable<UserRole> lstRoles = new UserRoleRepository().GetAll();
            ViewBag.RoleId = new SelectList(lstRoles, "Id", "RoleName", objUser.RoleId);
            return View(obj);
        }
        [HttpPost]
        public ActionResult Edit(ViewModels.CreateUserFormModel entity)
        {
            if (entity == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            IEnumerable<UserRole> lstRoles = new UserRoleRepository().GetAll();
            ViewBag.RoleId = new SelectList(lstRoles, "Id", "RoleName");
         
            User objUser = _userRepository.GetById(entity.Id);

            if (objUser.Username != entity.Username && _userRepository.GetByUsername(entity.Username) != null)
            {
                ViewBag.Message = HtmlUtility.BuildMessageTemplate("Tên đăng nhập đã tồn tại. Vui lòng chọn tên đăng nhập khác", HtmlUtility.MessageType.Warning);
                return View(entity);
            }

            //objUser.Password = _encrypt.Encrypt(entity.Password);
            objUser.Username = entity.Username;
            objUser.Fullname = entity.Fullname;
            objUser.RoleId = entity.RoleId;
            objUser.Active = entity.Active;

            if (_userRepository.Edit(objUser))
            {
                Session[Utilities.GlobalConst.SessionMessage] = (int)Utilities.HtmlUtility.ActionType.Updated;
                return RedirectToAction("Index");
            }
            else
                ViewBag.Message = Utilities.HtmlUtility.BuildMessageTemplate("Cập nhật người dùng không thành công. Vui lòng kiểm tra lại thông tin", Utilities.HtmlUtility.MessageType.Error);
            return View(entity);
        }

        public ActionResult Logout()
        {
            Session[GlobalConst.OAuthSession] = null;
            return RedirectToAction("Login", "User");
        }
    }
}