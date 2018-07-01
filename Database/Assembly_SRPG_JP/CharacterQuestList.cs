// Decompiled with JetBrains decompiler
// Type: SRPG.CharacterQuestList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SRPG
{
  [FlowNode.Pin(0, "Refresh(Chara)", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Refresh(Collabo)", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(10, "Refreshed", FlowNode.PinTypes.Output, 11)]
  [FlowNode.Pin(11, "Selected(Chara)", FlowNode.PinTypes.Output, 12)]
  [FlowNode.Pin(12, "Selected(Collabo)", FlowNode.PinTypes.Output, 13)]
  [FlowNode.Pin(13, "Sort Change", FlowNode.PinTypes.Output, 14)]
  public class CharacterQuestList : SRPG_FixedList, IFlowInterface, ISortableList
  {
    private const int PIN_ID_REFRESH_CHARA = 0;
    private const int PIN_ID_REFRESH_COLLABO = 1;
    private const int PIN_ID_REFRESHED = 10;
    private const int PIN_ID_CHARA_SELECTED = 11;
    private const int PIN_ID_COLLABO_REFRESHED = 12;
    private const int PIN_ID_SORT_CHANGE = 13;
    [SerializeField]
    private GameObject ItemTemplate;
    [SerializeField]
    private GameObject FlowRoot;
    [SerializeField]
    private SortMenu mSortMenu;
    [SerializeField]
    private SortMenuButton mSortMenuButton;
    private CharacterQuestList.FilterMethod mFilterMethod;

    private static string FilterValue(CharacterQuestList.EFilter filter)
    {
      return ((int) filter).ToString();
    }

    protected override void Start()
    {
      base.Start();
      // ISSUE: method pointer
      this.RegisterNextButtonCallBack(new UnityAction((object) this, __methodptr(OnNextPage)));
      // ISSUE: method pointer
      this.RegisterPrevButtonCallBack(new UnityAction((object) this, __methodptr(OnPreviousPage)));
    }

    protected override void OnRectTransformDimensionsChange()
    {
      base.OnRectTransformDimensionsChange();
      this.CancelRefresh();
    }

    protected override void OnItemSelect(GameObject go)
    {
      CharacterQuestData dataOfClass = DataSource.FindDataOfClass<CharacterQuestData>(go, (CharacterQuestData) null);
      if (dataOfClass == null)
        return;
      if (dataOfClass.questType == CharacterQuestData.EType.Chara)
      {
        if (!dataOfClass.HasUnit || dataOfClass.IsLock)
          return;
        GlobalVars.SelectedUnitUniqueID.Set(dataOfClass.unitData1.UniqueID);
        GlobalVars.PreBattleUnitUniqueID.Set(dataOfClass.unitData1.UniqueID);
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 11);
      }
      else
      {
        if (!dataOfClass.HasPairUnit || dataOfClass.IsLock)
          return;
        GlobalVars.SelectedCollaboSkillPair = dataOfClass.GetPairUnit();
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 12);
      }
    }

    public static List<KeyValuePair<QuestParam, bool>> GetCharacterQuests(UnitData unitData)
    {
      List<KeyValuePair<QuestParam, bool>> keyValuePairList = new List<KeyValuePair<QuestParam, bool>>();
      List<QuestParam> condQuests = unitData.FindCondQuests();
      UnitData.CharacterQuestParam[] charaEpisodeList = unitData.GetCharaEpisodeList();
      for (int index = 0; index < condQuests.Count; ++index)
      {
        bool flag = condQuests[index].state == QuestStates.Cleared;
        if (charaEpisodeList[index] != null && charaEpisodeList[index].IsAvailable && unitData.IsChQuestParentUnlocked(condQuests[index]) || flag)
        {
          KeyValuePair<QuestParam, bool> keyValuePair = new KeyValuePair<QuestParam, bool>(condQuests[index], true);
          keyValuePairList.Add(keyValuePair);
        }
        else
        {
          KeyValuePair<QuestParam, bool> keyValuePair = new KeyValuePair<QuestParam, bool>(condQuests[index], false);
          keyValuePairList.Add(keyValuePair);
        }
      }
      return keyValuePairList;
    }

    public static List<KeyValuePair<QuestParam, bool>> GetCollaboSkillQuests(UnitData unitData1, UnitData unitData2)
    {
      List<KeyValuePair<QuestParam, bool>> keyValuePairList = new List<KeyValuePair<QuestParam, bool>>();
      List<QuestParam> questList = CollaboSkillQuestList.GetCollaboSkillQuests(unitData1, unitData2);
      QuestParam[] availableQuests = MonoSingleton<GameManager>.Instance.Player.AvailableQuests;
      for (int i = 0; i < questList.Count; ++i)
      {
        bool flag1 = questList[i].IsDateUnlock(-1L);
        bool flag2 = Array.Find<QuestParam>(availableQuests, (Predicate<QuestParam>) (p => p == questList[i])) != null;
        bool flag3 = questList[i].state == QuestStates.Cleared;
        string empty = string.Empty;
        if (questList[i].IsEntryQuestConditionCh(unitData1, ref empty) && (flag1 && flag2 && !flag3))
        {
          KeyValuePair<QuestParam, bool> keyValuePair = new KeyValuePair<QuestParam, bool>(questList[i], true);
          keyValuePairList.Add(keyValuePair);
        }
        else
        {
          KeyValuePair<QuestParam, bool> keyValuePair = new KeyValuePair<QuestParam, bool>(questList[i], false);
          keyValuePairList.Add(keyValuePair);
        }
      }
      return keyValuePairList;
    }

    protected override void RefreshItems()
    {
      base.RefreshItems();
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 10);
    }

    protected override GameObject CreateItem()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ItemTemplate, (UnityEngine.Object) null))
        return (GameObject) null;
      GameObject gameObject = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.ItemTemplate);
      gameObject.SetActive(true);
      return gameObject;
    }

    public void SetSortMethod(string method, bool ascending, string[] filters)
    {
      this.SetSortMethod(filters);
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 13);
    }

    public void SetSortMethod(string[] filters)
    {
      if (filters == null)
        return;
      for (int index = 0; index < filters.Length; ++index)
        this.mFilterMethod = !(filters[index] == CharacterQuestList.FilterValue(CharacterQuestList.EFilter.Unlock)) ? (!(filters[index] == CharacterQuestList.FilterValue(CharacterQuestList.EFilter.Lock)) ? (!(filters[index] == CharacterQuestList.FilterValue(CharacterQuestList.EFilter.Complete)) ? new CharacterQuestList.FilterMethod(this.OnFilter_ALL) : new CharacterQuestList.FilterMethod(this.OnFilter_Complete)) : new CharacterQuestList.FilterMethod(this.OnFilter_Lock)) : new CharacterQuestList.FilterMethod(this.OnFilter_Unlock);
    }

    private bool OnFilter_Unlock(CharacterQuestData questData)
    {
      if (questData == null)
        return false;
      if (questData.Status != CharacterQuestData.EStatus.New)
        return questData.Status == CharacterQuestData.EStatus.Challenged;
      return true;
    }

    private bool OnFilter_Lock(CharacterQuestData questData)
    {
      if (questData == null)
        return false;
      return questData.Status == CharacterQuestData.EStatus.Lock;
    }

    private bool OnFilter_Complete(CharacterQuestData questData)
    {
      if (questData == null)
        return false;
      return questData.Status == CharacterQuestData.EStatus.Complete;
    }

    private bool OnFilter_ALL(CharacterQuestData questData)
    {
      return questData != null;
    }

    public void Activated(int pinID)
    {
      if (pinID == 0)
      {
        this.RefreshFilter();
        this.RefreshCharaData();
        this.Refresh();
      }
      else
      {
        if (pinID != 1)
          return;
        this.RefreshFilter();
        this.RefreshCollaboData();
        this.Refresh();
      }
    }

    private List<CharacterQuestDataChunk> GetCharacterQuestList()
    {
      GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) instanceDirect, (UnityEngine.Object) null))
        return (List<CharacterQuestDataChunk>) null;
      QuestParam[] availableQuests = MonoSingleton<GameManager>.Instance.Player.AvailableQuests;
      List<CharacterQuestDataChunk> characterQuestDataChunkList = new List<CharacterQuestDataChunk>();
      foreach (QuestParam questParam in availableQuests)
      {
        QuestParam qp = questParam;
        if (qp.type == QuestTypes.Character)
        {
          if (string.IsNullOrEmpty(qp.ChapterID))
            DebugUtility.LogError("ChapterIDが設定されていません:QuestParam.iname = " + qp.iname);
          else if (instanceDirect.MasterParam.ContainsUnitID(qp.ChapterID))
          {
            CharacterQuestDataChunk characterQuestDataChunk = characterQuestDataChunkList.Find((Predicate<CharacterQuestDataChunk>) (cqdc => cqdc.areaName == qp.ChapterID));
            if (characterQuestDataChunk == null)
            {
              characterQuestDataChunk = new CharacterQuestDataChunk();
              characterQuestDataChunkList.Add(characterQuestDataChunk);
            }
            characterQuestDataChunk.questParams.Add(qp);
            characterQuestDataChunk.areaName = qp.ChapterID;
            characterQuestDataChunk.SetUnitNameFromChapterID(qp.ChapterID);
            characterQuestDataChunk.unitParam = instanceDirect.MasterParam.GetUnitParam(characterQuestDataChunk.unitName);
          }
        }
      }
      return characterQuestDataChunkList;
    }

    private void RefreshFilter()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mSortMenu, (UnityEngine.Object) null))
        return;
      this.mSortMenuButton.ForceReloadFilter();
      this.SetSortMethod(this.mSortMenu.GetFilters(false));
    }

    public void RefreshCharaData()
    {
      GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) instanceDirect, (UnityEngine.Object) null))
        return;
      List<CharacterQuestData> characterQuestDataList = new List<CharacterQuestData>();
      List<CharacterQuestDataChunk> characterQuestList = this.GetCharacterQuestList();
      characterQuestList.Reverse();
      for (int index = 0; index < characterQuestList.Count; ++index)
      {
        CharacterQuestDataChunk characterQuestDataChunk = characterQuestList[index];
        if (characterQuestDataChunk.unitParam != null)
        {
          CharacterQuestData characterQuestData = new CharacterQuestData();
          characterQuestData.questType = CharacterQuestData.EType.Chara;
          UnitData dataByUniqueParam = instanceDirect.Player.FindUnitDataByUniqueParam(characterQuestDataChunk.unitParam);
          if (dataByUniqueParam == null)
            characterQuestData.unitParam1 = characterQuestDataChunk.unitParam;
          else
            characterQuestData.unitData1 = dataByUniqueParam;
          characterQuestData.UpdateStatus();
          characterQuestDataList.Add(characterQuestData);
        }
      }
      if (this.mFilterMethod == null)
        Debug.Log((object) "mFilterMethod == null");
      else
        characterQuestDataList = characterQuestDataList.FindAll((Predicate<CharacterQuestData>) (quest => this.mFilterMethod(quest)));
      this.SetData((object[]) characterQuestDataList.ToArray(), typeof (CharacterQuestData));
    }

    public void RefreshCollaboData()
    {
      GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) instanceDirect, (UnityEngine.Object) null))
        return;
      List<CharacterQuestData> characterQuestDataList = new List<CharacterQuestData>();
      List<CollaboSkillParam.Pair> pairLists = CollaboSkillParam.GetPairLists();
      if (pairLists != null)
      {
        for (int index = 0; index < pairLists.Count; ++index)
        {
          UnitData dataByUniqueParam1 = instanceDirect.Player.FindUnitDataByUniqueParam(pairLists[index].UnitParam1);
          UnitData dataByUniqueParam2 = instanceDirect.Player.FindUnitDataByUniqueParam(pairLists[index].UnitParam2);
          CharacterQuestData characterQuestData = new CharacterQuestData();
          characterQuestData.questType = CharacterQuestData.EType.Collabo;
          if (dataByUniqueParam1 == null)
            characterQuestData.unitParam1 = pairLists[index].UnitParam1;
          else
            characterQuestData.unitData1 = dataByUniqueParam1;
          if (dataByUniqueParam2 == null)
            characterQuestData.unitParam2 = pairLists[index].UnitParam2;
          else
            characterQuestData.unitData2 = dataByUniqueParam2;
          characterQuestData.UpdateStatus();
          characterQuestDataList.Add(characterQuestData);
        }
      }
      if (this.mFilterMethod == null)
        Debug.Log((object) "mFilterMethod == null");
      else
        characterQuestDataList = characterQuestDataList.FindAll((Predicate<CharacterQuestData>) (quest => this.mFilterMethod(quest)));
      this.SetData((object[]) characterQuestDataList.ToArray(), typeof (CharacterQuestData));
    }

    public void OnPreviousPage()
    {
      if (!this.IsActive())
        return;
      this.GotoPreviousPage();
    }

    public void OnNextPage()
    {
      if (!this.IsActive())
        return;
      this.GotoNextPage();
    }

    public enum EFilter
    {
      ALL,
      Unlock,
      Lock,
      Complete,
    }

    private delegate bool FilterMethod(CharacterQuestData questData);
  }
}
