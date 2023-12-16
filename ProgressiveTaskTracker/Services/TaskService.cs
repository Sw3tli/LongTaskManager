using ProgressiveTaskTracker.Services.Interfaces;

namespace ProgressiveTaskTracker.Services
{
    public class TaskService : ITaskService
    {
        public TaskService()
        {
        }

        public async Task<string> StartTaskAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetTaskProgressAsync()
        {
            throw new NotImplementedException();
        }
    }
}
