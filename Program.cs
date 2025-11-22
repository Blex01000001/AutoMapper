using AutoMapper.Extensions;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AutoMapper
{
    internal class Program
    {
        static void Main(string[] args)
        {

            //string str = "200";
            //MethodInfo parse = typeof(int).GetMethod("Parse", new Type[] { typeof(string) });
            //int value = (int)parse.Invoke(null, new object[] { str });

            //CardDAO dao = new CardDAO();
            //CardDTO dto = Mapper.NewMap<CardDTO>(dao);

            //foreach (PropertyInfo prop in typeof(CardDTO).GetProperties())
            //{
            //    var dtovalue = prop.GetValue(dto);
            //    if (dtovalue.GetType() != typeof(string) && dtovalue is IEnumerable)
            //    {
            //        Console.Write($"{prop.Name}:");
            //        foreach (var item in (IEnumerable)dtovalue)
            //        {
            //            Console.Write($"{item} ");
            //        }
            //        Console.Write($"\n");
            //    }
            //    else
            //    {
            //        Console.WriteLine($"{prop.Name}:{dtovalue}");
            //    }

            //}
            //IEnumerable<int> obj;


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


            // where 1=1 and xxx=xxx and yyy=yyy

            List<CardDAO> cards = new List<CardDAO>()
            {
                new CardDAO(){Qty = 40},
                new CardDAO(){Qty = 99},
                new CardDAO(){Qty = 70},
                new CardDAO(){Qty = 20},
                new CardDAO(){Qty = 0},
                new CardDAO(){Qty = 100},
                new CardDAO(){Qty = 30},
            };

            //mapper.ForMemeber(x=> x.ProductID,x=> int.Parse(x.pro))
            DateTime now = DateTime.Now;
            List<CardDAO> filterCards = FilterCards(cards, x =>  x.Qty*2 > x.Qty+10 && int.Parse(x.ID) %10==0 ? 100:0);
            //List<CardDAO> sortedCards = SortCards(cards, x => x.Qty);

            //foreach (var item in sortedCards)
            //{
            //    Console.WriteLine(item.Qty);
            //}

            //Member => 直接傳入類別屬性
            //Binary => 當今天有多種條件
            //Conditional => 條件式 (三元運算式)
            //Constant => 常數使用
            //MethodCall => 函數呼叫完後的結果
            //Unary => !x.Enabled (一元運算)
            Console.ReadLine();
        }
        static List<CardDAO> SortCards<T>(List<CardDAO> cards, Expression<Func<CardDAO, T>> sortProp)
        {
            //用快速排序來排序
            //List<CardDAO> sortedCards = new List<CardDAO>();
            //ParameterExpression param = Expression.Parameter(typeof(T), "x");
            
            MemberExpression memberBody = (MemberExpression)sortProp.Body;
            string memberName = memberBody.Member.Name;

            QuickSort.Sort(cards, 0, cards.Count - 1, memberName);

            return cards;
        }
        static List<CardDAO> FilterCards<T>(List<CardDAO> cards, Expression<Func<CardDAO, T>> filter)
        {
            //寫完Binary Member
            if (filter.Body is ConditionalExpression)
            {
                var temp = filter.Compile().Invoke(cards[0]);
                //遞迴晚點寫
                //ConditionalExpression exp = (ConditionalExpression)filter.Body;
                //var test = exp.Test;
                //var iftrue = exp.IfTrue;
                //var iffalse = exp.IfFalse;
            }
            else if (filter.Body is ConstantExpression)
            {
                ConstantExpression exp = (ConstantExpression)filter.Body;
                var result = exp.Value;
            }
            else if (filter.Body is MethodCallExpression)
            {
                MethodCallExpression exp = (MethodCallExpression)filter.Body;
                MethodInfo methodInfo = exp.Method;
                MemberExpression arg = (MemberExpression)exp.Arguments[0];
                ParameterExpression parameter = (ParameterExpression)arg.Expression;
                string propName = arg.Member.Name;
                var value = typeof(T).GetProperty(propName).GetValue(parameter);
                var result = methodInfo.Invoke(null, new object[] { value });
            }
            else if (filter.Body is UnaryExpression)
            {
                UnaryExpression exp = (UnaryExpression)filter.Body;
                //var result = exp.
            }

            var res = filter.Compile().Invoke(cards[0]);
            List<CardDAO> filterCards = new List<CardDAO>();
            // var data = filter.Invoke(cards[0]);


            return filterCards;

        }
    }
}
