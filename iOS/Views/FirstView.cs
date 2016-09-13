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
	public class FirstView : MvxViewController<FirstViewModel>, IUIViewControllerRestoration
	{
		UILabel _nameLabel;
		UILabel _counterLabel;
		UIButton _addCountButton;
		UIButton _lessCountButton;
		UIButton _toggleBackgroundButton;
		UIButton _navigateButton;

	    const string BackgroundColorKey = "kBackgroundColor";
		const string RestorationID = "FirstView";

        public FirstView() : base()
        {
			this.PrintLaunchState("ctr");
			RestorationIdentifier = RestorationID;
			RestorationClass = new Class(typeof(FirstView));
        }

        public override void ViewDidLoad()
		{
			this.PrintLaunchState("ViewDidLoad");
            base.ViewDidLoad();

			// Set up views
			View.BackgroundColor = UIColor.White;

			_nameLabel = new UILabel { TranslatesAutoresizingMaskIntoConstraints = false };
			_counterLabel = new UILabel { 
				TranslatesAutoresizingMaskIntoConstraints = false,
				TextAlignment = UITextAlignment.Center
			};

			_addCountButton = CreateButton("+");
			_lessCountButton = CreateButton("-");
			_navigateButton = CreateButton("Go to next screen");
			_toggleBackgroundButton = CreateButton("Toggle background color");
			_toggleBackgroundButton.TouchUpInside += OnToggleButtonPressed;

			View.AddSubviews(_nameLabel,
				 _counterLabel,
				 _addCountButton,
				 _lessCountButton,
				 _toggleBackgroundButton,
				 _navigateButton);

			// Set up constraints
			var viewsAndMetrics = new object[] {
				"name", _nameLabel,
				"counter", _counterLabel,
				"addBtn", _addCountButton,
				"removeBtn", _lessCountButton,
				"toggleBgBtn", _toggleBackgroundButton,
				"navigateBtn", _navigateButton
			};

			View.AddConstraints(NSLayoutConstraint.FromVisualFormat(
				"V:|-60-[name(45)]-10-[addBtn(45)]-10-[toggleBgBtn(45)]-10-[navigateBtn(45)]-(>=10)-|", 0, viewsAndMetrics));
			View.AddConstraints(NSLayoutConstraint.FromVisualFormat(
				"V:[counter(45)]", 0, viewsAndMetrics));
			View.AddConstraints(NSLayoutConstraint.FromVisualFormat(
				"V:[removeBtn(45)]", 0, viewsAndMetrics));

			View.AddConstraints(NSLayoutConstraint.FromVisualFormat(
				"H:|-[name]-|", 0, viewsAndMetrics));
			View.AddConstraints(NSLayoutConstraint.FromVisualFormat(
				"H:[removeBtn(45)]-10-[counter(45)]-10-[addBtn(45)]", NSLayoutFormatOptions.AlignAllCenterY, viewsAndMetrics));
			View.AddConstraint(NSLayoutConstraint.Create(_counterLabel, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, View, NSLayoutAttribute.CenterX, 1, 0));
			View.AddConstraints(NSLayoutConstraint.FromVisualFormat(
				"H:|-[toggleBgBtn]-|", 0, viewsAndMetrics));
			View.AddConstraints(NSLayoutConstraint.FromVisualFormat(
				"H:|-[navigateBtn]-|", 0, viewsAndMetrics));


			// Set up bindings
			var bindingSet = this.CreateBindingSet<FirstView, FirstViewModel>();

			bindingSet.Bind(_nameLabel).To(vm => vm.Name);
			bindingSet.Bind(_counterLabel).To(vm => vm.Count);
			bindingSet.Bind(_addCountButton).To(vm => vm.AddOneCommand);
			bindingSet.Bind(_lessCountButton).To(vm => vm.LessOneCommand);
			bindingSet.Bind(_navigateButton).To(vm => vm.NavigateCommand);

			bindingSet.Apply();

		}

		// State restore
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
			DebugUtil.PrintLaunchState(typeof(FirstView), "FromIdentifierPath");

            var request = new MvxViewModelRequest(typeof(FirstViewModel), null, null, null);
            var vml = Mvx.Resolve<IMvxViewModelLoader>();
            var vm = vml.LoadViewModel(request, null) as FirstViewModel;
            var firstView = new FirstView
            {
                ViewModel = vm
            };
            return firstView;
        }

		UIButton CreateButton(string title)
		{
			var btn = new UIButton
			{
				TranslatesAutoresizingMaskIntoConstraints = false,
				BackgroundColor = UIColor.White,
				Layer = {
					BorderWidth = 1,
					CornerRadius = 4,
					BorderColor = UIColor.Gray.CGColor
				}
			};
			btn.SetTitle(title, UIControlState.Normal);
			btn.SetTitleColor(UIColor.Blue, UIControlState.Normal);
			return btn;
		}

		void OnToggleButtonPressed(object sender, EventArgs e)
		{
			if (View.BackgroundColor == UIColor.White)
			{
				View.BackgroundColor = UIColor.Magenta;
			}
			else if (View.BackgroundColor == UIColor.Magenta) 
			{
				View.BackgroundColor = UIColor.White;
			}
		}
    }
}

