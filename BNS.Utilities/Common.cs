using System;
using System.Linq;
using System.Linq.Expressions;

using System.Linq.Dynamic.Core;
using System.Collections.Generic;
using static BNS.Utilities.Enums;
using Newtonsoft.Json;
using System.Reflection;

namespace BNS.Utilities
{
    public static class Common
    {
        public static string ToLowerFirstChar(this string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            return char.ToLower(input[0]) + input.Substring(1);
        }
        // The structure used by the new extension method
        public struct SearchCriteria
        {
            public string Column;
            public object Value;
            public EWhereCondition Condition;
        }
        public static string? FirstCharToLowerCase(this string? str)
        {
            if (!string.IsNullOrEmpty(str) && char.IsUpper(str[0]))
                return str.Length == 1 ? char.ToLower(str[0]).ToString() : char.ToLower(str[0]) + str[1..];

            return str;
        }
        // Implementation
        public static IQueryable<T> WhereOr<T>(this IQueryable<T> Query, string strCriterias)
        {
            if (string.IsNullOrEmpty(strCriterias))
                return Query;

            var Criterias = JsonConvert.DeserializeObject<List<SearchCriteria>>(strCriterias);
            if (Criterias == null || Criterias.Count == 0)
                return Query;
            LambdaExpression lambda;
            Expression resultCondition = null;

            // Create a member expression pointing to given column
            ParameterExpression parameter = Expression.Parameter(Query.ElementType, "p");

            foreach (var searchCriteria in Criterias)
            {
                if (string.IsNullOrEmpty(searchCriteria.Column) || searchCriteria.Value == null)
                    continue;

                MemberExpression memberAccess = null;
                var column = searchCriteria.Column.FirstCharToLowerCase();
                foreach (var property in column.Split('.'))
                    memberAccess = MemberExpression.Property
                        (memberAccess ?? (parameter as Expression), property);
                // Change the type of the parameter 'value'. it is necessary for comparisons (specially for booleans)
                var memberType = memberAccess.Type;
                var value = searchCriteria.Value;
                if (memberType == typeof(DateTime))
                {
                    value = DateTime.Parse(value.ToString()).AddDays(1).AddSeconds(-1).ToString();
                }
                ConstantExpression filter = Expression.Constant
                (
                    Convert.ChangeType(value, memberType != typeof(string) && memberType != typeof(Int32) ? memberAccess.Type : typeof(string))
                );


                //switch operation
                Expression condition = null;
                switch (searchCriteria.Condition)
                {
                    //equal ==
                    case EWhereCondition.Equal:
                        if (memberType != typeof(Int32))
                            condition = Expression.Equal(memberAccess, filter);
                        else
                        {
                            Expression condition2 = null;
                            foreach (var item in value.ToString().Split(','))
                            {
                                ConstantExpression filter2 = Expression.Constant
                               (
                                   Convert.ChangeType(item, typeof(Int32))
                               );
                                condition2 = Expression.Equal(memberAccess, filter2);
                                condition = condition != null ? Expression.Or(condition, condition2) : condition2;
                            }
                        }
                        break;
                    //not equal !=
                    case EWhereCondition.NotEqual:
                        if (memberType != typeof(Int32))
                            condition = Expression.NotEqual(memberAccess, filter);
                        else
                        {
                            Expression condition2 = null;
                            foreach (var item in value.ToString().Split(','))
                            {
                                ConstantExpression filter2 = Expression.Constant
                               (
                                   Convert.ChangeType(item, typeof(Int32))
                               );
                                condition2 = Expression.NotEqual(memberAccess, filter2);
                                condition = condition != null ? Expression.Or(condition, condition2) : condition2;
                            }
                        }
                        break;
                    // Greater
                    case EWhereCondition.Greater:
                        condition = Expression.GreaterThan(memberAccess, filter);
                        break;
                    // Greater or equal
                    case EWhereCondition.GreaterOrEqual:
                        condition = Expression.GreaterThanOrEqual(memberAccess, filter);
                        break;
                    // Less
                    case EWhereCondition.Less:
                        condition = Expression.LessThan(memberAccess, filter);
                        break;
                    // Less or equal
                    case EWhereCondition.LessEqual:
                        condition = Expression.LessThanOrEqual(memberAccess, filter);
                        break;
                    //string.Contains()
                    case EWhereCondition.Contains:

                        MethodInfo refmethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                        var parameters = Expression.Parameter(typeof(T), "p");
                        var property = Expression.Property(parameter, column);
                        var valueContains = Expression.Constant(value.ToString(), typeof(string));
                        condition = Expression.Call(property, refmethod, valueContains);
                        break;

                    default:
                        continue;
                }

                resultCondition = resultCondition != null ? Expression.And(resultCondition, condition) : condition;
            }

            lambda = Expression.Lambda(resultCondition, parameter);

            MethodCallExpression result = Expression.Call(
                       typeof(Queryable), "Where",
                       new[] { Query.ElementType },
                       Query.Expression,
                       lambda);

            return Query.Provider.CreateQuery<T>(result);

        }
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, Expression<Func<T, object>> orderQuery, string sort)
        {
            bool isAsc = true;
            if (sort == ESortEnum.desc.ToString())
                isAsc = false;
            else isAsc = true;
            if (isAsc)
                return source.OrderBy(orderQuery);
            else
                return source.OrderByDescending(orderQuery);
        }
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string columnName, string sort)
        {
            if (String.IsNullOrEmpty(columnName))
            {
                return source;
            }
            bool isAsc = true;
            if (sort == ESortEnum.desc.ToString())
                isAsc = false;
            else isAsc = true;
            columnName.ToLowerFirstChar();
            ParameterExpression parameter = Expression.Parameter(source.ElementType, "");

            MemberExpression property = Expression.Property(parameter, columnName);
            LambdaExpression lambda = Expression.Lambda(property, parameter);

            string methodName = isAsc ? "OrderBy" : "OrderByDescending";

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
