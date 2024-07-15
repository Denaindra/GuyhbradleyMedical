using MauiDotNET8.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiDotNET8.Modals.API
{
    public class Test
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime TestDateTimeUTC { get; set; }
        public IEnumerable<TestResponse> Responses { get; set; }
        public IEnumerable<TestNote> Notes { get; set; }
        public IEnumerable<string> Tags { get; set; }
        public TestResultState? State { get; set; }
        public DateTime? StateChangedAt { get; set; }
        public string UserDisplayName { get; set; }
        public string StateChangedBy { get; set; }
        public string StateChangedUserName { get; set; }
    }
}
