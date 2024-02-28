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
                    Title = "Apple",
                    Description = "Green apples are an excellent choice for a crisp, delicious snack or a versatile ingredient for your favorite recipes.",
                    ImageUri = new Uri("https://hips.hearstapps.com/hmg-prod/images/close-up-of-green-apple-against-white-background-royalty-free-image-1659450368.jpg"),
                    Price = 7.99M
                },
                new Product
                {
                    Title = "Pineapple",
                    Description = "Herbaceous, biennial, tropical plant that grows up to 1.0–1.5m high and produces a fleshy, edible fruit whose flesh ranges from nearly white to yellow.",
                    ImageUri = new Uri("https://www.collinsdictionary.com/images/full/pineapple_124985672.jpg"),
                    Price = 12.99M
                },
                new Product
                {
                    Title = "Eggs",
                    Description = "Chicken eggs",
                    ImageUri = new Uri("https://khpet.com/cdn/shop/articles/how-often-do-chickens-lay-eggs_800x800.jpg?v=1593020038"),
                    Price = 2.99M
                },
                new Product
                {
                    Title = "Milk",
                    Description = "Gallon of whole milk",
                    ImageUri = new Uri("https://cdn.store-factory.com/www.culinaide.com/content/product_4801045b.jpg?v=1597660182"),
                    Price = 4.49M
                },
                new Product
                {
                    Title = "Chocolate",
                    Description = "Dark chocolate bar",
                    ImageUri = new Uri("https://cdn11.bigcommerce.com/s-qbjojecpaq/images/stencil/1280x1280/products/442/1968/Untitled_design_-_2023-06-12T155310.493__67118.1686599665.png?c=1"),
                    Price = 1.99M
                },
                new Product
                {
                    Title = "Carrot",
                    Description = "Fresh carrots bundle",
                    ImageUri = new Uri("https://theseedcompany.ca/cdn/shop/files/cropCARR1923Carrot_SweetnessPelletedLong.png?v=1701543664"),
                    Price = 2.99M
                },
                new Product
                {
                    Title = "Coffee",
                    Description = "Bag of whole bean coffee",
                    ImageUri = new Uri("https://upload.wikimedia.org/wikipedia/commons/thumb/c/c5/Roasted_coffee_beans.jpg/640px-Roasted_coffee_beans.jpg"),
                    Price = 12.99M
                },
                new Product
                {
                    Title = "Pasta",
                    Description = "Box of spaghetti",
                    ImageUri = new Uri("https://www.allrecipes.com/thmb/5SdUVhHTMs-rta5sOblJESXThEE=/1500x0/filters:no_upscale():max_bytes(150000):strip_icc()/11691-tomato-and-garlic-pasta-ddmfs-3x4-1-bf607984a23541f4ad936b33b22c9074.jpg"),
                    Price = 1.49M
                },
                new Product
                {
                    Title = "Oatmeal",
                    Description = "Instant oatmeal packets",
                    ImageUri = new Uri("https://www.eatingwell.com/thmb/-UULlbERQCfJRQTnb5bwjoo9-UQ=/1500x0/filters:no_upscale():max_bytes(150000):strip_icc()/old-fashioned-oatmeal-hero-05-060861b81cb641cea272e068aba093fd.jpg"),
                    Price = 3.99M
                },
                new Product
                {
                    Title = "Oranges",
                    Description = "Navel oranges",
                    ImageUri = new Uri("https://media.istockphoto.com/id/185284489/photo/orange.jpg?s=612x612&w=0&k=20&c=m4EXknC74i2aYWCbjxbzZ6EtRaJkdSJNtekh4m1PspE="),
                    Price = 4.99M
                }
            );
            context.SaveChanges();
        }
    }

}
