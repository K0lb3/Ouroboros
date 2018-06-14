// Decompiled with JetBrains decompiler
// Type: SRPG.TargetID
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;

namespace SRPG
{
  [Serializable]
  public struct TargetID
  {
    public TargetID.IDType Type;
    public string ID;

    public enum IDType
    {
      ObjectID,
      UnitID,
      ActorID,
    }
  }
}
