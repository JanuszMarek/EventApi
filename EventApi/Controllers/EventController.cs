using Microsoft.AspNetCore.Mvc;

namespace EventApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return Ok();
        }
    }
}
