// Decompiled with JetBrains decompiler
// Type: Gsc.DOM.IObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Collections;
using System.Collections.Generic;

namespace Gsc.DOM
{
  public interface IObject : IEnumerable<IMember>, IEnumerable
  {
    int MemberCount { get; }

    bool HasMember(string name);

    bool TryGetValue(string name, out IValue value);

    IValue this[string name] { get; }
  }
}
