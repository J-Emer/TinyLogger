# TinyLogger
 Tiny File Logger for logging helpful information about your project to the console, or a `.txt` log file.
 
 ## Logging
 
 First create a new instance of the `Logger` class.
 

 ```csharp 
            Logger _logger = new Logger
            {
                LogFilePath = @"C:\Path\to\Log.txt",
                UseLogFile = true,
                Width = 100,
            };
```
 
 Next lets log some info to the console.
 
 ```csharp
             _logger.Log("Some Log Info");
```

This will simply log `Some Log Info` to both the `console` and `log.txt` file. Remember `UseLogFile = true`, if you only want the info to show up in the console then set `UseLogFile = false`

Next we can add in the `sending` object

 ```csharp
             _logger.Log(this, "Some Log Info");
```

Will output `[Tester]: Some Log Info` to the console or log file, or both.

We can also set the color of the message(console only) depending on the severity level of the message

```csharp
            _logger.Log(this, "Some Log Info", ConsoleLevel.Error);
```
Will output `[Tester]: Some Log Info` in the color red.

There are 5 ConsoleLevels to chose from
* Error: Red,
* Warning: Yellow,
* Info: White,
* Message: Blue,
* Success: Green,



## Table

Create a new `Table` instance

```csharp
            Table _table = new Table(_logger);
```
You will notice it requires the `Logger` from earlier.



Add in your Header
```csharp
            _table.AddHeader(new List<string>{"Col 1", "Col 2", "Col 3"});
```
And some Rows
```csharp
            _table.AddRow(new List<string>{"Item 1", "Item 2", "Item 3"});
            _table.AddRow(new List<string>{"Item 4", "Item 5", "Item 6"});
```
To output the tables contents call the `_table.Output();`

You can set the output color of the table by setting its `ConsoleLevel` property.

If you plan to resue the `table` multiple times you will need to call its `ClearHeader()` and `ClearRows()` methods before each output.


