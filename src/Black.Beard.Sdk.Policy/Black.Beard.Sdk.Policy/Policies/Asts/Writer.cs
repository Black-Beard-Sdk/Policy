﻿using System;
using System.Linq;
using System.Text;

namespace Bb.Policies.Asts;

public class Writer
{

    public Writer(StringBuilder? sb = null)
    {
        _sb = sb ?? new StringBuilder();
        _index = 0;
    }

    public void TrimBegin()
    {
        while (char.IsWhiteSpace(_sb[0]))
            _sb.Remove(0, 1);
    }

    public void TrimEnd()
    {
        if (_sb.Length > 0)
            while (char.IsWhiteSpace(_sb[_sb.Length - 1]))
                _sb.Remove(_sb.Length - 1, 1);
    }

    public void TrimBegin(params char[] toFind)
    {
        while (toFind.Contains(_sb[0]))
            _sb.Remove(0, 1);
    }

    public void TrimEnd(params char[] toFind)
    {
        while (toFind.Contains(_sb[_sb.Length - 1]))
            _sb.Remove(_sb.Length - 1, 1);
    }

    public void EnsureEndBy(char txt)
    {
        if (!EndBy(txt))
            Append(txt);
    }

    public void EnsureEndBy(string txt)
    {
        if (!EndBy(txt))
            Append(txt);
    }

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

    public bool EndBy(char text)
    {
        if (_sb.Length > 1)
            if (_sb[_sb.Length - 1] != text)
                return false;
        return true;

    }

    public void CleanIndent()
    {
        while (_index > 0)
            DelIndent();
    }

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

    public void AddIndent()
    {

        if (_index < 0)
            _index = 0;

        _index++;

        _sb.Append('\t');

    }



    public void AppendEndLine(params object[] values)
    {
        foreach (var value in values)
            _sb.Append(value);
        AppendEndLine();
    }

    public void AppendEndLine()
    {

        _sb.AppendLine();
        for (int i = 0; i < _index; i++)
            _sb.Append('\t');

    }

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

    public Writer Append(params string[] values)
    {
        foreach (var value in values)
            _sb.Append(value);

        return this;
    }

    public Writer Append(params char[] values)
    {
        foreach (var value in values)
            _sb.Append(value);

        return this;
    }

    public bool ToString(IWriter writer)
    {

        if (writer != null)
            return writer.ToString(this);

        return false;

    }

    public override string ToString()
    {
        return _sb.ToString();
    }

    public int Length => this._sb.Length;

    public StringBuilder Text { get => _sb; }


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



