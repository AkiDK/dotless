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
using dotless.Core.exceptions;

namespace dotless.Core.engine
{
    using System.Globalization;

    public class Number : Literal
    {
        //Have to wrap float instead of extending
        new internal double Value { get; set; }

        public Number(double value)
        {
            Value = value;
        }

        public Number(string unit, double value)
        {
            Unit = unit;
            Value = value;
        }

        public override string ToString()
        {
            return string.Format("{0}{1}", Value, Unit);
        }

        public override string ToCSharp()
        {

            return Value.ToString("N2", CultureInfo.InvariantCulture);
        }


        /// <summary>
        /// Ugly hack to make spec pass.
        /// </summary>
        /// <returns></returns>
        private string FormatValue()
        {

            string value = Decimal.Round((Decimal)Value, 2).ToString(NumberFormatInfo.InvariantInfo);
            if (value.StartsWith("0."))
                value = value.Remove(0, 1);
            if (value.StartsWith("-0."))
                value = value.Remove(1, 1);
            if (value.Contains(".") && value.EndsWith("0"))
                value = value.Substring(0, value.Length - 1);
            return value;
        }
        public override string ToCss()
        {
            return string.Format("{0}{1}",FormatValue(), Unit ?? "");
        }


        #region operator overrides
        public static Number operator +(Number number1, Number number2)
        {
            if (number1.Unit != number2.Unit && !string.IsNullOrEmpty(number1.Unit) && !string.IsNullOrEmpty(number2.Unit)) throw new MixedUnitsException();
            var unit = number1.Unit != "" ? number1.Unit : number2.Unit;
            return new Number(number1.Value + number2.Value) { Unit = unit };
        }
        public static Number operator +(Number number1, double number2)
        {
            return new Number(number1.Value + number2) { Unit = number1.Unit };
        }
        public static Number operator +(double number1, Number number2)
        {
            return new Number(number1 + number2.Value) { Unit = number2.Unit };
        }
        public static Number operator -(Number number1, Number number2)
        {
            if (number1.Unit != number2.Unit && !string.IsNullOrEmpty(number1.Unit) && !string.IsNullOrEmpty(number2.Unit)) throw new MixedUnitsException();
            var unit = number1.Unit != "" ? number1.Unit : number2.Unit;
            return new Number(number1.Value - number2.Value) { Unit = unit };
        }
        public static Number operator -(Number number1, double number2)
        {
            return new Number(number1.Value - number2) { Unit = number1.Unit };
        }
        public static Number operator -(double number1, Number number2)
        {
            return new Number(number1 - number2.Value) { Unit = number2.Unit };
        }      
        public static Number operator *(Number number1, Number number2)
        {
            if (number1.Unit != number2.Unit && !string.IsNullOrEmpty(number1.Unit) && !string.IsNullOrEmpty(number2.Unit)) throw new MixedUnitsException();
            var unit = number1.Unit != "" ? number1.Unit : number2.Unit;
            return new Number(number1.Value * number2.Value) { Unit = unit };
        }
        public static Number operator *(Number number1, double number2)
        {
            return new Number(number1.Value * number2) { Unit = number1.Unit };
        }
        public static Number operator *(double number1, Number number2)
        {
            return new Number(number1 * number2.Value) { Unit = number2.Unit };
        }
        public static Number operator /(Number number1, Number number2)
        {
            if (number1.Unit != number2.Unit && !string.IsNullOrEmpty(number1.Unit) && !string.IsNullOrEmpty(number2.Unit)) throw new MixedUnitsException();
            var unit = number1.Unit != "" ? number1.Unit : number2.Unit;
            return new Number(number1.Value / number2.Value) { Unit = unit };
        }
        public static Number operator /(Number number1, double number2)
        {
            return new Number(number1.Value/number2) {Unit = number1.Unit};
        }
        public static Number operator /(double number1, Number number2)
        {
            return new Number(number1 / number2.Value) { Unit = number2.Unit };
        }

        #endregion
    }
}