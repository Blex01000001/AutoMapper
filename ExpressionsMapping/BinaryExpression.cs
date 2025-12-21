using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using LinqExp = System.Linq.Expressions;

namespace AutoMapper.ExpressionsMapping
{
    public class BinaryExpression : BaseExpression
    {
        public override object GetValue(Expression expression, object sourceObj)
        {
            LinqExp.BinaryExpression binaryExpression = expression as LinqExp.BinaryExpression;

            var left = GetExpressionValue(binaryExpression.Left, sourceObj);
            var right = GetExpressionValue(binaryExpression.Right, sourceObj);

            int.TryParse(left.ToString(), out int leftValue);
            int.TryParse(right.ToString(), out int rightValue);

            // +-*/
            // > >= = <= <

            IComparable comparable =(IComparable)left;
            comparable.CompareTo(rightValue);

            ExpressionType[] reInt = new ExpressionType[] {ExpressionType.Add, ExpressionType.Subtract, ExpressionType.Multiply, ExpressionType.Divide };
            ExpressionType[] reBool = new ExpressionType[] { ExpressionType.GreaterThan, ExpressionType.GreaterThanOrEqual, ExpressionType.Equal, ExpressionType.LessThan, ExpressionType.LessThanOrEqual };
            if (reInt.Contains(binaryExpression.NodeType))
            {
                switch (binaryExpression.NodeType)
                {
                    case ExpressionType.Add:
                        return Convert.ToDecimal(left) + Convert.ToDecimal(right);
                    case ExpressionType.Subtract:
                        return Convert.ToDecimal(left) - Convert.ToDecimal(right);
                    case ExpressionType.Multiply:
                        return Convert.ToDecimal(left) * Convert.ToDecimal(right);
                    case ExpressionType.Divide:
                        return Convert.ToDecimal(left) / Convert.ToDecimal(right);
                }
            }
            else if (reBool.Contains(binaryExpression.NodeType))
            {
                if (leftValue.CompareTo(rightValue) == 1)
                {
                    return true;
                }
                return false;
            }
            return null;
        }
    }
}
