
using MauiDotNET8.Helpers;
using MauiDotNET8.Interface;
using MauiDotNET8.Modals.Auth;
using MauiDotNET8.Screens;
using MauiDotNET8.ViewModels.Base;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Abstractions;
using Microsoft.Maui.Storage;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiDotNET8.ViewModels
{
    public class UserViewModal : BaseViewModel
    {
        private readonly IB2CAuthenticationService authenticationService;
        private readonly AppShell appShell;
        private readonly FAQPage fAQPage;

        private bool isRunning;
        public UserViewModal(IB2CAuthenticationService authenticationService, AppShell appShell,FAQPage fAQPage)
        {
            this.authenticationService = authenticationService;
            this.appShell = appShell;
            this.fAQPage = fAQPage;
        }


        public bool IsRunning
        {
            get { 
                return isRunning; 
            }
            set { 
                isRunning = value;
                OnPropertyChanged(nameof(IsRunning));
            }
        }

        public void BuildTheApplicationbundle()
        {
            authenticationService.IntilizedTheAppplicationBulder();
        }

        public async void LoginSigin()
        {
            try
            {
                //IsRunning = true;
                //var userContext = await authenticationService.SignInInteractively();
                //if (!string.IsNullOrEmpty(userContext.AccessToken))
                {
                    //    IsRunning = false;
                    //    await SecureStorage.Default.SetAsync("login", string.Format("{0} {1}", userContext.GivenName, userContext.FamilyName));
                    //    await SecureStorage.Default.SetAsync("clinicIdentifier", userContext.ClinicIdentifier);
                    //await SecureStorage.Default.SetAsync("accessToken", userContext.AccessToken);
                    Application.Current.MainPage = this.appShell;
                }
            }
            catch(MsalClientException e)
            {
                IsRunning = false;
            }
            
        }

        public async void LogoutSignOut()
        {
            try
            {
                IsRunning = true;
                var results = await authenticationService.SignOutInteractively();
                if (results.IsLoggedOn)
                {
                    IsRunning = false;
                }
            }
            catch (MsalClientException e)
            {
                IsRunning = false;
            }
        }

        public void NavigateBackToLoginPage()
        {
            var loginPage = ServiceHelper.GetService<LoginPage>();
            Application.Current.MainPage = loginPage;
        }

        public async Task<string> GetLoginedUser()
        {
            try
            {
                var loggedUser = await SecureStorage.Default.GetAsync("login");
                if (string.IsNullOrEmpty(loggedUser))
                {
                    return string.Empty;
                }
                return loggedUser;
            }
            catch (NullReferenceException)
            {

            }
            return string.Empty;
        }

        public void NavigateToFAQPage()
        {
            navigation.PushAsync(fAQPage);
        }
    }
}
