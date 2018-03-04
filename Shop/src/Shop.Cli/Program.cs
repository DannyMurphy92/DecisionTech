using System;
using Castle.Windsor;
using CommandLine;
using Shop.Cli.Commands;
using Shop.Cli.Commands.AddItem;
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

            Console.WriteLine("Enter a command");
            args = Console.ReadLine()?.Split(' ');

            while (args != null && args[0].ToLower() != "e")
            {
                var options = Parser.Default.ParseArguments<AddItemOptions, TotalBasketOptions>(args);

                options.MapResult(
                    (AddItemOptions opts) => container.Resolve<AddItemCommand>().Execute(opts, basket),
                    (TotalBasketOptions opts) => container.Resolve<TotalBasketCommand>().Execute(opts, basket),
                    errs => 1
                );

                args = Console.ReadLine()?.Split(' ');
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
