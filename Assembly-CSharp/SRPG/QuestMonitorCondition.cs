// Decompiled with JetBrains decompiler
// Type: SRPG.QuestMonitorCondition
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

namespace SRPG
{
  [Serializable]
  public class QuestMonitorCondition
  {
    public List<UnitMonitorCondition> actions = new List<UnitMonitorCondition>();
    public List<UnitMonitorCondition> goals = new List<UnitMonitorCondition>();

    public void Clear()
    {
      this.actions.Clear();
      this.goals.Clear();
    }

    public void CopyTo(QuestMonitorCondition dst)
    {
      this.Clear();
      for (int index = 0; index < this.actions.Count; ++index)
        dst.actions.Add(this.actions[index]);
      for (int index = 0; index < this.goals.Count; ++index)
        dst.goals.Add(this.goals[index]);
    }
  }
}
