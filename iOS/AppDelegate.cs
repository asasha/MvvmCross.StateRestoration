using Foundation;
using UIKit;
using MvvmCross.iOS.Views.Presenters;
using MvvmCross.iOS.Platform;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using ObjCRuntime;

namespace MvvmCross.StateRestoration.iOS
{
	//Environment.GetFolderPath(Environment.SpecialFolder);

	[Register("AppDelegate")]
	public class AppDelegate : MvxApplicationDelegate
	{
	    bool _didDecode;

		public override UIWindow Window
		{
			get;
			set;
		}

		public override bool WillFinishLaunching(UIApplication application, NSDictionary launchOptions)
		{
			
			this.PrintLaunchState("WillFinishLaunching");

			if (Window == null)
			{
				Window = new UIWindow(UIScreen.MainScreen.Bounds);
			}

            var presenter = new SuperMvxIosViewPresenter(this, Window);
            var setup = new Setup(this, presenter);
            setup.Initialize();

            return true;
		}

	    public override void DidDecodeRestorableState(UIApplication application, NSCoder coder)
	    {
			this.PrintLaunchState("DidDecodeRestorableState");
	        _didDecode = true;
	    }

	    public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
			this.PrintLaunchState("FinishedLaunching");

            if (!_didDecode)
            {
                var startup = Mvx.Resolve<IMvxAppStart>();
                startup.Start();
            }

            Window.MakeKeyAndVisible();

            return true;
        }

        public override UIViewController GetViewController(UIApplication application, string[] restorationIdentifierComponents, NSCoder coder)
		{
			// GetViewController is used only to restore the navigation controller
			// can we check that a presenter exists?
			// can/should we serialize?
			this.PrintLaunchState("GetViewController");

            var presenter = new SuperMvxIosViewPresenter(this, Window);
			var nav = presenter.CreateRestoredNavigationController();
		    Window.RootViewController = nav;
		    return nav;
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

    public class SuperMvxIosViewPresenter : MvxIosViewPresenter
    {
		const string restorationId = "NavigationController";

		public SuperMvxIosViewPresenter(IUIApplicationDelegate applicationDelegate, UIWindow window) : base(applicationDelegate, window)
        {
        }

        public UINavigationController CreateRestoredNavigationController()
        {
            var navController =  new UINavigationController();
			navController.RestorationIdentifier = restorationId;
			return navController;
        }

		protected override UINavigationController CreateNavigationController(UIViewController viewController)
		{
			var navController = base.CreateNavigationController(viewController);
			navController.RestorationIdentifier = restorationId;
			return navController;
		}
    }
}


