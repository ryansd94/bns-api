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

        public static Expression<Func<T, bool>> WhereOr2<T>(string strCriterias)
        {
            if (string.IsNullOrEmpty(strCriterias))
                return null;

            var criteriasByDefaultColumn = JsonConvert.DeserializeObject<List<SearchCriteria>>(strCriterias).ToList();
            if (criteriasByDefaultColumn == null || criteriasByDefaultColumn.Count == 0)
                return null;
            Expression resultCondition = null;

            // Create a member expression pointing to given column
            ParameterExpression parameter = Expression.Parameter(typeof(T), "p");

            foreach (var searchCriteria in criteriasByDefaultColumn)
            {
                if (searchCriteria.IsCustom)
                {
                    var uuuu = FilterExtensions.BuildFilter<T>("TaskCustomColumnValues.Value", searchCriteria.Value.ToString());
                    resultCondition = resultCondition != null ? Expression.Equal(resultCondition, uuuu.Body) : uuuu.Body;
                }
                else
                {
                    if (string.IsNullOrEmpty(searchCriteria.Column) || searchCriteria.Value == null)
                        continue;

                    MemberExpression memberAccess = null;
                    var column = searchCriteria.Column.FirstCharToUpperCase();
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
                    ConstantExpression filter = null;
                    if (!memberType.IsGenericType)
                    {
                        filter = Expression.Constant
                        (
                            Convert.ChangeType(value, memberType != typeof(string) && memberType != typeof(Int32) ? memberAccess.Type : typeof(string))
                        );
                    }
                    else
                    {

                    }
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
                        case EWhereCondition.Dynamic:
                            if (memberAccess.Type.IsGenericType &&
            typeof(IEnumerable<>)
                .MakeGenericType(memberAccess.Type.GetGenericArguments())
                .IsAssignableFrom(memberAccess.Type))
                            {
                                // find AsQueryable method
                                var toQueryable = typeof(Queryable).GetMethods()
                                    .Where(m => m.Name == "AsQueryable")
                                    .Single(m => m.IsGenericMethod)
                                    .MakeGenericMethod(typeof(string));

                                // find Any method
                                var method = typeof(Queryable).GetMethods()
                                    .Where(m => m.Name == "Any")
                                    .Single(m => m.GetParameters().Length == 2)
                                    .MakeGenericMethod(typeof(string));

                                // make expression
                                condition = Expression.Call(
                                    null,
                                    method,
                                    Expression.Call(null, toQueryable, memberAccess),
                                    searchCriteria.Expression
                                );
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
            }

            //lambda = Expression.Lambda(resultCondition, parameter);

            return Expression.Lambda<Func<T, bool>>(resultCondition, parameter);
        }
        // Implementation
        public static IQueryable<T> WhereOr<T>(this IQueryable<T> query, string strCriterias)
        {
            if (string.IsNullOrEmpty(strCriterias))
                return query;

            var criteriasByDefaultColumn = JsonConvert.DeserializeObject<List<SearchCriteria>>(strCriterias).ToList();
            return WhereOr<T>(query, criteriasByDefaultColumn);
        }

        public static IQueryable<T> WhereOr<T>(this IQueryable<T> query, List<SearchCriteria> criterias)
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

            var filter = FilterExtensions.BuildFilter2<T>(lstFilters);

            return query.Where(filter);
        }
        public static IQueryable<T> WhereOr2<T>(this IQueryable<T> Query, string strCriterias)
        {
            if (string.IsNullOrEmpty(strCriterias))
                return Query;

            var criteriasByDefaultColumn = JsonConvert.DeserializeObject<List<SearchCriteria>>(strCriterias).ToList();
            if (criteriasByDefaultColumn == null || criteriasByDefaultColumn.Count == 0)
                return Query;
            LambdaExpression lambda;
            Expression resultCondition = null;

            // Create a member expression pointing to given column
            ParameterExpression parameter = Expression.Parameter(Query.ElementType, "p");

            foreach (var searchCriteria in criteriasByDefaultColumn)
            {
                if (searchCriteria.IsCustom)
                {
                    var uuuu = FilterExtensions.BuildFilter<T>("TaskCustomColumnValues.Value", searchCriteria.Value.ToString());
                    //resultCondition = resultCondition != null ? Expression.And(resultCondition, uuuu.Body) : uuuu.Body;
                    resultCondition = resultCondition != null ? Expression.And(resultCondition, uuuu) : uuuu;
                }
                else
                {
                    if (string.IsNullOrEmpty(searchCriteria.Column) || searchCriteria.Value == null)
                        continue;

                    MemberExpression memberAccess = null;
                    var column = searchCriteria.Column.FirstCharToUpperCase();
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
                    ConstantExpression filter = null;
                    if (!memberType.IsGenericType)
                    {
                        filter = Expression.Constant
                        (
                            Convert.ChangeType(value, memberType != typeof(string) && memberType != typeof(Int32) ? memberAccess.Type : typeof(string))
                        );
                    }
                    else
                    {

                    }
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
                        case EWhereCondition.Dynamic:
                            if (memberAccess.Type.IsGenericType &&
            typeof(IEnumerable<>)
                .MakeGenericType(memberAccess.Type.GetGenericArguments())
                .IsAssignableFrom(memberAccess.Type))
                            {
                                // find AsQueryable method
                                var toQueryable = typeof(Queryable).GetMethods()
                                    .Where(m => m.Name == "AsQueryable")
                                    .Single(m => m.IsGenericMethod)
                                    .MakeGenericMethod(typeof(string));

                                // find Any method
                                var method = typeof(Queryable).GetMethods()
                                    .Where(m => m.Name == "Any")
                                    .Single(m => m.GetParameters().Length == 2)
                                    .MakeGenericMethod(typeof(string));

                                // make expression
                                condition = Expression.Call(
                                    null,
                                    method,
                                    Expression.Call(null, toQueryable, memberAccess),
                                    searchCriteria.Expression
                                );
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
            //Expression methodCallExpression = Expression.Call(typeof(Queryable), methodName,
            //                      new Type[] { source.ElementType, property.Type },
            //                      source.Expression, Expression.Quote(lambda));

            //return source.Provider.CreateQuery<T>(methodCallExpression);
        }
        public static IQueryable<D> WhereQuery<D>(IQueryable<D> source, string columnName, string propertyValue)
        {
            return source.Where(m => m.GetType().GetProperty(columnName).GetValue(m, null).ToString() == propertyValue);
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

        public static class FilterExtensions
        {

            public static Expression<Func<T, bool>> BuildFilter<T>(string key, string value)
            {
                if (string.IsNullOrEmpty(key)) throw new ArgumentNullException(nameof(key));
                if (string.IsNullOrEmpty(value)) throw new ArgumentNullException(nameof(value));

                var p = Expression.Parameter(typeof(T), "p");
                Expression body = p;

                var stack = new Stack<Node>();

                foreach (string member in key.Split('.'))
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

                var constant = Expression.Constant(value, typeof(string));
                body = Expression.Call(body, nameof(string.Contains), null, constant);

                while (stack.Count != 0)
                {
                    var childFilter = Expression.Lambda(body, p);
                    var parent = stack.Pop();

                    body = Expression.Call(typeof(Enumerable),
                        nameof(Enumerable.Any),
                        new[] { p.Type },
                        parent.Body,
                        childFilter);

                    p = parent.Parameter;
                }
                return Expression.Lambda<Func<T, bool>>(body, p);
            }

            public static Expression<Func<T, bool>> BuildFilter2<T>(List<SearchCriteria> keys)
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
                        var stack = new Stack<Node>();
                        Expression body = p;
                        MemberExpression memberAccess = null;
                        if (item.Column.Contains('.'))
                        {
                            memberAccess = MemberExpression.Property
                            (memberAccess ?? (p as Expression), item.Column.Split('.')[0]);
                        }
                        else
                        {
                            memberAccess = MemberExpression.Property
                            (memberAccess ?? (p as Expression), item.Column);
                        }
                        var xxxxx = MemberExpression.GetActionType();
                        foreach (string member in item.Column.Split('.'))
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

                        if (item.Column.Contains('.') && parentItem.IsCustom)
                        {
                            if (item.Column.Split('.')[1].Equals("CustomColumnId"))
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
                                    MethodInfo equalsMethod = typeof(string).GetMethod("Equals", new[] { typeof(string) });
                                    if (body.Type.Name.Equals("Guid"))
                                    {
                                        equalsMethod = typeof(Guid).GetMethod("Equals", new[] { typeof(Guid) });
                                    }
                                    body = Expression.Call(body, equalsMethod, Expression.Constant(item.Value));
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

        public static IQueryable<TEntity> OrderBy2<TEntity>(this IQueryable<TEntity> source, string orderByStrValues) where TEntity : class
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

                var p = Expression.Parameter(typeof(TEntity), "p");
                var stack = new Stack<Node>();
                Expression body = p;
                if (propertyName.Contains('.'))
                {
                    // support to be sorted on child fields. 
                    var childProperties = propertyName.Split('.');

                    property = SearchProperty(typeof(TEntity), childProperties[0]);
                    if (property == null)
                        continue;


                    for (int i = 0; i < childProperties.Length; i++)
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
                        body = Expression.PropertyOrField(body, childProperties[i]);
                    }

                    while (stack.Count != 0)
                    {
                        var childFilter = Expression.Lambda(body, p);
                        var parent = stack.Pop();

                        var exp = Expression.Call(typeof(Enumerable),
                            nameof(Enumerable.Any),
                            new[] { p.Type },
                            parent.Body,
                            childFilter);

                        body = exp;
                        p = parent.Parameter;
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

                var orderByExpression = Expression.Lambda(body, parameter);

                queryExpr = Expression.Call(typeof(IQueryable), command, new Type[] { type, property.PropertyType }, queryExpr, Expression.Quote(orderByExpression));

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

        public class FilterCriteria : IFilterCriteria
        {

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

        public static IQueryable<T> Filter<T, D>(this IQueryable<T> query, IList<FilterCriteria> filterCriterias, LogicalOperator logicalOperator = LogicalOperator.And)
        {
            if (filterCriterias != null && filterCriterias.Any())
            {
                var resultCondition = filterCriterias.ToExpression<T, D>(query, logicalOperator);

                var parameter = Expression.Parameter(query.ElementType, "p");

                if (resultCondition != null)
                {
                    var lambda = Expression.Lambda(resultCondition, parameter);

                    var mce = Expression.Call(
                        typeof(Queryable), "Any",
                        new[] { query.ElementType },
                        query.Expression,
                        lambda);

                    return query.Provider.CreateQuery<T>(mce);
                }
            }
            return query;
        }

        public static Expression ToExpression<T, D>(this IList<FilterCriteria> filterCriterias, IQueryable<T> query, LogicalOperator logicalOperator = LogicalOperator.And)
        {
            Expression resultCondition = null;
            try
            {
                if (filterCriterias.Any())
                {
                    var parameter = Expression.Parameter(query.ElementType, "p");

                    foreach (var filterCriteria in filterCriterias)
                    {
                        var propertyExpression = filterCriteria.PropertyToCompare.Split('.').Aggregate<string, MemberExpression>(null, (current, property) => Expression.Property(current ?? (parameter as Expression), property));

                        Expression valueExpression;
                        var constantExpression = Expression.Constant(filterCriteria.ValueToCompare);

                        if (!filterCriteria.IsList)
                        {
                            valueExpression = Expression.Convert(constantExpression, propertyExpression.Type);
                        }
                        else
                        {
                            valueExpression = Expression.Call(typeof(Enumerable), "Any", new[] { typeof(string) },
                                                              propertyExpression, filterCriteria.Expression,
                                                              Expression.Constant(filterCriteria.ValueToCompare,
                                                                                  typeof(string)));
                        }

                        Expression condition;
                        switch (filterCriteria.FilterOperator)
                        {
                            case FilterOperator.IsEqualTo:
                                condition = Expression.Equal(propertyExpression, valueExpression);
                                break;
                            case FilterOperator.IsNotEqualTo:
                                condition = Expression.NotEqual(propertyExpression, valueExpression);
                                break;
                            case FilterOperator.IsGreaterThan:
                                condition = Expression.GreaterThan(propertyExpression, valueExpression);
                                break;
                            case FilterOperator.IsGreaterThanOrEqualTo:
                                condition = Expression.GreaterThanOrEqual(propertyExpression, valueExpression);
                                break;
                            case FilterOperator.IsLessThan:
                                condition = Expression.LessThan(propertyExpression, valueExpression);
                                break;
                            case FilterOperator.IsLessThanOrEqualTo:
                                condition = Expression.LessThanOrEqual(propertyExpression, valueExpression);
                                break;
                            case FilterOperator.Contains:
                                //condition = Expression.Call(propertyExpression, typeof(string).GetMethod("Contains", new[] { typeof(string) }), valueExpression);
                                if (propertyExpression.Type.IsGenericType &&
                                    typeof(IEnumerable<>)
                                        .MakeGenericType(propertyExpression.Type.GetGenericArguments())
                                        .IsAssignableFrom(propertyExpression.Type))
                                {
                                    // find AsQueryable method
                                    var toQueryable = typeof(Queryable).GetMethods()
                                        .Where(m => m.Name == "AsQueryable")
                                        .Single(m => m.IsGenericMethod)
                                        .MakeGenericMethod(typeof(D));

                                    // find Any method
                                    var method = typeof(Queryable).GetMethods()
                                        .Where(m => m.Name == "Any")
                                        .Single(m => m.GetParameters().Length == 2)
                                        .MakeGenericMethod(typeof(D));

                                    // make expression
                                    condition = Expression.Call(
                                        null,
                                        method,
                                        Expression.Call(null, toQueryable, propertyExpression),
                                        filterCriteria.Expression
                                    );
                                }
                                else
                                {
                                    condition = Expression.Call(propertyExpression, typeof(string).GetMethod("Contains", new[] { typeof(string) }), valueExpression);
                                }
                                break;
                            case FilterOperator.StartsWith:
                                condition = Expression.Call(propertyExpression, typeof(string).GetMethod("StartsWith", new[] { typeof(string) }), valueExpression);
                                break;
                            case FilterOperator.EndsWith:
                                condition = Expression.Call(propertyExpression, typeof(string).GetMethod("EndsWith", new[] { typeof(string) }), valueExpression);
                                break;
                            default:
                                condition = valueExpression;
                                break;
                        }

                        if (resultCondition != null)
                        {
                            switch (logicalOperator)
                            {
                                case LogicalOperator.And:
                                    resultCondition = Expression.AndAlso(resultCondition, condition);
                                    break;
                                case LogicalOperator.Or:
                                    resultCondition = Expression.OrElse(resultCondition, condition);
                                    break;
                            }
                        }
                        else
                        {
                            resultCondition = condition;
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
            return resultCondition;
        }

        public static class PivotTable
        {
            public static PivotTable<TRow, TColumn, TValue> Create<TRow, TColumn, TValue>(Dictionary<TRow, Dictionary<TColumn, TValue>> dictionary)
            where TRow : IComparable, IEquatable<TRow>
            where TColumn : IComparable, IEquatable<TColumn>
            {
                return new PivotTable<TRow, TColumn, TValue>(dictionary);
            }
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

        public class Element
        {
            public string Name { get; set; }
            public int Value { get; set; }
        }

        public static PivotTable<TRow, TColumn, TValue> ToPivot<TItem, TRow, TColumn, TValue>(
            this IEnumerable<TItem> source,
            Func<TItem, TRow> rowSelector,
            Func<TItem, TColumn> colSelector,
            Func<IEnumerable<TItem>, TValue> aggregatFunc
        )
            where TRow : IComparable, IEquatable<TRow>
            where TColumn : IComparable, IEquatable<TColumn>
        {
            var dic = source
                .GroupBy(rowSelector)
                .ToDictionary(x => x.Key, x => x.GroupBy(colSelector).ToDictionary(y => y.Key, y => aggregatFunc(y)));

            return PivotTable.Create(dic);
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
