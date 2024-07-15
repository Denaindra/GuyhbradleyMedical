using MauiDotNET8.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiDotNET8.Modals.API
{
    public class TestResponse
    {
        public int Id { get; set; }
        public TestResponseLevel TestResponseLevel { get; set; }
        public string OriginalValue { get; set; }
        public TestResponseType TestResponseType { get; set; }
    }
}
