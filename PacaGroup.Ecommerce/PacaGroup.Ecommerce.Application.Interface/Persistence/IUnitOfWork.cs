using PacaGroup.Ecommerce.Application.Interface.Persistence;

namespace PacaGroup.Ecommerce.Application.Interface.Persistense
{
    public interface IUnitOfWork : IDisposable
    {
        ICustomersRepository2 Customers { get; }
        IUsersRepository Users { get; }
        //ICategoriesRepository Categories { get; }
        IDiscountRepository Discounts { get; }
        Task<int> Save(CancellationToken cancellationToken);
    }
}
