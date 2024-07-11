using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiDotNET8.Enumerations
{
    public enum UrineProteinLevel
    {
        [Display(Name = "Nil")]
        Nil,
        [Display(Name = "Trace")]
        Trace,
        [Display(Name = "1*")]
        OneStar,
        [Display(Name = "2**")]
        TwoStars,
        [Display(Name = "3***")]
        ThreeStars
    }
}
