using Fuksi.VK.Services.Interfaces;
using VkNet.Abstractions;
using Fuksi.Common.Queue;
using Fuksi.Common.Queue.Abstractions;
using Fuksi.VkContracts;
using Fuksi.VK.Models.VkUpdate;

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
        public async Task TryMessageUserAsync(long userId, string message)
        {
            if (await CanWriteToUser((ulong)userId, 221861505))
            {
                await _vkApi.Messages.SendAsync(
                new VkNet.Model.MessagesSendParams
                {
                    UserId = userId,
                    GroupId = 221861505,
                    Message = message,
                    RandomId = 123123321  //ругается, пока оставлю так
                }
                );
            }

        }

        public async Task HandleUserAction(Update update)
        {
            await _eventQueue.Publish(new UserAction
            {
                VkUserId = update.Object.UserId,

                Status = update.Type
            },

            new PublishMessageInfo());

        }
        #region Helpers
        public async Task<bool> CanWriteToUser(ulong userId, ulong groupId)
        {
            var result =  await _vkApi.Messages.IsMessagesFromGroupAllowedAsync(userId, groupId);
            return result;
        }
        #endregion
    }
}
