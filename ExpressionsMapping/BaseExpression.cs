using AutoMapper.Enums;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using LinqExp = System.Linq.Expressions;

namespace AutoMapper.ExpressionsMapping
{
    public abstract class BaseExpression
    {
        public abstract object GetValue(Expression expression, object sourceObj);
        public object GetExpressionValue(Expression expSource, object sourceObj)
        {
            if (expSource is LinqExp.MemberExpression sourceMemberExp) // 成員存取
            {
                //FieldInfo value = (FieldInfo)sourceMemberExp.Member;
                //var re = value.GetValue(sourceMemberExp.Expression);


                ExpressionsType expType = ExpressionsType.MemberExpression;
                Type type = Type.GetType($"AutoMapper.ExpressionsMapping.{expType}");
                BaseExpression baseExpression = (BaseExpression)Activator.CreateInstance(type);
                
                return baseExpression.GetValue(sourceMemberExp, sourceObj);
            }
            else if (expSource is LinqExp.BinaryExpression sourceBinaryExp) // 二元運算子 (&&, ||, <, ==, <=)
            {
                ExpressionsType expType = ExpressionsType.BinaryExpression;
                Type type = Type.GetType($"AutoMapper.ExpressionsMapping.{expType}");
                BaseExpression baseExpression = (BaseExpression)Activator.CreateInstance(type);

                return baseExpression.GetValue(sourceBinaryExp, sourceObj);
            }
            else if (expSource is LinqExp.ConditionalExpression sourceConditionalExp) // 條件表達式 (?:)
            {
                ExpressionsType expType = ExpressionsType.ConditionalExpression;
                Type type = Type.GetType($"AutoMapper.ExpressionsMapping.{expType}");
                BaseExpression baseExpression = (BaseExpression)Activator.CreateInstance(type);

                Expression testExp = sourceConditionalExp.Test;
                bool test = (bool)baseExpression.GetValue(testExp, sourceObj);
                return test ? baseExpression.GetValue(sourceConditionalExp.IfTrue, sourceObj) : baseExpression.GetValue(sourceConditionalExp.IfFalse, sourceObj);
            }
            else if (expSource is LinqExp.ConstantExpression sourceConstantExp) // 常數
            {
                ExpressionsType expType = ExpressionsType.ConstantExpression;
                Type type = Type.GetType($"AutoMapper.ExpressionsMapping.{expType}");
                BaseExpression baseExpression = (BaseExpression)Activator.CreateInstance(type);
                return baseExpression.GetValue(sourceConstantExp, sourceObj);
            }
            else if (expSource is LinqExp.MethodCallExpression sourceMethodCallExp) // 方法呼叫
            {
                ExpressionsType expType = ExpressionsType.MethodCallExpression;
                Type type = Type.GetType($"AutoMapper.ExpressionsMapping.{expType}");
                BaseExpression baseExpression = (BaseExpression)Activator.CreateInstance(type);
                return baseExpression.GetValue(sourceMethodCallExp, sourceObj);
            }
            else if (expSource is LinqExp.UnaryExpression sourceUnaryExp) // 一元運算子 (!)
            {
                ExpressionsType expType = ExpressionsType.UnaryExpression;
                Type type = Type.GetType($"AutoMapper.ExpressionsMapping.{expType}");
                BaseExpression baseExpression = (BaseExpression)Activator.CreateInstance(type);
                return baseExpression.GetValue(sourceUnaryExp, sourceObj);
            }
            else if (expSource is LinqExp.ParameterExpression sourceParameterExp)
            {
                ExpressionsType expType = ExpressionsType.ParameterExpression;
                Type type = Type.GetType($"AutoMapper.ExpressionsMapping.{expType}");
                BaseExpression baseExpression = (BaseExpression)Activator.CreateInstance(type);
                return baseExpression.GetValue(sourceParameterExp, sourceObj);
            }

            throw new NotImplementedException();
        }
    }
}
