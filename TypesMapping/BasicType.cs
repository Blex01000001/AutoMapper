using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AutoMapper.TypesMapping
{
    internal class BasicType : ATypesMapping
    {
        public override object ConvertType(object sourceValue, Type targetType)
        {
            //轉string
            if (targetType == typeof(string)) return sourceValue.ToString();

            //轉int double...
            MethodInfo parseMethod = targetType.GetMethod("Parse", new Type[] { typeof(string) });
            return parseMethod.Invoke(null, new object[] { sourceValue.ToString() });
        }
    }
}
