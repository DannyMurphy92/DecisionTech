using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Core.Models;

namespace Shop.Core.UnitTests.Models
{
    [TestFixture]
    public class BasketTestFixture
    {

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
    }
}
