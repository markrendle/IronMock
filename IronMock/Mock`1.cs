using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Diagnostics;

namespace IronMock
{
    /// <summary>
    /// A wrapper for a dynamically generated implementation of an interface.
    /// </summary>
    /// <typeparam name="T">The interface to be implemented.</typeparam>
    public class Mock<T> : Mock
        where T : class
    {
        private readonly T _object;
        private readonly MockImplementer _implementer;

        internal Mock(MockImplementer implementer)
            : base(typeof(T))
        {
            _object = DuckTyper.ApplyInterface<T>(implementer);
            _implementer = implementer;
        }

        /// <summary>
        /// Gets the dynamically-generated implementation object.
        /// </summary>
        public T Object
        {
            get { return _object; }
        }

        /// <summary>
        /// Gets the call log.
        /// </summary>
        /// <value>The call log.</value>
        public CallLog<T> CallLog
        {
            get { return (CallLog<T>)_implementer.CallLog; }
        }

        /// <summary>
        /// Setups the method specified in the expression with a custom handler.
        /// </summary>
        /// <param name="expression">The expression representing the method.</param>
        /// <param name="handler">The handler.</param>
        public void SetupMethod(Expression<Action<T>> expression, Action handler)
        {
            _implementer.AddMockMember(new MockAction<T>(expression, handler));
        }

        /// <summary>
        /// Setups the method specified in the expression with a custom handler.
        /// </summary>
        /// <param name="expression">The expression representing the method.</param>
        /// <param name="handler">The handler.</param>
        public void SetupMethod<TReturn>(Expression<Func<T, TReturn>> expression, Func<TReturn> handler)
        {
            _implementer.AddMockMember(new MockFunc<T, TReturn>(expression, handler));
        }

        /// <summary>
        /// Setups the property specified in the expression with an initial value.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="expression">The expression representing the property.</param>
        /// <param name="value">The initial value.</param>
        public void SetupProperty<TProperty>(Expression<Func<T, TProperty>> expression, TProperty value)
        {
            _implementer.AddMockMember(new MockProperty<T, TProperty>(expression, value));
        }
    }
}
