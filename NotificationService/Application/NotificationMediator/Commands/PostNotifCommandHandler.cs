﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Newtonsoft.Json;
using NotificationService.Domain;

namespace NotificationService.Application.NotificationMediator.Commands
{
    public class PostNotifCommandHandler : IRequestHandler<PostNotifCommand, CommandsDTO>
    {
        private readonly NotifContext _context;

        public PostNotifCommandHandler(NotifContext context)
        {
            _context = context;
        }

        public async Task<CommandsDTO> Handle(PostNotifCommand request, CancellationToken cancellationToken)
        {
            var notifList = _context.notification.ToList();

            var notifData = new Notification()
            {
                Title = request.Data.Attributes.Title,
                Message = request.Data.Attributes.Message
            };

            if(!notifList.Any(x => x.Title == request.Data.Attributes.Title))
            {
                _context.notification.Add(notifData);
            }
            await _context.SaveChangesAsync();

            
            var notif = _context.notification.First(x => x.Title == request.Data.Attributes.Title);
            foreach(var k in request.Data.Attributes.Targets)
            {
                _context.notificationLogs.Add(new NotificationLogs
                {
                    Notification_id = notif.Id,
                    Type = request.Data.Attributes.Type,
                    From = request.Data.Attributes.From,
                    Target = k.Id,
                    Email_destination = k.Email_destination
                });

                await _context.SaveChangesAsync();
                await SendMail("kcg210@outlook.com", "kcg211@outlook.com", "ini subject", "dicoba lagi yaa");
            }
            return new CommandsDTO()
            {
                Message = "Successfully Added",
                Success = true
            };
        }

        public async Task<List<userModel>> GetUserData()
        {
            var client = new HttpClient();
            var data = await client.GetStringAsync("http://localhost:5012/payment");
            return JsonConvert.DeserializeObject<List<userModel>>(data);
        }

        public async Task SendMail(string emailfrom, string emailto, string subject, string body)
        {
            var client = new SmtpClient("smtp.mailtrap.io", 2525)
            {
                Credentials = new NetworkCredential("c58cd8a2750dab", "8df199c377a4d5"),
                EnableSsl = true
            };
            await client.SendMailAsync(emailfrom, emailto, subject, body);
            Console.WriteLine("Email has been sent");
        }
    }
}
