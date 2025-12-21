using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using LinqExp = System.Linq.Expressions;
namespace AutoMapper.ExpressionsMapping
{
    public class MemberExpression : BaseExpression
    {
        public override object GetValue(Expression expression, object sourceObj)
        {
            LinqExp.MemberExpression memExp = (LinqExp.MemberExpression)expression;
            //memExp.Expression !=  再遞迴
            
            var obj = GetExpressionValue(memExp.Expression, sourceObj);

            if(memExp.Member is PropertyInfo)
            {
                Console.WriteLine($"PropertyInfo");
                return ((PropertyInfo)memExp.Member).GetValue(obj);
                return sourceObj.GetType().GetProperty(memExp.Member.Name).GetValue(obj);

            }
            else if(memExp.Member is FieldInfo)
            {
                Console.WriteLine($"Field");
                return ((FieldInfo)memExp.Member).GetValue(obj);
            }


            var value = sourceObj.GetType().GetProperty(memExp.Member.Name).GetValue(obj);
            //var value = sourceObj.GetType().GetProperty(memExp.Member.Name).GetValue(sourceObj);
            return value;
        }
    }
}
