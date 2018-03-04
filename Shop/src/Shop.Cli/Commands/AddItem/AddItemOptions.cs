using CommandLine;

namespace Shop.Cli.Commands.AddItem
{
    [Verb("add", HelpText = "Adds an item to the basket")]
    public class AddItemOptions
    {
        [Option('i', "item", HelpText = "Name of the item to add to the basket")]
        public string ItemName { get; set; }

        [Option('q', "quantity", HelpText = "Quantity of the item to add to the basket")]
        public int Quantity { get; set; }
    }
}
