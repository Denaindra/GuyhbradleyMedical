using Android.App;
using Microsoft.Identity.Client;
using Android.Content;
namespace MauiDotNET8.Platforms.Android
{
    [Activity(Exported = true)]
    [IntentFilter(new[] { Intent.ActionView },
        Categories = new[] { Intent.CategoryBrowsable, Intent.CategoryDefault },
        DataHost = "auth",
        DataScheme = "msal30328afb-e743-4bdd-a261-f2c02efe2e2b")]
    public class MsalActivity : BrowserTabActivity
    {
    }
}
