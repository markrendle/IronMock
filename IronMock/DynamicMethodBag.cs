using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Dynamic;

namespace Dynamism.Proxy
{
    public class DynamicMethodBag : DynamicObject
    {
        private readonly Dictionary<string, Delegate> _delegates;
        private readonly Dictionary<string, object> _properties = new Dictionary<string, object>();

        public DynamicMethodBag(Dictionary<string, Delegate> delegates, Dictionary<string, object> properties)
        {
            _delegates = delegates;
            _properties = properties ?? new Dictionary<string, object>();
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            if (_delegates.ContainsKey(binder.Name))
            {
                result = _delegates[binder.Name].DynamicInvoke(args);
                return true;
            }

            return base.TryInvokeMember(binder, args, out result);
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            if (_properties.ContainsKey(binder.Name))
            {
                result = _properties[binder.Name];
                return true;
            }

            return base.TryGetMember(binder, out result);
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            _properties[binder.Name] = value;
            return true;
        }
    }
}
