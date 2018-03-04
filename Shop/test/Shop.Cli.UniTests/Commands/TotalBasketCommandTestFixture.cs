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
        private Basket basket;
        private TotalBasketOptions options;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            fixture = new Fixture().Customize(new AutoConfiguredMoqCustomization());

            basketServiceMock = fixture.Freeze<Mock<IBasketService>>();
        }

        [SetUp]
        public void Setup()
        {
            options = fixture.Create<TotalBasketOptions>();
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
            var subject = fixture.Create<TotalBasketCommand>();

            // Act
            subject.Execute(options, basket);

            // Assert
            basketServiceMock.Verify(b => b.CalculateTotal(basket));
        }


        [Test]
        public void Execute_WhenInvoked_ReturnsZero()
        {
            // Arrange
            var subject = fixture.Create<TotalBasketCommand>();

            // Act
            var result = subject.Execute(options, basket);

            // Assert
            Assert.AreEqual(0, result);
        }
    }
}
