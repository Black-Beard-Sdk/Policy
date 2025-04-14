
namespace Bb.Util
{
    /// <summary>
    /// Defines an ordered collection of Name/Value pairs where duplicate names are allowed but aren't typical.
    /// </summary>
    public interface INameValueList<TValue> : IList<(string Name, TValue Value)>, INameValueListBase<TValue>
	{
		/// <summary>
		/// Adds a new Name/Value pair.
		/// </summary>
		void Add(string name, TValue value);

		/// <summary>
		/// Replaces the first occurrence of the given Name with the given Value and removes any others,
		/// or adds a new Name/Value pair if none exist.
		/// </summary>
		void AddOrReplace(string name, TValue value);

		/// <summary>
		/// Removes all items of the given Name.
		/// </summary>
		/// <returns>true if any item of the given name is found, otherwise false.</returns>
		bool Remove(string name);
	}
}
