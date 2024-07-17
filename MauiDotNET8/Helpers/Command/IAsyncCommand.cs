using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MauiDotNET8.Helpers.Command
{
    public interface IAsyncCommand : ICommand
    {
        Task ExecuteAsync(object parameter);
    }
    public interface IAsyncCommand<TParameter> : IAsyncCommand
    {
        Task ExecuteAsync(TParameter parameter);
    }
}
