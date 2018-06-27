// Decompiled with JetBrains decompiler
// Type: SRPG.UnitEvolutionWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(102, "閉じる：完了", FlowNode.PinTypes.Output, 102)]
  [FlowNode.Pin(100, "ユニットが進化した", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(2, "閉じる", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(101, "入手クエストが選択された", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(0, "表示を更新", FlowNode.PinTypes.Input, 0)]
  public class UnitEvolutionWindow : MonoBehaviour, IFlowInterface
  {
    [HelpBox("アイテムの親となるゲームオブジェクト")]
    public RectTransform ListParent;
    [HelpBox("アイテムスロットの雛形")]
    public ListItemEvents ItemSlotTemplate;
    [HelpBox("不要なスロットの雛形")]
    public GameObject UnusedSlotTemplate;
    [HelpBox("不要なスロットを表示する")]
    public bool ShowUnusedSlots;
    [HelpBox("最大スロット数")]
    public int MaxSlots;
    [HelpBox("足りてないものを表示するラベル")]
    public Text HelpText;
    public Button EvolveButton;
    public UnitData Unit;
    public ScrollRect ScrollParent;
    public Transform QuestListParent;
    public GameObject QuestListItemTemplate;
    public SRPG_Button MainPanelCloseBtn;
    public GameObject ItemSlotRoot;
    public GameObject ItemSlotBox;
    public GameObject MainPanel;
    public GameObject SubPanel;
    private List<GameObject> mItems;
    private UnitData mCurrentUnit;
    private List<GameObject> mBoxs;
    private List<GameObject> mGainedQuests;
    private string mLastSelectItemIname;
    private float mDecelerationRate;
    public UnitEvolutionWindow.UnitEvolveEvent OnEvolve;
    public UnitEvolutionWindow.EvolveCloseEvent OnEvolveClose;

    public UnitEvolutionWindow()
    {
      base.\u002Ector();
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 0:
          this.Refresh2();
          break;
        case 2:
          if (this.OnEvolveClose != null)
            this.OnEvolveClose();
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 102);
          break;
      }
    }

    private void Start()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ItemSlotTemplate, (UnityEngine.Object) null))
        ((Component) this.ItemSlotTemplate).get_gameObject().SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UnusedSlotTemplate, (UnityEngine.Object) null))
        this.UnusedSlotTemplate.get_gameObject().SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.EvolveButton, (UnityEngine.Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.EvolveButton.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(OnEvolveClick)));
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ItemSlotBox, (UnityEngine.Object) null))
        this.ItemSlotBox.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.SubPanel, (UnityEngine.Object) null))
        this.SubPanel.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.QuestListItemTemplate, (UnityEngine.Object) null))
        this.QuestListItemTemplate.SetActive(false);
      this.Refresh2();
    }

    private void OnEvolveClick()
    {
      if (this.OnEvolveClose != null)
        this.OnEvolveClose();
      if (this.OnEvolve != null)
      {
        this.OnEvolve();
      }
      else
      {
        MonoSingleton<GameManager>.Instance.Player.RarityUpUnit(this.mCurrentUnit);
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
      }
    }

    private RecipeParam GetCurrentRecipe(UnitData unit)
    {
      unit = unit != null ? unit : this.mCurrentUnit;
      if (unit != null)
        return MonoSingleton<GameManager>.Instance.GetRecipeParam(unit.UnitParam.recipes[unit.Rarity]);
      DebugUtility.LogError("UnitEvolutionWindow.cs => GetCurrentRecipe():unit and mCurrentUnit is Null References!");
      return (RecipeParam) null;
    }

    private void OnItemSelect2(GameObject go)
    {
      if (this.mCurrentUnit == null)
      {
        DebugUtility.LogError("UnitEvolutionWindow.cs => OnItemSelect2():mCurrentUnit is Null or Empty!");
      }
      else
      {
        int index = this.mItems.IndexOf(go);
        if (index < 0)
          return;
        this.RefreshSubPanel(index);
      }
    }

    private void RefreshSubPanel(int index = -1)
    {
      this.ClearPanel();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.MainPanelCloseBtn, (UnityEngine.Object) null))
      {
        DebugUtility.LogWarning("UnitEvolutionWindow.cs => RefreshSubPanel():MainPanelCloseBtn is Null References!");
      }
      else
      {
        ((Component) this.MainPanelCloseBtn).get_gameObject().SetActive(false);
        if (index < 0)
        {
          DebugUtility.LogWarning("UnitEvolutionWindow.cs => RefreshSubPanel():index is 0!");
        }
        else
        {
          RecipeParam currentRecipe = this.GetCurrentRecipe(this.mCurrentUnit);
          if (currentRecipe == null)
          {
            DebugUtility.LogError("UnitEvolutionWindow.cs => RefreshSubPanel():recipeParam is Null References!");
          }
          else
          {
            ItemParam itemParam = MonoSingleton<GameManager>.GetInstanceDirect().GetItemParam(currentRecipe.items[index].iname);
            if (itemParam == null)
            {
              DebugUtility.LogError("UnitEvolutionWindow.cs => RefreshSubPanel():itemParam is Null References!");
            }
            else
            {
              this.SubPanel.SetActive(true);
              DataSource.Bind<ItemParam>(this.SubPanel, itemParam);
              GameParameter.UpdateAll(this.SubPanel.get_gameObject());
              if (this.mLastSelectItemIname != itemParam.iname)
              {
                this.ResetScrollPosition();
                this.mLastSelectItemIname = itemParam.iname;
              }
              if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) QuestDropParam.Instance, (UnityEngine.Object) null))
                return;
              QuestParam[] availableQuests = MonoSingleton<GameManager>.Instance.Player.AvailableQuests;
              List<QuestParam> itemDropQuestList = QuestDropParam.Instance.GetItemDropQuestList(itemParam, GlobalVars.GetDropTableGeneratedDateTime());
              // ISSUE: object of a compiler-generated type is created
              // ISSUE: variable of a compiler-generated type
              UnitEvolutionWindow.\u003CRefreshSubPanel\u003Ec__AnonStorey393 panelCAnonStorey393 = new UnitEvolutionWindow.\u003CRefreshSubPanel\u003Ec__AnonStorey393();
              using (List<QuestParam>.Enumerator enumerator = itemDropQuestList.GetEnumerator())
              {
                while (enumerator.MoveNext())
                {
                  // ISSUE: reference to a compiler-generated field
                  panelCAnonStorey393.qp = enumerator.Current;
                  // ISSUE: reference to a compiler-generated field
                  DebugUtility.Log("QuestList:" + panelCAnonStorey393.qp.iname);
                  // ISSUE: reference to a compiler-generated method
                  bool isActive = Array.Find<QuestParam>(availableQuests, new Predicate<QuestParam>(panelCAnonStorey393.\u003C\u003Em__454)) != null;
                  // ISSUE: reference to a compiler-generated field
                  this.AddList(panelCAnonStorey393.qp, isActive);
                }
              }
            }
          }
        }
      }
    }

    private void AddList(QuestParam qparam, bool isActive = false)
    {
      if (qparam == null || qparam.IsMulti)
        DebugUtility.LogWarning("UnitEvolutionWindow.cs => AddList():qparam is Null Reference!");
      else if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.QuestListItemTemplate, (UnityEngine.Object) null))
        DebugUtility.LogWarning("UnitEvolutionWindow.cs => AddList():QuestListItemTemplate is Null Reference!");
      else if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.QuestListParent, (UnityEngine.Object) null))
      {
        DebugUtility.LogWarning("UnitEvolutionWindow.cs => AddList():QuestListParent is Null Reference!");
      }
      else
      {
        GameObject gameObject = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.QuestListItemTemplate);
        this.mGainedQuests.Add(gameObject);
        SRPG_Button component = (SRPG_Button) gameObject.GetComponent<SRPG_Button>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
        {
          component.AddListener(new SRPG_Button.ButtonClickEvent(this.OnQuestSelect));
          bool flag = qparam.IsDateUnlock(-1L);
          ((Selectable) component).set_interactable(flag && isActive);
        }
        DataSource.Bind<QuestParam>(gameObject, qparam);
        gameObject.get_transform().SetParent(this.QuestListParent, false);
        gameObject.SetActive(true);
      }
    }

    private void ClearPanel()
    {
      GameUtility.DestroyGameObjects(this.mGainedQuests);
      this.mGainedQuests.Clear();
    }

    private void ResetScrollPosition()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.ScrollParent, (UnityEngine.Object) null))
        return;
      this.mDecelerationRate = this.ScrollParent.get_decelerationRate();
      this.ScrollParent.set_decelerationRate(0.0f);
      RectTransform questListParent = this.QuestListParent as RectTransform;
      questListParent.set_anchoredPosition(new Vector2((float) questListParent.get_anchoredPosition().x, 0.0f));
      this.StartCoroutine(this.RefreshScrollRect());
    }

    [DebuggerHidden]
    private IEnumerator RefreshScrollRect()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new UnitEvolutionWindow.\u003CRefreshScrollRect\u003Ec__Iterator135() { \u003C\u003Ef__this = this };
    }

    private void OnQuestSelect(SRPG_Button button)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      UnitEvolutionWindow.\u003COnQuestSelect\u003Ec__AnonStorey394 selectCAnonStorey394 = new UnitEvolutionWindow.\u003COnQuestSelect\u003Ec__AnonStorey394();
      int index = this.mGainedQuests.IndexOf(((Component) button).get_gameObject());
      // ISSUE: reference to a compiler-generated field
      selectCAnonStorey394.quest = DataSource.FindDataOfClass<QuestParam>(this.mGainedQuests[index], (QuestParam) null);
      // ISSUE: reference to a compiler-generated field
      if (selectCAnonStorey394.quest == null)
      {
        DebugUtility.LogWarning("UnitEvolutionWindow.cs => OnQuestSelect():quest is Null Reference!");
      }
      else
      {
        // ISSUE: reference to a compiler-generated field
        if (!selectCAnonStorey394.quest.IsDateUnlock(-1L))
        {
          UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.DISABLE_QUEST_DATE_UNLOCK"), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
        }
        else
        {
          // ISSUE: reference to a compiler-generated method
          if (Array.Find<QuestParam>(MonoSingleton<GameManager>.GetInstanceDirect().Player.AvailableQuests, new Predicate<QuestParam>(selectCAnonStorey394.\u003C\u003Em__455)) == null)
          {
            UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.DISABLE_QUEST_CHALLENGE"), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
          }
          else
          {
            // ISSUE: reference to a compiler-generated field
            GlobalVars.SelectedQuestID = selectCAnonStorey394.quest.iname;
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
          }
        }
      }
    }

    private void OnItemSelect(GameObject go)
    {
      int index1 = this.mItems.IndexOf(go);
      if (index1 < 0)
        return;
      ItemParam itemParam = MonoSingleton<GameManager>.Instance.GetItemParam(this.GetCurrentRecipe(this.mCurrentUnit).items[index1].iname);
      string msg = string.Empty;
      if (itemParam.quests != null)
      {
        for (int index2 = 0; index2 < itemParam.quests.Length; ++index2)
        {
          QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(itemParam.quests[index2]);
          msg = msg + quest.name + "\n";
        }
      }
      UIUtility.SystemMessage(itemParam.name, msg, (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
    }

    public void Refresh2()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.ItemSlotTemplate, (UnityEngine.Object) null))
        DebugUtility.LogWarning("UnitEvolutionWindow.cs => Refresh2():ItemSlotTemplate is Null or Empty!");
      else if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.ItemSlotRoot, (UnityEngine.Object) null))
        DebugUtility.LogWarning("UnitEvolutionWindow.cs => Refresh2():ItemSlotRoot is Null or Empty!");
      else if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.ItemSlotBox, (UnityEngine.Object) null))
        DebugUtility.LogWarning("UnitEvolutionWindow.cs => Refresh2():ItemSlotBox is Null or Empty!");
      else if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.SubPanel, (UnityEngine.Object) null))
      {
        DebugUtility.LogWarning("UnitEvolutionWindow.cs => Refresh2():SubPanel is Null References!");
      }
      else
      {
        this.SubPanel.SetActive(false);
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.MainPanelCloseBtn, (UnityEngine.Object) null))
        {
          DebugUtility.LogWarning("UnitEvolutionWindow.cs => Refresh2():MainPanelCloseBtn is Null References!");
        }
        else
        {
          ((Component) this.MainPanelCloseBtn).get_gameObject().SetActive(true);
          GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
          if (UnityEngine.Object.op_Equality((UnityEngine.Object) instanceDirect, (UnityEngine.Object) null))
            return;
          UnitData unitData = this.Unit == null ? instanceDirect.Player.FindUnitDataByUniqueID((long) GlobalVars.SelectedUnitUniqueID) : this.Unit;
          if (unitData == null)
            return;
          this.mCurrentUnit = unitData;
          GameUtility.DestroyGameObjects(this.mItems);
          this.mItems.Clear();
          GameUtility.DestroyGameObjects(this.mBoxs);
          this.mBoxs.Clear();
          DataSource.Bind<UnitData>(((Component) this).get_gameObject(), unitData);
          GameParameter.UpdateAll(((Component) this).get_gameObject());
          string key = (string) null;
          bool flag = unitData.CheckUnitRarityUp();
          RecipeParam currentRecipe = this.GetCurrentRecipe(unitData);
          DataSource.Bind<RecipeParam>(((Component) this).get_gameObject(), currentRecipe);
          if (string.IsNullOrEmpty(key) && currentRecipe != null && currentRecipe.cost > instanceDirect.Player.Gold)
          {
            key = "sys.GOLD_NOT_ENOUGH";
            flag = false;
          }
          if (string.IsNullOrEmpty(key) && unitData.Lv < unitData.GetRarityLevelCap(unitData.Rarity))
          {
            key = "sys.LEVEL_NOT_ENOUGH";
            flag = false;
          }
          if (currentRecipe == null)
            return;
          if (currentRecipe.items == null || currentRecipe.items.Length <= 0)
          {
            DebugUtility.LogWarning("UnitEvolutionWindow.cs => Refresh2():recipe_param.items is Null or Count 0!");
          }
          else
          {
            int length = currentRecipe.items.Length;
            GridLayoutGroup component = (GridLayoutGroup) this.ItemSlotBox.GetComponent<GridLayoutGroup>();
            if (UnityEngine.Object.op_Equality((UnityEngine.Object) component, (UnityEngine.Object) null))
            {
              DebugUtility.LogWarning("UnitEvolutionWindow.cs => Refresh2():gridlayout is Not Component [GridLayoutGroup]!");
            }
            else
            {
              GameObject gameObject1 = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.ItemSlotBox);
              gameObject1.get_transform().SetParent(this.ItemSlotRoot.get_transform(), false);
              gameObject1.SetActive(true);
              this.mBoxs.Add(gameObject1);
              if (length > component.get_constraintCount())
              {
                GameObject gameObject2 = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.ItemSlotBox);
                gameObject2.get_transform().SetParent(this.ItemSlotRoot.get_transform(), false);
                gameObject2.SetActive(true);
                this.mBoxs.Add(gameObject2);
              }
              for (int index1 = 0; index1 < length; ++index1)
              {
                RecipeItem recipeItem = currentRecipe.items[index1];
                if (recipeItem != null && !string.IsNullOrEmpty(recipeItem.iname))
                {
                  int index2 = 0;
                  if (length > component.get_constraintCount())
                  {
                    if (length % 2 == 0)
                    {
                      if (index1 >= component.get_constraintCount() - 1)
                        index2 = 1;
                    }
                    else if (index1 >= component.get_constraintCount())
                      index2 = 1;
                  }
                  ListItemEvents listItemEvents = (ListItemEvents) UnityEngine.Object.Instantiate<ListItemEvents>((M0) this.ItemSlotTemplate);
                  ((Component) listItemEvents).get_transform().SetParent(this.mBoxs[index2].get_transform(), false);
                  this.mItems.Add(((Component) listItemEvents).get_gameObject());
                  listItemEvents.OnSelect = new ListItemEvents.ListItemEvent(this.OnItemSelect2);
                  ((Component) listItemEvents).get_gameObject().SetActive(true);
                  ItemParam itemParam = instanceDirect.GetItemParam(recipeItem.iname);
                  if (itemParam == null)
                  {
                    DebugUtility.LogWarning("UnitEvolutionWindow.cs => Refresh2():item_param is Null References!");
                    return;
                  }
                  DataSource.Bind<ItemParam>(((Component) listItemEvents).get_gameObject(), itemParam);
                  JobEvolutionRecipe data = new JobEvolutionRecipe();
                  data.Item = itemParam;
                  data.RecipeItem = recipeItem;
                  data.Amount = instanceDirect.Player.GetItemAmount(recipeItem.iname);
                  data.RequiredAmount = recipeItem.num;
                  if (data.Amount < data.RequiredAmount)
                  {
                    flag = false;
                    if (key == null)
                      key = "sys.ITEM_NOT_ENOUGH";
                  }
                  DataSource.Bind<JobEvolutionRecipe>(((Component) listItemEvents).get_gameObject(), data);
                }
              }
              if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.HelpText, (UnityEngine.Object) null))
              {
                ((Component) this.HelpText).get_gameObject().SetActive(!string.IsNullOrEmpty(key));
                if (!string.IsNullOrEmpty(key))
                  this.HelpText.set_text(LocalizedText.Get(key));
              }
              if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.EvolveButton, (UnityEngine.Object) null))
                return;
              ((Selectable) this.EvolveButton).set_interactable(flag);
            }
          }
        }
      }
    }

    public void Refresh()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.ItemSlotTemplate, (UnityEngine.Object) null) || this.ShowUnusedSlots && UnityEngine.Object.op_Equality((UnityEngine.Object) this.UnusedSlotTemplate, (UnityEngine.Object) null))
        return;
      this.mCurrentUnit = this.Unit == null ? MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID((long) GlobalVars.SelectedUnitUniqueID) : this.Unit;
      if (this.mCurrentUnit == null)
        return;
      GameUtility.DestroyGameObjects(this.mItems);
      this.mItems.Clear();
      DataSource.Bind<UnitData>(((Component) this).get_gameObject(), this.mCurrentUnit);
      GameParameter.UpdateAll(((Component) this).get_gameObject());
      string key = (string) null;
      bool flag = this.mCurrentUnit.CheckUnitRarityUp();
      RecipeParam currentRecipe = this.GetCurrentRecipe(this.mCurrentUnit);
      DataSource.Bind<RecipeParam>(((Component) this).get_gameObject(), currentRecipe);
      if (key == null && currentRecipe != null && currentRecipe.cost > MonoSingleton<GameManager>.Instance.Player.Gold)
      {
        key = "sys.GOLD_NOT_ENOUGH";
        flag = false;
      }
      if (key == null && this.mCurrentUnit.Lv < this.mCurrentUnit.GetRarityLevelCap(this.mCurrentUnit.Rarity))
      {
        key = "sys.LEVEL_NOT_ENOUGH";
        flag = false;
      }
      if (currentRecipe != null)
      {
        for (int index = 0; index < currentRecipe.items.Length; ++index)
        {
          RecipeItem recipeItem = currentRecipe.items[index];
          if (recipeItem == null || string.IsNullOrEmpty(recipeItem.iname))
          {
            if (this.ShowUnusedSlots)
            {
              GameObject gameObject = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.UnusedSlotTemplate);
              gameObject.get_transform().SetParent((Transform) this.ListParent, false);
              this.mItems.Add(gameObject);
              gameObject.SetActive(true);
            }
          }
          else
          {
            ListItemEvents listItemEvents = (ListItemEvents) UnityEngine.Object.Instantiate<ListItemEvents>((M0) this.ItemSlotTemplate);
            ((Component) listItemEvents).get_transform().SetParent((Transform) this.ListParent, false);
            this.mItems.Add(((Component) listItemEvents).get_gameObject());
            listItemEvents.OnSelect = new ListItemEvents.ListItemEvent(this.OnItemSelect);
            ((Component) listItemEvents).get_gameObject().SetActive(true);
            ItemParam itemParam = MonoSingleton<GameManager>.Instance.GetItemParam(recipeItem.iname);
            JobEvolutionRecipe data = new JobEvolutionRecipe();
            data.Item = itemParam;
            data.RecipeItem = recipeItem;
            data.Amount = MonoSingleton<GameManager>.Instance.Player.GetItemAmount(recipeItem.iname);
            data.RequiredAmount = recipeItem.num;
            if (data.Amount < data.RequiredAmount)
            {
              flag = false;
              if (key == null)
                key = "sys.ITEM_NOT_ENOUGH";
            }
            DataSource.Bind<JobEvolutionRecipe>(((Component) listItemEvents).get_gameObject(), data);
          }
        }
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.HelpText, (UnityEngine.Object) null))
      {
        ((Component) this.HelpText).get_gameObject().SetActive(key != null);
        if (key != null)
          this.HelpText.set_text(LocalizedText.Get(key));
      }
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.EvolveButton, (UnityEngine.Object) null))
        return;
      ((Selectable) this.EvolveButton).set_interactable(flag);
    }

    public delegate void UnitEvolveEvent();

    public delegate void EvolveCloseEvent();
  }
}
