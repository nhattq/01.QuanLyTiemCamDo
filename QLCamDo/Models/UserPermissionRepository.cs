using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCamDo.Models
{
    public class UserPermissionRepository
    {
        public void Update(int roleId, int moduleId, bool access)
        {
            try
            {
                using (var db = new CamDoEntities())
                {
                    var temp = db.UserPermissions.FirstOrDefault(o => o.RoleId == roleId && o.ModuleId == moduleId);
                    if (temp == null)
                    {
                        db.UserPermissions.Add(new UserPermission { Access = access, ModuleId = moduleId, RoleId = roleId });
                    }
                    else
                        temp.Access = access;
                    db.SaveChanges();
                }
            }
            catch
            {

            }
        }

        public bool Get(int roleId, int module)
        {
            try
            {
                using (var db = new CamDoEntities())
                {
                    var temp = db.UserPermissions.FirstOrDefault(o => o.RoleId == roleId & o.ModuleId == module);
                    return temp == null ? false : temp.Access;
                }
            }
            catch { return false; }
        }

        public List<string> Get(int roleId, string controller)
        {
            try
            {
                //controller += "Controller";
                //using (var db = new NLOPCEntities())
                //{
                //    var temp = db.UserPermissions.Where(o => o.RoleId == roleId
                //    & o.Controller.ToLower() == controller.ToLower());
                //    return temp.Count() == 0 ? null : temp.Select(o => o.Action).ToList();
                //}
                return null;
            }
            catch { return null; }
        }

        public UserPermission GetById(int roleId, int moduleId)
        {
            try
            {
                //controller += "Controller";
                using (var db = new CamDoEntities())
                {
                    return db.UserPermissions.FirstOrDefault(o => o.RoleId == roleId && o.ModuleId == moduleId);
                }
            }
            catch { return null; }
        }

        public bool IsAccessModule(string action, string controller, int roleId)
        {
            try
            {
                if (roleId == 1)
                    return true;

                using (var db = new CamDoEntities())
                {
                    var temp = db.UserPermissions.FirstOrDefault(o => o.UserModule.Action.ToLower() == action.ToLower() &&
                    o.UserModule.Controller.ToLower() == controller.ToLower() + "controller" &&
                    o.RoleId == roleId);

                    if (temp == null)
                        return false;
                    return temp.Access;   
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
