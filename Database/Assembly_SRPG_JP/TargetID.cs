// Decompiled with JetBrains decompiler
// Type: SRPG.TargetID
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

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
