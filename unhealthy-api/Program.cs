var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

var status = "OK";

app.MapGet("/api/service2", () =>
{
    if (status != "OK")
    {
        Thread.Sleep(10000);
    }
    return $"Status: {status}\nHello World!";
});

app.MapGet("/api/service2/untested-request", () =>
{
    status = "FAILED";
    return $"Status: {status}\nService is broken!";
});

app.Run();