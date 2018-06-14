// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_TrophyParam
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;

namespace SRPG
{
  [Serializable]
  public class JSON_TrophyParam
  {
    public string iname;
    public string name;
    public string expr;
    public string[] flg_quests;
    public int ymd_start;
    public int day_reset;
    public int category;
    public int disp;
    public JSON_TrophyObjective[] objective;
    public string reward_item1_iname;
    public string reward_item2_iname;
    public string reward_item3_iname;
    public int reward_item1_num;
    public int reward_item2_num;
    public int reward_item3_num;
    public int reward_gold;
    public int reward_coin;
    public int reward_exp;
    public int reward_stamina;
    public int bgnr;
    public string begin_at;
    public string end_at;
    public string parent_iname;
    public int help;
  }
}
