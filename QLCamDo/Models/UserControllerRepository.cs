using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCamDo.Models
{
    public class UserControllerRepository
    {
        public bool Insert(UserController obj)
        {
            try
            {
                using (var db = new CamDoEntities())
                {
                    var temp = db.UserControllers.FirstOrDefault(o => o.Name == obj.Name && o.Action == obj.Action);
                    if (temp != null)
                        return false;
                    db.UserControllers.Add(obj);
                    db.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        public List<UserController> GetActions(string controller)
        {
            try
            {
                var db = new CamDoEntities();
                {
                    var temp = db.UserControllers.Where(o => o.Name == controller);
                    if (temp.Count() == 0)
                        return null;                  
                    return temp.ToList();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<UserController> GetControlles()
        {
            try
            {
                var db = new CamDoEntities();
                {
                    var temp = db.UserControllers.GroupBy(p => p.Name).Select(grp => grp.FirstOrDefault());
                    if (temp.Count() == 0)
                        return null;                    
                    return temp.ToList();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
