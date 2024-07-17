using MauiDotNET8.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiDotNET8.ViewModels.UrineProtein
{
    public class ProteinLevelNameValue
    {
        public string Name { get; set; }
        public UrineProteinLevel Value { get; set; }

        public ProteinLevelNameValue(string Name, UrineProteinLevel Value)
        {
            this.Name = Name;
            this.Value = Value;
        }
    }
}
