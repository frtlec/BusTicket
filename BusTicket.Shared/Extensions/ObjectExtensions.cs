using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BusTicket.Shared.Extensions
{
    public static class ObjectExtensions
    {
        public static T FindProperty<T>(this object obj,string propName)
        {
            Type t = obj.GetType();
            PropertyInfo statuProp = t.GetProperty(propName);
            if (statuProp==null)
                return default;
            return (T)statuProp.GetValue(obj);
        }
    }
}
