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
    public class SalesBusiness : IDatabaseBusiness<Sale>
    {
        public List<top_five_selling_products_Result> GetTopFiveSale()
        {
            using (MusicStoreDBEntities db = new MusicStoreDBEntities())
            {
                return db.top_five_selling_products().ToList();   
            }
        }

        public List<SP_Top_5_Seller_Result> GetTopFiveSeller()
        {
            using (MusicStoreDBEntities db = new MusicStoreDBEntities())
            {
                return db.SP_Top_5_Seller().ToList();
            }
        }


        public void Add(Sale entity)
        {
            using (var db = new MusicStoreDBEntities())
            {
                db.Sales.Attach(entity);
                db.Entry(entity).State = EntityState.Added;
                db.SaveChanges();
            }
        }

        public void Delete(Sale entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Edit(Sale entity)
        {
            throw new NotImplementedException();
        }

        public Sale Get(Expression<Func<Sale, bool>> expression)
        {
            throw new NotImplementedException();
        }



        public List<Sale> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<Sale> GetAll(Expression<Func<Sale, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Sale GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
