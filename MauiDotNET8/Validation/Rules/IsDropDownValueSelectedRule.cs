using MauiDotNET8.ViewModels.UrineProtein;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiDotNET8.Validation.Rules
{
    public class IsDropDownValueSelectedRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }

        public bool Check(T value)
        {
            if (value == null)
            {
                return false;
            }

            var proteinLevelNameValue = value as ProteinLevelNameValue;
            return ((int)proteinLevelNameValue.Value >= 0);
        }
    }
}
