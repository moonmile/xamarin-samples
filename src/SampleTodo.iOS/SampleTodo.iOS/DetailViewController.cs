using System;

using UIKit;

namespace SampleTodo.iOS
{
	public partial class DetailViewController : UIViewController
	{
		public ToDo DetailItem { get; set; }

		protected DetailViewController(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		public void SetDetailItem(ToDo newDetailItem)
		{
			if (DetailItem != newDetailItem)
			{
				DetailItem = newDetailItem;

				// Update the view
				ConfigureView();
			}
		}

		void ConfigureView()
		{
            if (label1 == null) return;

            // Update the user interface for the detail item
            label1.Text = this.DetailItem.Id.ToString();
            text1.Text = this.DetailItem.Text;
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			// Perform any additional setup after loading the view, typically from a nib.
			ConfigureView();
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}

