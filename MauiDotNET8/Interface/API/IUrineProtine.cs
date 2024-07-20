using MauiDotNET8.Modals.API;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiDotNET8.Interface.API
{
    public interface IUrineProtine
    {
        [Get("/api/TestResults/UrineProtein")]
        Task<UrineProteinTest[]> GetUrineProtineResults([AliasAs("code")] string code, [Authorize("Bearer")] string token);
    }
}
