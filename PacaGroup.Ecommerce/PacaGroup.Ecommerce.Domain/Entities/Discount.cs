using PacaGroup.Ecommerce.Domain.Common;
using PacaGroup.Ecommerce.Domain.Enums;

namespace PacaGroup.Ecommerce.Domain.Entities
{
    public class Discount : BaseAuditableEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Percent { get; set; }
        public DiscountStatus Status { get; set; }
    }
}
