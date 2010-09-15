using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Microsoft.Scripting;

namespace IronMock
{
    public class MockFunc<TInterface, TReturn> : IMockMember
    {
        private readonly string _name;
        private readonly object[] _arguments;
        private readonly Delegate _delegate;

        public MockFunc(Expression<Func<TInterface, TReturn>> expression, Func<TReturn> func)
        {
            var methodCallExpression = expression.Body as MethodCallExpression;
            if (methodCallExpression == null) throw new ArgumentException("MethodCallExpression expected");
            _name = methodCallExpression.Method.Name;
            _arguments = methodCallExpression.Arguments.Cast<ConstantExpression>().Select(expr => expr.Value).ToArray();
            _delegate = func;
        }

        /// <summary>
        /// Tests whether the method signature of the binder matches the signature of the mocked member.
        /// </summary>
        /// <param name="binder">The binder.</param>
        /// <param name="args">The args.</param>
        /// <returns>
        /// 	<c>true</c> if the signatures match; otherwise, <c>false</c>.
        /// </returns>
        public bool Matches(InvokeMemberBinder binder, object[] args)
        {
            return binder.Name == _name
                && !args.Where((arg, index) => !Equals(arg, _arguments[index])).Any();
        }

        /// <summary>
        /// Invokes the mocking delegate with the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns>
        /// The value returned by the delegate.
        /// </returns>
        public object Invoke(params object[] args)
        {
            return _delegate.DynamicInvoke(args);
        }


        /// <summary>
        /// Overridden: always returns false.
        /// </summary>
        /// <param name="binder">The binder.</param>
        /// <returns>
        /// 	<c>false</c>.
        /// </returns>
        public bool Matches(SetMemberBinder binder)
        {
            return false;
        }
    }
}
