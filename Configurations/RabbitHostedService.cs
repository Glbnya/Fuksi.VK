using Fuksi.Common.Queue.Abstractions;
using Fuksi.Common.Queue;
using Fuksi.VkContracts;
using Fuksi.VK.Services.Consumers;

namespace Fuksi.VK.Configurations
{
    public class RabbitHostedService : IHostedService
    {
        private readonly IList<IDisposable> _consumers = new List<IDisposable>();
        private readonly IEventQueue _eventQueue;

        public RabbitHostedService(IEventQueue eventQueue)
        {
            _eventQueue = eventQueue;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var subscription = await _eventQueue.Subscribe<TryWriteToUserMessage, CanWriteToUserConsumer>(
                    "CanWriteToUser.Consumer",
                    new SubscriptionInfo())
                ;

            _consumers.Add(subscription);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            foreach (var consumer in _consumers)
                consumer?.Dispose();


            return Task.CompletedTask;
        }
    }
}
