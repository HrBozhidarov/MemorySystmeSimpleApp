namespace MemorySystemApp.Controllers
{
    using MemorySystemApp.Models.pictures;
    using MemorySystemApp.Infrastructures;

    using Microsoft.AspNetCore.Mvc;
    using MemorySystemApp.Services.Identity;
    using Microsoft.AspNetCore.Authorization;

    [Authorize]
    public class PicturesController : ApiController
    {
        private readonly IPicturesService picturesService;
        public PicturesController(IPicturesService picturesService)
        {
            this.picturesService = picturesService;
        }

        [HttpPost]
        [Route(nameof(Create))]
        [Authorize]
        public ActionResult Create(PictureRequestModel model)
        {
            var userId = this.User.GetUserId();

            var isCreated = this.picturesService.Create(model, userId);
            if (!isCreated)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpGet]
        [Route(nameof(Details))]
        [Authorize]
        public ActionResult Details(int id)
        {

            return null;
        }
    }
}
