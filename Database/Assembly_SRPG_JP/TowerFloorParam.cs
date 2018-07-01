// Decompiled with JetBrains decompiler
// Type: SRPG.TowerFloorParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SRPG
{
  public class TowerFloorParam
  {
    public List<MapParam> map = new List<MapParam>(BattleCore.MAX_MAP);
    private QuestParam CachedQuestParam;
    private QuestParam BaseQuest;
    public string iname;
    public string title;
    public string name;
    public string expr;
    public string cond;
    public string tower_id;
    public string cond_quest;
    public string rdy_cnd;
    public string reward_id;
    public short pt;
    public short FloorIndex;
    public byte floor;
    private byte hp_recover_rate;
    public byte lv;
    public byte joblv;
    public bool can_help;
    public string deck;
    public byte[] tag_num;
    public bool DownLoaded;
    public string error_messarge;
    public byte is_unit_chg;
    public string me_id;
    public int is_wth_no_chg;
    public string wth_set_id;
    public string mission;
    public int naut;

    public QuestParam Clone(QuestParam original, bool useCache = false)
    {
      if (!useCache)
        return this.ConvertQuestParam(original == null ? this.BaseQuest : original);
      if (this.CachedQuestParam == null)
        this.CachedQuestParam = this.ConvertQuestParam(original == null ? this.BaseQuest : original);
      return this.CachedQuestParam;
    }

    public QuestParam GetQuestParam()
    {
      return this.Clone((QuestParam) null, true);
    }

    private QuestParam ConvertQuestParam(QuestParam original)
    {
      if (original == null)
        return (QuestParam) null;
      JSON_QuestParam json = new JSON_QuestParam();
      json.iname = this.iname;
      json.title = this.title;
      json.name = this.name;
      json.expr = this.expr;
      json.cond = this.cond;
      json.cond_quests = new string[1]{ this.cond_quest };
      json.map = new JSON_MapParam[this.map.Count];
      json.rdy_cnd = this.rdy_cnd;
      json.lv = (int) this.lv;
      json.pt = (int) this.pt;
      for (int index = 0; index < this.map.Count; ++index)
        json.map[index] = new JSON_MapParam()
        {
          set = this.map[index].mapSetName,
          scn = this.map[index].mapSceneName,
          bgm = this.map[index].bgmName,
          btl = this.map[index].battleSceneName,
          ev = this.map[index].eventSceneName
        };
      json.naut = this.naut;
      json.area = original.ChapterID;
      json.type = 7;
      json.notcon = 1;
      json.notitm = 1;
      json.gold = 0;
      json.is_unit_chg = (int) this.is_unit_chg;
      json.me_id = this.me_id;
      json.is_wth_no_chg = this.is_wth_no_chg;
      json.wth_set_id = this.wth_set_id;
      json.tower_mission = this.mission;
      TowerRewardParam towerReward = MonoSingleton<GameManager>.Instance.FindTowerReward(this.reward_id);
      if (towerReward != null)
      {
        List<TowerRewardItem> towerRewardItem1 = towerReward.GetTowerRewardItem();
        for (int index = 0; index < towerRewardItem1.Count; ++index)
        {
          TowerRewardItem towerRewardItem2 = towerRewardItem1[index];
          if (towerRewardItem2 != null && towerRewardItem2.type == TowerRewardItem.RewardType.Gold)
            json.gold = towerRewardItem2.num;
        }
      }
      QuestParam questParam = new QuestParam();
      questParam.Deserialize(json);
      return questParam;
    }

    public void Deserialize(JSON_TowerFloorParam json)
    {
      if (json == null)
        throw new InvalidJSONException();
      this.iname = json.iname;
      this.title = json.title;
      this.name = json.name;
      this.expr = json.expr;
      this.cond = json.cond;
      this.tower_id = json.tower_id;
      this.cond_quest = json.cond_quest;
      this.hp_recover_rate = json.hp_recover_rate;
      this.pt = (short) json.pt;
      this.lv = json.lv;
      this.joblv = json.joblv;
      this.can_help = json.can_help == (byte) 1;
      this.rdy_cnd = json.rdy_cnd;
      this.reward_id = json.reward_id;
      this.floor = json.floor;
      this.is_unit_chg = json.is_unit_chg;
      this.me_id = json.me_id;
      this.is_wth_no_chg = json.is_wth_no_chg;
      this.wth_set_id = json.wth_set_id;
      this.naut = json.naut;
      this.map.Clear();
      this.mission = json.mission;
      if (json.map != null)
      {
        for (int index = 0; index < json.map.Length; ++index)
        {
          MapParam mapParam = new MapParam();
          mapParam.Deserialize(json.map[index]);
          this.map.Add(mapParam);
        }
      }
      this.BaseQuest = MonoSingleton<GameManager>.Instance.FindBaseQuest(QuestTypes.Tower, this.tower_id);
    }

    public int CalcHelaNum(int max_hp)
    {
      return (int) ((double) max_hp * ((double) this.hp_recover_rate / 100.0));
    }

    public int GetFloorNo()
    {
      return int.Parse(Regex.Replace(this.name, "[^0-9]", string.Empty));
    }
  }
}
