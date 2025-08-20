using PacaGroup.Ecommerce.Infrastructure.Interface;

namespace PacaGroup.Ecommerce.Infrastructure.Repository
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
            System.GC.SuppressFinalize(this);
        }
    }
}
