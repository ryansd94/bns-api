using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BNS.Domain
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByIdAsync(Guid id);

        Task<T> GetDefaultAsync(Expression<Func<T, bool>> filter = null
                                                  );
        Task<T> GetDefaultAsync(Expression<Func<T, bool>> filter = null
                                , params Expression<Func<T, object>>[] includeProperties
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


        Task<IQueryable<T>> GetAsync(IQueryable<T> query, Expression<Func<T, bool>> filter = null
                                                  , Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null
                                                  , string sortColumnName = null, bool? isAscending = true
                                                  , params Expression<Func<T, object>>[] includeProperties
                                                  );
        Task<IQueryable<T>> OrderBy(IQueryable<T> source, string columnName, bool isAscending = true);
        Task AddAsync(T entity);
        Task DeleteAsync(T entity);
        Task UpdateAsync(T entity);
        Task<int> CountAsync(Expression<Func<T, bool>> filter = null);
        Task<int> SaveChangesAsync();
    }
}
