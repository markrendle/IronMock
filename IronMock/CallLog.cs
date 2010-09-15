using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace IronMock
{
    /// <summary>
    /// Provides a log of calls made to a Mock object during a test.
    /// </summary>
    public abstract class CallLog : Collection<CallRecord>
    {
        /// <summary>
        /// Adds a new method call item.
        /// </summary>
        /// <param name="name">The name of the method/member that was invoked.</param>
        public void AddNew(string name)
        {
            Add(new CallRecord(name, Enumerable.Empty<object>()));
        }

        /// <summary>
        /// Adds a new method call item.
        /// </summary>
        /// <param name="name">The name of the method/member that was invoked.</param>
        /// <param name="arg">The value of the argument that was passed.</param>
        public void AddNew(string name, object arg)
        {
            Add(new CallRecord(name, new[] { arg }));
        }

        /// <summary>
        /// Adds a new method call item.
        /// </summary>
        /// <param name="name">The name of the method/member that was invoked.</param>
        /// <param name="args">The values of the arguments that were passed.</param>
        public void AddNew(string name, IEnumerable<object> args)
        {
            Add(new CallRecord(name, args));
        }
    }
}
