using MediatR;
using PaymentService.Application.PaymentMediator.Request;

namespace PaymentService.Application.NotificationMediator.Commands
{
    public class DeletePaymentCommand : IRequest<PaymentDTO>
    {
        public int Id { get; set; }
        public DeletePaymentCommand(int id)
        {
            Id = id;
        }
    }
}
