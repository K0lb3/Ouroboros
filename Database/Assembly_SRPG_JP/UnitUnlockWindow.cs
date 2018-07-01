// Decompiled with JetBrains decompiler
// Type: SRPG.UnitUnlockWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(1, "Refresh", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(100, "Unlock", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(101, "Selected Quest", FlowNode.PinTypes.Output, 101)]
  public class UnitUnlockWindow : MonoBehaviour, IFlowInterface
  {
    public GameObject QuestList;
    public RectTransform QuestListParent;
    public GameObject QuestListItemTemplate;
    public Text TxtTitle;
    public Text TxtComment;
    public Text TxtQuestNothing;
    public GameObject GOUnlockLimit;
    public Button BtnDecide;
    public Button BtnCancel;
    private UnitParam UnlockUnit;
    private List<GameObject> mGainedQuests;

    public UnitUnlockWindow()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.QuestListItemTemplate, (UnityEngine.Object) null))
        this.QuestListItemTemplate.SetActive(false);
      this.Refresh();
    }

    public void Activated(int pinID)
    {
      if (pinID != 1)
        return;
      this.Refresh();
    }

    private void Refresh()
    {
      string unlockUnitId = GlobalVars.UnlockUnitID;
      this.UnlockUnit = MonoSingleton<GameManager>.Instance.GetUnitParam(unlockUnitId);
      DataSource.Bind<UnitParam>(((Component) this).get_gameObject(), this.UnlockUnit);
      UnitData unitDataByUnitId = MonoSingleton<GameManager>.GetInstanceDirect().Player.FindUnitDataByUnitID(unlockUnitId);
      if (unitDataByUnitId != null)
        DataSource.Bind<UnitData>(((Component) this).get_gameObject(), unitDataByUnitId);
      bool flag = false;
      if (MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueParam(this.UnlockUnit) == null)
        flag = MonoSingleton<GameManager>.Instance.Player.GetItemAmount(this.UnlockUnit.piece) >= this.UnlockUnit.GetUnlockNeedPieces();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.QuestList, (UnityEngine.Object) null))
        this.QuestList.SetActive(!flag);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BtnDecide, (UnityEngine.Object) null))
        ((Component) this.BtnDecide).get_gameObject().SetActive(flag);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BtnCancel, (UnityEngine.Object) null))
        ((Component) this.BtnCancel).get_gameObject().SetActive(flag);
      if (flag)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TxtTitle, (UnityEngine.Object) null))
          this.TxtTitle.set_text(LocalizedText.Get("sys.UNIT_UNLOCK_TITLE"));
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TxtComment, (UnityEngine.Object) null))
        {
          this.TxtComment.set_text(LocalizedText.Get("sys.UNIT_UNLOCK_COMMENT"));
          ((Component) this.TxtComment).get_gameObject().SetActive(true);
        }
      }
      else
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TxtTitle, (UnityEngine.Object) null))
          this.TxtTitle.set_text(LocalizedText.Get("sys.UNIT_GAINED_QUEST_TITLE"));
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TxtComment, (UnityEngine.Object) null))
        {
          this.TxtComment.set_text(LocalizedText.Get("sys.UNIT_GAINED_COMMENT"));
          ((Component) this.TxtComment).get_gameObject().SetActive(this.mGainedQuests.Count == 0);
        }
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.GOUnlockLimit, (UnityEngine.Object) null) && MonoSingleton<GameManager>.Instance.MasterParam.GetUnitUnlockTimeParam(this.UnlockUnit.unlock_time) != null)
          this.GOUnlockLimit.SetActive(true);
        this.RefreshGainedQuests(this.UnlockUnit);
      }
      GameParameter.UpdateAll(((Component) this).get_gameObject());
    }

    private void RefreshGainedQuests(UnitParam unit)
    {
      this.ClearPanel();
      this.QuestList.SetActive(false);
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.QuestListItemTemplate, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) this.QuestListParent, (UnityEngine.Object) null))
        return;
      ItemParam itemParam = MonoSingleton<GameManager>.Instance.GetItemParam(this.UnlockUnit.piece);
      DataSource.Bind<ItemParam>(this.QuestList, itemParam);
      this.QuestList.SetActive(true);
      this.SetScrollTop();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) QuestDropParam.Instance, (UnityEngine.Object) null))
        return;
      QuestParam[] availableQuests = MonoSingleton<GameManager>.Instance.Player.AvailableQuests;
      List<QuestParam> itemDropQuestList = QuestDropParam.Instance.GetItemDropQuestList(itemParam, GlobalVars.GetDropTableGeneratedDateTime());
      using (List<QuestParam>.Enumerator enumerator = itemDropQuestList.GetEnumerator())
      {
        while (enumerator.MoveNext())
          this.AddPanel(enumerator.Current, availableQuests);
      }
      if (itemDropQuestList.Count != 0 || !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TxtQuestNothing, (UnityEngine.Object) null))
        return;
      ((Component) this.TxtQuestNothing).get_gameObject().SetActive(true);
    }

    private void SetScrollTop()
    {
      RectTransform component = (RectTransform) ((Component) this.QuestListParent).GetComponent<RectTransform>();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
        return;
      Vector2 anchoredPosition = component.get_anchoredPosition();
      anchoredPosition.y = (__Null) 0.0;
      component.set_anchoredPosition(anchoredPosition);
    }

    public void ClearPanel()
    {
      this.mGainedQuests.Clear();
      for (int index = 0; index < ((Transform) this.QuestListParent).get_childCount(); ++index)
      {
        GameObject gameObject = ((Component) ((Transform) this.QuestListParent).GetChild(index)).get_gameObject();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.QuestListItemTemplate, (UnityEngine.Object) gameObject))
          UnityEngine.Object.Destroy((UnityEngine.Object) gameObject);
      }
    }

    private void AddPanel(QuestParam questparam, QuestParam[] availableQuests)
    {
      this.QuestList.SetActive(true);
      if (questparam == null || questparam.IsMulti)
        return;
      GameObject gameObject = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.QuestListItemTemplate);
      SRPG_Button component1 = (SRPG_Button) gameObject.GetComponent<SRPG_Button>();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component1, (UnityEngine.Object) null))
        component1.AddListener(new SRPG_Button.ButtonClickEvent(this.OnQuestSelect));
      this.mGainedQuests.Add(gameObject);
      Button component2 = (Button) gameObject.GetComponent<Button>();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component2, (UnityEngine.Object) null))
      {
        bool flag1 = questparam.IsDateUnlock(-1L);
        bool flag2 = Array.Find<QuestParam>(availableQuests, (Predicate<QuestParam>) (p => p == questparam)) != null;
        ((Selectable) component2).set_interactable(flag1 && flag2);
      }
      DataSource.Bind<QuestParam>(gameObject, questparam);
      gameObject.get_transform().SetParent((Transform) this.QuestListParent, false);
      gameObject.SetActive(true);
    }

    private void OnQuestSelect(SRPG_Button button)
    {
      QuestParam quest = DataSource.FindDataOfClass<QuestParam>(this.mGainedQuests[this.mGainedQuests.IndexOf(((Component) button).get_gameObject())], (QuestParam) null);
      if (quest == null)
        return;
      if (!quest.IsDateUnlock(-1L))
        UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.DISABLE_QUEST_DATE_UNLOCK"), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
      else if (Array.Find<QuestParam>(MonoSingleton<GameManager>.Instance.Player.AvailableQuests, (Predicate<QuestParam>) (p => p == quest)) == null)
      {
        UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.DISABLE_QUEST_CHALLENGE"), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
      }
      else
      {
        GlobalVars.SelectedQuestID = quest.iname;
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
      }
    }
  }
}
