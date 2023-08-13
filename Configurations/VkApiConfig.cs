using VkNet.Abstractions;
using VkNet.Model;

namespace Fuksi.VK.Configurations
{
    public static class VkApiConfig
    {
        public async static Task AddCallbackServerIfNecessary(this IApplicationBuilder builder)
        {
            var scope = builder.ApplicationServices.CreateScope();

            var vkApi = scope.ServiceProvider.GetService<IVkApi>();

            var connectedServers = await vkApi.Groups.GetCallbackServersAsync(221861505); // падает ошибка, если сервер в неподтвержденном состоянии

            if (connectedServers.Count == 0)
            {
                var server = await vkApi.Groups.AddCallbackServerAsync(221861505, "https://2e8b-46-0-52-29.ngrok-free.app/Callback", "fuksibot", "");

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
