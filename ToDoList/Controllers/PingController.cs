using Microsoft.AspNetCore.Mvc;
using ToDoList.Model;
using ToDoList.Services;

namespace ToDoList.Controllers
{
    [ApiController]
    [Route("/api/[controller]/")]
    public class PingController : ControllerBase
    {
        [HttpGet]
        public IActionResult Index()
        {
            return Ok("Pong");
        }
    }
}