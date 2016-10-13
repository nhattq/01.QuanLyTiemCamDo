using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCamDo.Models
{
    public class UserRepository
    {
        public User Get(string username, string password)
        {
            try
            {
                var db = new CamDoEntities();
                return db.Users.FirstOrDefault(o => o.Username == username && o.Password == password);
            }
            catch
            {
                return null;
            }
        }

        internal bool Insert(User user)
        {
            try
            {
                var db = new CamDoEntities();
                var temp = db.Users.FirstOrDefault(o => o.Username == user.Username);
                if (temp != null)
                    return false;

                db.Users.Add(user);
                db.SaveChanges();
                return true;

            }
            catch
            {
                return false;
            }
        }

        internal List<User> GetAll()
        {
            try
            {
                var db = new CamDoEntities();
                var temp = db.Users;
                return temp.Count() == 0 ? null : temp.OrderByDescending(o => o.CreatedDate).ToList();

            }
            catch
            {
                return null;
            }
        }

        internal bool Insert(ref User ObjUser)
        {
            try
            {
                var user = ObjUser;
                var db = new CamDoEntities();
                var temp = db.Users.FirstOrDefault(o => o.Username == user.Username);
                if (temp != null)
                    return false;

                db.Users.Add(user);
                db.SaveChanges();
                return true;

            }
            catch
            {
                return false;
            }
        }

        internal bool Update(User objUser)
        {
            try
            {
                var db = new CamDoEntities();
                var temp = db.Users.FirstOrDefault(o => o.Id == objUser.Id);
                if (temp == null)
                    return false;

                temp.Password = objUser.Password;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        internal User GetById(int id)
        {
            try
            {
                var db = new CamDoEntities();
                return db.Users.FirstOrDefault(o => o.Id == id);
            }
            catch
            {
                return null;
            }
        }

        internal object GetByUsername(string username)
        {
            try
            {
                var db = new CamDoEntities();
                return db.Users.FirstOrDefault(o => o.Username == username);
            }
            catch
            {
                return null;
            }
        }

        internal bool Edit(User objUser)
        {
            try
            {
                var db = new CamDoEntities();
                var temp = db.Users.FirstOrDefault(o => o.Id == objUser.Id);
                if (temp == null)
                    return false;
                temp.Active = objUser.Active;
                temp.Fullname = objUser.Fullname;
                temp.Password = objUser.Password;
                temp.RoleId = objUser.RoleId;
                //temp.StationId = objUser.StationId;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
