var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapGet("/api/root-service", () =>
{
    using var client = new HttpClient();
    var result1 = client.GetAsync("http://192.168.49.2/api/service1");
    var result2 = client.GetAsync("http://192.168.49.2/api/service2");
    return result1.Result.ToString() + result2.Result.ToString();
});

app.Run();