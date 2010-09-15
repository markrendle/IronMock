using System.Collections;
using System.Collections.Generic;
using IronMock;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace IronMock.Test
{
    
    
    /// <summary>
    ///This is a test class for DuckTyperTest and is intended
    ///to contain all DuckTyperTest Unit Tests
    ///</summary>
    [TestClass()]
    public class DuckTyperTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for GenerateClassCode
        ///</summary>
        [TestMethod()]
        [DeploymentItem("IronMock.dll")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GenerateClassCodeTest()
        {
            DuckTyper_Accessor.GenerateClassCode(null);
        }

        /// <summary>
        ///A test for GenerateClassName
        ///</summary>
        [TestMethod()]
        [DeploymentItem("IronMock.dll")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GenerateClassNameTest()
        {
            DuckTyper_Accessor.GenerateClassName(null);
        }

        [TestMethod()]
        public void CreateIStringIndexedProxyTest()
        {
            var proxy = DuckTyper.ApplyInterface<IStringIndexed>(new Dictionary<string, object> { { "Foo", "Bar" } });

            Assert.IsNotNull(proxy);
            Assert.AreEqual("Bar", proxy["Foo"]);
        }

        [TestMethod()]
        public void CreateIThingWithLengthProxyTest()
        {
            var proxy = DuckTyper.ApplyInterface<IThingWithLength>("Foo");

            Assert.IsNotNull(proxy);
            Assert.AreEqual(3, proxy.Length);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateProxyWithNullShouldThrowException()
        {
            DuckTyper.ApplyInterface<IList>(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateProxyWithNonInterfaceTypeShouldThrowException()
        {
            DuckTyper.ApplyInterface<DuckTyperTest>(this);
        }
    }
}
