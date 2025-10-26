using AutoMapper.Enums;
using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
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
        public static T Map<T>(object dao) where T : new ()
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

                if(dtoPprop.PropertyType != prop.PropertyType)
                {
                    if (dtoPprop.PropertyType.IsEnum)
                    {
                        dtovalue = Enum.Parse(dtoPprop.PropertyType, daoValue.ToString());
                    }
                    else if(prop.PropertyType.IsEnum && dtoPprop.PropertyType == typeof(int))
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
                        var methodSetValue = destType.GetMethod("SetValue",new Type[] { typeof(Object),typeof(int)});

                        for (int i = 0; i < sourceLength; i++)
                        {
                            var tempSour = parseMethod.Invoke(null, new object[] { sourceValue.GetValue(i) });
                            methodSetValue.Invoke(dtovalue, new object[] { tempSour ,i});
                        }
                    }
                    else if(daoValue.GetType() != typeof(string) && isEnumerable)
                    {
                        sourceType = daoValue.GetType().GetGenericArguments()[0];
                        Type targetType = typeof(List<int>);

                        var typeDefinition = targetType.GetGenericTypeDefinition(); // 將List<int> 拆解為 List<T>
                        Type argument = targetType.GetGenericArguments()[0];
                        var parseMethod = argument.GetMethod("Parse", new Type[] { sourceType });
                        var targetListType = typeDefinition.MakeGenericType(argument);
                        dtovalue = Activator.CreateInstance(targetType, null);

                        var methodAdd = targetListType.GetMethod("Add");

                        foreach (var item in (IEnumerable)daoValue)
                        {
                            methodAdd.Invoke(dtovalue, new object[] { parseMethod.Invoke(null, new object[] { item }) });
                        }
                    }
                    else if (dtoPprop.GetType().IsClass && prop.PropertyType.IsClass)
                    {
                        //StatusDao -> StatusDto
                        //物件 -> 物件
                        var method = typeof(Mapper).GetMethod("Map",BindingFlags.Static | BindingFlags.Public);
                        var mapperMethod = method.MakeGenericMethod(dtoPprop.PropertyType);
                        dtovalue = mapperMethod.Invoke(null, new object[] { daoValue});
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

                dtoPprop.SetValue(dto, dtovalue);
            }
            return dto;
        }
    }
}
