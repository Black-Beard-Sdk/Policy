namespace Bb.Http.Configuration
{
    /// <summary>
    /// Global default settings for Uurl.Http
    /// </summary>
    public class GlobalUrlHttpSettings : UrlHttpSettings
	{
		internal GlobalUrlHttpSettings() {
			ResetDefaults();
		}

		/// <summary>
		/// Defaults at the global level do not make sense and will always be null.
		/// </summary>
		public override UrlHttpSettings Defaults {
			get => null;
			set => throw new Exception("Global settings cannot be backed by any higher-level defauts.");
		}

		/// <summary>
		/// Gets or sets the factory that defines creating, caching, and reusing UrlClient instances and,
		/// by proxy, RestClient instances.
		/// </summary>
		public IUrlClientFactory UrlClientFactory {
			get => Get<IUrlClientFactory>();
			set => Set(value);
		}

		/// <summary>
		/// Resets all global settings to their default values.
		/// </summary>
		public override void ResetDefaults() {
			base.ResetDefaults();
			Timeout = TimeSpan.FromSeconds(100); // same as RestClient
			JsonSerializer = new DefaultJsonSerializer();
			UrlEncodedSerializer = new DefaultUrlEncodedSerializer();
			UrlClientFactory = new DefaultUrlClientFactory();
			Redirects.Enabled = true;
			Redirects.AllowSecureToInsecure = false;
			Redirects.ForwardHeaders = false;
			Redirects.ForwardAuthorizationHeader = false;
			Redirects.MaxAutoRedirects = 10;
		}
	}
}
