// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_QuestMonitorCondition
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

namespace SRPG
{
  [Serializable]
  public class JSON_QuestMonitorCondition
  {
    public UnitMonitorCondition[] actions;
    public UnitMonitorCondition[] goals;
    public UnitMonitorCondition[] withdraw;

    public void CopyTo(QuestMonitorCondition dst)
    {
      dst.Clear();
      if (this.actions != null && this.actions.Length > 0)
        dst.actions = new List<UnitMonitorCondition>((IEnumerable<UnitMonitorCondition>) this.actions);
      if (this.goals != null && this.goals.Length > 0)
        dst.goals = new List<UnitMonitorCondition>((IEnumerable<UnitMonitorCondition>) this.goals);
      if (this.withdraw == null || this.withdraw.Length <= 0)
        return;
      dst.withdraw = new List<UnitMonitorCondition>((IEnumerable<UnitMonitorCondition>) this.withdraw);
    }
  }
}
