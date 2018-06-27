// Decompiled with JetBrains decompiler
// Type: Gsc.DOM.Json.Member
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace Gsc.DOM.Json
{
  public struct Member : IMember
  {
    private readonly string name;
    private readonly Value value;

    public Member(string name, Value value)
    {
      this.name = name;
      this.value = value;
    }

    IValue IMember.Value
    {
      get
      {
        return (IValue) this.value;
      }
    }

    public string Name
    {
      get
      {
        return this.name;
      }
    }

    public Value Value
    {
      get
      {
        return this.value;
      }
    }
  }
}
