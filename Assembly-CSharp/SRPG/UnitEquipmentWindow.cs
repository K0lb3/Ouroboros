// Decompiled with JetBrains decompiler
// Type: SRPG.UnitEquipmentWindow
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(102, "アイテムが作成された", FlowNode.PinTypes.Output, 102)]
  [FlowNode.Pin(103, "アイテムが一括作成された", FlowNode.PinTypes.Output, 103)]
  [FlowNode.Pin(101, "入手クエストが選択された", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(100, "装備アイテムが装着された", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(2, "ウインドウの表示要素を再読み込み", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(1, "ウインドウを表示", FlowNode.PinTypes.Input, 1)]
  public class UnitEquipmentWindow : MonoBehaviour, IFlowInterface
  {
    public UnitEquipmentWindow.EquipEvent OnEquip;
    public GameObject SubWindow;
    public SRPG_Button EquipButton;
    public SRPG_Button ConfirmQuestButton;
    public SRPG_Button ConfirmRecipeButton;
    public SRPG_Button CreateButton;
    public SRPG_Button CreateButtonAll;
    public RectTransform RecipeParent;
    public RectTransform QuestsParent;
    public RectTransform SelectedItem;
    public RectTransform ItemTreeParent;
    public RectTransform ItemTreeArrow;
    public ListItemEvents ItemTreeTemplate;
    public RectTransform RecipeListParent;
    public RectTransform RecipeListLine;
    public ListItemEvents RecipeListItemTemplate;
    public RectTransform QuestListParent;
    public GameObject QuestListItemTemplate;
    public RectTransform EquipItemParamParent;
    public GameObject EquipItemParamTemplate;
    private EquipData mEquipmentData;
    private List<GameObject> mEquipmentParameters;
    private List<ItemParam> mItemParamTree;
    private List<GameObject> ItemTree;
    private List<GameObject> RecipeItems;
    private List<GameObject> GainedQuests;
    private bool mCreateItemSuccessed;

    public UnitEquipmentWindow()
    {
      base.\u002Ector();
    }

    public void Activated(int pinID)
    {
      if (pinID == 1)
      {
        if (Object.op_Inequality((Object) this.SubWindow, (Object) null))
          this.SubWindow.SetActive(false);
        this.Refresh();
      }
      if (pinID != 2)
        return;
      this.mCreateItemSuccessed = true;
      this.Refresh();
      this.mCreateItemSuccessed = false;
    }

    private void Start()
    {
      if (Object.op_Inequality((Object) this.SubWindow, (Object) null))
        this.SubWindow.SetActive(false);
      if (Object.op_Inequality((Object) this.EquipButton, (Object) null))
        this.EquipButton.AddListener(new SRPG_Button.ButtonClickEvent(this.OnEquipClick));
      if (Object.op_Inequality((Object) this.CreateButton, (Object) null))
        this.CreateButton.AddListener(new SRPG_Button.ButtonClickEvent(this.OnCreateClick));
      if (Object.op_Inequality((Object) this.CreateButtonAll, (Object) null))
        this.CreateButtonAll.AddListener(new SRPG_Button.ButtonClickEvent(this.OnCreateAllClick));
      if (Object.op_Inequality((Object) this.ConfirmQuestButton, (Object) null))
        this.ConfirmQuestButton.AddListener(new SRPG_Button.ButtonClickEvent(this.OnConfirmQuestClick));
      if (Object.op_Inequality((Object) this.ConfirmRecipeButton, (Object) null))
        this.ConfirmRecipeButton.AddListener(new SRPG_Button.ButtonClickEvent(this.OnConfirmRecipeClick));
      if (Object.op_Inequality((Object) this.ItemTreeTemplate, (Object) null))
        ((Component) this.ItemTreeTemplate).get_gameObject().SetActive(false);
      if (Object.op_Inequality((Object) this.RecipeListItemTemplate, (Object) null))
        ((Component) this.RecipeListItemTemplate).get_gameObject().SetActive(false);
      if (Object.op_Inequality((Object) this.QuestListItemTemplate, (Object) null))
        this.QuestListItemTemplate.SetActive(false);
      if (Object.op_Inequality((Object) this.EquipItemParamTemplate, (Object) null))
        this.EquipItemParamTemplate.SetActive(false);
      this.Refresh();
    }

    private int GetSelectedJobIndex(UnitData unit)
    {
      for (int index = 0; index < unit.JobCount; ++index)
      {
        if (unit.Jobs[index].UniqueID == (long) GlobalVars.SelectedJobUniqueID)
          return index;
      }
      return 0;
    }

    private void OnConfirmQuestClick(SRPG_Button button)
    {
      if (!((Selectable) button).IsInteractable())
        return;
      this.SubWindow.SetActive(true);
    }

    private void OnConfirmRecipeClick(SRPG_Button button)
    {
      if (!((Selectable) button).IsInteractable())
        return;
      this.SubWindow.SetActive(true);
    }

    private void OnEquipClick(SRPG_Button button)
    {
      if (!((Selectable) button).IsInteractable())
        return;
      if (this.OnEquip != null)
      {
        this.OnEquip();
      }
      else
      {
        UnitData unitDataByUniqueId = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID((long) GlobalVars.SelectedUnitUniqueID);
        if (unitDataByUniqueId == null)
          return;
        int selectedEquipmentSlot = (int) GlobalVars.SelectedEquipmentSlot;
        if (!MonoSingleton<GameManager>.Instance.Player.SetUnitEquipment(unitDataByUniqueId, selectedEquipmentSlot))
          return;
        GameParameter.UpdateAll(((Component) this).get_gameObject());
        int selectedJobIndex = this.GetSelectedJobIndex(unitDataByUniqueId);
        ItemData itemDataByItemParam = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemParam(unitDataByUniqueId.GetRankupEquipData(selectedJobIndex, selectedEquipmentSlot).ItemParam);
        GlobalVars.SelectedEquipUniqueID.Set(itemDataByItemParam.UniqueID);
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
      }
    }

    private void OnCreateClick(SRPG_Button button)
    {
      GlobalVars.SelectedCreateItemID = DataSource.FindDataOfClass<ItemParam>(((Component) this.SelectedItem).get_gameObject(), (ItemParam) null).iname;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 102);
    }

    private void OnCreateAllClick(SRPG_Button button)
    {
      GlobalVars.SelectedCreateItemID = DataSource.FindDataOfClass<ItemParam>(((Component) this.SelectedItem).get_gameObject(), (ItemParam) null).iname;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 103);
    }

    private void OnItemTreeSelect(GameObject go)
    {
      int index = this.ItemTree.IndexOf(go) + 1;
      if (index == this.ItemTree.Count)
        return;
      int count = this.mItemParamTree.Count - index;
      if (count > 0)
        this.mItemParamTree.RemoveRange(index, count);
      this.RefreshItemTree(false);
      this.RefreshRecipeItems();
      this.RefreshGainedQuests();
      GameParameter.UpdateAll(((Component) this).get_gameObject());
    }

    private void OnRecipeItemSelect(GameObject go)
    {
      this.mItemParamTree.Add(DataSource.FindDataOfClass<RecipeItemParameter>(this.RecipeItems[this.RecipeItems.IndexOf(go)].get_gameObject(), (RecipeItemParameter) null).Item);
      this.RefreshItemTree(false);
      this.RefreshRecipeItems();
      this.RefreshGainedQuests();
      GameParameter.UpdateAll(((Component) this).get_gameObject());
    }

    private void OnQuestSelect(SRPG_Button button)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      UnitEquipmentWindow.\u003COnQuestSelect\u003Ec__AnonStorey281 selectCAnonStorey281 = new UnitEquipmentWindow.\u003COnQuestSelect\u003Ec__AnonStorey281();
      int index = this.GainedQuests.IndexOf(((Component) button).get_gameObject());
      // ISSUE: reference to a compiler-generated field
      selectCAnonStorey281.quest = DataSource.FindDataOfClass<QuestParam>(this.GainedQuests[index], (QuestParam) null);
      // ISSUE: reference to a compiler-generated field
      if (selectCAnonStorey281.quest == null)
        return;
      // ISSUE: reference to a compiler-generated field
      if (!selectCAnonStorey281.quest.IsDateUnlock(-1L))
      {
        UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.DISABLE_QUEST_DATE_UNLOCK"), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
      }
      else
      {
        // ISSUE: reference to a compiler-generated method
        if (Array.Find<QuestParam>(MonoSingleton<GameManager>.Instance.Player.AvailableQuests, new Predicate<QuestParam>(selectCAnonStorey281.\u003C\u003Em__319)) == null)
        {
          UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.DISABLE_QUEST_CHALLENGE"), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
        }
        else
        {
          // ISSUE: reference to a compiler-generated field
          GlobalVars.SelectedQuestID = selectCAnonStorey281.quest.iname;
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
        }
      }
    }

    private void RefreshEquipButton(UnitData unit)
    {
      bool flag = false;
      int selectedJobIndex = this.GetSelectedJobIndex(unit);
      EquipData rankupEquipData = unit.GetRankupEquipData(selectedJobIndex, (int) GlobalVars.SelectedEquipmentSlot);
      if (rankupEquipData != null && rankupEquipData.IsValid() && (!rankupEquipData.IsEquiped() && MonoSingleton<GameManager>.Instance.Player.HasItem(rankupEquipData.ItemID)))
        flag = (int) rankupEquipData.ItemParam.equipLv <= unit.Lv;
      if (!Object.op_Inequality((Object) this.EquipButton, (Object) null))
        return;
      ((Selectable) this.EquipButton).set_interactable(flag);
    }

    private void Refresh()
    {
      this.Refresh((UnitData) null, (int) GlobalVars.SelectedEquipmentSlot);
    }

    public void Refresh(UnitData unit, int slot)
    {
      if (unit == null)
        unit = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID((long) GlobalVars.SelectedUnitUniqueID);
      if (unit == null)
        return;
      int selectedJobIndex = this.GetSelectedJobIndex(unit);
      EquipData[] rankupEquips = unit.GetRankupEquips(selectedJobIndex);
      if (slot < 0 || rankupEquips.Length <= slot)
        return;
      EquipData equipData = rankupEquips[slot];
      ItemParam itemParam = equipData.ItemParam;
      if (this.mItemParamTree.Count == 0 || this.mItemParamTree[0] != itemParam)
      {
        this.mItemParamTree.Clear();
        this.mItemParamTree.Add(itemParam);
      }
      if (equipData != null && equipData.IsValid() && equipData.IsEquiped())
      {
        this.mEquipmentData = equipData;
      }
      else
      {
        this.mEquipmentData = new EquipData();
        this.mEquipmentData.Setup(itemParam.iname);
      }
      for (int index = 0; index < this.mEquipmentParameters.Count; ++index)
        this.mEquipmentParameters[index].SetActive(false);
      if (this.mEquipmentData != null && this.mEquipmentData.Skill != null)
      {
        BuffEffect buffEffect = this.mEquipmentData.Skill.GetBuffEffect(SkillEffectTargets.Target);
        if (buffEffect != null && buffEffect.targets != null)
        {
          for (int index = 0; index < buffEffect.targets.Count; ++index)
          {
            if (index >= this.mEquipmentParameters.Count)
            {
              GameObject gameObject = (GameObject) Object.Instantiate<GameObject>((M0) this.EquipItemParamTemplate);
              gameObject.get_transform().SetParent((Transform) this.EquipItemParamParent, false);
              this.mEquipmentParameters.Add(gameObject);
            }
            GameObject equipmentParameter = this.mEquipmentParameters[index];
            EquipItemParameter data = DataSource.FindDataOfClass<EquipItemParameter>(equipmentParameter, (EquipItemParameter) null) ?? new EquipItemParameter();
            data.equip = this.mEquipmentData;
            data.param_index = index;
            DataSource.Bind<EquipItemParameter>(equipmentParameter, data);
            equipmentParameter.SetActive(true);
          }
        }
      }
      this.RefreshEquipButton(unit);
      DataSource.Bind<EquipData>(((Component) this).get_gameObject(), this.mEquipmentData);
      this.RefreshItemTree(this.mCreateItemSuccessed);
      this.RefreshRecipeItems();
      this.RefreshGainedQuests();
      GameParameter.UpdateAll(((Component) this).get_gameObject());
    }

    private void RefreshItemTree(bool created = false)
    {
      if (Object.op_Equality((Object) this.ItemTreeTemplate, (Object) null) || Object.op_Equality((Object) this.ItemTreeParent, (Object) null) || Object.op_Equality((Object) this.ItemTreeArrow, (Object) null))
        return;
      for (int index = 0; index < this.ItemTree.Count; ++index)
        this.ItemTree[index].get_gameObject().SetActive(false);
      if (created && this.mItemParamTree.Count > 1)
      {
        RecipeItem recipeItem = Array.Find<RecipeItem>(MonoSingleton<GameManager>.Instance.GetRecipeParam(this.mItemParamTree[this.mItemParamTree.Count - 2].recipe).items, (Predicate<RecipeItem>) (p => p.iname == this.mItemParamTree[this.mItemParamTree.Count - 1].iname));
        if (recipeItem != null && MonoSingleton<GameManager>.Instance.Player.GetItemAmount(this.mItemParamTree[this.mItemParamTree.Count - 1].iname) >= recipeItem.num)
          this.mItemParamTree.RemoveAt(this.mItemParamTree.Count - 1);
      }
      for (int index = 0; index < this.mItemParamTree.Count; ++index)
      {
        if (index >= this.ItemTree.Count)
        {
          ((Component) this.ItemTreeTemplate).get_gameObject().SetActive(index > 0);
          ((Component) this.ItemTreeArrow).get_gameObject().SetActive(index > 0);
          ListItemEvents listItemEvents = (ListItemEvents) Object.Instantiate<ListItemEvents>((M0) this.ItemTreeTemplate);
          ((Component) listItemEvents).get_transform().SetParent((Transform) this.ItemTreeParent, false);
          this.ItemTree.Add(((Component) listItemEvents).get_gameObject());
          ((Component) this.ItemTreeArrow).get_gameObject().SetActive(false);
          ((Component) this.ItemTreeTemplate).get_gameObject().SetActive(false);
        }
        ListItemEvents component = (ListItemEvents) this.ItemTree[index].GetComponent<ListItemEvents>();
        if (Object.op_Inequality((Object) component, (Object) null))
          component.OnSelect = new ListItemEvents.ListItemEvent(this.OnItemTreeSelect);
        this.ItemTree[index].get_gameObject().SetActive(true);
        DataSource.Bind<ItemParam>(this.ItemTree[index].get_gameObject(), this.mItemParamTree[index]);
      }
    }

    private void RefreshRecipeItems()
    {
      if (Object.op_Equality((Object) this.RecipeParent, (Object) null) || Object.op_Equality((Object) this.SelectedItem, (Object) null))
        return;
      ((Component) this.RecipeParent).get_gameObject().SetActive(false);
      if (Object.op_Inequality((Object) this.CreateButton, (Object) null))
        ((Component) this.CreateButton).get_gameObject().SetActive(false);
      if (Object.op_Inequality((Object) this.CreateButtonAll, (Object) null))
        ((Component) this.CreateButtonAll).get_gameObject().SetActive(false);
      if (Object.op_Inequality((Object) this.ConfirmRecipeButton, (Object) null))
        ((Component) this.ConfirmRecipeButton).get_gameObject().SetActive(false);
      if (Object.op_Equality((Object) this.RecipeListItemTemplate, (Object) null) || Object.op_Equality((Object) this.RecipeListParent, (Object) null) || Object.op_Equality((Object) this.RecipeListLine, (Object) null))
        return;
      for (int index = 0; index < this.RecipeItems.Count; ++index)
        this.RecipeItems[index].get_gameObject().SetActive(false);
      int index1 = this.mItemParamTree.Count - 1;
      DataSource.Bind<ItemParam>(((Component) this.SelectedItem).get_gameObject(), this.mItemParamTree[index1]);
      RecipeParam recipeParam = MonoSingleton<GameManager>.Instance.GetRecipeParam(this.mItemParamTree[index1].recipe);
      if (recipeParam == null)
        return;
      if (Object.op_Inequality((Object) this.ConfirmRecipeButton, (Object) null))
        ((Component) this.ConfirmRecipeButton).get_gameObject().SetActive(true);
      ((Component) this.RecipeParent).get_gameObject().SetActive(true);
      for (int index2 = 0; index2 < recipeParam.items.Length; ++index2)
      {
        if (index2 >= this.RecipeItems.Count)
        {
          ((Component) this.RecipeListItemTemplate).get_gameObject().SetActive(index2 > 0);
          ((Component) this.RecipeListLine).get_gameObject().SetActive(index2 > 0);
          ListItemEvents listItemEvents = (ListItemEvents) Object.Instantiate<ListItemEvents>((M0) this.RecipeListItemTemplate);
          ((Component) listItemEvents).get_transform().SetParent((Transform) this.RecipeListParent, false);
          this.RecipeItems.Add(((Component) listItemEvents).get_gameObject());
          ((Component) this.RecipeListLine).get_gameObject().SetActive(false);
          ((Component) this.RecipeListItemTemplate).get_gameObject().SetActive(false);
        }
        ListItemEvents component = (ListItemEvents) this.RecipeItems[index2].GetComponent<ListItemEvents>();
        if (Object.op_Inequality((Object) component, (Object) null))
          component.OnSelect = new ListItemEvents.ListItemEvent(this.OnRecipeItemSelect);
        this.RecipeItems[index2].get_gameObject().SetActive(true);
        RecipeItem recipeItem = recipeParam.items[index2];
        RecipeItemParameter data = new RecipeItemParameter();
        data.Item = MonoSingleton<GameManager>.Instance.GetItemParam(recipeItem.iname);
        data.RecipeItem = recipeItem;
        data.Amount = MonoSingleton<GameManager>.Instance.Player.GetItemAmount(data.Item.iname);
        data.RequiredAmount = recipeItem.num;
        DataSource.Bind<RecipeItemParameter>(this.RecipeItems[index2].get_gameObject(), data);
      }
      int cost = 0;
      Dictionary<string, int> consumes = (Dictionary<string, int>) null;
      bool is_ikkatsu = false;
      if (!MonoSingleton<GameManager>.GetInstanceDirect().Player.CheckEnableCreateItem(this.mItemParamTree[index1], ref is_ikkatsu, ref cost, ref consumes))
        return;
      if (is_ikkatsu)
      {
        if (!Object.op_Inequality((Object) this.CreateButtonAll, (Object) null))
          return;
        ((Component) this.CreateButtonAll).get_gameObject().SetActive(true);
      }
      else
      {
        if (!Object.op_Inequality((Object) this.CreateButton, (Object) null))
          return;
        ((Component) this.CreateButton).get_gameObject().SetActive(true);
      }
    }

    private void RefreshGainedQuests()
    {
      ((Component) this.QuestsParent).get_gameObject().SetActive(false);
      if (Object.op_Equality((Object) this.QuestListItemTemplate, (Object) null) || Object.op_Equality((Object) this.QuestListParent, (Object) null))
        return;
      for (int index = 0; index < this.GainedQuests.Count; ++index)
        this.GainedQuests[index].SetActive(false);
      if (Object.op_Inequality((Object) this.ConfirmQuestButton, (Object) null))
        ((Component) this.ConfirmQuestButton).get_gameObject().SetActive(false);
      int index1 = this.mItemParamTree.Count - 1;
      if (MonoSingleton<GameManager>.Instance.GetRecipeParam(this.mItemParamTree[index1].recipe) != null)
        return;
      if (Object.op_Inequality((Object) this.ConfirmQuestButton, (Object) null))
        ((Component) this.ConfirmQuestButton).get_gameObject().SetActive(true);
      DataSource.Bind<ItemParam>(((Component) this.QuestsParent).get_gameObject(), this.mItemParamTree[index1]);
      string[] quests = this.mItemParamTree[index1].quests;
      if (quests == null || quests.Length == 0)
        return;
      QuestParam[] availableQuests = MonoSingleton<GameManager>.Instance.Player.AvailableQuests;
      ((Component) this.QuestsParent).get_gameObject().SetActive(true);
      for (int index2 = 0; index2 < quests.Length; ++index2)
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        UnitEquipmentWindow.\u003CRefreshGainedQuests\u003Ec__AnonStorey282 questsCAnonStorey282 = new UnitEquipmentWindow.\u003CRefreshGainedQuests\u003Ec__AnonStorey282();
        if (!string.IsNullOrEmpty(quests[index2]))
        {
          // ISSUE: reference to a compiler-generated field
          questsCAnonStorey282.questparam = MonoSingleton<GameManager>.Instance.FindQuest(quests[index2]);
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          if (questsCAnonStorey282.questparam != null && !questsCAnonStorey282.questparam.IsMulti)
          {
            if (index2 >= this.GainedQuests.Count)
            {
              GameObject gameObject = (GameObject) Object.Instantiate<GameObject>((M0) this.QuestListItemTemplate);
              gameObject.get_transform().SetParent((Transform) this.QuestListParent, false);
              SRPG_Button component = (SRPG_Button) gameObject.GetComponent<SRPG_Button>();
              if (Object.op_Inequality((Object) component, (Object) null))
                component.AddListener(new SRPG_Button.ButtonClickEvent(this.OnQuestSelect));
              this.GainedQuests.Add(gameObject);
            }
            this.GainedQuests[index2].SetActive(true);
            SRPG_Button component1 = (SRPG_Button) this.GainedQuests[index2].GetComponent<SRPG_Button>();
            if (Object.op_Inequality((Object) component1, (Object) null))
            {
              // ISSUE: reference to a compiler-generated field
              bool flag1 = questsCAnonStorey282.questparam.IsDateUnlock(-1L);
              // ISSUE: reference to a compiler-generated method
              bool flag2 = Array.Find<QuestParam>(availableQuests, new Predicate<QuestParam>(questsCAnonStorey282.\u003C\u003Em__31B)) != null;
              ((Selectable) component1).set_interactable(flag1 && flag2);
            }
            // ISSUE: reference to a compiler-generated field
            DataSource.Bind<QuestParam>(this.GainedQuests[index2], questsCAnonStorey282.questparam);
          }
        }
      }
    }

    public delegate void EquipEvent();
  }
}
