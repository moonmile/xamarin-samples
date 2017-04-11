
using System;
using System.Drawing;

using Foundation;
using UIKit;

namespace SampleTodo.iOS
{
	public partial class SettingViewController : UIViewController
	{

		public SettingViewController(IntPtr handle) : base(handle)
		{
		}

		public override void DidReceiveMemoryWarning()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning();

			// Release any cached data, images, etc that aren't in use.
		}

		/// <summary>
		/// セグエの実行時
		/// </summary>
		/// <param name="segue"></param>
		/// <param name="sender"></param>
		public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
		{
			if (segue.Identifier == "")
			{
			}


		}
	}
}