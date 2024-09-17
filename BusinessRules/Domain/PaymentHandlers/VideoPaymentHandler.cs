namespace BusinessRules.Domain.PaymentHandlers;

using BusinessRules.Domain.Models;
using BusinessRules.Infrastructure;
using BusinessRules.Infrastructure.Models;

public class VideoPaymentHandler : IPaymentHandler
{
    readonly Video video;
    readonly IPackingSlipGenerator packingSlipGenerator;

    static string[] AddFirstAidToTheseTitles = new string[] { "Learning To Ski" };

    public VideoPaymentHandler(Video video, IPackingSlipGenerator packingSlipGenerator)
    {
        this.video = video;
        this.packingSlipGenerator = packingSlipGenerator;
    }

    public void Handle(Customer customer)
    {
        if (AddFirstAidToTheseTitles.Contains(video.Title))
            packingSlipGenerator.Generate(customer.Address, video, new Video("First Aid"));
        else
            packingSlipGenerator.Generate(customer.Address, video);
    }
}