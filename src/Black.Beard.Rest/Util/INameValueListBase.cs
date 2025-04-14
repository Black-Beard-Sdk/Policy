
namespace Bb.Util
{
    /// <summary>
    /// Defines common methods for INameValueList and IReadOnlyNameValueList.
    /// </summary>
    public interface INameValueListBase<TValue>
	{
		/// <summary>
		/// Returns the first Value of the given Name if one exists, otherwise null or default value.
		/// </summary>
		TValue FirstOrDefault(string name);

		/// <summary>
		/// Gets the first Value of the given Name, if one exists.
		/// </summary>
		/// <returns>true if any item of the given name is found, otherwise false.</returns>
		bool TryGetFirst(string name, out TValue value);

		/// <summary>
		/// Gets all Values of the given Name.
		/// </summary>
		IEnumerable<TValue> GetAll(string name);

		/// <summary>
		/// True if any items with the given Name exist.
		/// </summary>
		bool Contains(string name);

		/// <summary>
		/// True if any item with the given Name and Value exists.
		/// </summary>
		bool Contains(string name, TValue value);
	}
}
