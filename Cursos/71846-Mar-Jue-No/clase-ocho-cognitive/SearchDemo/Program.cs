      var configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

        // Obtener la configuración de Azure Search
        var serviceName = configuration["AzureSearch:ServiceName"];
        var apiKey = configuration["AzureSearch:ApiKey"];
        var indexName = configuration["AzureSearch:IndexName"];

        // Crear el cliente de búsqueda
        var searchClient = new SearchServiceClient(serviceName, new SearchCredentials(apiKey));
        var indexClient = searchClient.Indexes.GetClient(indexName);

        // Solicitar una consulta de búsqueda desde la consola
        Console.WriteLine("Ingrese el texto para buscar:");
        string searchText = Console.ReadLine();

        // Configurar los parámetros de búsqueda
        var parameters = new SearchParameters()
        {
            QueryType = QueryType.Full // Esto habilita búsqueda avanzada, incluida la de expresiones regulares
        };

        try
        {
            // Realizar la consulta
            var results = await indexClient.Documents.SearchAsync<String>(searchText, parameters);

            // Mostrar los resultados
            Console.WriteLine("\nResultados de la búsqueda:\n");

            foreach (var result in results.Results)
            {
                var accommodation = result.Document;
                Console.WriteLine(result.Document);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al realizar la búsqueda: {ex.Message}");
        }
    }
