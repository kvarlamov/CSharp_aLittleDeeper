using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace p23_ExpressionTrees;

public class IncludeOperatorStrategy<TEntity> : IOperatorStrategy<TEntity> where TEntity : class
{
    public ConditionOperatorKind ConditionOperatorKind => ConditionOperatorKind.GreaterThan;

    public Expression<Func<TEntity, bool>> GetNextExpression<TValue>(string prop, TValue value) =>
        GetNextExpressionInternal(prop, new List<TValue>() {value});

    public Expression<Func<TEntity, bool>> GetNextExpression<TValue>(string prop, List<TValue> values) => GetNextExpressionInternal(prop, values);
        
    private Expression<Func<TEntity, bool>> GetNextExpressionInternal<TValue>(string prop, List<TValue> values)
    {
        ParameterExpression param = Expression.Parameter(typeof(TEntity), "x");
        MemberExpression property = Expression.Property(param, prop);
        Expression body;
        if (typeof(TValue) == typeof(string))
        {
            var likeExpression = new List<MethodCallExpression>();
            foreach (var value in values)
            {
                string stringValue = value as string;
                if (string.IsNullOrWhiteSpace(stringValue))
                    continue;

                string searchString = new StringBuilder("%").Append(stringValue.Replace(" ", "%")).Append("%")
                    .ToString().ToLower();
                ConstantExpression constant = Expression.Constant(searchString);

                MethodInfo toLowerMethod = typeof(string).GetMethod("ToLower", Type.EmptyTypes);
                MethodCallExpression lowerProperty = Expression.Call(property, toLowerMethod);

                MethodCallExpression like = Expression.Call(
                    typeof(DbFunctionsExtensions),
                    nameof(DbFunctionsExtensions.Like),
                    null,
                    Expression.Default(typeof(DbFunctions)),
                    lowerProperty,
                    constant
                );
                likeExpression.Add(like);
            }

            body = likeExpression.Aggregate<MethodCallExpression, Expression>(null,
                (current, call) => current != null ? Expression.OrElse(current, call) : call);
        }
        else
        {
            MethodInfo methodInfo = typeof(List<TValue>).GetMethod("Contains");
            ConstantExpression list = Expression.Constant(values);
            body = Expression.Call(list, methodInfo!, property);
        }

        return Expression.Lambda<Func<TEntity, bool>>(body, param);
    }
}