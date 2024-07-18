
using MauiDotNET8.Interface;
using MauiDotNET8.Modals.Auth;
using Microsoft.Identity.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiDotNET8.Utilities.Auth
{
    public class B2CAuthenticationService : IB2CAuthenticationService
    {
        private PublicClientApplicationBuilder PublicClientApplicationBuilder;
        private readonly string[] scopes =  { "openid", "offline_access", "https://trakkaclinical.onmicrosoft.com/02c0ba5e-08fb-4539-8399-bb725b18cfe6/TestResults.ReadWrite" };

        public IPublicClientApplication PublicClientApplication { get; private set; }

        public void IntilizedTheAppplicationBulder()
        {
            this.PublicClientApplicationBuilder = PublicClientApplicationBuilder.Create("30328afb-e743-4bdd-a261-f2c02efe2e2b")
               .WithExperimentalFeatures()
               .WithB2CAuthority("https://trakkaclinical.b2clogin.com/tfp/trakkaclinical.onmicrosoft.com/B2C_1A_trakka_clinical_V2_signIn")
               .WithIosKeychainSecurityGroup("com.hamptonapp.mobile")
               .WithRedirectUri($"msal30328afb-e743-4bdd-a261-f2c02efe2e2b://auth");
            this.PublicClientApplication = this.PublicClientApplicationBuilder.Build();
        }
        public async Task<UserContext> SignInInteractively()
        {
            var accounts = await this.PublicClientApplication.GetAccountsAsync();
            if (accounts.Any())
            {
                var result = await AcquireTokenSilent(accounts);
                return result;
            }

            AuthenticationResult results = await this.PublicClientApplication.AcquireTokenInteractive(scopes)
             .WithParentActivityOrWindow(PlatformConfig.Instance.ParentWindow)
             .WithUseEmbeddedWebView(true)
             .ExecuteAsync();
            var userInfo = UpdateUserInfo(results);
            return userInfo;
        }
        public UserContext UpdateUserInfo(AuthenticationResult ar)
        {
            var newContext = new UserContext();
            newContext.IsLoggedOn = false;
            JObject user = ParseIdToken(ar.IdToken);

            newContext.AccessToken = ar.AccessToken;
            newContext.ExpiresOn = ar.ExpiresOn;
            newContext.Name = user["name"]?.ToString();
            newContext.UserIdentifier = user["oid"]?.ToString();

            newContext.ClinicIdentifier = user["clinic"]?.ToString();

            newContext.GivenName = user["given_name"]?.ToString();
            newContext.FamilyName = user["family_name"]?.ToString();

            newContext.StreetAddress = user["streetAddress"]?.ToString();
            newContext.City = user["city"]?.ToString();
            newContext.Province = user["state"]?.ToString();
            newContext.PostalCode = user["postalCode"]?.ToString();
            newContext.Country = user["country"]?.ToString();

            newContext.JobTitle = user["jobTitle"]?.ToString();

            var emails = user["emails"] as JArray;
            if (emails != null)
            {
                newContext.EmailAddress = emails[0].ToString();
            }
            newContext.IsLoggedOn = true;

            return newContext;
        }
        JObject ParseIdToken(string idToken)
        {
            // Get the piece with actual user info
            idToken = idToken.Split('.')[1];
            idToken = Base64UrlDecode(idToken);
            return JObject.Parse(idToken);
        }
        private string Base64UrlDecode(string s)
        {
            s = s.Replace('-', '+').Replace('_', '/');
            s = s.PadRight(s.Length + (4 - s.Length % 4) % 4, '=');
            var byteArray = Convert.FromBase64String(s);
            var decoded = Encoding.UTF8.GetString(byteArray, 0, byteArray.Count());
            return decoded;
        }
        public async Task<UserContext> SignOutInteractively()
        {
            var accounts = await this.PublicClientApplication.GetAccountsAsync();
            await this.PublicClientApplication.RemoveAsync(accounts.FirstOrDefault()).ConfigureAwait(false);
            var signedOutContext = new UserContext();
            signedOutContext.IsLoggedOn = false;
            return signedOutContext;
        }

        public async Task<UserContext> AcquireTokenSilent(IEnumerable<IAccount> accounts)
        {
            AuthenticationResult authResult = await this.PublicClientApplication.AcquireTokenSilent(scopes,accounts.LastOrDefault())
               .WithB2CAuthority("https://trakkaclinical.b2clogin.com/tfp/trakkaclinical.onmicrosoft.com/B2C_1A_trakka_clinical_V2_signIn")
               .ExecuteAsync();

            var newContext = UpdateUserInfo(authResult);
            return newContext;
        }
    }
}

