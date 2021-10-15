using BusinessModels.Modules.EventModule.Models;
using Infrastructure.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EventApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : Controller
    {
        private readonly IEventService eventService;

        public EventController(IEventService eventService)
        {
            this.eventService = eventService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] EventCreateModel createModel)
        {
            await eventService.CreateAsync(createModel);

            return Ok();
        }
    }
}
