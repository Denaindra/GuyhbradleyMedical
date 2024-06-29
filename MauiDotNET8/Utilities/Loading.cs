using Controls.UserDialogs.Maui;
using MauiDotNET8.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiDotNET8.Utilities
{
    public class Loading : ILoading
    {
        private readonly IUserDialogs userDialouge;
        public Loading(IUserDialogs userDialog)
        {
            this.userDialouge = userDialog;
        }

        public void EndIndiCator()
        {
            userDialouge.HideHud();
        }

        public void StartIndicator()
        {
            userDialouge.ShowLoading("Loading", MaskType.Clear);

        }
    }
}
