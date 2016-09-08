using Foundation;
using UIKit;
using MvvmCross.iOS.Views.Presenters;
using MvvmCross.iOS.Platform;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using ObjCRuntime;

namespace MvvmCross.StateRestoration.iOS
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the
	// User Interface of the application, as well as listening (and optionally responding) to application events from iOS.
	[Register("AppDelegate")]
	public class AppDelegate : MvxApplicationDelegate
	{
		// class-level declarations

		public override UIWindow Window
		{
			get;
			set;
		}

		public override bool WillFinishLaunching(UIApplication application, NSDictionary launchOptions)
		{
			// Override point for customization after application launch.
			// If not required for your application you can safely delete this method
			if (Window == null)
			{
				Window = new UIWindow(UIScreen.MainScreen.Bounds);
			}

			var presenter = new MvxIosViewPresenter(this, Window);
			var setup = new Setup(this, presenter);
			setup.Initialize();

			// This initialized with a certian ViewModel - how do I get this to play with state restoration?
			var startup = Mvx.Resolve<IMvxAppStart>();
			startup.Start();

			return true;
		}

		public override UIViewController GetViewController(UIApplication application, string[] restorationIdentifierComponents, NSCoder coder)
		{
			// How do I get ViewControllers to restore their own state?
			return new UIViewController();
		}


		public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
		{

			Window.MakeKeyAndVisible();

			var rootVC = UIApplication.SharedApplication.KeyWindow.RootViewController;
			rootVC.RestorationIdentifier = "RootViewController";


			return true;
		}

		public override bool ShouldSaveApplicationState(UIApplication application, NSCoder coder)
		{
			return true;
		}

		public override bool ShouldRestoreApplicationState(UIApplication application, NSCoder coder)
		{
			return true;
		}

	}
}


