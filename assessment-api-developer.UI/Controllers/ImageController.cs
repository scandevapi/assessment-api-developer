//using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace assessment_api_developer.UI.Controllers
{
    //[Authorize]
    //[Authorize(Policy = "Admin")]
    public class ImageController : Controller
    {
        private readonly IWebHostEnvironment _env;

        public ImageController(IWebHostEnvironment env)
        {
            _env = env;
        }

        [HttpGet("images/{filename}")]
        public IActionResult GetImage(string filename)
        {
            var path = Path.Combine(_env.ContentRootPath, "DownloadCenter", "Images", filename);
            if (!System.IO.File.Exists(path))
            {
                return NotFound();
            }

            var image = System.IO.File.OpenRead(path);
            return File(image, "image/jpeg");
        }
    }
}
