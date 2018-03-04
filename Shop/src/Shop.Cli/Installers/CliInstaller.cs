using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Shop.Cli.Commands.AddItem;
using Shop.Cli.Commands.ListItems;
using Shop.Cli.Commands.TotalBasket;
using Shop.Core.Installers;

namespace Shop.Cli.Installers
{
    public class CliInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Install(new CoreInstaller());

            container.Register(
                Component.For<AddItemCommand>()
                    .LifestyleSingleton()
            );
            container.Register(
                Component.For<TotalBasketCommand>()
                    .LifestyleSingleton()
            );
            container.Register(
                Component.For<ListItemsCommand>()
                    .LifestyleSingleton()
            );
        }
    }
}
