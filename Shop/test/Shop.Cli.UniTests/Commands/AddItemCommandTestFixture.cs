using AutoFixture;
using AutoFixture.AutoMoq;
using Moq;
using NUnit.Framework;
using Shop.Cli.Commands;
using Shop.Cli.Commands.AddItem;
using Shop.Core.Models;
using Shop.Core.Services.Interfaces;

namespace Shop.Cli.UniTests.Commands
{
    [TestFixture]
    public class AddItemCommandTestFixture
    {
        private IFixture fixture;

        private Mock<IBasketService> basketServiceMock;
        private Basket basket;
        private AddItemOptions options;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            fixture = new Fixture().Customize(new AutoConfiguredMoqCustomization());

            basketServiceMock = fixture.Freeze<Mock<IBasketService>>();
        }

        [SetUp]
        public void Setup()
        {
            options = fixture.Create<AddItemOptions>();
            basket = fixture.Create<Basket>();
        }

        [TearDown]
        public void Teardown()
        {
            basketServiceMock.Reset();
        }

        [Test]
        public void Execute_WhenInvoked_CallsBasketService()
        {
            // Arrange
            var subject = fixture.Create<AddItemCommand>();

            // Act
            subject.Execute(options, basket);

            // Assert
            basketServiceMock.Verify(b => b.AddProductToBasket(basket, options.ItemName, options.Quantity));
        }

        [Test]
        public void Execute_WhenInvoked_ReturnsZero()
        {
            // Arrange
            var subject = fixture.Create<AddItemCommand>();

            // Act
            var result = subject.Execute(options, basket);

            // Assert
            Assert.AreEqual(0, result);
        }
    }
}
