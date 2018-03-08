using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using sbkst.konzolR.Arguments;
using sbkst.konzolR.Extensions;
using sbkst.konzolR.TitleScreen;
namespace sbkst.konzolR.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            //tinkering arround with a simple console t-ui window framework (WIP)
            //so far it prints windows regarding to their zindex
            //the ui context houses a "canvas" wich does the actual rendering of the view port on 
            //a seperate console screen buffer and keeps track with matrix of changes and will try to only update
            //the canvas where needed - this is all very wip / tinkering
            using (Ui.UiContext test = new Ui.UiContext())
            {
                test.Initialize(ConsoleColor.Blue);
                test.AddWindow(new Ui.ConsoleWindow("Testwindow", "test-id", new Ui.Layout.Size(13, 5), new Ui.Layout.Position(2, 2)));
                test.GetWindow("test-id").Keys.On(ConsoleKey.Y, (window) =>
                {
                    window.ChangeBackgroundColor(ConsoleColor.Yellow);
                    return true;
                });
                test.GetWindow("test-id").Keys.On(ConsoleKey.R, (window) =>
                {
                    window.ChangeBackgroundColor(ConsoleColor.Red);
                    return true;
                });
                test.Keys.On(ConsoleKey.Q, ConsoleModifiers.Control, (ctx) =>
                 {
                     ctx.UnhookInputlook();
                     return true;
                 });

                test.HookInputLoop();

                var boundObject = new ABC
                {
                    A = "Testinput"
                };
                test.GetWindow("test-id").AddControl(new Ui.Controls.BoundTextboxControl<ABC>("abc", boundObject, bind => bind.A));
                test.GetWindow("test-id").AddControl(new Ui.Controls.ConsoleButton("ok-button", "Ok", () =>
                  {
                      test.RemoveWindow("test-id");
                  }));


                test.AddWindow(new Ui.ConsoleWindow("Hello!", "overlapping-id", new Ui.Layout.Size(9, 10), new Ui.Layout.Position(3, 3)));
                test.GetWindow("overlapping-id").ChangeBackgroundColor(ConsoleColor.Green);
                test.GetWindow("overlapping-id").AddControl(new Ui.Controls.ConsoleLabel("nameLabel", "Name", ConsoleColor.Green));
                test.GetWindow("overlapping-id").AddControl(new Ui.Controls.ConsoleTextbox("nameTextbox", "John Doe"));
                test.GetWindow("overlapping-id").AddControl(new Ui.Controls.ConsoleLabel("fromLabel", "From", ConsoleColor.Green));
                test.GetWindow("overlapping-id").AddControl(new Ui.Controls.ConsoleTextbox("fromTextbox", "Nowhere"));
                test.GetWindow("overlapping-id").Keys.WithFocusOn(ConsoleKey.N, (window) =>
                 {
                     test.MaximizeWindow(window.Id);
                     return true;
                 });
                test.GetWindow("overlapping-id").Keys.WithFocusOn(ConsoleKey.R, (window) =>
                {
                    test.RestoreWindowSize(window.Id);
                    return true;
                });
                test.HookInputLoop();

                //test.GetWindow("overlapping-id").ChangeBackgroundColor(ConsoleColor.DarkBlue);
                //Console.ReadKey();

                //test.Focus("test-id");
                //Console.ReadKey();
                //test.Focus("overlapping-id");
                //Console.ReadKey();
                //test.RemoveWindow("overlapping-id");
                //Console.ReadKey();
                //test.GetWindow("test-id").AddControl(new Ui.Controls.InfotextControl("infotest", "Hello there!"));
                //Console.ReadKey();

            }


            //title screen demo
            using (var title = TitleHelper.CreateTitlescreenFor("Superawsome Application!"))
            {
                title.Show();
                Console.ReadKey();
                title.ChangeText("Pretty cool eh? Want to see a loading bar too?");
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
            //do something with args
            Console.ReadKey();


            //some extensions
            Console.WriteLine("Extensions Demo");
            ConsoleExtensions.BlackOnWhite();
            List<ABC> dummyData = new List<ABC>
            {
                 new ABC
                 {
                      A = "Hello there",
                      B = 1,
                      C = "High-Ground"
                 },
                  new ABC
                 {
                      A = "General Kenobi",
                      B = -1,
                      C = "Low-Ground"
                 }
            };
            dummyData.TableizeToConsole(c => c.A, c => c.C, c => c.B);
            Console.ResetColor();
            Console.WriteLine("Enter your multiline text, press ESC once finished");
            string s = ConsoleExtensions.ReadMultiline();
            Console.WriteLine("Your wrote: {0}", s);
            ConsoleExtensions.WriteLnLog("Look at me I know the time!");

            //bar demos
            Console.WriteLine("Waiting bar demo - press enter twice");
            using (var bar = Loading.LoadingHelper.GetWaitingBar())
            {
                bar.Start();
                Console.ReadKey();
                bar.Done();
                Console.ReadKey();
            }


            Console.WriteLine("Loading bar demo");
            using (var bar = Loading.LoadingHelper.GetLoadingBar())
            {
                for (short i = 0; i <= 100; i++)
                {
                    bar.SetPercentage(i);
                    System.Threading.Thread.Sleep(50);
                }
                bar.Done();
            }


        }
    }
}
