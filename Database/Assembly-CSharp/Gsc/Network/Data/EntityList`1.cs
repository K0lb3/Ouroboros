// Decompiled with JetBrains decompiler
// Type: Gsc.Network.Data.EntityList`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Gsc.Network.Data
{
  public class EntityList<T> : IEnumerable, IList<T>, ICollection<T>, IEnumerable<T> where T : Entity<T>
  {
    private readonly SortedList<string, T> entityList;

    public EntityList(SortedList<string, T> arg)
    {
      this.entityList = arg;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) this.GetEnumerator();
    }

    public bool IsReadOnly
    {
      get
      {
        return true;
      }
    }

    public int Count
    {
      get
      {
        return this.entityList.Count;
      }
    }

    public bool ContainsKey(string key)
    {
      return this.entityList.ContainsKey(key);
    }

    public bool Contains(T value)
    {
      return this.ContainsKey(value.pk);
    }

    public bool TryGetValue(string key, out T value)
    {
      if (!this.entityList.TryGetValue(key, out value))
        return false;
      value = EntityList<T>.CloneEntity(value);
      return true;
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
      if (array == null)
        throw new ArgumentNullException();
      if (arrayIndex < 0)
        throw new ArgumentOutOfRangeException();
      if (array.Length + arrayIndex < this.entityList.Count)
        throw new ArithmeticException();
      IList<T> values = this.entityList.Values;
      for (int index = values.Count - 1; index >= 0; --index)
        array[index + arrayIndex] = EntityList<T>.CloneEntity(values[index]);
    }

    public int IndexOf(T value)
    {
      return this.entityList.IndexOfKey(value.pk);
    }

    [DebuggerHidden]
    public IEnumerator<T> GetEnumerator()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator<T>) new EntityList<T>.\u003CGetEnumerator\u003Ec__Iterator1A() { \u003C\u003Ef__this = this };
    }

    public T this[int index]
    {
      get
      {
        return EntityList<T>.CloneEntity(this.entityList.Values[index]);
      }
      set
      {
        throw new NotSupportedException();
      }
    }

    private static T CloneEntity(T entity)
    {
      T obj = entity.Clone();
      obj.ResolveRefs();
      return obj;
    }

    [Obsolete("Not Supported", true)]
    public void Insert(int index, T value)
    {
    }

    [Obsolete("Not Supported", true)]
    public void RemoveAt(int index)
    {
    }

    [Obsolete("Not Supported", true)]
    public void Add(T value)
    {
    }

    [Obsolete("Not Supported", true)]
    public void Clear()
    {
    }

    [Obsolete("Not Supported", true)]
    public bool Remove(T value)
    {
      return false;
    }
  }
}
