using Android.App;
using Microsoft.Identity.Client;

namespace DayCare.Platforms.Android;

// Replace YOUR_CLIENT_ID in DataScheme with the Application (client) ID from your Azure AD app registration.
// This value must match the ClientId constant in Services/MsalAuthService.cs.
[Activity(Exported = true)]
[IntentFilter(new[] { global::Android.Content.Intent.ActionView },
    Categories = new[] { global::Android.Content.Intent.CategoryBrowsable, global::Android.Content.Intent.CategoryDefault },
    DataHost = "auth",
    DataScheme = "msalYOUR_CLIENT_ID")]
public class MsalActivity : BrowserTabActivity
{
}
