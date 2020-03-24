using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PaymentService.Application.PaymentMediator.Request;
using PaymentService.Domain;

namespace PaymentService.Application.NotificationMediator.Commands
{
    public class PutPaymentCommandHandler : IRequestHandler<PutPaymentCommand, PaymentDTO>
    {
        private readonly PaymentContext _context;

        public PutPaymentCommandHandler(PaymentContext context)
        {
            _context = context;
        }

        public async Task<PaymentDTO> Handle(PutPaymentCommand request, CancellationToken cancellationToken)
        {
            var data = await _context.payment.FindAsync(request.Data.Attributes.Id);

            data.Order_id = request.Data.Attributes.Order_id;
            data.Transaction_id = request.Data.Attributes.Transaction_id;
            data.Payment_type = request.Data.Attributes.Payment_type;
            data.Gross_amount = request.Data.Attributes.Gross_amount;
            data.Bank = request.Data.Attributes.Bank;
            data.Transaction_time = request.Data.Attributes.Transaction_time;
            data.Transaction_status = request.Data.Attributes.Transaction_status;

            _context.SaveChanges();


            return new PaymentDTO
            {
                Message = "Success retreiving data",
                Success = true
            };
        }
    }
}
