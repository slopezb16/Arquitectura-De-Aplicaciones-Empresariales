using PacaGroup.Ecommerce.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacaGroup.Ecommerce.Infrastructure.Interface
{
    public interface ICustomersRepository
    {
        #region Metodos Sincronos
        bool Insert(Customers custumer);
        bool Update(Customers custumer);
        bool Delete(string CustumerId);
        Customers GetById(string ustumerId);
        IEnumerable<Customers> GetAll();
        #endregion

        #region Metodos Asincronos
        Task<bool> InsertAsync(Customers custumer);
        Task<bool> UpdateAsync(Customers custumer);
        Task<bool> DeleteAsync(string CustumerId);
        Task<Customers> GetByIdAsync(string CustumerId);
        Task<IEnumerable<Customers>> GetAllAsync();
        #endregion
    }
}
