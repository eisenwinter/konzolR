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
