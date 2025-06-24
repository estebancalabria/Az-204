// See https://aka.ms/new-console-template for more information
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

Console.WriteLine("Ingrese la URL del Key Vault:");
string keyVaultUrl = Console.ReadLine()!;

Console.WriteLine("Ingrese el nombre del secreto:");
string secretName = Console.ReadLine()!;

DefaultAzureCredential credential = new DefaultAzureCredential();
SecretClient client = new SecretClient(new Uri(keyVaultUrl), credential);

KeyVaultSecret secret = client.GetSecret(secretName);
Console.WriteLine($"El valor del secreto '{secretName}' es: {secret.Value}");
