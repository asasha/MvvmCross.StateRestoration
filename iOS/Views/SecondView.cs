using System;
using MvvmCross.iOS.Views;
using UIKit;
using CoreGraphics;
using ObjCRuntime;
using Foundation;
using System.Diagnostics;
using MvvmCross.Binding.ExtensionMethods;
using MvvmCross.Core.ViewModels;
using MvvmCross.iOS.Platform;
using MvvmCross.Platform;
using MvvmCross.Binding.BindingContext;

namespace MvvmCross.StateRestoration.iOS
{
	public class SecondView : MvxViewController<SecondViewModel>, IUIViewControllerRestoration
	{
		UILabel lbl;
		UIButton btn;

		const string BackgroundColorKey = "kBackgroundColor";
		const string RestorationID = "SecondView";

        public SecondView() : base()
        {
			this.PrintLaunchState("ctr");
			RestorationIdentifier = RestorationID;
			RestorationClass = new Class(typeof(SecondView));
        }


        public override void ViewDidLoad()
		{
			this.PrintLaunchState("ViewDidLoad");
            base.ViewDidLoad();
            var screenRect = View.Bounds;

			lbl = new UILabel(new CGRect(20, 50, screenRect.Width - 20, 100));
			lbl.Lines = 0;

			btn = new UIButton(new CGRect(20, 200, screenRect.Width - 40, 100));
			btn.BackgroundColor = UIColor.White;
			btn.SetTitle("Toggle Background", UIControlState.Normal);
			btn.SetTitleColor(UIColor.Blue, UIControlState.Normal);
			btn.Layer.BorderWidth = 1;
			btn.Layer.BorderColor = UIColor.Purple.CGColor;
			btn.TouchUpInside += OnBackgroundToggleTapped;

			View.BackgroundColor = UIColor.White;
			View.AddSubviews(lbl, btn);

			// Set up bindings
			var bindingSet = this.CreateBindingSet<SecondView, SecondViewModel>();
			bindingSet.Bind(lbl).To(vm => vm.Title);
			bindingSet.Apply();
		}

		void OnBackgroundToggleTapped(object sender, EventArgs e)
		{
			if (View.BackgroundColor == UIColor.White)
			{
				View.BackgroundColor = UIColor.LightGray;
			}
			else {
				View.BackgroundColor = UIColor.White;
			}
		}


		public override void EncodeRestorableState(Foundation.NSCoder coder)
		{
			this.PrintLaunchState("EncodeRestorableState");
			base.EncodeRestorableState(coder);
			coder.Encode(View.BackgroundColor, BackgroundColorKey);
		}
		public override void DecodeRestorableState(Foundation.NSCoder coder)
		{
			this.PrintLaunchState("DecodeRestorableState");
			base.DecodeRestorableState(coder);
			View.BackgroundColor = (UIColor) coder.DecodeObject(BackgroundColorKey);
		}

        [Export("viewControllerWithRestorationIdentifierPath:coder:")]
        static UIViewController FromIdentifierPath(string[] identifierComponents, NSCoder coder)
        {
			DebugUtil.PrintLaunchState(typeof(SecondView), "FromIdentifierPath");

            var request = new MvxViewModelRequest(typeof(SecondViewModel), null, null, null);
            var vml = Mvx.Resolve<IMvxViewModelLoader>();
            var vm = vml.LoadViewModel(request, null) as SecondViewModel;
            var secondView = new SecondView()
            {
                ViewModel = vm
            };
            return secondView;
        }
    }
}

