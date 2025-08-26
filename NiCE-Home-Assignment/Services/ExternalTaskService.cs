using System;
using System.Threading;

namespace NiCE_Home_Assignment.Services
{
    public class ExternalTaskService
    {
    
        public string GetTaskFromExternalSystem(string utterance)
        {
            for (int i = 0; i < 3; i++)
            {
                Random random = new Random();
                int randomNumber = random.Next(0, 3); // 33.33% to get 2.
                if (randomNumber == 2)
                {
                    return "Task";
                }
                Thread.Sleep(1000); // Sleep for 1 second and try again
            }
            return "Failed to get task from external dependency";
        }
    }
}
