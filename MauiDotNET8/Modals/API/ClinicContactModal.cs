using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiDotNET8.Modals.API
{
    public class ClinicContactModal
    {
        public string ClinicId { get; set; }
        public IEnumerable<ClinicContactConfiguration> ClinicContactNumbers { get; set; }
    }
}
