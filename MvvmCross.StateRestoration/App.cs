using System;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;

namespace MvvmCross.StateRestoration
{
	public class App : MvxApplication
	{
		public App()
		{
			Mvx.RegisterSingleton<IMvxAppStart>(new MvxAppStart<FirstViewModel>());
		}
	}
}

