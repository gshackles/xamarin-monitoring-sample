using System;
using System.Threading.Tasks;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using CompanySearch.Instrumentation;
using Xamarin;
using Xamarin.Forms;

namespace CompanySearch.Droid
{
	[Activity (Label = "Company Search", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
	{
		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

            //HockeyApp.Android.CrashManager.Register (this, "YOUR-HOCKEYAPP-APPID", new CompanySearchCrashManagerListener());
            //HockeyApp.Android.UpdateManager.Register (this, "YOUR-HOCKEYAPP-APPID");

            MetricService.Instance.Initialize(TargetPlatform.Android);

			Forms.Init (this, savedInstanceState);
			LoadApplication (new App ());
		}

        private class CompanySearchCrashManagerListener : HockeyApp.Android.CrashManagerListener
        {
            public override bool ShouldAutoUploadCrashes() => true;
        }
	}
}