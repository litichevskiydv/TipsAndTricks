namespace TipsAndTricksLibrary.Tests.Parsing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Dapper;
    using JetBrains.Annotations;
    using TipsAndTricksLibrary.Parsing;
    using Xunit;

    public class ParsingResultHandlerTests : UsingDbTestBase
    {
        [UsedImplicitly]
        public class DateTimeTestEntity
        {
            public int Id { get; set; }

            public ParsingResult<DateTime> Date { get; set; }
        }

        [UsedImplicitly]
        public class BoolTestEntity
        {
            public int Id { get; set; }

            public ParsingResult<bool> Flag { get; set; }
        }

        [UsedImplicitly]
        public class IntTestEntity
        {
            public int Id { get; set; }

            public ParsingResult<int> Param { get; set; }
        }

        [UsedImplicitly]
        public class DecimalTestEntity
        {
            public int Id { get; set; }

            public ParsingResult<decimal> Param { get; set; }
        }

        [UsedImplicitly]
        public static IEnumerable<object[]> PassDateTimeParsingResultTestsData;
        [UsedImplicitly]
        public static IEnumerable<object[]> PassBoolParsingResultTestsData;
        [UsedImplicitly]
        public static IEnumerable<object[]> PassIntParsingResultTestsData;
        [UsedImplicitly]
        public static IEnumerable<object[]> PassDecimalParsingResultTestsData;

        static ParsingResultHandlerTests()
        {
            PassDateTimeParsingResultTestsData = new[]
                                                 {
                                                     new object[]
                                                     {
                                                         new DateTimeTestEntity
                                                         {
                                                             Id = 1,
                                                             Date = new ParsingResult<DateTime>(new DateTime(2016, 07, 17, 18, 51, 51))
                                                         }
                                                     },
                                                     new object[]
                                                     {
                                                         new DateTimeTestEntity
                                                         {
                                                             Id = 1,
                                                             Date = new ParsingResult<DateTime>(null)
                                                         }
                                                     }
                                                 };
            PassBoolParsingResultTestsData = new[]
                                             {
                                                 new object[] {new BoolTestEntity {Id = 1, Flag = new ParsingResult<bool>(true)}},
                                                 new object[] {new BoolTestEntity {Id = 1, Flag = new ParsingResult<bool>(null)}}
                                             };
            PassIntParsingResultTestsData = new[]
                                            {
                                                new object[] {new IntTestEntity {Id = 1, Param = new ParsingResult<int>(2)}},
                                                new object[] {new IntTestEntity {Id = 1, Param = new ParsingResult<int>(null)}}
                                            };
            PassDecimalParsingResultTestsData = new[]
                                            {
                                                new object[] {new DecimalTestEntity {Id = 1, Param = new ParsingResult<decimal>(2.5m)}},
                                                new object[] {new DecimalTestEntity { Id = 1, Param = new ParsingResult<decimal>(null)}}
                                            };
        }

        [Fact]
        public void ShouldReadNotNullDateTimeParsingResultFromDb()
        {
            // Given
            SqlMapper.AddTypeHandler(new DateTimeParsingResultHandler());

            // When
            DateTimeTestEntity actualResult;
            using (var connection = GetConnection())
                actualResult = connection.Query<DateTimeTestEntity>("select 1 as [Id], dateadd(dd, 1, '20160716') as [Date]").Single();

            //// Then
            Assert.Equal(1, actualResult.Id);
            Assert.Equal(new ParsingResult<DateTime>(new DateTime(2016, 07, 17)), actualResult.Date);
        }

        [Fact]
        public void ShouldReadNullDateTimeParsingResultFromDb()
        {
            // Given
            SqlMapper.AddTypeHandler(new DateTimeParsingResultHandler());

            // When
            DateTimeTestEntity actualResult;
            using (var connection = GetConnection())
                actualResult = connection.Query<DateTimeTestEntity>("select 1 as [Id], null as [Date]").Single();

            //// Then
            Assert.Equal(1, actualResult.Id);
            Assert.Equal(new ParsingResult<DateTime>(null), actualResult.Date);
        }

        [Theory]
        [MemberData(nameof(PassDateTimeParsingResultTestsData))]
        public void ShouldPassDateTimeParsingResultToDb(DateTimeTestEntity expectedResult)
        {
            // Given
            SqlMapper.AddTypeHandler(new DateTimeParsingResultHandler());

            // When
            DateTimeTestEntity actualResult;
            using (var connection = GetConnection())
                actualResult = connection.Query<DateTimeTestEntity>("select @Id as [Id], @Date as [Date]",
                    new
                    {
                        expectedResult.Id,
                        expectedResult.Date
                    }).Single();

            //// Then
            Assert.Equal(expectedResult.Id, actualResult.Id);
            Assert.Equal(expectedResult.Date, actualResult.Date);
        }

        [Fact]
        public void ShouldReadNotNullBoolParsingResultFromDb()
        {
            // Given
            SqlMapper.AddTypeHandler(new BoolParsingResultHandler());

            // When
            BoolTestEntity actualResult;
            using (var connection = GetConnection())
                actualResult = connection.Query<BoolTestEntity>(@"
declare @Id int = 1
declare @Flag bit = 0
select @Id as [Id], @Flag as [Flag]").Single();

            //// Then
            Assert.Equal(1, actualResult.Id);
            Assert.Equal(new ParsingResult<bool>(false), actualResult.Flag);
        }

        [Fact]
        public void ShouldReadNullBoolParsingResultFromDb()
        {
            // Given
            SqlMapper.AddTypeHandler(new BoolParsingResultHandler());

            // When
            BoolTestEntity actualResult;
            using (var connection = GetConnection())
                actualResult = connection.Query<BoolTestEntity>(@"
declare @Id int = 1
declare @Flag bit = null
select @Id as [Id], @Flag as [Flag]").Single();

            //// Then
            Assert.Equal(1, actualResult.Id);
            Assert.Equal(new ParsingResult<bool>(null), actualResult.Flag);
        }

        [Theory]
        [MemberData(nameof(PassBoolParsingResultTestsData))]
        public void ShouldPassBoolParsingResultToDb(BoolTestEntity expectedResult)
        {
            // Given
            SqlMapper.AddTypeHandler(new BoolParsingResultHandler());

            // When
            BoolTestEntity actualResult;
            using (var connection = GetConnection())
                actualResult = connection.Query<BoolTestEntity>("select @Id as [Id], @Flag as [Flag]",
                    new
                    {
                        expectedResult.Id,
                        expectedResult.Flag
                    }).Single();

            //// Then
            Assert.Equal(expectedResult.Id, actualResult.Id);
            Assert.Equal(expectedResult.Flag, actualResult.Flag);
        }

        [Fact]
        public void ShouldReadNotNullIntParsingResultFromDb()
        {
            // Given
            SqlMapper.AddTypeHandler(new IntParsingResultHandler());

            // When
            IntTestEntity actualResult;
            using (var connection = GetConnection())
                actualResult = connection.Query<IntTestEntity>(@"
declare @Id int = 1
declare @Param int = 77
select @Id as [Id], @Param as [Param]").Single();

            //// Then
            Assert.Equal(1, actualResult.Id);
            Assert.Equal(new ParsingResult<int>(77), actualResult.Param);
        }

        [Fact]
        public void ShouldReadNullIntParsingResultFromDb()
        {
            // Given
            SqlMapper.AddTypeHandler(new IntParsingResultHandler());

            // When
            IntTestEntity actualResult;
            using (var connection = GetConnection())
                actualResult = connection.Query<IntTestEntity>(@"
declare @Id int = 1
declare @Param int = null
select @Id as [Id], @Param as [Param]").Single();

            //// Then
            Assert.Equal(1, actualResult.Id);
            Assert.Equal(new ParsingResult<int>(null), actualResult.Param);
        }

        [Theory]
        [MemberData(nameof(PassIntParsingResultTestsData))]
        public void ShouldPassIntParsingResultToDb(IntTestEntity expectedResult)
        {
            // Given
            SqlMapper.AddTypeHandler(new IntParsingResultHandler());

            // When
            IntTestEntity actualResult;
            using (var connection = GetConnection())
                actualResult = connection.Query<IntTestEntity>("select @Id as [Id], @Param as [Param]",
                    new
                    {
                        expectedResult.Id,
                        expectedResult.Param
                    }).Single();

            //// Then
            Assert.Equal(expectedResult.Id, actualResult.Id);
            Assert.Equal(expectedResult.Param, actualResult.Param);
        }

        [Fact]
        public void ShouldReadNotNullDecimalParsingResultFromDb()
        {
            // Given
            SqlMapper.AddTypeHandler(new DecimalParsingResultHandler());

            // When
            DecimalTestEntity actualResult;
            using (var connection = GetConnection())
                actualResult = connection.Query<DecimalTestEntity>(@"
declare @Id int = 1
declare @Param numeric(10,5) = 2.3
select @Id as [Id], @Param as [Param]").Single();

            //// Then
            Assert.Equal(1, actualResult.Id);
            Assert.Equal(new ParsingResult<decimal>(2.3m), actualResult.Param);
        }

        [Fact]
        public void ShouldReadNullDecimalParsingResultFromDb()
        {
            // Given
            SqlMapper.AddTypeHandler(new DecimalParsingResultHandler());

            // When
            DecimalTestEntity actualResult;
            using (var connection = GetConnection())
                actualResult = connection.Query<DecimalTestEntity>(@"
declare @Id int = 1
declare @Param numeric(10,5) = null
select @Id as [Id], @Param as [Param]").Single();

            //// Then
            Assert.Equal(1, actualResult.Id);
            Assert.Equal(new ParsingResult<decimal>(null), actualResult.Param);
        }

        [Theory]
        [MemberData(nameof(PassDecimalParsingResultTestsData))]
        public void ShouldPassDecimalParsingResultToDb(DecimalTestEntity expectedResult)
        {
            // Given
            SqlMapper.AddTypeHandler(new DecimalParsingResultHandler());

            // When
            DecimalTestEntity actualResult;
            using (var connection = GetConnection())
                actualResult = connection.Query<DecimalTestEntity>("select @Id as [Id], @Param as [Param]",
                    new
                    {
                        expectedResult.Id,
                        expectedResult.Param
                    }).Single();

            //// Then
            Assert.Equal(expectedResult.Id, actualResult.Id);
            Assert.Equal(expectedResult.Param, actualResult.Param);
        }
    }
}