using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq.Expressions;

namespace IronMock.Example
{
    /// <summary>
    /// Some example tests.
    /// </summary>
    [TestClass]
    public class ExampleTests
    {
        [TestMethod]
        public void TestIFuncTestDelegate()
        {
            var test = Mock.Create<IFuncTest>();

            // Set up the IFuncTest.RunTest method with a delegate to return 42.
            test.SetupMethod(t => t.RunTest(), () => 42);

            Assert.AreEqual(42, test.Object.RunTest());

            Assert.AreEqual(1, test.CallLog.GetCallCount(t => t.RunTest()));
        }

        [TestMethod]
        public void TestIFuncTestAction()
        {
            bool b = false;

            var test = Mock.Create<IFuncTest>();

            // Set up the IFuncTest.ActionTest method with a delegate to set a locally-scoped variable.
            test.SetupMethod(t => t.ActionTest(), () => b = true);
            test.Object.ActionTest();

            Assert.IsTrue(b);

            Assert.AreEqual(1, test.CallLog.GetCallCount(t => t.ActionTest()));
        }

        [TestMethod]
        public void TestIFuncTestReadonlyProperty()
        {
            var test = Mock.Create<IFuncTest>();

            // Set up the IFuncTest.ReadonlyValue property to return 101.
            test.SetupProperty(t => t.ReadonlyValue, 101);

            Assert.AreEqual(101, test.Object.ReadonlyValue);

            Assert.AreEqual(1, test.CallLog.GetPropertyGetCount(t => t.ReadonlyValue));
        }

        [TestMethod]
        public void TestIFuncTestReadWriteProperty()
        {
            var test = Mock.Create<IFuncTest>();

            // Set up the IFuncTest.Value property to return 101 on first access.
            test.SetupProperty(t => t.Value, 101);

            Assert.AreEqual(101, test.Object.Value);
            test.Object.Value = 69;
            Assert.AreEqual(69, test.Object.Value);
            Assert.AreEqual(1, test.CallLog.GetPropertySetCount(t => t.Value, 69));
        }

        [TestMethod]
        public void TestIFuncTestDefaultReadWriteProperty()
        {
            var test = Mock.Create<IFuncTest>();

            // Properties are automagically implemented.

            test.Object.Value = 69;
            Assert.AreEqual(69, test.Object.Value);
            Assert.AreEqual(1, test.CallLog.GetPropertySetCount(t => t.Value, 69));
        }

        [TestMethod]
        public void TestIFuncTestDefaultReadProperty()
        {
            var test = Mock.Create<IFuncTest>();

            // Automagically implemented properties return the default for their type.

            Assert.AreEqual(0, test.Object.Value);
            Assert.AreEqual(1, test.CallLog.GetPropertyGetCount(t => t.Value));
        }

        [TestMethod]
        public void TestIFuncTestDefaultFunctionCallCountWithCorrectArgument()
        {
            var test = Mock.Create<IFuncTest>();

            // Methods are automagically implemented.

            test.Object.DefaultTestX(42);

            // Check the number of times the method was called with 42.
            Assert.AreEqual(1, test.CallLog.GetCallCount(t => t.DefaultTestX(42)));
        }


        [TestMethod]
        public void TestIFuncTestDefaultFunctionCallCountWithWrongArgument()
        {
            var test = Mock.Create<IFuncTest>();

            test.Object.DefaultTestX(43);

            // GetCallCount only returns for the specified value...

            Assert.AreEqual(0, test.CallLog.GetCallCount(t => t.DefaultTestX(42)));
        }
    }
}
