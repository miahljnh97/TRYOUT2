using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentService.Domain;

namespace PaymentService.Application.NotificationMediator.Queries.GetNotifs
{
    public class GetPaymentsQueryHandler : IRequestHandler<GetPaymentsQuery, GetPaymentsDTO>
    {
        private readonly PaymentContext _context;
        public GetPaymentsQueryHandler(PaymentContext context)
        {
            _context = context;
        }

        public async Task<GetPaymentsDTO> Handle(GetPaymentsQuery request, CancellationToken cancellationToken)
        {
            var data = await _context.payment.ToListAsync();

            return new GetPaymentsDTO
            {
                Success = true,
                Message = "Success retreiving data",
                Data = data
            };
        }
    }
}
