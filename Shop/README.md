# Shop CLI

## Running
    To start the application run `dotnet Shop.Cli.dll` afterwhich you will be asked to enter a command

## Commands

### add
    Adds a specified quantity of an item to the basket
    options
        -i, -item       Name of the item to add to the basket
        -q, -quantity   Quantity of the item to add to the basket

    Example: `add -i bread -q 5`

### total
    Calculates the total of the basket including applicable offers
    options
        null
    
    Example: `total`

### exit
    options
        null
        
    Example: `exit`

### list
    Lists all the available products
    options
        null

    Example: `list`

### basket
    Lists all the items in the basket
    options
        null

    Example: `basket`

### help
    Displays help text
    options
        null
    Example: `help`