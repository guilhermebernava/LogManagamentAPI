using LogManagamentAPI.DTOs;
using Microsoft.AspNetCore.SignalR;
using Nest;

namespace LogManagamentAPI.Hubs;

public class LogHub : Hub
{
    private readonly ElasticClient _elasticClient;

    public LogHub(ElasticClient elasticClient)
    {
        _elasticClient = elasticClient;
    }
   
    public async Task<Result> SendLog(LogEntry logEntry)
    {
        try
        {
            var response = await _elasticClient.IndexDocumentAsync(logEntry);
            return response.Result;

        }
        catch (Exception e)
        {
            throw e;
        }
    }
}