using MauiDotNET8.Enumerations;
using MauiDotNET8.Interface.API;
using MauiDotNET8.Modals.API;
using MauiDotNET8.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiDotNET8.ViewModels.UrineProtein
{
    public class UrineProteinViewModel:BaseViewModel<UrineProteinTest>
    {
        private ObservableCollection<UrineProteinTestAndResponse> urineProteinTestAndResponses;
        private bool hasNoUrineProteinTests = false;
        private readonly IUrineProtine urineProtine;
        public UrineProteinViewModel()
        {
           urineProtine = GetIUrineProtinelAPI();
        }

        public ObservableCollection<UrineProteinTestAndResponse> UrineProteinTestAndResponses
        {
            get { return urineProteinTestAndResponses; }
            set
            {
                urineProteinTestAndResponses = value;
                OnPropertyChanged(nameof(UrineProteinTestAndResponses));
            }
        }

        public bool HasNoUrineProteinTests
        {
            get { return hasNoUrineProteinTests; }
            set { SetProperty(ref hasNoUrineProteinTests, value); }
        }

        public async Task GetUrineProteinTests()
        {
            try
            {
                IsBusy = true;
                await Task.Delay(250);
                var urineProteinTestResults = await urineProtine.GetUrineProtineResults("glCEJnehDpVwtp/u/rLgEHznsD6cv0U2ygzBNgQLChs0KqLtMELKtA==", await GetAccessToken());
                if (urineProteinTestResults.Any())
                {
                    UrineProteinTestAndResponses = new ObservableCollection<UrineProteinTestAndResponse>();

                    HasNoUrineProteinTests = urineProteinTestResults.Any() == true ? false : true;

                    foreach (UrineProteinTest test in urineProteinTestResults)
                    {
                        var testAndResponse = new UrineProteinTestAndResponse()
                        {
                            UrineProteinLevel = test.UrineProteinLevel,
                            TestDateTimeUTC = test.TestDateTimeUTC,
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
                                case TestResponseType.UrinProteinProteinLevel:
                                    testAndResponse.UrineProteinResponse = response.TestResponseLevel;
                                    break;
                                default:
                                    break;
                            }
                        }
                        testAndResponse.Responses = responses;
                        UrineProteinTestAndResponses.Add(testAndResponse);
                    }
                }
                
                IsBusy = false;
            }
            catch(Exception ex)
            {
                IsBusy = false;
            }

        }
    }
}
