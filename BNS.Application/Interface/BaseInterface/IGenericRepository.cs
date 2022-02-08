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
        Task<T> GetByIdAsync(Guid id);

        Task<T> GetDefaultAsync(Expression<Func<T, bool>> filter = null
                                                  );
        Task<IQueryable<T>> GetAsync(Expression<Func<T, bool>> filter = null
                                                  , Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null
                                                  );
        Task<IQueryable<T>> GetAsync(Expression<Func<T, bool>> filter = null
                                                  , Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null 
                                                  , params Expression<Func<T, object>>[] includeProperties  
                                                  );

        Task<IQueryable<T>> GetAsync(Expression<Func<T, bool>> filter = null
                                                  , Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null
                                                  , string sortColumnName = null, bool? isAscending = true 
                                                  , params Expression<Func<T, object>>[] includeProperties  
                                                  );


        Task<IQueryable<T>> GetAsync(IQueryable<T> query,Expression<Func<T, bool>> filter = null
                                                  , Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null
                                                  , string sortColumnName = null, bool? isAscending = true
                                                  , params Expression<Func<T, object>>[] includeProperties
                                                  );
        Task<IQueryable<T>> OrderBy  (IQueryable<T> source, string columnName, bool isAscending = true);
        Task<int> AddAsync(T entity);
        Task<int> DeleteAsync(T entity);
        Task<int> UpdateAsync(T entity);
    }
}
