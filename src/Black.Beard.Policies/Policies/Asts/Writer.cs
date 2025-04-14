using System.Text;

namespace Bb.Policies.Asts;

/// <summary>
/// Provides functionality for building and manipulating text output for policy expressions.
/// </summary>
/// <remarks>
/// Writer is a utility class that wraps a StringBuilder and provides additional methods
/// for formatting text with indentation, line breaks, and other text manipulation operations.
/// It is primarily used for converting policy AST nodes to their string representation.
/// </remarks>
public class Writer
{

    /// <summary>
    /// Initializes a new instance of the <see cref="Writer"/> class.
    /// </summary>
    /// <param name="sb">An optional StringBuilder to use as the underlying text buffer. If null, a new StringBuilder will be created.</param>
    /// <remarks>
    /// This constructor initializes a new Writer with the specified StringBuilder or creates a new one if none is provided.
    /// The initial indentation level is set to 0.
    /// </remarks>
    /// <example>
    /// <code lang="C#">
    /// // Create a new Writer with a default StringBuilder
    /// var writer = new Writer();
    /// 
    /// // Create a Writer with a custom StringBuilder
    /// var sb = new StringBuilder("Initial text");
    /// var writerWithCustomSb = new Writer(sb);
    /// </code>
    /// </example>
    public Writer(StringBuilder? sb = null)
    {
        _sb = sb ?? new StringBuilder();
        _index = 0;
    }

    /// <summary>
    /// Removes whitespace characters from the beginning of the text.
    /// </summary>
    /// <remarks>
    /// This method removes all whitespace characters from the start of the text until a non-whitespace character is found.
    /// </remarks>
    /// <exception cref="System.IndexOutOfRangeException">Thrown when the StringBuilder is empty.</exception>
    /// <example>
    /// <code lang="C#">
    /// var writer = new Writer();
    /// writer.Append("   Hello");
    /// writer.TrimBegin(); // Writer now contains "Hello"
    /// </code>
    /// </example>
    public void TrimBegin()
    {
        while (char.IsWhiteSpace(_sb[0]))
            _sb.Remove(0, 1);
    }

    /// <summary>
    /// Removes whitespace characters from the end of the text.
    /// </summary>
    /// <remarks>
    /// This method removes all whitespace characters from the end of the text until a non-whitespace character is found.
    /// It safely checks if the StringBuilder has content before attempting to trim.
    /// </remarks>
    /// <example>
    /// <code lang="C#">
    /// var writer = new Writer();
    /// writer.Append("Hello   ");
    /// writer.TrimEnd(); // Writer now contains "Hello"
    /// </code>
    /// </example>
    public void TrimEnd()
    {
        if (_sb.Length > 0)
            while (char.IsWhiteSpace(_sb[_sb.Length - 1]))
                _sb.Remove(_sb.Length - 1, 1);
    }

    /// <summary>
    /// Removes specified characters from the beginning of the text.
    /// </summary>
    /// <param name="toFind">An array of characters to remove from the beginning of the text.</param>
    /// <remarks>
    /// This method removes all characters from the start of the text that match any character in the provided array.
    /// </remarks>
    /// <exception cref="System.IndexOutOfRangeException">Thrown when the StringBuilder is empty.</exception>
    /// <example>
    /// <code lang="C#">
    /// var writer = new Writer();
    /// writer.Append("###Hello");
    /// writer.TrimBegin('#', '-'); // Writer now contains "Hello"
    /// </code>
    /// </example>
    public void TrimBegin(params char[] toFind)
    {
        while (toFind.Contains(_sb[0]))
            _sb.Remove(0, 1);
    }

    /// <summary>
    /// Removes specified characters from the end of the text.
    /// </summary>
    /// <param name="toFind">An array of characters to remove from the end of the text.</param>
    /// <remarks>
    /// This method removes all characters from the end of the text that match any character in the provided array.
    /// </remarks>
    /// <exception cref="System.IndexOutOfRangeException">Thrown when the StringBuilder is empty.</exception>
    /// <example>
    /// <code lang="C#">
    /// var writer = new Writer();
    /// writer.Append("Hello###");
    /// writer.TrimEnd('#', '-'); // Writer now contains "Hello"
    /// </code>
    /// </example>
    public void TrimEnd(params char[] toFind)
    {
        while (toFind.Contains(_sb[_sb.Length - 1]))
            _sb.Remove(_sb.Length - 1, 1);
    }

    /// <summary>
    /// Ensures that the text ends with the specified character.
    /// </summary>
    /// <param name="txt">The character that should appear at the end of the text.</param>
    /// <remarks>
    /// This method checks if the text already ends with the specified character.
    /// If not, it appends the character to the end of the text.
    /// </remarks>
    /// <example>
    /// <code lang="C#">
    /// var writer = new Writer();
    /// writer.Append("Hello");
    /// writer.EnsureEndBy('.'); // Writer now contains "Hello."
    /// </code>
    /// </example>
    public void EnsureEndBy(char txt)
    {
        if (!EndBy(txt))
            Append(txt);
    }

    /// <summary>
    /// Ensures that the text ends with the specified string.
    /// </summary>
    /// <param name="txt">The string that should appear at the end of the text.</param>
    /// <remarks>
    /// This method checks if the text already ends with the specified string.
    /// If not, it appends the string to the end of the text.
    /// </remarks>
    /// <example>
    /// <code lang="C#">
    /// var writer = new Writer();
    /// writer.Append("Hello");
    /// writer.EnsureEndBy("!"); // Writer now contains "Hello!"
    /// </code>
    /// </example>
    public void EnsureEndBy(string txt)
    {
        if (!EndBy(txt))
            Append(txt);
    }

    /// <summary>
    /// Checks if the text ends with the specified string.
    /// </summary>
    /// <param name="text">The string to check for at the end of the text.</param>
    /// <returns><c>true</c> if the text ends with the specified string; otherwise, <c>false</c>.</returns>
    /// <remarks>
    /// This method compares the end of the current text with the provided string.
    /// It performs a character-by-character comparison rather than using the built-in EndsWith method.
    /// </remarks>
    /// <example>
    /// <code lang="C#">
    /// var writer = new Writer();
    /// writer.Append("Hello World");
    /// bool endsWithWorld = writer.EndBy("World"); // Returns true
    /// bool endsWithHello = writer.EndBy("Hello"); // Returns false
    /// </code>
    /// </example>
    public bool EndBy(string text)
    {
        if (_sb.Length >= text.Length)
        {
            var s = _sb.Length - text.Length;
            for (int i = 0; i < text.Length; i++)
            {
                var left = _sb[s + i];
                var right = text[i];
                if (left != right)
                    return false;
            }
        }

        return true;

    }

    /// <summary>
    /// Checks if the text ends with the specified character.
    /// </summary>
    /// <param name="text">The character to check for at the end of the text.</param>
    /// <returns><c>true</c> if the text ends with the specified character; otherwise, <c>false</c>.</returns>
    /// <remarks>
    /// This method checks if the last character of the current text matches the provided character.
    /// </remarks>
    /// <example>
    /// <code lang="C#">
    /// var writer = new Writer();
    /// writer.Append("Hello!");
    /// bool endsWithExclamation = writer.EndBy('!'); // Returns true
    /// bool endsWithQuestion = writer.EndBy('?'); // Returns false
    /// </code>
    /// </example>
    public bool EndBy(char text)
    {
        if (_sb.Length > 1)
            if (_sb[_sb.Length - 1] != text)
                return false;
        return true;

    }

    /// <summary>
    /// Removes all indentation levels from the text.
    /// </summary>
    /// <remarks>
    /// This method resets the indentation level to zero and removes any indentation tabs from the end of the text.
    /// </remarks>
    /// <example>
    /// <code lang="C#">
    /// var writer = new Writer();
    /// writer.AddIndent();
    /// writer.AddIndent();
    /// writer.Append("Hello");
    /// writer.CleanIndent(); // Removes all indents
    /// </code>
    /// </example>
    public void CleanIndent()
    {
        while (_index > 0)
            DelIndent();
    }

    /// <summary>
    /// Removes one level of indentation from the text.
    /// </summary>
    /// <remarks>
    /// This method decreases the indentation level by one and removes the last tab character if present.
    /// It ensures the indentation level doesn't go below zero.
    /// </remarks>
    /// <example>
    /// <code lang="C#">
    /// var writer = new Writer();
    /// writer.AddIndent();
    /// writer.AddIndent();
    /// writer.Append("Hello");
    /// writer.DelIndent(); // Removes one level of indentation
    /// </code>
    /// </example>
    public void DelIndent()
    {
        _index--;
        if (_index < 0)
            _index = 0;
        else
        {
            var last = _sb[_sb.Length - 1];
            if (last == '\t')
                _sb.Remove(_sb.Length - 1, 1);
        }
    }

    /// <summary>
    /// Adds one level of indentation to the text.
    /// </summary>
    /// <remarks>
    /// This method increases the indentation level by one and appends a tab character to the text.
    /// It ensures the indentation level isn't negative before incrementing.
    /// </remarks>
    /// <example>
    /// <code lang="C#">
    /// var writer = new Writer();
    /// writer.Append("Line 1");
    /// writer.AppendEndLine();
    /// writer.AddIndent();
    /// writer.Append("Indented Line"); // This line will be indented
    /// </code>
    /// </example>
    public void AddIndent()
    {

        if (_index < 0)
            _index = 0;

        _index++;

        _sb.Append('\t');

    }

    /// <summary>
    /// Appends the specified values followed by a line ending and appropriate indentation.
    /// </summary>
    /// <param name="values">The values to append before ending the line.</param>
    /// <remarks>
    /// This method appends all the specified values to the current text, then adds a line break
    /// followed by tabs corresponding to the current indentation level.
    /// </remarks>
    /// <example>
    /// <code lang="C#">
    /// var writer = new Writer();
    /// writer.AddIndent();
    /// writer.AppendEndLine("Hello", " ", "World");
    /// // Appends "Hello World" followed by a line break and a tab on the next line
    /// </code>
    /// </example>
    public void AppendEndLine(params object[] values)
    {
        foreach (var value in values)
            _sb.Append(value);
        AppendEndLine();
    }

    /// <summary>
    /// Appends a line ending and appropriate indentation.
    /// </summary>
    /// <remarks>
    /// This method adds a line break to the current text, then adds tabs corresponding to the current indentation level.
    /// </remarks>
    /// <example>
    /// <code lang="C#">
    /// var writer = new Writer();
    /// writer.AddIndent();
    /// writer.Append("Line 1");
    /// writer.AppendEndLine();
    /// writer.Append("Line 2"); // This will start with one tab indentation
    /// </code>
    /// </example>
    public void AppendEndLine()
    {

        _sb.AppendLine();
        for (int i = 0; i < _index; i++)
            _sb.Append('\t');

    }

    /// <summary>
    /// Appends the specified objects to the text.
    /// </summary>
    /// <param name="values">The objects to append to the text.</param>
    /// <returns>This writer instance to enable method chaining.</returns>
    /// <remarks>
    /// This method appends the string representation of each object to the text.
    /// If an object implements IWriter, its ToString method is called with this writer as parameter.
    /// </remarks>
    /// <example>
    /// <code lang="C#">
    /// var writer = new Writer();
    /// writer.Append("Hello", " ", "World", "!", 123);
    /// // Writer now contains "Hello World!123"
    /// </code>
    /// </example>
    /// <returns>
    /// The current <see cref="Writer"/> instance for method chaining.
    /// </returns>
    public Writer Append(params object[] values)
    {
        foreach (var value in values)
        {
            if (value is IWriter i)
                ToString(i);
            else
                _sb.Append(value);
        }
        return this;
    }

    /// <summary>
    /// Appends the specified strings to the text.
    /// </summary>
    /// <param name="values">The strings to append to the text.</param>
    /// <returns>This writer instance to enable method chaining.</returns>
    /// <remarks>
    /// This method appends each of the specified strings to the text.
    /// </remarks>
    /// <example>
    /// <code lang="C#">
    /// var writer = new Writer();
    /// writer.Append("Hello", " ", "World", "!");
    /// // Writer now contains "Hello World!"
    /// </code>
    /// </example>
    /// <returns>
    /// The current <see cref="Writer"/> instance for method chaining.
    /// </returns>
    public Writer Append(params string[] values)
    {
        foreach (var value in values)
            _sb.Append(value);

        return this;
    }

    /// <summary>
    /// Appends the specified characters to the text.
    /// </summary>
    /// <param name="values">The characters to append to the text.</param>
    /// <returns>This writer instance to enable method chaining.</returns>
    /// <remarks>
    /// This method appends each of the specified characters to the text.
    /// </remarks>
    /// <example>
    /// <code lang="C#">
    /// var writer = new Writer();
    /// writer.Append('H', 'e', 'l', 'l', 'o');
    /// // Writer now contains "Hello"
    /// </code>
    /// </example>
    /// <returns>
    /// The current <see cref="Writer"/> instance for method chaining.
    /// </returns>
    public Writer Append(params char[] values)
    {
        foreach (var value in values)
            _sb.Append(value);

        return this;
    }

    /// <summary>
    /// Calls the ToString method on the specified IWriter object, passing this writer as parameter.
    /// </summary>
    /// <param name="writer">The IWriter object whose ToString method should be called.</param>
    /// <returns><c>true</c> if the operation was successful; otherwise, <c>false</c>.</returns>
    /// <remarks>
    /// This method is used to append the string representation of an IWriter object to this writer.
    /// It returns false if the provided writer is null.
    /// </remarks>
    /// <example>
    /// <code lang="C#">
    /// var writer = new Writer();
    /// var policyConstant = new PolicyConstant("value", ConstantType.String);
    /// writer.ToString(policyConstant); // Appends the string representation of the policy constant
    /// </code>
    /// </example>
    /// <returns>
    /// <c>true</c> if the operation was successful; <c>false</c> if the writer parameter is null.
    /// </returns>
    public bool ToString(IWriter writer)
    {

        if (writer != null)
            return writer.ToString(this);

        return false;

    }

    /// <summary>
    /// Returns the string representation of the content in this writer.
    /// </summary>
    /// <returns>A string containing all the text that has been appended to this writer.</returns>
    /// <remarks>
    /// This method returns the string representation of the underlying StringBuilder.
    /// </remarks>
    /// <example>
    /// <code lang="C#">
    /// var writer = new Writer();
    /// writer.Append("Hello World");
    /// string result = writer.ToString(); // Returns "Hello World"
    /// </code>
    /// </example>
    /// <returns>
    /// A <see cref="System.String"/> representation of the content in this writer.
    /// </returns>
    public override string ToString()
    {
        return _sb.ToString();
    }

    /// <summary>
    /// Gets the number of characters in the current text.
    /// </summary>
    /// <remarks>
    /// This property returns the length of the underlying StringBuilder.
    /// </remarks>
    /// <example>
    /// <code lang="C#">
    /// var writer = new Writer();
    /// writer.Append("Hello");
    /// int length = writer.Length; // Returns 5
    /// </code>
    /// </example>
    /// <returns>
    /// An <see cref="System.Int32"/> representing the number of characters in the current text.
    /// </returns>
    public int Length => this._sb.Length;

    /// <summary>
    /// Gets the underlying StringBuilder used by this writer.
    /// </summary>
    /// <remarks>
    /// This property provides direct access to the StringBuilder instance that stores the text.
    /// </remarks>
    /// <example>
    /// <code lang="C#">
    /// var writer = new Writer();
    /// writer.Append("Hello");
    /// StringBuilder sb = writer.Text;
    /// sb.Append(" World"); // Modifies the writer's content directly
    /// </code>
    /// </example>
    /// <returns>
    /// A <see cref="System.Text.StringBuilder"/> containing the writer's text.
    /// </returns>
    public StringBuilder Text { get => _sb; }

    /// <summary>
    /// Gets the number of characters in the current text.
    /// </summary>
    public int Count { get => _sb.Length; }

    private readonly StringBuilder _sb;
    private int _index;

    private class _disposable : IDisposable
    {

        public _disposable(Writer writer)
        {
            this._writer = writer;
        }

        protected virtual void Dispose(bool disposing)
        {

            if (!disposedValue)
            {
                if (disposing)
                {
                    //_strategy.ApplyIndentLineAfterEnding(_writer);
                    //_strategy.ApplyReturnLineAfterEnding(_writer);
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        private bool disposedValue;
        private Writer _writer;

    }

}



