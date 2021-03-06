# konzolR 
C# Console Utility Microlibrary

[![Packagist](https://img.shields.io/packagist/l/doctrine/orm.svg?style=for-the-badge)]()
[![NuGet](https://img.shields.io/badge/NUGET-v0.1.0-blue.svg?style=for-the-badge)](https://www.nuget.org/packages/sbkst.konzolR/)
## About
konzolR is a Micro Library for C# Console Applications

## Why
The why is easy, I've a -lot- of custom written Build Tools in my tool chain to support me in my daily business. Those are all console applications which are embedded in the pre- and post-Build Proccess. One thing that bugged me while maintaining those where those *tedious* generic things I almost always need in those tools and I used to do over and over again. Altough those arent hard tasks and mostly not library worthy they bugged me so much that I created this (micro-)library 🔧

## 🗃 Whats in the box 
### ⚙ (simple) Argument Binder
The Argument Binder is designed to be **verbose**, I literally want to know whats going on when looking at the source. We all know or have seen horrible constructs for handling the passed arguments - not that we wrote any of those 😸 - and you mostly loose it after the second if/switch block. 

So show me a example already!

So we want to bind our a args against

```c#
class InputArgs
    {
        public Boolean Force { get; set; }
        public Boolean OutputToFileSupplied { get; set; }
        public string FilePath { get; set; }
        public Boolean UsersSupplied { get; set; }
        public string[] UserNames { get; set; }
        public Boolean HexSupplied { get; set; }
        public string Hex { get; set; }
    }
```

with this

```c#
static void Main(string[] args)
        {
//create a new argument binder to bind it to InputArgs
var setup = ArgumentBinder.Create<InputArgs>();
//Setup the arguments we want to bind
var binder = setup
         .CreateFor("-f", "Forces creation", field => field.Force)
         .CreatePathArgumentFor("-out", "Sets outputfile dir", c => c.OutputToFileSupplied, c => c.FilePath)
         .CreateArgumentWithPayloadFor("-addr", "Sets Hex-Address", c => c.HexSupplied, c => c.Hex, new Regex("[^[0-9A-F]+$]"))
         .CreateArrayArgumentFor("-users", "List of users", c => c.UsersSupplied, c => c.UserNames)
         .Build();
try
{
    //bind it
    var input = binder.Bind(args);
    //do something with the input like
    if (input.Force) Console.WriteLine("Forced");
}
catch (ArgumentBinderException e) //check for binding errors
{
    Console.WriteLine("Please check the supplied arguments.");
    //dispay details whats wrong
    e.BindingErrors.ToList().ForEach(err =>
    {
        Console.WriteLine(err);
    });
    //display help text
    binder.Help();
}
catch (Exception ex)
{
    Console.WriteLine("Uh-Oh something went horrible wrong here.");
    Console.WriteLine("Error occured: {0}", ex.Message);
}
        }
```
So whats going on is, the binder is beeing setup with commands and then tries to bind the those to the supplied type. It does check the conditions for the argument-types supplied but wont allow you to define certain chains that need to be supplied.The binder itself is `stupid` its job is to automap the supplied args to the POCO, after that we still would have to validate the supplied arguments. While this seems pointless it is not; at least for me. It adds verbosity and readability to the the argument part - if you dont like it; dont use it.

For a more comprehensive overview check the `Documentation-Section`

### ⚙ (simple) Dialog 
This is a helper for basic console dialogs (dialog as in q&a - not visually like in curses). Its main purpose is to simplify first-time and one-time setups. The result of the dialog will be bound to a poco again for later processing. 

Lets have a look at some code.

We are binding to:
```c#
    public class TestInput
    {
        public string SomeString { get; set; }
        public bool SomeBool { get; set; }
        public int SomeInt { get; set; }
        public decimal SomeDecimal { get; set; }
        public decimal? OptionalDecimal { get; set; }
        public string FilteredString { get; set; }
    }
```

The dialog itself

```c#
var dialog = DialogHelper.Create<TestInput>("Hello, I am the test dialog, I will help you set things up");
dialog.Ask(c => c.SomeString, "Whats some string?");
dialog.Ask(c => c.SomeBool, "Whats some bool?");
dialog.Ask(c => c.SomeDecimal, "Whats some decimal?");
dialog.Ask(c => c.SomeInt, "Whats some int?");
dialog.AskOptional(c => c.OptionalDecimal, "You might want to add another decimal?");
dialog.AskOptional(c => c.FilteredString, "This is a skipable filtered string!", new Regex("[a-b]"));
dialog.Ask(c => c.FilteredString, "Now you cant skip this filtered string!", new Regex("[a-b]"));
TestInput data = dialog.Run();
```

This will start a line-by-line dialog asking the user the questions specified. As you may have noticed there are mandatory questions with .Ask and optional with .AskOptional, where AskOptional accepts nullable types. For more information refer to the `Documentation-Section`

### ⚙ (simple) Loading Bar (& Waiting Bar)
Displays a classic `progress bar`, the loading bar takes a percentage value which it will be drawn to, while the waiting bar will just keep filling itself up till its stopped. This goes 
well with the title screen and preloading ressources or checking stati before running the application.

A loading bar example

```c#
using (var bar = Loading.LoadingHelper.GetLoadingBar())
{
    for (short i = 0; i <= 100; i++)
    {
        bar.SetPercentage(i);
        //or perform a actually usefull task
        System.Threading.Thread.Sleep(50);
    }
    bar.Done();
}
```

A waiting bar example

```c#
using (var bar = Loading.LoadingHelper.GetWaitingBar())
{
    bar.Start();
    TimeConsumingStuff();
    bar.Done();
}
```

### ⚙ (simple) Title Screen
The title screen simply will center a supplied application name in a ascii box. Messages below the box can be supplied and it can be paired with a loading or waiting bar.  

```c#
using (var title = TitleHelper.CreateTitlescreenFor("Superawsome Application!"))
{
    title.Show();
    Console.ReadKey();
    title.ChangeText("Pretty cool eh? Want to see a loading bar too? Press enter");
    Console.ReadKey();
    title.ChangeText("Here we go");
    using (var bar = Loading.LoadingHelper.GetLoadingBar())
    {
        bar.Start();
        title.ChangeText("Doing some important stuff");
        bar.SetPercentage(25);
        System.Threading.Thread.Sleep(50);
        title.ChangeText("Almost there ...");
        bar.SetPercentage(50);
        System.Threading.Thread.Sleep(500);
        bar.SetPercentage(80);
        title.ChangeText("Finishing up...");
        System.Threading.Thread.Sleep(800);
        bar.SetPercentage(100);
        title.ChangeText("...and we are ready to go!");
    }
    Console.ReadKey();
    title.Close();

}
```

**Screenshot of the example**
![Screenshot](assets/screenshot-titlescreen-loadingbar.png)


### ⚙ Console Extensions
It also ships with a handfull of convenience utility functions, please refer to the documentation section below

## Documentation

### Argument Binder

Namespace `sbkst.konzolR.Arguments`

The static method `ArgumentBinder.Create<T>` creates a new Argument Binder Setup, where one can define how the 
arguments are going to be bound on the supplied model T.

The Argument Setup supports `CreateFor` which creates a binding for boolean value, `CreateArgumentWithPayloadFor`
which supports binding against a boolean and a second string value which gets validated trough the supplied regex.
`CreatePathArgumentFor` binds against a boolean value and a string value, where the string value gets stripped of 
leading and trailing `"` if supplied.  `CreateArrayArgumentFor` binds to a boolean value and a string array,
the seperator for the second argument can be supplied, the default value is `,`. The `Build` Method creates the
final binder. The binder itself has a `Bind` method which tries to bind the supplied argument array to the defined 
model and a `help` method which displays a simple help.

### Dialog

Namespace `sbkst.konzolR.SimpleDialog`

A new dialog gets created with the static method `DialogHelper.Create<T>`, which takes the introduction text 
to the dialog as first argument, the dialog itself can then be composed with the `Ask` and `AskOptional` Methods.
The difference between `Ask` and `AskOptional` is that the user can skip optional ones and has to set a valid value
for the ask ones, also optional is bound against nullable types. `.Run` exectues the dialog and returns the given
answers as the object defined.

### Loading Bar & Waiting Bar

Namespace `sbkst.konzolR.Loading`

A new loading or waiting bar can be created with the static method `LoadingHelper.GetLoadingBar()` or `GetWaitingBar`. 
The difference between those two is, that the loading bar supports setting a fixed percentage, with the `SetPercentage()` method, (a.e.  classic progress bar)
while the waiting bar just supports `Start` and `Done` and will in between those two calls just keep filling itself up. 
Its used for unknown durations or progress.

### Title Screen

Namespace `sbkst.konzolR.TitleScreen`

Create a new `ITitleScreen` with the static method `TitleHelper.CreateTitlescreenFor`. The title screen object itself supports
`Show` which will display the title screen, `ChangeText` which will update the message below the title screen to the supplied text
and `Close` which will clear the the title screen.

### The Extensions

Namespace `sbkst.konzolR.Extensions`

`ConsoleExtensions.WriteLnLog(string text)` outputs text directly to the console with leading date and time
`ConsoleExtensions.WhiteOnBlack()` sets black background and white text
`ConsoleExtensions.BlackOnWhite()` sets white background and black text
`ConsoleExtensions.ReadMultiline()` will read console input until the `ESC` key is pressed

`TableizeToConsole` extends the ICollection interface and will try to print the given collection to the console 
as an ascii art table. it accepts a argument list of fields to set the columns. 
*warning* this wont do the prettiest output possible, but is fine for debugging and internal purposes,
or if you dont care if some table headers have ugly names