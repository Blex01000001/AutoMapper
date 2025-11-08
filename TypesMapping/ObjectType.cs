using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AutoMapper.TypesMapping
{
    internal class ObjectType : ATypesMapping
    {
        public override object ConvertType(object sourceValue, Type targetType)
        {
            object targetValue;
            var method = typeof(Mapper).GetMethod("NewMap", BindingFlags.Static | BindingFlags.Public);
            var mapperMethod = method.MakeGenericMethod(targetType);
            targetValue = mapperMethod.Invoke(null, new object[] { sourceValue });
            return targetValue;
        }
    }
}
