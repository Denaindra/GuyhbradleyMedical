using MauiDotNET8.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiDotNET8.Modals.API
{
    public class ClinicContactConfiguration
    {
        [Display(Name = "Contact Type")]
        public ClinicContactType? ContactType { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
    }
}
