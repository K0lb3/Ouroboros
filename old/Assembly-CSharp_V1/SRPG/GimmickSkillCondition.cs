// Decompiled with JetBrains decompiler
// Type: SRPG.GimmickSkillCondition
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;

namespace SRPG
{
  public class GimmickSkillCondition
  {
    public List<Unit> units = new List<Unit>();
    public List<Unit> targets = new List<Unit>();
    public GimmickSkillTriggerTypes type;
    public List<Grid> grids;
    public int count;
  }
}
