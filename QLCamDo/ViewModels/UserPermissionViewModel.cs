using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLCamDo.ViewModels
{
    public class UserPermissionViewModel
    {
        public int RoleId { get; set; }
        public int ModuleId { get; set; }
        public string ModuleName { get; set; }
        public bool Access { get; set; }
    }
}