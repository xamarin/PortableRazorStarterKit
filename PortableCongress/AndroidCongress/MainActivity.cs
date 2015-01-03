using System;
using Android.App;
using Android.Content;
using Android.Webkit;
using Android.OS;
using Congress;
using PortableCongress;

namespace AndroidCongress
{
	[Activity (Label = "@string/app_name", MainLauncher = true, ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
	public class MainActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			Congress.ResourceManager.EnsureResources (
				typeof(PortableCongress.Politician).Assembly, 
				String.Format ("/data/data/{0}/files", Application.Context.PackageName));

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			var webView = FindViewById<WebView> (Resource.Id.webView);

			var politicianController = new PoliticianController (
				                           new HybridWebView (webView), 
				                           new DataAccess ());

			PortableRazor.RouteHandler.RegisterController ("Politician", politicianController);

			politicianController.ShowPoliticianList ();
		}
	}
}


