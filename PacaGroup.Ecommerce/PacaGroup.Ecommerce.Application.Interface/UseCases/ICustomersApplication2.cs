using PacaGroup.Ecommerce.Application.DTO;
using PacaGroup.Ecommerce.Transversal.Common;

namespace PacaGroup.Ecommerce.Application.Interface.UseCases
{
    public interface ICustomersApplication2
    {
        Task<Response<bool>> InsertAsync(CustomerDto2 CustomersDto2);
        Task<Response<bool>> UpdateAsync(CustomerDto2 CustomersDto2);
        Task<Response<bool>> DeleteAsync(string customerId);
        Task<Response<CustomerDto2>> GetAsync(string customerId);
        Task<Response<IEnumerable<CustomerDto2>>> GetAllAsync();
        Task<ResponsePagination<IEnumerable<CustomerDto>>> GetAllWithPaginationAsync(int pageNumber, int pageSize);
    }
}
