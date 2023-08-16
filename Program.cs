using Fuksi.VK.Configurations;
using Fuksi.VK.Services;
using Fuksi.VK.Services.Interfaces;
using VkNet;
using VkNet.Abstractions;
using VkNet.Model;
using Fuksi.Common.Queue.DI;
using Fuksi.VK.Services.Consumers;
using Fuksi.VK.Models;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);
var settings = builder.Configuration.GetSection("Settings").Get<Settings>();
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddHostedService<RabbitHostedService>();
builder.Services.Configure<Settings>(
    builder.Configuration.GetSection("Settings"));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IVkApiService, VkApiService>();
builder.Services.AddTransient<CanWriteToUserConsumer>();
builder.Services.AddRabbitMq(settings.RabbitConnectionString);

builder.Services.AddScoped<IVkApi>(provider =>
{
    var ff = builder.Configuration.Get<Settings>();
    var vkApi = new VkApi();
    vkApi.Authorize(new ApiAuthParams
    {
        AccessToken = settings.AccessToken,
    });

    return vkApi;
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await app.AddCallbackServerIfNecessary(settings);

app.Run();
