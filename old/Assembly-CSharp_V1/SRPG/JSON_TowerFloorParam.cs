// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_TowerFloorParam
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
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
      return jsonQuestParam;
    }
  }
}
