using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IronMock
{
    /// <summary>
    /// Provides information about calls made to a method on a <see cref="Mock`1"/> object.
    /// </summary>
    public class CallRecord
    {
        private readonly string _methodName;
        private readonly object[] _args;
        private readonly DateTime _timestamp;

        /// <summary>
        /// Initializes a new instance of the <see cref="CallRecord"/> class.
        /// </summary>
        /// <param name="methodName">Name of the method that was called.</param>
        /// <param name="args">The arguments passed to the method.</param>
        public CallRecord(string methodName, IEnumerable<object> args) : this(methodName, args, DateTime.Now)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CallRecord"/> class.
        /// </summary>
        /// <param name="methodName">Name of the method that was called.</param>
        /// <param name="args">The arguments passed to the method.</param>
        public CallRecord(string methodName, IEnumerable<object> args, DateTime timestamp)
        {
            _methodName = methodName;
            _timestamp = timestamp;
            _args = args.ToArray();
        }

        /// <summary>
        /// Gets the time the method was called.
        /// </summary>
        public DateTime Timestamp
        {
            get { return _timestamp; }
        }

        /// <summary>
        /// Gets the arguments passed to the method.
        /// </summary>
        public IEnumerable<object> Args
        {
            get { return _args.AsEnumerable(); }
        }

        /// <summary>
        /// Gets the name of the method that was called.
        /// </summary>
        public string MethodName
        {
            get { return _methodName; }
        }
    }
}
