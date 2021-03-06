﻿namespace BScript.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using BScript.Language;
    using BScript.Utilities;

    public class DotExpression : IExpression
    {
        private IExpression expression;
        private string name;

        public DotExpression(IExpression expression, string name)
        {
            this.expression = expression;
            this.name = name;
        }

        public IExpression Expression { get { return this.expression; } }

        public string Name { get { return this.name; } }

        public string FullName
        {
            get
            {
                if (this.expression is DotExpression)
                    return ((DotExpression)this.expression).FullName + "." + this.name;

                return ((NameExpression)this.expression).Name + "." + this.name;
            }
        }

        public object Evaluate(Context context)
        {
            var value = this.expression.Evaluate(context);
            var type = value.GetType();

            return ObjectUtilities.GetValue(value, this.name);
        }
    }
}
