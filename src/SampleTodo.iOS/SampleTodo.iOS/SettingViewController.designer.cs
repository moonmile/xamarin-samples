// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace SampleTodo.iOS
{
	[Register("SettingViewController")]
	partial class SettingViewController
	{
		[Outlet]
		[GeneratedCode("iOS Designer", "1.0")]
		UIKit.UISwitch swDispCompleted { get; set; }
		[Outlet]
		[GeneratedCode("iOS Designer", "1.0")]
		UIKit.UIPickerView pkOrder { get; set; }
		void ReleaseDesignerOutlets()
		{
			if (swDispCompleted != null)
			{
				swDispCompleted.Dispose();
				swDispCompleted = null;
			}

			if (pkOrder != null)
			{
				pkOrder.Dispose();
				pkOrder = null;
			}
		}
	}
}