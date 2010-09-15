using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Dynamic;
using System.Linq.Expressions;

namespace IronMock
{
    public class MockImplementer : DynamicObject
    {
        private readonly Type _type;
        private readonly List<IMockMember> _mockMembers = new List<IMockMember>();
        private readonly CallLog _callLog;
        private readonly Dictionary<string, object> _properties = new Dictionary<string, object>();

        public MockImplementer(Type type, CallLog callLog)
        {
            _type = type;
            _callLog = callLog;
        }

        /// <summary>
        /// Provides the implementation for operations that invoke a member. Classes derived from the <see cref="T:System.Dynamic.DynamicObject"/> class can override this method to specify dynamic behavior for operations such as calling a method.
        /// </summary>
        /// <param name="binder">Provides information about the dynamic operation. The binder.Name property provides the name of the member on which the dynamic operation is performed. For example, for the statement sampleObject.SampleMethod(100), where sampleObject is an instance of the class derived from the <see cref="T:System.Dynamic.DynamicObject"/> class, binder.Name returns "SampleMethod". The binder.IgnoreCase property specifies whether the member name is case-sensitive.</param>
        /// <param name="args">The arguments that are passed to the object member during the invoke operation. For example, for the statement sampleObject.SampleMethod(100), where sampleObject is derived from the <see cref="T:System.Dynamic.DynamicObject"/> class, <paramref name="args[0]"/> is equal to 100.</param>
        /// <param name="result">The result of the member invocation.</param>
        /// <returns>
        /// true if the operation is successful; otherwise, false. If this method returns false, the run-time binder of the language determines the behavior. (In most cases, a language-specific run-time exception is thrown.)
        /// </returns>
        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            _callLog.AddNew(binder.Name, args);

            var mockMember = _mockMembers.FirstOrDefault(mm => mm.Matches(binder, args));

            if (mockMember != null)
            {
                result = mockMember.Invoke(args);
                return true;
            }

            if (_properties.ContainsKey(binder.Name))
            {
                return base.TryInvokeMember(binder, args, out result);
            }

            Type returnType = null;
            var method = _type.GetMethod(binder.Name);
            if (method != null)
            {
                returnType = method.ReturnType;
            }
            else
            {
                var property = _type.GetProperty(binder.Name);
                if (property != null)
                {
                    returnType = property.PropertyType;
                }
            }

            result = returnType == null ? null : returnType.IsValueType ? Activator.CreateInstance(returnType) : null;

            return true;
        }

        /// <summary>
        /// Provides the implementation for operations that get member values. Classes derived from the <see cref="T:System.Dynamic.DynamicObject"/> class can override this method to specify dynamic behavior for operations such as getting a value for a property.
        /// </summary>
        /// <param name="binder">Provides information about the object that called the dynamic operation. The binder.Name property provides the name of the member on which the dynamic operation is performed. For example, for the Console.WriteLine(sampleObject.SampleProperty) statement, where sampleObject is an instance of the class derived from the <see cref="T:System.Dynamic.DynamicObject"/> class, binder.Name returns "SampleProperty". The binder.IgnoreCase property specifies whether the member name is case-sensitive.</param>
        /// <param name="result">The result of the get operation. For example, if the method is called for a property, you can assign the property value to <paramref name="result"/>.</param>
        /// <returns>
        /// true if the operation is successful; otherwise, false. If this method returns false, the run-time binder of the language determines the behavior. (In most cases, a run-time exception is thrown.)
        /// </returns>
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = _properties[binder.Name];
            return true;
        }

        /// <summary>
        /// Provides the implementation for operations that set member values. Classes derived from the <see cref="T:System.Dynamic.DynamicObject"/> class can override this method to specify dynamic behavior for operations such as setting a value for a property.
        /// </summary>
        /// <param name="binder">Provides information about the object that called the dynamic operation. The binder.Name property provides the name of the member to which the value is being assigned. For example, for the statement sampleObject.SampleProperty = "Test", where sampleObject is an instance of the class derived from the <see cref="T:System.Dynamic.DynamicObject"/> class, binder.Name returns "SampleProperty". The binder.IgnoreCase property specifies whether the member name is case-sensitive.</param>
        /// <param name="value">The value to set to the member. For example, for sampleObject.SampleProperty = "Test", where sampleObject is an instance of the class derived from the <see cref="T:System.Dynamic.DynamicObject"/> class, the <paramref name="value"/> is "Test".</param>
        /// <returns>
        /// true if the operation is successful; otherwise, false. If this method returns false, the run-time binder of the language determines the behavior. (In most cases, a language-specific run-time exception is thrown.)
        /// </returns>
        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            _callLog.AddNew(binder.Name, value);
            var mockMember = _mockMembers.FirstOrDefault(mm => mm.Matches(binder));

            if (mockMember != null)
            {
                mockMember.Invoke(value);
            }
            else
            {
                _properties[binder.Name] = value;
            }
            return true;
        }

        /// <summary>
        /// Adds a mock member.
        /// </summary>
        /// <param name="member">The member.</param>
        public void AddMockMember(IMockMember member)
        {
            _mockMembers.Add(member);
        }

        /// <summary>
        /// Gets the call log.
        /// </summary>
        /// <value>The call log.</value>
        public CallLog CallLog { get { return _callLog; } }
    }
}
