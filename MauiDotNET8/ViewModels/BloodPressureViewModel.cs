using MauiDotNET8.Enumerations;
using MauiDotNET8.Interface.API;
using MauiDotNET8.Modals.API;
using MauiDotNET8.Utilities;
using MauiDotNET8.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiDotNET8.ViewModels
{
    public class BloodPressureViewModel: BaseViewModel<BloodPressureTest>
    {
        private readonly IBloodPressure bloodPressure;
        ObservableCollection<BloodPressureTestAndResponse> bloodPressureTestAndResponses;
        bool hasNoBloodPressureTests = false;
        public BloodPressureViewModel()
        {
            Title = "Your Blood Pressure";
            bloodPressure = GetTrakkaclinicalAPI();
        }

        public ObservableCollection<BloodPressureTestAndResponse> BloodPressureTestAndResponses
        {
            get { 
                return bloodPressureTestAndResponses; 
            }
            set { 
                 bloodPressureTestAndResponses = value;
                OnPropertyChanged(nameof(BloodPressureTestAndResponses));
            }
        }

        public bool HasNoBloodPressureTests
        {
            get { return hasNoBloodPressureTests; }
            set { SetProperty(ref hasNoBloodPressureTests, value); }
        }

        public async Task<bool> GetBloodPressureTests()
        {

            IsBusy = true;
            try
            {
                var bloodPressureResults = await bloodPressure.GteBloofPresureResults("glCEJnehDpVwtp/u/rLgEHznsD6cv0U2ygzBNgQLChs0KqLtMELKtA==", AppConstant.apptoken);
                HasNoBloodPressureTests = bloodPressureResults.Any() == true ? false : true;

                if (bloodPressureResults.Any())
                {
                    BloodPressureTestAndResponses = new ObservableCollection<BloodPressureTestAndResponse>();
                }
                foreach (BloodPressureTest test in bloodPressureResults)
                {
                    var testAndResponse = new BloodPressureTestAndResponse()
                    {
                        TestDateTimeUTC = test.TestDateTimeUTC,
                        Systolic = test.Systolic,
                        Diastolic = test.Diastolic
                    };

                    var responses = new List<TestResponse>();
                    foreach (TestResponse response in test.Responses)
                    {
                        responses.Add(new TestResponse()
                        {
                            TestResponseLevel = response.TestResponseLevel,
                            TestResponseType = response.TestResponseType
                        });

                        switch (response.TestResponseType)
                        {
                            case TestResponseType.BloodPressureSystolic:
                                testAndResponse.SystolicResponse = response.TestResponseLevel;
                                break;
                            case TestResponseType.BloodPressureDiastolic:
                                testAndResponse.DiastolicResponse = response.TestResponseLevel;
                                break;
                            case TestResponseType.BloodPressureHeadaches:
                                testAndResponse.Headaches = test.Headaches;
                                testAndResponse.HeadachesResponse = response.TestResponseLevel;
                                break;
                            case TestResponseType.BloodPressureBlurredVision:
                                testAndResponse.BlurredVision = test.BlurredVision;
                                testAndResponse.BlurredVisionResponse = response.TestResponseLevel;
                                break;
                            case TestResponseType.BloodPressureAbdominalPain:
                                testAndResponse.AbdominalPain = test.AbdominalPain;
                                testAndResponse.AbdominalPainResponse = response.TestResponseLevel;
                                break;
                            default:
                                break;
                        }
                    }
                    testAndResponse.Responses = responses;
                    BloodPressureTestAndResponses.Add(testAndResponse);
                }

                IsBusy = false;
            }
            catch(Exception ex)
            {
                IsBusy = false;
            }

            return true;

        }
    }
}
