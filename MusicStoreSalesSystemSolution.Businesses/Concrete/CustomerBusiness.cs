using MusicStoreSalesSystemSolution.Businesses.Abstract;
using MusicStoreSalesSystemSolution.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MusicStoreSalesSystemSolution.Businesses.Concrete
{
    public class CustomerBusiness : IDatabaseBusiness<Customer>
    {
        public void Add(Customer entity)
        {
            using (var db = new MusicStoreDBEntities())
            {
                db.Customers.Attach(entity);
                db.Entry(entity).State = EntityState.Added;
                db.SaveChanges();
            }
        }

        public void Delete(Customer entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Edit(Customer entity)
        {
            throw new NotImplementedException();
        }

        public Customer Get(Expression<Func<Customer, bool>> expression)
        {
            using (var db = new MusicStoreDBEntities())
            {
               return db.Customers.Where(expression).FirstOrDefault();
            }
        }

        public List<Customer> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<Customer> GetAll(Expression<Func<Customer, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Customer GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
