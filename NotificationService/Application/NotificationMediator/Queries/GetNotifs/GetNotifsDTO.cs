using System.Collections.Generic;
using NotificationService.Application.NotificationMediator.Request;
using NotificationService.Domain;

namespace NotificationService.Application.NotificationMediator.Queries.GetNotifs
{
    public class GetNotifsDTO : BaseDTO
    {
        public List<NotifData> Data { get; set; }
    }
}
