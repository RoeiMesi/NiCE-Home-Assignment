using NiCE_Home_Assignment.API.Controllers;
using System;
using System.Threading;

namespace NiCE_Home_Assignment.Services
{
    public class ExternalTaskService
    {
        private readonly ILogger<ExternalTaskService> _logger;
        public ExternalTaskService(ILogger<ExternalTaskService> logger)
        {
            _logger = logger;
        }

        public string GetTaskFromExternalSystem(string utterance)
        {
            for (int i = 0; i < 3; i++)
            {
                Random random = new Random();
                int randomNumber = random.Next(0, 3); // 33.33% to get 2.
                if (randomNumber == 2)
                {
                    _logger.LogInformation($"Succeeded to get task from external service in attempt number {i + 1}");
                    return "Task";
                }
                Thread.Sleep(1000); // Sleep for 1 second and try again
            }
            return "Failed to get task from external dependency";
        }
    }
}
