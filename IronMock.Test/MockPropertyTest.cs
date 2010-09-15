using IronMock;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq.Expressions;

namespace IronMock.Test
{
    
    
    /// <summary>
    ///This is a test class for MockPropertyTest and is intended
    ///to contain all MockPropertyTest Unit Tests
    ///</summary>
    [TestClass()]
    public class MockPropertyTest
    {
        /// <summary>
        ///A test for MockProperty`2 Constructor
        ///</summary>
        public void MockPropertyConstructorTestHelper<T, TPropertyType>()
        {
            Expression<Func<T, TPropertyType>> expression = t => default(TPropertyType);
            TPropertyType value = default(TPropertyType);
            var target = new MockProperty<T, TPropertyType>(expression, value);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void MockPropertyConstructorTest()
        {
            MockPropertyConstructorTestHelper<GenericParameterHelper, GenericParameterHelper>();
        }
    }
}
