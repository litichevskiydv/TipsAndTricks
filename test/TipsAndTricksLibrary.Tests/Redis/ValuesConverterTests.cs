namespace TipsAndTricksLibrary.Tests.Redis
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reflection;
    using JetBrains.Annotations;
    using StackExchange.Redis;
    using TipsAndTricksLibrary.Redis.Converters;
    using Xunit;

    public class ValuesConverterTests
    {
        [UsedImplicitly]
        public static IEnumerable<object[]> TestsData;

        private readonly ValuesConverter _converter;
        private readonly MethodInfo _convertGenericDefinition;

        static ValuesConverterTests()
        {
            TestsData = new[]
                        {
                            new object[] {5, (RedisValue) 5},
                            new object[] {1.5d, (RedisValue) 1.5d},
                            new object[] {"abc", (RedisValue) "abc"},
                            new object[] {new {A = 1}, (RedisValue)"{\"A\":1}" }
                        };
        }

        public ValuesConverterTests()
        {
            _converter = new ValuesConverter();

            Expression<Func<int, RedisValue>> intConverter = x => _converter.Convert(x);
            _convertGenericDefinition = ((MethodCallExpression) intConverter.Body).Method.GetGenericMethodDefinition();
        }

        [Theory]
        [MemberData(nameof(TestsData))]
        public void ShouldConvertToRedisValue(object value, RedisValue expectedRedisValue)
        {
            // When
            var convertDefinition = _convertGenericDefinition.MakeGenericMethod(value.GetType());
            var actualRedisValue = (RedisValue) convertDefinition.Invoke(_converter, new[] {value});

            // Then
            Assert.Equal(expectedRedisValue, actualRedisValue);
        }
    }
}