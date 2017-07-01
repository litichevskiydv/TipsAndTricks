namespace TipsAndTricksLibrary.Tests.Redis
{
    using TipsAndTricksLibrary.Redis.Converters;
    using Xunit;

    public class ValuesConverterTests
    {
        private readonly ValuesConverter _converter;

        public ValuesConverterTests()
        {
            _converter = new ValuesConverter();
        }

        [Fact]
        public void ShouldConvertIntToRedisValue()
        {
            // Given
            const int expectedValue = 5;

            // When
            var actualRedisValue = _converter.Convert(expectedValue);

            // Then
            Assert.Equal(expectedValue, actualRedisValue);
        }
    }
}