using PacaGroup.Ecommerce.Domain.Entity;

namespace PacaGroup.Ecommerce.Application.Interface.Persistense
{
    public interface IGenericRepository<T> where T : class
    {
        #region Metodos Sincronos
        bool Insert(T entity);
        bool Update(T entity);
        bool Delete(string CustumerId);
        Customer GetById(string ustumerId);
        IEnumerable<Customer> GetAll();
        #endregion

        Task<bool> InsertAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(string customerId);
        Task<T?> GetAsync(string customerId);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(string ustumerId);
        Task<IEnumerable<T>> GetAllWithPaginationAsync(int pageNumber, int pageSize);
        Task<int> CountAsync();

    }
}
