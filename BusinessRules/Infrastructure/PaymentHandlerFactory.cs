namespace BusinessRules.Infrastructure;

using BusinessRules.Domain.Models;
using BusinessRules.Domain.PaymentHandlers;
using BusinessRules.Infrastructure.Models;

public static class PaymentHandlerFactory
{
    public static IPaymentHandler Create(Product product)
    {
        if (product is PhysicalProduct physicalProduct)
            return CreatePhysicalProductPaymentHandler(physicalProduct);

        return product switch
        {
            Membership membership => new MembershipPaymentHandler(membership, new EmailService()),
            Video video => new VideoPaymentHandler(video, new PackingSlipGenerator()),
            _ => throw new InvalidOperationException()
        };
    }

    static IPaymentHandler CreatePhysicalProductPaymentHandler(PhysicalProduct physicalProduct)
    {
        var packingSlipGenerator = new PackingSlipGenerator();
        var physicalProductHandler = new PhysicalProductPaymentHandler(physicalProduct, packingSlipGenerator);

        return physicalProduct switch
        {
            Book => new BookPaymentHandler(packingSlipGenerator, physicalProductHandler),
            _ => physicalProductHandler
        };
    }
}