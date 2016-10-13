using System;
using System.Collections.Generic;
using System.Linq; 
using System.Linq.Expressions;

namespace QLCamDo.Models
{
    public class UserRoleRepository
    {
        public UserRoleRepository()
        {
        }

        public void Add(UserRole entity)
        {
            throw new NotImplementedException();
        }

        public bool Insert(UserRole entity)
        {
            try
            {
                using (var db = new CamDoEntities())
                {
                    var temp = db.UserRoles.FirstOrDefault(o=>o.RoleName==entity.RoleName);
                    if (temp != null)
                        return false;
                    db.UserRoles.Add(entity);
                    db.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        public bool Edit(UserRole entity)
        {
            try
            {
                using (var db = new CamDoEntities())
                {
                    var temp = db.UserRoles.FirstOrDefault(o => o.Id == entity.Id);
                    if (temp == null)
                        return false;
                    temp.Description = entity.Description;
                    temp.LastModified = entity.LastModified;
                    temp.RoleName = entity.RoleName;
                    temp.IsAccessControlCenter = entity.IsAccessControlCenter;
                    db.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        public void Delete(Expression<Func<UserRole, bool>> where)
        {
            throw new NotImplementedException();
        }

        public void Delete(UserRole entity)
        {
            throw new NotImplementedException();
        }

        public UserRole Get(Expression<Func<UserRole, bool>> where)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserRole> GetAll()
        {
            try
            {
                using (var db = new CamDoEntities())
                {
                    return db.UserRoles.ToList();
                }
            }
            catch(Exception exx)
            {
                return new List<UserRole>();
            }
        }

        public UserRole GetById(string id)
        {
            throw new NotImplementedException();
        }

        public UserRole GetById(long id)
        {
            try
            {
                using (var db = new CamDoEntities())
                {
                    return db.UserRoles.FirstOrDefault(o => o.Id == id);
                }
            }
            catch
            {
                return null;
            }
        }

        public IEnumerable<UserRole> GetMany(Expression<Func<UserRole, bool>> where)
        {
            throw new NotImplementedException();
        }  
    }  
}
