using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using AutoMapper.Enums;

namespace AutoMapper
{
    public class ExpressionModel
    {
        public Expression SelfExpression { get; set; }
        public ExpressionsType ExpressionsType { get; set; }

        public ExpressionModel(Expression selfExpression, ExpressionsType expressionsType)
        {
            this.SelfExpression = selfExpression;
            this.ExpressionsType = expressionsType;
        }


    }
}
