using MauiDotNET8.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiDotNET8.Modals.API
{
    public class BloodPressureTest:Test
    {
        [Required]
        public short? Systolic { get; set; }
        [Required]
        public short? Diastolic { get; set; }
        //[Required]
        public FrequencyValue Headaches { get; set; } = FrequencyValue.NotAnswered;
        //[Required]
        public FrequencyValue BlurredVision { get; set; } = FrequencyValue.NotAnswered;
        //[Required]
        public FrequencyValue AbdominalPain { get; set; } = FrequencyValue.NotAnswered;
    }
}
