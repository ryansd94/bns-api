using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Reflection;
using static BNS.Utilities.Enums;

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
        public class SearchCriteria
        {
            public string Column;
            public object Value;
            public EWhereCondition Condition;
            public bool IsCustom;
            public Expression Expression { get; set; }
            public EAndOr AndOr { get; set; } = EAndOr.And;
            public List<SearchCriteria> SearchCriteriaSubs { get; set; }
        }
        public static string FirstCharToLowerCase(this string? str)
        {
            if (!string.IsNullOrEmpty(str) && char.IsUpper(str[0]))
                return str.Length == 1 ? char.ToLower(str[0]).ToString() : char.ToLower(str[0]) + str[1..];

            return str;
        }
        public static string FirstCharToUpperCase(this string? str)
        {
            if (!string.IsNullOrEmpty(str) && char.IsLower(str[0]))
                return str.Length == 1 ? char.ToUpper(str[0]).ToString() : char.ToUpper(str[0]) + str[1..];

            return str;
        }

        // Implementation
        public static IQueryable<T> WhereOr<T>(this IQueryable<T> query, string strCriterias, string strDefaultCriterias = null)
        {
            if (string.IsNullOrEmpty(strCriterias) && string.IsNullOrEmpty(strDefaultCriterias))
                return query;

            var criteriasByDefaultColumn = new List<SearchCriteria>();
            if (!string.IsNullOrEmpty(strCriterias))
            {
                criteriasByDefaultColumn = JsonConvert.DeserializeObject<List<SearchCriteria>>(strCriterias).ToList();
            }
            if (!string.IsNullOrEmpty(strDefaultCriterias))
            {
                criteriasByDefaultColumn.AddRange(JsonConvert.DeserializeObject<List<SearchCriteria>>(strDefaultCriterias).ToList());
            }

            return WhereOr<T>(query, criteriasByDefaultColumn);
        }

        private static IQueryable<T> WhereOr<T>(this IQueryable<T> query, List<SearchCriteria> criterias)
        {
            if (criterias == null || criterias.Count == 0)
                return query;

            var lstFilters = new List<SearchCriteria>();
            foreach (var searchCriteria in criterias)
            {
                if (searchCriteria.IsCustom)
                {
                    var andOr = EAndOr.And;
                    if (searchCriteria.Condition == EWhereCondition.NotEqual || searchCriteria.Condition == EWhereCondition.NotContains)
                    {
                        lstFilters.Add(new SearchCriteria
                        {
                            Column = "TaskCustomColumnValues",
                            Value = 0,
                            Condition = EWhereCondition.LessThanOrEqual,
                            IsCustom = true,
                            AndOr = EAndOr.And
                        });
                        lstFilters.Add(new SearchCriteria
                        {
                            Column = "TaskCustomColumnValues.CustomColumnId",
                            Value = searchCriteria.Column,
                            Condition = searchCriteria.Condition,
                            IsCustom = true,
                            AndOr = EAndOr.Or
                        });
                        andOr = EAndOr.Or;
                    }

                    lstFilters.Add(new SearchCriteria
                    {
                        IsCustom = true,
                        AndOr = andOr,
                        SearchCriteriaSubs = new List<SearchCriteria>
                        {
                            new SearchCriteria
                            {
                                Column = "TaskCustomColumnValues.Value",
                                Value = searchCriteria.Value,
                                Condition = searchCriteria.Condition,
                                AndOr = EAndOr.And
                            },
                            new SearchCriteria
                            {
                                Column = "TaskCustomColumnValues.CustomColumnId",
                                Value = searchCriteria.Column,
                                Condition = EWhereCondition.Equal,
                                AndOr = EAndOr.And
                            }
                        }
                    });
                }
                else
                {
                    lstFilters.Add(searchCriteria);
                }
            }

            var filter = FilterExtensions.BuildFilter<T>(lstFilters);

            return query.Where(filter);
        }

        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string columnName, string sort)
        {
            if (string.IsNullOrEmpty(columnName))
            {
                return source;
            }
            bool isAsc = true;
            if (sort == ESortEnum.desc.ToString())
                isAsc = false;
            else isAsc = true;

            string methodName = isAsc ? "OrderBy" : "OrderByDescending";

            return ApplyOrder<T>(source, columnName, methodName);
        }

        private readonly struct Node
        {
            public Node(ParameterExpression parameter, Expression body)
            {
                Parameter = parameter;
                Body = body;
            }

            public ParameterExpression Parameter { get; }
            public Expression Body { get; }
        }

        public static string ToUpperFirstChar(this string value)
        {
            if(string.IsNullOrEmpty(value))
            {
                return value;
            }
            return char.ToUpper(value[0]) + value.Substring(1);
        }

        public static class FilterExtensions
        {
            public static Expression<Func<T, bool>> BuildFilter<T>(List<SearchCriteria> keys)
            {
                if (keys == null || keys.Count == 0) throw new ArgumentNullException(nameof(keys));

                Expression resultCondition = null;
                var p = Expression.Parameter(typeof(T), "p");

                foreach (var parentItem in keys)
                {
                    var childItems = new List<SearchCriteria>();
                    if (parentItem.SearchCriteriaSubs != null && parentItem.SearchCriteriaSubs.Count > 0)
                    {
                        childItems.AddRange(parentItem.SearchCriteriaSubs);
                    }
                    else
                    {
                        childItems.Add(parentItem);
                    }
                    Expression childCondition = null;
                    foreach (var item in childItems)
                    {
                        var column = item.Column;
                        if (item.IsCustom == false)
                        {
                            column = column.ToUpperFirstChar();
                        }
                        var stack = new Stack<Node>();
                        Expression body = p;
                        MemberExpression memberAccess = null;
                        if (column.Contains('.'))
                        {
                            memberAccess = MemberExpression.Property
                            (memberAccess ?? (p as Expression), column.Split('.')[0]);
                        }
                        else
                        {
                            memberAccess = MemberExpression.Property
                            (memberAccess ?? (p as Expression), column);
                        }
                        foreach (string member in column.Split('.'))
                        {
                            if (body.Type.IsGenericType)
                            {
                                var genericArgs = body.Type.GetGenericArguments();
                                if (genericArgs.Length == 1 && typeof(IEnumerable<>)
                                    .MakeGenericType(genericArgs)
                                    .IsAssignableFrom(body.Type))
                                {
                                    stack.Push(new Node(p, body));
                                    p = Expression.Parameter(genericArgs[0], "s" + stack.Count);
                                    body = p;
                                }
                            }

                            body = Expression.PropertyOrField(body, member);
                        }

                        if (column.Contains('.') && parentItem.IsCustom)
                        {
                            if (column.Split('.')[1].Equals("CustomColumnId"))
                            {
                                var constant = Expression.Constant(Guid.Parse(item.Value.ToString()), typeof(Guid));
                                body = Expression.Call(body, nameof(Guid.Equals), null, constant);
                            }
                            else
                            {
                                switch (item.Condition)
                                {
                                    case EWhereCondition.Equal:

                                        MethodInfo equalsMethod = typeof(string).GetMethod("Equals", new[] { typeof(string) });
                                        body = Expression.Call(body, equalsMethod, Expression.Constant(item.Value.ToString()));
                                        //body = Expression.Call(body, nameof(string.Equals), null, constant);
                                        break;
                                    case EWhereCondition.NotEqual:
                                        body = Expression.Equal(body, Expression.Constant(item.Value.ToString()));
                                        break;
                                    case EWhereCondition.Contains:
                                        //var methodInfo = typeof(List<T>).GetMethod("Contains", new Type[] { typeof(T) });

                                        //var list = Expression.Constant(new List<string>{item.Value.ToString() });

                                        //var param = Expression.Parameter(typeof(T), "j");
                                        //var value = Expression.Property(param, "Value");
                                        //body = Expression.Call(list, methodInfo, value);
                                        body = Expression.Call(body, nameof(string.Contains), null, Expression.Constant(item.Value, typeof(string)));
                                        break;
                                    case EWhereCondition.NotContains:
                                        //var methodInfo = typeof(List<T>).GetMethod("Contains", new Type[] { typeof(T) });

                                        //var list = Expression.Constant(new List<string>{item.Value.ToString() });

                                        //var param = Expression.Parameter(typeof(T), "j");
                                        //var value = Expression.Property(param, "Value");
                                        //body = Expression.Call(list, methodInfo, value);
                                        body = Expression.Call(body, nameof(string.Contains), null, Expression.Constant(item.Value, typeof(string)));
                                        break;
                                }
                            }
                        }
                        else
                        {
                            switch (item.Condition)
                            {
                                case EWhereCondition.Equal:
                                    MethodInfo equalsMethod = null;
                                    if (body.Type == typeof(Nullable<System.Guid>))
                                    {
                                        var filter1 = Expression.Constant(
                            Convert.ChangeType(Guid.Parse(item.Value.ToString()), memberAccess.Type.GetGenericArguments()[0]));
                                        Expression typeFilter = Expression.Convert(filter1, memberAccess.Type);
                                        body = Expression.Equal(memberAccess, typeFilter);
                                    }
                                    else if (body.Type == typeof(System.Guid))
                                    {
                                        equalsMethod = typeof(Guid).GetMethod("Equals", new[] { typeof(Guid) });
                                        body = Expression.Call(body, equalsMethod, Expression.Constant(item.Value));
                                    }
                                    else
                                    {
                                        equalsMethod = typeof(string).GetMethod("Equals", new[] { typeof(string) });
                                        body = Expression.Call(body, equalsMethod, Expression.Constant(item.Value));
                                    }
                                    break;
                                case EWhereCondition.NotEqual:
                                    body = Expression.NotEqual(body, Expression.Constant(item.Value));
                                    break;
                                case EWhereCondition.LessThanOrEqual:
                                    var count = Expression.PropertyOrField(body, "Count");
                                    var enumerableCountMethod = typeof(Enumerable).GetMethods()
                                        .First(method => method.Name == "Count" && method.GetParameters().Length == 1)
                                        .MakeGenericMethod(typeof(T));
                                    body = Expression.LessThanOrEqual(count, Expression.Constant(int.Parse(item.Value.ToString())));
                                    break;
                                case EWhereCondition.Contains:
                                    body = Expression.Call(body, nameof(string.Contains), null, Expression.Constant(item.Value.ToString()));
                                    break;
                                case EWhereCondition.Greater:
                                    var greaterConstant = Expression.Constant(item.Value, ((PropertyInfo)memberAccess.Member).PropertyType);
                                    body = Expression.GreaterThan(memberAccess, greaterConstant);
                                    break;
                                case EWhereCondition.GreaterOrEqual:
                                    var greaterOrEqualConstant = Expression.Constant(item.Value, ((PropertyInfo)memberAccess.Member).PropertyType);
                                    body = Expression.GreaterThanOrEqual(memberAccess, greaterOrEqualConstant);
                                    break;
                                case EWhereCondition.Less:
                                    var lessConstant = Expression.Constant(item.Value, ((PropertyInfo)memberAccess.Member).PropertyType);
                                    body = Expression.LessThan(memberAccess, lessConstant);
                                    break;
                                case EWhereCondition.LessEqual:
                                    var lessEqualnConstant = Expression.Constant(item.Value, ((PropertyInfo)memberAccess.Member).PropertyType);
                                    body = Expression.LessThanOrEqual(memberAccess, lessEqualnConstant);
                                    break;
                                case EWhereCondition.NotContains:
                                    body = Expression.Call(body, nameof(string.Contains), null, Expression.Constant(item.Value.ToString()));
                                    break;
                            }
                        }
                        if (stack.Count != 0)
                        {
                            while (stack.Count != 0)
                            {
                                var childFilter = Expression.Lambda(body, p);
                                var parent = stack.Pop();

                                var exp = Expression.Call(typeof(Enumerable),
                                    nameof(Enumerable.Any),
                                    new[] { p.Type },
                                    parent.Body,
                                    childFilter);

                                if (item.Condition == EWhereCondition.NotContains || item.Condition == EWhereCondition.NotEqual)
                                {
                                    body = Expression.Not(exp);
                                }
                                else body = exp;
                                p = parent.Parameter;
                            }
                        }
                        else
                        {
                            if (item.Condition == EWhereCondition.NotContains || item.Condition == EWhereCondition.NotEqual)
                            {
                                body = Expression.Not(body);
                            }
                        }

                        childCondition = childCondition != null ? (item.AndOr == EAndOr.And ? Expression.And(childCondition, body) : Expression.Or(childCondition, body)) : body;
                    }
                    resultCondition = resultCondition != null ? (parentItem.AndOr == EAndOr.And ? Expression.And(resultCondition, childCondition) : Expression.Or(resultCondition, childCondition)) : childCondition;
                }
                return Expression.Lambda<Func<T, bool>>(resultCondition, p);
            }
        }
        static IOrderedQueryable<T> ApplyOrder<T>(
            IQueryable<T> source,
            string property,
            string methodName)
        {
            bool isCustomColumn = false;
            string[] props = property.Split('.');
            if (props.Length > 1)
            {
                if (props[0].Equals("taskCustomColumnValues")) isCustomColumn = true;
            }
            List<string> values = new List<string>() { property };
            Type type = typeof(T);
            ParameterExpression arg = Expression.Parameter(source.ElementType, "p");
            Expression expr = arg;
            if (!isCustomColumn)
            {
                foreach (string prop in props)
                {
                    // use reflection (not ComponentModel) to mirror LINQ
                    PropertyInfo pi = type.GetProperty(prop.FirstCharToUpperCase());
                    expr = Expression.Property(expr, pi);
                    type = pi.PropertyType;
                }
            }
            else
            {
                PropertyInfo pi = type.GetProperty(property.FirstCharToUpperCase());
                expr = Expression.Property(expr, pi);
                type = pi.PropertyType;
            }
            Type delegateType = typeof(Func<,>).MakeGenericType(typeof(T), type);
            LambdaExpression lambda = Expression.Lambda(delegateType, expr, arg);

            object result = typeof(Queryable).GetMethods().Single(
                    method => method.Name == methodName
                            && method.IsGenericMethodDefinition
                            && method.GetGenericArguments().Length == 2
                            && method.GetParameters().Length == 2)
                    .MakeGenericMethod(typeof(T), type)
                    .Invoke(null, new object[] { source, lambda });
            return (IOrderedQueryable<T>)result;
        }

        public static IQueryable<TEntity> OrderBy<TEntity>(this IQueryable<TEntity> source, string orderByStrValues) where TEntity : class
        {
            var queryExpr = source.Expression;
            var methodAsc = "OrderBy";
            var methodDesc = "OrderByDescending";

            var orderByValues = orderByStrValues.Trim().Split(',').Select(x => x.Trim()).ToList();

            foreach (var orderPairCommand in orderByValues)
            {
                var command = orderPairCommand.ToUpper().EndsWith("DESC") ? methodDesc : methodAsc;

                //Get propertyname and remove optional ASC or DESC
                var propertyName = orderPairCommand.Split(' ')[0].Trim();

                var type = typeof(TEntity);
                var parameter = Expression.Parameter(type, "p");

                PropertyInfo property;
                MemberExpression propertyAccess;

                if (propertyName.Contains('.'))
                {
                    // support to be sorted on child fields. 
                    var childProperties = propertyName.Split('.');

                    property = SearchProperty(typeof(TEntity), childProperties[0]);
                    if (property == null)
                        continue;

                    propertyAccess = Expression.MakeMemberAccess(parameter, property);

                    for (int i = 1; i < childProperties.Length; i++)
                    {
                        var t = property.PropertyType;
                        property = SearchProperty(t, childProperties[i]);

                        if (property == null)
                            continue;

                        propertyAccess = Expression.MakeMemberAccess(propertyAccess, property);
                    }
                }
                else
                {
                    property = null;
                    property = SearchProperty(type, propertyName);

                    if (property == null)
                        continue;

                    propertyAccess = Expression.MakeMemberAccess(parameter, property);
                }

                var orderByExpression = Expression.Lambda(propertyAccess, parameter);

                queryExpr = Expression.Call(typeof(Queryable), command, new Type[] { type, property.PropertyType }, queryExpr, Expression.Quote(orderByExpression));

                methodAsc = "ThenBy";
                methodDesc = "ThenByDescending";
            }

            return source.Provider.CreateQuery<TEntity>(queryExpr); ;
        }

        private static PropertyInfo SearchProperty(Type type, string propertyName)
        {
            foreach (var item in type.GetProperties())
                if (item.Name.ToLower() == propertyName.ToLower())
                    return item;
            return null;
        }

        public enum FilterOperator
        {
            IsEqualTo,
            IsNotEqualTo,
            IsGreaterThan,
            IsGreaterThanOrEqualTo,
            IsLessThan,
            IsLessThanOrEqualTo,
            Contains,
            StartsWith,
            EndsWith
        }

        public class IFilterCriteria
        {
            public string PropertyToCompare { get; set; }
            public object ValueToCompare { get; set; }
            public FilterOperator FilterOperator { get; set; }
            public bool IsList { get; set; }
            public Expression Expression { get; set; }
        }

        public enum LogicalOperator
        {
            And,
            Or
        }

        public class PivotTable<TRow, TColumn, TValue>
            where TRow : IComparable, IEquatable<TRow>
            where TColumn : IComparable, IEquatable<TColumn>
        {
            private readonly Dictionary<TRow, Dictionary<TColumn, TValue>> _dictionary;

            public PivotTable(Dictionary<TRow, Dictionary<TColumn, TValue>> dictionary)
            {
                _dictionary = dictionary;
            }

            public bool HasValue(TRow row, TColumn col)
            {
                return _dictionary.ContainsKey(row) && _dictionary[row].ContainsKey(col);
            }

            public TValue GetValue(TRow row, TColumn col)
            {
                return _dictionary[row][col];
            }
        }

        public sealed class ValueKey
        {
            public readonly object[] DimKeys;
            public ValueKey(params object[] dimKeys)
            {
                DimKeys = dimKeys;
            }
            public override int GetHashCode()
            {
                if (DimKeys == null) return 0;
                int hashCode = DimKeys.Length;
                for (int i = 0; i < DimKeys.Length; i++)
                {
                    hashCode ^= DimKeys[i].GetHashCode();
                }
                return hashCode;
            }
            public override bool Equals(object obj)
            {
                if (obj == null || !(obj is ValueKey))
                    return false;
                var x = DimKeys;
                var y = ((ValueKey)obj).DimKeys;
                if (ReferenceEquals(x, y))
                    return true;
                if (x.Length != y.Length)
                    return false;
                for (int i = 0; i < x.Length; i++)
                {
                    if (!x[i].Equals(y[i]))
                        return false;
                }
                return true;
            }
        }
    }
}
