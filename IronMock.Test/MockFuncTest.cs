using IronMock;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq.Expressions;
using System.Dynamic;

namespace IronMock.Test
{
    
    
    /// <summary>
    ///This is a test class for MockFuncTest and is intended
    ///to contain all MockFuncTest Unit Tests
    ///</summary>
    [TestClass()]
    public class MockFuncTest
    {
        /// <summary>
        ///A test for Matches
        ///</summary>
        public void MatchesTestHelper<TInterface>()
        {
            Expression<Func<TInterface, int>> expression = t => t.GetHashCode();
            var target = new MockFunc<TInterface, int>(expression, () => 42);
            SetMemberBinder binder = null;
            const bool expected = false;
            bool actual = target.Matches(binder);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void MatchesTest()
        {
            MatchesTestHelper<GenericParameterHelper>();
        }

        /// <summary>
        ///A test for MockFunc`2 Constructor
        ///</summary>
        public void MockFuncConstructorTestHelper<TInterface, TReturn>()
        {
            Expression<Func<TInterface, TReturn>> expression = t => default(TReturn);
            Func<TReturn> func = () => default(TReturn);
            MockFunc<TInterface, TReturn> target = new MockFunc<TInterface, TReturn>(expression, func);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void MockFuncConstructorTest()
        {
            MockFuncConstructorTestHelper<GenericParameterHelper, GenericParameterHelper>();
        }
    }
}
