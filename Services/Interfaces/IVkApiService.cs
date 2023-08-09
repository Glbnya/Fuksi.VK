using Fuksi.VK.Models;
using VkNet.Model;

namespace Fuksi.VK.Services.Interfaces
{
    public interface IVkApiService
    {
        Task<bool> CanWriteToUser(ulong userId, ulong groupId);
        Task UserFuksiMembership(Update update);
    }
}
