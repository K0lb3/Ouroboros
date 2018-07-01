// Decompiled with JetBrains decompiler
// Type: SRPG.CharacterQuestData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

namespace SRPG
{
  public class CharacterQuestData
  {
    private CharacterQuestData.EStatus status;
    public CharacterQuestData.EType questType;
    public QuestParam questParam;
    public UnitData unitData1;
    public UnitData unitData2;
    public UnitParam unitParam1;
    public UnitParam unitParam2;

    public bool HasUnit
    {
      get
      {
        return this.unitData1 != null;
      }
    }

    public bool HasPairUnit
    {
      get
      {
        if (this.unitData1 != null)
          return this.unitData2 != null;
        return false;
      }
    }

    public CharacterQuestData.EStatus Status
    {
      get
      {
        return this.status;
      }
    }

    public bool IsLock
    {
      get
      {
        return this.status == CharacterQuestData.EStatus.Lock;
      }
    }

    public bool IsChallenged
    {
      get
      {
        return this.status == CharacterQuestData.EStatus.Challenged;
      }
    }

    public bool IsComplete
    {
      get
      {
        return this.status == CharacterQuestData.EStatus.Complete;
      }
    }

    public bool IsNew
    {
      get
      {
        return this.status == CharacterQuestData.EStatus.New;
      }
    }

    public CollaboSkillParam.Pair GetPairUnit()
    {
      if (this.unitData1 == null || this.unitData2 == null)
        return (CollaboSkillParam.Pair) null;
      return new CollaboSkillParam.Pair(this.unitData1.UnitParam, this.unitData2.UnitParam);
    }

    public void UpdateStatus()
    {
      if (this.questType == CharacterQuestData.EType.Chara)
      {
        if (this.unitData1 != null)
        {
          if (this.unitData1.IsOpenCharacterQuest())
          {
            List<KeyValuePair<QuestParam, bool>> characterQuests = CharacterQuestList.GetCharacterQuests(this.unitData1);
            int count = characterQuests.FindAll((Predicate<KeyValuePair<QuestParam, bool>>) (pair => pair.Key.state == QuestStates.Cleared)).Count;
            if (characterQuests.FindAll((Predicate<KeyValuePair<QuestParam, bool>>) (pair =>
            {
              if (pair.Key.state == QuestStates.New)
                return pair.Value;
              return false;
            })).Count > 0)
              this.status = CharacterQuestData.EStatus.New;
            else if (count == characterQuests.Count)
              this.status = CharacterQuestData.EStatus.Complete;
            else
              this.status = CharacterQuestData.EStatus.Challenged;
          }
          else
            this.status = CharacterQuestData.EStatus.Lock;
        }
        else
          this.status = CharacterQuestData.EStatus.Lock;
      }
      else if (this.unitData1 != null && this.unitData2 != null)
      {
        List<KeyValuePair<QuestParam, bool>> collaboSkillQuests = CharacterQuestList.GetCollaboSkillQuests(this.unitData1, this.unitData2);
        int count = collaboSkillQuests.FindAll((Predicate<KeyValuePair<QuestParam, bool>>) (pair => pair.Key.state == QuestStates.Cleared)).Count;
        if (collaboSkillQuests.FindAll((Predicate<KeyValuePair<QuestParam, bool>>) (pair =>
        {
          if (pair.Key.state == QuestStates.New)
            return pair.Value;
          return false;
        })).Count > 0)
          this.status = CharacterQuestData.EStatus.New;
        else if (count == collaboSkillQuests.Count)
          this.status = CharacterQuestData.EStatus.Complete;
        else
          this.status = CharacterQuestData.EStatus.Challenged;
      }
      else
        this.status = CharacterQuestData.EStatus.Lock;
    }

    public enum EType
    {
      Chara,
      Collabo,
    }

    public enum EStatus
    {
      New,
      Challenged,
      Lock,
      Complete,
    }
  }
}
