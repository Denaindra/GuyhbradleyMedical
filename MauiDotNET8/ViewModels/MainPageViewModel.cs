using MauiDotNET8.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiDotNET8.ViewModels
{
    public class MainPageViewModel
    {
        private readonly ILoading loading;
        public MainPageViewModel(ILoading loading)
        {
            this.loading = loading;
        }

        public void StartLoading()
        {
            loading.StartIndicator();
        }

        public void EndLoading()
        {
            loading.EndIndiCator();
        }
    }
}
