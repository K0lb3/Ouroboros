// Decompiled with JetBrains decompiler
// Type: SRPG.UnitTobiraEnhanceWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(50, "素材をクリック", FlowNode.PinTypes.Input, 50)]
  [FlowNode.Pin(51, "クエストをクリック", FlowNode.PinTypes.Input, 51)]
  [FlowNode.Pin(101, "入手クエストが選択された", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(100, "扉強化ボタン押下", FlowNode.PinTypes.Input, 100)]
  [FlowNode.Pin(102, "閉じる：完了", FlowNode.PinTypes.Output, 102)]
  [FlowNode.Pin(0, "表示を更新", FlowNode.PinTypes.Input, 0)]
  public class UnitTobiraEnhanceWindow : MonoBehaviour, IFlowInterface
  {
    public const int ON_CLICK_ELEMENT_BUTTON = 50;
    public const int ON_CLICK_QUEST_BUTTON = 51;
    public const int ON_CLICK_TOBIRA_ENHANCE_BUTTON = 100;
    public const int OUT_SELECT_QUEST = 101;
    public const int OUT_CLOSE_WINDOW = 102;
    [HelpBox("アイテムの親となるゲームオブジェクト")]
    public RectTransform ListParent;
    [HelpBox("アイテムスロットの雛形")]
    public GameObject ItemSlotTemplate;
    [HelpBox("不要なスロットの雛形")]
    public GameObject UnusedSlotTemplate;
    [HelpBox("不要なスロットを表示する")]
    public bool ShowUnusedSlots;
    [HelpBox("最大スロット数")]
    public int MaxSlots;
    [HelpBox("足りてないものを表示するラベル")]
    public Text HelpText;
    [SerializeField]
    private Button EnhanceTobiraButton;
    [SerializeField]
    private ScrollRect ScrollParent;
    [SerializeField]
    private Transform QuestListParent;
    [SerializeField]
    private GameObject QuestListItemTemplate;
    [SerializeField]
    private Button MainPanelCloseBtn;
    [SerializeField]
    private GameObject ItemSlotRoot;
    [SerializeField]
    private GameObject ItemSlotBox;
    [SerializeField]
    private GameObject MainPanel;
    [SerializeField]
    private GameObject SubPanel;
    [SerializeField]
    private Text TitleText;
    [SerializeField]
    private Text MessageText;
    private UnitData mCurrentUnit;
    private TobiraData mCurrentTobira;
    private List<GameObject> mItems;
    private List<GameObject> mBoxs;
    private List<GameObject> mGainedQuests;
    private string mLastSelectItemIname;
    private float mDecelerationRate;
    public UnitTobiraEnhanceWindow.CallbackEvent OnCallback;

    public UnitTobiraEnhanceWindow()
    {
      base.\u002Ector();
    }

    public void Initialize(UnitData unit, TobiraData tobira)
    {
      this.mCurrentUnit = unit;
      this.mCurrentTobira = tobira;
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 0:
          this.Refresh();
          break;
        case 50:
          SerializeValueList currentValue1 = FlowNode_ButtonEvent.currentValue as SerializeValueList;
          if (currentValue1 == null)
            break;
          TobiraEnhanceRecipe dataSource = currentValue1.GetDataSource<TobiraEnhanceRecipe>("_self");
          if (dataSource == null)
            break;
          this.RefreshSubPanel(dataSource.Index);
          break;
        case 51:
          SerializeValueList currentValue2 = FlowNode_ButtonEvent.currentValue as SerializeValueList;
          if (currentValue2 == null)
            break;
          QuestParam dataOfClass = DataSource.FindDataOfClass<QuestParam>(currentValue2.GetGameObject("_self"), (QuestParam) null);
          if (dataOfClass == null)
            break;
          this.OnQuestSelect(dataOfClass);
          break;
        case 100:
          if (this.OnCallback != null)
            this.OnCallback();
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 102);
          break;
      }
    }

    private void Start()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ItemSlotTemplate, (UnityEngine.Object) null))
        this.ItemSlotTemplate.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UnusedSlotTemplate, (UnityEngine.Object) null))
        this.UnusedSlotTemplate.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ItemSlotBox, (UnityEngine.Object) null))
        this.ItemSlotBox.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.SubPanel, (UnityEngine.Object) null))
        this.SubPanel.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.QuestListItemTemplate, (UnityEngine.Object) null))
        this.QuestListItemTemplate.SetActive(false);
      string categoryName = TobiraParam.GetCategoryName(this.mCurrentTobira.Param.TobiraCategory);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TitleText, (UnityEngine.Object) null))
        this.TitleText.set_text(string.Format(LocalizedText.Get("sys.TOBIRA_ENHANCE_ITEM_BTN_ENHANCE_TITLE"), (object) categoryName));
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.MessageText, (UnityEngine.Object) null))
        this.MessageText.set_text(string.Format(LocalizedText.Get("sys.TOBIRA_ENHANCE_ITEM_BTN_ENHANCE_MESSAGE"), (object) categoryName));
      this.Refresh();
    }

    private void OnQuestSelect(QuestParam quest)
    {
      if (quest == null)
        DebugUtility.LogWarning("UnitTobiraEnhanceWindow.cs => OnQuestSelect():quest is Null Reference!");
      else if (!quest.IsDateUnlock(-1L))
        UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.DISABLE_QUEST_DATE_UNLOCK"), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
      else if (Array.Find<QuestParam>(MonoSingleton<GameManager>.GetInstanceDirect().Player.AvailableQuests, (Predicate<QuestParam>) (p => p.iname == quest.iname)) == null)
      {
        UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.DISABLE_QUEST_CHALLENGE"), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
      }
      else
      {
        GlobalVars.SelectedQuestID = quest.iname;
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
      }
    }

    private void RefreshSubPanel(int index)
    {
      GameUtility.DestroyGameObjects(this.mGainedQuests);
      this.mGainedQuests.Clear();
      ((Component) this.MainPanelCloseBtn).get_gameObject().SetActive(false);
      if (index < 0)
      {
        DebugUtility.LogWarning("UnitTobiraEnhanceWindow.cs => RefreshSubPanel():index is 0!");
      }
      else
      {
        TobiraRecipeParam currentRecipe = this.GetCurrentRecipe();
        if (currentRecipe == null)
        {
          DebugUtility.LogError("UnitTobiraEnhanceWindow.cs => RefreshSubPanel():recipeParam is Null References!");
        }
        else
        {
          ItemParam data = (ItemParam) null;
          int num = 0;
          if (currentRecipe.UnitPieceNum > 0)
          {
            if (index == num)
            {
              data = MonoSingleton<GameManager>.Instance.GetItemParam(this.mCurrentUnit.UnitParam.piece);
              goto label_22;
            }
            else
              ++num;
          }
          if (currentRecipe.ElementNum > 0)
          {
            if (index == num)
            {
              data = this.mCurrentUnit.GetElementPieceParam();
              goto label_22;
            }
            else
              ++num;
          }
          if (currentRecipe.UnlockElementNum > 0)
          {
            if (index == num)
            {
              data = MonoSingleton<GameManager>.Instance.GetItemParam(this.mCurrentUnit.GetUnlockTobiraElementID());
              goto label_22;
            }
            else
              ++num;
          }
          if (currentRecipe.UnlockBirthNum > 0)
          {
            if (index == num)
            {
              data = MonoSingleton<GameManager>.Instance.GetItemParam(this.mCurrentUnit.GetUnlockTobiraBirthID());
              goto label_22;
            }
            else
              ++num;
          }
          int index1 = index - num;
          if (0 <= index1 && index1 < currentRecipe.Materials.Length)
            data = MonoSingleton<GameManager>.Instance.GetItemParam(currentRecipe.Materials[index1].Iname);
label_22:
          if (data == null)
          {
            DebugUtility.LogError("UnitTobiraEnhanceWindow.cs => RefreshSubPanel():itemParam is Null References!");
          }
          else
          {
            this.SubPanel.SetActive(true);
            DataSource.Bind<ItemParam>(this.SubPanel, data);
            GameParameter.UpdateAll(this.SubPanel.get_gameObject());
            if (this.mLastSelectItemIname != data.iname)
            {
              this.ResetScrollPosition();
              this.mLastSelectItemIname = data.iname;
            }
            if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) QuestDropParam.Instance, (UnityEngine.Object) null))
              return;
            QuestParam[] availableQuests = MonoSingleton<GameManager>.Instance.Player.AvailableQuests;
            List<QuestParam> itemDropQuestList = QuestDropParam.Instance.GetItemDropQuestList(data, GlobalVars.GetDropTableGeneratedDateTime());
            using (List<QuestParam>.Enumerator enumerator = itemDropQuestList.GetEnumerator())
            {
              while (enumerator.MoveNext())
              {
                QuestParam qp = enumerator.Current;
                bool isActive = Array.Find<QuestParam>(availableQuests, (Predicate<QuestParam>) (p => p.iname == qp.iname)) != null;
                this.AddList(qp, isActive);
              }
            }
          }
        }
      }
    }

    private void AddList(QuestParam qparam, bool isActive = false)
    {
      if (qparam == null || qparam.IsMulti)
        DebugUtility.LogWarning("UnitTobiraEnhanceWindow.cs => AddList():qparam is Null Reference!");
      else if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.QuestListItemTemplate, (UnityEngine.Object) null))
        DebugUtility.LogWarning("UnitTobiraEnhanceWindow.cs => AddList():QuestListItemTemplate is Null Reference!");
      else if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.QuestListParent, (UnityEngine.Object) null))
      {
        DebugUtility.LogWarning("UnitTobiraEnhanceWindow.cs => AddList():QuestListParent is Null Reference!");
      }
      else
      {
        GameObject gameObject = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.QuestListItemTemplate);
        gameObject.get_transform().SetParent(this.QuestListParent, false);
        gameObject.SetActive(true);
        this.mGainedQuests.Add(gameObject);
        Button component = (Button) gameObject.GetComponent<Button>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
        {
          bool flag = qparam.IsDateUnlock(-1L);
          ((Selectable) component).set_interactable(flag && isActive);
        }
        DataSource.Bind<QuestParam>(gameObject, qparam);
      }
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
      return (IEnumerator) new UnitTobiraEnhanceWindow.\u003CRefreshScrollRect\u003Ec__Iterator177()
      {
        \u003C\u003Ef__this = this
      };
    }

    private void Refresh()
    {
      if (!this._CheckNullReference())
        return;
      ((Component) this.MainPanelCloseBtn).get_gameObject().SetActive(true);
      this.SubPanel.SetActive(false);
      GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) instanceDirect, (UnityEngine.Object) null))
        return;
      GameUtility.DestroyGameObjects(this.mItems);
      GameUtility.DestroyGameObjects(this.mBoxs);
      this.mItems.Clear();
      this.mBoxs.Clear();
      DataSource.Bind<UnitData>(((Component) this).get_gameObject(), this.mCurrentUnit);
      GameParameter.UpdateAll(((Component) this).get_gameObject());
      string key = (string) null;
      bool flag1 = true;
      TobiraRecipeParam currentRecipe = this.GetCurrentRecipe();
      if (currentRecipe == null)
        return;
      DataSource.Bind<TobiraRecipeParam>(((Component) this).get_gameObject(), currentRecipe);
      if (currentRecipe.Cost > instanceDirect.Player.Gold)
      {
        key = "sys.GOLD_NOT_ENOUGH";
        flag1 = false;
      }
      int length = currentRecipe.Materials.Length;
      if (currentRecipe.UnitPieceNum > 0)
        ++length;
      if (currentRecipe.ElementNum > 0)
        ++length;
      if (currentRecipe.UnlockElementNum > 0)
        ++length;
      if (currentRecipe.UnlockBirthNum > 0)
        ++length;
      GridLayoutGroup component = (GridLayoutGroup) this.ItemSlotBox.GetComponent<GridLayoutGroup>();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) component, (UnityEngine.Object) null))
      {
        DebugUtility.LogWarning("UnitTobiraEnhanceWindow.cs => Refresh2():gridlayout is Not Component [GridLayoutGroup]!");
      }
      else
      {
        int num1 = length / component.get_constraintCount() + 1;
        for (int index = 0; index < num1; ++index)
        {
          GameObject gameObject = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.ItemSlotBox);
          gameObject.get_transform().SetParent(this.ItemSlotRoot.get_transform(), false);
          gameObject.SetActive(true);
          this.mBoxs.Add(gameObject);
        }
        int num2 = 0;
        int index1 = 0;
        if (currentRecipe.UnitPieceNum > 0)
        {
          GameObject gameObject = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.ItemSlotTemplate);
          gameObject.get_transform().SetParent(this.mBoxs[index1].get_transform(), false);
          gameObject.SetActive(true);
          this.mItems.Add(gameObject.get_gameObject());
          ItemParam itemParam = instanceDirect.GetItemParam(this.mCurrentUnit.UnitParam.piece);
          if (itemParam == null)
          {
            DebugUtility.LogWarning("UnitTobiraEnhanceWindow.cs => Refresh():item_param is Null References!");
            return;
          }
          DataSource.Bind<ItemParam>(gameObject.get_gameObject(), itemParam);
          DataSource.Bind<TobiraEnhanceRecipe>(gameObject.get_gameObject(), new TobiraEnhanceRecipe()
          {
            Amount = instanceDirect.Player.GetItemAmount(itemParam.iname),
            RequiredAmount = currentRecipe.UnitPieceNum,
            Index = num2
          });
          if (flag1 && currentRecipe.UnitPieceNum > instanceDirect.Player.GetItemAmount(itemParam.iname))
          {
            flag1 = false;
            key = "sys.ITEM_NOT_ENOUGH";
          }
          ++num2;
        }
        int index2 = num2 / component.get_constraintCount();
        if (currentRecipe.ElementNum > 0)
        {
          GameObject gameObject = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.ItemSlotTemplate);
          gameObject.get_transform().SetParent(this.mBoxs[index2].get_transform(), false);
          gameObject.SetActive(true);
          this.mItems.Add(gameObject.get_gameObject());
          ItemParam elementPieceParam = this.mCurrentUnit.GetElementPieceParam();
          if (elementPieceParam == null)
          {
            DebugUtility.LogWarning("UnitTobiraEnhanceWindow.cs => Refresh():item_param is Null References!");
            return;
          }
          DataSource.Bind<ItemParam>(gameObject.get_gameObject(), elementPieceParam);
          DataSource.Bind<TobiraEnhanceRecipe>(gameObject.get_gameObject(), new TobiraEnhanceRecipe()
          {
            Amount = instanceDirect.Player.GetItemAmount(elementPieceParam.iname),
            RequiredAmount = currentRecipe.ElementNum,
            Index = num2
          });
          if (flag1 && currentRecipe.ElementNum > instanceDirect.Player.GetItemAmount(elementPieceParam.iname))
          {
            flag1 = false;
            key = "sys.ITEM_NOT_ENOUGH";
          }
          ++num2;
        }
        int index3 = num2 / component.get_constraintCount();
        if (currentRecipe.UnlockElementNum > 0)
        {
          GameObject gameObject = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.ItemSlotTemplate);
          gameObject.get_transform().SetParent(this.mBoxs[index3].get_transform(), false);
          gameObject.SetActive(true);
          this.mItems.Add(gameObject.get_gameObject());
          ItemParam itemParam = instanceDirect.GetItemParam(this.mCurrentUnit.GetUnlockTobiraElementID());
          if (itemParam == null)
          {
            DebugUtility.LogWarning("UnitTobiraEnhanceWindow.cs => Refresh():item_param is Null References!");
            return;
          }
          DataSource.Bind<ItemParam>(gameObject.get_gameObject(), itemParam);
          DataSource.Bind<TobiraEnhanceRecipe>(gameObject.get_gameObject(), new TobiraEnhanceRecipe()
          {
            Amount = instanceDirect.Player.GetItemAmount(itemParam.iname),
            RequiredAmount = currentRecipe.UnlockElementNum,
            Index = num2
          });
          if (flag1 && currentRecipe.UnlockElementNum > instanceDirect.Player.GetItemAmount(this.mCurrentUnit.GetUnlockTobiraElementID()))
          {
            flag1 = false;
            key = "sys.ITEM_NOT_ENOUGH";
          }
          ++num2;
        }
        int index4 = num2 / component.get_constraintCount();
        if (currentRecipe.UnlockBirthNum > 0)
        {
          GameObject gameObject = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.ItemSlotTemplate);
          gameObject.get_transform().SetParent(this.mBoxs[index4].get_transform(), false);
          gameObject.SetActive(true);
          this.mItems.Add(gameObject.get_gameObject());
          ItemParam itemParam = instanceDirect.GetItemParam(this.mCurrentUnit.GetUnlockTobiraBirthID());
          if (itemParam == null)
          {
            DebugUtility.LogWarning("UnitTobiraEnhanceWindow.cs => Refresh():item_param is Null References!");
            return;
          }
          DataSource.Bind<ItemParam>(gameObject.get_gameObject(), itemParam);
          DataSource.Bind<TobiraEnhanceRecipe>(gameObject.get_gameObject(), new TobiraEnhanceRecipe()
          {
            Amount = instanceDirect.Player.GetItemAmount(itemParam.iname),
            RequiredAmount = currentRecipe.UnlockBirthNum,
            Index = num2
          });
          if (flag1 && currentRecipe.UnlockBirthNum > instanceDirect.Player.GetItemAmount(this.mCurrentUnit.GetUnlockTobiraBirthID()))
          {
            flag1 = false;
            key = "sys.ITEM_NOT_ENOUGH";
          }
          ++num2;
        }
        foreach (TobiraRecipeMaterialParam material in currentRecipe.Materials)
        {
          if (material != null && !string.IsNullOrEmpty(material.Iname))
          {
            int index5 = num2 / component.get_constraintCount();
            GameObject gameObject = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.ItemSlotTemplate);
            gameObject.get_transform().SetParent(this.mBoxs[index5].get_transform(), false);
            gameObject.SetActive(true);
            this.mItems.Add(gameObject.get_gameObject());
            ItemParam itemParam = instanceDirect.GetItemParam(material.Iname);
            if (itemParam == null)
            {
              DebugUtility.LogWarning("UnitTobiraEnhanceWindow.cs => Refresh():item_param is Null References!");
              return;
            }
            DataSource.Bind<ItemParam>(gameObject.get_gameObject(), itemParam);
            DataSource.Bind<TobiraEnhanceRecipe>(gameObject.get_gameObject(), new TobiraEnhanceRecipe()
            {
              Amount = instanceDirect.Player.GetItemAmount(itemParam.iname),
              RequiredAmount = material.Num,
              Index = num2
            });
            if (flag1 && material.Num > instanceDirect.Player.GetItemAmount(material.Iname))
            {
              flag1 = false;
              key = "sys.ITEM_NOT_ENOUGH";
            }
            ++num2;
          }
        }
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.HelpText, (UnityEngine.Object) null))
        {
          bool flag2 = !string.IsNullOrEmpty(key);
          ((Component) this.HelpText).get_gameObject().SetActive(flag2);
          if (flag2)
            this.HelpText.set_text(LocalizedText.Get(key));
        }
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.EnhanceTobiraButton, (UnityEngine.Object) null))
          return;
        ((Selectable) this.EnhanceTobiraButton).set_interactable(flag1);
      }
    }

    private bool _CheckNullReference()
    {
      if (this.mCurrentUnit == null || this.mCurrentTobira == null)
      {
        DebugUtility.LogWarning("UnitTobiraEnhanceWindow.cs => Refresh():CurrentUnit or CurrentTobira is Null or Empty!");
        return false;
      }
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.ItemSlotTemplate, (UnityEngine.Object) null))
      {
        DebugUtility.LogWarning("UnitTobiraEnhanceWindow.cs => Refresh():ItemSlotTemplate is Null or Empty!");
        return false;
      }
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.ItemSlotRoot, (UnityEngine.Object) null))
      {
        DebugUtility.LogWarning("UnitTobiraEnhanceWindow.cs => Refresh():ItemSlotRoot is Null or Empty!");
        return false;
      }
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.ItemSlotBox, (UnityEngine.Object) null))
      {
        DebugUtility.LogWarning("UnitTobiraEnhanceWindow.cs => Refresh():ItemSlotBox is Null or Empty!");
        return false;
      }
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.SubPanel, (UnityEngine.Object) null))
      {
        DebugUtility.LogWarning("UnitTobiraEnhanceWindow.cs => Refresh():SubPanel is Null References!");
        return false;
      }
      if (!UnityEngine.Object.op_Equality((UnityEngine.Object) this.MainPanelCloseBtn, (UnityEngine.Object) null))
        return true;
      DebugUtility.LogWarning("UnitTobiraEnhanceWindow.cs => Refresh():MainPanelCloseBtn is Null References!");
      return false;
    }

    private TobiraRecipeParam GetCurrentRecipe()
    {
      if (this.mCurrentUnit != null)
        return MonoSingleton<GameManager>.Instance.MasterParam.GetTobiraRecipe(this.mCurrentUnit.UnitID, this.mCurrentTobira.Param.TobiraCategory, this.mCurrentTobira.Lv);
      DebugUtility.LogError("UnitTobiraEnhanceWindow.cs => GetCurrentRecipe():unit and mCurrentUnit is Null References!");
      return (TobiraRecipeParam) null;
    }

    public delegate void CallbackEvent();
  }
}
