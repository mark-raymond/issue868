using System;
using Microsoft.Data.SqlClient;

namespace GetServiceName
{
    internal static class Program
    {
        private static int Main(string[] args)
        {
            if (args.Length != 3)
            {
                Console.Error.WriteLine("Usage: GetServiceName SERVER USERNAME PASSWORD");
                return 1;
            }

            const string commandText = @"SELECT @@SERVICENAME";

            var server = args[0];
            var username = args[1];
            var password = args[2];

            var csb = new SqlConnectionStringBuilder
            {
                InitialCatalog = "master",
                DataSource = server,
                UserID = username,
                Password = password,
                IntegratedSecurity = false
            };

            try
            {
                var connectionString = csb.ToString();
                Console.WriteLine($"Connection string: {connectionString}");
                Console.WriteLine($"Executing SQL: {commandText}");

                using var connection = new SqlConnection(connectionString);
                connection.Open();
                using var command = new SqlCommand(commandText, connection);
                var result = command.ExecuteScalar();
                Console.WriteLine($"Result: {result}");
            }
            catch (Exception e)
            {
                Console.WriteLine("Caught an exception");
                Console.WriteLine(e.Message);
            }
            finally
            {
                Console.WriteLine("End of program");
            }

            return 0;
        }
    }
}
