using Microsoft.AspNetCore.Connections;
using TaskWorker.Handlers.Interfaces;

namespace TaskWorker.Receivers
{
    public class TaskReceiver : IDisposable
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly IMessageHandler _messageHandler;
        private const string QueueName = "task_queue";

        public RabbitMQReceiver(IMessageHandler messageHandler)
        {
            _messageHandler = messageHandler;

            var factory = new ConnectionFactory() { HostName = "localhost" };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: QueueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                _messageHandler.HandleMessage(message);

                _channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
            };

            _channel.BasicConsume(queue: QueueName, autoAck: false, consumer: consumer);
        }

        public void Dispose()
        {
            _channel.Close();
            _connection.Close();
        }
    }
}
