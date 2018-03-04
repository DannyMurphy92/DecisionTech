using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Shop.Cli.Commands.Exit;

namespace Shop.Cli.UniTests.Commands
{
    [TestFixture]
    public class ExitCommandTestfixture
    {

        [Test]
        public void Execute_WhenInvoked_ReturnsOne()
        {
            // Arrange
            var subject = new ExitCommand();

            // Act
            var result = subject.Execute(new ExitOptions());

            // Assert
            Assert.AreEqual(1, result);
        }
    }
}
