namespace BusinessRules.Infrastructure;

using BusinessRules.Domain.Models;
using BusinessRules.Infrastructure.DataMock;
using BusinessRules.Infrastructure.Models;

public interface IEmailService
{
    void SendForNewMembership(Customer customer);
    void SendForUpgradeMembership(Customer customer);
}

public class EmailService : IEmailService
{
    public void SendForNewMembership(Customer customer)
    {
        var mail = new Mail("app", customer.FullName(), $"Your membership is activated to status {customer.ActiveMembership}");
        FakeMails.SentMails.Add(mail);
    }

    public void SendForUpgradeMembership(Customer customer)
    {
        var mail = new Mail("app", customer.FullName(), $"Your membership has been upgraded to {customer.ActiveMembership}");
        FakeMails.SentMails.Add(mail);
    }
}
