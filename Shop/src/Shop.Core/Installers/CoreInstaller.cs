using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Shop.Core.Models;
using Shop.Core.Offers;
using Shop.Core.Offers.Interfaces;
using Shop.Core.Services;
using Shop.Core.Services.Interfaces;

namespace Shop.Core.Installers
{
    public class CoreInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Kernel.Resolver.AddSubResolver(new CollectionResolver(container.Kernel));
            container.Kernel.Resolver.AddSubResolver(new ListResolver(container.Kernel));

            container.Register(
                Component.For<IProductRepo>()
                .ImplementedBy<ProductRepo>()
                .DependsOn(Dependency.OnValue("products", Products()))
                .LifestyleSingleton()
            );
            container.Register(
                Component.For<IOffer>()
                    .ImplementedBy<FourMilkOffer>()
                    .LifestyleSingleton());
            container.Register(
                Component.For<IOffer>()
                    .ImplementedBy<BreadAndButterOffer>()
                    .LifestyleSingleton());
            container.Register(
                Component.For<IBasketService>()
                    .ImplementedBy<BasketService>()
                    .LifestyleSingleton());
        }

        private IList<Product> Products()
        {
            return new List<Product>
            {
                new Product
                {
                    Name = "Butter",
                    Price = 0.8
                },
                new Product
                {
                    Name = "Milk",
                    Price = 1.15
                },
                new Product
                {
                    Name = "Bread",
                    Price = 1
                },
            };
        }
    }
}
