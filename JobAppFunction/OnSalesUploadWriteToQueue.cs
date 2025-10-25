using JobAppFunction.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace JobAppFunction;

public class OnSalesUploadWriteToQueue
{
    private readonly ILogger<OnSalesUploadWriteToQueue> _logger;

    public OnSalesUploadWriteToQueue(ILogger<OnSalesUploadWriteToQueue> logger)
    {
        _logger = logger;
    }

    [Function("OnSalesUploadWriteToQueue")]
    //[QueueOutput("SalesRequestInbound", Connection = "AzureWebJobsStorage")]
    public async Task<SalesRequest> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
    {
        //return new OkObjectResult("Hello, World!");
        string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        SalesRequest? data = JsonConvert.DeserializeObject<SalesRequest>(requestBody);

        _logger.LogInformation("C# HTTP trigger function processed a request.");
        return data ?? new SalesRequest();
    }
}