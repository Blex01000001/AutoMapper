using AutoMapper.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace AutoMapper.Extensions
{
    public static class TypeExtension
    {
        public static PropType GetPropType(this Type type)
        {
            if (type.IsArray)
            {
                return PropType.ArrayType;
            }
            else if (type.IsEnum)
            {
                return PropType.EnumType;
            }
            else if (type.GetInterfaces().Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IEnumerable<>)) && type != typeof(string))
            {
                return PropType.EnumerableType;
            }
            else if (type.IsClass && type != typeof(string))
            {
                return PropType.ObjectType;
            }
                return PropType.BasicType;
        }
    }
}
