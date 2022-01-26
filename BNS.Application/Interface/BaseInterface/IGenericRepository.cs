using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BNS.Application.Interface
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetById(Guid id);

        Task<IQueryable<T>> GetAsync(Expression<Func<T, bool>> filter = null
                                                  , Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null 
                                                  , params Expression<Func<T, object>>[] includeProperties  
                                                  );

        Task<IQueryable<T>> GetAsync(Expression<Func<T, bool>> filter = null
                                                  , Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null
                                                  , string sortColumnName = "", bool isAscending = true 
                                                  , Expression<Func<T, object>>[] includeProperties = null
                                                  );


        Task<int> Add(T entity);
        Task<int> Delete(T entity);
        Task<int> Update(T entity);
    }
}
