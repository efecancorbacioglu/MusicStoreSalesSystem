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
    public class SalesBusiness : IDatabaseBusiness<Sale>
    {
        public void Add(Sale entity)
        {
            throw new NotImplementedException();
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
