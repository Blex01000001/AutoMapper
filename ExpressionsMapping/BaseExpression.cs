using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AutoMapper.ExpressionsMapping
{
    public abstract class BaseExpression
    {
        public abstract object GetValue(Expression expression, object sourceObj);
    }
}
