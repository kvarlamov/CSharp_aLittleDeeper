using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

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

    //todo - add Nullable long type
}