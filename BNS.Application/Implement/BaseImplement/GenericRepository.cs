using BNS.Application.Interface;
using BNS.Data.Entities;
using BNS.Data.EntityContext;
using BNS.Utilities;
using LazZiya.ExpressLocalization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BNS.Application.Implement
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly BNSDbContext _context;
        public GenericRepository(BNSDbContext context)
        {
            _context = context;
        }

        public static void SetCreatedUpdatedUsername<D>(List<D> data, List<CF_Account> accounts)
        {
            foreach (var item in data)
            {
                var createdUserId = item.GetType().GetProperty("CreatedUserId");
                var createdUserIdValue = createdUserId.GetValue(item, null);
                if (createdUserIdValue != null)
                {
                    var user = accounts.Where(s => s.Id == Guid.Parse(createdUserIdValue.ToString())).FirstOrDefault();
                    item.GetType().GetProperty("CreatedUser").SetValue(item, user != null ? user.Cf_Employee.EmployeeName : string.Empty);
                }

                var updatedUserId = item.GetType().GetProperty("UpdatedUserId");
                var updatedUserIdValue = updatedUserId.GetValue(item, null);
                if (updatedUserIdValue != null)
                {
                    var user = accounts.Where(s => s.Id == Guid.Parse(updatedUserIdValue.ToString())).FirstOrDefault();
                    item.GetType().GetProperty("UpdatedUser").SetValue(item, user != null ? user.Cf_Employee.EmployeeName : string.Empty);
                }
            }
        }
        public async Task<int> Add(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<T> GetById(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> filter = null
                                                    , Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null
                                                    , int? start = null, int? length = null
                                                    ,   Expression<Func<T, object>>[] includeProperties = null
                                                    )
        {
            return await GetAsync(filter, orderBy, "", false, start, length, includeProperties);
        }


        private async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> filter = null
                                                    , Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null
                                                    , string sortColumnName = "", bool isAscending = true
                                                    , int? start = null, int? length = null
                                                    ,   Expression<Func<T, object>>[] includeProperties = null
                                                    )
        {
            IQueryable<T> query = _context.Set<T>();
            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }
            if (orderBy != null)
            {
                query = orderBy(query);
            }
            if (!string.IsNullOrEmpty(sortColumnName))
            {
                ParameterExpression parameter = Expression.Parameter(query.ElementType, "");

                MemberExpression property = Expression.Property(parameter, sortColumnName);
                LambdaExpression lambda = Expression.Lambda(property, parameter);

                string methodName = isAscending ? "OrderBy" : "OrderByDescending";

                Expression methodCallExpression = Expression.Call(typeof(Queryable), methodName,
                                      new Type[] { query.ElementType, property.Type },
                                      query.Expression, Expression.Quote(lambda));

                query = query.Provider.CreateQuery<T>(methodCallExpression);
            }
            if (start != null)
                query = query.Skip(start.Value).Take(length.Value);
            return await query.ToListAsync();
        }

        public async Task<int> Update(T entity)
        {
            _context.Set<T>().Update(entity);
            return await _context.SaveChangesAsync();
        }

        protected async Task<int> GetMaxNumber<D>(Guid shopIndex, Guid? branchIndex) where D : class
        {
            try
            {
                IQueryable<D> query = null;
                if (branchIndex != null)
                    query = _context.Set<D>().AsQueryable().Where("ShopIndex = " + '"' + shopIndex + '"'
                        + " && Isdelete = null && BranchIndex =" + '"' + branchIndex + '"'
                        + "");
                else
                    query = _context.Set<D>().AsQueryable().Where("ShopIndex = " + '"' + shopIndex + '"'
                    + " && Isdelete = null");
                query = Common.OrderBy(query, "Number", false);
                var rs = await query.Skip(0).Take(1).ToDynamicListAsync();
                if (rs.Count > 0)
                {
                    var number = rs[0];
                    var propertyInfo = number.GetType().GetProperty("Number");
                    var value = propertyInfo.GetValue(number, null);
                    if (value != null)
                        return (int)value + 1;
                }
                return 1;
            }
            catch (Exception ex)
            {
                return 1;
            }
        }
    }
}
