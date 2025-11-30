using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace AutoMapper.TypesMapping
{
    internal class EnumerableType : ATypesMapping
    {
        public override object ConvertType(object sourceValue, Type targetType)
        {
            object targetValue;
            Type sourceType = sourceValue.GetType();
            sourceType = sourceValue.GetType().GetGenericArguments()[0];

            var typeDefinition = targetType.GetGenericTypeDefinition(); // 將List<int> 拆解為 List<T>
            Type argument = targetType.GetGenericArguments()[0];

            MethodInfo methodInfo;
            if (argument == typeof(int))
            {
                methodInfo = argument.GetMethod("Parse", new Type[] { sourceType });
            }
            else
            {
                //Mapper.Map
                var method = typeof(Mapper).GetMethod("NewMap", BindingFlags.Static | BindingFlags.Public);
                methodInfo = method.MakeGenericMethod(argument, null);
            }

            var targetListType = typeDefinition.MakeGenericType(argument);

            targetValue = Activator.CreateInstance(targetType, null);

            var methodAdd = targetListType.GetMethod("Add");

            foreach (var item in (IEnumerable)sourceValue)
            {
                methodAdd.Invoke(targetValue, new object[] { methodInfo.Invoke(null, new object[] { item }) });
            }
            return targetValue;
        }
    }
}
