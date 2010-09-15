using IronMock;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq.Expressions;

namespace IronMock.Test
{
    
    
    /// <summary>
    ///This is a test class for CallInfoTest and is intended
    ///to contain all CallInfoTest Unit Tests
    ///</summary>
    [TestClass()]
    public class CallInfoTest
    {
        /// <summary>
        ///A test for Create
        ///</summary>
        public void CreateFuncTestNotUnaryHelper<T>()
        {
            Expression<Func<T, object>> expression = t => "";
            CallInfo.Create(expression);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateFuncTestNotUnary()
        {
            CreateFuncTestNotUnaryHelper<GenericParameterHelper>();
        }

        /// <summary>
        ///A test for Create
        ///</summary>
        public void CreateFuncTestNotMemberHelper<T>()
        {
            Expression<Func<T, object>> expression = t => t.GetHashCode();
            CallInfo.Create(expression);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateFuncTest()
        {
            CreateFuncTestNotMemberHelper<GenericParameterHelper>();
        }

        /// <summary>
        ///A test for Create
        ///</summary>
        public void CreateActionTestHelper<T>()
        {
            // Chuck a lambda in there.
            Action a = () => "".GetHashCode();
            Expression<Action<T>> expression = t => a();
            CallInfo.Create(expression);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateActionTest()
        {
            CreateActionTestHelper<GenericParameterHelper>();
        }
    }
}
