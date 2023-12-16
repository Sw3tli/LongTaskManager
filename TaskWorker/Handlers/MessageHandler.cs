using TaskWorker.Handlers.Interfaces;

namespace TaskWorker.Handlers
{
    public class MessageHandler : IMessageHandler
    {
        public void HandleMessage(string message)
        {
            switch (message)
            {
                case "starttask":
                    Console.WriteLine("Received a message to start a task.");
                    // Implement logic to start a long-running task here
                    // For demonstration, let's simulate a task with progress reporting
                    SimulateLongRunningTask();
                    break;
                case "taskprogress":
                    
                default:
                    Console.WriteLine($"Received an unknown message: {message}");
                    break;
            }
        }

        // Simulates a long-running task with progress reporting
        private void SimulateLongRunningTask()
        {
            int progress = 0;
            while (progress <= 100)
            {
                Console.WriteLine($"Task progress: {progress}%");
                progress += 2; // Increment progress by 2%
                Thread.Sleep(1000);
            }
            Console.WriteLine("Task completed.");
        }
    }
}
