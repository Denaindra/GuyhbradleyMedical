using MauiDotNET8.Modals.API;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiDotNET8.Interface.API
{
    public interface IBloodPressure
    {
        [Get("/api/TestResults/BloodPressure")]
        Task<List<BloodPressureTest>> GteBloofPresureResults([AliasAs("code")] string code, [Authorize("Bearer")] string token);

        [Get("/api/Clinic/MobileStartup")]
        Task<ClinicContactModal> GteMobileServiceResults([AliasAs("code")] string code, [Authorize("Bearer")] string token);

        [Post("/api/TestResults/BloodPressure")]
        Task<Modals.API.ApiResponse<IEnumerable<TestResponse>>> PostBloodPressure([AliasAs("code")] string code, BloodPressureTest bloodPressure, [Authorize("Bearer")] string token);

    }
}
