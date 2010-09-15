using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IronMock.Test
{
    public interface IStringIndexed
    {
        object this[string key] { get; }
    }
}
