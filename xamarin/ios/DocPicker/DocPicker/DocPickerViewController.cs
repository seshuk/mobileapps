using System;
using CoreGraphics;

using Foundation;
using UIKit;
using MobileCoreServices;
using System.Drawing;

namespace DocPicker
{
	public partial class DocPickerViewController : UIViewController
	{
		#region Private Variables
		private nfloat _documentTextHeight = 0;
		#endregion

		#region Computed Properties
		static bool UserInterfaceIdiomIsPhone {
			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
		}

		/// <summary>
		/// Returns the delegate of the current running application
		/// </summary>
		/// <value>The this app.</value>
		public AppDelegate ThisApp {
			get { return (AppDelegate)UIApplication.SharedApplication.Delegate; }
		}
		#endregion

		#region Constructors
		public DocPickerViewController (IntPtr handle) : base (handle)
		{
		}
		#endregion

		#region Private Methods
		/// <summary>
		/// Moves a file from a given source url location to a given destination url.
		/// </summary>
		/// <returns><c>true</c>, if file was moved, <c>false</c> otherwise.</returns>
		/// <param name="fromURL">From UR.</param>
		/// <param name="toURL">To UR.</param>
		private bool MoveFile(string fromURL, string toURL) {
			bool successful = true;

			// Get source options
			var srcURL = NSUrl.FromFilename (fromURL);
			var srcIntent = NSFileAccessIntent.CreateReadingIntent (srcURL, NSFileCoordinatorReadingOptions.ForUploading);

			// Get destination options
			var dstURL = NSUrl.FromFilename (toURL);
			var dstIntent = NSFileAccessIntent.CreateReadingIntent (dstURL, NSFileCoordinatorReadingOptions.ForUploading);

			// Create an array
			var intents = new NSFileAccessIntent[] {
				srcIntent,
				dstIntent
			};

			// Initialize a file coordination with intents
			var queue = new NSOperationQueue ();
			var fileCoordinator = new NSFileCoordinator ();
			fileCoordinator.CoordinateAccess (intents, queue, (err) => {
				// Was there an error?
				if (err!=null) {
					// Yes, inform caller
					Console.WriteLine("Error: {0}",err.LocalizedDescription);
					successful = false;
				}
			});

			// Return successful
			return successful;
		}

		#endregion

		#region Private Methods
		/// <summary>
		///  Adjust the size of the <c>DocumentText</c> text editor to account for the keyboard being displayed
		/// </summary>
		/// <param name="height">The new text area height</param>
		private void MoveDocumentText(nfloat height) {

			// Animate size change
			UIView.BeginAnimations("keyboard");
			UIView.SetAnimationDuration(0.3f);

			// Adjust frame to move the text away from the keyboard
			//DocumentText.Frame = new CGRect (0, DocumentText.Frame.Y, DocumentText.Frame.Width, height);

			// Start animation
			UIView.CommitAnimations();
		}
		#endregion

		#region Override Methods
		/// <Docs>Called when the system is running low on memory.</Docs>
		/// <summary>
		/// Dids the receive memory warning.
		/// </summary>
		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		/// <summary>
		/// Views the did load.
		/// </summary>
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			// Save the default text area height
			//_documentTextHeight = DocumentText.Frame.Height;

			// var picker = new UIDocumentPickerViewController (srcURL, UIDocumentPickerMode.ExportToService);

			// Watch for a new document being created
			ThisApp.DocumentLoaded += (document) => {
				// Display the contents of the document
				//DocumentText.Text = document.Contents;

				// Watch for the document being modified by an outside source
				document.DocumentModified += (doc) => {
					// Display the updated contents of the document
					//DocumentText.Text = doc.Contents;
					Console.WriteLine("Document contents have been updated");
				};
			};


			firstDocument.TouchUpInside += LoadMenu;

			secondButton.TouchUpInside += LoadMenu;

            compareButton.TouchUpInside += (sender, e) => {
                // TODO: ADD method call to compare api

                var alert = UIAlertController.Create("coming soon", "this function is coming soon", UIAlertControllerStyle.Alert);
                alert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, (action) => {
                    // Add OK details here...
                }));
                
                this.PresentViewController(alert, true, null);
            };

		}
		//	UTType.Image, UTType.PNG,,
		private void LoadMenu(object s, EventArgs e)
		{

			// Allow the Document picker to select a range of document types
			var allowedUTIs = new string[] {
					UTType.UTF8PlainText,
					UTType.PlainText,
					UTType.RTF,
					UTType.Text,
					UTType.PDF,
					UTType.CompositeContent
				};
			UIButton btn = s as UIButton;
			// Display the picker
			//var picker = new UIDocumentPickerViewController (allowedUTIs, UIDocumentPickerMode.Open);
			var pickerMenu = new UIDocumentMenuViewController(allowedUTIs, UIDocumentPickerMode.Import);
			pickerMenu.DidPickDocumentPicker += (sender, args) =>
			{

				// Wireup Document Picker
				args.DocumentPicker.DidPickDocument += (sndr, pArgs) =>
				{
					
					// IMPORTANT! You must lock the security scope before you can
					// access this file
					var securityEnabled = pArgs.Url.StartAccessingSecurityScopedResource();


					string fileName = System.IO.Path.GetFileName(pArgs.Url.Path);
					 
					if (btn != null) {
						if (btn == secondButton)
							secondDocName.Text = fileName;
						else
							firstDocName.Text = fileName;
					}else
						firstDocName.Text = fileName;
					
					// Open the document
					ThisApp.OpenDocument(pArgs.Url);

					// TODO: This should work but doesn't
					// Apple's WWDC 2014 sample project does this but it blows
					// up in Xamarin
					NSFileCoordinator fileCoordinator = new NSFileCoordinator();
					NSError err;
					fileCoordinator.CoordinateRead(pArgs.Url, NSFileCoordinatorReadingOptions.WithoutChanges, out err, (NSUrl newUrl) =>
					{
						NSData data = NSData.FromUrl(newUrl);
						Console.WriteLine("Data:  {0}", data);

						var str = NSString.FromData(data, NSStringEncoding.UTF8);
						Console.WriteLine(str);

						/*var bytes = System.IO.File.ReadAllBytes(newUrl.Path);
						var strNet = System.Text.Encoding.UTF8.GetString(bytes);
						Console.WriteLine(strNet);*/
						var txt = string.Format("{0} KB", Math.Ceiling( (double) data.Length / 1024));

						if (btn == firstDocument)
							firstFileSize.Text = txt;
						else
							SecondFileSize.Text = txt;

						//var viewer = UIDocumentInteractionController.FromUrl(newUrl);
						//viewer.PresentOpenInMenu(new RectangleF(0, -260, 320, 320), this.View, true);

					});

					// IMPORTANT! You must release the security lock established
					// above.
					pArgs.Url.StopAccessingSecurityScopedResource();
				};

				// Display the document picker
				PresentViewController(args.DocumentPicker, true, null);
			};

			pickerMenu.ModalPresentationStyle = UIModalPresentationStyle.Popover;
			PresentViewController(pickerMenu, true, null);
			UIPopoverPresentationController presentationPopover = pickerMenu.PopoverPresentationController;
			if (presentationPopover != null)
			{
				presentationPopover.SourceView = this.View;
				presentationPopover.PermittedArrowDirections = UIPopoverArrowDirection.Down;
				presentationPopover.SourceRect = ((UIButton)s).Frame;
			}
		}
		/// <summary>
		/// Views the will appear.
		/// </summary>
		/// <param name="animated">If set to <c>true</c> animated.</param>
		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
		}

		/// <summary>
		/// Views the did appear.
		/// </summary>
		/// <param name="animated">If set to <c>true</c> animated.</param>
		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
		}

		/// <summary>
		/// Views the will disappear.
		/// </summary>
		/// <param name="animated">If set to <c>true</c> animated.</param>
		public override void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);
		}

		/// <summary>
		/// Views the did disappear.
		/// </summary>
		/// <param name="animated">If set to <c>true</c> animated.</param>
		public override void ViewDidDisappear (bool animated)
		{
			base.ViewDidDisappear (animated);
		}
		#endregion
	}
}

