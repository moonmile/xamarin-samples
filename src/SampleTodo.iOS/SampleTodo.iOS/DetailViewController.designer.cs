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
    [Register ("DetailViewController")]
    partial class DetailViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIBarButtonItem btnSave { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UISwitch swCompleted { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UISwitch swDue { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel textCreateAt { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField textDue { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel textId { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField textText { get; set; }

        [Action ("BtnSave_Activated:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void BtnSave_Activated (UIKit.UIBarButtonItem sender);

        void ReleaseDesignerOutlets ()
        {
            if (btnSave != null) {
                btnSave.Dispose ();
                btnSave = null;
            }

            if (swCompleted != null) {
                swCompleted.Dispose ();
                swCompleted = null;
            }

            if (swDue != null) {
                swDue.Dispose ();
                swDue = null;
            }

            if (textCreateAt != null) {
                textCreateAt.Dispose ();
                textCreateAt = null;
            }

            if (textDue != null) {
                textDue.Dispose ();
                textDue = null;
            }

            if (textId != null) {
                textId.Dispose ();
                textId = null;
            }

            if (textText != null) {
                textText.Dispose ();
                textText = null;
            }
        }
    }
}