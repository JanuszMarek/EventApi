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

        [HttpPost("{id}")]
        public async Task<IActionResult> GetDetailsAsync([FromRoute] int id)
        {
            var eventModel = await eventService.GetDetailsAsync(id);

            return Ok(eventModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] EventCreateModel createModel)
        {
            await eventService.CreateAsync(createModel);

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditAsync([FromRoute] int id, [FromBody] EventEditModel editModel)
        {
            await eventService.EditAsync(id, editModel);

            return Ok();
        }
    }
}
