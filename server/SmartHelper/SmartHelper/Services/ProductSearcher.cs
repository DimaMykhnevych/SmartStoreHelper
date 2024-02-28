using Azure.AI.TextAnalytics;
using Microsoft.EntityFrameworkCore;
using SmartHelper.Context;
using SmartHelper.Helpers.EntitiesRecognition;
using SmartHelper.Helpers.SpeechRecognition;
using SmartHelper.Models;
using System.Reflection;

namespace SmartHelper.Services
{
    public class ProductSearcher : IProductSearcher
    {
        private readonly ISpeechRecognition _speechRecognition;
        private readonly IEntitiesRecognition _entitiesRecognition;
        private readonly SmartHelperDbContext _dbContext;

        public ProductSearcher(
            ISpeechRecognition speechRecognition,
            IEntitiesRecognition entities,
            SmartHelperDbContext dbContext)
        {
            _speechRecognition = speechRecognition;
            _entitiesRecognition = entities;
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Product>> SearchAsync(IFormFile file)
        {
            Guid guid = Guid.NewGuid();
            var runDir = Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location);
            var direcotryToSave = Path.Combine(runDir, "requests");
            if (!Directory.Exists(direcotryToSave))
            {
                Directory.CreateDirectory(direcotryToSave);
            }

            var pathToSave = Path.Combine(direcotryToSave, $"{Path.GetFileNameWithoutExtension(file.FileName)}-{guid}{Path.GetExtension(file.FileName)}");
            using Stream fileStream = new FileStream(pathToSave, FileMode.Create);
            await file.CopyToAsync(fileStream);

            var speech = await _speechRecognition.RecognizedSpeechAsync(pathToSave);
            var entities = await _entitiesRecognition.RecognizedEntitiesAsync(speech);

            var productNames = entities
                .Where(e => e.Category == EntityCategory.Product)
                .Select(e => e.Text)
                .ToList();

            var products = await _dbContext.Products.ToListAsync();
            return products.Where(p => productNames.Contains(p.Title, StringComparer.OrdinalIgnoreCase));
        }
    }
}
