using MusicStoreSalesSystemSolution.Businesses.Abstract;
using MusicStoreSalesSystemSolution.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MusicStoreSalesSystemSolution.Businesses.Concrete
{
    public class UserBusiness : IDatabaseBusiness<User>
    {
        public void Add(User entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(User entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Edit(User entity)
        {
            throw new NotImplementedException();
        }

        public User Get(Expression<Func<User, bool>> expression)
        {
            using (var db = new MusicStoreDBEntities())
            {
                return db.Users.Where(expression).FirstOrDefault();
            }
        }

        public List<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<User> GetAll(Expression<Func<User, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public User GetById(int id)
        {
            using (var db = new MusicStoreDBEntities())
            {
                return db.Users.Where(u => u.UserId == id).FirstOrDefault();
            }
        }

        public bool UserIsAdmin(string username)
        {
            using (var db = new MusicStoreDBEntities())
            {
                var user = db.Users.Where(u => u.UserName == username).FirstOrDefault();
                if (user.RoleId == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
