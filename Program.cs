using brochureapi.EFCoreInMemoryDbDemo;
using brochureapi.Mappings;
using brochureapi.repository; // If you have your repositories in a folder
using brochureapi.services;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
// Add services to the container.
// Register In-Memory DbContext for Brochure
builder.Services.AddDbContext<ApiContext>(options =>
    options.UseInMemoryDatabase("brochureDb"));
builder.Services.AddAutoMapper(typeof(Mapper));
//// Register Brochure Repository
builder.Services.AddScoped<IBrochureService, BrochureService>();
builder.Services.AddScoped<IBrochurePageService, BrochurePageService>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
