using PacaGroup.Ecommerce.Application.DTO;
using PacaGroup.Ecommerce.Transversal.Common;

namespace PacaGroup.Ecommerce.Application.Interface
{
    public interface ICategoriesApplication
    {
        Task<Response<IEnumerable<CategoriesDto>>> GetAll();
    }
}
