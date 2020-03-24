using PaymentService.Domain;

namespace PaymentService.Application.UserMediator.Queries.GetUser
{
    public class GetPaymentDTO : BaseDTO
    {
        public Payment Data { get; set; } 
    }
}
