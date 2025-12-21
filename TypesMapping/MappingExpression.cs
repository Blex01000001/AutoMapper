using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AutoMapper.TypesMapping
{
    public class MappingExpression<TDestination, TSource>
    {
        //                TDest             TSource  
        public Dictionary<PropertyInfo, ExpressionModel> propNameKeyValue = new Dictionary<PropertyInfo, ExpressionModel>();
        string sourcePropName;
        string DestPropName;
        public MappingExpression<TDestination, TSource> ForMember<TDestinationProp, TSourceProp>(
            Expression<Func<TSource, TSourceProp>> expSource, 
            Expression<Func<TDestination, TDestinationProp>> expDest)
        {
            MemberExpression destMemberExpression =(MemberExpression)expDest.Body;
            PropertyInfo destProperty = (PropertyInfo) destMemberExpression.Member;

            if (expSource.Body is MemberExpression sourceMemberExp) // 成員存取
            {
                propNameKeyValue.Add(destProperty, new ExpressionModel(sourceMemberExp, Enums.ExpressionsType.MemberExpression));
            }
            else if (expSource.Body is BinaryExpression sourceBinaryExp) // 二元運算子 (&&, ||, <, ==, <=)
            {
                var left = sourceBinaryExp.Left;
                var right = sourceBinaryExp.Right;
                // 接著遞迴
                propNameKeyValue.Add(destProperty, new ExpressionModel(sourceBinaryExp, Enums.ExpressionsType.BinaryExpression));

            }
            else if (expSource.Body is ConditionalExpression sourceConditionalExp) // 條件表達式 (?:)
            {
                var test = sourceConditionalExp.Test;
                var IfTrue = sourceConditionalExp.IfTrue;
                var IfFalse = sourceConditionalExp.IfFalse;
                // 接著遞迴
                propNameKeyValue.Add(destProperty, new ExpressionModel(sourceConditionalExp, Enums.ExpressionsType.ConditionalExpression));
            }
            else if (expSource.Body is ConstantExpression sourceConstantExp) // 常數
            {
                propNameKeyValue.Add(destProperty, new ExpressionModel(sourceConstantExp, Enums.ExpressionsType.ConstantExpression));
            }
            else if (expSource.Body is MethodCallExpression sourceMethodCallExp) // 方法呼叫
            {
                var obj = sourceMethodCallExp.Object;
                var arg = sourceMethodCallExp.Arguments.ToArray();
                // 接著遞迴
                propNameKeyValue.Add(destProperty, new ExpressionModel(sourceMethodCallExp, Enums.ExpressionsType.MethodCallExpression));
            }
            else if (expSource.Body is UnaryExpression sourceUnaryExp) // 一元運算子 (!)
            {
                var body = sourceUnaryExp.Operand;
                // 接著遞迴
                propNameKeyValue.Add(destProperty, new ExpressionModel(sourceUnaryExp, Enums.ExpressionsType.UnaryExpression));
            }
            return this;
        }
    }
}
