using Android.App;
using Microsoft.Identity.Client;

namespace DayCare.Platforms.Android;

// Replace YOUR_CLIENT_ID in DataScheme with the Application (client) ID from your Azure AD app registration.
// This value must match the ClientId constant in Services/MsalAuthService.cs.
[Activity(Exported = true)]
[IntentFilter(new[] { Android.Content.Intent.ActionView },
    Categories = new[] { Android.Content.Intent.CategoryBrowsable, Android.Content.Intent.CategoryDefault },
    DataHost = "auth",
    DataScheme = "msalYOUR_CLIENT_ID")]
public class MsalActivity : BrowserTabActivity
{
}
