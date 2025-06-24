using Azure.Data.Tables;

Console.WriteLine("Bienvenido al cliente de Azure Storage Queue");

Console.WriteLine("Ingrese el connection string de Azure Storage:");
string connectionString = Console.ReadLine()!;

Console.WriteLine("Ingrese el nombre de la table:");
string tableName = Console.ReadLine()!;

Console.WriteLine("Ingree el nombre de la persona:");
string personaNombre = Console.ReadLine()!;

TableClient tableClient = new TableClient(connectionString, tableName);

try
{
    // Crear la tabla si no existe
    tableClient.CreateIfNotExists();
    Console.WriteLine($"Tabla '{tableName}' creada o ya existe.");

    // Insertar una entidad de ejemplo
    var entity = new TableEntity("Persona", Guid.NewGuid().ToString())
    {
        { "Nombre", personaNombre },
    };
    
    tableClient.AddEntity(entity);
    Console.WriteLine("Entidad insertada correctamente.");
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}