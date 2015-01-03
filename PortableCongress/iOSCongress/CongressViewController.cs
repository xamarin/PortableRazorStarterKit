using System;
using CoreGraphics;
using System.Reflection;
using Foundation;
using UIKit;
using Congress;
using PortableCongress;

namespace iOSCongress
{
	public partial class CongressViewController : UIViewController
	{
		static bool UserInterfaceIdiomIsPhone {
			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
		}

		public CongressViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			var politicianController = new PoliticianController (
				                        new HybridWebView (webView), 
				                        new DataAccess ());

			PortableRazor.RouteHandler.RegisterController ("Politician", politicianController);

			politicianController.ShowPoliticianList ();
		}
	}
}

