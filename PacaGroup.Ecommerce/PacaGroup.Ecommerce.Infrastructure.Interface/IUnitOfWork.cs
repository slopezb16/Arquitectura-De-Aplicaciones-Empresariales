namespace PacaGroup.Ecommerce.Infrastructure.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        ICustomersRepository2 Customers { get; }
        IUsersRepository Users { get; }
    }
}
