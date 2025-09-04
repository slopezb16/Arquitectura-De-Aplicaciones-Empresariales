using MassTransit;
using PacaGroup.Ecommerce.Application.Interface.Infrastructure;

namespace PacaGroup.Ecommerce.Infrastructure.EventBus
{
    public class EventBusRabbitMQ : IEventBus
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public EventBusRabbitMQ(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public async void Publish<T>(T @event)
        {
            await _publishEndpoint.Publish(@event);
        }
    }
}
