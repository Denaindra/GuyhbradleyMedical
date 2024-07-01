using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiDotNET8.Modals.Auth
{
    public class UserContext
    {
        public string Name { get; set; }
        public string UserIdentifier { get; set; }
        public string ClinicIdentifier { get; set; }
        public bool IsLoggedOn { get; set; }
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        public string Province { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string EmailAddress { get; set; }
        public string JobTitle { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string AccessToken { get; set; }
        public DateTimeOffset? ExpiresOn { get; set; }
    }
}
