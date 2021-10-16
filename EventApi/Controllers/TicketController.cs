using BusinessModels.Modules.EventTicketModule.Models;
using Infrastructure.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace EventApi.Controllers
{
    [ApiController]
    [Route("api/Event/{eventId}/[controller]")]
    public class TicketController : Controller
    {
        private readonly IEventTicketService eventTicketService;

        public TicketController(IEventTicketService eventTicketService)
        {
            this.eventTicketService = eventTicketService;
        }

        [HttpGet()]
        [SwaggerOperation(nameof(GetListForEventAsync))]
        public async Task<IActionResult> GetListForEventAsync([FromRoute] int eventId)
        {
            var tickets = await eventTicketService.GetListForEventAsync(eventId);

            return Ok(tickets);
        }

        [HttpPost()]
        [SwaggerOperation(nameof(BuyAsync))]
        public async Task<IActionResult> BuyAsync(
            [FromRoute] int eventId,
            [FromBody] EventTicketCreateModel eventTicketCreateModel)
        {
            var result = await eventTicketService.BuyTicketAsync(eventId, eventTicketCreateModel);

            return Ok(result);
        }

        [HttpDelete("{ticketId}")]
        [SwaggerOperation(nameof(ReturnTicketAsync))]
        public async Task<IActionResult> ReturnTicketAsync([FromRoute] int ticketId)
        {
            await eventTicketService.ReturnTicketAsync(ticketId);

            return Ok();
        }
    }
}
