using System.Net;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;
using Confluent.Kafka;

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

app.MapGet("/api/service2/kafka", () =>
{
    var config = new ConsumerConfig
    {
        BootstrapServers = "192.168.49.2:31094",
        GroupId = "foo",
        AutoOffsetReset = AutoOffsetReset.Earliest
    };

    using (var c = new ConsumerBuilder<Ignore, string>(config).Build())
    {
        c.Subscribe("test");
        CancellationTokenSource cts = new CancellationTokenSource();
        Console.CancelKeyPress += (_, e) => {
            e.Cancel = true; // prevent the process from terminating.
            cts.Cancel();
        };
        
        var cr = c.Consume(cts.Token);
        
        return cr.Message.ToString();
    }
});

app.MapGet("/api/service2/kafka-write", () =>
{
    var config = new ProducerConfig
    {
        BootstrapServers = "192.168.49.2:31094",
        ClientId = Dns.GetHostName(),
    };
    
    using (var producer = new ProducerBuilder<Null, string>(config).Build())
    {
        producer.ProduceAsync("test", new Message<Null, string> {Value = "a log message"});
    }
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