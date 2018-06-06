// Decompiled with JetBrains decompiler
// Type: SRPG.UnitEvolutionWindow
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(0, "表示を更新", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(100, "ユニットが進化した", FlowNode.PinTypes.Output, 100)]
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
    public UnitEvolutionWindow.UnitEvolveEvent OnEvolve;
    private List<GameObject> mItems;
    private UnitData mCurrentUnit;

    public UnitEvolutionWindow()
    {
      base.\u002Ector();
    }

    public void Activated(int pinID)
    {
      if (pinID != 0)
        return;
      this.Refresh();
    }

    private void Start()
    {
      if (Object.op_Inequality((Object) this.ItemSlotTemplate, (Object) null))
        ((Component) this.ItemSlotTemplate).get_gameObject().SetActive(false);
      if (Object.op_Inequality((Object) this.UnusedSlotTemplate, (Object) null))
        this.UnusedSlotTemplate.get_gameObject().SetActive(false);
      if (Object.op_Inequality((Object) this.EvolveButton, (Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.EvolveButton.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(OnEvolveClick)));
      }
      this.Refresh();
    }

    private void OnEvolveClick()
    {
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

    private RecipeParam GetCurrentRecipe()
    {
      return MonoSingleton<GameManager>.Instance.GetRecipeParam(this.mCurrentUnit.UnitParam.recipes[this.mCurrentUnit.Rarity]);
    }

    private void OnItemSelect(GameObject go)
    {
      int index1 = this.mItems.IndexOf(go);
      if (index1 < 0)
        return;
      ItemParam itemParam = MonoSingleton<GameManager>.Instance.GetItemParam(this.GetCurrentRecipe().items[index1].iname);
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

    public void Refresh()
    {
      if (Object.op_Equality((Object) this.ItemSlotTemplate, (Object) null) || this.ShowUnusedSlots && Object.op_Equality((Object) this.UnusedSlotTemplate, (Object) null))
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
      RecipeParam currentRecipe = this.GetCurrentRecipe();
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
              GameObject gameObject = (GameObject) Object.Instantiate<GameObject>((M0) this.UnusedSlotTemplate);
              gameObject.get_transform().SetParent((Transform) this.ListParent, false);
              this.mItems.Add(gameObject);
              gameObject.SetActive(true);
            }
          }
          else
          {
            ListItemEvents listItemEvents = (ListItemEvents) Object.Instantiate<ListItemEvents>((M0) this.ItemSlotTemplate);
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
      if (Object.op_Inequality((Object) this.HelpText, (Object) null))
      {
        ((Component) this.HelpText).get_gameObject().SetActive(key != null);
        if (key != null)
          this.HelpText.set_text(LocalizedText.Get(key));
      }
      if (!Object.op_Inequality((Object) this.EvolveButton, (Object) null))
        return;
      ((Selectable) this.EvolveButton).set_interactable(flag);
    }

    public delegate void UnitEvolveEvent();
  }
}
