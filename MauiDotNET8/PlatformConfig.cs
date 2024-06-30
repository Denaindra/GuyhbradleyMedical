using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiDotNET8
{
    public class PlatformConfig
    {
        public static PlatformConfig Instance { get; } = new PlatformConfig();
        public object ParentWindow { get; set; }

    }
}
