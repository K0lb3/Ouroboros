// Decompiled with JetBrains decompiler
// Type: SRPG.QuestBonusObjective
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;

namespace SRPG
{
  public class QuestBonusObjective
  {
    private static Dictionary<EMissionType, QuestMissionTypeAttribute> m_QuestMissionTypeDic = new Dictionary<EMissionType, QuestMissionTypeAttribute>();
    private static Dictionary<EMissionType, TowerQuestMissionTypeAttribute> m_TowerQuestMissionTypeDic = new Dictionary<EMissionType, TowerQuestMissionTypeAttribute>();
    public const string TXT_ID_SUFFIX_PROGRESS = "_PROGRESS";
    public const string TXT_ID_SUFFIX_PROGRESS_TARGET = "_PROGRESS_TARGET";
    public const string TXT_ID_SUFFIX_PROGRESS_OK = "_PROGRESS_OK";
    public const string TXT_ID_SUFFIX_PROGRESS_NG = "_PROGRESS_NG";
    public const int FAILED_MISSION_VALUE = 1;
    public string item;
    public int itemNum;
    public RewardType itemType;
    public EMissionType Type;
    public string TypeParam;
    public bool IsTakeoverProgress;

    public bool IsMissionTypeExecSkill()
    {
      return this.Type == EMissionType.UseTargetSkill || this.Type == EMissionType.KillstreakByUsingTargetSkill || this.Type == EMissionType.KillstreakByUsingTargetItem;
    }

    public bool IsProgressMission()
    {
      if (!this.IsTakeoverProgress)
        return false;
      TowerQuestMissionTypeAttribute missionTypeAttribute = this.GetTowerQuestMissionTypeAttribute(this.Type);
      if (missionTypeAttribute == null)
      {
        DebugUtility.LogError(string.Format("塔ではサポートされていないミッションです。⇒ type : {0}", (object) this.Type));
        return false;
      }
      if (missionTypeAttribute.ProgressJudgeType != QuestMissionProgressJudgeType.NotSupport)
        return true;
      DebugUtility.LogError(string.Format("このミッションはバトルを跨ぐ設定をサポートしていません。⇒ type : {0}", (object) this.Type));
      return false;
    }

    public bool IsMissionTypeAllowLose()
    {
      if (!this.IsTakeoverProgress)
        return this.Type == EMissionType.Killstreak || this.Type == EMissionType.TotalDamagesTakenMin || (this.Type == EMissionType.TotalDamagesMin || this.Type == EMissionType.TargetKillstreak) || (this.Type == EMissionType.BreakObjClashMin || this.Type == EMissionType.TotalGetGemCount_Over);
      return this.IsProgressMission() && this.Type != EMissionType.Finisher;
    }

    public bool CheckHomeMissionValueAchievable(int missions_val)
    {
      if (this.Type == EMissionType.ChallengeCountMax)
        ++missions_val;
      return this.CheckMissionValueAchievable(missions_val);
    }

    public bool CheckMissionValueAchievable(int missions_val)
    {
      if (!this.IsProgressMission())
        return false;
      TowerQuestMissionTypeAttribute missionTypeAttribute1 = this.GetTowerQuestMissionTypeAttribute(this.Type);
      if (missionTypeAttribute1 == null)
      {
        DebugUtility.LogError(string.Format("塔ではサポートされていないミッションです。⇒ type : {0}", (object) this.Type));
        return false;
      }
      QuestMissionTypeAttribute missionTypeAttribute2 = this.GetQuestMissionTypeAttribute(this.Type);
      string empty = string.Empty;
      int num = 0;
      if (missionTypeAttribute2.ValueType == QuestMissionValueType.ValueIsInt)
        this.TryGetIntValue(ref num);
      else if (missionTypeAttribute2.ValueType == QuestMissionValueType.ValueIsKeyAndIntValue)
        this.TryGetKeyValue(ref empty, ref num);
      switch (missionTypeAttribute1.ProgressJudgeType)
      {
        case QuestMissionProgressJudgeType.GreatorEqual:
          return missions_val >= num;
        case QuestMissionProgressJudgeType.Greator:
          return missions_val > num;
        case QuestMissionProgressJudgeType.LessEqual:
          return missions_val <= num;
        case QuestMissionProgressJudgeType.Less:
          return missions_val < num;
        case QuestMissionProgressJudgeType.Equal:
          return missions_val == num;
        case QuestMissionProgressJudgeType.EqualZero:
          return missions_val == 0;
        default:
          return false;
      }
    }

    public bool TryGetIntValue(ref int value)
    {
      return int.TryParse(this.TypeParam, out value);
    }

    public bool TryGetKeyValue(ref string key, ref int value)
    {
      if (string.IsNullOrEmpty(this.TypeParam))
      {
        DebugUtility.LogError("条件が設定されていません。");
        return false;
      }
      string[] strArray = this.TypeParam.Replace(" ", string.Empty).Split(',');
      if (strArray.Length < 2)
        return false;
      key = strArray[0].Trim();
      return int.TryParse(strArray[1].Trim(), out value);
    }

    public bool TryGetArray(ref string[] values)
    {
      if (string.IsNullOrEmpty(this.TypeParam))
      {
        DebugUtility.LogError("条件が設定されていません。");
        return false;
      }
      string str = this.TypeParam.Replace(" ", string.Empty);
      values = str.Split(',');
      return true;
    }

    private QuestMissionTypeAttribute GetQuestMissionTypeAttribute(EMissionType missionType)
    {
      QuestMissionTypeAttribute missionTypeAttribute = (QuestMissionTypeAttribute) null;
      if (!QuestBonusObjective.m_QuestMissionTypeDic.TryGetValue(missionType, out missionTypeAttribute))
      {
        missionTypeAttribute = GameUtility.GetCustomAttribute<QuestMissionTypeAttribute>((object) missionType, false);
        QuestBonusObjective.m_QuestMissionTypeDic.Add(missionType, missionTypeAttribute);
      }
      return missionTypeAttribute;
    }

    private TowerQuestMissionTypeAttribute GetTowerQuestMissionTypeAttribute(EMissionType missionType)
    {
      TowerQuestMissionTypeAttribute missionTypeAttribute = (TowerQuestMissionTypeAttribute) null;
      if (!QuestBonusObjective.m_TowerQuestMissionTypeDic.TryGetValue(missionType, out missionTypeAttribute))
      {
        missionTypeAttribute = GameUtility.GetCustomAttribute<TowerQuestMissionTypeAttribute>((object) missionType, false);
        QuestBonusObjective.m_TowerQuestMissionTypeDic.Add(missionType, missionTypeAttribute);
      }
      return missionTypeAttribute;
    }
  }
}
