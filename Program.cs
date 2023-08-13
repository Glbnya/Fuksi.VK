using Fuksi.VK.Configurations;
using Fuksi.VK.Services;
using Fuksi.VK.Services.Interfaces;
using VkNet;
using VkNet.Abstractions;
using VkNet.Model;
using Fuksi.Common.Queue.DI;
using Fuksi.VK.Services.Consumers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IVkApiService, VkApiService>();
builder.Services.AddTransient<CanWriteToUserConsumer>();
builder.Services.AddRabbitMq("host=127.0.0.1:5672;publisherConfirms=true;username=guest;password=guest;requestedHeartbeat=3600");
builder.Services.AddScoped<IVkApi>(provider =>
{
    var vkApi = new VkApi();

    vkApi.Authorize(new ApiAuthParams
    {
        AccessToken = builder.Configuration["Config:AccessToken"]
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

await app.AddCallbackServerIfNecessary();

app.Run();
