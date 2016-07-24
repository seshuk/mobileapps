// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace DocPicker
{
    [Register ("DocPickerViewController")]
    partial class DocPickerViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIBarButtonItem ActionButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton compareButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextView DocumentText { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel firstDocName { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton firstDocument { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel firstFileSize { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIBarButtonItem SaveButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton secondButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel secondDocName { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel SecondFileSize { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (ActionButton != null) {
                ActionButton.Dispose ();
                ActionButton = null;
            }

            if (compareButton != null) {
                compareButton.Dispose ();
                compareButton = null;
            }

            if (DocumentText != null) {
                DocumentText.Dispose ();
                DocumentText = null;
            }

            if (firstDocName != null) {
                firstDocName.Dispose ();
                firstDocName = null;
            }

            if (firstDocument != null) {
                firstDocument.Dispose ();
                firstDocument = null;
            }

            if (firstFileSize != null) {
                firstFileSize.Dispose ();
                firstFileSize = null;
            }

            if (SaveButton != null) {
                SaveButton.Dispose ();
                SaveButton = null;
            }

            if (secondButton != null) {
                secondButton.Dispose ();
                secondButton = null;
            }

            if (secondDocName != null) {
                secondDocName.Dispose ();
                secondDocName = null;
            }

            if (SecondFileSize != null) {
                SecondFileSize.Dispose ();
                SecondFileSize = null;
            }
        }
    }
}