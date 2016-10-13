using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCamDo.Models
{
    public class UserModuleRepository
    {
        public List<UserModule> GetAll()
        {
            try
            {
                using (var db = new CamDoEntities())
                {
                    var temp = db.UserModules;
                    return temp.Count() == 0 ? null : temp.ToList();
                }

            }
            catch { return null; }
        }

        public UserModule GetById(int id)
        {
            try
            {
                using (var db = new CamDoEntities())
                {
                    return db.UserModules.FirstOrDefault(o => o.Id == id);
                }

            }
            catch { return null; }
        }
        public UserModule Get(string controller, string action)
        {
            try
            {
                controller = controller + "Controller";
                using (var db = new CamDoEntities())
                {
                    return db.UserModules.FirstOrDefault(o => o.Controller == controller & o.Action == action);
                }

            }
            catch { return null; }
        }
        public List<UserModule> GetAll(int parentId)
        {
            try
            {
                using (var db = new CamDoEntities())
                {
                    var temp = db.UserModules.Where(o => o.ParentId == parentId);
                    return temp.Count() == 0 ? null : temp.ToList();
                }

            }
            catch { return null; }
        }
        public bool Delete(int id)
        {
            try
            {
                using (var db = new CamDoEntities())
                {
                    var temp = db.UserModules.FirstOrDefault(o => o.Id == id);
                    if (temp == null)
                        return false;
                    db.UserModules.Remove(temp);
                    db.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        public bool Update(UserModule obj)
        {
            try
            {
                using (var db = new CamDoEntities())
                {
                    var temp = db.UserModules.FirstOrDefault(o => o.Id == obj.Id);
                    if (temp == null)
                        return false;
                    temp.Action = obj.Action;
                    temp.Controller = obj.Controller;
                    temp.DisplayMenu = obj.DisplayMenu;
                    temp.Name = obj.Name;
                    temp.Order = obj.Order;
                    temp.ParentId = obj.ParentId;
                    temp.Icon = obj.Icon;
                    db.SaveChanges();
                    return true;
                }
            }
            catch
            {
                throw;
            }
        }
        public int Insert(UserModule obj)
        {
            try
            {
                using (var db = new CamDoEntities())
                {
                    var temp = db.UserModules.FirstOrDefault(o => o.Action == obj.Action && o.Controller == obj.Controller && o.ParentId == obj.ParentId);
                    if (temp != null)
                        return 1;
                    db.UserModules.Add(obj);
                    db.SaveChanges();
                    return 0;
                }
            }
            catch
            {
                return 2;
            }
        }
    }
}
