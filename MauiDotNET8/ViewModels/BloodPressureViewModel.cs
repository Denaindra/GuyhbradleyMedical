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
                var resulttoken = "eyJhbGciOiJSUzI1NiIsImtpZCI6InVTLTljR0ozMzZkZjJoU3B0V2Nza2MxSEtKMktPMmprNldlMnNSdTY4UVEiLCJ0eXAiOiJKV1QifQ.eyJhdWQiOiIwMmMwYmE1ZS0wOGZiLTQ1MzktODM5OS1iYjcyNWIxOGNmZTYiLCJpc3MiOiJodHRwczovL3RyYWtrYWNsaW5pY2FsLmIyY2xvZ2luLmNvbS9lOTJiY2Q5NS03YWFhLTQwMTUtODIwZS02NTkxNTkzMGFiOWIvdjIuMC8iLCJleHAiOjE3MjEwMTQ2NzAsIm5iZiI6MTcyMTAxMTA3MCwic3ViIjoiZGRlNjRhYjAtMjE5MC00MjYzLTljYTAtM2UyOTI2ZWM3MzkzIiwib2lkIjoiZGRlNjRhYjAtMjE5MC00MjYzLTljYTAtM2UyOTI2ZWM3MzkzIiwibmFtZSI6Ikd1eSBCcmFkbGV5IiwiZ2l2ZW5fbmFtZSI6Ikd1eSIsImZhbWlseV9uYW1lIjoiQnJhZGxleSIsImVtYWlsIjoidHJha2thLmNsaW5pY2FsQGdtYWlsLmNvbSIsImdyb3VwcyI6WyJQYXRpZW50cyJdLCJtYW5hZ2VyIjoiYjUxMjRhNjQtYTgwZi00NmU3LWEwYTctNGY5ZTRiYjQ5MjAzIiwiY2xpbmljIjoiODE5ZDJiZmItYzVlZi00ZDczLWJiNjMtNGY5Yjc3YWY5MjYyIiwidGlkIjoiZTkyYmNkOTUtN2FhYS00MDE1LTgyMGUtNjU5MTU5MzBhYjliIiwic2NwIjoiVGVzdFJlc3VsdHMuUmVhZFdyaXRlIiwiYXpwIjoiMzAzMjhhZmItZTc0My00YmRkLWEyNjEtZjJjMDJlZmUyZTJiIiwidmVyIjoiMS4wIiwiaWF0IjoxNzIxMDExMDcwfQ.KZ055RRj24kVZ3AMg9H0IbP8OAqhrLL5JYCzFY8AuQ1Z7-mFjoYXc45hSOKWz6kN_FK8J1TR41t4WUCfAor4lb3p1i-Y3U6wCpSQHalr4ODCmPCeY4eRByLX8wrlPMVAPexSxtUoSFVQls5ldpdSccp6-WUq0E0_4VMGXy1EQeovaSK-0mEHHie9Sh00ZATfOJWhIlHd9ivOurC0O9tNHpaBzYVwjuWFn9zKfDqCvk7wwOOhKUgURMGpTVv9EAzPsNfgrJEufM8Fvc_38_uUILtgJ7zwzdFILFf8FcWC23b9Oq_vHrOtMDFuf8f8OD-88oOoO7amX4jHSYP__5qfHw";
                var bloodPressureResults = await bloodPressure.GteBloofPresureResults("glCEJnehDpVwtp/u/rLgEHznsD6cv0U2ygzBNgQLChs0KqLtMELKtA==", resulttoken);

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
