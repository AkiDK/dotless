﻿/* Copyright 2009 dotless project, http://www.dotlesscss.com
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * 
 *     http://www.apache.org/licenses/LICENSE-2.0
 *     
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License. */

using System;
using System.Linq;
using System.Reflection;
using dotless.Core.engine.Functions;
using dotless.Core.utils;

namespace dotless.Core.engine
{
    using System.Collections.Generic;
    using System.Text;

    public class Function : Literal, IEvaluatable
    {
        public IList<INode> Args
        {
            get; set;
        }

        public Function(string value, IList<INode> args)
            : base(value)
        {
            Args = args;
        }
        public override string ToCss()
        {
            return Evaluate().ToCss();
        }

        public INode Evaluate()
        {
            // RGB color hack

            Type functionType = Type.GetType("dotless.Core.engine.Functions." + Value + "Function", false, true);
            if(functionType == null)
            {
                return new Literal(string.Format("{0}({1})", Value.ToUpper(), ArgsString));                
            }
            var function = (FunctionBase)Activator.CreateInstance(functionType);

            var args = Args
              .Select(a => a is IEvaluatable ? (a as IEvaluatable).Evaluate() : a)
              .ToArray();

            function.SetArguments(args);
            return function.Evaluate();
        }

        protected string ArgsString
        {
            get
            {
                return string.Join(", ", Args.Select(arg => arg.ToCss()).ToArray());
            }
        }
    }
}