using Fuksi.VK.Models;
using Fuksi.VK.Services.Interfaces;
using VkNet;
using VkNet.Abstractions;
using VkNet.Model;
using Fuksi.Common.Queue;
using Fuksi.Common.Queue.Abstractions;
using Fuksi.VK.Models.Membership;

namespace Fuksi.VK.Services
{
    public class VkApiService : IVkApiService
    {
        private readonly IVkApi _vkApi;
        private readonly IEventQueue _eventQueue;
        public VkApiService(IConfiguration configuration, IVkApi vkApi, IEventQueue eventQueue)
        {
            _vkApi = vkApi;
            _eventQueue = eventQueue;
        }
        public async Task<bool> CanWriteToUser(ulong userId, ulong groupId)
        {
            return await _vkApi.Messages.IsMessagesFromGroupAllowedAsync(userId, groupId);
        }

        public async Task UserFuksiMembership(Update update)
        {
            await _eventQueue.Publish(new MembershipStatus
            {
                VkUserId = update.Object.UserId,

                Status = update.Type
            }, 
            
            new PublishMessageInfo());

            return Task.CompletedTask;
        }
    }
}
