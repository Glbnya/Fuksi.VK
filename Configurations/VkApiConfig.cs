using Fuksi.VK.Models;
using Microsoft.Extensions.Options;
using VkNet.Abstractions;
using VkNet.Model;

namespace Fuksi.VK.Configurations
{
    public static class VkApiConfig
    {
        public async static Task AddCallbackServerIfNecessary(this IApplicationBuilder builder, Settings settings)
        {

            var scope = builder.ApplicationServices.CreateScope();

            var vkApi = scope.ServiceProvider.GetService<IVkApi>();

            var connectedServers = await vkApi.Groups.GetCallbackServersAsync(221861505); // падает ошибка, если сервер в неподтвержденном состоянии

            if (connectedServers.Count == 0)
            {
                var server = await vkApi.Groups.AddCallbackServerAsync(settings.GroupId, settings.CallbackUrl, "fuksibot", "");

                await vkApi.Groups.SetCallbackSettingsAsync(
                    new CallbackServerParams
                    {
                        GroupId = 221861505,
                        ServerId = server,
                        CallbackSettings = new CallbackSettings
                        {
                            MessageAllow = true,
                            MessageDeny = true,
                            GroupJoin = true,
                            GroupLeave = true,
                        }
                    });
            }
        }
    }
}
