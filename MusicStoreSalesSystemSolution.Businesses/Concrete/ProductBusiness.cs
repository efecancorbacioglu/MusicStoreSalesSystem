using MusicStoreSalesSystemSolution.Businesses.Abstract;
using MusicStoreSalesSystemSolution.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MusicStoreSalesSystemSolution.Businesses.Concrete
{
    public class ProductBusiness : IDatabaseBusiness<Product>
    {
        public void Add(Product entity)
        {
            using (var db = new MusicStoreDBEntities())
            {
                db.Products.Add(entity);
                db.SaveChanges();
            }
        }

        public void Delete(Product entity)
        {
            using (var db = new MusicStoreDBEntities())
            {
                db.Products.Attach(entity);
                db.Entry(entity).State = EntityState.Deleted;
                db.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            using (var db = new MusicStoreDBEntities())
            {
                var product = db.Products.Find(id);
                product.IsActive = false;
                db.SaveChanges();
            }
        }

        public void Edit(Product entity)
        {
            using (var db = new MusicStoreDBEntities())
            {
                db.Products.Attach(entity);
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public Product Get(Expression<Func<Product, bool>> expression)
        {
            using (var db = new MusicStoreDBEntities())
            {
                return db.Products.Where(expression).FirstOrDefault();
            }
        }

        public List<SP_GetProducts_Result> GetAll()
        {
            using (var db = new MusicStoreDBEntities())
            {
                return db.SP_GetProducts().Where(p => p.IsActive == true).ToList();
            }
        }

        public List<Product> GetAll(Expression<Func<Product, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Product GetById(int id)
        {
            using (var db = new MusicStoreDBEntities())
            {
                return db.Products.Where(p => p.ProductId == id).FirstOrDefault();
            }
        }

        List<Product> IDatabaseBusiness<Product>.GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
