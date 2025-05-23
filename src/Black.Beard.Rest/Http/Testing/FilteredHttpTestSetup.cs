﻿using Bb.Http.Configuration;

namespace Bb.Http.Testing
{
	/// <summary>
	/// Represents a set of request conditions and fake responses for faking HTTP calls in tests.
	/// Usually created fluently via HttpTest.ForCallsTo, rather than instantiated directly.
	/// </summary>
	public class FilteredHttpTestSetup : HttpTestSetup
	{
		private readonly List<Func<UrlCall, bool>> _filters = new List<Func<UrlCall, bool>>();

		/// <summary>
		/// Constructs a new instance of FilteredHttpTestSetup.
		/// </summary>
		/// <param name="settings">UrlHttpSettings used in fake calls.</param>
		/// <param name="urlPatterns">URL(s) or URL pattern(s) that this HttpTestSetup applies to. Can contain * wildcard.</param>
		public FilteredHttpTestSetup(UrlHttpSettings settings, params string[] urlPatterns) : base(settings) {
			if (urlPatterns.Any())
				With(call => urlPatterns.Any(p => Util.MatchesUrlPattern(call.Request.Url, p)));
		}

		/// <summary>
		/// Returns true if the given UrlCall matches one of the URL patterns and all other criteria defined for this HttpTestSetup.
		/// </summary>
		internal bool IsMatch(UrlCall call) => _filters.All(f => f(call));

		/// <summary>
		/// Defines a condition for which this HttpTestSetup applies.
		/// </summary>
		public FilteredHttpTestSetup With(Func<UrlCall, bool> condition) {
			_filters.Add(condition);
			return this;
		}

		/// <summary>
		/// Defines a condition for which this HttpTestSetup does NOT apply.
		/// </summary>
		public FilteredHttpTestSetup Without(Func<UrlCall, bool> condition) {
			return With(c => !condition(c));
		}

		/// <summary>
		/// Defines one or more HTTP verbs, any of which a call must match in order for this HttpTestSetup to apply.
		/// </summary>
		public FilteredHttpTestSetup WithVerb(params HttpMethod[] verbs) {
			return With(call => call.HasAnyVerb(verbs));
		}

		/// <summary>
		/// Defines one or more HTTP verbs, any of which a call must match in order for this HttpTestSetup to apply.
		/// </summary>
		public FilteredHttpTestSetup WithVerb(params string[] verbs) {
			return With(call => call.HasAnyVerb(verbs));
		}

		/// <summary>
		/// Defines a query parameter and (optionally) its value that a call must contain in order for this HttpTestSetup to apply.
		/// </summary>
		public FilteredHttpTestSetup WithQueryParam(string name, object value = null) {
			return With(c => c.HasQueryParam(name, value));
		}

		/// <summary>
		/// Defines a query parameter and (optionally) its value that a call must NOT contain in order for this HttpTestSetup to apply.
		/// </summary>
		public FilteredHttpTestSetup WithoutQueryParam(string name, object value = null) {
			return Without(c => c.HasQueryParam(name, value));
		}

		/// <summary>
		/// Defines query parameter names, ALL of which a call must contain in order for this HttpTestSetup to apply.
		/// </summary>
		public FilteredHttpTestSetup WithQueryParam(params string[] names) {
			return With(c => c.HasAllQueryParam(names));
		}

		/// <summary>
		/// Defines query parameter names, NONE of which a call must contain in order for this HttpTestSetup to apply.
		/// If no names are provided, call must not contain any query parameters.
		/// </summary>
		public FilteredHttpTestSetup WithoutQueryParam(params string[] names) {
			return Without(c => c.HasAnyQueryParam(names));
		}

		/// <summary>
		/// Defines query parameters, ALL of which a call must contain in order for this HttpTestSetup to apply.
		/// </summary>
		/// <param name="values">Object (usually anonymous) or dictionary that is parsed to name/value query parameters to check for. Values may contain * wildcard.</param>
		public FilteredHttpTestSetup WithQueryParam(object values) {
			return With(c => c.HasQueryParam(values));
		}

		/// <summary>
		/// Defines query parameters, NONE of which a call must contain in order for this HttpTestSetup to apply.
		/// </summary>
		/// <param name="values">Object (usually anonymous) or dictionary that is parsed to name/value query parameters to check for. Values may contain * wildcard.</param>
		public FilteredHttpTestSetup WithoutQueryParam(object values) {
			return Without(c => c.HasQueryParam(values));
		}

		/// <summary>
		/// Defines query parameter names, ANY of which a call must contain in order for this HttpTestSetup to apply.
		/// If no names are provided, call must contain at least one query parameter with any name.
		/// </summary>
		public FilteredHttpTestSetup WithAnyQueryParam(params string[] names) {
			return With(c => c.HasAnyQueryParam(names));
		}

		/// <summary>
		/// Defines a request header and (optionally) its value that a call must contain in order for this HttpTestSetup to apply.
		/// </summary>
		public FilteredHttpTestSetup WithHeader(string name, string valuePattern = null) {
			return With(c => c.HasHeader(name, valuePattern));
		}

		/// <summary>
		/// Defines a request header and (optionally) its value that a call must NOT contain in order for this HttpTestSetup to apply.
		/// </summary>
		public FilteredHttpTestSetup WithoutHeader(string name, string valuePattern = null) {
			return Without(c => c.HasHeader(name, valuePattern));
		}

		/// <summary>
		/// Defines a request body that must exist in order for this HttpTestSetup to apply.
		/// The * wildcard can be used.
		/// </summary>
		public FilteredHttpTestSetup WithRequestBody(string pattern) {
			return With(call => Util.MatchesPattern(call.RequestBody, pattern));
		}

		/// <summary>
		/// Defines an object that, when serialized to JSON, must match the request body in order for this HttpTestSetup to apply.
		/// </summary>
		public FilteredHttpTestSetup WithRequestJson(object body) {
			return WithRequestBody(Settings.JsonSerializer.Serialize(body));
		}
	}
}