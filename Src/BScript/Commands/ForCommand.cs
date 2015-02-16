﻿namespace BScript.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using BScript.Expressions;

    public class ForCommand : ICommand
    {
        private string name;
        private IExpression fromexpr;
        private IExpression toexpr;
        private ICommand body;

        public ForCommand(string name, IExpression fromexpr, IExpression toexpr, ICommand body)
        {
            this.name = name;
            this.fromexpr = fromexpr;
            this.toexpr = toexpr;
            this.body = body;
        }

        public string Name { get { return this.name; } }

        public IExpression FromExpression { get { return this.fromexpr; } }

        public IExpression ToExpression { get { return this.toexpr; } }

        public ICommand Body { get { return this.body; } }

        public void Execute(Context context)
        {
            throw new NotImplementedException();
        }
    }
}