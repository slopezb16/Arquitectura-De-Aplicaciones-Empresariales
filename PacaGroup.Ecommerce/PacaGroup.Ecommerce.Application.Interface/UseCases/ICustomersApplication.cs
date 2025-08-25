using PacaGroup.Ecommerce.Application.DTO;
using PacaGroup.Ecommerce.Transversal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacaGroup.Ecommerce.Application.Interface.UseCases
{
    public interface ICustomersApplication
    {
        #region Metodos Sincronos
        Response<bool> Insert(CustomerDto custumer);
        Response<bool> Update(CustomerDto custumer);
        Response<bool> Delete(string CustumerId);
        Response<CustomerDto> GetById(string ustumerId);
        Response<IEnumerable<CustomerDto>> GetAll();
        #endregion

        #region Metodos Asincronos
        Task<Response<bool>> InsertAsync(CustomerDto custumer);
        Task<Response<bool>> UpdateAsync(CustomerDto custumer);
        Task<Response<bool>> DeleteAsync(string CustumerId);
        Task<Response<CustomerDto>> GetByIdAsync(string CustumerId);
        Task<Response<IEnumerable<CustomerDto>>> GetAllAsync();
        #endregion
    }
}
