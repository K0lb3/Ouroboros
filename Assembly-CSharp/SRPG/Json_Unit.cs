// Decompiled with JetBrains decompiler
// Type: SRPG.Json_Unit
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;

namespace SRPG
{
  [Serializable]
  public class Json_Unit
  {
    public long iid;
    public string iname;
    public int rare;
    public int plus;
    public int lv;
    public int exp;
    public Json_MasterAbility abil;
    public Json_CollaboAbility c_abil;
    public Json_Job[] jobs;
    public Json_UnitSelectable select;
    public string[] quest_clear_unlocks;
  }
}
