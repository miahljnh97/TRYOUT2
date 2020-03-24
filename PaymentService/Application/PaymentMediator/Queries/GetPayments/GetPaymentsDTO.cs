using System;
using System.Collections.Generic;
using PaymentService.Domain;

namespace PaymentService.Application.NotificationMediator.Queries.GetNotifs
{
    public class GetPaymentsDTO : BaseDTO
    {
        public List<Payment> Data { get; set; }
    }
}
