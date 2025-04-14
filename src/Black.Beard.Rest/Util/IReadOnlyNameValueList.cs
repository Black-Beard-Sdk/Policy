
namespace Bb.Util
{
    /// <summary>
    /// Defines a read-only ordered collection of Name/Value pairs where duplicate names are allowed but aren't typical.
    /// </summary>
    public interface IReadOnlyNameValueList<TValue> : IReadOnlyList<(string Name, TValue Value)>, INameValueListBase<TValue>
	{

	}

}
