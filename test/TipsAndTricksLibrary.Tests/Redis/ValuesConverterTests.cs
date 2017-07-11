namespace TipsAndTricksLibrary.Tests.Redis
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reflection;
    using JetBrains.Annotations;
    using StackExchange.Redis;
    using TipsAndTricksLibrary.Redis.StackExhange.Converters;
    using Xunit;

    public class ValuesConverterTests
    {
        [UsedImplicitly]
        public static IEnumerable<object[]> ConvertToTestsData;
        [UsedImplicitly]
        public static IEnumerable<object[]> ConvertFromTestsData;

        private readonly ValuesConverter _converter;
        private readonly MethodInfo _convertToGenericDefinition;
        private readonly MethodInfo _convertFromGenericDefinition;

        static ValuesConverterTests()
        {
            ConvertToTestsData = new[]
                                 {
                                     new object[] {5, (RedisValue) 5},
                                     new object[] {1.5d, (RedisValue) 1.5d},
                                     new object[] {"abc", (RedisValue) "abc"},
                                     new object[] {new {A = 1}, (RedisValue) "{\"A\":1}"}
                                 };
            ConvertFromTestsData = new[]
                                   {
                                       new object[] {(RedisValue) 5, 5},
                                       new object[] {(RedisValue) 1.5d, 1.5d},
                                       new object[] {(RedisValue) "abc", "abc"},
                                       new object[] {(RedisValue) "{\"A\":1}", new {A = 1}}
                                   };
        }

        public ValuesConverterTests()
        {
            _converter = new ValuesConverter();

            Expression<Func<int, RedisValue>> intToRedisValueConverter = x => _converter.ConvertTo(x);
            _convertToGenericDefinition = ((MethodCallExpression) intToRedisValueConverter.Body).Method.GetGenericMethodDefinition();

            Expression<Func<RedisValue, int>> fromRedisValueToIntConverter = x => _converter.ConvertFrom<int>(x);
            _convertFromGenericDefinition = ((MethodCallExpression)fromRedisValueToIntConverter.Body).Method.GetGenericMethodDefinition();
        }

        [Theory]
        [MemberData(nameof(ConvertToTestsData))]
        public void ShouldConvertToRedisValue(object value, RedisValue expectedRedisValue)
        {
            // When
            var convertDefinition = _convertToGenericDefinition.MakeGenericMethod(value.GetType());
            var actualRedisValue = (RedisValue) convertDefinition.Invoke(_converter, new[] {value});

            // Then
            Assert.Equal(expectedRedisValue, actualRedisValue);
        }

        [Theory]
        [MemberData(nameof(ConvertFromTestsData))]
        public void ShouldConvertFromRedisValue(RedisValue redisValue, object expectedValue)
        {
            // Whene
            var convertDefinition = _convertFromGenericDefinition.MakeGenericMethod(expectedValue.GetType());
            var actualValue = convertDefinition.Invoke(_converter, new object[] { redisValue });

            // Then
            Assert.Equal(expectedValue, actualValue);
        }
    }
}