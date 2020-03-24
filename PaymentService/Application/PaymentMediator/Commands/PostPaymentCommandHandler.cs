using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Newtonsoft.Json;
using PaymentService.Application.NotificationMediator.Commands;
using PaymentService.Application.PaymentMediator.Request;
using PaymentService.Domain;
using RabbitMQ.Client;

namespace PaymentService.Application.NotificationMediator.Commands
{
    public class PostPaymentCommandHandler : IRequestHandler<PostPaymentCommand, PaymentDTO>
    {
        private readonly PaymentContext _context;
        private static readonly HttpClient client = new HttpClient();

        public PostPaymentCommandHandler(PaymentContext context)
        {
            _context = context;
        }

        public async Task<PaymentDTO> Handle(PostPaymentCommand request, CancellationToken cancellationToken)
        {
            var data = new Payment()
            {
                Payment_type = request.Data.Attributes.Payment_type,
                Gross_amount = request.Data.Attributes.Gross_amount,
                Bank = request.Data.Attributes.Bank,
                Order_id = request.Data.Attributes.Order_id
            };

            _context.Add(data);
            await _context.SaveChangesAsync();

            var payment = _context.payment.First(x => x.Transaction_status == request.Data.Attributes.Transaction_status);
            var target = new TargetCommand() { Id = payment.Id};

            var command = new PostCommand()
            {
                Title = "hello",
                Message = "this is message body",
                Type = "email",
                From = 123456,
                Targets = new List<TargetCommand>() { target }
            };

            var attributes = new Attribute<PostCommand>()
            {
                Attributes = command
            };

            var httpContent = new CommandDTO<PostCommand>()
            {
                Data = attributes
            };

            var jsonObject = JsonConvert.SerializeObject(httpContent);
            //var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");
            //await client.PostAsync("http://localhost:5010/notification", content);

            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var notification = connection.CreateModel())
            {
                notification.ExchangeDeclare("userExchange", "fanout");
                notification.QueueDeclare(
                    queue: "userQueue",
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                notification.QueueBind(
                    queue: "userQueue",
                    exchange: "userExchange",
                    routingKey: string.Empty
                    );

                var body = Encoding.UTF8.GetBytes(jsonObject);

                var properties = notification.CreateBasicProperties();
                properties.Persistent = true;

                notification.BasicPublish(
                    exchange: "",
                    routingKey: "userQueue",
                    basicProperties: null,
                    body: body
                    );

                Console.WriteLine("User data has been forwarded");
            }

            return new PaymentDTO()
            {
                Message = "Successfully Added",
                Success = true
            };
        }
    }
}
