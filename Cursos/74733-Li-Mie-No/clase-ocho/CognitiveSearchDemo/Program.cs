using Azure;
using Azure.Search.Documents;
using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;



Console.WriteLine("Enter the endpoint URL:");
string endpoint = Console.ReadLine()!;

Console.WriteLine("Enter Key:");
string key = Console.ReadLine()!;

string indexName = "demo-index";

var credential = new AzureKeyCredential(key);

// Crear el indice de busqueda
var indexClient = new SearchIndexClient(new Uri(endpoint), credential);
var fieldBuilder = new FieldBuilder();
var searchFields = fieldBuilder.Build(typeof(Document));
var definition = new SearchIndex(indexName, searchFields);
await indexClient.CreateOrUpdateIndexAsync(definition);


// Cargar los documentos
var searchClient = new SearchClient(new Uri(endpoint), indexName, credential);

/*var documents = new[]
{
    new Document { Id = "1", Title = "Aprender C#", Content = "Curso introductorio de programación." },
    new Document { Id = "2", Title = "Azure Cognitive Services", Content = "Usar servicios inteligentes en la nube. Azure." }
};

await searchClient.UploadDocumentsAsync(documents);
Console.WriteLine("Documentos cargados.");*/


//Buscar documentos
Console.WriteLine("Ingrese el texto a buscar:");
string searchText = Console.ReadLine()!;
var response = await searchClient.SearchAsync<Document>(searchText);
Console.WriteLine("Resultados de búsqueda:");

await foreach (var result in response.Value.GetResultsAsync())
{
    Console.WriteLine($"- {result.Document.Title}: {result.Document.Content}");
}

