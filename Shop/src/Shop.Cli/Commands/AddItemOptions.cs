using CommandLine;

namespace Shop.Cli.Commands
{
    [Verb("add")]
    public class AddItemOptions
    {
        [Option('i', "item", HelpText = "Name of the item to add to the basket")]
        public string ItemName { get; set; }

        [Option('q', "quantity", HelpText = "Quantity of the item to add to the basket")]
        public int Quantity { get; set; }
    }
}
