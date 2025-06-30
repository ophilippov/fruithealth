using FruitHealth;
using FruitHealth.Api;
using FruitHealth.Api.Middlewares;
using FruitHealth.FruityVice.Client;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddFruitHealthApplicationServices();
builder.Services.AddFruityViceClient();

builder.Services.AddControllers();

builder.Services.AddOpenApi();

builder.Services.AddGlobalExceptionHandling();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
