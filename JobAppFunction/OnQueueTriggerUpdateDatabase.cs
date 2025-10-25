using System;
using Azure.Storage.Queues.Models;
using JobAppFunction.Data;
using JobAppFunction.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;


namespace JobAppFunction;

public class OnQueueTriggerUpdateDatabase
{
    private readonly ILogger<OnQueueTriggerUpdateDatabase> _logger;
    private readonly ApplicationDbContext _dbContext;

    public OnQueueTriggerUpdateDatabase(ILogger<OnQueueTriggerUpdateDatabase> logger, 
        ApplicationDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    //this function get triggered when the AZ Storage queue is updated
    [Function(nameof(OnQueueTriggerUpdateDatabase))]
    public void Run([QueueTrigger("SalesRequestInbound")] QueueMessage message)
    {
        string messageBody = message.Body.ToString();
        SalesRequest? salesRequest = JsonConvert.DeserializeObject<SalesRequest>(messageBody);

        if (salesRequest != null)
        {
            salesRequest.Status = "";
            _dbContext.SalesRequests.Add(salesRequest);
            _dbContext.SaveChanges();
        }
        else 
        {
            _logger.LogWarning("Failed to deserialize the message body into a SalesRequest object.");
        }
        
        _logger.LogInformation("C# Queue trigger function processed: {messageText}", message.MessageText);
    }
}