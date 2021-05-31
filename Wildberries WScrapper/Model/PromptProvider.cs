using System;
using Gecko;

namespace Wildberries_WScrapper.Model
{
	public class PromptProvider : nsIAuthPrompt2
	{
		private readonly string username;
		private readonly string password;
		public PromptProvider(string username, string password)
		{
			this.username = username;
			this.password = password;
		}
		public bool PromptAuth(nsIChannel aChannel, uint level, nsIAuthInformation authInfo)
		{
			nsString.Set(new Action<nsAString>(authInfo.SetUsernameAttribute), username);
			nsString.Set(new Action<nsAString>(authInfo.SetPasswordAttribute), password);
			return true;
		}

		public nsICancelable AsyncPromptAuth(nsIChannel aChannel, nsIAuthPromptCallback aCallback, nsISupports aContext, uint level, nsIAuthInformation authInfo)
		{
			nsICancelable result;
			try
			{
				var text = nsString.Get(new Action<nsAString>(authInfo.GetUsernameAttribute));
				var text2 = nsString.Get(new Action<nsAString>(authInfo.GetPasswordAttribute));
				var text3 = nsString.Get(new Action<nsAString>(authInfo.GetRealmAttribute));
				var t = new System.Windows.Forms.Timer() { Interval = 1000 };
				t.Start();
				t.Tick += (object s, EventArgs e) =>
				{
					var value = username;
					var value2 = password;
					nsString.Set(new Action<nsAString>(authInfo.SetUsernameAttribute), value);
					nsString.Set(new Action<nsAString>(authInfo.SetPasswordAttribute), value2);
					aCallback.OnAuthAvailable(aContext, authInfo);
					t.Stop();
				};
			}
			catch
			{

			}
			var cancelable = new Cancelable();
			result = cancelable;
			return result;
		}
	}

	public class Cancelable : nsICancelable
	{
		public void Cancel(int aReason)
		{

		}
	}
}
