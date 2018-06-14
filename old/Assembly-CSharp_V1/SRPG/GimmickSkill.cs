// Decompiled with JetBrains decompiler
// Type: SRPG.GimmickSkill
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;

namespace SRPG
{
  public class GimmickSkill
  {
    public List<string> skills = new List<string>();
    public List<Unit> users = new List<Unit>();
    public List<Unit> targets = new List<Unit>();
    public GimmickSkillCondition condition = new GimmickSkillCondition();
    public int count;
    public bool IsCompleted;
    public bool IsStarter;
    public Unit starter;
  }
}
