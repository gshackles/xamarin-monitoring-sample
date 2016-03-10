using Foundation;
using UIKit;
using Xamarin;
using Xamarin.Forms;
using CompanySearch.Instrumentation;
using HockeyApp;
using System;
using System.Threading.Tasks;

namespace CompanySearch.iOS
{
	[Register("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
            var manager = BITHockeyManager.SharedHockeyManager;
            manager.Configure("YOUR-HOCKEYAPP-APPID");
            manager.CrashManager.CrashManagerStatus = BITCrashManagerStatus.AutoSend;
            manager.StartManager();
            manager.Authenticator.AuthenticateInstallation();

            MetricService.Instance.Initialize(TargetPlatform.iOS);

			Forms.Init();

			LoadApplication(new App());

			return base.FinishedLaunching(app, options);
		}
	}
}