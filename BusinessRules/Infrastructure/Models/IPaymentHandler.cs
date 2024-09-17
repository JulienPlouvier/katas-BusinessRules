namespace BusinessRules.Infrastructure.Models;

using BusinessRules.Domain.Models;

public interface IPaymentHandler
{
    void Handle(Customer customer);
}