﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dotless.Core.Parser.Infrastructure;
using dotless.Core.Parser.Infrastructure.Nodes;

namespace dotless.Core.Parser.Tree
{
    public class Extend : Node
    {
        public Extend(List<Selector> exact, List<Selector> partial)
        {
            Exact = exact;
            Partial = partial;
        }

        public List<Selector> Exact{ get; set; }
        public List<Selector> Partial { get; set; }

        public override Node Evaluate(Env env)
        {
            return this;
        }

        public override void AppendCSS(Env env)
        {
            
        }
    }
}
