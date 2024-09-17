namespace BusinessRules_Tests;

using BusinessRules.Domain;
using BusinessRules.Domain.Models;
using BusinessRules.Infrastructure;
using BusinessRules.Infrastructure.DataMock;
using System.Collections.Generic;

[Collection("PaymentHandler")]
public class VideoPaymentHandlerTest : IAsyncLifetime
{
    Customer john = new Customer("John Ronald Reuel", "Tolkien", "jrrtolkien@hotmail.com", "12 hobbittown");

    [Fact]
    public void VideoPaymentShouldGeneratePackingSlip()
    {
        john = john with { Address = "12 hobbittown" }
        var video = new Video("La Communauté de l'Anneau");
        var handler = PaymentHandlerFactory.Create(video);
        var payment = new Payment(video, john, handler);

        payment.Realize();

        FakeDatabase.PackingSlips.Should().HaveCount(1)
            .And.BeEquivalentTo(new List<PackingSlip> { new("12 hobbittown", new[] { video }) });
    }

    [Fact]
    public void LearningToSkiVideoPaymentShouldAddFirstAidVideoToPackingSlip()
    {
        var video = new Video("Learning To Ski");
        var handler = PaymentHandlerFactory.Create(video);
        var payment = new Payment(video, john, handler);

        payment.Realize();

        FakeDatabase.PackingSlips.Should().HaveCount(1)
        .And.BeEquivalentTo(new List<PackingSlip> { new("12 hobbittown", new[] { video, new Video("First Aid") }) });
    }

    public Task DisposeAsync()
    {
        FakeDatabase.Clear();
        return Task.CompletedTask;
    }

    public Task InitializeAsync() => Task.CompletedTask;
}
