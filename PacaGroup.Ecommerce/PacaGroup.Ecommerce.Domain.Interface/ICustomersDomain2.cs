using PacaGroup.Ecommerce.Domain.Entity;

namespace PacaGroup.Ecommerce.Domain.Interface
{
    public interface ICustomersDomain2
    {
        Task<bool> DeleteAsync(string customerId);
        Task<IEnumerable<Customers>> GetAllAsync();
        Task<Customers> GetAsync(string customerId);
        Task<bool> InsertAsync(Customers customer);
        Task<bool> UpdateAsync(Customers customer);
    }
}
