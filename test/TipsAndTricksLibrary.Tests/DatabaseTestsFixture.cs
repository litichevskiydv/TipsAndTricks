namespace TipsAndTricksLibrary.Tests
{
    using System;
    using System.Data.SqlClient;
    using JetBrains.Annotations;

    [UsedImplicitly]
    public abstract class UsingDbTestBase
    {
        private readonly bool _isAppVeyor;
        private readonly bool _isGithubActions;

        private string ConnectionString
        {
            get
            {
                if (_isAppVeyor)
                    return @"Data Source = (local)\SQL2017;Initial Catalog=tempdb;User Id=sa;Password=Password12!";
                if (_isGithubActions)
                    return "Data Source = localhost;Initial Catalog=tempdb;User Id=sa;Password=Password12!";


                return @"Data Source = (localdb)\MSSQLLocalDB;Initial Catalog = tempdb; Integrated Security = True";
            }
        }

        protected SqlConnection GetConnection()
        {
            var connection = new SqlConnection(ConnectionString);
            connection.Open();
            return connection;
        }

        protected UsingDbTestBase()
        {
            _isAppVeyor = Environment.GetEnvironmentVariable("APPVEYOR")?.ToUpperInvariant() == "TRUE";
            _isGithubActions = Environment.GetEnvironmentVariable("GITHUB_ACTIONS")?.ToUpperInvariant() == "TRUE";
        }
    }
}