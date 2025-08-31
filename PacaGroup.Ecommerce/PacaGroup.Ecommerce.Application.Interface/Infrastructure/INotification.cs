namespace PacaGroup.Ecommerce.Application.Interface.Infrastructure
{
    public interface INotification
    {
        Task<bool> SendMailAsync(string subject, string body, CancellationToken cancellationToken = new());
    }
}
