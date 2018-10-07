using Microsoft.AspNetCore.Mvc;

namespace ServerSentEvents.Controllers
{
    public class EventsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
