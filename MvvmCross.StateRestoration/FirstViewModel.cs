using System;
using MvvmCross.Core.ViewModels;

namespace MvvmCross.StateRestoration
{
	public class FirstViewModel : MvxViewModel
	{
		public string Name {get { return "First View! Boom!"; } }

		public string Count
		{
			get { return count.ToString(); }
			set { count = int.Parse(value); }
		}

		public MvxCommand AddOneCommand
		{
			get { return _addOneCommand = _addOneCommand ?? new MvxCommand(IncrementCount); }
		}

		public MvxCommand LessOneCommand
		{
			get { return _lessOneCommand = _lessOneCommand ?? new MvxCommand(DecrementCount); }
		}

		public MvxCommand NavigateCommand
		{
			get { return _navigateCommand = _navigateCommand ?? new MvxCommand(NavigateToSecondViewModel); }
		}

		int count = 0;
		MvxCommand _addOneCommand;
		MvxCommand _lessOneCommand;
		MvxCommand _navigateCommand;

		void IncrementCount()
		{
			count += 1;
			RaisePropertyChanged(() => Count);
		}

		void DecrementCount()
		{
			count -= 1;
			RaisePropertyChanged(() => Count);
		}

		void NavigateToSecondViewModel()
		{
			ShowViewModel<SecondViewModel>();
		}
	}
}

