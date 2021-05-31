using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Gecko;
using Wildberries_WScrapper.Model;

namespace Wildberries_WScrapper.Controls
{
	class GeckoBrowserEx : Gecko.GeckoWebBrowser
	{
		static GeckoBrowserEx()
		{
			//GeckoPreferences.User["network.proxy.type"] = 1;
			//GeckoPreferences.User["network.proxy.http"] = "88.205.225.203";
			//GeckoPreferences.User["network.proxy.http_port"] = 3128;
			//GeckoPreferences.User["network.proxy.ssl"] = "88.205.225.203";
			//GeckoPreferences.User["network.proxy.ssl_port"] = 3128;

			//GeckoPreferences.User["places.history.enabled"] = false;
			//GeckoPreferences.User["security.warn_viewing_mixed"] = false;
			//GeckoPreferences.User["plugin.state.flash"] = true;
			//GeckoPreferences.User["browser.cache.disk.enable"] = true;
			//GeckoPreferences.User["browser.cache.memory.enable"] = true;
			//GeckoPreferences.User["dom.max_script_run_time"] = 0;
			//GeckoPreferences.User["browser.download.manager.showAlertOnComplete"] = false;
			//GeckoPreferences.User["privacy.popups.showBrowserMessage"] = true;
			//GeckoPreferences.User["browser.xul.error_pages.enabled"] = true;
			//GeckoPreferences.User["view_source.editor.external"] = true;
			//GeckoPreferences.User["browser.frames.enabled"] = true;
			//GeckoPreferences.User["browser.sessionhistory.cache_subframes"] = true;
			//GeckoPreferences.Default["extensions.blocklist.enabled"] = false;
			//GeckoPreferences.Default["dom.ipc.plugins.enabled.npswf32.dll"] = true;
			//GeckoPreferences.Default["config.use_system_prefs"] = true;
			GeckoPreferences.User["general.useragent.override"] = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:67.0) Gecko/20100101 Firefox/67.0";

		}

		private int requestsCount = 0;
		public new void Navigate(string url)
		{
			base.Navigate(url);
			if (requestsCount++ > 100)
			{
				limitReached(url);
				requestsCount = 0;
			}
		}
		public GeckoBrowserEx()
		{
			Dock = DockStyle.Fill;
			UseHttpActivityObserver = true;
			PromptFactory.PromptServiceCreator = () => new PromptProvider("", "");
			this.DocumentCompleted += GeckoWebBrowser_DocumentCompleted;
			this.ObserveHttpModifyRequest += GeckoWebBrowser_ObserveHttpModifyRequest;
		}
		private event Action pageLoaded = null;
		public event Action PageLoaded
		{
			add => pageLoaded += value;
			remove => pageLoaded -= value;
		}
		private event Action<string> limitReached = null;
		public event Action<string> RequestLimitReached
		{
			add => limitReached += value;
			remove => limitReached -= value;
		}
		public new void Dispose()
		{
			this.Stop();
			if (this.Parent != null)
				this.Parent.Controls.Remove(this);
			base.Dispose();
		}
		private void GeckoWebBrowser_DocumentCompleted(object sender, Gecko.Events.GeckoDocumentCompletedEventArgs e)
		{
			pageLoaded?.Invoke();
		}
		private void GeckoWebBrowser_ObserveHttpModifyRequest(object sender, GeckoObserveHttpModifyRequestEventArgs e)
		{
			if (e.Uri.AbsoluteUri.EndsWith("png") || e.Uri.AbsoluteUri.EndsWith("webp") || e.Uri.AbsoluteUri.EndsWith("jpg"))
			{
				e.Cancel = true;
			}

			if (e.Uri.AbsoluteUri.Contains(".png"))
			{
				e.Cancel = true;
				return;
			}
			string pattern = @"\d{2,}?x\d{2,}";
			string input = e.Uri.AbsoluteUri;
			RegexOptions options = RegexOptions.Multiline;
			var r = Regex.Match(input, pattern, options);
			if (r.Success)
			{
				e.Cancel = true;
			}
		}
	}
}
