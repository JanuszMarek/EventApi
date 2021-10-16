using BusinessModels.Modules.EventModule.Models;
using Entities.Models;
using EventApi.ActionFilters;
using Infrastructure.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
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

        [HttpGet()]
        [SwaggerOperation(nameof(GetListAsync))]
        public async Task<IActionResult> GetListAsync()
        {
            var events = await eventService.GetListAsync();

            return Ok(events);
        }

        [HttpGet("{eventId}")]
        [ServiceFilter(typeof(EntityExistFilter<Event, int>))]
        [SwaggerOperation(nameof(GetDetailsAsync))]
        public async Task<IActionResult> GetDetailsAsync([FromRoute] int eventId)
        {
            var eventModel = await eventService.GetDetailAsync(eventId);

            return Ok(eventModel);
        }

        [HttpPost]
        [SwaggerOperation(nameof(CreateAsync))]
        public async Task<IActionResult> CreateAsync([FromBody] EventCreateModel createModel)
        {
            await eventService.CreateAsync(createModel);

            return Ok();
        }

        [HttpPut("{eventId}")]
        [ServiceFilter(typeof(EntityExistFilter<Event, int>))]
        [SwaggerOperation(nameof(EditAsync))]
        public async Task<IActionResult> EditAsync([FromRoute] int eventId, [FromBody] EventEditModel editModel)
        {
            await eventService.EditAsync(eventId, editModel);

            return Ok();
        }

        [HttpDelete("{eventId}")]
        [ServiceFilter(typeof(EntityExistFilter<Event, int>))]
        [SwaggerOperation(nameof(DeleteAsync))]
        public async Task<IActionResult> DeleteAsync([FromRoute] int eventId)
        {
            await eventService.DeleteAsync(eventId);

            return Ok();
        }
    }
}
