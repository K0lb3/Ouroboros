// Decompiled with JetBrains decompiler
// Type: SRPG.QuestCampaignChildParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

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
    public QuestCampaignTrust campaignTrust;

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
      this.campaignTrust = (QuestCampaignTrust) null;
      return true;
    }

    public QuestCampaignData[] MakeData()
    {
      if (this.scope == QuestCampaignScopes.Unit || this.scope == QuestCampaignScopes.UnitAndQuest)
        return new QuestCampaignData[1]
        {
          new QuestCampaignData()
          {
            type = QuestCampaignValueTypes.ExpUnit,
            unit = this.unit,
            value = this.expUnit
          }
        };
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
      if (this.campaignTrust != null)
      {
        if (this.campaignTrust.concept_card != null)
          ++length;
        if (this.campaignTrust.card_trust_lottery_rate > 0)
          ++length;
        if (this.campaignTrust.card_trust_qe_bonus > 0)
          ++length;
      }
      QuestCampaignData[] questCampaignDataArray = new QuestCampaignData[length];
      int index = length - 1;
      if (this.apRate != 100)
      {
        questCampaignDataArray[index] = new QuestCampaignData()
        {
          type = QuestCampaignValueTypes.Ap,
          value = this.apRate
        };
        --index;
      }
      if (this.expUnit != 100)
      {
        questCampaignDataArray[index] = new QuestCampaignData()
        {
          type = QuestCampaignValueTypes.ExpUnit,
          value = this.expUnit
        };
        --index;
      }
      if (this.expPlayer != 100)
      {
        questCampaignDataArray[index] = new QuestCampaignData()
        {
          type = QuestCampaignValueTypes.ExpPlayer,
          value = this.expPlayer
        };
        --index;
      }
      if (this.dropNum != 100)
      {
        questCampaignDataArray[index] = new QuestCampaignData()
        {
          type = QuestCampaignValueTypes.DropNum,
          value = this.dropNum
        };
        --index;
      }
      if (this.dropRate != 100)
      {
        questCampaignDataArray[index] = new QuestCampaignData()
        {
          type = QuestCampaignValueTypes.DropRate,
          value = this.dropRate
        };
        --index;
      }
      if (this.campaignTrust != null)
      {
        if (this.campaignTrust.concept_card != null)
        {
          questCampaignDataArray[index] = new QuestCampaignData()
          {
            type = QuestCampaignValueTypes.TrustSpecific,
            value = 0
          };
          --index;
        }
        if (this.campaignTrust.card_trust_lottery_rate > 0)
        {
          questCampaignDataArray[index] = new QuestCampaignData()
          {
            type = QuestCampaignValueTypes.TrustIncidence,
            value = this.campaignTrust.card_trust_lottery_rate
          };
          --index;
        }
        if (this.campaignTrust.card_trust_qe_bonus > 0)
        {
          questCampaignDataArray[index] = new QuestCampaignData()
          {
            type = QuestCampaignValueTypes.TrustUp,
            value = this.campaignTrust.card_trust_qe_bonus
          };
          int num = index - 1;
        }
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
        if (parent.IsAvailablePeriod(now) && parent.endAt.CompareTo(DateTime.MaxValue) < 0)
          return true;
      }
      return false;
    }
  }
}
