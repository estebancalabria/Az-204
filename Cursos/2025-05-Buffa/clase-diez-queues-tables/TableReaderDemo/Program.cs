using Azure.Data.Tables;

Console.WriteLine("Bienvenido al cliente de Azure Storage Queue");

Console.WriteLine("Ingrese el connection string de Azure Storage:");
string connectionString = Console.ReadLine()!;

Console.WriteLine("Ingrese el nombre de la table:");
string tableName = Console.ReadLine()!;

TableClient tableClient = new TableClient(connectionString, tableName);// See https://aka.ms/new-console-template for more information

await foreach (var entity in tableClient.QueryAsync<TableEntity>(ent => true))
{
    Console.WriteLine($"Nombre Persona: {entity.GetString("Nombre")}");
}

Console.WriteLine("Presione cualquier tecla para salir...");
Console.ReadKey();