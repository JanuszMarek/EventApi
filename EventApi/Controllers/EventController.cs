using BusinessModels.Modules.EventModule.Models;
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

        [HttpGet("{id}")]
        [SwaggerOperation(nameof(GetDetailsAsync))]
        public async Task<IActionResult> GetDetailsAsync([FromRoute] int id)
        {
            var eventModel = await eventService.GetDetailAsync(id);

            return Ok(eventModel);
        }

        [HttpPost]
        [SwaggerOperation(nameof(CreateAsync))]
        public async Task<IActionResult> CreateAsync([FromBody] EventCreateModel createModel)
        {
            await eventService.CreateAsync(createModel);

            return Ok();
        }

        [HttpPut("{id}")]
        [SwaggerOperation(nameof(EditAsync))]
        public async Task<IActionResult> EditAsync([FromRoute] int id, [FromBody] EventEditModel editModel)
        {
            await eventService.EditAsync(id, editModel);

            return Ok();
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(nameof(DeleteAsync))]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            await eventService.DeleteAsync(id);

            return Ok();
        }
    }
}
