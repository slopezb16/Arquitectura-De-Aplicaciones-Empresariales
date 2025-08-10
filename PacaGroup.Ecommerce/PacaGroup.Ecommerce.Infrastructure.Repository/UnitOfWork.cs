using PacaGroup.Ecommerce.Infrastructure.Interface;

namespace PacaGroup.Ecommerce.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public ICustomersRepository2 Customers { get; }
        public UnitOfWork(ICustomersRepository2 customers)
        {
            Customers = customers;
        }

        public void Dispose()
        {
            System.GC.SuppressFinalize(this);
        }
    }
}
