using AutoFixture;
using AutoFixture.AutoMoq;
using Moq;
using NUnit.Framework;
using Shop.Cli.Commands;
using Shop.Core.Models;
using Shop.Core.Services.Interfaces;

namespace Shop.Cli.UniTests.Commands
{
    [TestFixture]
    public class AddItemCommandTestFixture
    {
        private IFixture fixture;

        private Mock<IBasketService> basketServiceMock;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            fixture = new Fixture().Customize(new AutoConfiguredMoqCustomization());

            basketServiceMock = fixture.Freeze<Mock<IBasketService>>();
        }

        [Test]
        public void Execute_WhenInvoked_CallsBasketService()
        {
            // Arrange
            var options = fixture.Create<AddItemOptions>();
            var basket = fixture.Create<Basket>();
            var subject = fixture.Create<AddItemCommand>();

            // Act
            subject.Execute(options, basket);

            // Assert
            basketServiceMock.Verify(b => b.AddProductToBasket(basket, options.ItemName, options.Quantity));
        }
    }
}
