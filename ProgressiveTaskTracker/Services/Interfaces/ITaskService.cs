namespace ProgressiveTaskTracker.Services.Interfaces
{
    public interface ITaskService
    {
        Task<string> StartTaskAsync();

        Task<string> GetTaskProgressAsync();
    }
}