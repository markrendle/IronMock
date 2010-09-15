using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Microsoft.Scripting.Hosting;
using System.Dynamic;

namespace IronMock
{
    /// <summary>
    /// Provides the Create method for mocking types.
    /// </summary>
    public abstract class Mock
    {
        private readonly Type _type;

        protected Mock(Type type)
        {
            _type = type;
        }

        /// <summary>
        /// Creates a Mock object of the specified type.
        /// </summary>
        /// <typeparam name="T">The interface the Mock should implement</typeparam>
        /// <returns>A <see cref="Mock`1"/> object.</returns>
        public static Mock<T> Create<T>()
            where T : class
        {
            if (!typeof(T).IsInterface) throw new ArgumentException("T must be an interface type.");

            var impl = new MockImplementer(typeof(T), new CallLog<T>());
            return new Mock<T>(impl);
        }

        /// <summary>
        /// Gets the type being implemented.
        /// </summary>
        public Type Type
        {
            get { return _type; }
        }
    }
}
