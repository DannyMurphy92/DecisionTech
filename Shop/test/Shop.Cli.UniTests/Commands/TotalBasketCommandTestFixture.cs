using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoMoq;
using Moq;
using NUnit.Framework;
using Shop.Cli.Commands;
using Shop.Cli.Commands.TotalBasket;
using Shop.Core.Models;
using Shop.Core.Services.Interfaces;

namespace Shop.Cli.UniTests.Commands
{
    [TestFixture]
    public class TotalBasketCommandTestFixture
    {
        private IFixture fixture;

        private Mock<IBasketService> basketServiceMock;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            fixture = new Fixture().Customize(new AutoConfiguredMoqCustomization());

            basketServiceMock = fixture.Freeze<Mock<IBasketService>>();
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
            var options = fixture.Create<TotalBasketOptions>();
            var basket = fixture.Create<Basket>();
            var subject = fixture.Create<TotalBasketCommand>();

            // Act
            subject.Execute(options, basket);

            // Assert
            basketServiceMock.Verify(b => b.CalculateTotal(basket));
        }
    }
}
