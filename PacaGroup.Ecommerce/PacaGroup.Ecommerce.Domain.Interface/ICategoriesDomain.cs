using PacaGroup.Ecommerce.Domain.Entity;

namespace PacaGroup.Ecommerce.Domain.Interface
{
    public interface ICategoriesDomain
    {
        Task<IEnumerable<Categories>> GetAll();
    }
}
