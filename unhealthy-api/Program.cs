var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

var status = "OK";

app.MapGet("/api/service2", () =>
{
    if (status != "OK")
    {
        Thread.Sleep(10000);
    }
    return "Hello World!";
});

app.MapGet("/api/service2/untested-request", () =>
{
    status = "FAILED";
    return "Service is broken!";
});

app.Run();