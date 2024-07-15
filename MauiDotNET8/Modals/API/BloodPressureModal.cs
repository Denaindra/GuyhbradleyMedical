using Azure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiDotNET8.Modals.API
{
    public class BloodPressureModal
    {
        public int systolic { get; set; }
        public long diastolic { get; set; }
        public long headaches { get; set; }
        public long blurredVision { get; set; }
        public long abdominalPain { get; set; }
        public long id { get; set; }
        public string userId { get; set; }
        public string testDateTimeUtc { get; set; }
        public List<Response> responses { get; set; }
        public List<object> notes { get; set; }
        public List<object> tags { get; set; }
        public long State { get; set; }
        public object stateChangedAt { get; set; }
        public object userDisplayName { get; set; }
        public object stateChangedBy { get; set; }
        public object stateChangedUserName { get; set; }
    }
    public partial class Response
    {
        public long id { get; set; }
        public long testResponseLevel { get; set; }
        public string originalValue { get; set; }
        public long testResponseType { get; set; }
    }
}
