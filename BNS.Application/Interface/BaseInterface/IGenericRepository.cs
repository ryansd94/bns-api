using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BNS.Application.Interface
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetById(Guid id);
        Task<IEnumerable<T>> GetAll();
        Task<int> Add(T entity);
        Task<int> Delete(T entity);
        Task<int> Update(T entity);
    }
}
