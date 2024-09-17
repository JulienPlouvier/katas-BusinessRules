namespace BusinessRules.Domain.PaymentHandlers;

using BusinessRules.Domain.Models;
using BusinessRules.Infrastructure;
using BusinessRules.Infrastructure.Models;

public class BookPaymentHandler : IPaymentHandler
{
    readonly IPackingSlipGenerator packingSlipGenerator;
    PhysicalProductPaymentHandler nextHandler;

    public BookPaymentHandler(IPackingSlipGenerator packingSlipGenerator, PhysicalProductPaymentHandler handler)
    {
        this.packingSlipGenerator = packingSlipGenerator;
        nextHandler = handler;

    }

    public PackingSlip? PackingSlip;

    public void Handle(Customer customer)
    {
        nextHandler.Handle(customer);
        PackingSlip = packingSlipGenerator.Duplicate(nextHandler.PackingSlip);
    }
}
