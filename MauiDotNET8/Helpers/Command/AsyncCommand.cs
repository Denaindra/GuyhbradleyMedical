using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MauiDotNET8.Helpers.Command
{
    public class AsyncCommand<TParameter> : IAsyncCommand<TParameter>
    {
        public event EventHandler CanExecuteChanged;

        private bool _isExecuting;
        private Func<Task> executeGetAsync;
        private Func<bool> canExecuteSubmit;
        private readonly Func<TParameter, Task> _execute;
        private readonly Func<bool> _canExecute;
        //private readonly IErrorHandler _errorHandler;

        public AsyncCommand(
            Func<TParameter, Task> execute,
            Func<bool> canExecute = null
            //IErrorHandler errorHandler = null
            )
        {
            _execute = execute;
            _canExecute = canExecute;
            //_errorHandler = errorHandler;
        }

        public AsyncCommand(Func<Task> executeGetAsync, Func<bool> canExecuteSubmit)
        {
            this.executeGetAsync = executeGetAsync;
            this.canExecuteSubmit = canExecuteSubmit;
        }

        public bool CanExecute()
        {
            return !_isExecuting && (_canExecute?.Invoke() ?? true);
        }

        internal Task ExecuteAsync()
        {
            throw new NotImplementedException();
        }

        public async Task ExecuteAsync(TParameter parameter)
        {
            if (CanExecute())
            {
                try
                {
                    _isExecuting = true;
                    await _execute(parameter);
                }
                finally
                {
                    _isExecuting = false;
                }
            }

            RaiseCanExecuteChanged();
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        #region Explicit implementations
        bool ICommand.CanExecute(object parameter)
        {
            return CanExecute();
        }

        async void ICommand.Execute(object parameter)
        {
            await ExecuteAsync((TParameter)parameter);//.FireAndForgetSafeAsync(_errorHandler);
        }

        async Task IAsyncCommand.ExecuteAsync(object parameter)
        {
            await ExecuteAsync((TParameter)parameter);//.FireAndForgetSafeAsync(_errorHandler);
        }
        #endregion
    }
}
