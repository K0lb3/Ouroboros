// Decompiled with JetBrains decompiler
// Type: SRPG.Json_Unit
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
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
    public int fav;
    public Json_MasterAbility abil;
    public Json_CollaboAbility c_abil;
    public Json_Job[] jobs;
    public Json_UnitSelectable select;
    public string[] quest_clear_unlocks;
    public int elem;
  }
}
