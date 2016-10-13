using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QLCamDo.ViewModels
{
    public class UserFormModel
    {
        public UserFormModel()
        {
            Login = new LoginFormModel();
            Register = new RegisterFormModel();
            ForgetPassword = new ForgetPasswordFormModel();
        }
        public LoginFormModel Login { get; set; }

        public RegisterFormModel Register { get; set; }

        public ForgetPasswordFormModel ForgetPassword { get; set; }

    }
    public class RegisterFormModel
    {
        [Required(ErrorMessage = "Please input field")]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please input field")]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please input field")]
        [RegularExpression(@"^(([A-Za-z0-9]+_+)|([A-Za-z0-9]+\-+)|([A-Za-z0-9]+\.+)|([A-Za-z0-9]+\++))*[A-Za-z0-9]+@((\w+\-+)|(\w+\.))*\w{1,63}\.[a-zA-Z]{2,6}$", ErrorMessage = "Invalid Email Address")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please input field")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Text)]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please input field")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Password Mismatch")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Please select a package service")]
        public int PackageId { get; set; }

    }

    public class CreateUserFormModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên hiển thị")]
        [Display(Name = "Tên hiển thị")]
        public string Fullname { get; set; }

        [Required(ErrorMessage = "PVui lòng nhập tên đăng nhập")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Text)]
        [Display(Name = "Tên đăng nhập")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Xác nhận mật khẩu")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Mật khẩu xác nhận không chính xác")]
        public string ConfirmPassword { get; set; }
        public int RoleId { get; set; }
        public int StationId { get; set; }
        public bool Active { get; set; }
    }

    public class LoginFormModel
    {
        [Required(ErrorMessage = "Vui lòng nhập tên đăng nhập")]
        //[RegularExpression(@"^(([A-Za-z0-9]+_+)|([A-Za-z0-9]+\-+)|([A-Za-z0-9]+\.+)|([A-Za-z0-9]+\++))*[A-Za-z0-9]+@((\w+\-+)|(\w+\.))*\w{1,63}\.[a-zA-Z]{2,6}$", ErrorMessage = "Invalid Email Address")]
        //[Display(Name = "Email")]
        [DataType(DataType.Text)]
        public string Username { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string StoreCode { get; set; }
        public string Password { get; set; }

        [Display(Name = "Ghi nhớ tôi?")]
        public bool RememberMe { get; set; }
    }

    public class ForgetPasswordFormModel
    {
        [Required(ErrorMessage = "Please input field")]
        [RegularExpression(@"^(([A-Za-z0-9]+_+)|([A-Za-z0-9]+\-+)|([A-Za-z0-9]+\.+)|([A-Za-z0-9]+\++))*[A-Za-z0-9]+@((\w+\-+)|(\w+\.))*\w{1,63}\.[a-zA-Z]{2,6}$", ErrorMessage = "Invalid Email Address")]
        [Display(Name = "Email")]
        [DataType(DataType.Text)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please input field")]
        [DataType(DataType.Password)]
        [Display(Name = "Username")]
        public string Username { get; set; }
    }

    public class ChangePasswordFormModel
    {
        [Required(ErrorMessage = "Vui lòng nhập")]
        [Display(Name = "Old Password")]
        [MinLength(8, ErrorMessage = "Mật khẩu > 8 ký tự")]
        [DataType(DataType.Text)]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập")]
        [DataType(DataType.Text)]
        [MinLength(8, ErrorMessage = "Mật khẩu > 8 ký tự")]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập")]
        [DataType(DataType.Text)]
        [Display(Name = "Confirm New Password")]
        [MinLength(8, ErrorMessage = "Mật khẩu > 8 ký tự")]
        [Compare("NewPassword", ErrorMessage = "Xác nhận mật khẩu không chính xác")]
        public string ConfirmNewPassword { get; set; }
    }

    public class ProfileFormModel
    {
        public int Id { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public bool? Gender { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public double? ZipCode { get; set; }
        public double? ContactNo { get; set; }
        public string UserId { get; set; }
    }
}