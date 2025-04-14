

namespace Bb.Http.Configuration
{
	/// <summary>
	/// Contract for serializing and deserializes objects.
	/// </summary>
    public interface ISerializer
    {
		/// <summary>
		/// Serializes an object to a string representation.
		/// </summary>
	    string Serialize(object obj);

        /// <summary>
        /// Deserializes an object from a string representation.
        /// </summary>
        T Deserializes<T>(string s);
		/// <summary>
		/// Deserializes an object from a stream representation.
		/// </summary>
		T Deserializes<T>(Stream stream);
    }
}
