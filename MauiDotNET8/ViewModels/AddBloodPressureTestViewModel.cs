using CommunityToolkit.Maui.Views;
using MauiDotNET8.Enumerations;
using MauiDotNET8.Helpers.Command;
using MauiDotNET8.Interface.API;
using MauiDotNET8.Modals.API;
using MauiDotNET8.Screens.PopupNotify;
using MauiDotNET8.Validation;
using MauiDotNET8.Validation.Rules;
using MauiDotNET8.ViewModels.Base;
using MauiDotNET8.ViewModels.BloodPressure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MauiDotNET8.ViewModels
{
    public class AddBloodPressureTestViewModel: BaseViewModel<BloodPressureTest>
    {
        public DateTime date;
        public TimeSpan time;
        public ValidatableObject<short?> systolic;
        public ValidatableObject<short?> diastolic;
        public IList<FrequencyNameValue> headaches;
        public FrequencyNameValue selectedHeadache;
        public IList<FrequencyNameValue> blurredVision;
        public FrequencyNameValue selectedBlurredVision;
        public IList<FrequencyNameValue> abdominalPains;
        public FrequencyNameValue selectedAbdominalPains;
        public IEnumerable<TestResponse> testResponses;
        private bool isAlert = false;
        private readonly IBloodPressure bloodPressure;

        public ICommand ValidateSystolicCommand => new Command(() => ValidateSystolic());
        public ICommand ValidateDiastolicCommand => new Command(() => ValidateDiastolic());
        public ICommand AlertPopupCommand;


        public AddBloodPressureTestViewModel()
        {
            SetPropertyValues();
            AddValidations();
            bloodPressure = GetTrakkaclinicalAPI();
        }


        void SetPropertyValues()
        {
            systolic = new ValidatableObject<short?>();
            diastolic = new ValidatableObject<short?>();
            date = DateTime.Now;
            time = date.TimeOfDay;

            headaches = new FrequencyNameValue[] {
                new FrequencyNameValue("No", FrequencyValue.No),
                new FrequencyNameValue("Occasional", FrequencyValue.Occasional),
                new FrequencyNameValue("Frequent", FrequencyValue.Frequent),
                new FrequencyNameValue("Persistent", FrequencyValue.Persistent)
            };

            blurredVision = new FrequencyNameValue[] {
                new FrequencyNameValue("No", FrequencyValue.No),
                new FrequencyNameValue("Occasional", FrequencyValue.Occasional),
                new FrequencyNameValue("Frequent", FrequencyValue.Frequent),
                new FrequencyNameValue("Persistent", FrequencyValue.Persistent)
            };

            abdominalPains = new FrequencyNameValue[] {
                new FrequencyNameValue("No", FrequencyValue.No),
                new FrequencyNameValue("Occasional", FrequencyValue.Occasional),
                new FrequencyNameValue("Frequent", FrequencyValue.Frequent),
                new FrequencyNameValue("Persistent", FrequencyValue.Persistent)
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

        public ValidatableObject<short?> Systolic
        {
            get { return systolic; }
            set { SetProperty(ref systolic, value); }
        }

        public ValidatableObject<short?> Diastolic
        {
            get { return diastolic; }
            set { SetProperty(ref diastolic, value); }
        }

        public IList<FrequencyNameValue> Headaches
        {
            get { return headaches; }
            set { SetProperty(ref headaches, value); }
        }

        public FrequencyNameValue SelectedHeadache
        {
            get { return selectedHeadache; }
            set { SetProperty(ref selectedHeadache, value); }
        }

        public IList<FrequencyNameValue> BlurredVision
        {
            get { return blurredVision; }
            set { SetProperty(ref blurredVision, value); }
        }

        public FrequencyNameValue SelectedBlurredVision
        {
            get { return selectedBlurredVision; }
            set { SetProperty(ref selectedBlurredVision, value); }
        }

        public IList<FrequencyNameValue> AbdominalPains
        {
            get { return abdominalPains; }
            set { SetProperty(ref abdominalPains, value); }
        }

        public FrequencyNameValue SelectedAbdominalPains
        {
            get { return selectedAbdominalPains; }
            set { SetProperty(ref selectedAbdominalPains, value); }
        }

        private BloodPressureTest BloodPressureResult
        {
            get
            {
                BloodPressureTest bloodPressureResult = new BloodPressureTest();
                bloodPressureResult.TestDateTimeUTC = date.Date.Add(time).ToUniversalTime();
                bloodPressureResult.Systolic = Systolic.Value ?? default(short);
                bloodPressureResult.Diastolic = Diastolic.Value ?? default(short);
                bloodPressureResult.Headaches = SelectedHeadache != null ? selectedHeadache.Value : FrequencyValue.NotAnswered;
                bloodPressureResult.BlurredVision = SelectedBlurredVision != null ? selectedBlurredVision.Value : FrequencyValue.NotAnswered;
                bloodPressureResult.AbdominalPain = SelectedAbdominalPains != null ? selectedAbdominalPains.Value : FrequencyValue.NotAnswered;
                bloodPressureResult.Responses = new List<TestResponse>();
                return bloodPressureResult;
            }
        }

        private bool ValidateSystolic()
        {
            return systolic.Validate();
        }

        private bool ValidateDiastolic()
        {
            return diastolic.Validate();
        }

        public bool IsValid
        {
            get { return ValidateSystolic() & ValidateDiastolic(); }
        }

        public bool IsAlert
        {
            get { return isAlert; }
            set { SetProperty(ref isAlert, value); }
        }

        private void AddValidations()
        {
            systolic.Validations.Add(new IsInNumericRangeRule<short?> { ValidationMessage = "Enter a number between 40 and 300", Minimum = 40, Maximum = 300 });
            diastolic.Validations.Add(new IsInNumericRangeRule<short?> { ValidationMessage = "Must be between 20 and 200", Minimum = 20, Maximum = 200 });
        }

        public IAsyncCommand SaveBloodPressureResultCommand
        {
            get
            {
                return new AsyncCommand<BloodPressureTest>(ExecuteSubmitAsync, CanExecuteSubmit);
            }
        }

        //public async Task SaveResult()
        //{
        //    //var command = new AsyncCommand<BloodPressureTest>(ExecuteSubmitAsync, CanExecuteSubmit);
        //    //await command.ExecuteAsync(BloodPressureResult);
        //}

        private async Task<bool> ExecuteSubmitAsync(BloodPressureTest bloodPressureResult)
        {
            try
            {
                IsBusy = true;
                var results = await bloodPressure.PostBloodPressure("glCEJnehDpVwtp/u/rLgEHznsD6cv0U2ygzBNgQLChs0KqLtMELKtA==", bloodPressureResult, await GetAccessToken());
                if (results.Success)
                {
                    TestResponses = results.Data;
                    await CheckForAlerts();
                    return true;
                }
                return false;
            }
            catch(Exception ex)
            {
                IsBusy = false;
            }
            finally
            {
               // Analytics.TrackEvent(Constants.Analytics.BloodPressureTestAdded);
              
            }
            return false;
        }

        public IEnumerable<TestResponse> TestResponses
        {
            get { return testResponses; }
            set { SetProperty(ref testResponses, value); }
        }

        private async Task<bool> CheckForAlerts()
        {
            foreach (TestResponse testResponse in TestResponses)
            {
                if (testResponse.TestResponseLevel == TestResponseLevel.Alert)
                {
                    IsAlert = true;
                    return true;
                }
            }
            return false;
        }

        public async Task<bool> SaveDetails(ActivityIndicator activityIndicator, Button saveResultsButton)
        {

            //if (SaveBloodPressureResultCommand.CanExecute(null))
            //{
                //  this.IsEnabled = false;
                activityIndicator.IsRunning = true;
                saveResultsButton.Text = "Saving your result";

                try
                {
                //await SaveResult();
                    var results =  await ExecuteSubmitAsync(BloodPressureResult);
                    if (results)
                    {
                        activityIndicator.IsRunning = false;
                        return true;
                    }

                    // MessagingCenter.Send<App>((App)Application.Current, Constants.MessagingCentre.RefreshBloodPressures);
                    // await Navigation.RemovePopupPageAsync(this);
                    //if (IsAlert)
                    //{
                    //    var alertPopup = new AlertPopup(this);

                    //    foreach (TestResponse testResponse in TestResponses)
                    //    {
                    //        if (testResponse.TestResponseLevel == TestResponseLevel.Alert)
                    //        {
                    //            switch (testResponse.TestResponseType)
                    //            {
                    //                case TestResponseType.BloodPressureDiastolic:
                    //                    alertPopup.AlertMessages.Add("Your diastolic blood pressure reading is outside the normal range.");
                    //                    break;
                    //                case TestResponseType.BloodPressureSystolic:
                    //                    alertPopup.AlertMessages.Add("Your systolic blood pressure reading is outside the normal range.");
                    //                    break;
                    //                case TestResponseType.BloodPressureHeadaches:
                    //                    alertPopup.AlertMessages.Add("Your persistent headache is not normal.");
                    //                    break;
                    //                case TestResponseType.BloodPressureAbdominalPain:
                    //                    alertPopup.AlertMessages.Add("Your persistent abdominal pain is not normal.");
                    //                    break;
                    //                case TestResponseType.BloodPressureBlurredVision:
                    //                    alertPopup.AlertMessages.Add("Your persistent blurred vision is not normal.");
                    //                    break;
                    //                default:
                    //                    break;
                    //            }

                    //        }
                    //    }
                    //    App.Current.MainPage.ShowPopupAsync(alertPopup);
                    //    // await PopupNavigation.Instance.PushAsync(alertPopup);
                    //}

                }
                catch (Exception ex)
                {
                //TODO notify
                activityIndicator.IsRunning = false;
                return false;
                }
                finally
                {
                    //IsEnabled = true;
                    activityIndicator.IsRunning = false;
                }
           // }
            return false;

        }
    }
}
