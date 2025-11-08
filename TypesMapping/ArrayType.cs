using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace AutoMapper.TypesMapping
{
    internal class ArrayType : ATypesMapping
    {
        public override object ConvertType(object sourceValue, Type targetType)
        {
            Type targetElementType = targetType.GetElementType();
            object targetValue;
            Array sourceValueArr = (Array)sourceValue;
            var sourceElementType = sourceValue.GetType().GetElementType();
            int sourceLength = sourceValueArr.Length;

            targetValue = Array.CreateInstance(targetType.GetElementType(), sourceLength);

            var parseMethod = targetElementType.GetMethod("Parse", new Type[] { sourceElementType });
            var methodSetValue = targetType.GetMethod("SetValue", new Type[] { typeof(Object), typeof(int) });

            for (int i = 0; i < sourceLength; i++)
            {
                var tempSour = parseMethod.Invoke(null, new object[] { sourceValueArr.GetValue(i) });
                methodSetValue.Invoke(targetValue, new object[] { tempSour, i });
            }
            return targetValue;
        }
    }
}
