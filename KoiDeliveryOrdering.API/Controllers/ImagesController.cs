using KoiDeliveryOrdering.API.Payloads;
using KoiDeliveryOrdering.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace KoiDeliveryOrdering.API.Controllers
{
    [ApiController]
    public class ImagesController : Controller
    {
        private readonly IImageService _imageService;

        public ImagesController(IImageService imageService)
        {
            _imageService = imageService;
        }

        [HttpPost(ApiRoute.Image.Upload)]
        public async Task<IActionResult> UploadAsync(IFormFile file)
        {
            var imageUrl = await _imageService.UploadAsync(file);
            if (imageUrl == null)
            {
                return Problem("Something went wrong!", null, (int)HttpStatusCode.InternalServerError);
            }

            return Json(new { link = imageUrl });
        }
    }
}