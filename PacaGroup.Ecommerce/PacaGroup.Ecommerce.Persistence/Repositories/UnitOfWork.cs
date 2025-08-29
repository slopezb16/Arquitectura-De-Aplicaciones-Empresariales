using PacaGroup.Ecommerce.Application.Interface.Persistence;
using PacaGroup.Ecommerce.Application.Interface.Persistense;
using PacaGroup.Ecommerce.Persistence.Contexts;

namespace PacaGroup.Ecommerce.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public ICustomersRepository2 Customers { get; }

        public IUsersRepository Users { get; }

        public IDiscountRepository Discounts { get; }

        private readonly ApplicationDbContext _applicationDbContext;

        public UnitOfWork(ICustomersRepository2 customers, IUsersRepository users, IDiscountRepository discounts, ApplicationDbContext applicationDbContext)
        {
            Customers = customers;
            Users = users;
            Discounts = discounts;
            _applicationDbContext = applicationDbContext;
        }

        public async Task<int> Save(CancellationToken cancellationToken)
        {
            return await _applicationDbContext.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
