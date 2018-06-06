// Decompiled with JetBrains decompiler
// Type: SRPG.UnitKakeraWindow
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(1, "Refresh", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(100, "クエスト選択", FlowNode.PinTypes.Output, 100)]
  public class UnitKakeraWindow : MonoBehaviour, IFlowInterface
  {
    public UnitKakeraWindow.KakuseiWindowEvent OnKakuseiAccept;
    public GameObject QuestList;
    public RectTransform QuestListParent;
    public GameObject QuestListItemTemplate;
    public GameObject Kakera_Unit;
    public GameObject Kakera_Elem;
    public GameObject Kakera_Common;
    public Text Kakera_Consume_Unit;
    public Text Kakera_Consume_Elem;
    public Text Kakera_Consume_Common;
    public Text Kakera_Amount_Unit;
    public Text Kakera_Amount_Elem;
    public Text Kakera_Amount_Common;
    public Text Kakera_Consume_Message;
    public Text Kakera_Caution_Message;
    public GameObject NotFoundGainQuestObject;
    public GameObject CautionObject;
    public Button DecideButton;
    public GameObject JobUnlock;
    private UnitData mCurrentUnit;
    private JobParam mUnlockJobParam;
    private List<GameObject> mGainedQuests;
    private List<ItemData> mUsedElemPieces;

    public UnitKakeraWindow()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      if (Object.op_Inequality((Object) this.QuestListItemTemplate, (Object) null))
        this.QuestListItemTemplate.SetActive(false);
      if (!Object.op_Inequality((Object) this.DecideButton, (Object) null))
        return;
      // ISSUE: method pointer
      ((UnityEvent) this.DecideButton.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(OnDecideClick)));
    }

    public void Activated(int pinID)
    {
      if (pinID != 1)
        return;
      this.Refresh(this.mCurrentUnit, this.mUnlockJobParam);
    }

    public void Refresh(UnitData unit = null, JobParam jobUnlock = null)
    {
      if (unit == null)
        return;
      this.mCurrentUnit = unit;
      this.mUnlockJobParam = jobUnlock;
      this.mUsedElemPieces.Clear();
      DataSource.Bind<UnitData>(((Component) this).get_gameObject(), unit);
      int awakeLv = unit.AwakeLv;
      bool flag1 = unit.GetAwakeLevelCap() > awakeLv;
      if (Object.op_Inequality((Object) this.CautionObject, (Object) null))
        this.CautionObject.SetActive(!flag1);
      if (Object.op_Inequality((Object) this.DecideButton, (Object) null))
        ((Selectable) this.DecideButton).set_interactable(flag1 && unit.CheckUnitAwaking());
      if (flag1)
      {
        PlayerData player = MonoSingleton<GameManager>.Instance.Player;
        int awakeNeedPieces = unit.GetAwakeNeedPieces();
        bool flag2 = false;
        if (Object.op_Inequality((Object) this.Kakera_Unit, (Object) null))
        {
          ItemData itemDataByItemId = player.FindItemDataByItemID((string) unit.UnitParam.piece);
          if (itemDataByItemId != null && itemDataByItemId.Num > 0 && awakeNeedPieces > 0)
          {
            DataSource.Bind<ItemData>(this.Kakera_Unit, itemDataByItemId);
            int num = Math.Min(awakeNeedPieces, itemDataByItemId.Num);
            if (Object.op_Inequality((Object) this.Kakera_Consume_Unit, (Object) null))
              this.Kakera_Consume_Unit.set_text(string.Format(LocalizedText.Get("sys.KAKUSEI_KAKERA_CONSUME"), (object) itemDataByItemId.Param.name, (object) num));
            if (Object.op_Inequality((Object) this.Kakera_Amount_Unit, (Object) null))
              this.Kakera_Amount_Unit.set_text(string.Format(LocalizedText.Get("sys.KAKUSEI_KAKERA_AMOUNT"), (object) itemDataByItemId.Num));
            this.Kakera_Unit.SetActive(true);
            flag2 = true;
            awakeNeedPieces -= num;
          }
          else
            this.Kakera_Unit.SetActive(false);
        }
        if (Object.op_Inequality((Object) this.Kakera_Elem, (Object) null))
        {
          ItemData elementPieceData = unit.GetElementPieceData();
          if (elementPieceData != null && elementPieceData.Num > 0 && awakeNeedPieces > 0)
          {
            DataSource.Bind<ItemData>(this.Kakera_Elem, elementPieceData);
            int num = Math.Min(awakeNeedPieces, elementPieceData.Num);
            if (Object.op_Inequality((Object) this.Kakera_Consume_Elem, (Object) null))
              this.Kakera_Consume_Elem.set_text(string.Format(LocalizedText.Get("sys.KAKUSEI_KAKERA_CONSUME"), (object) elementPieceData.Param.name, (object) num));
            if (Object.op_Inequality((Object) this.Kakera_Amount_Elem, (Object) null))
              this.Kakera_Amount_Elem.set_text(string.Format(LocalizedText.Get("sys.KAKUSEI_KAKERA_AMOUNT"), (object) elementPieceData.Num));
            this.Kakera_Elem.SetActive(true);
            flag2 = true;
            awakeNeedPieces -= num;
            this.mUsedElemPieces.Add(elementPieceData);
          }
          else
            this.Kakera_Elem.SetActive(false);
        }
        if (Object.op_Inequality((Object) this.Kakera_Common, (Object) null))
        {
          ItemData commonPieceData = unit.GetCommonPieceData();
          if (commonPieceData != null && commonPieceData.Num > 0 && awakeNeedPieces > 0)
          {
            DataSource.Bind<ItemData>(this.Kakera_Common, commonPieceData);
            int num = Math.Min(awakeNeedPieces, commonPieceData.Num);
            if (Object.op_Inequality((Object) this.Kakera_Consume_Common, (Object) null))
              this.Kakera_Consume_Common.set_text(string.Format(LocalizedText.Get("sys.KAKUSEI_KAKERA_CONSUME"), (object) commonPieceData.Param.name, (object) num));
            if (Object.op_Inequality((Object) this.Kakera_Amount_Common, (Object) null))
              this.Kakera_Amount_Common.set_text(string.Format(LocalizedText.Get("sys.KAKUSEI_KAKERA_AMOUNT"), (object) commonPieceData.Num));
            this.Kakera_Common.SetActive(true);
            flag2 = true;
            awakeNeedPieces -= num;
            this.mUsedElemPieces.Add(commonPieceData);
          }
          else
            this.Kakera_Common.SetActive(false);
        }
        if (Object.op_Inequality((Object) this.JobUnlock, (Object) null))
        {
          bool flag3 = false;
          if (jobUnlock != null)
          {
            DataSource.Bind<JobParam>(this.JobUnlock, jobUnlock);
            flag3 = true;
          }
          this.JobUnlock.SetActive(flag3);
        }
        if (flag2)
        {
          if (Object.op_Inequality((Object) this.Kakera_Consume_Message, (Object) null))
            this.Kakera_Consume_Message.set_text(LocalizedText.Get(awakeNeedPieces != 0 ? "sys.CONFIRM_KAKUSEI4" : "sys.CONFIRM_KAKUSEI2"));
        }
        else
        {
          if (Object.op_Inequality((Object) this.Kakera_Caution_Message, (Object) null))
            this.Kakera_Caution_Message.set_text(LocalizedText.Get("sys.CONFIRM_KAKUSEI3"));
          if (Object.op_Inequality((Object) this.CautionObject, (Object) null))
            this.CautionObject.SetActive(true);
        }
      }
      else if (Object.op_Inequality((Object) this.Kakera_Caution_Message, (Object) null))
        this.Kakera_Caution_Message.set_text(LocalizedText.Get("sys.KAKUSEI_CAPPED"));
      this.RefreshGainedQuests(unit);
      GameParameter.UpdateAll(((Component) this).get_gameObject());
    }

    private void RefreshGainedQuests(UnitData unit)
    {
      if (Object.op_Equality((Object) this.QuestList, (Object) null))
        return;
      this.QuestList.SetActive(false);
      if (Object.op_Inequality((Object) this.NotFoundGainQuestObject, (Object) null))
      {
        Text component = (Text) this.NotFoundGainQuestObject.GetComponent<Text>();
        if (Object.op_Inequality((Object) component, (Object) null))
          component.set_text(LocalizedText.Get("sys.UNIT_GAINED_COMMENT"));
        this.NotFoundGainQuestObject.SetActive(true);
      }
      if (Object.op_Equality((Object) this.QuestListItemTemplate, (Object) null) || Object.op_Equality((Object) this.QuestListParent, (Object) null) || unit == null)
        return;
      for (int index = 0; index < this.mGainedQuests.Count; ++index)
        this.mGainedQuests[index].SetActive(false);
      ItemParam itemParam = MonoSingleton<GameManager>.Instance.GetItemParam((string) unit.UnitParam.piece);
      DataSource.Bind<ItemParam>(this.QuestList, itemParam);
      string[] quests = itemParam.quests;
      if (quests == null || quests.Length == 0)
        return;
      QuestParam[] availableQuests = MonoSingleton<GameManager>.Instance.Player.AvailableQuests;
      this.QuestList.SetActive(true);
      if (Object.op_Inequality((Object) this.NotFoundGainQuestObject, (Object) null))
        this.NotFoundGainQuestObject.SetActive(false);
      int index1 = 0;
      int index2 = 0;
      for (; index1 < quests.Length; ++index1)
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        UnitKakeraWindow.\u003CRefreshGainedQuests\u003Ec__AnonStorey287 questsCAnonStorey287 = new UnitKakeraWindow.\u003CRefreshGainedQuests\u003Ec__AnonStorey287();
        if (!string.IsNullOrEmpty(quests[index1]))
        {
          // ISSUE: reference to a compiler-generated field
          questsCAnonStorey287.questparam = MonoSingleton<GameManager>.Instance.FindQuest(quests[index1]);
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          if (questsCAnonStorey287.questparam != null && !questsCAnonStorey287.questparam.IsMulti)
          {
            if (index2 >= this.mGainedQuests.Count)
            {
              GameObject gameObject = (GameObject) Object.Instantiate<GameObject>((M0) this.QuestListItemTemplate);
              gameObject.get_transform().SetParent((Transform) this.QuestListParent, false);
              SRPG_Button component = (SRPG_Button) gameObject.GetComponent<SRPG_Button>();
              if (Object.op_Inequality((Object) component, (Object) null))
                component.AddListener(new SRPG_Button.ButtonClickEvent(this.OnQuestSelect));
              this.mGainedQuests.Add(gameObject);
            }
            GameObject mGainedQuest = this.mGainedQuests[index2];
            mGainedQuest.SetActive(true);
            Button component1 = (Button) mGainedQuest.GetComponent<Button>();
            if (Object.op_Inequality((Object) component1, (Object) null))
            {
              // ISSUE: reference to a compiler-generated field
              bool flag1 = questsCAnonStorey287.questparam.IsDateUnlock(-1L);
              // ISSUE: reference to a compiler-generated method
              bool flag2 = Array.Find<QuestParam>(availableQuests, new Predicate<QuestParam>(questsCAnonStorey287.\u003C\u003Em__322)) != null;
              ((Selectable) component1).set_interactable(flag1 && flag2);
            }
            // ISSUE: reference to a compiler-generated field
            DataSource.Bind<QuestParam>(mGainedQuest, questsCAnonStorey287.questparam);
            ++index2;
          }
        }
      }
    }

    private void OnQuestSelect(SRPG_Button button)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      UnitKakeraWindow.\u003COnQuestSelect\u003Ec__AnonStorey288 selectCAnonStorey288 = new UnitKakeraWindow.\u003COnQuestSelect\u003Ec__AnonStorey288();
      int index = this.mGainedQuests.IndexOf(((Component) button).get_gameObject());
      // ISSUE: reference to a compiler-generated field
      selectCAnonStorey288.quest = DataSource.FindDataOfClass<QuestParam>(this.mGainedQuests[index], (QuestParam) null);
      // ISSUE: reference to a compiler-generated field
      if (selectCAnonStorey288.quest == null)
        return;
      // ISSUE: reference to a compiler-generated field
      if (!selectCAnonStorey288.quest.IsDateUnlock(-1L))
      {
        UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.DISABLE_QUEST_DATE_UNLOCK"), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
      }
      else
      {
        // ISSUE: reference to a compiler-generated method
        if (Array.Find<QuestParam>(MonoSingleton<GameManager>.Instance.Player.AvailableQuests, new Predicate<QuestParam>(selectCAnonStorey288.\u003C\u003Em__323)) == null)
        {
          UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.DISABLE_QUEST_CHALLENGE"), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
        }
        else
        {
          // ISSUE: reference to a compiler-generated field
          GlobalVars.SelectedQuestID = selectCAnonStorey288.quest.iname;
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
        }
      }
    }

    private void OnDecideClick()
    {
      if (this.mUsedElemPieces.Count > 0)
      {
        string str = (string) null;
        for (int index = 0; index < this.mUsedElemPieces.Count; ++index)
        {
          if (index > 0)
            str += "、";
          str += this.mUsedElemPieces[index].Param.name;
        }
        UIUtility.ConfirmBox(string.Format(LocalizedText.Get("sys.KAKUSEI_CONFIRM_ELEMENT_KAKERA"), (object) str), new UIUtility.DialogResultEvent(this.OnKakusei), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
      }
      else
        this.OnKakusei((GameObject) null);
    }

    private void OnKakusei(GameObject go)
    {
      if (this.OnKakuseiAccept != null)
        this.OnKakuseiAccept();
      else
        MonoSingleton<GameManager>.Instance.Player.AwakingUnit(this.mCurrentUnit);
    }

    public delegate void KakuseiWindowEvent();
  }
}
