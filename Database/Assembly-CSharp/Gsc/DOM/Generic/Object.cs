// Decompiled with JetBrains decompiler
// Type: Gsc.DOM.Generic.Object
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Gsc.DOM.Generic
{
  public class Object : IEnumerable<IMember>, IEnumerable, IObject, IEnumerable<Member>
  {
    private readonly List<Member> members;

    public Object()
    {
      this.members = new List<Member>();
    }

    bool IObject.TryGetValue(string name, out IValue value)
    {
      Value obj;
      bool flag = this.TryGetValue(name, out obj);
      value = (IValue) obj;
      return flag;
    }

    [DebuggerHidden]
    IEnumerator<IMember> IEnumerable<IMember>.GetEnumerator()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator<IMember>) new Object.GetEnumerator\u003Ec__Iterator4() { \u003C\u003Ef__this = this };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) this.GetEnumerator();
    }

    IValue IObject.this[string name]
    {
      get
      {
        return (IValue) this[name];
      }
    }

    public int MemberCount
    {
      get
      {
        return this.members.Count;
      }
    }

    private int IndexOf(string name)
    {
      return this.members.FindIndex((Predicate<Member>) (x => x.Name == name));
    }

    public bool HasMember(string name)
    {
      return this.IndexOf(name) >= 0;
    }

    public bool TryGetValue(string name, out Value value)
    {
      int index = this.IndexOf(name);
      if (index >= 0)
      {
        value = this.members[index].Value;
        return true;
      }
      value = new Value();
      return false;
    }

    [DebuggerHidden]
    public IEnumerator<Member> GetEnumerator()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator<Member>) new Object.\u003CGetEnumerator\u003Ec__Iterator5() { \u003C\u003Ef__this = this };
    }

    public void Add(string name, Value value)
    {
      this.members.Add(new Member(name, value));
    }

    public Value this[string name]
    {
      get
      {
        Value obj;
        if (this.TryGetValue(name, out obj))
          return obj;
        throw new KeyNotFoundException();
      }
      set
      {
        this.Add(name, value);
      }
    }
  }
}
