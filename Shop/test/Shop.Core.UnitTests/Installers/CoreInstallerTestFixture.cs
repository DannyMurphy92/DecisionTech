using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Windsor;
using NUnit.Framework;
using Shop.Core.Installers;
using Shop.Core.Offers;
using Shop.Core.Offers.Interfaces;
using Shop.Core.Services;
using Shop.Core.Services.Interfaces;

namespace Shop.Core.UnitTests.Installers
{
    [TestFixture]
    public class CoreInstallerTestFixture
    {
        [Test]
        public void Install_WhenCalled_RegistersImplementations()
        {
            // Arrange
            var container = new WindsorContainer();
            
            // Act
            container.Install(new CoreInstaller());

            // Assert
            Assert.AreEqual(typeof(ProductRepo), container.Resolve<IProductRepo>().GetType());
            Assert.IsTrue(container.ResolveAll<IOffer>().Any(o => o.GetType() == typeof(FourMilkOffer)));
            Assert.IsTrue(container.ResolveAll<IOffer>().Any(o => o.GetType() == typeof(BreadAndButterOffer)));
            Assert.AreEqual(typeof(BasketService), container.Resolve<IBasketService>().GetType());
        }
    }
}
