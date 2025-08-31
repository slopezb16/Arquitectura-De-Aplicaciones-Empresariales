// See https://aka.ms/new-console-template for more information
using MassTransit;
using PacaGroup.Ecommerce.Domain.Events;
using System.Text.Json;

namespace PacaGroup.Ecommerce.ConsoleApp.Consumer
{
    public class DiscountCreatedConsumer : IConsumer<DiscountCreatedEvent>
    {
        public async Task Consume(ConsumeContext<DiscountCreatedEvent> context)
        {
            var jsonMessage = JsonSerializer.Serialize(context.Message);
            await Console.Out.WriteLineAsync($"Message from producer : {jsonMessage}");
        }
    }
}
