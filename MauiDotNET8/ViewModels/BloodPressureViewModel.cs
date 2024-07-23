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
        private ObservableCollection<BloodPressureTestAndResponse> bloodPressureTestAndResponses;
        private bool hasNoBloodPressureTests = false;
        private ObservableCollection<ChartDataModel> mapData { get; set; }
        public BloodPressureViewModel()
        {
            Title = "Your Blood Pressure";
            bloodPressure = GetTrakkaclinicalAPI();

            //MapData = new ObservableCollection<ChartDataModel>
            //{
            //    new ChartDataModel { DateTime = "Jan", High = 111, Low = 222 },
            //    new ChartDataModel { DateTime = "Feb", High = 145, Low = 255 },
            //    new ChartDataModel { DateTime = "Mar", High = 45, Low = 58 },
            //    new ChartDataModel { DateTime = "Apr", High = 68, Low = 58 },
            //    new ChartDataModel { DateTime = "May", High = 177, Low = 200 },
            //    new ChartDataModel { DateTime = "Jun", High = 222, Low = 22 },
            //};
        }

        public ObservableCollection<ChartDataModel> MapData 
        {
            get
            {
                return mapData;
            }
            set
            {
                mapData = value;
                OnPropertyChanged(nameof(MapData));
            }
        }

        public string XAxesTitle
        {
            get
            {
                return "DateTime";
            }
        }
        public string YAxesTitle 
        { 
            get 
            {
                return "Value"; 
            } 
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
            MapData = new ObservableCollection<ChartDataModel>();
            try
            {
                var bloodPressureResults = await bloodPressure.GteBloofPresureResults("glCEJnehDpVwtp/u/rLgEHznsD6cv0U2ygzBNgQLChs0KqLtMELKtA==", await GetAccessToken());
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

                   MapData.Add(new ChartDataModel() { DateTime = test.TestDateTimeUTC.ToString("yyyy-MM-dd"), High = (Double)test.Systolic, Low =(Double)test.Diastolic});

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

    public class ChartDataModel
    {
        public string DateTime { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
    }
}
