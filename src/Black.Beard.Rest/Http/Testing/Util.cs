using System.Collections;
using System.Text.RegularExpressions;
using Bb.Util;

namespace Bb.Http.Testing
{
	/// <summary>
	/// Utility methods used by both HttpTestSetup and HttpTestAssertion
	/// </summary>
	internal static class Util
	{
		internal static bool HasAnyVerb(this UrlCall call, HttpMethod[] verbs) {
			// for good measure, check both UrlRequest.Verb and HttpRequestMessage.Method
			return verbs.Any(verb => call.Request.Verb == verb && call.HttpRequestMessage.Method == verb);
		}

		internal static bool HasAnyVerb(this UrlCall call, string[] verbs) {
			// for good measure, check both UrlRequest.Verb and HttpRequestMessage.Method
			return verbs.Any(verb =>
				call.Request.Verb.Method.OrdinalEquals(verb, true) &&
				call.HttpRequestMessage.Method.Method.OrdinalEquals(verb, true));
		}

		/// <summary>
		/// null value means just check for existence by name
		/// </summary>
		internal static bool HasQueryParam(this UrlCall call, string name, object value = null) {
			if (value == null)
				return call.Request.Url.QueryParams.Contains(name);

			var paramVals = call.Request.Url.QueryParams
				.Where(p => p.Name == name)
				.Select(p => p.Value.ToInvariantString())
				.ToList();

			if (!paramVals.Any())
				return false;
			if (!(value is string) && value is IEnumerable en) {
				var values = en.Cast<object>().Select(o => o.ToInvariantString()).ToList();
				return values.Intersect(paramVals).Count() == values.Count;
			}
			return paramVals.Any(v => MatchesValueOrPattern(v, value));
		}

		internal static bool HasAllQueryParam(this UrlCall call, string[] names) {
			return call.Request.Url.QueryParams
			   .Select(p => p.Name)
			   .Intersect(names)
			   .Count() == names.Length;
		}

		internal static bool HasAnyQueryParam(this UrlCall call, string[] names) {
			var qp = call.Request.Url.QueryParams;
			return names.Any() ? qp
			   .Select(p => p.Name)
			   .Intersect(names)
			   .Any() : qp.Any();
		}

		internal static bool HasQueryParam(this UrlCall call, object values) {
			return values.ToKeyValuePairs().All(kv => call.HasQueryParam(kv.Key, kv.Value));
		}

		/// <summary>
		/// null value means just check for existence by name
		/// </summary>
		internal static bool HasHeader(this UrlCall call, string name, object value = null) {
			return (value == null) ?
				call.Request.Headers.Contains(name) :
				call.Request.Headers.TryGetFirst(name, out var val) && MatchesValueOrPattern(val, value);
		}

		/// <summary>
		/// null value means just check for existence by name
		/// </summary>
		internal static bool HasCookie(this UrlCall call, string name, object value = null) {
			return (value == null) ?
				call.Request.Cookies.Any(c => c.Name == name) :
				MatchesValueOrPattern(call.Request.Cookies.FirstOrDefault(c => c.Name == name).Value, value);
		}

		private static bool MatchesValueOrPattern(object valueToMatch, object value) {
			if (valueToMatch is string pattern && value is string s)
				return MatchesPattern(pattern, s);
			// string match is good enough
			return valueToMatch?.ToInvariantString() == value?.ToInvariantString();
		}

		/// <summary>
		/// same as MatchesPattern, but doesn't require trailing * to ignore query string
		/// </summary>
		internal static bool MatchesUrlPattern(string url, string pattern) {
			if (MatchesPattern(url, pattern))
				return true;
			if (pattern.OrdinalEndsWith("*"))
				return false;
			if (pattern.OrdinalContains("?"))
				return MatchesPattern(url, pattern + "&*");
			else
				return MatchesPattern(url, pattern + "?*");
		}

		/// <summary>
		/// match simple patterns with * wildcard
		/// </summary>
		internal static bool MatchesPattern(string textToCheck, string pattern) {
			// avoid regex'ing in simple cases
			if (string.IsNullOrEmpty(pattern) || pattern == "*") return true;
			if (string.IsNullOrEmpty(textToCheck)) return false;
			if (!pattern.OrdinalContains("*")) return textToCheck == pattern;

			var regex = "^" + Regex.Escape(pattern).Replace("\\*", "(.*)") + "$";
			return Regex.IsMatch(textToCheck ?? string.Empty, regex);
		}
	}
}
