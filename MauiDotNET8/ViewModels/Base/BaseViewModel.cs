﻿using MauiDotNET8.Interface.API;
using MauiDotNET8.Modals.API;
using Refit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MauiDotNET8.ViewModels.Base
{
    public class BaseViewModel<T> : BaseViewModel
    {
       // protected AzureDataStore<T> azureDataStore = new Services.AzureDataStore<T>();
    }

    public class BaseViewModel : INotifyPropertyChanged
    {
        private static IBloodPressure ibloodPressure;
        private static IUrineProtine iUrineProtine;
        public INavigation navigation;

        bool isBusy = false;
        string title = string.Empty;
        bool isDirty = false;

        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        public bool IsDirty
        {
            get
            {
                return isDirty;
            }
        }
        
        public async Task<string> GetAccessToken()
        {
          var result = await SecureStorage.Default.GetAsync("accessToken");
          return result;
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);

            isDirty = true;

            return true;
        }

        protected bool CanExecuteSubmit()
        {
            return !IsBusy;
        }

        public async void PushModalAsync(Page page)
        {
            await navigation.PushModalAsync(page);
        }
        public async void PushAsyncPage(Page page)
        {
            await navigation.PushAsync(page);
        }
        public async void PopAsyncy()
        {
            await navigation.PopAsync();
        }
        public async void PopModalAsyncy()
        {
            await navigation.PopModalAsync();
        }
        public async void PopToRootAsync()
        {
            await navigation.PopToRootAsync();
        }

        public IBloodPressure GetTrakkaclinicalAPI()
        {
            if (ibloodPressure is null)
            {
                ibloodPressure = RestService.For<IBloodPressure>("https://mobile-api.trakkaclinical.com");
            }
            return ibloodPressure;
        }

        public IUrineProtine GetIUrineProtinelAPI()
        {
            if (iUrineProtine is null)
            {
                iUrineProtine = RestService.For<IUrineProtine>("https://mobile-api.trakkaclinical.com");
            }
            return iUrineProtine;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async Task<ClinicContactModal> GetMobileServiceString()
        {
            try
            {
                var results = await GetTrakkaclinicalAPI().GteMobileServiceResults("glCEJnehDpVwtp/u/rLgEHznsD6cv0U2ygzBNgQLChs0KqLtMELKtA==", await GetAccessToken());
                return results;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion
    }
}
