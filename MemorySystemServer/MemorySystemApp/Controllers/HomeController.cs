namespace MemorySystemApp.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : ApiController
    {
        public IActionResult GET()
        {
            return Ok();
        }
    }
}
