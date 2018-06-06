// Decompiled with JetBrains decompiler
// Type: SRPG.UnitUnlockWindow
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
  [FlowNode.Pin(100, "Unlock", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(1, "Refresh", FlowNode.PinTypes.Input, 1)]
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
    private List<GameObject> GainedQuests;

    public UnitUnlockWindow()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      if (Object.op_Inequality((Object) this.QuestListItemTemplate, (Object) null))
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
      this.UnlockUnit = MonoSingleton<GameManager>.Instance.GetUnitParam(GlobalVars.UnlockUnitID);
      DataSource.Bind<UnitParam>(((Component) this).get_gameObject(), this.UnlockUnit);
      bool flag = false;
      if (MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueParam(this.UnlockUnit) == null)
        flag = MonoSingleton<GameManager>.Instance.Player.GetItemAmount((string) this.UnlockUnit.piece) >= this.UnlockUnit.GetUnlockNeedPieces();
      if (Object.op_Inequality((Object) this.QuestList, (Object) null))
        this.QuestList.SetActive(!flag);
      if (Object.op_Inequality((Object) this.BtnDecide, (Object) null))
        ((Component) this.BtnDecide).get_gameObject().SetActive(flag);
      if (Object.op_Inequality((Object) this.BtnCancel, (Object) null))
        ((Component) this.BtnCancel).get_gameObject().SetActive(flag);
      if (flag)
      {
        if (Object.op_Inequality((Object) this.TxtTitle, (Object) null))
          this.TxtTitle.set_text(LocalizedText.Get("sys.UNIT_UNLOCK_TITLE"));
        if (Object.op_Inequality((Object) this.TxtComment, (Object) null))
        {
          this.TxtComment.set_text(LocalizedText.Get("sys.UNIT_UNLOCK_COMMENT"));
          ((Component) this.TxtComment).get_gameObject().SetActive(true);
        }
      }
      else
      {
        this.RefreshGainedQuests();
        if (Object.op_Inequality((Object) this.TxtTitle, (Object) null))
          this.TxtTitle.set_text(LocalizedText.Get("sys.UNIT_GAINED_QUEST_TITLE"));
        if (Object.op_Inequality((Object) this.TxtComment, (Object) null))
        {
          this.TxtComment.set_text(LocalizedText.Get("sys.UNIT_GAINED_COMMENT"));
          ((Component) this.TxtComment).get_gameObject().SetActive(this.GainedQuests.Count == 0);
        }
      }
      GameParameter.UpdateAll(((Component) this).get_gameObject());
    }

    private void RefreshGainedQuests()
    {
      this.QuestList.SetActive(false);
      if (Object.op_Equality((Object) this.QuestListItemTemplate, (Object) null) || Object.op_Equality((Object) this.QuestListParent, (Object) null))
        return;
      for (int index = 0; index < this.GainedQuests.Count; ++index)
        this.GainedQuests[index].SetActive(false);
      ItemParam itemParam = MonoSingleton<GameManager>.Instance.GetItemParam((string) this.UnlockUnit.piece);
      DataSource.Bind<ItemParam>(this.QuestList, itemParam);
      string[] quests = itemParam.quests;
      if (quests == null || quests.Length == 0)
        return;
      QuestParam[] availableQuests = MonoSingleton<GameManager>.Instance.Player.AvailableQuests;
      this.QuestList.SetActive(true);
      int index1 = 0;
      int index2 = 0;
      for (; index1 < quests.Length; ++index1)
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        UnitUnlockWindow.\u003CRefreshGainedQuests\u003Ec__AnonStorey28B questsCAnonStorey28B = new UnitUnlockWindow.\u003CRefreshGainedQuests\u003Ec__AnonStorey28B();
        if (!string.IsNullOrEmpty(quests[index1]))
        {
          // ISSUE: reference to a compiler-generated field
          questsCAnonStorey28B.questparam = MonoSingleton<GameManager>.Instance.FindQuest(quests[index1]);
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          if (questsCAnonStorey28B.questparam != null && !questsCAnonStorey28B.questparam.IsMulti)
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
            GameObject gainedQuest = this.GainedQuests[index2];
            gainedQuest.SetActive(true);
            Button component1 = (Button) gainedQuest.GetComponent<Button>();
            if (Object.op_Inequality((Object) component1, (Object) null))
            {
              // ISSUE: reference to a compiler-generated field
              bool flag1 = questsCAnonStorey28B.questparam.IsDateUnlock(-1L);
              // ISSUE: reference to a compiler-generated method
              bool flag2 = Array.Find<QuestParam>(availableQuests, new Predicate<QuestParam>(questsCAnonStorey28B.\u003C\u003Em__326)) != null;
              ((Selectable) component1).set_interactable(flag1 && flag2);
            }
            // ISSUE: reference to a compiler-generated field
            DataSource.Bind<QuestParam>(gainedQuest, questsCAnonStorey28B.questparam);
            ++index2;
          }
        }
      }
    }

    private void OnQuestSelect(SRPG_Button button)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      UnitUnlockWindow.\u003COnQuestSelect\u003Ec__AnonStorey28C selectCAnonStorey28C = new UnitUnlockWindow.\u003COnQuestSelect\u003Ec__AnonStorey28C();
      int index = this.GainedQuests.IndexOf(((Component) button).get_gameObject());
      // ISSUE: reference to a compiler-generated field
      selectCAnonStorey28C.quest = DataSource.FindDataOfClass<QuestParam>(this.GainedQuests[index], (QuestParam) null);
      // ISSUE: reference to a compiler-generated field
      if (selectCAnonStorey28C.quest == null)
        return;
      // ISSUE: reference to a compiler-generated field
      if (!selectCAnonStorey28C.quest.IsDateUnlock(-1L))
      {
        UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.DISABLE_QUEST_DATE_UNLOCK"), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
      }
      else
      {
        // ISSUE: reference to a compiler-generated method
        if (Array.Find<QuestParam>(MonoSingleton<GameManager>.Instance.Player.AvailableQuests, new Predicate<QuestParam>(selectCAnonStorey28C.\u003C\u003Em__327)) == null)
        {
          UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.DISABLE_QUEST_CHALLENGE"), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
        }
        else
        {
          // ISSUE: reference to a compiler-generated field
          GlobalVars.SelectedQuestID = selectCAnonStorey28C.quest.iname;
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
        }
      }
    }
  }
}
