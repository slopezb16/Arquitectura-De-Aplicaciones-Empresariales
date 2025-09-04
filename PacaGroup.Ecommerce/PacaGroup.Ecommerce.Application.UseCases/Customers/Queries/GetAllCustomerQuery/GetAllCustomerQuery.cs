using MediatR;
using PacaGroup.Ecommerce.Application.DTO;
using PacaGroup.Ecommerce.Transversal.Common;

namespace PacaGroup.Ecommerce.Application.UseCases.Customers.Queries.GetAllCustomerQuery
{
    public sealed record GetAllCustomerQuery : IRequest<Response<IEnumerable<CustomerDto>>>
    {
    }
}
