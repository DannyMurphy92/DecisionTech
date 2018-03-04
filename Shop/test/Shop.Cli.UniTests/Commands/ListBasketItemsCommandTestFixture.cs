using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoMoq;
using NUnit.Framework;
using Shop.Cli.Commands.ListBasket;
using Shop.Core.Models;

namespace Shop.Cli.UniTests.Commands
{
    public class ListBasketItemsCommandTestFixture
    {
        private IFixture fixture;

        private Basket basket;
        private ListBasketItemsOptions options;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            fixture = new Fixture().Customize(new AutoConfiguredMoqCustomization());
        }

        [SetUp]
        public void Setup()
        {
            basket = fixture.Create<Basket>();
        }

        [Test]
        public void Execute_WhenInvoekd_ReturnsZero()
        {
            // Arrange
            var subject = fixture.Create<ListBasketItemsCommand>();

            // Act
            var result = subject.Execute(options, basket);

            // Assert
            Assert.AreEqual(0, result);
        }
    }
}
