using Microsoft.EntityFrameworkCore;
using SmartHelper.Context;
using SmartHelper.Helpers.EntitiesRecognition;
using SmartHelper.Helpers.SpeechRecognition;
using SmartHelper.Options;
using SmartHelper.Seeding;
using SmartHelper.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    //builder.WithOrigins("http://localhost:4200")
    builder.AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()
    );
});

builder.Services.Configure<SpeechRecognitionApiOptions>(builder.Configuration.GetSection(nameof(SpeechRecognitionApiOptions)));
builder.Services.Configure<TextAnalyticsApiClientOptions>(builder.Configuration.GetSection(nameof(TextAnalyticsApiClientOptions)));

builder.Services.AddSingleton<ISpeechRecognition, SpeechRecognition>();
builder.Services.AddSingleton<IEntitiesRecognition, EntitiesRecognition>();
builder.Services.AddTransient<IProductSearcher, ProductSearcher>();

string connectionString = builder.Configuration["ConnectionStrings:SmartHelperDb"];
builder.Services.AddDbContext<SmartHelperDbContext>(opt =>
        opt.UseSqlServer(connectionString));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    SeedData.Initialize(services);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.UseCors("CorsPolicy");

app.Run();
