using NUnit.Framework;
using System.Linq;
using AutoFixture;
using AutoFixture.AutoMoq;
using Shop.Core.Models;

namespace Shop.Core.UnitTests.Models
{
    [TestFixture]
    public class BasketTestFixture
    {
        private IFixture fixture;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            fixture = new Fixture().Customize(new AutoConfiguredMoqCustomization());
        }

        [Test]
        public void Instance_WhenCalledMultipleTimes_ReturnsTheSameInstanceOfBasket()
        {
            // Arrange

            // Act
            var ins1 = Basket.Instance();
            var ins2 = Basket.Instance();

            // Assert
            Assert.AreEqual(ins1, ins2);
        }

        [Test]
        public void AddItem_WhenInvoked_AddsCorrectItemAndQuantityToBasket()
        {
            // Arrange
            var subject = Basket.Instance();
            var product = fixture.Create<Product>();
            var quantity = 10;

            // Act
            subject.AddItem(product, quantity);

            // Assert
            Assert.That(subject.Items.Any(i => i.Product.Name == product.Name && i.Quantity == quantity));
        }

        [Test]
        public void AddItem_IfItemWithNameAlreadyInBasket_IncrementsQuantityByCorrectAmount()
        {
            // Arrange
            var subject = Basket.Instance();
            var initProduct = fixture.Create<Product>();
            var additonalProduct = fixture.Build<Product>()
                .With(p => p.Name, initProduct.Name)
                .Create();

            var initQty = 10;
            var additionalQty = 5;

            subject.AddItem(initProduct, initQty);
            Assert.That(subject.Items.Any(i => i.Product == initProduct && i.Quantity == initQty));

            // Act
            subject.AddItem(additonalProduct, additionalQty);

            // Assert
            Assert.AreEqual(1, subject.Items.Select(i => i.Product.Name == initProduct.Name).Count());
            Assert.That(subject.Items.Any(i => i.Product.Name == initProduct.Name && i.Quantity == (initQty + additionalQty)));
        }
    }
}
