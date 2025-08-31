// See https://aka.ms/new-console-template for more information
using PacaGroup.Ecommerce.Domain.Enums;

namespace PacaGroup.Ecommerce.Domain.Events
{
    public class DiscountCreatedEvent
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Percent { get; set; }
        public DiscountStatus Status { get; set; }
    }
}
