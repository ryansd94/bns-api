﻿using BNS.Data.Entities;
using BNS.Data.EntityContext;
using BNS.Domain;
using BNS.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BNS.Service.Implement
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly BNSDbContext _context;
        public GenericRepository(BNSDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<IQueryable<T>> GetAsync(Expression<Func<T, bool>> filter = null
                                                    , Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null
                                                    , Expression<Func<T, object>>[] includeProperties = null
                                                    )
        {
            return await GetAsync(filter, orderBy, null, null, includeProperties);
        }


        public async Task<IQueryable<T>> GetAsync(Expression<Func<T, bool>> filter = null
                                                    , Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null
                                                    , string sortColumnName = "", bool? isAscending = true
                                                    , Expression<Func<T, object>>[] includeProperties = null
                                                    )
        {
            IQueryable<T> query = _context.Set<T>();
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }
            if (filter != null)
            {
                query = query.Where(filter);
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

                string methodName = isAscending.Value ? "OrderBy" : "OrderByDescending";

                Expression methodCallExpression = Expression.Call(typeof(Queryable), methodName,
                                      new Type[] { query.ElementType, property.Type },
                                      query.Expression, Expression.Quote(lambda));

                query = query.Provider.CreateQuery<T>(methodCallExpression);
            }
            return query;
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
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
                query = query.OrderBy("Number", "asc");
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

        public async Task<IQueryable<T>> GetAsync(IQueryable<T> query, Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string sortColumnName = null, bool? isAscending = true, params Expression<Func<T, object>>[] includeProperties)
        {
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }
            if (filter != null)
            {
                query = query.Where(filter);
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

                string methodName = isAscending.Value ? "OrderBy" : "OrderByDescending";

                Expression methodCallExpression = Expression.Call(typeof(Queryable), methodName,
                                      new Type[] { query.ElementType, property.Type },
                                      query.Expression, Expression.Quote(lambda));

                query = query.Provider.CreateQuery<T>(methodCallExpression);
            }
            return query;
        }

        public async Task<IQueryable<T>> OrderBy(IQueryable<T> source, string columnName, bool isAscending = true)
        {
            if (String.IsNullOrEmpty(columnName))
            {
                return source;
            }

            ParameterExpression parameter = Expression.Parameter(source.ElementType, "");

            MemberExpression property = Expression.Property(parameter, columnName);
            LambdaExpression lambda = Expression.Lambda(property, parameter);

            string methodName = isAscending ? "OrderBy" : "OrderByDescending";

            Expression methodCallExpression = Expression.Call(typeof(Queryable), methodName,
                                  new Type[] { source.ElementType, property.Type },
                                  source.Expression, Expression.Quote(lambda));
            source = source.Provider.CreateQuery<T>(methodCallExpression);
            return source.Provider.CreateQuery<T>(methodCallExpression);
        }

        public async Task<IQueryable<T>> GetAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            return await GetAsync(filter, orderBy, null, null, null);
        }
        private async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> filter = null
                                                   , Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null
                                                   , string sortColumnName = "", bool? isAscending = true
                                                   , Expression<Func<T, object>>[] includeProperties = null
                                                   )
        {
            IQueryable<T> query = _context.Set<T>();
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }
            if (filter != null)
            {
                query = query.Where(filter);
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

                string methodName = isAscending.Value ? "OrderBy" : "OrderByDescending";

                Expression methodCallExpression = Expression.Call(typeof(Queryable), methodName,
                                      new Type[] { query.ElementType, property.Type },
                                      query.Expression, Expression.Quote(lambda));

                query = query.Provider.CreateQuery<T>(methodCallExpression);
            }
            return await query.FirstOrDefaultAsync();
        }
        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> filter = null)
        {
            return await FirstOrDefaultAsync(filter, null, null, null, null);
        }

        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> filter = null, params Expression<Func<T, object>>[] includeProperties)
        {
            return await FirstOrDefaultAsync(filter, null, null, null, includeProperties);
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> filter = null)
        {
            IQueryable<T> query = _context.Set<T>();

            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.CountAsync();
        }
    }
}
