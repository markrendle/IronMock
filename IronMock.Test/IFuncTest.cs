using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IronMock.Test
{
    public interface IFuncTest
    {
        int RunTest();
        int DefaultTestX(int arg);
        void ActionTest();
        int Value { get; set; }
        int ReadonlyValue { get; }
    }
}
