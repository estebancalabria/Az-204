using Microsoft.Data.SqlClient;

Console.WriteLine("Enter the database connection string:");
string connectionString = Console.ReadLine()!;

try
{
    using (SqlConnection connection = new SqlConnection(connectionString))
    {
        connection.Open();
        string query = "SELECT * from SalesLT.Customer";
        SqlCommand command = new SqlCommand(query, connection);
        SqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            Console.WriteLine(reader["FirstName"] + " " + reader["LastName"]);
        }

        Console.WriteLine("Connection successful!");
    }
}
catch (SqlException ex)
{
    Console.WriteLine("Connection failed. Error: " + ex.Message);
}