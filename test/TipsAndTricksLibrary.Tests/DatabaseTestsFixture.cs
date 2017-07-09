namespace TipsAndTricksLibrary.Tests
{
    using System;
    using System.Data.SqlClient;
    using JetBrains.Annotations;

    [UsedImplicitly]
    public abstract class UsingDbTestBase
    {
        private static readonly bool IsAppVeyor = Environment.GetEnvironmentVariable("Appveyor")?.ToUpperInvariant() == "TRUE";
        private static readonly bool IsTravis = Environment.GetEnvironmentVariable("TRAVIS")?.ToUpperInvariant() == "TRUE";

        protected static string ConnectionString
        {
            get
            {
                if (IsAppVeyor)
                    return @"Data Source = (local)\SQL2016;Initial Catalog=tempdb;User Id=sa;Password=Password12!";
                return IsTravis
                    ? @"Data Source = localhost;Initial Catalog=tempdb;User Id=sa;Password=Password12!"
                    : @"Data Source = (localdb)\MSSQLLocalDB;Initial Catalog = tempdb; Integrated Security = True";
            }
        }

        protected static SqlConnection GetConnection()
        {
            var connection = new SqlConnection(ConnectionString);
            connection.Open();
            return connection;
        }
    }
}