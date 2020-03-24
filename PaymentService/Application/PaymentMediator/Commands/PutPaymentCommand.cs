using MediatR;
using PaymentService.Application.PaymentMediator.Request;
using PaymentService.Domain;

namespace PaymentService.Application.NotificationMediator.Commands
{
    public class PutPaymentCommand : CommandDTO<Payment>, IRequest<PaymentDTO>
    {
    }
}
