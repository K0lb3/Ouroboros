// Decompiled with JetBrains decompiler
// Type: Gsc.DOM.FastJSON.List
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Gsc.DOM.FastJSON
{
  public class List : IList, ICollection, IEnumerable, IList<object>, ICollection<object>, IEnumerable<object>
  {
    private readonly IArray value;
    private object[] _elements;

    public List(IArray value)
    {
      this.value = value;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) this.GetEnumerator();
    }

    [Obsolete("not supported.", true)]
    void ICollection.CopyTo(Array array, int index)
    {
    }

    [Obsolete("not supported.", true)]
    int IList.Add(object value)
    {
      return 0;
    }

    [Obsolete("not supported.", true)]
    void IList.Remove(object value)
    {
    }

    bool IList.IsFixedSize
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

    public int Count
    {
      get
      {
        return this.value.Length;
      }
    }

    private object[] GetElements(bool create)
    {
      if (this._elements == null && create)
      {
        this._elements = new object[this.value.Length];
        for (int index = 0; index < this.value.Length; ++index)
          this._elements[index] = Json.Deserialize(this.value[index]);
      }
      return this._elements;
    }

    public bool Contains(object value)
    {
      return this.IndexOf(value) >= 0;
    }

    public int IndexOf(object value)
    {
      return Array.IndexOf<object>(this.GetElements(true), value);
    }

    public object this[int index]
    {
      get
      {
        object[] elements = this.GetElements(false);
        if (elements != null)
          return elements[index];
        return Json.Deserialize(this.value[index]);
      }
      set
      {
        throw new NotSupportedException();
      }
    }

    [DebuggerHidden]
    public IEnumerator<object> GetEnumerator()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator<object>) new List.\u003CGetEnumerator\u003Ec__Iterator13() { \u003C\u003Ef__this = this };
    }

    public bool IsReadOnly
    {
      get
      {
        return true;
      }
    }

    [Obsolete("not supported.", true)]
    public void CopyTo(object[] dst, int index)
    {
    }

    [Obsolete("not supported.", true)]
    public void Add(object value)
    {
    }

    [Obsolete("not supported.", true)]
    public void Clear()
    {
    }

    [Obsolete("not supported.", true)]
    public void Insert(int index, object value)
    {
    }

    [Obsolete("not supported.", true)]
    public bool Remove(object value)
    {
      return false;
    }

    [Obsolete("not supported.", true)]
    public void RemoveAt(int index)
    {
    }
  }
}
