// Decompiled with JetBrains decompiler
// Type: SRPG.QuestCampaignChildParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;

namespace SRPG
{
  public class QuestCampaignChildParam
  {
    public string iname;
    public QuestCampaignScopes scope;
    public QuestTypes questType;
    public QuestDifficulties questMode;
    public string questId;
    public string unit;
    public int dropRate;
    public int dropNum;
    public int expPlayer;
    public int expUnit;
    public int apRate;
    public QuestCampaignParentParam[] parents;

    public bool Deserialize(JSON_QuestCampaignChildParam json)
    {
      this.iname = json.iname;
      this.scope = (QuestCampaignScopes) json.scope;
      this.questType = (QuestTypes) json.quest_type;
      this.questMode = (QuestDifficulties) json.quest_mode;
      this.questId = json.quest_id;
      this.unit = json.unit;
      this.dropRate = json.drop_rate;
      this.dropNum = json.drop_num;
      this.expPlayer = json.exp_player;
      this.expUnit = json.exp_unit;
      this.apRate = json.ap_rate;
      this.parents = new QuestCampaignParentParam[0];
      return true;
    }

    public QuestCampaignData[] MakeData()
    {
      QuestCampaignData questCampaignData = new QuestCampaignData();
      if (this.scope == QuestCampaignScopes.Unit)
      {
        questCampaignData.type = QuestCampaignValueTypes.ExpUnit;
        questCampaignData.unit = this.unit;
        questCampaignData.value = this.expUnit;
        return new QuestCampaignData[1]{ questCampaignData };
      }
      if (this.scope == QuestCampaignScopes.UnitAndQuest)
        questCampaignData.unit = this.unit;
      int length = 0;
      if (this.apRate != 100)
        ++length;
      if (this.expUnit != 100)
        ++length;
      if (this.expPlayer != 100)
        ++length;
      if (this.dropNum != 100)
        ++length;
      if (this.dropRate != 100)
        ++length;
      QuestCampaignData[] questCampaignDataArray = new QuestCampaignData[length];
      int index = length - 1;
      if (this.apRate != 100)
      {
        questCampaignData.type = QuestCampaignValueTypes.Ap;
        questCampaignData.value = this.apRate;
        questCampaignDataArray[index] = questCampaignData;
        --index;
      }
      if (this.expUnit != 100)
      {
        questCampaignData.type = QuestCampaignValueTypes.ExpUnit;
        questCampaignData.value = this.expUnit;
        questCampaignDataArray[index] = questCampaignData;
        --index;
      }
      if (this.expPlayer != 100)
      {
        questCampaignData.type = QuestCampaignValueTypes.ExpPlayer;
        questCampaignData.value = this.expPlayer;
        questCampaignDataArray[index] = questCampaignData;
        --index;
      }
      if (this.dropNum != 100)
      {
        questCampaignData.type = QuestCampaignValueTypes.DropNum;
        questCampaignData.value = this.dropNum;
        questCampaignDataArray[index] = questCampaignData;
        --index;
      }
      if (this.dropRate != 100)
      {
        questCampaignData.type = QuestCampaignValueTypes.DropRate;
        questCampaignData.value = this.dropRate;
        questCampaignDataArray[index] = questCampaignData;
        int num = index - 1;
      }
      return questCampaignDataArray;
    }

    public bool IsValidQuest(QuestParam questParam)
    {
      bool flag = false;
      switch (this.scope)
      {
        case QuestCampaignScopes.Quest:
          flag = this.questId == questParam.iname;
          break;
        case QuestCampaignScopes.QuestType:
          flag = this.questType == questParam.type && this.questMode == questParam.difficulty;
          break;
        case QuestCampaignScopes.Unit:
          flag = true;
          break;
        case QuestCampaignScopes.UnitAndQuest:
          flag = this.questId == questParam.iname;
          break;
      }
      return flag;
    }

    public bool IsAvailablePeriod(DateTime now)
    {
      foreach (QuestCampaignParentParam parent in this.parents)
      {
        if (parent.IsAvailablePeriod(now))
          return true;
      }
      return false;
    }
  }
}
