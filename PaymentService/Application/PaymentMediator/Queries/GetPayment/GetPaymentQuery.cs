using System;
using MediatR;

namespace PaymentService.Application.UserMediator.Queries.GetUser
{
    public class GetPaymentQuery : IRequest<GetPaymentDTO>
    {
        public int Id { get; set; }
        public GetPaymentQuery(int id)
        {
            Id = id;
        }
    }
}
