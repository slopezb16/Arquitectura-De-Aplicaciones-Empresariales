using MediatR;
using PacaGroup.Ecommerce.Application.DTO;
using PacaGroup.Ecommerce.Transversal.Common;

namespace PacaGroup.Ecommerce.Application.UseCases.Customers.Queries.GetCustomerQuery
{
    public sealed record GetCustomerQuery : IRequest<Response<CustomerDto>>
    {
        public string CustomerId { get; set; }
    }
}
