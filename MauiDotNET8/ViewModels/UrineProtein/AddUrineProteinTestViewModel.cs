using MauiDotNET8.Enumerations;
using MauiDotNET8.Helpers.Command;
using MauiDotNET8.Interface.API;
using MauiDotNET8.Modals.API;
using MauiDotNET8.Screens.PopupNotify;
using MauiDotNET8.Screens.PopupViews;
using MauiDotNET8.Validation;
using MauiDotNET8.Validation.Rules;
using MauiDotNET8.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MauiDotNET8.ViewModels.UrineProtein
{
    public class AddUrineProteinTestViewModel: BaseViewModel<UrineProteinTest>
    {
        private DateTime date;
        private TimeSpan time;
        private IList<ValidatableObject<ProteinLevelNameValue>> proteinLevel;
        private ValidatableObject<ProteinLevelNameValue> selectedProteinLevel;
        private IUrineProtine urineProtine;
        public ICommand ValidateProteinLevelCommand => new Command(() => ValidateProteinLevel());
        public ICommand AlertPopupCommand;

        public AddUrineProteinTestViewModel()
        {
            this.urineProtine = GetIUrineProtinelAPI();   
        }
        public void SetPropertyValues()
        {
            date = DateTime.Now;
            time = date.TimeOfDay;
            selectedProteinLevel = new ValidatableObject<ProteinLevelNameValue>();

            proteinLevel = new ValidatableObject<ProteinLevelNameValue>[] {
                new ValidatableObject<ProteinLevelNameValue>
                {
                    Value = new ProteinLevelNameValue(UrineProteinLevel.Nil.ToString(), UrineProteinLevel.Nil)
                },
                new ValidatableObject<ProteinLevelNameValue>
                {
                    Value = new ProteinLevelNameValue(UrineProteinLevel.Trace.ToString(),UrineProteinLevel.Trace)
                },
                new ValidatableObject<ProteinLevelNameValue>
                {
                    Value = new ProteinLevelNameValue("1*", Enumerations.UrineProteinLevel.OneStar)
                },
                new ValidatableObject<ProteinLevelNameValue>
                {
                    Value = new ProteinLevelNameValue("2**", Enumerations.UrineProteinLevel.TwoStars)
                },
                new ValidatableObject<ProteinLevelNameValue>
                {
                    Value = new ProteinLevelNameValue("3***", Enumerations.UrineProteinLevel.ThreeStars)
                }
            };
        }
        public TimeSpan Time
        {
            get { return time; }
            set { SetProperty(ref time, value); }
        }
        public DateTime Date
        {
            get { return date; }
            set { SetProperty(ref date, value); }
        }
        public IList<ValidatableObject<ProteinLevelNameValue>> ProteinLevel
        {
            get { return proteinLevel; }
            set { SetProperty(ref proteinLevel, value); }
        }
        public ValidatableObject<ProteinLevelNameValue> SelectedProteinLevel
        {
            get {
                try
                {
                    var results = selectedProteinLevel.Errors;
                }
                catch(Exception ex)
                {

                }
                return selectedProteinLevel; 
            }
            set { 
                SetProperty(ref selectedProteinLevel, value); 
            }
        }
        private UrineProteinTest UrineProteinTest
        {
            get
            {
                UrineProteinTest test = new UrineProteinTest();
                test.TestDateTimeUTC = date.Date.Add(time).ToUniversalTime();
                test.UrineProteinLevel = selectedProteinLevel.Value.Value;
                return test;
            }
        }
        public IAsyncCommand SaveUrineProteinTestCommand
        {
            get
            {
                return new AsyncCommand<UrineProteinTest>(ExecuteSubmitAsync, CanExecuteSubmit);
            }
        }
        public async Task SaveResult()
        {

            var command = new AsyncCommand<UrineProteinTest>(ExecuteSubmitAsync, CanExecuteSubmit);
            await command.ExecuteAsync(UrineProteinTest);
        }
        private async Task ExecuteSubmitAsync(UrineProteinTest test)
        {
            try
            {
                IsBusy = true;
                urineProtine.PopstUrineProtineResults("glCEJnehDpVwtp/u/rLgEHznsD6cv0U2ygzBNgQLChs0KqLtMELKtA==", UrineProteinTest, await GetAccessToken());
            }
            catch(Exception e)
            {

            }
            finally
            {
                IsBusy = false;
            }
        }
        public bool IsValid
        {
            get { return ValidateProteinLevel(); }
        }
        private bool ValidateProteinLevel()
        {
            return SelectedProteinLevel.Validate();
        }
        public void AddValidations()
        {
            SelectedProteinLevel.Validations.Add(new IsDropDownValueSelectedRule<ProteinLevelNameValue> { ValidationMessage = "Please select a blood protein level" });
        }
        public async void SaveResultsPopups(bool isRunning, Button saveResultsButton)
        {
            bool answer = await App.Current.MainPage.DisplayAlert("Confirmation Required", GetConfirmationAlertText(), "Yes", "No");
            if (answer)
            {
                if (SaveUrineProteinTestCommand.CanExecute(null))
                {
                    isRunning = true;
                    saveResultsButton.Text = "Saving your result";
                    try
                    {
                        await SaveResult();
                        if (SelectedProteinLevel.Value.Value == UrineProteinLevel.ThreeStars)
                        {
                            AlertPopupCommand.Execute(null);
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                    finally
                    {
                        isRunning = false;
                    }
                }
            }
        }
        private string GetConfirmationAlertText()
        {
            var text = "Please confirm you wish to save the following Urine Protein Test details:" + Environment.NewLine +
                Environment.NewLine + "Urine Protein Level: " + SelectedProteinLevel.Value.Name + Environment.NewLine;
            return text;
        }
    }
}
