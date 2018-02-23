using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace sbkst.konzolR.Extensions
{
    public static class ConsoleExtensions
    {
        /// <summary>
        /// writes output with leading date time
        /// </summary>
        /// <param name="text"></param>
        public static void WriteLnLog(string text)
        {
            Console.WriteLine(String.Format("[{0} {1}] {2}", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(),text));
        }

        /// <summary>
        /// black background - white text
        /// </summary>
        public static void WhiteOnBlack()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// white background - black text
        /// </summary>

        public static void BlackOnWhite()
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
        }

        /// <summary>
        /// reads multiline input, finish with escape
        /// </summary>
        /// <returns></returns>
        public static string ReadMultiline()
        {
            StringBuilder sb = new StringBuilder();
            var key = Console.ReadKey();
            while (key.Key != ConsoleKey.Escape)
            {
                if(key.Key == ConsoleKey.Enter)
                {
                    Console.CursorTop += 1;
                    sb.Append(Environment.NewLine);
                }
                else
                {
                    sb.Append(key.KeyChar);
                }
                key = Console.ReadKey();
            }
            return sb.ToString();
        }

        /// <summary>
        /// creates a table from the given object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection">collection to be displayed</param>
        /// <param name="field">fields to print out</param>
        public static void TableizeToConsole<T>(this ICollection<T> collection, params Expression<Func<T, IConvertible>>[] field)
        {
            if (!collection.Any())
            {
                Console.WriteLine("#".PadLeft(20) + " No Results " + "#".PadLeft(20));
            }
            else
            {
                if (!field.Any())
                {
                    Console.WriteLine("No output fields selected!");
                    return;
                }

                StringBuilder sb = new StringBuilder();
                List<int> widths = new List<int>();
                foreach (var f in field)
                {
                    var cast = f.Body.ToString();
                    var compiled = f.Compile();
                    int max = collection.Select(s => compiled(s).ToString()).Max(s => s.Length);
                    if (max < cast.Length)
                    {
                        max = cast.Length;
                    }
                    sb.Append(" | " + cast.PadLeft(max));
                    widths.Add(max);
                }
                sb.AppendLine(" |");
                string lineSeperator = " |".PadRight(sb.Length - 3, '=') + "|";
                sb.AppendLine(lineSeperator);
                foreach (var d in collection)
                {
                    int col = 0;
                    foreach (var f in field)
                    {
                        var cast = f.Compile();
                        string val = cast(d).ToString();
                        sb.Append(" | " + val.PadLeft(widths[col]));
                        col++;
                    }
                    sb.AppendLine(" |");
                }
                sb.AppendLine(lineSeperator);
                Console.WriteLine(lineSeperator);
                Console.WriteLine(sb);

            }
        }

    }
}
