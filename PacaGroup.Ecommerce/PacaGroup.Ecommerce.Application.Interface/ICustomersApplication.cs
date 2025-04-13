using PacaGroup.Ecommerce.Application.DTO;
using PacaGroup.Ecommerce.Transversal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacaGroup.Ecommerce.Application.Interface
{
    public interface ICustomersApplication
    {
        #region Metodos Sincronos
        Response<bool> Insert(CustomersDto custumer);
        Response<bool> Update(CustomersDto custumer);
        Response<bool> Delete(string CustumerId);
        Response<CustomersDto> GetById(string ustumerId);
        Response<IEnumerable<CustomersDto>> GetAll();
        #endregion

        #region Metodos Asincronos
        Task<Response<bool>> InsertAsync(CustomersDto custumer);
        Task<Response<bool>> UpdateAsync(CustomersDto custumer);
        Task<Response<bool>> DeleteAsync(string CustumerId);
        Task<Response<CustomersDto>> GetByIdAsync(string CustumerId);
        Task<Response<IEnumerable<CustomersDto>>> GetAllAsync();
        #endregion
    }
}
