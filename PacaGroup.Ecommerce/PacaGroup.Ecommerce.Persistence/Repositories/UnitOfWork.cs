using PacaGroup.Ecommerce.Application.Interface.Persistense;

namespace PacaGroup.Ecommerce.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public ICustomersRepository2 Customers { get; }

        public IUsersRepository Users { get; }

        public UnitOfWork(ICustomersRepository2 customers, IUsersRepository users)
        {
            Customers = customers;
            Users = users;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
