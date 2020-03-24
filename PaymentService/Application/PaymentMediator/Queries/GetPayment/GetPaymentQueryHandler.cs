using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PaymentService.Domain;

namespace PaymentService.Application.UserMediator.Queries.GetUser
{
    public class GetPaymentQueryHandler : IRequestHandler<GetPaymentQuery, GetPaymentDTO>
    {
        private readonly PaymentContext _context;
        public GetPaymentQueryHandler(PaymentContext context)
        {
            _context = context;
        }

        public async Task<GetPaymentDTO> Handle(GetPaymentQuery request, CancellationToken cancellationToken)
        {
            var data = await _context.payment.FindAsync(request.Id);

            if (data == null)
            {
                return null;
            }
            else
            {
                return new GetPaymentDTO
                {
                    Success = true,
                    Message = "Success retreiving data",
                    Data = data
                };
            }
        }
    }
}
