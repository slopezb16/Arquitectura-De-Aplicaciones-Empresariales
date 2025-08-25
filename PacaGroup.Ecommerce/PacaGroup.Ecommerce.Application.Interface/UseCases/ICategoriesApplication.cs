using PacaGroup.Ecommerce.Application.DTO;
using PacaGroup.Ecommerce.Transversal.Common;

namespace PacaGroup.Ecommerce.Application.Interface.UseCases
{
    public interface ICategoriesApplication
    {
        Task<Response<IEnumerable<CategoryDto>>> GetAll();
    }
}
