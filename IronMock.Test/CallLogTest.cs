using IronMock;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IronMock.Test
{
    
    
    /// <summary>
    ///This is a test class for CallLogTest and is intended
    ///to contain all CallLogTest Unit Tests
    ///</summary>
    [TestClass()]
    public class CallLogTest
    {
        internal virtual CallLog CreateCallLog()
        {
            return new CallLog<IFuncTest>();
        }

        /// <summary>
        ///A test for AddNew
        ///</summary>
        [TestMethod()]
        public void AddNewTestWithNoArgs()
        {
            CallLog target = CreateCallLog();
            string name = "Test";
            target.AddNew(name);
            Assert.AreEqual(name, target[0].MethodName);
        }

        /// <summary>
        ///A test for AddNew
        ///</summary>
        [TestMethod()]
        public void AddNewTestWithSingleArg()
        {
            CallLog target = CreateCallLog();
            string name = "Test";
            object arg = 1;
            target.AddNew(name, arg);
            Assert.AreEqual(name, target[0].MethodName);
            Assert.AreEqual(arg, target[0].Args.Single());
        }

        /// <summary>
        ///A test for AddNew
        ///</summary>
        [TestMethod()]
        public void AddNewTestWithTwoArgs()
        {
            CallLog target = CreateCallLog();
            string name = "Test";
            object[] args = new object[] {1, 2};
            target.AddNew(name, args);
            Assert.AreEqual(name, target[0].MethodName);
            Assert.AreEqual(args[0], target[0].Args.First());
            Assert.AreEqual(args[1], target[0].Args.Last());
        }
    }
}
