using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Scripting.Hosting;

namespace IronMock
{
    public class DuckTyper
    {
        const string Template =
@"class {0}
  include ::{1}

  def initialize(wrapped)
    @wrapped = wrapped
  end

  def method_missing(sym, *args, &block)
    @wrapped.send sym.ToString, *args, &block
  end
end";

        private static readonly ScriptEngine Engine = IronRuby.Ruby.CreateEngine();
        private static readonly Dictionary<Type, string> Types = new Dictionary<Type, string>();
        private static readonly object Sync = new object();

        /// <summary>
        /// Applies an interface to an object.
        /// </summary>
        /// <typeparam name="T">The interface to apply</typeparam>
        /// <param name="objectToWrap">The object to wrap.</param>
        /// <returns>A wrapper around the object which implements the interface.</returns>
        public static T ApplyInterface<T>(object objectToWrap)
            where T : class
        {
            if (objectToWrap == null) throw new ArgumentNullException("objectToWrap");
            if (!typeof(T).IsInterface) throw new ArgumentException("T must be an interface type.");

            PrepareType(typeof(T));

            return WrapObject<T>(objectToWrap);
        }

        private static T WrapObject<T>(object objectToWrap)
            where T : class
        {
            var scope = Engine.CreateScope();
            scope.SetVariable("object_to_wrap", objectToWrap);

            string newCode = Types[typeof(T)] + ".new(object_to_wrap)";
            return Engine.Execute(newCode, scope) as T;
        }

        private static void PrepareType(Type interfaceType)
        {
            if (Types.ContainsKey(interfaceType)) return;

            lock (Sync)
            {
                if (!Types.ContainsKey(interfaceType))
                {
                    Engine.Runtime.LoadAssembly(interfaceType.Assembly);
                    Engine.Execute(GenerateClassCode(interfaceType));
                    Types.Add(interfaceType, GenerateClassName(interfaceType));
                }
            }
        }

        private static string GenerateClassCode(Type interfaceType)
        {
            if (interfaceType == null) throw new ArgumentNullException("interfaceType");
            return string.Format(Template, GenerateClassName(interfaceType), interfaceType.FullName.Replace(".", "::"));
        }

        private static string GenerateClassName(Type interfaceType)
        {
            if (interfaceType == null) throw new ArgumentNullException("interfaceType");
            return interfaceType.FullName.Replace(".", "") + "DuckType";
        }
    }
}
