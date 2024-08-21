using LogManagamentAPI.DTOs;
using LogManagamentAPI.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Client;
using Nest;
using Serilog;
using Serilog.Sinks.Elasticsearch;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(builder.Configuration["Elasticsearch:Url"]!))
    {
        AutoRegisterTemplate = true,
        IndexFormat = "logs-{0:yyyy.MM.dd}"
    })
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog();

var settings = new ConnectionSettings(new Uri(builder.Configuration["Elasticsearch:Url"]!))
        .DefaultIndex("logs")    
        .PrettyJson()
        .RequestTimeout(TimeSpan.FromMinutes(2))
        .ServerCertificateValidationCallback((o, certificate, chain, errors) => true);

var client = new ElasticClient(settings);
builder.Services.AddSingleton(client);
builder.Services.AddSignalR();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();

app.MapHub<LogHub>("/logHub");

app.MapGet("/logs", async ([FromServices] ElasticClient elasticClient) =>
{
    //trazendo todos os dados que estao salvos no elasticSearch
    var searchResponse = await elasticClient.SearchAsync<LogEntry>(s => s
        .Index("logs")
        .Size(1000)
        .Sort(ss => ss.Descending(p => p.Timestamp))
    );

    return Results.Ok(searchResponse.Documents);
});

app.MapPost("/send-log", async (LogEntry logEntry) =>
{
    try
    {
        //criando a conexao com o HUB
        var hubConnection = new HubConnectionBuilder()
            .WithUrl("https://localhost:7276/logHub") 
            .Build();

        //conectando com o HUB
        await hubConnection.StartAsync();
        //chamando um metodo publico dele, que vai colocar o log no elasticsearch
        await hubConnection.InvokeAsync("SendLog", logEntry);
        return Results.Ok("Log sended");
    }
    catch (Exception ex)
    {
        return Results.Problem("Error when sending log: " + ex.Message);
    }
});

app.Run();
