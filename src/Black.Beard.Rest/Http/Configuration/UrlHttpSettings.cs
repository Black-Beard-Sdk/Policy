
using System.Runtime.CompilerServices;
using Bb.Http.Testing;

namespace Bb.Http.Configuration
{
    /// <summary>
    /// A set of properties that affect Url.Http behavior
    /// </summary>
    public class UrlHttpSettings
	{
		// Values are dictionary-backed so we can check for key existence. Can't do null-coalescing
		// because if a setting is set to null at the request level, that should stick.
		private readonly IDictionary<string, object> _vals = new Dictionary<string, object>();

		private UrlHttpSettings _defaults;

        /// <summary>
        /// Creates a new UrlHttpSettings object.
        /// </summary>
        public UrlHttpSettings() {
			Redirects = new RedirectSettings(this);
			ResetDefaults();
		}
		/// <summary>
		/// Gets or sets the default values to fall back on when values are not explicitly set on this instance.
		/// </summary>
		public virtual UrlHttpSettings Defaults {
			get => _defaults ?? UrlHttp.GlobalSettings;
			set => _defaults = value;
		}

		/// <summary>
		/// Gets or sets the HTTP request timeout.
		/// </summary>
		public TimeSpan? Timeout {
			get => Get<TimeSpan?>();
			set => Set(value);
		}

		/// <summary>
		/// Gets or sets a pattern representing a range of HTTP status codes which (in addtion to 2xx) will NOT result in Url.Http throwing an Exception.
		/// Examples: "3xx", "100,300,600", "100-299,6xx", "*" (allow everything)
		/// 2xx will never throw regardless of this setting.
		/// </summary>
		public string AllowedHttpStatusRange {
			get => Get<string>();
			set => Set(value);
		}

		/// <summary>
		/// Gets or sets object used to serialize and deserialize JSON. Default implementation uses Newtonsoft Json.NET.
		/// </summary>
		public ISerializer JsonSerializer {
			get => Get<ISerializer>();
			set => Set(value);
		}

		/// <summary>
		/// Gets or sets object used to serialize URL-encoded data. (Deserialization not supported in default implementation.)
		/// </summary>
		public ISerializer UrlEncodedSerializer {
			get => Get<ISerializer>();
			set => Set(value);
		}

		/// <summary>
		/// Gets object whose properties describe how Url.Http should handle redirect (3xx) responses.
		/// </summary>
		public RedirectSettings Redirects { get; }

		/// <summary>
		/// Gets or sets a callback that is invoked immediately before every HTTP request is sent.
		/// </summary>
		public Action<UrlCall> BeforeCall {
			get => Get<Action<UrlCall>>();
			set => Set(value);
		}

		/// <summary>
		/// Gets or sets a callback that is invoked asynchronously immediately before every HTTP request is sent.
		/// </summary>
		public Func<UrlCall, Task> BeforeCallAsync {
			get => Get<Func<UrlCall, Task>>();
			set => Set(value);
		}

		/// <summary>
		/// Gets or sets a callback that is invoked immediately after every HTTP response is received.
		/// </summary>
		public Action<UrlCall> AfterCall {
			get => Get<Action<UrlCall>>();
			set => Set(value);
		}

		/// <summary>
		/// Gets or sets a callback that is invoked asynchronously immediately after every HTTP response is received.
		/// </summary>
		public Func<UrlCall, Task> AfterCallAsync {
			get => Get<Func<UrlCall, Task>>();
			set => Set(value);
		}

		/// <summary>
		/// Gets or sets a callback that is invoked when an error occurs during any HTTP call, including when any non-success
		/// HTTP status code is returned in the response. Response should be null-checked if used in the event handler.
		/// </summary>
		public Action<UrlCall> OnError {
			get => Get<Action<UrlCall>>();
			set => Set(value);
		}

		/// <summary>
		/// Gets or sets a callback that is invoked asynchronously when an error occurs during any HTTP call, including when any non-success
		/// HTTP status code is returned in the response. Response should be null-checked if used in the event handler.
		/// </summary>
		public Func<UrlCall, Task> OnErrorAsync {
			get => Get<Func<UrlCall, Task>>();
			set => Set(value);
		}

		/// <summary>
		/// Gets or sets a callback that is invoked when any 3xx response with a Location header is received.
		/// You can inspect/manipulate the call.Redirect object to determine what will happen next.
		/// An auto-redirect will only happen if call.Redirect.Follow is true upon exiting the callback.
		/// </summary>
		public Action<UrlCall> OnRedirect {
			get => Get<Action<UrlCall>>();
			set => Set(value);
		}

		/// <summary>
		/// Gets or sets a callback that is invoked asynchronously when any 3xx response with a Location header is received.
		/// You can inspect/manipulate the call.Redirect object to determine what will happen next.
		/// An auto-redirect will only happen if call.Redirect.Follow is true upon exiting the callback.
		/// </summary>
		public Func<UrlCall, Task> OnRedirectAsync {
			get => Get<Func<UrlCall, Task>>();
			set => Set(value);
		}

		/// <summary>
		/// Resets all overridden settings to their default values. For example, on a UrlRequest,
		/// all settings are reset to UrlClient-level settings.
		/// </summary>
		public virtual void ResetDefaults() {
			_vals.Clear();
		}

		/// <summary>
		/// Gets a settings value from this instance if explicitly set, otherwise from the default settings that back this instance.
		/// </summary>
		internal T? Get<T>([CallerMemberName]string? propName = null) {
			var testVals = HttpTest.Current?.Settings._vals;
			return
				testVals?.ContainsKey(propName) == true ? (T)testVals[propName] :
				_vals.ContainsKey(propName) ? (T)_vals[propName] :
				Defaults != null ? Defaults.Get<T>(propName) :
				default;
		}

		/// <summary>
		/// Sets a settings value for this instance.
		/// </summary>
		internal void Set<T>(T value, [CallerMemberName]string propName = null) {
			_vals[propName] = value;
		}
	}
}
