// Decompiled with JetBrains decompiler
// Type: SRPG.GimmickEventCondition
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

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
