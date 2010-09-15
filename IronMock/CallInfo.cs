using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace IronMock
{
    public class CallInfo
    {
        private readonly string _name;
        private readonly object[] _args;

        private CallInfo(string name, object[] args)
        {
            _name = name;
            _args = args;
        }

        /// <summary>
        /// Gets the arguments passed to the method/property.
        /// </summary>
        /// <value>The args.</value>
        public object[] Args
        {
            get { return _args; }
        }

        /// <summary>
        /// Gets the name of the method/property.
        /// </summary>
        /// <value>The name.</value>
        public string Name
        {
            get { return _name; }
        }

        public static CallInfo Create<T>(Expression<Action<T>> expression)
        {
            var methodCallExpression = expression.Body as MethodCallExpression;
            if (methodCallExpression == null) throw new ArgumentException("Invalid expression.");

            return new CallInfo(methodCallExpression.Method.Name, methodCallExpression.Arguments.Cast<ConstantExpression>().Select(expr => expr.Value).ToArray());
        }

        public static CallInfo Create<T>(Expression<Func<T,object>> expression)
        {
            var unaryExpression = expression.Body as UnaryExpression;
            if (unaryExpression == null) throw new ArgumentException("Invalid expression.");
            var operandExpression = unaryExpression.Operand as MemberExpression;
            if (operandExpression == null) throw new ArgumentException("Invalid expression.");

            return new CallInfo(operandExpression.Member.Name, new object[0]);
        }
    }
}
