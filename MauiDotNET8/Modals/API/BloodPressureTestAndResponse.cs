using MauiDotNET8.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiDotNET8.Modals.API
{
    public class BloodPressureTestAndResponse: BloodPressureTest
    {

        public TestResponseLevel SystolicResponse { get; set; }


        public string SystolicResponseMessage
        {
            get
            {
                if (SystolicResponse == TestResponseLevel.Alert)
                {
                    return "Your systolic blood pressure is outside the normal range";
                }
                else if (SystolicResponse == TestResponseLevel.Warning)
                {
                    return "Your systolic blood pressure is outside the normal range";
                }
                else
                {
                    return "";
                }
            }
        }

        public TestResponseLevel DiastolicResponse { get; set; }

        public string DiastolicResponseMessage
        {
            get
            {
                if (DiastolicResponse == TestResponseLevel.Alert)
                {
                    return "Your diastolic blood pressure is outside the normal range";
                }
                else if (DiastolicResponse == TestResponseLevel.Warning)
                {
                    return "Your diastolic blood pressure is outside the normal range";
                }
                else
                {
                    return "";
                }
            }
        }



        public TestResponseLevel HeadachesResponse { get; set; }

        public string HeadachesResponseMessage
        {
            get
            {
                if (HeadachesResponse == TestResponseLevel.Alert) return "Your persistent headaches are not normal";
                return "";
            }
        }

        public TestResponseLevel BlurredVisionResponse { get; set; }

        public string BlurredVisionResponseMessage
        {
            get
            {
                if (BlurredVisionResponse == TestResponseLevel.Alert) return "Your persistent blurred vision is not normal";
                return "";
            }
        }

        public TestResponseLevel AbdominalPainResponse { get; set; }

        public string AbdominalPainResponseMessage
        {
            get
            {
                if (AbdominalPainResponse == TestResponseLevel.Alert) return "Your persistent abdominal pain is not normal";
                return "";
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
                IEnumerable<TestResponseLevel?> propertiesToCheck = new List<TestResponseLevel?>() { SystolicResponse, DiastolicResponse, HeadachesResponse, BlurredVisionResponse, AbdominalPainResponse };
                if (CheckResponses(propertiesToCheck, TestResponseLevel.Alert)) return TestResponseLevel.Alert;
                if (CheckResponses(propertiesToCheck, TestResponseLevel.Warning)) return TestResponseLevel.Warning;
                return TestResponseLevel.Ok;
            }
        }


        private bool CheckResponses(IEnumerable<TestResponseLevel?> responses, TestResponseLevel responseLevelToCheck)
        {
            foreach (var response in responses)
            {
                if (response != null && response == responseLevelToCheck) return true;
            }
            return false;
        }

    }
}
