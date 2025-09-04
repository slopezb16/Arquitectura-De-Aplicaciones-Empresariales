using MediatR;
using PacaGroup.Ecommerce.Transversal.Common;

namespace PacaGroup.Ecommerce.Application.UseCases.Customers.Commands.DeleteCustomerCommand
{
    public sealed record DeleteCustomerCommand : IRequest<Response<bool>>
    {
        public string CustomerId { get; set; }
    }
}
