// Decompiled with JetBrains decompiler
// Type: SRPG.RecentPartyPanel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class RecentPartyPanel : MonoBehaviour
  {
    [SerializeField]
    private GenericSlot UnitSlotTemplate;
    [SerializeField]
    private GenericSlot NpcSlotTemplate;
    [SerializeField]
    private Transform MembersHolder;
    [SerializeField]
    private GameObject[] ConditionItems;
    [SerializeField]
    private GameObject[] ConditionStars;
    [SerializeField]
    private Text UserName;
    [SerializeField]
    private Text Level;
    [SerializeField]
    private Text ClearDate;
    private QuestParam mCurrentQuest;
    private UnitData[] mCurrentParty;
    private SupportData mCurrentSupport;
    private List<UnitData> mGuestUnits;
    private GenericSlot[] UnitSlots;
    private GenericSlot[] SubUnitSlots;
    private GenericSlot GuestUnitSlot;
    private GenericSlot FriendSlot;
    private List<PartySlotData> mSlotData;
    private List<SRPG_Button> allUnitButtons;

    public RecentPartyPanel()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      GameUtility.SetGameObjectActive((Component) this.UnitSlotTemplate, false);
      GameUtility.SetGameObjectActive((Component) this.NpcSlotTemplate, false);
    }

    public void SetUnitIconPressedCallback(SRPG_Button.ButtonClickEvent callback)
    {
      if (this.allUnitButtons == null)
        return;
      using (List<SRPG_Button>.Enumerator enumerator = this.allUnitButtons.GetEnumerator())
      {
        while (enumerator.MoveNext())
          enumerator.Current.AddListener(callback);
      }
    }

    public void SetPartyInfo(UnitData[] party, SupportData supportData, QuestParam questParam)
    {
      this.mCurrentQuest = MonoSingleton<GameManager>.Instance.FindQuest(GlobalVars.SelectedQuestID);
      this.mCurrentParty = party;
      this.mCurrentSupport = supportData;
      this.CreateSlots();
    }

    public void SetUserName(string value)
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UserName, (UnityEngine.Object) null))
        return;
      this.UserName.set_text(value);
    }

    public void SetUserRank(string value)
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Level, (UnityEngine.Object) null))
        return;
      this.Level.set_text(value);
    }

    public void SetClearDate(string value)
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ClearDate, (UnityEngine.Object) null))
        return;
      this.ClearDate.set_text(value);
    }

    public void SetConditionStarActive(int index, bool active)
    {
      if (index >= this.ConditionStars.Length)
        return;
      this.ConditionStars[index].SetActive(active);
    }

    public void SetConditionItemActive(int index, bool active)
    {
      if (index >= this.ConditionItems.Length)
        return;
      this.ConditionItems[index].SetActive(active);
    }

    private GenericSlot CreateSlotObject(PartySlotData slotData, GenericSlot template, Transform parent)
    {
      GenericSlot component = (GenericSlot) ((GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) ((Component) template).get_gameObject())).GetComponent<GenericSlot>();
      ((Component) component).get_transform().SetParent(parent, false);
      ((Component) component).get_gameObject().SetActive(true);
      component.SetSlotData<PartySlotData>(slotData);
      return component;
    }

    private void DestroyPartySlotObjects()
    {
      if (this.UnitSlots != null)
        GameUtility.DestroyGameObjects<GenericSlot>(this.UnitSlots);
      GameUtility.DestroyGameObject((Component) this.FriendSlot);
      this.mSlotData.Clear();
    }

    private void CreateSlots()
    {
      this.DestroyPartySlotObjects();
      List<PartySlotData> mainSlotData = new List<PartySlotData>();
      List<PartySlotData> subSlotData = new List<PartySlotData>();
      PartySlotData supportSlotData = (PartySlotData) null;
      PartyUtility.CreatePartySlotData(this.mCurrentQuest, out mainSlotData, out subSlotData, out supportSlotData);
      List<GenericSlot> genericSlotList = new List<GenericSlot>();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.MembersHolder, (UnityEngine.Object) null) && mainSlotData.Count > 0)
      {
        using (List<PartySlotData>.Enumerator enumerator = mainSlotData.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            PartySlotData current = enumerator.Current;
            GenericSlot genericSlot = current.Type == PartySlotType.Npc || current.Type == PartySlotType.NpcHero ? this.CreateSlotObject(current, this.NpcSlotTemplate, this.MembersHolder) : this.CreateSlotObject(current, this.UnitSlotTemplate, this.MembersHolder);
            genericSlotList.Add(genericSlot);
          }
        }
        if (supportSlotData != null)
          this.FriendSlot = this.CreateSlotObject(supportSlotData, this.UnitSlotTemplate, this.MembersHolder);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.MembersHolder, (UnityEngine.Object) null) && subSlotData.Count > 0)
      {
        using (List<PartySlotData>.Enumerator enumerator = subSlotData.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            PartySlotData current = enumerator.Current;
            GenericSlot genericSlot = current.Type == PartySlotType.Npc || current.Type == PartySlotType.NpcHero ? this.CreateSlotObject(current, this.NpcSlotTemplate, this.MembersHolder) : this.CreateSlotObject(current, this.UnitSlotTemplate, this.MembersHolder);
            genericSlotList.Add(genericSlot);
          }
        }
      }
      this.mSlotData.AddRange((IEnumerable<PartySlotData>) mainSlotData);
      this.mSlotData.AddRange((IEnumerable<PartySlotData>) subSlotData);
      this.UnitSlots = genericSlotList.ToArray();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.FriendSlot, (UnityEngine.Object) null) && this.mCurrentQuest != null && this.mCurrentQuest.type == QuestTypes.Tower)
      {
        TowerFloorParam towerFloor = MonoSingleton<GameManager>.Instance.FindTowerFloor(this.mCurrentQuest.iname);
        if (towerFloor != null)
          ((Component) this.FriendSlot).get_gameObject().SetActive(towerFloor.can_help);
      }
      PartyUtility.MergePartySlotWithPartyUnits(this.mCurrentQuest, this.mSlotData, this.mCurrentParty, this.mGuestUnits, false);
      this.ReflectionUnitSlot();
      this.allUnitButtons = ((IEnumerable<GenericSlot>) this.UnitSlots).Select<GenericSlot, SRPG_Button>((Func<GenericSlot, SRPG_Button>) (slot => (SRPG_Button) ((Component) slot).GetComponent<SRPG_Button>())).Where<SRPG_Button>((Func<SRPG_Button, bool>) (button => UnityEngine.Object.op_Inequality((UnityEngine.Object) button, (UnityEngine.Object) null))).ToList<SRPG_Button>();
      this.allUnitButtons.Add((SRPG_Button) ((Component) this.FriendSlot).GetComponent<SRPG_Button>());
    }

    private void ReflectionUnitSlot()
    {
      int index1 = 0;
      for (int index2 = 0; index2 < this.UnitSlots.Length && index2 < this.mCurrentParty.Length && index2 < this.mSlotData.Count; ++index2)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UnitSlots[index2], (UnityEngine.Object) null))
        {
          this.UnitSlots[index2].SetSlotData<QuestParam>(this.mCurrentQuest);
          PartySlotData partySlotData = this.mSlotData[index2];
          if (partySlotData.Type == PartySlotType.ForcedHero)
          {
            if (this.mGuestUnits != null && index1 < this.mGuestUnits.Count)
              this.UnitSlots[index2].SetSlotData<UnitData>(this.mGuestUnits[index1]);
            ++index1;
          }
          else if (partySlotData.Type == PartySlotType.Npc || partySlotData.Type == PartySlotType.NpcHero)
          {
            UnitParam unitParam = MonoSingleton<GameManager>.Instance.MasterParam.GetUnitParam(partySlotData.UnitName);
            this.UnitSlots[index2].SetSlotData<UnitParam>(unitParam);
          }
          else
            this.UnitSlots[index2].SetSlotData<UnitData>(this.mCurrentParty[index2]);
        }
      }
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.FriendSlot, (UnityEngine.Object) null))
        return;
      this.FriendSlot.SetSlotData<QuestParam>(this.mCurrentQuest);
      this.FriendSlot.SetSlotData<SupportData>(this.mCurrentSupport);
      if (this.mCurrentSupport == null)
        return;
      this.FriendSlot.SetSlotData<UnitData>(this.mCurrentSupport.Unit);
    }
  }
}
