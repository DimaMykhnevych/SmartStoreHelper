using Microsoft.AspNetCore.Mvc;
using SmartHelper.Services;

namespace SmartHelper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductSearcher _productSearcher;

        public ProductController(IProductSearcher productSearcher)
        {
            _productSearcher = productSearcher;
        }

        [HttpPost]
        [Route("recognize-and-search")]
        public async Task<IActionResult> UploadCargoPhoto(IFormFile file)
        {
            var result = await _productSearcher.SearchAsync(file);
            return Ok(result);
        }
    }
}
