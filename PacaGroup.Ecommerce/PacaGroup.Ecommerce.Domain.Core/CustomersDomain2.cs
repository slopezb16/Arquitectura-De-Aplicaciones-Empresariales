using PacaGroup.Ecommerce.Domain.Entity;
using PacaGroup.Ecommerce.Domain.Interface;
using PacaGroup.Ecommerce.Infrastructure.Interface;

namespace PacaGroup.Ecommerce.Domain.Core
{
    internal class CustomersDomain2 : ICustomersDomain2
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomersDomain2(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> DeleteAsync(string customerId)
        {
            return await _unitOfWork.Customers.DeleteAsync(customerId);
        }

        public async Task<IEnumerable<Customers>> GetAllAsync()
        {
            return await _unitOfWork.Customers.GetAllAsync();
        }

        public async Task<Customers> GetAsync(string customerId)
        {
            return await _unitOfWork.Customers.GetAsync(customerId);
        }

        public async Task<bool> InsertAsync(Customers customer)
        {
            return await _unitOfWork.Customers.InsertAsync(customer);
        }

        public async Task<bool> UpdateAsync(Customers customer)
        {
            return await _unitOfWork.Customers.UpdateAsync(customer);
        }
    }
}
