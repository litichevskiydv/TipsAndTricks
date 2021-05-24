namespace TipsAndTricksLibrary.Tests.LanguageDepths
{
    using System;
    using System.Linq;
    using Xunit;

    public class FunctionsEqualityTests
    {
        private int EvenNumbersHandler(int value) => value;

        private int OddNumbersHandler(int value) => value + 1;

        private Func<int, int> GetHandler(int value)
        {
            if (value % 2 == 0)
                return EvenNumbersHandler;

            return OddNumbersHandler;
        }

        [Fact]
        public void ShouldDetectSameFunctions()
        {
            // Given
            var numbers = new[] {1, 2, 3, 4};

            // When
            var numbersByHandlers = numbers.GroupBy(GetHandler).ToArray();

            // Then
            Assert.Equal(2, numbersByHandlers.Length);
            Assert.Equal(new[] {numbers[0], numbers[2]}, numbersByHandlers[0].ToArray());
            Assert.Equal(new[] {numbers[1], numbers[3]}, numbersByHandlers[1].ToArray());
        }

    }
}