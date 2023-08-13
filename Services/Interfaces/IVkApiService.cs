using Fuksi.VK.Models.VkUpdate;

namespace Fuksi.VK.Services.Interfaces
{
    public interface IVkApiService
    {
        Task TryMessageUserAsync(long userId, string message);
        Task<bool> CanWriteToUser(ulong userId, ulong groupId);
        Task HandleUserAction(Update update);
    }
}
