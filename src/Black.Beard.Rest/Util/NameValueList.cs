
namespace Bb.Util
{

    /// <summary>
    /// An ordered collection of Name/Value pairs where duplicate names are allowed but aren't typical.
    /// Useful for things where a dictionary would work great if not for those pesky edge cases (headers, cookies, etc).
    /// </summary>
    public class NameValueList<TValue> : List<(string Name, TValue Value)>, INameValueList<TValue>, IReadOnlyNameValueList<TValue>
	{
		private bool _caseSensitiveNames;

		/// <summary>
		/// Instantiates a new empty NameValueList.
		/// </summary>
		public NameValueList(bool caseSensitiveNames) {
			_caseSensitiveNames = caseSensitiveNames;
		}

		/// <summary>
		/// Instantiates a new NameValueList with the Name/Value pairs provided.
		/// </summary>
		public NameValueList(IEnumerable<(string Name, TValue Value)> items, bool caseSensitiveNames) {
			_caseSensitiveNames = caseSensitiveNames;
			AddRange(items);
		}

		/// <inheritdoc />
		public void Add(string name, TValue value) => Add((name, value));

		/// <inheritdoc />
		public void AddOrReplace(string name, TValue value) {
			var i = 0;
			var replaced = false;
			while (i < this.Count) {
				if (!this[i].Name.OrdinalEquals(name, !_caseSensitiveNames))
					i++;
				else if (replaced)
					this.RemoveAt(i);
				else {
					this[i] = (name, value);
					replaced = true;
					i++;
				}
			}

			if (!replaced)
				this.Add(name, value);
		}

		/// <inheritdoc />
		public bool Remove(string name) => RemoveAll(x => x.Name.OrdinalEquals(name, !_caseSensitiveNames)) > 0;

		/// <inheritdoc />
		public TValue FirstOrDefault(string name) => GetAll(name).FirstOrDefault();

		/// <inheritdoc />
		public bool TryGetFirst(string name, out TValue value) {
			foreach (var v in GetAll(name)) {
				value = v;
				return true;
			}
			value = default;
			return false;
		}

		/// <inheritdoc />
		public IEnumerable<TValue> GetAll(string name) => this
			.Where(x => x.Name.OrdinalEquals(name, !_caseSensitiveNames))
			.Select(x => x.Value);

		/// <inheritdoc />
		public bool Contains(string name) => this.Any(x => x.Name.OrdinalEquals(name, !_caseSensitiveNames));

		/// <inheritdoc />
		public bool Contains(string name, TValue value) => Contains((name, value));
	}
}
