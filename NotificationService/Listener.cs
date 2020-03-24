using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace NotificationService
{
    public class Listener
    {
        public void Receive()
        {
            var client = new HttpClient();
            var factory = new ConnectionFactory() { HostName = "some-rabbit" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare("userExchange", "fanout");

                var queueName = channel.QueueDeclare(
                    queue: "userQueue",
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                channel.QueueBind(queueName, "userExchange", string.Empty);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += async (model, k) =>
                {
                    var body = k.Body;
                    var message = Encoding.UTF8.GetString(body);
                    var content = new StringContent(message, Encoding.UTF8, "application/json");
                    Console.WriteLine($"Processing data from queue");
                    await client.PostAsync("http://localhost:5010/notification", content);

                };

                channel.BasicConsume(queue: "userQueue",
                                     autoAck: true,
                                     consumer: consumer);
                Console.ReadLine();
                Thread.Sleep(10000);
            }
        }
    }
}