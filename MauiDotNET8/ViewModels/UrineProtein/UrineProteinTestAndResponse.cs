using MauiDotNET8.Enumerations;
using MauiDotNET8.Modals.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiDotNET8.ViewModels.UrineProtein
{
   public class UrineProteinTestAndResponse: UrineProteinTest
    {
        public new List<TestResponse> UrineProteinTestResponses = new List<TestResponse>();

        public TestResponseLevel UrineProteinResponse { get; set; }

        public string UrineProteinResponseMessage
        {
            get
            {
                if (UrineProteinResponse == TestResponseLevel.Alert)
                {
                    return "Your urine protein is outside the normal range";
                }
                else if (UrineProteinResponse == TestResponseLevel.Warning)
                {
                    return "Your urine protein is outside the normal range";
                }
                else
                {
                    return "";
                }
            }
        }

        public DateTime TestDateTimeLocal
        {
            get
            {
                return TestDateTimeUTC.ToLocalTime();
            }
        }

        public TestResponseLevel Status
        {
            get
            {
                return UrineProteinResponse;
            }
        }
    }
}
