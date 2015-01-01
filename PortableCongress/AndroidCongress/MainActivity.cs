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

			// Use subclassed WebViewClient to intercept hybrid native calls
			var webViewClient = new HybridWebViewClient ();

			var politicianController = new PoliticianController (
				                           new HybridWebView (webView), 
				                           new DataAccess ());

			PortableRazor.RouteHandler.RegisterController ("Politician", politicianController);

			webView.SetWebViewClient (webViewClient);
            webView.Settings.CacheMode = CacheModes.CacheElseNetwork;
			webView.Settings.JavaScriptEnabled = true;
			webView.SetWebChromeClient (new HybridWebChromeClient (this));

			politicianController.ShowPoliticianList ();
		}

		class HybridWebViewClient : WebViewClient {
			public override bool ShouldOverrideUrlLoading (WebView webView, string url) {

				var handled = PortableRazor.RouteHandler.HandleRequest (url);
				return handled;
			}
		}

	    class HybridWebChromeClient : WebChromeClient {
			Context context;

			public HybridWebChromeClient (Context context) : base () {
				this.context = context;
			}

			public override bool OnJsAlert (WebView view, string url, string message, JsResult result) {
				var alertDialogBuilder = new AlertDialog.Builder (context)
					.SetMessage (message)
					.SetCancelable (false)
					.SetPositiveButton ("Ok", (sender, args) => {
						result.Confirm ();
					});

				alertDialogBuilder.Create().Show();

				return true;
			}

			public override bool OnJsConfirm (WebView view, string url, string message, JsResult result) {
				var alertDialogBuilder = new AlertDialog.Builder (context)
					.SetMessage (message)
					.SetCancelable (false)
					.SetPositiveButton ("Ok", (sender, args) => {
						result.Confirm();
					})
					.SetNegativeButton ("Cancel", (sender, args) => {
						result.Cancel();
					});

				alertDialogBuilder.Create().Show();

				return true;
			}
		}
	}
}


