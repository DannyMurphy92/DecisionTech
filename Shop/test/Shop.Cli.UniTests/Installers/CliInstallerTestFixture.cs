using Castle.Windsor;
using NUnit.Framework;
using Shop.Cli.Commands;
using Shop.Cli.Installers;

namespace Shop.Cli.UniTests.Installers
{
    [TestFixture]
    public class CliInstallerTestFixture
    {
        [Test]
        public void Install_WhenCalled_RegistersImplementations()
        {
            // Arrange
            var container = new WindsorContainer();

            // Act
            container.Install(new CliInstaller());

            // Assert
            Assert.AreEqual(typeof(AddItemCommand), container.Resolve<AddItemCommand>().GetType());
        }
    }
}
