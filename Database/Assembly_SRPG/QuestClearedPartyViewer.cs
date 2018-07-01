// Decompiled with JetBrains decompiler
// Type: SRPG.QuestClearedPartyViewer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class QuestClearedPartyViewer : MonoBehaviour
  {
    [SerializeField]
    private GenericSlot[] UnitSlots;
    [SerializeField]
    private GenericSlot[] SubUnitSlots;
    [SerializeField]
    private GenericSlot GuestUnitSlot;
    [SerializeField]
    private GenericSlot FriendSlot;
    [SerializeField]
    private GameObject[] StoryNormalObjects;
    [SerializeField]
    private GameObject[] HeloOnlyObjects;
    [SerializeField]
    private GenericSlot[] ItemSlots;
    [SerializeField]
    public Text TotalAtk;
    [SerializeField]
    private GenericSlot LeaderSkill;
    [SerializeField]
    private GenericSlot SupportSkill;
    [SerializeField]
    private GameObject[] ConditionItems;
    [SerializeField]
    private GameObject[] ConditionStars;
    [SerializeField]
    private GameObject[] EnableUploadObjects;
    private UnitData mGuestUnit;
    private QuestParam mCurrentQuest;
    private bool mIsHeloOnly;
    private UnitData[] mCurrentParty;
    private SupportData mCurrentSupport;
    private UnitData[] mUserSelectionParty;
    private int[] mUserSelectionAchievement;
    private ItemData[] mUsedItems;
    private SupportData mSupportData;
    private bool mIsUserOwnUnits;

    public QuestClearedPartyViewer()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      this.mIsUserOwnUnits = GlobalVars.UserSelectionPartyDataInfo == null;
      if (!this.mIsUserOwnUnits)
      {
        this.mUserSelectionParty = GlobalVars.UserSelectionPartyDataInfo.unitData;
        this.mSupportData = GlobalVars.UserSelectionPartyDataInfo.supportData;
        this.mUserSelectionAchievement = GlobalVars.UserSelectionPartyDataInfo.achievements;
        this.mUsedItems = GlobalVars.UserSelectionPartyDataInfo.usedItems;
      }
      foreach (GameObject enableUploadObject in this.EnableUploadObjects)
        enableUploadObject.SetActive(this.mIsUserOwnUnits);
      this.LoadParty();
      this.UpdateLeaderSkills();
      this.CalcTotalAttack();
      this.LoadInventory();
      this.LoadConditions();
    }

    private void OnDestroy()
    {
      GlobalVars.UserSelectionPartyDataInfo = (GlobalVars.UserSelectionPartyData) null;
    }

    private void LoadParty()
    {
      this.mCurrentQuest = MonoSingleton<GameManager>.Instance.FindQuest(GlobalVars.SelectedQuestID);
      this.mIsHeloOnly = PartyUtility.IsSoloStoryParty(this.mCurrentQuest);
      if (this.StoryNormalObjects != null)
      {
        for (int index = 0; index < this.StoryNormalObjects.Length; ++index)
        {
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.StoryNormalObjects[index], (UnityEngine.Object) null))
            this.StoryNormalObjects[index].SetActive(!this.mIsHeloOnly);
        }
      }
      if (this.HeloOnlyObjects != null)
      {
        for (int index = 0; index < this.HeloOnlyObjects.Length; ++index)
        {
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.HeloOnlyObjects[index], (UnityEngine.Object) null))
            this.HeloOnlyObjects[index].SetActive(this.mIsHeloOnly);
        }
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.GuestUnitSlot, (UnityEngine.Object) null))
      {
        if (this.mIsHeloOnly)
          ((Component) this.GuestUnitSlot).get_transform().SetSiblingIndex(0);
        else
          ((Component) this.GuestUnitSlot).get_transform().SetSiblingIndex(4);
      }
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      PartyWindow2.EditPartyTypes type = PartyUtility.GetEditPartyTypes(this.mCurrentQuest);
      if (type == PartyWindow2.EditPartyTypes.Auto)
        type = PartyWindow2.EditPartyTypes.Normal;
      PartyData partyOfType = MonoSingleton<GameManager>.Instance.Player.FindPartyOfType(type.ToPlayerPartyType());
      this.mCurrentParty = new UnitData[partyOfType.MAX_UNIT];
      UnitData data = (UnitData) null;
      string iname = this.mCurrentQuest.units.Get(0);
      if (this.mIsUserOwnUnits)
      {
        data = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUnitID(iname);
        for (int index = 0; index < partyOfType.MAX_UNIT; ++index)
        {
          long unitUniqueId = partyOfType.GetUnitUniqueID(index);
          if (unitUniqueId > 0L)
            this.mCurrentParty[index] = player.FindUnitDataByUniqueID(unitUniqueId);
        }
      }
      else
      {
        for (int index = 0; index < partyOfType.MAX_UNIT && index < this.mUserSelectionParty.Length; ++index)
        {
          if (this.mUserSelectionParty[index] == null)
            this.mCurrentParty[index] = (UnitData) null;
          if (this.mUserSelectionParty[index] != null && this.mUserSelectionParty[index].UnitParam.iname == iname)
            data = this.mUserSelectionParty[index];
          else
            this.mCurrentParty[index] = this.mUserSelectionParty[index];
        }
      }
      int count = this.mCurrentParty.Length - 2;
      UnitData[] array1 = ((IEnumerable<UnitData>) this.mCurrentParty).Skip<UnitData>(count).Take<UnitData>(2).ToArray<UnitData>();
      UnitData[] array2 = ((IEnumerable<UnitData>) this.mCurrentParty).Take<UnitData>(count).ToArray<UnitData>();
      for (int index = 0; index < this.SubUnitSlots.Length && index < array1.Length; ++index)
      {
        this.SubUnitSlots[index].SetSlotData<QuestParam>(this.mCurrentQuest);
        this.SubUnitSlots[index].SetSlotData<UnitData>(array1[index]);
      }
      for (int index = 0; index < this.UnitSlots.Length && index < array2.Length; ++index)
      {
        this.UnitSlots[index].SetSlotData<QuestParam>(this.mCurrentQuest);
        this.UnitSlots[index].SetSlotData<UnitData>(array2[index]);
      }
      if (data != null)
      {
        this.mGuestUnit = data;
        this.GuestUnitSlot.SetSlotData<QuestParam>(this.mCurrentQuest);
        this.GuestUnitSlot.SetSlotData<UnitData>(data);
        ((Component) this.GuestUnitSlot).get_gameObject().SetActive(true);
      }
      else
        ((Component) this.GuestUnitSlot).get_gameObject().SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.FriendSlot, (UnityEngine.Object) null))
      {
        if (this.mCurrentQuest.type == QuestTypes.Tower)
        {
          ((Component) this.FriendSlot).get_gameObject().SetActive(false);
        }
        else
        {
          ((Component) this.FriendSlot).get_gameObject().SetActive(true);
          if (this.mIsUserOwnUnits)
          {
            this.mCurrentSupport = (SupportData) GlobalVars.SelectedSupport;
            this.FriendSlot.SetSlotData<UnitData>(this.mCurrentSupport != null ? this.mCurrentSupport.Unit : (UnitData) null);
          }
          else if (this.mSupportData != null)
            this.FriendSlot.SetSlotData<UnitData>(this.mSupportData.Unit);
        }
      }
      int num;
      switch (type)
      {
        case PartyWindow2.EditPartyTypes.Normal:
        case PartyWindow2.EditPartyTypes.Arena:
        case PartyWindow2.EditPartyTypes.ArenaDef:
        case PartyWindow2.EditPartyTypes.Character:
          num = 3;
          break;
        case PartyWindow2.EditPartyTypes.Tower:
          num = 5;
          break;
        default:
          if ((this.mCurrentQuest == null ? 0 : (this.mCurrentQuest.UseFixEditor ? 1 : 0)) == 0)
          {
            num = 4;
            break;
          }
          goto case PartyWindow2.EditPartyTypes.Normal;
      }
      for (int index = 0; index < this.UnitSlots.Length; ++index)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UnitSlots[index], (UnityEngine.Object) null))
          ((Component) this.UnitSlots[index]).get_gameObject().SetActive(index < num);
      }
    }

    private void LoadInventory()
    {
      if (this.mIsUserOwnUnits)
      {
        int index = 0;
        using (Dictionary<OString, OInt>.Enumerator enumerator = SceneBattle.Instance.Battle.GetQuestRecord().used_items.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            KeyValuePair<OString, OInt> current = enumerator.Current;
            if (index >= this.ItemSlots.Length)
              break;
            string key = (string) current.Key;
            int num = (int) current.Value;
            ItemData data = new ItemData();
            data.Setup(0L, key, num);
            this.ItemSlots[index].SetSlotData<ItemData>(data);
            ++index;
          }
        }
      }
      else
      {
        if (this.mUsedItems == null)
          return;
        for (int index = 0; index < this.ItemSlots.Length && index < this.mUsedItems.Length; ++index)
          this.ItemSlots[index].SetSlotData<ItemData>(this.mUsedItems[index]);
      }
    }

    private void CalcTotalAttack()
    {
      int num = 0;
      for (int index = 0; index < this.mCurrentParty.Length; ++index)
      {
        UnitData unitData = this.mCurrentParty[index];
        if (unitData != null)
          num = num + (int) unitData.Status.param.atk + (int) unitData.Status.param.mag;
      }
      if (this.mCurrentSupport != null && this.mCurrentSupport.Unit != null)
        num = num + (int) this.mCurrentSupport.Unit.Status.param.atk + (int) this.mCurrentSupport.Unit.Status.param.mag;
      if (this.mGuestUnit != null)
        num = num + (int) this.mGuestUnit.Status.param.atk + (int) this.mGuestUnit.Status.param.mag;
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TotalAtk, (UnityEngine.Object) null))
        return;
      this.TotalAtk.set_text(num.ToString());
    }

    private void UpdateLeaderSkills()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.LeaderSkill, (UnityEngine.Object) null))
      {
        SkillParam data = (SkillParam) null;
        if (this.mIsHeloOnly)
        {
          if (this.mGuestUnit != null && this.mGuestUnit.LeaderSkill != null)
            data = this.mGuestUnit.LeaderSkill.SkillParam;
        }
        else if (this.mCurrentParty[0] != null)
        {
          if (this.mCurrentParty[0].LeaderSkill != null)
            data = this.mCurrentParty[0].LeaderSkill.SkillParam;
        }
        else if (this.mGuestUnit != null && this.mGuestUnit.LeaderSkill != null)
          data = this.mGuestUnit.LeaderSkill.SkillParam;
        this.LeaderSkill.SetSlotData<SkillParam>(data);
      }
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.SupportSkill, (UnityEngine.Object) null))
        return;
      SupportData supportData = !this.mIsUserOwnUnits ? this.mSupportData : MonoSingleton<GameManager>.Instance.Player.Supports.Find((Predicate<SupportData>) (f => f.FUID == GlobalVars.SelectedFriendID));
      SkillParam data1 = (SkillParam) null;
      if (supportData != null && supportData.Unit.LeaderSkill != null)
        data1 = supportData.Unit.LeaderSkill.SkillParam;
      this.SupportSkill.SetSlotData<SkillParam>(data1);
    }

    private void LoadConditions()
    {
      if (this.ConditionItems == null || this.ConditionStars == null)
        return;
      if (this.mIsUserOwnUnits)
        this.LoadConditionStarsFromBattle();
      else
        this.LoadConditionStarsFromUserSelection();
      if (this.mCurrentQuest.type != QuestTypes.Tower || this.ConditionItems.Length < 3)
        return;
      this.ConditionItems[2].SetActive(false);
    }

    private void LoadConditionStarsFromBattle()
    {
      SceneBattle instance = SceneBattle.Instance;
      for (int index1 = 0; index1 < this.ConditionStars.Length; ++index1)
      {
        bool flag = false;
        switch (index1)
        {
          case 0:
            flag = !instance.Battle.PlayByManually;
            break;
          case 1:
            flag = instance.Battle.IsAllAlive();
            break;
          case 2:
            BattleCore.Record questRecord = instance.Battle.GetQuestRecord();
            flag = true;
            for (int index2 = 0; index2 < questRecord.bonusCount; ++index2)
            {
              if ((questRecord.allBonusFlags & 1 << index2) == 0)
              {
                flag = false;
                break;
              }
            }
            break;
        }
        this.ConditionStars[index1].SetActive(flag);
      }
    }

    private void LoadConditionStarsFromUserSelection()
    {
      for (int index = 0; index < this.ConditionStars.Length && index < this.mUserSelectionAchievement.Length; ++index)
      {
        bool flag = this.mUserSelectionAchievement[index] != 0;
        this.ConditionStars[index].SetActive(flag);
      }
    }
  }
}
