using System;
using MvvmCross.iOS.Views;
using UIKit;
using CoreGraphics;
using ObjCRuntime;
using Foundation;
using System.Diagnostics;
using MvvmCross.Binding.ExtensionMethods;

namespace MvvmCross.StateRestoration.iOS
{
	[Adopts("UIViewControllerRestoration")]
	public class FirstView : MvxViewController<FirstViewModel>
	{
		UILabel lbl;
		UIButton btn;

		const string BackgroundColorKey = "kBackgroundColor";
		const string RestorationID = "FirstView";

		public FirstView()
		{
			RestorationIdentifier = RestorationID;
			RestorationClass = new Class(typeof(FirstView));
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			var screenRect = View.Bounds;

			lbl = new UILabel(new CGRect(20, 50, screenRect.Width - 20, 100));
			lbl.Lines = 0;
			lbl.Text = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

			btn = new UIButton(new CGRect(20, 200, screenRect.Width - 40, 100));
			btn.BackgroundColor = UIColor.White;
			btn.SetTitle("Toggle Background", UIControlState.Normal);
			btn.SetTitleColor(UIColor.Blue, UIControlState.Normal);
			btn.Layer.BorderWidth = 1;
			btn.Layer.BorderColor = UIColor.Purple.CGColor;
			btn.TouchUpInside += OnBackgroundToggleTapped;

			View.BackgroundColor = UIColor.White;
			View.AddSubviews(lbl, btn);
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
			base.EncodeRestorableState(coder);
			coder.Encode(View.BackgroundColor, BackgroundColorKey);
		}
		public override void DecodeRestorableState(Foundation.NSCoder coder)
		{
			base.DecodeRestorableState(coder);

			View.BackgroundColor = (UIColor) coder.DecodeObject(BackgroundColorKey);
		}

		[Export("viewControllerWithRestorationIdentifierPath:coder:")]
		static UIViewController FromIdentifierPath(string[] identifierComponents, NSCoder coder)
		{
			// why isn't this called? Why (according to) docs do I have to set the Restoration properies here
			// as well as in the constructor?
			var vc = new FirstView();
			vc.RestorationIdentifier = identifierComponents[identifierComponents.Length - 1];
			vc.RestorationClass = new Class(typeof(FirstView));
			return vc;
		}

	}
}

