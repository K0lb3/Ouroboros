// Decompiled with JetBrains decompiler
// Type: SRPG.GimmickEventCondition
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;

namespace SRPG
{
  public class GimmickEventCondition
  {
    public List<Unit> units = new List<Unit>();
    public List<Unit> targets = new List<Unit>();
    public List<TrickData> td_targets = new List<TrickData>();
    public GimmickEventTriggerType type;
    public string td_iname;
    public string td_tag;
    public List<Grid> grids;
    public int count;
  }
}
