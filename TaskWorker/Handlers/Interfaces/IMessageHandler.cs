namespace TaskWorker.Handlers.Interfaces
{
    public interface IMessageHandler
    {
        void HandleMessage(string message);
    }
}