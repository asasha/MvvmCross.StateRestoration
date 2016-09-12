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

namespace MvvmCross.StateRestoration.iOS
{
	public class SecondView : MvxViewController<SecondViewModel>, IUIViewControllerRestoration
	{
		UILabel lbl;
		UIButton btn;

		const string BackgroundColorKey = "kBackgroundColor";
		const string RestorationID = "MyRestoreIDSecondView";

        //
        // Summary:
        //     Default constructor that initializes a new instance of this class with no parameters.
        public SecondView() : base()
        {
            InitMePlease();
        }

        ////
        //// Summary:
        ////     A constructor that initializes the object from the data stored in the unarchiver
        ////     object.
        ////
        //// Parameters:
        ////   coder:
        ////     The unarchiver object.
        ////
        //// Remarks:
        ////     This constructor is provided to allow the class to be initialized from an unarchiver
        ////     (for example, during NIB deserialization). This is part of the Foundation.NSCoding
        ////     protocol.
        ////     If you want to create a subclass of this object and continue to support deserialization
        ////     from an archive, you should implement a constructor with an identical signature:
        ////     taking a single parameter of type Foundation.NSCoder and decorate it with the
        ////     [Export("initWithCoder:"] attribute declaration.
        ////     The state of this object can also be serialized by using the companion method,
        ////     EncodeTo.
        //public FirstView(NSCoder coder) : base(coder)
        //{
        //    Console.WriteLine(coder);
        //    InitMePlease();
        //}

        //
        // Summary:
        //     A constructor used when creating a view controller using the information that
        //     is stored in the nib file.
        //
        // Parameters:
        //   nibName:
        //     The NIB name, or null.
        //     This parameter can be null.
        //
        //   bundle:
        //     The bundle where the search for the NIB takes place, if you pass null, this searches
        //     for the NIB on the main bundle.
        //     This parameter can be null.
        //
        // Remarks:
        //     This is the designated initializer for this class.
        //     The nib file you specify is not loaded right away. It is loaded the FirstView time
        //     the view controller's view is accessed. If you want to perform additional initialization
        //     after the nib file is loaded, override the viewDidLoad method and perform your
        //     tasks there.
        //     If you specify nil for the nibName parameter and you do not override the loadView
        //     method, the view controller searches for a nib file using other means. See nibName.
        //     If your app uses a storyboard to define a view controller and its associated
        //     views, your app never initializes objects of that class directly. Instead, view
        //     controllers are either instantiated by the storyboard either automatically
        //     by iOS when a segue is triggered or programmatically when your app calls the
        //     storyboard object’s instantiateViewControllerWithIdentifier: method. When instantiating
        //     a view controller from a storyboard, iOS initializes the new view controller
        //     by calling its initWithCoder: method instead. iOS automatically sets the nibName
        //     property to a nib file stored inside the storyboard.
        //     For more information about how a view controller loads its view, see Resource
        //     Management in View Controllers in View Controller Programming Guide for iOS.
        public SecondView(string nibName, NSBundle bundle) : base(nibName, bundle)
        {
            Console.WriteLine("nib:{0}, bundle:{1}", nibName, bundle);
            InitMePlease();
        }

        ////
        //// Summary:
        ////     Constructor to call on derived classes to skip initialization and merely allocate
        ////     the object.
        ////
        //// Parameters:
        ////   t:
        ////     Unused sentinel value, pass NSObjectFlag.Empty.
        ////
        //// Remarks:
        ////     This constructor should be called by derived classes when they completely construct
        ////     the object in managed code and merely want the runtime to allocate and initialize
        ////     the NSObject. This is required to implement the two-step initialization process
        ////     that Objective-C uses, the FirstView step is to perform the object allocation, the
        ////     second step is to initialize the object. When you invoke the constructor that
        ////     takes the NSObjectFlag.Empty you taking advantage of a direct path that goes
        ////     all the way up to NSObject to merely allocate the object's memory and bind the
        ////     Objective-C and C# objects together. The actual initialization of the object
        ////     is up to you.
        ////     This constructor is typically used by the binding generator to allocate the object,
        ////     but prevent the actual initialization to take place. Once the allocation has
        ////     taken place, the constructor has to initialize the object. With constructors
        ////     generated by the binding generator this means that it manually invokes one of
        ////     the "init" methods to initialize the object.
        ////     It is your responsibility to completely initialize the object if you chain up
        ////     using the NSObjectFlag.Empty path.
        ////     In general, if your constructors invokes the NSObjectFlag.Empty base implementation,
        ////     then it should be calling an Objective-C init method. If this is not the case,
        ////     you should instead chain to the proper constructor in your class.
        ////     The argument value is ignored and merely ensures that the only code that is executed
        ////     is the construction phase is the basic NSObject allocation and runtime type registration.
        ////     Typically the chaining would look like this:
        ////     // // The NSObjectFlag merely allocates the object and registers the // C# class
        ////     with the Objective-C runtime if necessary, but no actual // initXxx method is
        ////     invoked, that is done later in the constructor // // This is taken from MonoTouch's
        ////     source code: // [Export ("initWithFrame:")] public UIView (System.Drawing.RectangleF
        ////     frame) : base (NSObjectFlag.Empty) { // Invoke the init method now. var initWithFrame
        ////     = new Selector ("initWithFrame:").Handle; if (IsDirectBinding) Handle = MonoTouch.ObjCRuntime.Messaging.IntPtr_objc_msgSend_RectangleF
        ////     (this.Handle, initWithFrame, frame); else Handle = MonoTouch.ObjCRuntime.Messaging.IntPtr_objc_msgSendSuper_RectangleF
        ////     (this.SuperHandle, initWithFrame, frame); }
        //protected FirstView(NSObjectFlag t) : base(t)
        //{
        //    Console.WriteLine(t);
        //    InitMePlease();

        //}

        //
        // Summary:
        //     A constructor used when creating managed representations of unmanaged objects;
        //     Called by the runtime.
        //
        // Parameters:
        //   handle:
        //     Pointer (handle) to the unmanaged object.
        //
        // Remarks:
        //     This constructor is invoked by the runtime infrastructure (ObjCRuntime.GetNSObject
        //     (System.IntPtr)) to create a new managed representation for a pointer to an unmanaged
        //     Objective-C object. You should not invoke this method directly, instead you should
        //     call the GetNSObject method as it will prevent two instances of a managed object
        //     to point to the same native object.
        protected internal SecondView(IntPtr handle) : base(handle)
        {
            Console.WriteLine(handle);
            InitMePlease();
        }
        public void InitMePlease()
        {
            RestorationIdentifier = RestorationID;
            RestorationClass = new Class(typeof(SecondView));
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

