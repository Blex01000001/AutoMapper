using AutoMapper.Enums;
using AutoMapper.Extensions;
using AutoMapper.TypesMapping;
using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace AutoMapper
{
    public class Mapper
    {
        public Mapper() { }
        public static T Map<T>(object dao) where T : new()
        {


            var daoProps = dao.GetType().GetProperties();
            T dto = new T();

            foreach (PropertyInfo prop in daoProps)
            {
                var dtoPprop = typeof(T).GetProperty(prop.Name);
                if (dtoPprop == null)
                    continue;
                var daoValue = prop.GetValue(dao);

                bool isEnumerable = daoValue.GetType().GetInterfaces().Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IEnumerable<>));

                Type destType = dtoPprop.PropertyType;
                Type destElementType = destType.GetElementType();
                Type sourceType = daoValue.GetType();

                object dtovalue = daoValue;

                if (dtoPprop.PropertyType != prop.PropertyType)
                {
                    if (dtoPprop.PropertyType.IsEnum)
                    {
                        dtovalue = Enum.Parse(dtoPprop.PropertyType, daoValue.ToString());
                    }
                    else if (prop.PropertyType.IsEnum && dtoPprop.PropertyType == typeof(int))
                    {
                        dtovalue = (int)daoValue;
                    }
                    else if (dtoPprop.PropertyType == typeof(double) || dtoPprop.PropertyType == typeof(int))
                    {
                        MethodInfo parse = dtoPprop.PropertyType.GetMethod("Parse", new Type[] { typeof(string) });
                        dtovalue = parse.Invoke(null, new object[] { daoValue.ToString() });
                    }
                    else if (dtoPprop.PropertyType == typeof(string))
                    {
                        dtovalue = daoValue.ToString();
                    }
                    else if (sourceType.IsArray && destType.IsArray)
                    {
                        var sourceElementType = sourceType.GetElementType();
                        Array sourceValue = (Array)daoValue;
                        int sourceLength = sourceValue.Length;

                        dtovalue = Array.CreateInstance(destType.GetElementType(), sourceLength);

                        var parseMethod = destElementType.GetMethod("Parse", new Type[] { sourceElementType });
                        var methodSetValue = destType.GetMethod("SetValue", new Type[] { typeof(Object), typeof(int) });

                        for (int i = 0; i < sourceLength; i++)
                        {
                            var tempSour = parseMethod.Invoke(null, new object[] { sourceValue.GetValue(i) });
                            methodSetValue.Invoke(dtovalue, new object[] { tempSour, i });
                        }
                    }
                    else if (daoValue.GetType() != typeof(string) && isEnumerable)
                    {
                        sourceType = daoValue.GetType().GetGenericArguments()[0];


                        Type targetType = destType;

                        var typeDefinition = targetType.GetGenericTypeDefinition(); // 將List<int> 拆解為 List<T>
                        Type argument = targetType.GetGenericArguments()[0];

                        MethodInfo methodInfo;
                        if (argument == typeof(int))
                        {
                            Console.WriteLine("methodInfo Parse");
                            methodInfo = argument.GetMethod("Parse", new Type[] { sourceType });
                        }
                        else
                        {
                            //Mapper.Map
                            Console.WriteLine("methodInfo Map");
                            var method = typeof(Mapper).GetMethod("Map", BindingFlags.Static | BindingFlags.Public);
                            methodInfo = method.MakeGenericMethod(argument);
                        }

                        var targetListType = typeDefinition.MakeGenericType(argument);

                        dtovalue = Activator.CreateInstance(targetType, null);

                        var methodAdd = targetListType.GetMethod("Add");

                        foreach (var item in (IEnumerable)daoValue)
                        {
                            methodAdd.Invoke(dtovalue, new object[] { methodInfo.Invoke(null, new object[] { item }) });
                        }
                    }
                    else if (dtoPprop.GetType().IsClass && prop.PropertyType.IsClass)
                    {
                        //StatusDao -> StatusDto
                        //物件 -> 物件
                        var method = typeof(Mapper).GetMethod("Map", BindingFlags.Static | BindingFlags.Public);
                        var mapperMethod = method.MakeGenericMethod(dtoPprop.PropertyType);
                        dtovalue = mapperMethod.Invoke(null, new object[] { daoValue });
                    }
                }
                // Convert.ChangeType(dtovalue,)

                // int char float double boolean enum decimal long short

                // int,double,float,decimal,long,short -> string
                // string -> int,double,float,decimal,long,short


                //HW:
                //1.嘗試將其他類型轉成字串 (ToString)
                //2.研究看看 enum 對轉 (能處 enum 轉數字 / 數字轉 enum,再進階一點，字串轉enum)
                //20251019
                //3.list對轉
                //20251026
                //4.Array對轉
                //5.物件轉物件
                //20251102
                //
                //

                dtoPprop.SetValue(dto, dtovalue);
            }
            return dto;
        }



        public static T NewMap<T>(object dao) where T : new()
        {
            PropertyInfo[] daoProps = dao.GetType().GetProperties();
            T dto = new T();

            foreach (PropertyInfo sourceProp in daoProps)
            {
                var sourceValue = sourceProp.GetValue(dao);
                var targetProp = dto.GetType().GetProperty(sourceProp.Name);
                if (targetProp == null) continue;
                var targetValue = Convert(sourceValue, targetProp.PropertyType);
                targetProp.SetValue(dto, targetValue);
            }
            return dto;
        }

        public static object Convert(object sourceValue,Type targetType)
        {
            if(sourceValue.GetType() == targetType) return sourceValue;


            PropType propType = targetType.GetPropType();

            Type type = Type.GetType($"AutoMapper.TypesMapping.{propType}");
            ATypesMapping typesMapping = (ATypesMapping)Activator.CreateInstance(type);
            //用反射找方法去執行轉換

        }


      

    }
}
