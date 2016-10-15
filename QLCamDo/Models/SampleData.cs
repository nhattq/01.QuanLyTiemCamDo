using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLCamDo.Models
{
    public class SampleData
    {
        public static void InsertSample()
        {
            new UserRoleRepository().Insert(new UserRole
            {
                Id = 1,
                CreatedDate = DateTime.Now,
                Description = "Administrator",
                LastModified = DateTime.Now,
                RoleName = "Administrator",
                IsAccessControlCenter=true,                   
            });

            new UserRepository().Insert(new User
            {
                Id = 1,
                Username = "administrator",
                Password = new Helpers.EncryptHelper().Encrypt("NhatLinh@1234"),
                Fullname = "Administrator",
                Active = true,
                RoleId = 1,
                CreatedDate = DateTime.Now,
                LastVisit = DateTime.Now,
            });

            AddControllerAndAction();
            var temp = new List<UserModule>
            {
                new UserModule {Id=1,Action="Index",Controller="UserModuleController",Name="Cấu hình hệ thống",ParentId=int.MinValue,CreatedDate=DateTime.Now,DisplayMenu=true,Icon="cog" },
                new UserModule {Id=2,Action="Index",Controller="UserModuleController",Name="Chức năng",ParentId=1,CreatedDate=DateTime.Now ,DisplayMenu=true ,Icon="sun-o"},
                new UserModule {Id=3,Action="Index",Controller="UserRoleController",Name="Vai trò người dùng",ParentId=1,CreatedDate=DateTime.Now,DisplayMenu=true,Icon="sun-o"  },
                new UserModule {Id=3,Action="Index",Controller="UserController",Name="Quản trị người dùng",ParentId=1,CreatedDate=DateTime.Now,DisplayMenu=true,Icon="users"  },
            };
            var module = new UserModuleRepository();
            foreach (var obj in temp)
            {
                module.Insert(obj);
            }

        }
        public static bool AddControllerAndAction()
        {
            System.Reflection.Assembly asm = System.Reflection.Assembly.GetAssembly(typeof(QLCamDo.MvcApplication));

            UserControllerRepository _controllerRepository = new UserControllerRepository();
            var controlleractionlist = asm.GetTypes()
                  .Where(type => typeof(System.Web.Mvc.Controller).IsAssignableFrom(type))
                  .SelectMany(type => type.GetMethods(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.DeclaredOnly | System.Reflection.BindingFlags.Public))
                  .Where(m => !m.GetCustomAttributes(typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute), true).Any())
                  .Select(x => new { Controller = x.DeclaringType.Name, Action = x.Name, Type = x.ReturnType.Name })
                  .OrderBy(x => x.Controller).ThenBy(x => x.Action).ToList();

            if (controlleractionlist.Count() != 0)
            {
                foreach (var obj in controlleractionlist)
                {
                    _controllerRepository.Insert(new UserController { Action = obj.Action, Name = obj.Controller, Type = obj.Type });
                }
            }
            return true;
        }
    }
}