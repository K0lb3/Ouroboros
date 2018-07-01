// Decompiled with JetBrains decompiler
// Type: SRPG.LogMapTrick
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

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
