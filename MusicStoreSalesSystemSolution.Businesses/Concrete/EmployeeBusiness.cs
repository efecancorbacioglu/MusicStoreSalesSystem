using MusicStoreSalesSystemSolution.Businesses.Abstract;
using MusicStoreSalesSystemSolution.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace MusicStoreSalesSystemSolution.Businesses.Concrete
{
    public class EmployeeBusiness : IDatabaseBusiness<Employee>
    {
        public void Add(Employee entity)
        {
            using (var db = new MusicStoreDBEntities())
            {
                db.Employees.Add(entity);
                db.SaveChanges();
            }
        }

        public void Delete(Employee entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            using (var db = new MusicStoreDBEntities())
            {
                var employee = db.Employees.Find(id);
                db.Employees.Remove(employee);               
                db.SaveChanges();
            }
        }

        public void Edit(Employee entity)
        {
            using (var db = new MusicStoreDBEntities())
            {
                db.Employees.Attach(entity);
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public Employee Get(Expression<Func<Employee, bool>> expression)
        {
            using (var db = new MusicStoreDBEntities())
            {
                return db.Employees.Where(expression).FirstOrDefault();
            }
        }

        public List<Employee> GetAll()
        {
            using (var db = new MusicStoreDBEntities())
            {
                return db.Employees.ToList();
            }
        }
 
        public List<Employee> GetAll(Expression<Func<Employee, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Employee GetById(int id)
        {
            using (var db = new MusicStoreDBEntities())
            {
                return db.Employees.Where(p => p.EmployeeId == id).FirstOrDefault();
            }
        }
    }
}
