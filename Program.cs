using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AutoMapper
{
    internal class Program
    {
        static void Main(string[] args)
        {

            string str = "200";
            MethodInfo parse = typeof(int).GetMethod("Parse", new Type[] { typeof(string) });
            int value = (int)parse.Invoke(null, new object[] { str });

            CardDAO dao = new CardDAO();
            CardDTO dto = Mapper.Map<CardDTO>(dao);

            foreach (PropertyInfo prop in typeof(CardDTO).GetProperties())
            {
                var dtovalue = prop.GetValue(dto);
                if (dtovalue.GetType() != typeof(string) && dtovalue is IEnumerable)
                {
                    Console.Write($"{prop.Name}:");
                    foreach (var item in (IEnumerable)dtovalue)
                    {
                        Console.Write($"{item} ");
                    }
                    Console.Write($"\n");
                }
                else
                {
                    Console.WriteLine($"{prop.Name}:{dtovalue}");
                }

            }
            IEnumerable<int> obj;


            #region Array對轉

            //object source = new string[] { "1", "2", "3", "4", "5" };
            //Type destType = typeof(int[]);
            //Type destElementType = destType.GetElementType();
            //Type sourceType = source.GetType();

            //if (sourceType.IsArray && destType.IsArray)
            //{
            //    var sourceElementType = sourceType.GetElementType();
            //    Array sourceValue = (Array)source;
            //    int sourceLength = sourceValue.Length;

            //    var destValue = Array.CreateInstance(destType.GetElementType(), sourceLength);

            //    var parseMethod = destElementType.GetMethod("Parse", new Type[] { sourceElementType });

            //    for (int i = 0; i < sourceLength; i++)
            //    {
            //        var tempSour = parseMethod.Invoke(null, new object[] { sourceValue.GetValue(i) });
            //        destValue.SetValue(tempSour, i);
            //    }
            //}



            #endregion


            #region IEnumerable對轉

            //object strings = new ConcurrentBag<string>() { "1", "2", "3", "4", "5" };

            //bool isEnumerable = strings.GetType().GetInterfaces().Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IEnumerable<>));

            //if (strings.GetType() != typeof(string) && isEnumerable) 
            //{
            //    var sourceType = strings.GetType().GetGenericArguments()[0];
            //    Type targetType = typeof(List<int>);

            //    //0. 我得知道他是List?
            //    //1. 我沒有 List的實體
            //    //2. 我知道是string 轉 xxx Type, 可是我不知道xxx是誰

            //    var typeDefinition = targetType.GetGenericTypeDefinition(); // 將List<int> 拆解為 List<T>
            //    Type argument = targetType.GetGenericArguments()[0];
            //    var parseMethod = argument.GetMethod("Parse", new Type[] { sourceType });
            //    var targetListType = typeDefinition.MakeGenericType(argument);
            //    var targetList = Activator.CreateInstance(targetType, null);

            //    var methodAdd = targetListType.GetMethod("Add");

            //    foreach (var item in (IEnumerable)strings)
            //    {
            //        methodAdd.Invoke(targetList, new object[] { parseMethod.Invoke(null, new object[] { item }) });
            //    }
            //}

            #endregion

            Console.ReadLine();
        }
    }
}
