using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Collections.ObjectModel;

namespace IronMock
{
    public class CallLog<T> : CallLog
    {
        /// <summary>
        /// Gets a count of how many times a method was called.
        /// </summary>
        /// <param name="methodName">Name of the method.</param>
        /// <returns>The count.</returns>
        public int GetCallCount(string methodName)
        {
            return this.Count(c => c.MethodName == methodName);
        }

        /// <summary>
        /// Gets a count of how many times a method was called.
        /// </summary>
        /// <param name="expression">An expression representing the call.</param>
        /// <returns>The count.</returns>
        public int GetCallCount(Expression<Action<T>> expression)
        {
            var callInfo = CallInfo.Create(expression);

            return this.Count(mc => mc.MethodName == callInfo.Name
                    && mc.Args.ListEqual(callInfo.Args));
        }

        /// <summary>
        /// Gets a count of how many times a property was set to the specified value.
        /// </summary>
        /// <param name="expression">An expression representing the property.</param>
        /// <param name="value">The value.</param>
        /// <returns><see cref="int"/></returns>
        public int GetPropertySetCount(Expression<Func<T, object>> expression, object value)
        {
            var callInfo = CallInfo.Create(expression);

            return this.Count(mc => mc.Args.Count() == 1 && mc.MethodName == callInfo.Name
                    && Equals(value, mc.Args.Single()));
        }

        /// <summary>
        /// Gets a count of how many times a property was set.
        /// </summary>
        /// <param name="expression">An expression representing the property.</param>
        /// <returns><see cref="int"/></returns>
        public int GetPropertySetCount(Expression<Func<T, object>> expression)
        {
            var callInfo = CallInfo.Create(expression);

            return this.Count(mc => mc.MethodName == callInfo.Name);
        }

        /// <summary>
        /// Gets a count of how many times a property getter was called.
        /// </summary>
        /// <param name="expression">An expression representing the call.</param>
        /// <returns>The count.</returns>
        public int GetPropertyGetCount<TPropertyType>(Expression<Func<T, TPropertyType>> expression)
        {
            return this.Count(mc => ((MemberExpression)expression.Body).Member.Name == mc.MethodName);
        }

        /// <summary>
        /// Gets records for all calls made to the specified method during testing.
        /// </summary>
        /// <returns>List of <see cref="CallRecord"/>.</returns>
        public IEnumerable<CallRecord> GetCallsFor(string methodName)
        {
            return this.Where(c => c.MethodName == methodName);
        }
    }
}
