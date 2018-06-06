// Decompiled with JetBrains decompiler
// Type: SRPG.QuestParam
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  public class QuestParam
  {
    private readonly int MULTI_MAX_TOTAL_UNIT = 4;
    private readonly int MULTI_MAX_PLAYER_UNIT = 2;
    public OInt dailyCount = (OInt) 0;
    public OInt dailyReset = (OInt) 0;
    public string[] dropItems = new string[0];
    public OInt point = (OInt) 0;
    public OInt aplv = (OInt) 0;
    public OInt pexp = (OInt) 0;
    public OInt uexp = (OInt) 0;
    public OInt gold = (OInt) 0;
    public OInt mcoin = (OInt) 0;
    public OInt clock_win = (OInt) 0;
    public OInt clock_lose = (OInt) 0;
    public OInt win = (OInt) 0;
    public OInt lose = (OInt) 0;
    public OInt multi = (OInt) 0;
    public OInt multiDead = (OInt) 0;
    public OInt playerNum = (OInt) 0;
    public OInt unitNum = (OInt) 0;
    public List<MapParam> map = new List<MapParam>(BattleCore.MAX_MAP);
    protected string localizedNameID;
    protected string localizedCondID;
    protected string localizedTitleID;
    public string iname;
    public string title;
    public string name;
    public string expr;
    public string cond;
    public string[] pieces;
    public string world;
    public string ChapterID;
    public string mission;
    public string[] cond_quests;
    public string[] units;
    public QuestDifficulties difficulty;
    public string navigation;
    public string storyTextID;
    public QuestStates state;
    public OInt clear_missions;
    public QuestBonusObjective[] bonusObjective;
    public OInt challengeLimit;
    public OBool isDailyReset;
    public QuestTypes type;
    public int lv;
    public string event_start;
    public string event_clear;
    public string ticket;
    private QuestParam.QuestFlags mFlags;
    public long start;
    public long end;
    public long key_end;
    public int key_cnt;
    public int key_limit;
    public bool hidden;
    public bool replayLimit;
    public string VersusThumnail;
    public string MapBuff;
    public int VersusMoveCount;
    public string AllowedJobs;
    public QuestParam.Tags AllowedTags;
    public int PhysBonus;
    public int MagBonus;
    public int Beginner;
    public string ItemLayout;
    public ChapterParam Chapter;
    private int[] AtkTypeMags;
    public QuestCondParam EntryCondition;
    public QuestCondParam EntryConditionCh;
    public bool UseFixEditor;
    public bool IsNoStartVoice;
    public bool UseSupportUnit;

    protected void localizeFields(string language)
    {
      this.init();
      this.name = LocalizedText.SGGet(language, GameUtility.LocalizedQuestParamFileName, this.localizedNameID);
      this.title = LocalizedText.SGGet(language, GameUtility.LocalizedQuestParamFileName, this.localizedTitleID);
      this.cond = LocalizedText.SGGet(language, GameUtility.LocalizedQuestParamFileName, this.localizedCondID);
    }

    protected void init()
    {
      this.localizedNameID = this.GetType().GenerateLocalizedID(this.iname, "NAME");
      this.localizedCondID = this.GetType().GenerateLocalizedID(this.iname, "COND");
      this.localizedTitleID = this.GetType().GenerateLocalizedID(this.iname, "TITLE");
    }

    public void Deserialize(string language, JSON_QuestParam json)
    {
      this.Deserialize(json);
      this.localizeFields(language);
    }

    public bool IsMulti
    {
      get
      {
        return (int) this.multi != 0;
      }
    }

    public bool IsMultiEvent
    {
      get
      {
        return (int) this.multi >= 100;
      }
    }

    public bool IsMultiVersus
    {
      get
      {
        if ((int) this.multi != 2)
          return (int) this.multi == 102;
        return true;
      }
    }

    public bool Silent
    {
      get
      {
        return (this.mFlags & QuestParam.QuestFlags.Silent) != (QuestParam.QuestFlags) 0;
      }
    }

    public bool IsScenario
    {
      get
      {
        if (this.map.Count != 0)
          return string.IsNullOrEmpty(this.map[0].mapSetName);
        return true;
      }
    }

    public bool IsStory
    {
      get
      {
        return this.type == QuestTypes.Story;
      }
    }

    public bool IsEvent
    {
      get
      {
        return this.type == QuestTypes.Event;
      }
    }

    public bool IsVersus
    {
      get
      {
        if (this.type != QuestTypes.VersusFree)
          return this.type == QuestTypes.VersusRank;
        return true;
      }
    }

    public bool IsKeyQuest
    {
      get
      {
        if (this.Chapter != null)
          return this.Chapter.IsKeyQuest();
        return false;
      }
    }

    public int GainPlayerExp
    {
      get
      {
        return (int) this.pexp;
      }
    }

    public int GainUnitExp
    {
      get
      {
        return (int) this.uexp;
      }
    }

    public int OverClockTimeWin
    {
      get
      {
        return (int) this.clock_win;
      }
    }

    public int OverClockTimeLose
    {
      get
      {
        return (int) this.clock_lose;
      }
    }

    public bool IsBeginner
    {
      get
      {
        return 0 != this.Beginner;
      }
    }

    public int GetAtkTypeMag(AttackDetailTypes type)
    {
      if (this.AtkTypeMags != null)
        return this.AtkTypeMags[(int) type];
      return 0;
    }

    public void SetAtkTypeMag(int[] mags)
    {
      this.AtkTypeMags = mags;
    }

    public void Deserialize(JSON_QuestParam json)
    {
      if (json == null)
        throw new InvalidJSONException();
      this.iname = json.iname;
      this.name = json.name;
      this.expr = json.expr;
      this.cond = json.cond;
      this.mission = json.mission;
      this.pexp = (OInt) json.pexp;
      this.uexp = (OInt) json.uexp;
      this.gold = (OInt) json.gold;
      this.mcoin = (OInt) json.mcoin;
      this.point = (OInt) json.pt;
      this.multi = (OInt) json.multi;
      this.multiDead = (OInt) json.multi_dead;
      this.playerNum = (OInt) json.pnum;
      this.unitNum = (OInt) (json.unum <= this.MULTI_MAX_PLAYER_UNIT ? json.unum : this.MULTI_MAX_PLAYER_UNIT);
      this.aplv = (OInt) json.aplv;
      this.challengeLimit = (OInt) json.limit;
      this.isDailyReset = (OBool) (json.dayreset > 0);
      if ((int) this.multi != 0)
      {
        if (json.pnum * json.unum > this.MULTI_MAX_TOTAL_UNIT)
          DebugUtility.LogError("iname:" + json.iname + " / Current total unit is " + (object) (json.pnum * json.unum) + ". Please set the total number of units to" + (object) this.MULTI_MAX_TOTAL_UNIT);
        if (json.unum > this.MULTI_MAX_PLAYER_UNIT)
          DebugUtility.LogError("iname:" + json.iname + " / Current 1 player unit is " + (object) json.unum + ". Please set the 1 player number of units to" + (object) this.MULTI_MAX_PLAYER_UNIT);
      }
      this.key_limit = json.key_limit;
      this.clock_win = (OInt) json.ctw;
      this.clock_lose = (OInt) json.ctl;
      this.lv = Math.Max(json.lv, 1);
      this.win = (OInt) json.win;
      this.lose = (OInt) json.lose;
      this.type = (QuestTypes) json.type;
      this.cond_quests = (string[]) null;
      this.units = (string[]) null;
      this.pieces = (string[]) null;
      this.ChapterID = json.area;
      this.world = json.world;
      this.storyTextID = json.text;
      this.hidden = json.hide != 0;
      this.replayLimit = json.replay_limit != 0;
      this.ticket = json.ticket;
      this.title = json.title;
      this.navigation = json.nav;
      this.AllowedJobs = json.ajob;
      this.AllowedTags = (QuestParam.Tags) 0;
      if (!string.IsNullOrEmpty(json.atag))
      {
        string[] strArray = json.atag.Split(',');
        for (int index = 0; index < strArray.Length; ++index)
        {
          if (!string.IsNullOrEmpty(strArray[index]))
            this.AllowedTags |= (QuestParam.Tags) Enum.Parse(typeof (QuestParam.Tags), strArray[index]);
        }
      }
      this.PhysBonus = json.phyb + 100;
      this.MagBonus = json.magb + 100;
      this.Beginner = json.bgnr;
      this.ItemLayout = json.i_lyt;
      this.dropItems = json.drops;
      ObjectiveParam objective = MonoSingleton<GameManager>.GetInstanceDirect().FindObjective(json.mission);
      if (objective != null)
      {
        this.bonusObjective = new QuestBonusObjective[objective.objective.Length];
        for (int index = 0; index < objective.objective.Length; ++index)
        {
          this.bonusObjective[index] = new QuestBonusObjective();
          this.bonusObjective[index].Type = (EMissionType) objective.objective[index].type;
          this.bonusObjective[index].TypeParam = objective.objective[index].val;
          this.bonusObjective[index].item = objective.objective[index].item;
          this.bonusObjective[index].itemNum = objective.objective[index].num;
        }
      }
      MagnificationParam magnification = MonoSingleton<GameManager>.GetInstanceDirect().FindMagnification(json.atk_mag);
      if (magnification != null && magnification.atkMagnifications != null)
        this.AtkTypeMags = magnification.atkMagnifications;
      QuestCondParam questCond1 = MonoSingleton<GameManager>.GetInstanceDirect().FindQuestCond(json.rdy_cnd);
      if (questCond1 != null)
        this.EntryCondition = questCond1;
      QuestCondParam questCond2 = MonoSingleton<GameManager>.GetInstanceDirect().FindQuestCond(json.rdy_cnd_ch);
      if (questCond2 != null)
        this.EntryConditionCh = questCond2;
      this.difficulty = (QuestDifficulties) json.mode;
      if (json.pieces != null)
      {
        this.pieces = new string[json.pieces.Length];
        for (int index = 0; index < json.pieces.Length; ++index)
          this.pieces[index] = json.pieces[index];
      }
      if (json.units != null)
      {
        this.units = new string[json.units.Length];
        for (int index = 0; index < json.units.Length; ++index)
          this.units[index] = json.units[index];
      }
      if (json.cond_quests != null)
      {
        this.cond_quests = new string[json.cond_quests.Length];
        for (int index = 0; index < json.cond_quests.Length; ++index)
          this.cond_quests[index] = json.cond_quests[index];
      }
      this.map.Clear();
      if (json.map != null)
      {
        for (int index = 0; index < json.map.Length; ++index)
        {
          MapParam mapParam = new MapParam();
          mapParam.Deserialize(json.map[index]);
          this.map.Add(mapParam);
        }
      }
      this.event_start = json.evst;
      this.event_clear = json.evw;
      this.mFlags = (QuestParam.QuestFlags) 0;
      if (json.retr == 0)
        this.mFlags |= QuestParam.QuestFlags.AllowRetreat;
      if (json.naut == 0)
        this.mFlags |= QuestParam.QuestFlags.AllowAutoPlay;
      if (json.swin != 0)
        this.mFlags |= QuestParam.QuestFlags.Silent;
      if (json.notabl != 0)
        this.mFlags |= QuestParam.QuestFlags.DisableAbilities;
      if (json.notitm != 0)
        this.mFlags |= QuestParam.QuestFlags.DisableItems;
      if (json.notcon != 0)
        this.mFlags |= QuestParam.QuestFlags.DisableContinue;
      this.UseFixEditor = json.fix_editor != 0;
      this.IsNoStartVoice = json.is_no_start_voice != 0;
      this.UseSupportUnit = json.sprt == 0;
      this.VersusThumnail = json.thumnail;
      this.MapBuff = json.mskill;
      this.VersusMoveCount = json.vsmovecnt;
    }

    public bool IsUnitAllowed(UnitData unit)
    {
      if (unit == null)
        return true;
      if (!string.IsNullOrEmpty(this.AllowedJobs) && unit.CurrentJob != null && unit.CurrentJob.Param != null)
      {
        string iname = unit.CurrentJob.Param.iname;
        int length = iname.Length;
        int num = this.AllowedJobs.IndexOf(iname);
        if (num < 0 || 0 < num && (int) this.AllowedJobs[num - 1] != 44 || num + length < this.AllowedJobs.Length && (int) this.AllowedJobs[num + length - 1] != 44)
          return false;
      }
      return this.AllowedTags == (QuestParam.Tags) 0 || ((this.AllowedTags & QuestParam.Tags.MAL) == (QuestParam.Tags) 0 || unit.UnitParam.sex == ESex.Male) && ((this.AllowedTags & QuestParam.Tags.FEM) == (QuestParam.Tags) 0 || unit.UnitParam.sex == ESex.Female) && ((this.AllowedTags & QuestParam.Tags.HERO) == (QuestParam.Tags) 0 || (int) unit.UnitParam.hero != 0);
    }

    public bool IsQuestCondition()
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      if (!instance.Player.IsBeginner() && this.IsBeginner)
        return false;
      if (this.cond_quests != null)
      {
        for (int index = 0; index < this.cond_quests.Length; ++index)
        {
          QuestParam quest = instance.FindQuest(this.cond_quests[index]);
          if (quest != null && quest.state != QuestStates.Cleared)
            return false;
        }
      }
      return true;
    }

    public List<QuestParam> DetectNotClearConditionQuests()
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      if (!instance.Player.IsBeginner() && this.IsBeginner)
        return (List<QuestParam>) null;
      if (this.cond_quests == null)
        return (List<QuestParam>) null;
      List<QuestParam> questParamList = new List<QuestParam>();
      for (int index = 0; index < this.cond_quests.Length; ++index)
      {
        QuestParam quest = instance.FindQuest(this.cond_quests[index]);
        if (quest != null && quest.state != QuestStates.Cleared)
          questParamList.Add(quest);
      }
      return questParamList;
    }

    public bool IsEntryQuestCondition(UnitData[] entryUnits, ref string error)
    {
      error = string.Empty;
      if (this.EntryCondition == null)
        return true;
      int num = MonoSingleton<GameManager>.Instance.Player.CalcLevel();
      if (this.EntryCondition.plvmax > 0 && num > this.EntryCondition.plvmax)
      {
        error = "sys.PARTYEDITOR_PLV";
        return false;
      }
      if (this.EntryCondition.plvmin > 0 && num < this.EntryCondition.plvmin)
      {
        error = "sys.PARTYEDITOR_PLV";
        return false;
      }
      foreach (UnitData entryUnit in entryUnits)
      {
        if (entryUnit != null && !this.IsEntryQuestCondition(entryUnit, ref error))
          return false;
      }
      return true;
    }

    public bool IsEntryQuestCondition(UnitData unit, ref string error)
    {
      return this.IsEntryQuestCondition(this.EntryCondition, unit, ref error);
    }

    public bool IsEntryQuestConditionCh(UnitData unit, ref string error)
    {
      return this.IsEntryQuestCondition(this.EntryConditionCh, unit, ref error);
    }

    private bool IsEntryQuestCondition(QuestCondParam condition, UnitData unit, ref string error)
    {
      error = string.Empty;
      if (condition == null)
      {
        if (this.type == QuestTypes.Tower)
          DebugUtility.LogError("塔 コンディションが入っていません iname = " + this.iname);
        return true;
      }
      if (unit == null)
        return false;
      if (condition.unit != null && condition.unit.Length > 0 && Array.IndexOf<string>(condition.unit, unit.UnitID) == -1)
      {
        error = "sys.PARTYEDITOR_UNIT";
        return false;
      }
      if (condition.sex != ESex.Unknown && condition.sex != unit.UnitParam.sex)
      {
        error = "sys.PARTYEDITOR_SEX";
        return false;
      }
      int num1 = Math.Max(condition.rmax - 1, 0);
      if (condition.rmax > 0 && unit.Rarity > num1)
      {
        error = "sys.PARTYEDITOR_RARITY";
        return false;
      }
      int num2 = Math.Max(condition.rmin - 1, 0);
      if (condition.rmin > 0 && unit.Rarity < num2)
      {
        error = "sys.PARTYEDITOR_RARITY";
        return false;
      }
      if (condition.hmax > 0 && ((int) unit.UnitParam.height == 0 || (int) unit.UnitParam.height > condition.hmax))
      {
        error = "sys.PARTYEDITOR_HEIGHT";
        return false;
      }
      if (condition.hmin > 0 && ((int) unit.UnitParam.height == 0 || (int) unit.UnitParam.height < condition.hmin))
      {
        error = "sys.PARTYEDITOR_HEIGHT";
        return false;
      }
      if (condition.wmax > 0 && ((int) unit.UnitParam.weight == 0 || (int) unit.UnitParam.weight > condition.wmax))
      {
        error = "sys.PARTYEDITOR_WEIGHT";
        return false;
      }
      if (condition.wmin > 0 && ((int) unit.UnitParam.weight == 0 || (int) unit.UnitParam.weight < condition.wmin))
      {
        error = "sys.PARTYEDITOR_WEIGHT";
        return false;
      }
      if (condition.jobset != null && condition.jobset.Length > 0 && Array.IndexOf<int>(condition.jobset, 1) != -1)
      {
        int jobIndex = unit.JobIndex;
        if (jobIndex < 0 || jobIndex >= condition.jobset.Length || condition.jobset[jobIndex] == 0)
        {
          error = "sys.PARTYEDITOR_JOBINDEX";
          return false;
        }
      }
      if (condition.birth != null && condition.birth.Length > 0 && Array.IndexOf<string>(condition.birth, (string) unit.UnitParam.birth) == -1)
      {
        error = "sys.PARTYEDITOR_BIRTH";
        return false;
      }
      if (condition.isElemLimit && condition.elem[(int) unit.Element] == 0)
      {
        error = "sys.PARTYEDITOR_ELEM";
        return false;
      }
      if (condition.job != null)
      {
        JobData currentJob = unit.CurrentJob;
        if (currentJob == null || currentJob.Param == null)
        {
          error = "sys.PARTYEDITOR_JOB";
          return false;
        }
        if (Array.IndexOf<string>(condition.job, currentJob.JobID) == -1)
        {
          if (string.IsNullOrEmpty(currentJob.Param.origin))
          {
            error = "sys.PARTYEDITOR_JOB";
            return false;
          }
          JobParam jobParam = MonoSingleton<GameManager>.GetInstanceDirect().GetJobParam(currentJob.Param.origin);
          if (jobParam == null || Array.IndexOf<string>(condition.job, jobParam.iname) == -1)
          {
            error = "sys.PARTYEDITOR_JOB";
            return false;
          }
        }
      }
      int num3 = unit.CalcLevel();
      if (condition.ulvmax > 0 && num3 > condition.ulvmax)
      {
        error = "sys.PARTYEDITOR_ULV";
        return false;
      }
      if (condition.ulvmin > 0 && num3 < condition.ulvmin)
      {
        error = "sys.PARTYEDITOR_ULV";
        return false;
      }
      if (this.type == QuestTypes.Tower && MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(unit.UniqueID) != null)
      {
        TowerResuponse towerResuponse = MonoSingleton<GameManager>.Instance.TowerResuponse;
        if (towerResuponse.pdeck != null)
        {
          TowerResuponse.PlayerUnit playerUnit = towerResuponse.pdeck.Find((Predicate<TowerResuponse.PlayerUnit>) (x => unit.UnitParam.iname == x.unitname));
          if (playerUnit != null && playerUnit.isDied)
          {
            error = "sys.ERROR_TOWER_DEAD_UNIT";
            return false;
          }
        }
      }
      return true;
    }

    public List<string> GetEntryQuestConditions(bool titled = true)
    {
      return this.GetEntryQuestConditionsInternal(this.EntryCondition, titled);
    }

    public List<string> GetEntryQuestConditionsCh(bool titled = true)
    {
      return this.GetEntryQuestConditionsInternal(this.EntryConditionCh, titled);
    }

    private List<string> GetEntryQuestConditionsInternal(QuestCondParam condParam, bool titled = true)
    {
      List<string> stringList1 = new List<string>();
      if (condParam == null)
        return stringList1;
      GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
      if (condParam.plvmin > 0 || condParam.plvmax > 0)
      {
        int playerLevelCap = instanceDirect.MasterParam.GetPlayerLevelCap();
        int num1 = Math.Max(condParam.plvmin, 1);
        int num2 = Math.Min(condParam.plvmax, playerLevelCap);
        string str1 = string.Empty;
        string str2;
        if (titled)
        {
          string str3 = LocalizedText.Get("sys.PARTYEDITOR_COND_PLV");
          if (num1 > 0)
            str3 += (string) (object) num1;
          str2 = str3 + LocalizedText.Get("sys.TILDE");
          if (num2 > 0)
            str2 += (string) (object) num2;
        }
        else
        {
          if (num1 > 0)
            str1 = str1 + LocalizedText.Get("sys.PLV") + (object) num1;
          str2 = str1 + LocalizedText.Get("sys.TILDE");
          if (num2 > 0)
            str2 = str2 + LocalizedText.Get("sys.PLV") + (object) num2;
        }
        stringList1.Add(str2);
      }
      if (condParam.ulvmin > 0 || condParam.ulvmax > 0)
      {
        int unitMaxLevel = instanceDirect.MasterParam.GetUnitMaxLevel();
        int num1 = Math.Max(condParam.ulvmin, 1);
        int num2 = Math.Min(condParam.ulvmax, unitMaxLevel);
        string str1 = string.Empty;
        string str2;
        if (titled)
        {
          string str3 = LocalizedText.Get("sys.PARTYEDITOR_COND_ULV");
          if (num1 > 0)
            str3 += (string) (object) num1;
          str2 = str3 + LocalizedText.Get("sys.TILDE");
          if (num2 > 0)
            str2 += (string) (object) num2;
        }
        else
        {
          if (num1 > 0)
            str1 = str1 + LocalizedText.Get("sys.ULV") + (object) num1;
          str2 = str1 + LocalizedText.Get("sys.TILDE");
          if (num2 > 0)
            str2 = str2 + LocalizedText.Get("sys.ULV") + (object) num2;
        }
        stringList1.Add(str2);
      }
      if (condParam.unit != null && condParam.unit.Length > 0)
      {
        List<string> stringList2 = new List<string>();
        for (int index = 0; index < condParam.unit.Length; ++index)
        {
          UnitParam unitParam = instanceDirect.GetUnitParam(condParam.unit[index]);
          if (unitParam != null && !stringList2.Contains(unitParam.name))
            stringList2.Add(unitParam.name);
        }
        if (stringList2.Count > 0)
        {
          string str1 = string.Empty;
          for (int index = 0; index < stringList2.Count; ++index)
            str1 = str1 + (index <= 0 ? string.Empty : ", ") + stringList2[index];
          if (!string.IsNullOrEmpty(str1))
          {
            string str2 = string.Empty;
            if (titled)
              str2 = LocalizedText.Get("sys.PARTYEDITOR_COND_UNIT") + (object) ' ';
            string str3 = str2 + str1;
            stringList1.Add(str3);
          }
        }
      }
      if (condParam.job != null && condParam.job.Length > 0)
      {
        List<string> stringList2 = new List<string>();
        for (int index = 0; index < condParam.job.Length; ++index)
        {
          JobParam jobParam = instanceDirect.GetJobParam(condParam.job[index]);
          if (jobParam != null && !stringList2.Contains(jobParam.name))
            stringList2.Add(jobParam.name);
        }
        if (stringList2.Count > 0)
        {
          string str1 = string.Empty;
          for (int index = 0; index < stringList2.Count; ++index)
            str1 = str1 + (index <= 0 ? string.Empty : ", ") + stringList2[index];
          if (!string.IsNullOrEmpty(str1))
          {
            string str2 = string.Empty;
            if (titled)
              str2 = LocalizedText.Get("sys.PARTYEDITOR_COND_JOB") + (object) ' ';
            string str3 = str2 + str1;
            stringList1.Add(str3);
          }
        }
      }
      if (condParam.jobset != null && condParam.jobset.Length > 0 && Array.IndexOf<int>(condParam.jobset, 1) != -1)
      {
        string str1 = string.Empty;
        int index = 0;
        int num1 = 0;
        for (; index < condParam.jobset.Length; ++index)
        {
          if (condParam.jobset[index] != 0)
          {
            int num2 = index + 1;
            str1 = str1 + (num1 <= 0 ? (object) string.Empty : (object) ", ") + (object) num2;
            ++num1;
          }
        }
        if (!string.IsNullOrEmpty(str1))
        {
          string empty = string.Empty;
          if (titled)
            empty = LocalizedText.Get("sys.PARTYEDITOR_COND_JOBINDEX");
          string str2 = empty + LocalizedText.Get("sys.PARTYEDITOR_COND_JOBINDEX_VALUE", new object[1]{ (object) str1 });
          stringList1.Add(str2);
        }
      }
      if (condParam.rmin > 0 || condParam.rmax > 0)
      {
        string empty = string.Empty;
        if (titled)
          empty = LocalizedText.Get("sys.PARTYEDITOR_COND_RARITY");
        int num1 = Math.Max(condParam.rmin - 1, 0);
        int num2 = Math.Max(condParam.rmax - 1, 0);
        string str;
        if (num1 == num2)
        {
          str = empty + LocalizedText.Get("sys.RARITY_STAR_" + (object) num1);
        }
        else
        {
          if (num1 > 0)
            empty += LocalizedText.Get("sys.RARITY_STAR_" + (object) num1);
          str = empty + LocalizedText.Get("sys.TILDE");
          if (num2 > 0)
            str += LocalizedText.Get("sys.RARITY_STAR_" + (object) num2);
        }
        stringList1.Add(str);
      }
      if (condParam.isElemLimit)
      {
        string str1 = string.Empty;
        int index = 0;
        int num = 0;
        for (; index < condParam.elem.Length; ++index)
        {
          if (index != 0 && condParam.elem[index] != 0)
          {
            str1 = str1 + (num <= 0 ? string.Empty : ", ") + LocalizedText.Get("sys.UNIT_ELEMENT_" + (object) index);
            ++num;
          }
        }
        if (!string.IsNullOrEmpty(str1))
        {
          string empty = string.Empty;
          if (titled)
            empty = LocalizedText.Get("sys.PARTYEDITOR_COND_ELEM");
          string str2 = empty + str1;
          stringList1.Add(str2);
        }
      }
      if (condParam.birth != null && condParam.birth.Length > 0)
      {
        string str1 = string.Empty;
        for (int index = 0; index < condParam.birth.Length; ++index)
          str1 = str1 + (index <= 0 ? string.Empty : ", ") + condParam.birth[index];
        if (!string.IsNullOrEmpty(str1))
        {
          string empty = string.Empty;
          if (titled)
            empty = LocalizedText.Get("sys.PARTYEDITOR_COND_BIRTH");
          string str2 = empty + str1;
          stringList1.Add(str2);
        }
      }
      if (condParam.sex != ESex.Unknown)
      {
        string str1 = LocalizedText.Get("sys.SEX_" + (object) condParam.sex);
        if (!string.IsNullOrEmpty(str1))
        {
          string empty = string.Empty;
          if (titled)
            empty = LocalizedText.Get("sys.PARTYEDITOR_COND_SEX");
          string str2 = empty + str1;
          stringList1.Add(str2);
        }
      }
      if (condParam.hmin > 0 || condParam.hmax > 0)
      {
        int hmin = condParam.hmin;
        int hmax = condParam.hmax;
        string str1 = string.Empty;
        if (titled)
          str1 = LocalizedText.Get("sys.PARTYEDITOR_COND_HEIGHT");
        if (hmin > 0)
          str1 = str1 + (object) hmin + LocalizedText.Get("sys.CM_HEIGHT");
        string str2 = str1 + LocalizedText.Get("sys.TILDE");
        if (hmax > 0)
          str2 = str2 + (object) hmax + LocalizedText.Get("sys.CM_HEIGHT");
        stringList1.Add(str2);
      }
      if (condParam.wmin > 0 || condParam.wmax > 0)
      {
        int wmin = condParam.wmin;
        int wmax = condParam.wmax;
        string str1 = string.Empty;
        if (titled)
          str1 = LocalizedText.Get("sys.PARTYEDITOR_COND_WEIGHT");
        if (wmin > 0)
          str1 = str1 + (object) wmin + LocalizedText.Get("sys.KG_WEIGHT");
        string str2 = str1 + LocalizedText.Get("sys.TILDE");
        if (wmax > 0)
          str2 = str2 + (object) wmax + LocalizedText.Get("sys.KG_WEIGHT");
        stringList1.Add(str2);
      }
      return stringList1;
    }

    public bool IsJigen
    {
      get
      {
        return this.end != 0L;
      }
    }

    public bool IsDateUnlock(long serverTime = -1)
    {
      long num = serverTime >= 0L ? serverTime : Network.GetServerTime();
      if (!MonoSingleton<GameManager>.Instance.Player.IsBeginner() && this.IsBeginner)
        return false;
      if (!this.IsJigen)
        return !this.hidden;
      return this.start <= num && num < this.end;
    }

    public bool IsReplayDateUnlock(long serverTime = -1)
    {
      if (!this.replayLimit)
        return true;
      return this.IsDateUnlock(serverTime);
    }

    public int GetSelectMainMemberNum()
    {
      PartyData party = MonoSingleton<GameManager>.Instance.Player.Partys[(int) GlobalVars.SelectedPartyIndex];
      int num = 0;
      switch (this.type)
      {
        case QuestTypes.Story:
        case QuestTypes.Multi:
        case QuestTypes.Tutorial:
        case QuestTypes.Character:
          num = party.MAX_MAINMEMBER - 1;
          break;
        case QuestTypes.Arena:
        case QuestTypes.Free:
        case QuestTypes.Event:
        case QuestTypes.Tower:
          num = party.MAX_MAINMEMBER;
          break;
      }
      if (this.UseFixEditor)
        num = party.MAX_MAINMEMBER - 1;
      return num;
    }

    public bool CheckEnableEntrySubMembers()
    {
      switch (this.type)
      {
        case QuestTypes.Story:
        case QuestTypes.Tutorial:
        case QuestTypes.Free:
        case QuestTypes.Event:
        case QuestTypes.Character:
        case QuestTypes.Tower:
          return true;
        default:
          return false;
      }
    }

    public bool CheckAllowedAutoBattle()
    {
      switch (this.type)
      {
        case QuestTypes.Story:
        case QuestTypes.Free:
        case QuestTypes.Event:
        case QuestTypes.Character:
        case QuestTypes.Tower:
          return (this.mFlags & QuestParam.QuestFlags.AllowAutoPlay) != (QuestParam.QuestFlags) 0;
        default:
          return false;
      }
    }

    public bool CheckAllowedRetreat()
    {
      if (this.type == QuestTypes.Tutorial)
        return false;
      return (this.mFlags & QuestParam.QuestFlags.AllowRetreat) != (QuestParam.QuestFlags) 0;
    }

    public bool CheckDisableAbilities()
    {
      return (this.mFlags & QuestParam.QuestFlags.DisableAbilities) != (QuestParam.QuestFlags) 0;
    }

    public bool CheckDisableItems()
    {
      return (this.mFlags & QuestParam.QuestFlags.DisableItems) != (QuestParam.QuestFlags) 0;
    }

    public bool CheckDisableContinue()
    {
      return (this.mFlags & QuestParam.QuestFlags.DisableContinue) != (QuestParam.QuestFlags) 0;
    }

    public bool CheckEnableQuestResult()
    {
      return this.type != QuestTypes.Tutorial && this.type != QuestTypes.Arena;
    }

    public bool CheckEnableGainedItem()
    {
      return this.type != QuestTypes.Tutorial;
    }

    public bool CheckEnableGainedGold()
    {
      return this.type != QuestTypes.Tutorial;
    }

    public bool CheckEnableGainedExp()
    {
      return this.type != QuestTypes.Tutorial;
    }

    public bool CheckEnableSuspendStart()
    {
      return this.type != QuestTypes.Tutorial && this.type != QuestTypes.Arena && this.type != QuestTypes.Multi;
    }

    public void SetChallangeCount(int count)
    {
      if (this.IsKeyQuest && this.Chapter != null && this.Chapter.GetKeyQuestType() == KeyQuestTypes.Count)
        this.key_cnt = count;
      else
        this.dailyCount = (OInt) count;
    }

    public int GetChallangeCount()
    {
      if (this.IsKeyQuest && this.Chapter != null && this.Chapter.GetKeyQuestType() == KeyQuestTypes.Count)
        return this.key_cnt;
      return (int) this.dailyCount;
    }

    public int GetChallangeLimit()
    {
      if (this.IsKeyQuest && this.Chapter != null && this.Chapter.GetKeyQuestType() == KeyQuestTypes.Count)
        return this.key_limit;
      return (int) this.challengeLimit;
    }

    public bool CheckEnableChallange()
    {
      int challangeLimit = this.GetChallangeLimit();
      return challangeLimit == 0 || this.GetChallangeCount() < challangeLimit;
    }

    public bool CheckEnableReset()
    {
      if (this.difficulty != QuestDifficulties.Elite)
        return false;
      return (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.EliteResetMax > (int) this.dailyReset;
    }

    public int RequiredApWithPlayerLv(int playerLv, bool campaign = true)
    {
      if (playerLv < (int) this.aplv)
        return 0;
      int point = (int) this.point;
      if (campaign)
      {
        foreach (QuestCampaignData questCampaign in MonoSingleton<GameManager>.Instance.FindQuestCampaigns(this))
        {
          if (questCampaign.type == QuestCampaignValueTypes.Ap)
          {
            point = Mathf.RoundToInt((float) point * questCampaign.GetRate());
            break;
          }
        }
      }
      return point;
    }

    public static QuestTypes ToQuestType(string name)
    {
      string key = name;
      if (key != null)
      {
        // ISSUE: reference to a compiler-generated field
        if (QuestParam.\u003C\u003Ef__switch\u0024map3 == null)
        {
          // ISSUE: reference to a compiler-generated field
          QuestParam.\u003C\u003Ef__switch\u0024map3 = new Dictionary<string, int>(10)
          {
            {
              "Story",
              0
            },
            {
              "multi",
              1
            },
            {
              "Arena",
              2
            },
            {
              "Tutorial",
              3
            },
            {
              "Free",
              4
            },
            {
              "Event",
              5
            },
            {
              "Character",
              6
            },
            {
              "tower",
              7
            },
            {
              "vs",
              8
            },
            {
              "vs_tower",
              8
            }
          };
        }
        int num;
        // ISSUE: reference to a compiler-generated field
        if (QuestParam.\u003C\u003Ef__switch\u0024map3.TryGetValue(key, out num))
        {
          switch (num)
          {
            case 0:
              return QuestTypes.Story;
            case 1:
              return QuestTypes.Multi;
            case 2:
              return QuestTypes.Arena;
            case 3:
              return QuestTypes.Tutorial;
            case 4:
              return QuestTypes.Free;
            case 5:
              return QuestTypes.Event;
            case 6:
              return QuestTypes.Character;
            case 7:
              return QuestTypes.Tower;
            case 8:
              return QuestTypes.VersusFree;
          }
        }
      }
      return QuestTypes.Story;
    }

    public bool IsCharacterQuest()
    {
      if (this.type == QuestTypes.Character && this.world != null)
        return this.world == GameSettings.Instance.CharacterQuestSection;
      return false;
    }

    public static PlayerPartyTypes QuestToPartyIndex(QuestTypes type)
    {
      switch (type)
      {
        case QuestTypes.Multi:
          return PlayerPartyTypes.Multiplay;
        case QuestTypes.Arena:
          return PlayerPartyTypes.Arena;
        case QuestTypes.Free:
          return PlayerPartyTypes.Event;
        case QuestTypes.Character:
          return PlayerPartyTypes.Character;
        case QuestTypes.Tower:
          return PlayerPartyTypes.Tower;
        case QuestTypes.VersusFree:
        case QuestTypes.VersusRank:
          return PlayerPartyTypes.Versus;
        default:
          return PlayerPartyTypes.Normal;
      }
    }

    public bool IsKeyUnlock(long serverTime = -1)
    {
      if (this.Chapter != null)
        return this.Chapter.IsKeyUnlock(serverTime >= 0L ? serverTime : Network.GetServerTime());
      return false;
    }

    public void GetPartyTypes(out PlayerPartyTypes playerPartyType, out PlayerPartyTypes enemyPartyType)
    {
      switch (this.type)
      {
        case QuestTypes.Multi:
          playerPartyType = PlayerPartyTypes.Multiplay;
          enemyPartyType = PlayerPartyTypes.Multiplay;
          break;
        case QuestTypes.Arena:
          playerPartyType = PlayerPartyTypes.Arena;
          enemyPartyType = PlayerPartyTypes.ArenaDef;
          break;
        case QuestTypes.Event:
          playerPartyType = PlayerPartyTypes.Event;
          enemyPartyType = PlayerPartyTypes.Event;
          break;
        case QuestTypes.Tower:
          playerPartyType = PlayerPartyTypes.Tower;
          enemyPartyType = PlayerPartyTypes.Tower;
          break;
        default:
          playerPartyType = PlayerPartyTypes.Normal;
          enemyPartyType = PlayerPartyTypes.Normal;
          break;
      }
    }

    public bool TransSectionGotoQuest(string questID, out QuestTypes quest_type, UIUtility.DialogResultEvent callback)
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      quest_type = QuestTypes.Story;
      if (string.IsNullOrEmpty(questID))
      {
        this.TransSectionGotoNormal();
        quest_type = QuestTypes.Story;
        return true;
      }
      QuestParam quest = instance.FindQuest(questID);
      if (quest == null)
      {
        this.TransSectionGotoNormal();
        quest_type = QuestTypes.Story;
        UIUtility.SystemMessage((string) null, LocalizedText.Get("sys.QUEST_UNAVAILABLE"), callback, (GameObject) null, false, -1);
        return false;
      }
      QuestTypes type = quest.type;
      switch (type)
      {
        case QuestTypes.Event:
          quest_type = QuestTypes.Event;
          if (!this.TransSectionGotoEvent(questID, callback))
            return false;
          break;
        case QuestTypes.Tower:
          quest_type = QuestTypes.Tower;
          if (!this.TransSectionGotoTower(questID, out quest_type))
            return false;
          break;
        default:
          if (type == QuestTypes.Multi)
          {
            quest_type = QuestTypes.Multi;
            break;
          }
          quest_type = QuestTypes.Story;
          if (!this.TransSectionGotoStory(questID, callback))
            return false;
          break;
      }
      return true;
    }

    public bool TransSectionGotoNormal()
    {
      FlowNode_SelectLatestChapter.SelectLatestChapter();
      return true;
    }

    public bool TransSectionGotoElite(UIUtility.DialogResultEvent callback)
    {
      QuestParam[] availableQuests = MonoSingleton<GameManager>.Instance.Player.AvailableQuests;
      QuestParam questParam = (QuestParam) null;
      for (int index = availableQuests.Length - 1; index >= 0; --index)
      {
        if (availableQuests[index].difficulty == QuestDifficulties.Elite)
        {
          questParam = availableQuests[index];
          break;
        }
      }
      if (questParam == null)
      {
        this.TransSectionGotoNormal();
        UIUtility.SystemMessage((string) null, LocalizedText.Get("sys.EQUEST_UNAVAILABLE"), callback, (GameObject) null, false, -1);
        return false;
      }
      string chapterId = questParam.ChapterID;
      string str = "WD_01";
      ChapterParam[] chapters = MonoSingleton<GameManager>.Instance.Chapters;
      for (int index = 0; index < chapters.Length; ++index)
      {
        if (chapters[index].iname == chapterId)
        {
          str = chapters[index].section;
          break;
        }
      }
      GlobalVars.SelectedQuestID = questParam.iname;
      GlobalVars.SelectedChapter.Set(chapterId);
      GlobalVars.SelectedSection.Set(str);
      return true;
    }

    public bool TransSectionGotoStory(string questID, UIUtility.DialogResultEvent callback)
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(questID);
      if (!player.IsQuestAvailable(questID))
      {
        UIUtility.SystemMessage((string) null, LocalizedText.Get("sys.QUEST_UNAVAILABLE"), callback, (GameObject) null, false, -1);
        this.TransSectionGotoNormal();
        return false;
      }
      string str1 = quest == null ? (string) null : quest.ChapterID;
      string str2 = "WD_01";
      ChapterParam[] chapters = MonoSingleton<GameManager>.Instance.Chapters;
      for (int index = 0; index < chapters.Length; ++index)
      {
        if (chapters[index].iname == str1)
        {
          str2 = chapters[index].section;
          break;
        }
      }
      GlobalVars.SelectedQuestID = questID;
      GlobalVars.SelectedChapter.Set(str1);
      GlobalVars.SelectedSection.Set(str2);
      return true;
    }

    public bool TransSectionGotoTower(string questID, out QuestTypes quest_type)
    {
      quest_type = QuestTypes.Tower;
      TowerFloorParam towerFloorParam = (TowerFloorParam) null;
      if (!string.IsNullOrEmpty(questID))
      {
        towerFloorParam = MonoSingleton<GameManager>.Instance.FindTowerFloor(questID);
        if (towerFloorParam == null)
        {
          DebugUtility.LogError("[クエストID = " + questID + "]が見つかりません。");
          this.GotoEventListChapter();
          quest_type = QuestTypes.Event;
          return true;
        }
      }
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      if (player.CheckUnlock(UnlockTargets.Tower))
      {
        if (towerFloorParam != null)
        {
          GlobalVars.SelectedTowerID = towerFloorParam.tower_id;
        }
        else
        {
          this.GotoEventListChapter();
          quest_type = QuestTypes.Event;
        }
        return true;
      }
      LevelLock.ShowLockMessage(player.Lv, player.VipRank, UnlockTargets.Tower);
      return false;
    }

    public bool TransSectionGotoEvent(string questID, UIUtility.DialogResultEvent callback)
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(questID);
      if (!player.IsQuestAvailable(questID))
      {
        UIUtility.SystemMessage((string) null, LocalizedText.Get("sys.EVENT_UNAVAILABLE"), callback, (GameObject) null, false, -1);
        this.GotoEventListChapter();
        return false;
      }
      this.GotoEventListQuest(questID, quest == null ? (string) null : quest.ChapterID);
      return true;
    }

    public void GotoEventListChapter()
    {
      FlowNode_Variable.Set("SHOW_CHAPTER", "1");
      GlobalVars.SelectedQuestID = (string) null;
      GlobalVars.SelectedChapter.Set((string) null);
      GlobalVars.SelectedSection.Set("WD_DAILY");
    }

    public void GotoEventListQuest(string questID, string chapter)
    {
      ChapterParam chapterParam = chapter == null ? (ChapterParam) null : MonoSingleton<GameManager>.Instance.FindArea(chapter);
      if (chapterParam != null && chapterParam.IsKeyQuest())
      {
        FlowNode_Variable.Set("SHOW_CHAPTER", "1");
        GlobalVars.ReqEventPageListType = GlobalVars.EventQuestListType.KeyQuest;
        GlobalVars.SelectedQuestID = (string) null;
        GlobalVars.SelectedChapter.Set((string) null);
        GlobalVars.SelectedSection.Set("WD_DAILY");
      }
      else
      {
        FlowNode_Variable.Set("SHOW_CHAPTER", string.IsNullOrEmpty(questID) || string.IsNullOrEmpty(chapter) ? "1" : "0");
        GlobalVars.SelectedQuestID = questID;
        GlobalVars.SelectedChapter.Set(chapter);
        GlobalVars.SelectedSection.Set("WD_DAILY");
      }
    }

    [Flags]
    private enum QuestFlags
    {
      AllowRetreat = 1,
      AllowAutoPlay = 2,
      Silent = 4,
      DisableAbilities = 8,
      DisableItems = 16, // 0x00000010
      DisableContinue = 32, // 0x00000020
    }

    [Flags]
    public enum Tags
    {
      MAL = 1,
      FEM = 2,
      HERO = 4,
    }
  }
}
