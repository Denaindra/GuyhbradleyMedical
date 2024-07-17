using MauiDotNET8.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiDotNET8.ViewModels.BloodPressure
{
    public class FrequencyNameValue
    {
        public string Name { get; set; }
        public FrequencyValue Value { get; set; }

        public FrequencyNameValue(string Name, FrequencyValue Value)
        {
            this.Name = Name;
            this.Value = Value;
        }
    }
}
