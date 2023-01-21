using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MusicStoreSalesSystemSolution.Businesses.Abstract
{
    public interface IDatabaseBusiness<T> where T : class
    {
        T GetById(int id);

        T Get(Expression<Func<T, bool>> expression);

        List<T> GetAll();

        List<T> GetAll(Expression<Func<T, bool>> expression);

        void Add(T entity);

        void Edit(T entity);

        void Delete(T entity);

        void Delete(int id);
    }
}
