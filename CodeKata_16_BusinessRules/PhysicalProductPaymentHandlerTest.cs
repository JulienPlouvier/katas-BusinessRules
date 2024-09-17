namespace BusinessRules_Tests;

using BusinessRules.Domain;
using BusinessRules.Domain.Models;
using BusinessRules.Infrastructure;
using BusinessRules.Infrastructure.DataMock;
using System.Threading.Tasks;

[Collection("PaymentHandler")]
public class PhysicalProductPaymentHandlerTest : IAsyncLifetime
{
    Customer customer = new("toto", "Foo", "totoFoo@mail.com", "2 Av des Champs Elysés");

    public Task InitializeAsync() => Task.CompletedTask;

    [Fact]
    public void RealizingPaymentForPhysicalProductShouldGenerateASinglePackingSlip()
    {
        var product = new PhysicalProduct("hammer");
        var handler = PaymentHandlerFactory.Create(product);
        var payment = new Payment(product, customer, handler);

        payment.Realize();

        FakeDatabase.PackingSlips.Should().HaveCount(1)
            .And.BeEquivalentTo(new List<PackingSlip> { new("2 Av des Champs Elysés", new[] { product }) });
    }

    [Fact]
    public void RealizingPaymentForBookShouldGenerateTwoPackingSlips()
    {
        var book = new Book("A Song of Ice and Fire");
        var handler = PaymentHandlerFactory.Create(book);
        var payment = new Payment(book, customer, handler);

        payment.Realize();

        List<PackingSlip> expectedPackingSlips = new List<PackingSlip>
        {
            new ("2 Av des Champs Elysés", new []{book}),
            new ("2 Av des Champs Elysés", new []{book})
        };

        FakeDatabase.PackingSlips.Should().HaveCount(2).And.BeEquivalentTo(expectedPackingSlips);
    }

    public Task DisposeAsync()
    {
        FakeDatabase.Clear();
        return Task.CompletedTask;
    }
}
