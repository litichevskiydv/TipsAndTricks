namespace TipsAndTricksLibrary.Tests.Cryptography.Rsa
{
    using System;
    using System.Collections.Generic;
    using System.Security.Cryptography;
    using JetBrains.Annotations;
    using KellermanSoftware.CompareNetObjects;
    using TipsAndTricksLibrary.Cryptography.Rsa;
    using Xunit;

    public class OpensslFormatParserTests
    {
        #region TestCases

        public class PrivateKeyParsingTestCase
        {
            public string Key { get; set; }

            public RSAParameters Expected { get; set; }
        }

        #endregion

        private readonly OpensslFormatParser _parser;

        [UsedImplicitly]
        public static TheoryData<PrivateKeyParsingTestCase> PrivateKeyParsingTestCases;

        public OpensslFormatParserTests()
        {
            _parser = new OpensslFormatParser();
        }

        static OpensslFormatParserTests()
        {
            PrivateKeyParsingTestCases =
                new TheoryData<PrivateKeyParsingTestCase>
                {
                    new PrivateKeyParsingTestCase
                    {
                        Key = "MIIEowIBAAKCAQEAyLd+6VxX/2Ubq9z2jR+PACUjQOsfpXtoTGGv3XacIlD9bE09LMKtE14KdEbwX2Z14raZ257g0Gk1svnNJDr1w45uNUlybV4VzRMcKP3aJTu+PILoG/sNEeuP8Y/AFO3dHxU7zZ4vyleWlqVK2UZIgg07XYntkAm+nQM4SflGBqSzSjFyt5eDKSTVpSZA6+Ed+DSgJYB+uDbDxrwMqZzlTa94szR/mRgri18rn1cRe8W/DlNoG6Kzq1fLveDzgODGlaCrcLyHXsEmdEVoXyFU7eHfzvg7lE5H6QMwlUCzNleR4Jbc6zQIaJUm/I0JU0Xdx1kYY+AWE2hUgq+7OSwfFQIDAQABAoIBAAEOfjuKQehVRu0Dr8SUavNMPsBDvJnpaWYliYB39GB13q+oRG8s4y5b62ArU26dIne8EFlIn9RZCXBUwlCzgsGxyUC8jz6mJSU8OYS5uWFCzECTS9eB3dK/U7Wo1REHI4fbW8I8V/IPvfozCo7UQON2YF6gsEB23KsE7lHtXwH/748y8OhAg9CUtXZ6wnMxiFlwCJxwPgzSvcxc/3wvXxuGjdo6AGsBxzAARANJMxjiLjXZLto5IlMBNVkqp99z7z8gN0ZkfC6RqdE4uWU/4HzQ8us2C6D7Ha5gyLGeU6CPBkEcC7EdRyMZXSUpA8IE0Os3qCvLZEE7lq9ylws1G80CgYEA/FUv0ctje/tr9jEVK4OT9j8Z5DbYj5WUowMa8veuf8+I6PC7TcsmxwCsh34zyICiO7eg2APoJGG1HvOmvvPNm6H14pSROyeDU2EK4dsyrhgz9otcF4R1t1p1N3kQpW1/a6UZS3b62cuVKOdAH604bPiEX2FK/E/pbwVLxaXT0ksCgYEAy6JFEw1IA2W6UxH0MmGi0+VXi1md7NxLl2lAv8YSie20wCd/ZP+DbZwrlOEqa8txWz9nGREPfKJIoKOji0mxcWZWo06eoMRXgkeCyVFFqgfQPbyWUspI3J6Iub0xWwpmwfWEZNBB5ltUZw/81ZbJOGONMxwiyqUwTVjTpBpH+B8CgYEAqSMMt6qGV7+yoUZLM2yyDDhFyaQ9yxjvlcjqEnyVGhTpZ4Kdekq3BmNcQkIEwdv/Ytn2VXVg8KoRttqJJavUYQLSB3ugUa9tpsEAg13jfbC5fAPzycu/ABUBRxq+XrSj2WrEJsJs6Po+VDmV0OXSUbDPQgO8OJ30EbfSD3zVHDsCgYBihytg4oY7jpcu5nr/fZppaIiJVl1BY/33TugTYmVJ84gHiZTsa21ONcgoiyIZciBxBCaATG7v4R2/DC5vkMKYmenFrp62LwogcTVo7zgD4y/xDzS07kd+5/5D0LT21fuOUZszpEieiwY1r9ioCANok1KYrj5vSqoqYpZUypqUBQKBgCrIPqoWJq2Vhfz/EU3SUnusS32YVDxyHWC5SZ45K+NeykHRXTBne0C3dnDOg4GTB60IrGhJCz/IVEM+K+Lu81zf2yeULTHE+Cdw3UkooPNbMaJPDLbYi3/lQYtvZmemonEkyaaTBjtcZucokeHkkAQAIzmxLNS31GKMGiXxaZXp",
                        Expected
                            = new RSAParameters
                              {
                                  Modulus = Convert.FromBase64String("yLd+6VxX/2Ubq9z2jR+PACUjQOsfpXtoTGGv3XacIlD9bE09LMKtE14KdEbwX2Z14raZ257g0Gk1svnNJDr1w45uNUlybV4VzRMcKP3aJTu+PILoG/sNEeuP8Y/AFO3dHxU7zZ4vyleWlqVK2UZIgg07XYntkAm+nQM4SflGBqSzSjFyt5eDKSTVpSZA6+Ed+DSgJYB+uDbDxrwMqZzlTa94szR/mRgri18rn1cRe8W/DlNoG6Kzq1fLveDzgODGlaCrcLyHXsEmdEVoXyFU7eHfzvg7lE5H6QMwlUCzNleR4Jbc6zQIaJUm/I0JU0Xdx1kYY+AWE2hUgq+7OSwfFQ=="),
                                  Exponent = Convert.FromBase64String("AQAB"),
                                  P = Convert.FromBase64String("/FUv0ctje/tr9jEVK4OT9j8Z5DbYj5WUowMa8veuf8+I6PC7TcsmxwCsh34zyICiO7eg2APoJGG1HvOmvvPNm6H14pSROyeDU2EK4dsyrhgz9otcF4R1t1p1N3kQpW1/a6UZS3b62cuVKOdAH604bPiEX2FK/E/pbwVLxaXT0ks="),
                                  Q = Convert.FromBase64String("y6JFEw1IA2W6UxH0MmGi0+VXi1md7NxLl2lAv8YSie20wCd/ZP+DbZwrlOEqa8txWz9nGREPfKJIoKOji0mxcWZWo06eoMRXgkeCyVFFqgfQPbyWUspI3J6Iub0xWwpmwfWEZNBB5ltUZw/81ZbJOGONMxwiyqUwTVjTpBpH+B8="),
                                  DP = Convert.FromBase64String("qSMMt6qGV7+yoUZLM2yyDDhFyaQ9yxjvlcjqEnyVGhTpZ4Kdekq3BmNcQkIEwdv/Ytn2VXVg8KoRttqJJavUYQLSB3ugUa9tpsEAg13jfbC5fAPzycu/ABUBRxq+XrSj2WrEJsJs6Po+VDmV0OXSUbDPQgO8OJ30EbfSD3zVHDs="),
                                  DQ = Convert.FromBase64String("YocrYOKGO46XLuZ6/32aaWiIiVZdQWP9907oE2JlSfOIB4mU7GttTjXIKIsiGXIgcQQmgExu7+Edvwwub5DCmJnpxa6eti8KIHE1aO84A+Mv8Q80tO5Hfuf+Q9C09tX7jlGbM6RInosGNa/YqAgDaJNSmK4+b0qqKmKWVMqalAU="),
                                  InverseQ = Convert.FromBase64String("Ksg+qhYmrZWF/P8RTdJSe6xLfZhUPHIdYLlJnjkr417KQdFdMGd7QLd2cM6DgZMHrQisaEkLP8hUQz4r4u7zXN/bJ5QtMcT4J3DdSSig81sxok8MttiLf+VBi29mZ6aicSTJppMGO1xm5yiR4eSQBAAjObEs1LfUYowaJfFplek="),
                                  D = Convert.FromBase64String("AQ5+O4pB6FVG7QOvxJRq80w+wEO8melpZiWJgHf0YHXer6hEbyzjLlvrYCtTbp0id7wQWUif1FkJcFTCULOCwbHJQLyPPqYlJTw5hLm5YULMQJNL14Hd0r9TtajVEQcjh9tbwjxX8g+9+jMKjtRA43ZgXqCwQHbcqwTuUe1fAf/vjzLw6ECD0JS1dnrCczGIWXAInHA+DNK9zFz/fC9fG4aN2joAawHHMABEA0kzGOIuNdku2jkiUwE1WSqn33PvPyA3RmR8LpGp0Ti5ZT/gfNDy6zYLoPsdrmDIsZ5ToI8GQRwLsR1HIxldJSkDwgTQ6zeoK8tkQTuWr3KXCzUbzQ==")
                              }
                    }
                };
        }

        [Theory]
        [MemberData(nameof(PrivateKeyParsingTestCases))]
        public void ShouldParsePrivateKey(PrivateKeyParsingTestCase testCase)
        {
            // When
            var actual = _parser.ParsePrivateKey(testCase.Key);

            // Then
            Assert.True(new CompareLogic().Compare(testCase.Expected, actual).AreEqual);
        }
    }
}