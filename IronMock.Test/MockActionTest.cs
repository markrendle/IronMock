using IronMock;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq.Expressions;

namespace IronMock.Test
{
    
    
    /// <summary>
    ///This is a test class for MockActionTest and is intended
    ///to contain all MockActionTest Unit Tests
    ///</summary>
    [TestClass()]
    public class MockActionTest
    {
        /// <summary>
        ///A test for MockAction`1 Constructor
        ///</summary>
        public void MockActionConstructorTestHelper<T>()
        {
            Action a = () => "".GetHashCode();
            Expression<Action<T>> expression = t => a();
            MockAction<T> target = new MockAction<T>(expression, () => { });
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void MockActionConstructorTest()
        {
            MockActionConstructorTestHelper<GenericParameterHelper>();
        }
    }
}
