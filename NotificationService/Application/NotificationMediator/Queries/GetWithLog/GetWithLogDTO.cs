using System.Collections.Generic;
using NotificationService.Application.NotificationMediator.Request;
using NotificationService.Domain;

namespace NotificationService.Application.NotificationMediator.Queries.GetWithLog
{
    public class GetWithLogDTO : BaseDTO
    {
        public List<NotifDTO> Data { get; set; }
    }
}
