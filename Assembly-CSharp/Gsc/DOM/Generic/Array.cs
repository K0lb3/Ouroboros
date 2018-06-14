// Decompiled with JetBrains decompiler
// Type: Gsc.DOM.Generic.Array
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Gsc.DOM.Generic
{
  public class Array : IEnumerable, IEnumerable<IValue>, IArray, IEnumerable<Value>
  {
    private readonly List<Value> values;

    public Array()
    {
      this.values = new List<Value>();
    }

    [DebuggerHidden]
    IEnumerator<IValue> IEnumerable<IValue>.GetEnumerator()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator<IValue>) new Array.GetEnumerator\u003Ec__Iterator14() { \u003C\u003Ef__this = this };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) this.GetEnumerator();
    }

    IValue IArray.this[int index]
    {
      get
      {
        return (IValue) this[index];
      }
    }

    public int Length
    {
      get
      {
        return this.values.Count;
      }
    }

    [DebuggerHidden]
    public IEnumerator<Value> GetEnumerator()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator<Value>) new Array.\u003CGetEnumerator\u003Ec__Iterator15() { \u003C\u003Ef__this = this };
    }

    public virtual void Add(Value value)
    {
      this.values.Add(value);
    }

    public Value this[int index]
    {
      get
      {
        return this.values[index];
      }
      set
      {
        this.values[index] = value;
      }
    }
  }
}
