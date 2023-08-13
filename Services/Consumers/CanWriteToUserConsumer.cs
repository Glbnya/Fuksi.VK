using Fuksi.Common.Queue.Consumer;
using Fuksi.VK.Services.Interfaces;
using Fuksi.VkContracts;

namespace Fuksi.VK.Services.Consumers
{
    public class CanWriteToUserConsumer : IConsumer<TryWriteToUserMessage>
    {
        private readonly IVkApiService _vkApiService;
        public CanWriteToUserConsumer(IVkApiService vkApiService)
        {
            _vkApiService = vkApiService;
        }
        public async Task Consume(TryWriteToUserMessage message)
        {
            var userId = message.UserId;

            var messageContent = message.MessageContent;

            await _vkApiService.TryMessageUserAsync(userId, messageContent);

        }
    }
}
