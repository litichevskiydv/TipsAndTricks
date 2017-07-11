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

    public class KeysConverterTests
    {
        [UsedImplicitly]
        public static IEnumerable<object[]> TestsData;

        private readonly KeysConverter _converter;
        private readonly MethodInfo _convertGenericDefinition;

        static KeysConverterTests()
        {
            TestsData = new[]
                        {
                            new object[] {"1", (RedisKey) "1"},
                            new object[] {1, (RedisKey) "1"},
                            new object[] {new Guid("f64e12d5-c0eb-49fe-8631-3a61f4920b2e"), (RedisKey)"f64e12d5-c0eb-49fe-8631-3a61f4920b2e" } 
                        };
        }

        public KeysConverterTests()
        {
            _converter = new KeysConverter();

            Expression<Func<int, RedisKey>> intConverter = x => _converter.Convert(x);
            _convertGenericDefinition = ((MethodCallExpression)intConverter.Body).Method.GetGenericMethodDefinition();
        }

        [Theory]
        [MemberData(nameof(TestsData))]
        public void ShouldConvertToRedisKey(object key, RedisKey expectedRedisKey)
        {
            // When
            var convertDefinition = _convertGenericDefinition.MakeGenericMethod(key.GetType());
            var actualRedisValue = (RedisKey)convertDefinition.Invoke(_converter, new[] { key });

            // Then
            Assert.Equal(expectedRedisKey, actualRedisValue);
        }
    }
}