using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NotificationService.Domain;

namespace NotificationService.Application.NotificationMediator.Commands
{
    public class DeleteNotifCommandHandler : IRequestHandler<DeleteNotifCommand, CommandsDTO>
    {
        private readonly NotifContext _context;
        public DeleteNotifCommandHandler(NotifContext context)
        {
            _context = context;
        }

        public async Task<CommandsDTO> Handle(DeleteNotifCommand request, CancellationToken cancellationToken)
        {
            var data = await _context.notification.FindAsync(request.Id);

            if (data == null)
            {
                return null;
            }

            _context.notification.Remove(data);
            await _context.SaveChangesAsync();

            return new CommandsDTO { Message = "Successfull", Success = true };
        }
    }
}
