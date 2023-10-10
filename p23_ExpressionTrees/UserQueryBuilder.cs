using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace p23_ExpressionTrees
{
    public class UserQueryBuilder : BaseQueryBuilder<User>
    {
        private readonly List<ITypeStrategy<User>> _typeStrategies;

        private readonly Dictionary<string, Type> _propertyTypes = new Dictionary<string, Type>()
        {
            {nameof(User.Id), typeof(long)},
            {nameof(User.FirstName), typeof(string)},
            {nameof(User.LastName), typeof(string)},
            {nameof(User.Gender), typeof(long)},
            {nameof(User.BirthDayDate), typeof(DateTime)}
        };

        public UserQueryBuilder()
        {
            // todo inject strategies or add manually
        }
        
        protected override Expression<Func<User, bool>> GetNextExpression(Filter filter)
        {
            if (!_propertyTypes.TryGetValue(filter.Name, out var propType))
                return null;

            return GetTypeStrategy(propType).GetExpression(filter);
        }

        private ITypeStrategy<User> GetTypeStrategy(Type type)
        {
            var strategy = _typeStrategies.FirstOrDefault(s => s.PropType == type);
            if (_typeStrategies.FirstOrDefault(s => s.PropType == type) == null)
                throw new NotImplementedException("strategy with provided type not found");

            return strategy;
        }
    }

    public interface ITypeStrategy<TEntity> where TEntity : class
    {
        Type PropType { get; }
        
        Expression<Func<TEntity, bool>> GetExpression(Filter filter);
    }

    public class StringTypeStrategy<TEntity> : ITypeStrategy<TEntity> where TEntity : class
    {
        private readonly IOperatorFactory<TEntity> _operatorFactory;

        public StringTypeStrategy(IOperatorFactory<TEntity> operatorFactory)
        {
            _operatorFactory = operatorFactory;
        }
        
        public Type PropType => typeof(string);
        public Expression<Func<TEntity, bool>> GetExpression(Filter filter)
        {
            return _operatorFactory.GetNextExpression(filter);
        }
    }

    public interface IOperatorFactory<TEntity> where TEntity : class
    {
        Expression<Func<TEntity, bool>> GetNextExpression<TValue>(string prop, TValue value, ConditionOperatorKind conditionOperatorKind);
    }

    public class OperatorFactory<TEntity> : IOperatorFactory<TEntity> where TEntity : class
    {
        private readonly IEnumerable<IOperatorStrategy<TEntity>> _operatorStrategies;

        public OperatorFactory(IEnumerable<IOperatorStrategy<TEntity>> operatorStrategies)
        {
            _operatorStrategies = operatorStrategies;
        }
        
        public Expression<Func<TEntity, bool>> GetNextExpression<TValue>(string prop, TValue value, ConditionOperatorKind conditionOperatorKind) => 
            GetTypeStrategy(conditionOperatorKind).GetNextExpression(prop, value);

        private IOperatorStrategy<TEntity> GetTypeStrategy(ConditionOperatorKind? conditionOperatorKind)
        {
            var strategy = _operatorStrategies.FirstOrDefault(s => s.ConditionOperatorKind == conditionOperatorKind);
            if (strategy == null)
                throw new NotImplementedException($"strategy with provided {nameof(conditionOperatorKind)} not found");

            return strategy;
        }
    }

    public interface IOperatorStrategy<TEntity> where TEntity : class
    {
        ConditionOperatorKind ConditionOperatorKind { get; }

        Expression<Func<TEntity, bool>> GetNextExpression<TValue>(string prop, TValue value);
        Expression<Func<TEntity, bool>> GetNextExpression<TValue>(string prop, List<TValue> values);
    }
    
    public class EqualOperatorStrategy<TEntity> : IOperatorStrategy<TEntity> where TEntity : class
    {
        public ConditionOperatorKind ConditionOperatorKind => ConditionOperatorKind.Equal;
        public Expression<Func<TEntity, bool>> GetNextExpression<TValue>(string prop, TValue value)
        {
            ParameterExpression param = Expression.Parameter(typeof(TEntity), "x");
            MemberExpression property = Expression.Property(param, prop);
            ConstantExpression constant = Expression.Constant(value, typeof(TValue));
            BinaryExpression equal = Expression.Equal(property, constant);
            
            return Expression.Lambda<Func<TEntity, bool>>(equal, param);
        }

        public Expression<Func<TEntity, bool>> GetNextExpression<TValue>(string prop, List<TValue> values) => null;
    }
    
    public class GreaterThanOperatorStrategy<TEntity> : IOperatorStrategy<TEntity> where TEntity : class
    {
        public ConditionOperatorKind ConditionOperatorKind => ConditionOperatorKind.GreaterThan;
        public Expression<Func<TEntity, bool>> GetNextExpression<TValue>(string prop, TValue value)
        {
            ParameterExpression param = Expression.Parameter(typeof(TEntity), "x");
            MemberExpression property = Expression.Property(param, prop);
            ConstantExpression constant = Expression.Constant(value, typeof(TValue));
            BinaryExpression equal = Expression.GreaterThan(property, constant);
            
            return Expression.Lambda<Func<TEntity, bool>>(equal, param);
        }

        public Expression<Func<TEntity, bool>> GetNextExpression<TValue>(string prop, List<TValue> values) => null;
    }
    
    public class ContainsAllOperatorStrategy<TEntity> : IOperatorStrategy<TEntity> where TEntity : class
    {
        public ConditionOperatorKind ConditionOperatorKind => ConditionOperatorKind.Contains;
        public Expression<Func<TEntity, bool>> GetNextExpression<TValue>(string prop, TValue value) => null;

        public Expression<Func<TEntity, bool>> GetNextExpression<TValue>(string prop, List<TValue> values)
        {
            ParameterExpression param = Expression.Parameter(typeof(TEntity), "x");
            MemberExpression property = Expression.Property(param, prop);
            BinaryExpression containsAll = null;

            foreach (var value in values)
            {
                ConstantExpression constant = Expression.Constant(value, typeof(TValue));
                BinaryExpression expr = Expression.Equal(property, constant);
                containsAll = containsAll != null ? Expression.OrElse(containsAll, expr) : expr;
            }
            
            
            return Expression.Lambda<Func<TEntity, bool>>(containsAll!, param);
        }
    }
    
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
                        typeof(DbFunctionExtensions),
                        nameof(DbFunctionExtensions.Like),
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
}