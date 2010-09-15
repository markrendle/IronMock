using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Microsoft.Scripting;

namespace IronMock
{
    public class MockProperty<T,TPropertyType> : IMockMember
    {
        private readonly string _name;
        private TPropertyType _value;

        public MockProperty(Expression<Func<T, TPropertyType>> expression, TPropertyType value)
        {
            var memberExpression = expression.Body as MemberExpression;
            if (memberExpression == null) throw new ArgumentException("MemberExpression expected");
            _name = memberExpression.Member.Name;
            _value = value;
        }

        public bool Matches(InvokeMemberBinder binder, object[] args)
        {
            return binder.Name == _name;
        }

        public object Invoke(params object[] args)
        {
            if (args.Length == 0) return _value;
            _value = (TPropertyType) args[0];
            return null;
        }

        public bool Matches(SetMemberBinder binder)
        {
            return binder.Name == _name;
        }
    }
}
