﻿using TaskWorker.Handlers.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace TaskWorker.Receivers
{
    public class TaskReceiver : IDisposable
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly IMessageHandler _messageHandler;
        private const string QueueName = "long-task-queue";

        public TaskReceiver(IMessageHandler messageHandler)
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

        void IDisposable.Dispose()
        {
            _channel.Close();
            _connection.Close();
        }
    }
}
