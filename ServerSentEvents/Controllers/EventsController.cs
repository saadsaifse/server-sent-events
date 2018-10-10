using Microsoft.AspNetCore.Mvc;
using ServerSentEvents.Infrastructure;
using System.Threading.Tasks;

namespace ServerSentEvents.Controllers
{
    public class EventsController : Controller
    {
        readonly ISseService eventService;
        public EventsController(ISseService service)
        {
            eventService = service;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Ping()
        {
            await eventService.SendToAll($"Ping received");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Message([FromBody] string message)
        {
            await eventService.SendToAll($"Message received: '{message}'");
            return View();
        }
    }
}
