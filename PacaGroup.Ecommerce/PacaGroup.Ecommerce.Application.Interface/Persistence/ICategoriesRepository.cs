using PacaGroup.Ecommerce.Domain.Entities;

namespace PacaGroup.Ecommerce.Application.Interface.Persistence
{
    public interface ICategoriesRepository
    {
        Task<IEnumerable<Category>> GetAll();
    }
}
