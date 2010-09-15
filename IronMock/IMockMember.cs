using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Dynamic;

namespace IronMock
{
    /// <summary>
    /// Defines an Interface for delegate-based member mocks.
    /// </summary>
    public interface IMockMember
    {
        /// <summary>
        /// Tests whether the method signature of the binder matches the signature of the mocked member.
        /// </summary>
        /// <param name="binder">The binder.</param>
        /// <param name="args">The args.</param>
        /// <returns><c>true</c> if the signatures match; otherwise, <c>false</c>.</returns>
        bool Matches(InvokeMemberBinder binder, params object[] args);

        /// <summary>
        /// Tests whether the method signature of the property setting binder matches the signature of the mocked member.
        /// </summary>
        /// <param name="binder">The binder.</param>
        /// <returns><c>true</c> if the signatures match; otherwise, <c>false</c>.</returns>
        bool Matches(SetMemberBinder binder);

        /// <summary>
        /// Invokes the mocking delegate with the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns>The value returned by the delegate, if any.</returns>
        object Invoke(params object[] args);
    }
}
