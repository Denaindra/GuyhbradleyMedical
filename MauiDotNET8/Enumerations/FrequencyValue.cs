using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiDotNET8.Enumerations
{
    public enum FrequencyValue
    {
        [Display(Name = "Please select")]
        NotAnswered = -1,
        No,
        Occasional,
        Frequent,
        Persistent
    }
}
