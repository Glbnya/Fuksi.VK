using Microsoft.Extensions.Options;
using VkNet;
using VkNet.Abstractions;
using VkNet.Model;

namespace Fuksi.VK.Configurations
{
    public static class VkApiConfig
    {
        public async static Task<IServiceCollection> AddCallbackServerIfNecessary(this IServiceCollection services)
        {
            using var serviceProvider = services.BuildServiceProvider();

            var vkApi = serviceProvider.GetRequiredService<IVkApi>();

            var connectedServers = await vkApi.Groups.GetCallbackServersAsync(221861505); // падает ошибка, если сервер в неподтвержденном состоянии

            if (connectedServers.Count == 0)
                await vkApi.Groups.AddCallbackServerAsync(221861505, "https://e02d-46-0-84-44.ngrok-free.app/Callback", "fuksibot", "");

            return services;
        }
    }
}
