namespace TipsAndTricksLibrary.Tests
{
    using System;
    using System.Data.SqlClient;
    using JetBrains.Annotations;

    [UsedImplicitly]
    public abstract class UsingDbTestBase
    {
        private readonly bool _isAppVeyorWindows;
        private readonly bool _isAppVeyorLinux;

        private readonly bool _isTravis;

        private string ConnectionString
        {
            get
            {
                if (_isAppVeyorWindows)
                    return @"Data Source = (local)\SQL2016;Initial Catalog=tempdb;User Id=sa;Password=Password12!";
                if (_isAppVeyorLinux)
                    return "Data Source = localhost;Initial Catalog=tempdb;User Id=sa;Password=Password12!";

                return _isTravis
                    ? "Data Source = localhost;Initial Catalog=tempdb;User Id=sa;Password=Password12!"
                    : @"Data Source = (localdb)\MSSQLLocalDB;Initial Catalog = tempdb; Integrated Security = True";
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
            var isAppVeyor = Environment.GetEnvironmentVariable("APPVEYOR")?.ToUpperInvariant() == "TRUE";
            _isAppVeyorWindows = isAppVeyor && Environment.GetEnvironmentVariable("CI_WINDOWS")?.ToUpperInvariant() == "TRUE";
            _isAppVeyorLinux = isAppVeyor && Environment.GetEnvironmentVariable("CI_LINUX")?.ToUpperInvariant() == "TRUE";

            _isTravis = Environment.GetEnvironmentVariable("TRAVIS")?.ToUpperInvariant() == "TRUE";
        }
    }
}