namespace BusinessRules_Tests;

using BusinessRules.Domain;
using BusinessRules.Domain.Models;
using BusinessRules.Infrastructure;
using BusinessRules.Infrastructure.DataMock;
using BusinessRules.Infrastructure.Models;
using System.Threading.Tasks;

[Collection("PaymentHandler")]
public class MembershipPaymentHandlerTest : IAsyncLifetime
{
    public Task InitializeAsync()
    {
        var customer = new Customer("Stanley", "Kubrick", "stanleyK@yahoo.net", "3 Hollywood Lane");
        FakeDatabase.Customers.Add(customer);
        return Task.CompletedTask;
    }

    [Fact]
    public void RealizingAMembershipPaymentHandlerTestShouldAffectTheMembershipOfTheCustomer()
    {
        var membership = new Membership("basic");
        var customer = FakeDatabase.Customers.Single(x => x.Surname == "Stanley");
        var handler = PaymentHandlerFactory.Create(membership);
        var payment = new Payment(membership, customer, handler);

        customer.ActiveMembership.Should().BeNull();

        payment.Realize();

        FakeDatabase.Customers.Single(x => x.Surname == "Stanley").ActiveMembership.Should().Be("basic");
        FakeMails.SentMails.Single().Should().Be(new Mail("app", "Stanley Kubrick", "Your membership is activated to status basic"));
    }

    [Fact]
    public void RealizingAMembershipUpgradePaymentHandlerTestShouldAffectTheMembershipOfTheCustomer()
    {
        var membership = new Membership("platinium", IsAnUpgrade: true);
        var customer = FakeDatabase.Customers.Single(x => x.Surname == "Stanley");
        var handler = PaymentHandlerFactory.Create(membership);
        var payment = new Payment(membership, customer, handler);

        customer.ActiveMembership.Should().BeNull();

        payment.Realize();

        FakeDatabase.Customers.Single(x => x.Surname == "Stanley").ActiveMembership.Should().Be("platinium");
        FakeMails.SentMails.Single().Should().Be(new Mail("app", "Stanley Kubrick", "Your membership has been upgraded to platinium"));
    }

    public Task DisposeAsync()
    {
        FakeDatabase.Clear();
        FakeMails.Clear();
        return Task.CompletedTask;
    }
}
