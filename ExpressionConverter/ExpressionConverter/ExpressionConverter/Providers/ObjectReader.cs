using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Reflection;

namespace ExpressionConverter.Providers
{
    internal class ObjectReader<T> : IEnumerable<T> where T : class, new()
    {
        Enumerator _enumerator;

        internal ObjectReader(DbDataReader reader)
        {
            this._enumerator = new Enumerator(reader);
        }

        public IEnumerator<T> GetEnumerator()
        {
            var e = _enumerator;
            if (e == null)
            {
                throw new InvalidOperationException("Cannot enumerate more than once");
            }
            _enumerator = null;
            return e;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        class Enumerator : IEnumerator<T>, IEnumerator, IDisposable
        {
            readonly DbDataReader _reader;
            readonly FieldInfo[] _fields;
            int[] _fieldLookup;
            T _current;

            internal Enumerator(DbDataReader reader)
            {
                _reader = reader;
                _fields = typeof(T).GetFields();
            }

            public T Current => _current;

            object IEnumerator.Current => _current;

            public bool MoveNext()
            {
                if (!this._reader.Read()) return false;
                if (this._fieldLookup == null)
                {
                    this.InitFieldLookup();
                }
                var instance = new T();
                for (int i = 0, n = this._fields.Length; i < n; i++)
                {
                    int index = this._fieldLookup[i];
                    if (index < 0) continue;
                    var fieldInfo = this._fields[i];
                    fieldInfo.SetValue(instance, this._reader.IsDBNull(index) ? null : this._reader.GetValue(index));
                }
                this._current = instance;
                return true;
            }

            public void Reset()
            {
            }

            public void Dispose()
            {
                this._reader.Dispose();
            }

            private void InitFieldLookup()
            {
                var map = new Dictionary<string, int>(StringComparer.InvariantCultureIgnoreCase);
                for (int i = 0, n = this._reader.FieldCount; i < n; i++)
                {
                    map.Add(this._reader.GetName(i), i);
                }

                _fieldLookup = new int[this._fields.Length];
                for (int i = 0, n = this._fields.Length; i < n; i++)
                {
                    int index;
                    if (map.TryGetValue(this._fields[i].Name, out index))
                    {
                        this._fieldLookup[i] = index;
                    }
                    else
                    {
                        this._fieldLookup[i] = -1;
                    }
                }
            }
        }
    }
}
