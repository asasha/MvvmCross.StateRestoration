using System;
using System.Diagnostics;
using ObjCRuntime;

namespace MvvmCross.StateRestoration.iOS
{
	public static class DebugUtil
	{
		public static void PrintLaunchState(this Object obj, string methodName)
		{
			var caller = obj.GetType().ToString();
			Debug.WriteLine($"[{caller}]: {methodName}");
		}
	}
}