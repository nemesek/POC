using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ExpressionConverter.Providers
{
    internal static class TypeSystem
    {
        private static Type FindIEnumerable(Type seqType)
        {
            while (true)
            {
                if (seqType == null || seqType == typeof (string)) return null;
                if (seqType.IsArray) return typeof (IEnumerable<>).MakeGenericType(seqType.GetElementType());
                if (seqType.IsGenericType)
                {
                    foreach (var ienum in seqType.GetGenericArguments().Select(arg => typeof (IEnumerable<>).MakeGenericType(arg)).Where(ienum => ienum.IsAssignableFrom(seqType)))
                    {
                        return ienum;
                    }
                }

                Type[] ifaces = seqType.GetInterfaces();
                if (ifaces.Length > 0)
                {
                    foreach (var ienum in ifaces.Select(FindIEnumerable).Where(ienum => ienum != null))
                    {
                        return ienum;
                    }
                }

                if (seqType.BaseType == null || seqType.BaseType == typeof (object)) return null;
                seqType = seqType.BaseType;
            }
        }

        internal static Type GetSequenceType(Type elementType)
        {
            return typeof(IEnumerable<>).MakeGenericType(elementType);
        }
        internal static Type GetElementType(Type seqType)
        {
            Type ienum = FindIEnumerable(seqType);
            return ienum == null ? seqType : ienum.GetGenericArguments()[0];
        }
        internal static bool IsNullableType(Type type)
        {
            return type != null && type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }
        internal static bool IsNullAssignable(Type type)
        {
            return !type.IsValueType || IsNullableType(type);
        }
        internal static Type GetNonNullableType(Type type)
        {
            return IsNullableType(type) ? type.GetGenericArguments()[0] : type;
        }

        internal static Type GetMemberType(MemberInfo mi)
        {
            var fi = mi as FieldInfo;
            if (fi != null) return fi.FieldType;
            var pi = mi as PropertyInfo;
            if (pi != null) return pi.PropertyType;
            var ei = mi as EventInfo;
            return ei?.EventHandlerType;
        }
    }
}
