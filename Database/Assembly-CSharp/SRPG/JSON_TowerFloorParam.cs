// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_TowerFloorParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;

namespace SRPG
{
  [Serializable]
  public class JSON_TowerFloorParam
  {
    public string iname;
    public string title;
    public string name;
    public string expr;
    public string cond;
    public string tower_id;
    public string cond_quest;
    public string rdy_cnd;
    public string reward_id;
    public JSON_MapParam[] map;
    public string deck;
    public byte[] tag;
    public byte hp_recover_rate;
    public byte pt;
    public byte lv;
    public byte joblv;
    public byte can_help;
    public byte floor;
    public int[] rand_tag;
    public byte is_unit_chg;
    public int naut;
    public string me_id;
    public int is_wth_no_chg;
    public string wth_set_id;

    public JSON_QuestParam ConvertQuestParam()
    {
      JSON_QuestParam jsonQuestParam = new JSON_QuestParam();
      jsonQuestParam.iname = this.iname;
      jsonQuestParam.title = this.title;
      jsonQuestParam.name = this.name;
      jsonQuestParam.expr = this.expr;
      jsonQuestParam.cond = this.cond;
      jsonQuestParam.cond_quests = new string[1]
      {
        this.cond_quest
      };
      jsonQuestParam.map = new JSON_MapParam[this.map.Length];
      jsonQuestParam.rdy_cnd = this.rdy_cnd;
      jsonQuestParam.lv = (int) this.lv;
      jsonQuestParam.pt = (int) this.pt;
      for (int index = 0; index < this.map.Length; ++index)
        jsonQuestParam.map[index] = new JSON_MapParam()
        {
          set = this.map[index].set,
          scn = this.map[index].scn,
          bgm = this.map[index].bgm,
          btl = this.map[index].btl,
          ev = this.map[index].ev
        };
      jsonQuestParam.type = 7;
      jsonQuestParam.notcon = 1;
      jsonQuestParam.notitm = 1;
      jsonQuestParam.gold = 0;
      jsonQuestParam.is_unit_chg = (int) this.is_unit_chg;
      jsonQuestParam.naut = this.naut;
      jsonQuestParam.me_id = this.me_id;
      jsonQuestParam.is_wth_no_chg = this.is_wth_no_chg;
      jsonQuestParam.wth_set_id = this.wth_set_id;
      return jsonQuestParam;
    }
  }
}
