// Decompiled with JetBrains decompiler
// Type: SRPG.RecentPartyPanel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class RecentPartyPanel : MonoBehaviour
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
    private SRPG_Button[] allUnitButtons;
    [SerializeField]
    private GameObject[] StoryNormalObjects;
    [SerializeField]
    private GameObject[] HeloOnlyObjects;
    private UnitData[] mCurrentParty;
    private QuestParam mCurrentQuest;
    private bool mIsHeloOnly;
    public Text UserName;
    public Text Level;
    public Text ClearDate;
    public GameObject[] ConditionItems;
    public GameObject[] ConditionStars;

    public RecentPartyPanel()
    {
      base.\u002Ector();
    }

    public void SetUnitIconPressedCallback(SRPG_Button.ButtonClickEvent callback)
    {
      if (this.allUnitButtons == null)
        return;
      foreach (SRPG_Button allUnitButton in this.allUnitButtons)
        allUnitButton.AddListener(callback);
    }

    public void SetPartyInfo(UnitData[] party, SupportData supportData, QuestParam questParam)
    {
      this.mCurrentQuest = MonoSingleton<GameManager>.Instance.FindQuest(GlobalVars.SelectedQuestID);
      this.mIsHeloOnly = PartyUtility.IsSoloStoryParty(this.mCurrentQuest);
      if (this.StoryNormalObjects != null)
      {
        for (int index = 0; index < this.StoryNormalObjects.Length; ++index)
        {
          if (Object.op_Inequality((Object) this.StoryNormalObjects[index], (Object) null))
            this.StoryNormalObjects[index].SetActive(!this.mIsHeloOnly);
        }
      }
      if (this.HeloOnlyObjects != null)
      {
        for (int index = 0; index < this.HeloOnlyObjects.Length; ++index)
        {
          if (Object.op_Inequality((Object) this.HeloOnlyObjects[index], (Object) null))
            this.HeloOnlyObjects[index].SetActive(this.mIsHeloOnly);
        }
      }
      if (Object.op_Inequality((Object) this.GuestUnitSlot, (Object) null))
      {
        if (this.mIsHeloOnly)
          ((Component) this.GuestUnitSlot).get_transform().SetSiblingIndex(0);
        else
          ((Component) this.GuestUnitSlot).get_transform().SetSiblingIndex(4);
      }
      PartyWindow2.EditPartyTypes type = PartyUtility.GetEditPartyTypes(this.mCurrentQuest);
      if (type == PartyWindow2.EditPartyTypes.Auto)
        type = PartyWindow2.EditPartyTypes.Normal;
      PartyData partyOfType = MonoSingleton<GameManager>.Instance.Player.FindPartyOfType(type.ToPlayerPartyType());
      this.mCurrentParty = new UnitData[partyOfType.MAX_UNIT];
      UnitData data = (UnitData) null;
      string str = this.mCurrentQuest.units.Get(0);
      for (int index = 0; index < partyOfType.MAX_UNIT && index < party.Length; ++index)
      {
        if (party[index] == null)
          this.mCurrentParty[index] = (UnitData) null;
        if (party[index] != null && party[index].UnitParam.iname == str)
          data = party[index];
        else
          this.mCurrentParty[index] = party[index];
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
        this.GuestUnitSlot.SetSlotData<QuestParam>(this.mCurrentQuest);
        this.GuestUnitSlot.SetSlotData<UnitData>(data);
        ((Component) this.GuestUnitSlot).get_gameObject().SetActive(true);
      }
      else
        ((Component) this.GuestUnitSlot).get_gameObject().SetActive(false);
      if (Object.op_Inequality((Object) this.FriendSlot, (Object) null))
      {
        if (this.mCurrentQuest.type == QuestTypes.Tower)
        {
          ((Component) this.FriendSlot).get_gameObject().SetActive(false);
        }
        else
        {
          ((Component) this.FriendSlot).get_gameObject().SetActive(true);
          if (supportData != null)
            this.FriendSlot.SetSlotData<UnitData>(supportData.Unit);
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
        if (Object.op_Inequality((Object) this.UnitSlots[index], (Object) null))
          ((Component) this.UnitSlots[index]).get_gameObject().SetActive(index < num);
      }
    }
  }
}
