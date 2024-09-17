namespace BusinessRules_Tests;

using BusinessRules.Domain.Models;
using BusinessRules.Domain.PaymentHandlers;
using BusinessRules.Infrastructure;

public class PaymentHandlerFactoryTest
{
    [Fact]
    public void CreatingABookPaymentHandlerShouldGenerateABookPaymentHandler()
    {
        var book = new Book("Fondation");

        var handler = PaymentHandlerFactory.Create(book);

        handler.Should().BeAssignableTo<BookPaymentHandler>();
    }

    [Fact]
    public void CreatingAMembershipPaymentHandlerShouldGenerateAMembershipPaymentHandler()
    {
        var newMembership = new Membership("gold", IsAnUpgrade: false);

        var handler = PaymentHandlerFactory.Create(newMembership);

        handler.Should().BeAssignableTo<MembershipPaymentHandler>();
    }

    [Fact]
    public void CreatingABasketBallPaymentHandlerShouldGenerateAPhysicalProductPaymentHandler()
    {
        var basketBall = new PhysicalProduct("Basket Ball");

        var handler = PaymentHandlerFactory.Create(basketBall);

        handler.Should().BeAssignableTo<PhysicalProductPaymentHandler>();
    }

    [Fact]
    public void CreatingAVideoPaymentHandlerShouldGenerateAVideoPaymentHandler()
    {
        var movie = new Video("Pulp Fiction");

        var handler = PaymentHandlerFactory.Create(movie);

        handler.Should().BeAssignableTo<VideoPaymentHandler>();
    }
}