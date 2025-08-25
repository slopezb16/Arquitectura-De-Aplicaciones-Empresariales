namespace PacaGroup.Ecommerce.Application.Interface.Persistense
{
    public interface IUnitOfWork : IDisposable
    {
        ICustomersRepository2 Customers { get; }
        IUsersRepository Users { get; }
    }
}
