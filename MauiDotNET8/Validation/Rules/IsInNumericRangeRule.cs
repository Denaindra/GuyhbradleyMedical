using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiDotNET8.Validation.Rules
{
    public class IsInNumericRangeRule<T> : IValidationRule<T>
    {
        public int Maximum { get; set; }
        public int Minimum { get; set; }
        public string ValidationMessage { get; set; }

        public bool Check(T value)
        {
            if (value == null)
            {
                return false;
            }

            var s = value as short?;

            return (s >= Minimum && s <= Maximum);

        }
    }
}
