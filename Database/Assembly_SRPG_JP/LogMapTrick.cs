// Decompiled with JetBrains decompiler
// Type: SRPG.LogMapTrick
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;

namespace SRPG
{
  public class LogMapTrick : BattleLog
  {
    public List<LogMapTrick.TargetInfo> TargetInfoLists = new List<LogMapTrick.TargetInfo>();
    public TrickData TrickData;

    public class TargetInfo
    {
      public Unit Target;
      public bool IsEffective;
      public int Heal;
      public int Damage;
      public EUnitCondition FailCondition;
      public EUnitCondition CureCondition;
      public Grid KnockBackGrid;
    }
  }
}
