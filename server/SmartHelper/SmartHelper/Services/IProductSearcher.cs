using SmartHelper.Models;

namespace SmartHelper.Services
{
    public interface IProductSearcher
    {
        Task<IEnumerable<Product>> SearchAsync(IFormFile file);
    }
}
