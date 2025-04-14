using Bb.Util;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Bb
{


    /// <summary>
    /// A mutable object for fluently building and parsing URLs.
    /// </summary>
    public class Url
    {

        #region public properties

        public Url IsHttp()
        {
            this.Scheme = "http";
            return this;
        }

        public Url IsHttps()
        {
            this.Scheme = "https";
            return this;
        }

        /// <summary>
        /// The scheme of the URL, i.e. "http". Does not include ":" delimiter. Empty string if the URL is relative.
        /// </summary>
        public string Scheme
        {
            get => EnsureParsed()._scheme;
            set => EnsureParsed()._scheme = value;
        }

        public Url WithUserInfo(string userInfo)
        {
            this.UserInfo = userInfo;
            return this;
        }

        /// <summary>
        /// i.e. "user:pass" in "https://user:pass@www.site.com". Empty string if not present.
        /// </summary>
        public string UserInfo
        {
            get => EnsureParsed()._userInfo;
            set => EnsureParsed()._userInfo = value;
        }

        public Url WithHost(string host)
        {
            this.Host = host;
            return this;
        }

        /// <summary>
        /// i.e. "www.site.com" in "https://www.site.com:8080/path". Does not include user info or port.
        /// </summary>
        public string Host
        {
            get => EnsureParsed()._host;
            set => EnsureParsed()._host = value;
        }

        public Url WithPort(int port)
        {
            this.Port = port;
            return this;
        }

        /// <summary>
        /// Port number of the URL. Null if not explicitly specified.
        /// </summary>
        public int? Port
        {
            get => EnsureParsed()._port;
            set => EnsureParsed()._port = value;
        }

        /// <summary>
        /// i.e. "www.site.com:8080" in "https://www.site.com:8080/path". Includes both user info and port, if included.
        /// </summary>
        public string Authority => string.Concat(
            UserInfo,
            UserInfo?.Length > 0 ? "@" : String.Empty,
            Host,
            Port.HasValue ? ":" : String.Empty,
            Port);

        /// <summary>
        /// i.e. "https://www.site.com:8080" in "https://www.site.com:8080/path" (everything before the path).
        /// </summary>
        public string Root => string.Concat(
            Scheme,
            Scheme?.Length > 0 ? ":" : String.Empty,
            Authority?.Length > 0 ? "//" : String.Empty,
            Authority);

        public Uri BaseAddress => new Uri(this.Root);

        /// <summary>
        /// i.e. "/path" in "https://www.site.com/path". Empty string if not present. Leading and trailing "/" retained exactly as specified by user.
        /// </summary>
        public string Path
        {
            get
            {

                EnsureParsed();

                List<string> _segments = new List<string>(_pathSegments.Count);
                foreach (Segment item in PathSegments)
                    _segments.Add(item.EncodedValue);

                return string.Concat(
                    _leadingSlash ? Slash : String.Empty,
                    string.Join(Slash, _segments),
                    _trailingSlash && PathSegments.Any() ? Slash : String.Empty);

            }
            set
            {
                PathSegments.Clear();
                _trailingSlash = false;
                if (string.IsNullOrEmpty(value))
                    _leadingSlash = false;
                else if (value == Slash)
                    _leadingSlash = true;
                else
                    WithPathSegment(value ?? String.Empty);
            }
        }

        /// <summary>
        /// The "/"-delimited segments of the path, not including leading or trailing "/" characters.
        /// </summary>
        public IList<Segment> PathSegments => EnsureParsed()._pathSegments;

        /// <summary>
        /// i.e. "x=1&amp;y=2" in "https://www.site.com/path?x=1&amp;y=2". Does not include "?".
        /// </summary>
        public string Query
        {
            get => QueryParams.ToString();
            set => EnsureParsed()._queryParams = new QueryParamCollection(value);
        }

        /// <summary>
        /// Query parsed to name/value pairs.
        /// </summary>
        public QueryParamCollection QueryParams => EnsureParsed()._queryParams;

        /// <summary>
        /// i.e. "fragment" in "https://www.site.com/path?x=y#frag". Does not include "#".
        /// </summary>
        public string Fragment
        {
            get => EnsureParsed()._fragment;
            set => EnsureParsed()._fragment = value;
        }

        /// <summary>
        /// True if URL does not start with a non-empty scheme. i.e. false for "https://www.site.com", true for "//www.site.com".
        /// </summary>
        public bool IsRelative => string.IsNullOrEmpty(Scheme);

        /// <summary>
        /// True if Url is absolute and scheme is https or wss.
        /// </summary>
        public bool IsSecureScheme => !IsRelative && (Scheme.OrdinalEquals("https", true) || Scheme.OrdinalEquals("wss", true));
        #endregion

        #region constructors and parsing methods

        /// <summary>
        /// Constructs a Url object from another Url object.
        /// </summary>
        /// <param name="scheme"></param>
        /// <param name="host"></param>
        /// <param name="port"></param>
        /// <param name="segments"></param>
        /// <example>
        /// <code lang="csharp">
        ///     var url = new Url("api.example.com", 80, "api", "v1");
        /// </code>
        /// </example>
        public Url(string scheme, string host, int port, params string[] segments)
            : this()
        {

            this.Scheme = scheme;
            this.Host = host;
            this.Port = port;

            this.WithPathSegment(segments);

        }

        /// <summary>
        /// Constructs a Url object from a string.
        /// </summary>
        /// <param name="baseUrl">The URL to use as a starting point.</param>
        /// <example>
        /// <code lang="csharp">
        ///     var url = new Url("https://api.example.com:80");
        /// </code>
        /// </example>
        public Url(string baseUrl = null)
        {
            _originalString = baseUrl?.Trim();
        }

        /// <summary>
        /// Constructs a Url object from a System.Uri.
        /// </summary>
        /// <param name="uri">The System.Uri (required)</param>
        /// <exception cref="ArgumentNullException"><paramref name="uri"/> is <see langword="null" />.</exception>
        /// <example>
        /// <code lang="csharp">
        ///     var url = new Url(new Uri("http://api.example.com:80"), "api", "v1");
        /// </code>
        /// </example>
        public Url(Uri uri, params string[] segments)
        {
            _originalString = (uri ?? throw new ArgumentNullException(nameof(uri))).OriginalString;
            ParseInternal(uri); // parse eagerly, taking advantage of the fact that we already have a parsed Uri
            WithPathSegment(segments);
        }

        /// <summary>
        /// Parses a URL string into a Url object.
        /// </summary>
        public static Url Parse(string url) => new Url(url).ParseInternal();

        private Url EnsureParsed() => _parsed ? this : ParseInternal();

        private Url ParseInternal(Uri uri = null)
        {
            _parsed = true;

            uri = uri ?? new Uri(_originalString ?? String.Empty, UriKind.RelativeOrAbsolute);

            if (uri.OriginalString.OrdinalStartsWith("//"))
            {
                ParseInternal(new Uri("http:" + uri.OriginalString));
                _scheme = string.Empty;
            }
            else if (uri.OriginalString.OrdinalStartsWith(Slash))
            {
                ParseInternal(new Uri("http://temp.com" + uri.OriginalString));
                _scheme = string.Empty;
                _host = string.Empty;
                _leadingSlash = true;
            }
            else if (uri.IsAbsoluteUri)
            {
                _scheme = uri.Scheme;
                _userInfo = uri.UserInfo;
                _host = uri.Host;
                _port = _originalString?.OrdinalStartsWith($"{Root}:{uri.Port}", ignoreCase: true) == true ? uri.Port : (int?)null;
                // don't default Port if not included explicitly
                _pathSegments = new List<Segment>();
                if (uri.AbsolutePath.Length > 0 && uri.AbsolutePath != Slash)
                    WithPathSegment(uri.AbsolutePath);
                _queryParams = new QueryParamCollection(uri.Query);
                _fragment = uri.Fragment.TrimStart('#'); // quirk - formal def of fragment does not include the #

                _leadingSlash = uri.OriginalString.OrdinalStartsWith(Root + Slash, ignoreCase: true);
                _trailingSlash = _pathSegments.Any() && uri.AbsolutePath.OrdinalEndsWith(Slash);
                _trailingQmark = uri.Query == "?";
                _trailingHash = uri.Fragment == "#";

                // more quirk fixes
                var hasAuthority = uri.OriginalString.OrdinalStartsWith($"{Scheme}://", ignoreCase: true);
                if (hasAuthority && Authority.Length == 0 && PathSegments.Any())
                {
                    // Uri didn't parse Authority when it should have
                    _host = _pathSegments[0].Value;
                    _pathSegments.RemoveAt(0);
                }
                else if (!hasAuthority && Authority.Length > 0)
                {
                    // Uri parsed Authority when it should not have
                    _pathSegments.Insert(0, new Segment(Authority));
                    _userInfo = string.Empty;
                    _host = string.Empty;
                    _port = null;
                }
            }
            // if it's relative, System.Uri refuses to parse any of it. these hacks will force the matter
            else
            {
                ParseInternal(new Uri("http://temp.com/" + uri.OriginalString));
                _scheme = string.Empty;
                _host = string.Empty;
                _leadingSlash = false;
            }

            return this;
        }

        /// <summary>
        /// Parses a URL query to a QueryParamCollection.
        /// </summary>
        /// <param name="query">The URL query to parse.</param>
        public static QueryParamCollection ParseQueryParam(string query) => new QueryParamCollection(query);

        /// <summary>
        /// Splits the given path into segments, encoding illegal characters, "?", and "#".
        /// </summary>
        /// <param name="path">The path to split.</param>
        /// <returns></returns>
        public static IEnumerable<Segment> ParsePathSegment(string path)
        {
            var segments = EncodeIllegalCharacters(path) // path.ReplaceVariables(this.)
                .Replace("?", "%3F")
                .Replace("#", "%23")
                .Split('/');

            if (!segments.Any())
                yield break;

            // skip first and/or last segment if either empty, but not any in between. "///" should return 2 empty segments for example. 
            var start = segments.First().Length > 0 ? 0 : 1;
            var count = segments.Length - (segments.Last().Length > 0 ? 0 : 1);

            for (var i = start; i < count; i++)
            {
                var s = segments[i];
                if (!string.IsNullOrEmpty(s) && s != ".")
                    yield return new Segment(segments[i]);
            }
        }

        #endregion

        #region fluent builder methods

        #region path segment

        /// <summary>
        /// Appends a segment to the URL path, ensuring there is one and only one '/' character as a separator.
        /// </summary>
        /// <param name="segment">The segment to append</param>
        /// <param name="fullyEncode">If true, URL-encodes reserved characters such as '/', '+', and '%'. Otherwise, only encodes strictly illegal characters (including '%' but only when not followed by 2 hex characters).</param>
        /// <returns>the Url object with the segment appended</returns>
        /// <exception cref="ArgumentNullException"><paramref name="segment"/> is <see langword="null" />.</exception>
        public Url WithPathSegment(object segment, bool fullyEncode = false)
        {
            if (segment == null)
                throw new ArgumentNullException(nameof(segment));

            EnsureParsed();

            if (fullyEncode)
            {
                var m = Uri.EscapeDataString(segment.ToInvariantString());
                var s = new Segment(m);
                PathSegments.Add(s);
                _trailingSlash = false;
            }
            else
            {
                var subpath = segment.ToInvariantString();
                foreach (Segment s in ParsePathSegment(subpath))
                    PathSegments.Add(s);
                _trailingSlash = subpath.OrdinalEndsWith(Slash);
            }

            _leadingSlash |= !IsRelative;
            return this;
        }

        /// <summary>
        /// Appends multiple segments to the URL path, ensuring there is one and only one '/' character as a separator.
        /// </summary>
        /// <param name="segments">The segments to append</param>
        /// <returns>the Url object with the segments appended</returns>
        public Url WithPathSegment(params object[] segments)
        {
            foreach (var segment in segments)
                WithPathSegment(segment);

            return this;
        }

        /// <summary>
        /// Appends multiple segments to the URL path, ensuring there is one and only one '/' character as a separator.
        /// </summary>
        /// <param name="segments">The segments to append</param>
        /// <returns>the Url object with the segments appended</returns>
        public Url WithPathSegment(IEnumerable<object> segments)
        {
            foreach (var s in segments)
                WithPathSegment(s);

            return this;
        }

        /// <summary>
        /// Removes the last path segment from the URL.
        /// </summary>
        /// <returns>The Url object.</returns>
        public Url RemovePathSegment()
        {
            if (PathSegments.Any())
                PathSegments.RemoveAt(PathSegments.Count - 1);
            return this;
        }

        /// <summary>
        /// Removes the entire path component of the URL, including the leading slash.
        /// </summary>
        /// <returns>The Url object.</returns>
        public Url RemovePath()
        {
            PathSegments.Clear();
            _leadingSlash = _trailingSlash = false;
            return this;
        }

        /// <summary>
        /// Set the URL fragment fluently.
        /// </summary>
        /// <param name="fragment">The part of the URL after #</param>
        /// <returns>The Url object with the new fragment set</returns>
        public Url SetFragment(string fragment)
        {
            Fragment = fragment ?? String.Empty;
            return this;
        }

        /// <summary>
        /// Removes the URL fragment including the #.
        /// </summary>
        /// <returns>The Url object with the fragment removed</returns>
        public Url RemoveFragment() => SetFragment(string.Empty);

        #endregion path segment

        #region query params

        /// <summary>
        /// Adds a parameter to the query, overwriting the value if name exists.
        /// </summary>
        /// <param name="name">Name of query parameter</param>
        /// <param name="value">Value of query parameter</param>
        /// <param name="nullValueHandling">Indicates how to handle null values. Defaults to Remove (any existing)</param>
        /// <returns>The Url object with the query parameter added</returns>
        public Url WithQueryParam(string name, object value, NullValueHandling nullValueHandling = NullValueHandling.Remove)
        {
            QueryParams.AddOrReplace(name, value, false, nullValueHandling);
            return this;
        }

        /// <summary>
        /// Adds a parameter to the query, overwriting the value if name exists.
        /// </summary>
        /// <param name="name">Name of query parameter</param>
        /// <param name="value">Value of query parameter</param>
        /// <param name="isEncoded">Set to true to indicate the value is already URL-encoded</param>
        /// <param name="nullValueHandling">Indicates how to handle null values. Defaults to Remove (any existing)</param>
        /// <returns>The Url object with the query parameter added</returns>
        /// <exception cref="ArgumentNullException"><paramref name="name"/> is <see langword="null" />.</exception>
        public Url WithQueryParam(string name, string value, bool isEncoded = false, NullValueHandling nullValueHandling = NullValueHandling.Remove)
        {
            QueryParams.AddOrReplace(name, value, isEncoded, nullValueHandling);
            return this;
        }

        /// <summary>
        /// Adds a parameter without a value to the query, removing any existing value.
        /// </summary>
        /// <param name="name">Name of query parameter</param>
        /// <returns>The Url object with the query parameter added</returns>
        public Url WithQueryParam(string name)
        {
            QueryParams.AddOrReplace(name, null, false, NullValueHandling.NameOnly);
            return this;
        }

        /// <summary>
        /// Parses values (usually an anonymous object or dictionary) into name/value pairs and adds them to the query, overwriting any that already exist.
        /// </summary>
        /// <param name="values">Typically an anonymous object, ie: new { x = 1, y = 2 }</param>
        /// <param name="nullValueHandling">Indicates how to handle null values. Defaults to Remove (any existing)</param>
        /// <returns>The Url object with the query parameters added</returns>
        public Url WithQueryParam(object values, NullValueHandling nullValueHandling = NullValueHandling.Remove)
        {
            if (values == null)
                return this;

            if (values is string s)
                return WithQueryParam(s);

            foreach (var kv in values.ToKeyValuePairs())
                WithQueryParam(kv.Key, kv.Value, nullValueHandling);

            return this;
        }

        /// <summary>
        /// Adds multiple parameters without values to the query.
        /// </summary>
        /// <param name="names">Names of query parameters.</param>
        /// <returns>The Url object with the query parameter added</returns>
        public Url WithQueryParam(IEnumerable<string> names)
        {
            if (names == null)
                return this;

            foreach (var name in names.Where(n => !string.IsNullOrEmpty(n)))
                WithQueryParam(name);

            return this;
        }

        /// <summary>
        /// Adds multiple parameters without values to the query.
        /// </summary>
        /// <param name="names">Names of query parameters</param>
        /// <returns>The Url object with the query parameter added.</returns>
        public Url WithQueryParam(params string[] names) => WithQueryParam(names as IEnumerable<string>);


        public Url Map(params (string, string)[] values)
        {
            foreach (var item in values)
                this.Map(item.Item1, item.Item2);
            return this;
        }       

        public void Map(string name, string value)
        {

            var p = PathSegments.ToArray();
            foreach (var item in p)
                if (item.IsVariable && item.Value == name)
                {
                    var o = _pathSegments.IndexOf(item);
                    _pathSegments[o] = new Segment(value);
                }

            var q = this.QueryParams;
            foreach (var item in q)
                if (item.Name == name && item.Value.IsVariable && item.Value.Match(name))
                    QueryParams.AddOrReplace(name, value, false, NullValueHandling.Ignore);

        }



        /// <summary>
        /// Removes a name/value pair from the query by name.
        /// </summary>
        /// <param name="name">Query string parameter name to remove</param>
        /// <returns>The Url object with the query parameter removed</returns>
        public Url RemoveQueryParam(string name)
        {
            QueryParams.Remove(name);
            return this;
        }

        /// <summary>
        /// Removes multiple name/value pairs from the query by name.
        /// </summary>
        /// <param name="names">Query string parameter names to remove</param>
        /// <returns>The Url object.</returns>
        public Url RemoveQueryParam(params string[] names)
        {
            foreach (var name in names)
                QueryParams.Remove(name);
            return this;
        }

        /// <summary>
        /// Removes multiple name/value pairs from the query by name.
        /// </summary>
        /// <param name="names">Query string parameter names to remove</param>
        /// <returns>The Url object with the query parameters removed</returns>
        public Url RemoveQueryParam(IEnumerable<string> names)
        {
            foreach (var name in names)
                QueryParams.Remove(name);
            return this;
        }

        /// <summary>
        /// Removes the entire query component of the URL.
        /// </summary>
        /// <returns>The Url object.</returns>
        public Url RemoveQuery()
        {
            QueryParams.Clear();
            return this;
        }

        #endregion query params

        /// <summary>
        /// Resets the URL to its root, including the scheme, any user info, host, and port (if specified).
        /// </summary>
        /// <returns>The Url object trimmed to its root.</returns>
        public Url ResetToRoot()
        {
            PathSegments.Clear();
            QueryParams.Clear();
            Fragment = String.Empty;
            _leadingSlash = false;
            _trailingSlash = false;
            return this;
        }

        /// <summary>
        /// Resets the URL to its original state as set in the constructor.
        /// </summary>
        public Url Reset()
        {
            if (_parsed)
            {
                _scheme = null;
                _userInfo = null;
                _host = null;
                _port = null;
                _pathSegments = null;
                _queryParams = null;
                _fragment = null;
                _leadingSlash = false;
                _trailingSlash = false;
                _parsed = false;
            }
            return this;
        }

        /// <summary>
        /// Creates a copy of this Url.
        /// </summary>
        public Url Clone() => new Url(this.ToString());
        #endregion

        #region conversion, equality, etc.

        /// <summary>
        /// Converts this Url object to its string representation.
        /// </summary>
        /// <param name="encodeSpaceAsPlus">Indicates whether to encode spaces with the "+" character instead of "%20"</param>
        /// <returns></returns>
        public string ToString(bool encodeSpaceAsPlus)
        {

            //if (!_parsed)
            //    return _originalString ?? string.Empty;

            var p = Path;

            return string.Concat
            (
                Root,
                encodeSpaceAsPlus ? p.Replace("%20", "+") : p,
                _trailingQmark || QueryParams.Any() ? "?" : string.Empty,
                QueryParams.ToString(encodeSpaceAsPlus),
                _trailingHash || Fragment?.Length > 0 ? "#" : string.Empty,
                Fragment
            ).Trim();

        }

        /// <summary>
        /// Converts this Url object to its string representation.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => ToString(false);

        /// <summary>
        /// Converts this Url object to System.Uri
        /// </summary>
        /// <returns>The System.Uri object</returns>
        public Uri ToUri() => new Uri(this.ToString(), UriKind.RelativeOrAbsolute);

        /// <summary>
        /// Implicit conversion from Url to System.Uri.
        /// </summary>
        /// <param name="url"></param>
        public static implicit operator Uri(Url url) => url.ToUri();

        /// <summary>
        /// Implicit conversion from System.Uri to Url.
        /// </summary>
        /// <returns>The string</returns>
        public static implicit operator Url(Uri uri) => new Url(uri.ToString());

        /// <summary>
        /// Implicit conversion from Url to String.
        /// </summary>
        /// <param name="url">The Url object</param>
        /// <returns>The string</returns>
        public static implicit operator string(Url url) => url?.ToString();

        /// <summary>
        /// Implicit conversion from String to Url.
        /// </summary>
        /// <param name="url">The String representation of the URL</param>
        /// <returns>The string</returns>
        public static implicit operator Url(string url) => new Url(url);

        /// <summary>
        /// True if obj is an instance of Url and its string representation is equal to this instance's string representation.
        /// </summary>
        /// <param name="obj">The object to compare to this instance.</param>
        /// <returns></returns>
        public override bool Equals(object obj) => obj is Url url && this.ToString().OrdinalEquals(url.ToString());

        /// <summary>
        /// Returns the hash code for this Url.
        /// </summary>
        public override int GetHashCode() => this.ToString().GetHashCode();
        #endregion

        #region static utility methods        

        /// <summary>
        /// Basically a Path.Combine for URLs. Ensures exactly one '/' separates each segment,
        /// and exactly on '&amp;' separates each query parameter.
        /// URL-encodes illegal characters but not reserved characters.
        /// </summary>
        /// <param name="parts">URL parts to combine.</param>
        public static string Combine(params string[] parts)
        {
            if (parts == null)
                throw new ArgumentNullException(nameof(parts));

            string result = String.Empty;
            bool inQuery = false, inFragment = false;

            string CombineEnsureSingleSeparator(string a, string b, char separator)
            {
                if (string.IsNullOrEmpty(a)) return b;
                if (string.IsNullOrEmpty(b)) return a;
                return a.TrimEnd(separator) + separator + b.TrimStart(separator);
            }

            foreach (var part in parts)
            {
                if (string.IsNullOrEmpty(part))
                    continue;

                if (result.OrdinalEndsWith("?") || part.OrdinalStartsWith("?"))
                    result = CombineEnsureSingleSeparator(result, part, '?');
                else if (result.OrdinalEndsWith("#") || part.OrdinalStartsWith("#"))
                    result = CombineEnsureSingleSeparator(result, part, '#');
                else if (inFragment)
                    result += part;
                else if (inQuery)
                    result = CombineEnsureSingleSeparator(result, part, '&');
                else
                    result = CombineEnsureSingleSeparator(result, part, '/');

                if (part.OrdinalContains("#"))
                {
                    inQuery = false;
                    inFragment = true;
                }
                else if (!inFragment && part.OrdinalContains("?"))
                {
                    inQuery = true;
                }
            }
            return EncodeIllegalCharacters(result);
        }

        /// <summary>
        /// Decodes a URL-encoded string.
        /// </summary>
        /// <param name="s">The URL-encoded string.</param>
        /// <param name="interpretPlusAsSpace">If true, any '+' character will be decoded to a space.</param>
        /// <returns></returns>
        public static string Decode(string s, bool interpretPlusAsSpace)
        {
            if (string.IsNullOrEmpty(s))
                return s;

            return Uri.UnescapeDataString(interpretPlusAsSpace ? s.Replace("+", " ") : s);
        }

        private const int MAX_URL_LENGTH = 65519;

        /// <summary>
        /// URL-encodes a string, including reserved characters such as '/' and '?'.
        /// </summary>
        /// <param name="str">The string to encode.</param>
        /// <param name="encodeSpaceAsPlus">If true, spaces will be encoded as + signs. Otherwise, they'll be encoded as %20.</param>
        /// <returns>The encoded URL.</returns>
        public static string Encode(string str, bool encodeSpaceAsPlus = false)
        {

            if (string.IsNullOrEmpty(str))
                return str;

            if (str.Length > MAX_URL_LENGTH)
            {
                // Uri.EscapeDataString is going to throw because the string is "too long", so break it into pieces and concat them
                var parts = new string[(int)Math.Ceiling((double)str.Length / MAX_URL_LENGTH)];
                for (var i = 0; i < parts.Length; i++)
                {
                    var start = i * MAX_URL_LENGTH;
                    var len = Math.Min(MAX_URL_LENGTH, str.Length - start);
                    parts[i] = Uri.EscapeDataString(str.Substring(start, len));
                }
                str = string.Concat(parts);
            }
            else
            {
                str = Uri.EscapeDataString(str);
            }

            return encodeSpaceAsPlus ? str.Replace("%20", "+") : str;

        }

        /// <summary>
        /// URL-encodes characters in a string that are neither reserved nor unreserved. Avoids encoding reserved characters such as '/' and '?'. Avoids encoding '%' if it begins a %-hex-hex sequence (i.e. avoids double-encoding).
        /// </summary>
        /// <param name="s">The string to encode.</param>
        /// <param name="encodeSpaceAsPlus">If true, spaces will be encoded as + signs. Otherwise, they'll be encoded as %20.</param>
        /// <returns>The encoded URL.</returns>
        public static string EncodeIllegalCharacters(string s, bool encodeSpaceAsPlus = false)
        {
            if (string.IsNullOrEmpty(s))
                return s;

            if (encodeSpaceAsPlus)
                s = s.Replace(" ", "+");

            // Uri.EscapeUriString mostly does what we want - encodes illegal characters only - but it has a quirk
            // in that % isn't illegal if it's the start of a %- encoded sequence https://stackoverflow.com/a/47636037/62600

            // no % characters, so avoid the regex overhead
            if (!s.OrdinalContains("%"))
                return Uri.EscapeUriString(s);

            // pick out all %-hex-hex matches and avoid double-encoding
            return Regex.Replace(s, "(.*?)((%[0-9A-Fa-f]{2})|$)", c =>
            {
                var a = c.Groups[1].Value; // group 1 is a sequence with no %- encoding - encode illegal characters
                var b = c.Groups[2].Value; // group 2 is a valid 3-character %- encoded sequence - leave it alone!
                return Uri.EscapeUriString(a) + b;
            });
        }

        /// <summary>
        /// Checks if a string is a well-formed absolute URL.
        /// </summary>
        /// <param name="url">The string to check</param>
        /// <returns>true if the string is a well-formed absolute URL</returns>
        public static bool IsValid(string url) => url != null && Uri.IsWellFormedUriString(url, UriKind.Absolute);

        #endregion


        public static string Slash = "/";

        //private Variables _variables = new Variables();
        private string _originalString;
        private bool _parsed;
        private string _scheme;
        private string _userInfo;
        private string _host;
        private List<Segment> _pathSegments;
        private QueryParamCollection _queryParams;
        private string _fragment;
        private int? _port;
        private bool _leadingSlash;
        private bool _trailingSlash;
        private bool _trailingQmark;
        private bool _trailingHash;

    }

}