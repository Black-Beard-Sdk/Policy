using System.Text.Json;
using System.Text.Json.Nodes;

namespace Bb.Http.Configuration
{

	/// <summary>
	/// ISerializer implementation based on System.Text.Json.
	/// Default serializer used in calls to GetJsonAsync, PostJsonAsync, etc.
	/// </summary>
	public class DefaultJsonSerializer : ISerializer
	{
		private readonly JsonSerializerOptions _options;

		/// <summary>
		/// Initializes a new instance of the <see cref="DefaultJsonSerializer"/> class.
		/// </summary>
		/// <param name="options">Options to control (de)serialization behavior.</param>
		public DefaultJsonSerializer(JsonSerializerOptions? options = null) {
			_options = options ?? new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
		}

		/// <summary>
		/// Serializes the specified object to a JSON string.
		/// </summary>
		/// <param name="obj">The object to serialize.</param>
		public string Serialize(object obj) => JsonSerializer.Serialize(obj, _options);    

        /// <summary>
        /// Deserializes the specified JSON string to an object of type T.
        /// </summary>
        /// <param name="s">The JSON string to deserializes.</param>
        public T? Deserializes<T>(string s) => string.IsNullOrWhiteSpace(s) ? default : JsonSerializer.Deserialize<T>(s, _options);

		/// <summary>
		/// Deserializes the specified stream to an object of type T.
		/// </summary>
		/// <param name="stream">The stream to deserializes.</param>
		public T? Deserializes<T>(Stream stream) => stream.Length == 0 ? default : JsonSerializer.Deserialize<T>(stream, _options);
	}

	   
}