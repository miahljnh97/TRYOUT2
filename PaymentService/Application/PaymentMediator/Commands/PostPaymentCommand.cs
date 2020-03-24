using System.Collections.Generic;
using MediatR;
using PaymentService.Application.PaymentMediator.Request;
using PaymentService.Domain;

namespace PaymentService.Application.NotificationMediator.Commands
{
    public class PostPaymentCommand : CommandDTO<Payment>, IRequest<PaymentDTO>
    {
    }

    public class PostCommand
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public string Type { get; set; }
        public int From { get; set; }
        public List<TargetCommand> Targets { get; set; }
    }

    public class TargetCommand
    {
        public int Id { get; set; }
        public string Email_destination { get; set; }
    }

}
