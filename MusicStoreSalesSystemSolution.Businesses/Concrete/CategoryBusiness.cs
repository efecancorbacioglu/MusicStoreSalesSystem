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
    public class CategoryBusiness : IDatabaseBusiness<Category>
    {
        public void Add(Category entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Category entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Edit(Category entity)
        {
            throw new NotImplementedException();
        }

        public Category Get(Expression<Func<Category, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public List<Category> GetAll()
        {
            using (var db = new MusicStoreDBEntities())
            {
                return db.Categories.ToList();
            }
        }

        public List<Category> GetAll(Expression<Func<Category, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Category GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
