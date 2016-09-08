using System;
using MvvmCross.Core.ViewModels;
using MvvmCross.iOS.Platform;
using MvvmCross.iOS.Views.Presenters;

namespace MvvmCross.StateRestoration.iOS
{
	public class Setup : MvxIosSetup
	{
		public Setup(IMvxApplicationDelegate applicationDelegate, MvxIosViewPresenter presenter)
			: base(applicationDelegate, presenter)
		{
		}

		protected override IMvxApplication CreateApp()
		{
			return new App();
		}
	}
}

