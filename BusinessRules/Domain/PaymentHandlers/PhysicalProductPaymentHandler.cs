namespace BusinessRules.Domain.PaymentHandlers;

using BusinessRules.Domain.Models;
using BusinessRules.Infrastructure;
using BusinessRules.Infrastructure.Models;

public class PhysicalProductPaymentHandler : IPaymentHandler
{
    readonly PhysicalProduct product;
    readonly IPackingSlipGenerator packingSlipGenerator;

    public PhysicalProductPaymentHandler(PhysicalProduct product, IPackingSlipGenerator packingSlipGenerator)
    {
        this.product = product;
        this.packingSlipGenerator = packingSlipGenerator;
    }

    public PackingSlip? PackingSlip;

    public void Handle(Customer customer)
    {
        PackingSlip = packingSlipGenerator.Generate(customer.Address, product);
    }
}