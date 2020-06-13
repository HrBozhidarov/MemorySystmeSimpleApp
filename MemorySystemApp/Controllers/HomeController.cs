namespace MemorySystemApp.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        public IActionResult GET()
        {
            return Ok("Works");
        }
    }
}
