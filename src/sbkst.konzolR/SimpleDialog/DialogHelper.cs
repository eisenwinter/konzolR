using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sbkst.konzolR.SimpleDialog.OptionTypes;
using sbkst.konzolR.SimpleDialog.Internals;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
namespace sbkst.konzolR.SimpleDialog
{
    public static class DialogHelper
    {
        /// <summary>
        /// Create a new simple dialog object
        /// </summary>
        /// <typeparam name="T">type of item to be bound</typeparam>
        /// <param name="introduction">introduction text that will be displayed at the start</param>
        /// <returns></returns>
        public static ISimpleDialog<T> Create<T>(string introduction)
        {
            var dialog = new SimpleConsoleDialog<T>(introduction);
            return dialog;
        }

        /// <summary>
        /// Adding a question which expects a optional string as answer
        /// </summary>
        /// <typeparam name="T">bound type</typeparam>
        /// <param name="dialog">the dialog</param>
        /// <param name="field">field to be bound</param>
        /// <param name="question">the question</param>
        /// <returns>the dialog</returns>
        public static ISimpleDialog<T> AskOptional<T>(this ISimpleDialog<T> dialog, Expression<Func<T, string>> field, string question)
        {
            dialog.AddOption(new StringDialogOption<T>(field, question, dialog.Item, true));
            return dialog;
        }
        /// <summary>
        /// Adding a question which expects a optional string that matches the given regex as answer
        /// </summary>
        /// <typeparam name="T">bound type</typeparam>
        /// <param name="dialog">the dialog</param>
        /// <param name="field">field to be bound</param>
        /// <param name="question">the question</param>
        /// <param name="filter">regex</param>
        /// <returns>the dialog</returns>
        public static ISimpleDialog<T> AskOptional<T>(this ISimpleDialog<T> dialog, Expression<Func<T, string>> field, string question, Regex filter)
        {
            dialog.AddOption(new StringDialogOption<T>(field, question, dialog.Item, true, filter));
            return dialog;
        }


        /// <summary>
        /// Adding a question which expects a optional bool as answer
        /// </summary>
        /// <typeparam name="T">bound type</typeparam>
        /// <param name="dialog">the dialog</param>
        /// <param name="field">field to be bound</param>
        /// <param name="question">the question</param>
        /// <returns>the dialog</returns>
        public static ISimpleDialog<T> AskOptional<T>(this ISimpleDialog<T> dialog, Expression<Func<T, bool?>> field, string question)
        {
            dialog.AddOption(new OptBooleanDialogOption<T>(field, question, dialog.Item));
            return dialog;
        }

        /// <summary>
        /// Adding a question which expects a optional decimal as answer
        /// </summary>
        /// <typeparam name="T">bound type</typeparam>
        /// <param name="dialog">the dialog</param>
        /// <param name="field">field to be bound</param>
        /// <param name="question">the question</param>
        /// <returns>the dialog</returns>

        public static ISimpleDialog<T> AskOptional<T>(this ISimpleDialog<T> dialog, Expression<Func<T, decimal?>> field, string question)
        {
            dialog.AddOption(new OptDecimalDialogOption<T>(field, question, dialog.Item));
            return dialog;
        }

        /// <summary>
        /// Adding a question which expects a optional integer as answer
        /// </summary>
        /// <typeparam name="T">bound type</typeparam>
        /// <param name="dialog">the dialog</param>
        /// <param name="field">field to be bound</param>
        /// <param name="question">the question</param>
        /// <returns>the dialog</returns>

        public static ISimpleDialog<T> AskOptional<T>(this ISimpleDialog<T> dialog, Expression<Func<T, int?>> field, string question)
        {
            dialog.AddOption(new OptIntegerDialogOption<T>(field, question, dialog.Item));
            return dialog;
        }

        /// <summary>
        /// Adding a question which expects a string as answer
        /// </summary>
        /// <typeparam name="T">bound type</typeparam>
        /// <param name="dialog">the dialog</param>
        /// <param name="field">field to be bound</param>
        /// <param name="question">the question</param>
        /// <returns>the dialog</returns>

        public static ISimpleDialog<T> Ask<T>(this ISimpleDialog<T> dialog, Expression<Func<T, string>> field, string question)
        {
            dialog.AddOption(new StringDialogOption<T>(field, question, dialog.Item, false));
            return dialog;
        }

        /// <summary>
        /// Adding a question which expects a string that matches the given regex as answer
        /// </summary>
        /// <typeparam name="T">bound type</typeparam>
        /// <param name="dialog">the dialog</param>
        /// <param name="field">field to be bound</param>
        /// <param name="question">the question</param>
        /// <param name="filter">regex</param>
        /// <returns>the dialog</returns>
        public static ISimpleDialog<T> Ask<T>(this ISimpleDialog<T> dialog, Expression<Func<T, string>> field, string question, Regex filter)
        {
            dialog.AddOption(new StringDialogOption<T>(field, question, dialog.Item, false, filter));
            return dialog;
        }
        /// <summary>
        /// Adding a question which expects a bool as answer
        /// </summary>
        /// <typeparam name="T">bound type</typeparam>
        /// <param name="dialog">the dialog</param>
        /// <param name="field">field to be bound</param>
        /// <param name="question">the question</param>
        /// <returns>the dialog</returns>
        public static ISimpleDialog<T> Ask<T>(this ISimpleDialog<T> dialog, Expression<Func<T, bool>> field, string question)
        {
            dialog.AddOption(new BooleanDialogOption<T>(field, question, dialog.Item));
            return dialog;
        }
        /// <summary>
        /// Adding a question which expects a int as answer
        /// </summary>
        /// <typeparam name="T">bound type</typeparam>
        /// <param name="dialog">the dialog</param>
        /// <param name="field">field to be bound</param>
        /// <param name="question">the question</param>
        /// <returns>the dialog</returns>
        public static ISimpleDialog<T> Ask<T>(this ISimpleDialog<T> dialog, Expression<Func<T, int>> field, string question)
        {
            dialog.AddOption(new IntegerDialogOption<T>(field, question, dialog.Item));
            return dialog;
        }

        /// <summary>
        /// Adding a question which expects a decimal as answer
        /// </summary>
        /// <typeparam name="T">bound type</typeparam>
        /// <param name="dialog">the dialog</param>
        /// <param name="field">field to be bound</param>
        /// <param name="question">the question</param>
        /// <returns>the dialog</returns>

        public static ISimpleDialog<T> Ask<T>(this ISimpleDialog<T> dialog, Expression<Func<T, decimal>> field, string question)
        {
            dialog.AddOption(new DecimalDialogOption<T>(field, question, dialog.Item));
            return dialog;
        }
    }
}
