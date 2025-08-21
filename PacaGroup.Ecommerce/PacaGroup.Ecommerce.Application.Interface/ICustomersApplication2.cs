using PacaGroup.Ecommerce.Application.DTO;
using PacaGroup.Ecommerce.Transversal.Common;

namespace PacaGroup.Ecommerce.Application.Interface
{
    public interface ICustomersApplication2
    {
        Task<Response<bool>> InsertAsync(CustomersDto2 CustomersDto2);
        Task<Response<bool>> UpdateAsync(CustomersDto2 CustomersDto2);
        Task<Response<bool>> DeleteAsync(string customerId);
        Task<Response<CustomersDto2>> GetAsync(string customerId);
        Task<Response<IEnumerable<CustomersDto2>>> GetAllAsync();
        Task<ResponsePagination<IEnumerable<CustomersDto>>> GetAllWithPaginationAsync(int pageNumber, int pageSize);
    }
}
