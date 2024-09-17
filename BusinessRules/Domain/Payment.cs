namespace BusinessRules.Domain;

using BusinessRules.Domain.Models;
using BusinessRules.Infrastructure.Models;

public class Payment
{
    readonly Product product;
    readonly Customer customer;
    readonly IPaymentHandler handler;

    public Payment(Product product, Customer customer, IPaymentHandler handler)
    {
        this.product = product;
        this.customer = customer;
        this.handler = handler;
    }

    public Task<bool> Realize()
    {
        handler.Handle(customer);
        return Task.FromResult(true);
    }
}
