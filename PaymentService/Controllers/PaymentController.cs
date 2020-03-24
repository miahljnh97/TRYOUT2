using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PaymentService.Application.NotificationMediator.Commands;
using PaymentService.Application.NotificationMediator.Queries.GetNotifs;
using PaymentService.Application.UserMediator.Queries.GetUser;

namespace UserService.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class PaymentController : ControllerBase
    {
        private IMediator _mediatr;
        public PaymentController(IMediator mediator)
        {
            _mediatr = mediator;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var notif = new GetPaymentsQuery();

            return Ok(await _mediatr.Send(notif));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var notif = new GetPaymentQuery(id);

            return Ok(await _mediatr.Send(notif));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            var notif = new DeletePaymentCommand(id);
            var result = await _mediatr.Send(notif);
            return result != null ? (IActionResult)Ok(new { Message = "success" }) : NotFound(new { Message = "Payment not found" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, PutPaymentCommand data)
        {
            data.Data.Attributes.Id = id;
            var result = await _mediatr.Send(data);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(PostPaymentCommand data)
        {
            var result = await _mediatr.Send(data);
            return Ok(result);
        }
    }
}
