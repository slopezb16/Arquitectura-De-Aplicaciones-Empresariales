using PacaGroup.Ecommerce.Domain.Entity;
using PacaGroup.Ecommerce.Domain.Interface;
using PacaGroup.Ecommerce.Infrastructure.Interface;

public class CustomersDomain : ICustomersDomain
{
    private readonly ICustomersRepository _customersRepository;

    public CustomersDomain(ICustomersRepository customersRepository)
    {
        _customersRepository = customersRepository;
    }

    #region Métodos Sincronos
    public bool Insert(Customers customer)
    {
        // Regla de negocio: No insertar si ya existe
        //var existingCustomer =  _customersRepository.GetById(customer.CustomerId);
        //if (existingCustomer != null)
        //{
        //    throw new Exception("El cliente ya existe.");
        //}

        // Si no existe, lo insertamos
        return _customersRepository.Insert(customer);
    }

    public bool Update(Customers customer)
    {
        return _customersRepository.Update(customer);
    }

    public bool Delete(string customerId)
    {
        return _customersRepository.Delete(customerId);
    }

    public Customers GetById(string customerId)
    {
        return _customersRepository.GetById(customerId);
    }

    public IEnumerable<Customers> GetAll()
    {
        return _customersRepository.GetAll();
    }
    #endregion

    #region Métodos Asincronos
    public async Task<bool> InsertAsync(Customers customer)
    {
        return await _customersRepository.InsertAsync(customer);
    }

    public async Task<bool> UpdateAsync(Customers customer)
    {
        return await _customersRepository.UpdateAsync(customer);
    }

    public async Task<bool> DeleteAsync(string customerId)
    {
        return await _customersRepository.DeleteAsync(customerId);
    }

    public async Task<Customers> GetByIdAsync(string customerId)
    {
        return await _customersRepository.GetByIdAsync(customerId);
    }

    public async Task<IEnumerable<Customers>> GetAllAsync()
    {
        return await _customersRepository.GetAllAsync();
    }
    #endregion
}
