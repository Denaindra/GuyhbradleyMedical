using MauiDotNET8.Interface;
using MauiDotNET8.Modals.Auth;
using MauiDotNET8.ViewModels.Base;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Abstractions;
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

        private bool isRunning;
        public UserViewModal(IB2CAuthenticationService authenticationService, AppShell appShell)
        {
            this.authenticationService = authenticationService;
            this.appShell = appShell;
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
                IsRunning = true;
                var results = await authenticationService.SignInInteractively();
                if (!string.IsNullOrEmpty(results.AccessToken))
                {
                    IsRunning = false;
                    // Application.Current.MainPage = this.appShell;
                }

            }
            catch(MsalClientException e)
            {
                IsRunning = false;
            }
            
        }
    }
}
