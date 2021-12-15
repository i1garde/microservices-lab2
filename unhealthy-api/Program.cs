using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

var status = "OK";

app.MapGet("/api/service2", async () =>
{
    if (status != "OK")
    {
        await Lab.Wait();
    }

    Message message = new Message {Route = "/api/service2", Status = $"{status}", Responce = "Hello World!"};
    return message;
});

app.MapGet("/api/service2/untested-request", () =>
{
    status = "FAILED";
    return $"Status: {status}\nService is broken!";
});

app.Run();

public class Message
{
    public string Route { get; set; }
    
    public string Status { get; set; }
    public string Responce { get; set; }
}

public static class Lab
{
    public static async Task Wait() => await Task.Delay(10000);
}