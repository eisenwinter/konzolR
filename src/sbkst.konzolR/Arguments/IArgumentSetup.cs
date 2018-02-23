using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
namespace sbkst.konzolR.Arguments
{
    public interface IArgumentSetup<T> where T : class
    {

        /// <summary>
        /// creates a binding for a boolean argument (a.e. like -f)
        /// </summary>
        /// <param name="command">the command that should be bound  (a.e. -f)</param>
        /// <param name="helptext">the helptext provided for the argument (a.e. forces something)</param>
        /// <param name="field">the field on which it should be bound</param>
        /// <returns></returns>
        IArgumentSetup<T> CreateFor(string command, string helptext, Expression<Func<T, bool>> field);

        /// <summary>
        /// creates a binding for a two-part argument (a.e. -f something)
        /// the second argument is checked trough the supplied regex
        /// </summary>
        /// <param name="command">the command itself (a.e. -f)</param>
        /// <param name="helptext">helptext for the command</param>
        /// <param name="field">field on which the command switch is bound to</param>
        /// <param name="payload">field on which the second part is bound to</param>
        /// <param name="payloadFilter">regex to validate the second part</param>
        /// <returns></returns>
        IArgumentSetup<T> CreateArgumentWithPayloadFor(string command, string helptext, Expression<Func<T, bool>> field, Expression<Func<T, string>> payload, Regex payloadFilter);

    
        /// <summary>
        /// creates a binding for a path(or file) switch (a.e. -d "some.dat)
        /// it automatically removed the " wrapping
        /// </summary>
        /// <param name="command">the command itself (a.e. -d)</param>
        /// <param name="helptext">helptext for the given command</param>
        /// <param name="field">field on which the command switch is bound to</param>
        /// <param name="path">field on which the path is bound to</param>
        /// <returns></returns>
        IArgumentSetup<T> CreatePathArgumentFor(string command, string helptext, Expression<Func<T, bool>> field, Expression<Func<T, string>> path);

        /// <summary>
        /// creates a string array out of a two-part argument (a.e -addr a1,bc,ca)
        /// the seperator can be specified, it defaults to ,
        /// </summary>
        /// <param name="command">the command itself (a.e. -addr)</param>
        /// <param name="helptext">helptext for command</param>
        /// <param name="field">field on whihc the command switch is bound to</param>
        /// <param name="target">the array for the binding</param>
        /// <param name="seperator">the seperator</param>
        /// <returns></returns>
        IArgumentSetup<T> CreateArrayArgumentFor(string command, string helptext, Expression<Func<T, bool>> field, Expression<Func<T, string[]>> target, char seperator = ',');

        /// <summary>
        /// builds the argumentbinder after setup is done 
        /// its ready to use after this
        /// </summary>
        /// <returns></returns>
        ICommandLineArgumentBinder<T> Build();
    }


}
