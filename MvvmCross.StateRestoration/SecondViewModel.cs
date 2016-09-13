using System;
using MvvmCross.Core.ViewModels;

namespace MvvmCross.StateRestoration
{
	public class SecondViewModel : MvxViewModel
	{
		public string Title { get { return "Second!"; } }

		// need to store this somewhere. Will try at some point.
		public string EntryText
		{
			get { return _entryText; }
			set
			{
				_entryText = value;
				RaisePropertyChanged(() => EntryText);
			}
		}

		string _entryText;

	}
}

