
using Microsoft.Data.SqlClient;


Console.WriteLine("Username: ");
string username = Console.ReadLine()!.Trim();

Console.WriteLine("Password: ");
string password = Console.ReadLine()!.Trim();

string connectionString = $"Server=tcp:db4trainner.database.windows.net,1433;Initial Catalog=demo;Persist Security Info=False;User ID={username};Password={password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

using (SqlConnection connection = new SqlConnection(connectionString))
{
    try
    {
        connection.Open();
        Console.WriteLine("Connection successful!");

        SqlCommand qry = new SqlCommand("SELECT * FROM SalesLT.Product", connection);
        SqlDataReader reader = qry.ExecuteReader();
        while (reader.Read())
        {
            Console.WriteLine($"{reader["ProductID"]}, {reader["Name"]}, {reader["ProductNumber"]}, {reader["Color"]}, {reader["StandardCost"]}, {reader["ListPrice"]}");
        }

    }
    catch (SqlException ex)
    {
        Console.WriteLine($"Connection failed: {ex.Message}");
    }
}


