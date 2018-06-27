// Decompiled with JetBrains decompiler
// Type: Gsc.DOM.FastJSON.Dictionary
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;

namespace Gsc.DOM.FastJSON
{
  public class Dictionary : IDictionary, ICollection, IEnumerable, IDictionary<string, object>, ICollection<KeyValuePair<string, object>>, IEnumerable<KeyValuePair<string, object>>
  {
    private readonly Dictionary<string, object> values;

    public Dictionary(IObject value)
    {
      this.values = new Dictionary<string, object>(value.MemberCount);
      foreach (IMember member in (IEnumerable<IMember>) value)
        this.values.Add(member.Name, Json.Deserialize(member.Value));
    }

    ICollection IDictionary.Keys
    {
      get
      {
        return (ICollection) this.values.Keys;
      }
    }

    ICollection IDictionary.Values
    {
      get
      {
        return (ICollection) this.values.Values;
      }
    }

    object IDictionary.this[object key]
    {
      get
      {
        return this.values[(string) key];
      }
      set
      {
        throw new NotSupportedException();
      }
    }

    bool IDictionary.Contains(object key)
    {
      return this.ContainsKey((string) key);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) this.GetEnumerator();
    }

    IDictionaryEnumerator IDictionary.GetEnumerator()
    {
      return (IDictionaryEnumerator) this.values.GetEnumerator();
    }

    bool IDictionary.IsFixedSize
    {
      get
      {
        return true;
      }
    }

    bool ICollection.IsSynchronized
    {
      get
      {
        return false;
      }
    }

    object ICollection.SyncRoot
    {
      get
      {
        return (object) this;
      }
    }

    [Obsolete("not supported.", true)]
    void ICollection.CopyTo(Array array, int index)
    {
    }

    [Obsolete("not supported.", true)]
    void IDictionary.Add(object key, object value)
    {
    }

    [Obsolete("not supported.", true)]
    void IDictionary.Remove(object key)
    {
    }

    public int Count
    {
      get
      {
        return this.values.Count;
      }
    }

    public ICollection<string> Keys
    {
      get
      {
        return (ICollection<string>) this.values.Keys;
      }
    }

    public ICollection<object> Values
    {
      get
      {
        return (ICollection<object>) this.values.Values;
      }
    }

    public object this[string key]
    {
      get
      {
        return this.values[key];
      }
      set
      {
        throw new NotSupportedException();
      }
    }

    public bool ContainsKey(string key)
    {
      return this.values.ContainsKey(key);
    }

    public bool TryGetValue(string key, out object value)
    {
      return this.values.TryGetValue(key, out value);
    }

    public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
    {
      return (IEnumerator<KeyValuePair<string, object>>) this.values.GetEnumerator();
    }

    public bool IsReadOnly
    {
      get
      {
        return true;
      }
    }

    [Obsolete("not supported.", true)]
    public bool Contains(KeyValuePair<string, object> member)
    {
      return false;
    }

    [Obsolete("not supported.", true)]
    public void CopyTo(KeyValuePair<string, object>[] dst, int index)
    {
    }

    [Obsolete("not supported.", true)]
    public void Add(string key, object value)
    {
    }

    [Obsolete("not supported.", true)]
    public void Add(KeyValuePair<string, object> member)
    {
    }

    [Obsolete("not supported.", true)]
    public void Clear()
    {
    }

    [Obsolete("not supported.", true)]
    public bool Remove(string key)
    {
      return false;
    }

    [Obsolete("not supported.", true)]
    public bool Remove(KeyValuePair<string, object> member)
    {
      return false;
    }
  }
}
