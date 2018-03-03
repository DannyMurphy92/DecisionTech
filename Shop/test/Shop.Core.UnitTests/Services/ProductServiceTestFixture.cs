using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFixture;
using NUnit.Framework;
using Shop.Core.Models;
using Shop.Core.Services;

namespace Shop.Core.UnitTests.Services
{
    [TestFixture]
    public class ProductServiceTestFixture
    {
        private IFixture fixture;

        private IList<Product> products;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            fixture = new Fixture();
        }

        [SetUp]
        public void Setup()
        {

            products = fixture.CreateMany<Product>(10).ToList();
        }

        [Test]
        public void GetProductByName_WhenProductExists_ReturnsCorrectProduct()
        {
            // Arrange
            var subject = new ProductRepo(products);

            // Act
            var result = subject.GetProductByName(products[0].Name);

            // Assert
            Assert.AreEqual(products[0], result);
        }

        [Test]
        public void GetProductByName_WhenProductDoesNotExist_ReturnsNull()
        {
            // Arrange
            var subject = new ProductRepo(products);

            // Act
            var result = subject.GetProductByName(fixture.Create<string>());

            // Assert
            Assert.IsNull(result);
        }
    }
}
