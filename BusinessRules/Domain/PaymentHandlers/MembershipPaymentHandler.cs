namespace BusinessRules.Domain.PaymentHandlers;

using BusinessRules.Domain.Models;
using BusinessRules.Infrastructure;
using BusinessRules.Infrastructure.DataMock;
using BusinessRules.Infrastructure.Models;
using BusinessRules.Infrastructure.SyntaxicSugar;

public class MembershipPaymentHandler : IPaymentHandler
{
    readonly Membership membership;
    readonly IEmailService emailService;

    public MembershipPaymentHandler(Membership membership, IEmailService emailService)
    {
        this.membership = membership;
        this.emailService = emailService;
    }

    public void Handle(Customer customer)
    {
        var customerWithMembership = customer with { ActiveMembership = membership.Name };
        FakeDatabase.Customers = FakeDatabase.Customers
            .AddOrReplace(customerWithMembership,
                x => x.Surname == customer.Surname && x.Name == customer.Name)
            .ToList();

        if (membership.IsAnUpgrade)
            emailService.SendForUpgradeMembership(customerWithMembership);
        else
            emailService.SendForNewMembership(customerWithMembership);
    }
}
