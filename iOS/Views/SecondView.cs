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
using MvvmCross.Platform.iOS;

namespace MvvmCross.StateRestoration.iOS
{
	public class SecondView : MvxViewController<SecondViewModel>, IUIViewControllerRestoration
	{
		UILabel _nameLabel;
		UITextView _textView;

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

			// Set up views
			View.BackgroundColor = UIColor.White;

			_nameLabel = new UILabel { TranslatesAutoresizingMaskIntoConstraints = false };

			_textView = new UITextView
			{
				TranslatesAutoresizingMaskIntoConstraints = false,
				Layer = {
					BorderWidth = 1,
					CornerRadius = 4,
					BorderColor = UIColor.Gray.CGColor
				}
			};

			View.AddSubviews(_nameLabel, _textView);

			// Set up constraints
			var viewsAndMetrics = new object[] {
				"name", _nameLabel,
				"textView", _textView,
			};

			View.AddConstraints(NSLayoutConstraint.FromVisualFormat(
				"V:|-60-[name(45)]-10-[textView]-20-|", 0, viewsAndMetrics));
			View.AddConstraints(NSLayoutConstraint.FromVisualFormat(
				"H:|-[name]-|", 0, viewsAndMetrics));
			View.AddConstraints(NSLayoutConstraint.FromVisualFormat(
				"H:|-[textView]-|", 0, viewsAndMetrics));

			// Set up bindings
			var bindingSet = this.CreateBindingSet<SecondView, SecondViewModel>();
			bindingSet.Bind(_nameLabel).To(vm => vm.Title);
			bindingSet.Bind(_textView).For(v => v.Text).To(vm => vm.EntryText);
			bindingSet.Apply();
		}

		public override void EncodeRestorableState(Foundation.NSCoder coder)
		{
			this.PrintLaunchState("EncodeRestorableState");
			base.EncodeRestorableState(coder);
		}
		public override void DecodeRestorableState(Foundation.NSCoder coder)
		{
			this.PrintLaunchState("DecodeRestorableState");
			base.DecodeRestorableState(coder);
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

