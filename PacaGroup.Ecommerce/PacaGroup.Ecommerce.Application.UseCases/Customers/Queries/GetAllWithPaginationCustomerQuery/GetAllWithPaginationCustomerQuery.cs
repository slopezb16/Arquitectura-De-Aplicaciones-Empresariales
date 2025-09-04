using MediatR;
using PacaGroup.Ecommerce.Application.DTO;
using PacaGroup.Ecommerce.Transversal.Common;

namespace PacaGroup.Ecommerce.Application.UseCases.Customers.Queries.GetAllWithPaginationCustomerQuery
{
    public sealed record GetAllWithPaginationCustomerQuery : IRequest<ResponsePagination<IEnumerable<CustomerDto>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
