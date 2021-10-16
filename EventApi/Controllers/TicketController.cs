using BusinessModels.Modules.EventTicketModule.Models;
using Entities.Models;
using EventApi.ActionFilters;
using EventApi.Constants;
using Infrastructure.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace EventApi.Controllers
{
    [ApiController]
    [Route("api/Event/{eventId}/[controller]")]
    [ServiceFilter(typeof(EntityExistFilter<Event, int>))]
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

            return Ok(result ? ResponseMessages.TICKET_BUY_SUCCESS : ResponseMessages.TICKET_BUY_FAILURE);
        }

        [HttpDelete("{eventTicketId}")]
        [ServiceFilter(typeof(EntityExistFilter<EventTicket, long>))]
        [SwaggerOperation(nameof(ReturnTicketAsync))]
        public async Task<IActionResult> ReturnTicketAsync([FromRoute] int eventTicketId)
        {
            await eventTicketService.ReturnTicketAsync(eventTicketId);

            return Ok();
        }
    }
}
