using System;
using Foundation;
using UIKit;
using PortableCongress;
using PortableRazor;

namespace iOSCongress
{
	class HybridWebView : IHybridWebView {
		UIWebView webView;

		public HybridWebView(UIWebView uiWebView) {
			webView = uiWebView;
			webView.ShouldStartLoad += HandleShouldStartLoad;
		}

		bool HandleShouldStartLoad (UIWebView webView, NSUrlRequest request, UIWebViewNavigationType navigationType) {
			var handled = RouteHandler.HandleRequest (request.Url.AbsoluteString);
			return !handled;
		}

		#region IHybridWebView implementation

		public void LoadHtmlString (string html)
		{
			var url = new NSUrl (Environment.GetFolderPath (System.Environment.SpecialFolder.Personal), true);
			webView.LoadHtmlString (html, url);
		}

		public string EvaluateJavascript (string script) 
		{
			return webView.EvaluateJavascript (script);
		}

		#endregion
	}
}

