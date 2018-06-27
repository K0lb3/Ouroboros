// Decompiled with JetBrains decompiler
// Type: SRPG.UnitUnlockWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

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
        flag = MonoSingleton<GameManager>.Instance.Player.GetItemAmount((string) this.UnlockUnit.piece) >= this.UnlockUnit.GetUnlockNeedPieces();
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
      ItemParam itemParam = MonoSingleton<GameManager>.Instance.GetItemParam((string) this.UnlockUnit.piece);
      DataSource.Bind<ItemParam>(this.QuestList, itemParam);
      this.QuestList.SetActive(true);
      this.SetScrollTop();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) QuestDropParam.Instance, (UnityEngine.Object) null))
        return;
      QuestParam[] availableQuests = MonoSingleton<GameManager>.Instance.Player.AvailableQuests;
      using (List<QuestParam>.Enumerator enumerator = QuestDropParam.Instance.GetItemDropQuestList(itemParam, GlobalVars.GetDropTableGeneratedDateTime()).GetEnumerator())
      {
        while (enumerator.MoveNext())
          this.AddPanel(enumerator.Current, availableQuests);
      }
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
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      UnitUnlockWindow.\u003COnQuestSelect\u003Ec__AnonStorey3A3 selectCAnonStorey3A3 = new UnitUnlockWindow.\u003COnQuestSelect\u003Ec__AnonStorey3A3();
      int index = this.mGainedQuests.IndexOf(((Component) button).get_gameObject());
      // ISSUE: reference to a compiler-generated field
      selectCAnonStorey3A3.quest = DataSource.FindDataOfClass<QuestParam>(this.mGainedQuests[index], (QuestParam) null);
      // ISSUE: reference to a compiler-generated field
      if (selectCAnonStorey3A3.quest == null)
        return;
      // ISSUE: reference to a compiler-generated field
      if (!selectCAnonStorey3A3.quest.IsDateUnlock(-1L))
      {
        UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.DISABLE_QUEST_DATE_UNLOCK"), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
      }
      else
      {
        // ISSUE: reference to a compiler-generated method
        if (Array.Find<QuestParam>(MonoSingleton<GameManager>.Instance.Player.AvailableQuests, new Predicate<QuestParam>(selectCAnonStorey3A3.\u003C\u003Em__46F)) == null)
        {
          UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.DISABLE_QUEST_CHALLENGE"), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
        }
        else
        {
          // ISSUE: reference to a compiler-generated field
          GlobalVars.SelectedQuestID = selectCAnonStorey3A3.quest.iname;
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
        }
      }
    }
  }
}
