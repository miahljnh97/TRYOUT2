using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PaymentService.Application.PaymentMediator.Request;
using PaymentService.Domain;

namespace PaymentService.Application.NotificationMediator.Commands
{
    public class DeletePaymentCommandHandler : IRequestHandler<DeletePaymentCommand, PaymentDTO>
    {
        private readonly PaymentContext _context;
        public DeletePaymentCommandHandler(PaymentContext context)
        {
            _context = context;
        }

        public async Task<PaymentDTO> Handle(DeletePaymentCommand request, CancellationToken cancellationToken)
        {
            var data = await _context.payment.FindAsync(request.Id);

            if (data == null)
            {
                return null;
            }
            else
            {
                _context.payment.Remove(data);
                await _context.SaveChangesAsync();

                return new PaymentDTO { Message = "Successfully deleted data", Success = true };
            }
           
        }
    }
}
