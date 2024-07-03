using MauiDotNET8.Modals.Auth;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiDotNET8.Interface
{
    public interface IB2CAuthenticationService
    {
        void IntilizedTheAppplicationBulder();
        Task<UserContext> SignInInteractively();
        Task<UserContext> SignOutInteractively();
    }
}
