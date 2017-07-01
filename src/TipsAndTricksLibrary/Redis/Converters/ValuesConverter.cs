namespace TipsAndTricksLibrary.Redis.Converters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using Newtonsoft.Json;
    using StackExchange.Redis;

    public class ValuesConverter : IValuesConverter
    {
        private readonly Dictionary<Type, Func<object, RedisValue>> _availableConverters;

        public ValuesConverter()
        {
            var redisValueType = typeof(RedisValue);

            Expression<Func<int, RedisValue>> intConverter = x => x;
            var methodName = ((UnaryExpression) intConverter.Body).Method.Name;
            var otherConverters = typeof(RedisValue).GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Where(x => x.Name == methodName && x.ReturnType == redisValueType)
                .ToArray();

            var objectType = typeof(object);
            _availableConverters = new Dictionary<Type, Func<object, RedisValue>>();
            foreach (var otherConverter in otherConverters)
            {
                var convertibleType = otherConverter.GetParameters().Single().ParameterType;

                var parameter = Expression.Parameter(objectType, "x");
                var converter = Expression.Lambda(
                        Expression.Convert(
                            Expression.Convert(parameter, convertibleType),
                            redisValueType,
                            otherConverter),
                        parameter)
                    .Compile();

                _availableConverters.Add(convertibleType, (Func<object, RedisValue>) converter);
            }
        }

        public RedisValue Convert<TValue>(TValue value)
        {
            Func<object, RedisValue> converter;
            if (_availableConverters.TryGetValue(typeof(TValue), out converter))
                return converter(value);
            return JsonConvert.SerializeObject(value);
        }
    }
}