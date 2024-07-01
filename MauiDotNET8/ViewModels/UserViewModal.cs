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
        public UserViewModal(IB2CAuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
        }

        public void BuildTheApplicationbundle()
        {
            authenticationService.IntilizedTheAppplicationBulder();
        }

        public async void LoginSigin()
        {
           var results = await authenticationService.SignInInteractively();
        }
    }
}
