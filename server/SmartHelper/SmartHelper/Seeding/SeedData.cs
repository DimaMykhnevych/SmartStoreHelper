using Microsoft.EntityFrameworkCore;
using SmartHelper.Context;
using SmartHelper.Models;

namespace SmartHelper.Seeding
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new SmartHelperDbContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<SmartHelperDbContext>>());

            if (context.Products.Any())
            {
                return;   // DB has been seeded
            }

            context.Products.AddRange(
                new Product
                {
                    Title = "apple bread",
                    Description = "Apple bread yummy",
                    ImageUri = new Uri("http://test"),
                    Price = 7.99M
                },
                new Product
                {
                    Title = "pineapple",
                    Description = "pineapple yummy",
                    ImageUri = new Uri("http://test-pineapple"),
                    Price = 12.99M
                },
                new Product
                {
                    Title = "eggs",
                    Description = "eggs yummy",
                    ImageUri = new Uri("http://test-eggs"),
                    Price = 2.99M
                }
            );
            context.SaveChanges();
        }
    }

}
