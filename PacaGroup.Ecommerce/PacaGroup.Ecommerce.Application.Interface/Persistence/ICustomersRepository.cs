using PacaGroup.Ecommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacaGroup.Ecommerce.Application.Interface.Persistense
{
    public interface ICustomersRepository
    {
        #region Metodos Sincronos
        bool Insert(Customer custumer);
        bool Update(Customer custumer);
        bool Delete(string CustumerId);
        Customer GetById(string ustumerId);
        IEnumerable<Customer> GetAll();
        #endregion

        #region Metodos Asincronos
        Task<bool> InsertAsync(Customer custumer);
        Task<bool> UpdateAsync(Customer custumer);
        Task<bool> DeleteAsync(string CustumerId);
        Task<Customer> GetByIdAsync(string CustumerId);
        Task<IEnumerable<Customer>> GetAllAsync();
        #endregion
    }
}
