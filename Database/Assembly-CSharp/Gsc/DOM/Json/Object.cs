// Decompiled with JetBrains decompiler
// Type: Gsc.DOM.Json.Object
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Gsc.DOM.Json
{
  public struct Object : IEnumerable<IMember>, IEnumerable, IObject, IEnumerable<Member>
  {
    private readonly rapidjson.Object value;

    public Object(rapidjson.Object value)
    {
      this.value = value;
    }

    bool IObject.TryGetValue(string name, out IValue value)
    {
      rapidjson.Value obj;
      bool flag = this.value.TryGetValue(name, out obj);
      value = (IValue) new Value(obj);
      return flag;
    }

    [DebuggerHidden]
    IEnumerator<IMember> IEnumerable<IMember>.GetEnumerator()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator<IMember>) new Object.GetEnumerator\u003Ec__Iterator16() { \u003C\u003Ef__this = this };
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
        return this.value.MemberCount;
      }
    }

    public bool HasMember(string name)
    {
      return this.value.HasMember(name);
    }

    public bool TryGetValue(string name, out Value value)
    {
      rapidjson.Value obj;
      bool flag = this.value.TryGetValue(name, out obj);
      value = new Value(obj);
      return flag;
    }

    [DebuggerHidden]
    public IEnumerator<Member> GetEnumerator()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator<Member>) new Object.\u003CGetEnumerator\u003Ec__Iterator17() { \u003C\u003Ef__this = this };
    }

    public Value this[string name]
    {
      get
      {
        return new Value(this.value[name]);
      }
    }
  }
}
