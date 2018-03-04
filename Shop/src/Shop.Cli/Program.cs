using System;
using Castle.Windsor;
using CommandLine;
using Shop.Cli.Commands.AddItem;
using Shop.Cli.Commands.Exit;
using Shop.Cli.Commands.ListBasket;
using Shop.Cli.Commands.ListItems;
using Shop.Cli.Commands.TotalBasket;
using Shop.Cli.Installers;
using Shop.Core.Models;

namespace Shop.Cli
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var container = CreateContainer();

            var basket = new Basket();
            var result = 0;

            while (args != null && result == 0)
            {
                Console.WriteLine("Enter a command");
                args = Console.ReadLine()?.Split(' ');
                var options = Parser.Default.ParseArguments<
                    AddItemOptions, 
                    TotalBasketOptions, 
                    ListItemsOptions, 
                    ExitOptions,
                    ListBasketItemsOptions>(args);

                result = options.MapResult(
                    (AddItemOptions opts) => container.Resolve<AddItemCommand>().Execute(opts, basket),
                    (TotalBasketOptions opts) => container.Resolve<TotalBasketCommand>().Execute(opts, basket),
                    (ListItemsOptions opts) => container.Resolve<ListItemsCommand>().Execute(opts),
                    (ExitOptions opts) => container.Resolve<ExitCommand>().Execute(opts),
                    (ListBasketItemsOptions opts) => container.Resolve<ListBasketItemsCommand>().Execute(opts, basket),
                    errs => 2
                );

                Console.WriteLine("\n");
            }
        }

        private static IWindsorContainer CreateContainer()
        {
            var container = new WindsorContainer();
            container.Install(new CliInstaller());

            return container;
        }
    }
}
