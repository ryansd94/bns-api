using System;
using System.Linq;
using System.Linq.Expressions;

using System.Linq.Dynamic.Core;
using System.Collections.Generic;

namespace BNS.Utilities
{
    public static class Common
    {
        public static IQueryable<T> OrderBy<T>(IQueryable<T> source, string columnName, bool isAscending = true)
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

            return source.Provider.CreateQuery<T>(methodCallExpression);
        }
        public static IQueryable<D> WhereQuery<D>(IQueryable<D> source, string columnName, string propertyValue)
        {
            return source.Where(m => m.GetType().GetProperty(columnName).GetValue(m, null).ToString() == propertyValue);
        }


        public static IQueryable<T> SearchBy<T, D>(IQueryable<T> source, D model, string value)
        {
            var columns = new List<string>();
            foreach (var prop in model.GetType().GetProperties())
            {
                if (prop.PropertyType != typeof(string) && prop.PropertyType != typeof(int?) && prop.PropertyType != typeof(int))
                    continue;
                if (prop.Name.ToLower() != "index" &&
                    prop.Name.ToLower() != "updateddate" &&
                    prop.Name.ToLower() != "updateddatestr" &&
                    prop.Name.ToLower() != "updateduser" &&
                    prop.Name.ToLower() != "createddate" &&
                      prop.Name.ToLower() != "createddatestr" &&
                        prop.Name.ToLower() != "createduser")
                    columns.Add(prop.Name);
            }
            string query = string.Join(" or ", columns.Select(c => $"string(object({c})).Contains(\"{value}\")"));

            return source.Where(query);
        }






    }
}
