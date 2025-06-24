var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.UseHttpsRedirection();

app.MapGet("/", () =>
{
    return Results.Ok("<h1>Hello World!</h1>");
});

app.MapPost("/", async (HttpRequest request) =>
{
    StreamReader reader = new StreamReader(request.Body);
    string body = await reader.ReadToEndAsync();
    Console.WriteLine($"Received POST request with body: {body}");

    //Tengo que aceptar la subscripción de evento Eventgrid
    var events = System.Text.Json.JsonDocument.Parse(body).RootElement;
    if (events[0].TryGetProperty("eventType", out var eventType)
        && eventType.GetString() == "Microsoft.EventGrid.SubscriptionValidationEvent")
    {
        var validationCode = events[0].GetProperty("data").GetProperty("validationCode").GetString();
        Console.WriteLine($"Validation code: {validationCode}");
        
        return Results.Json(new {
            validationResponse = validationCode
        });
    }

    return Results.Ok();
});

app.Run();
