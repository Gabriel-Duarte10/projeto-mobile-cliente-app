using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using AndroidX.AppCompat.App;
using AndroidUri = Android.Net.Uri;
namespace projeto_mobile_cliente_app;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
// Para URLs HTTP
[IntentFilter(new[] { Intent.ActionView },
     Categories = new[]
     {
         Intent.ActionView,
         Intent.CategoryDefault,
         Intent.CategoryBrowsable
     },
     DataScheme = "http", DataHost = "projetomobile-f277d.firebaseapp.com", DataPathPrefix = "/cliente/redefinir-senha"
     )
 ]

// Para URLs HTTPS
[IntentFilter(new[] { Intent.ActionView },
     Categories = new[]
     {
         Intent.ActionView,
         Intent.CategoryDefault,
         Intent.CategoryBrowsable
     },
     DataScheme = "https", DataHost = "projetomobile-f277d.firebaseapp.com", DataPathPrefix = "/cliente/redefinir-senha"
     )
 ]
public class MainActivity : MauiAppCompatActivity
{
    protected override void OnCreate(Bundle bundle)
    {
        base.OnCreate(bundle); // Essa chamada é necessária para preservar o comportamento normal do ciclo de vida do Android.
        Delegate.SetLocalNightMode(AppCompatDelegate.ModeNightNo);

        if (Intent.Action == Intent.ActionView)
        {
            AndroidUri uri = Intent.Data;

            if (uri != null && uri.Path.StartsWith("/cliente/redefinir-senha"))
            {
                string token = uri.GetQueryParameter("token");
                if (!string.IsNullOrEmpty(token))
                {
                    App.OnPasswordResetLinkReceived(token);
                }
            }
        }
    }
}
