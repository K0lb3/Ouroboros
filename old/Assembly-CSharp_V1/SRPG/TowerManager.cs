// Decompiled with JetBrains decompiler
// Type: SRPG.TowerManager
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
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
  [FlowNode.Pin(6, "リセット完了", FlowNode.PinTypes.Output, 6)]
  [FlowNode.Pin(4, "リセット", FlowNode.PinTypes.Output, 4)]
  [FlowNode.Pin(5, "リセット 幻晶石不足", FlowNode.PinTypes.Output, 5)]
  [FlowNode.Pin(0, "初期化", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "更新", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "回復", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(3, "回復 幻晶石不足", FlowNode.PinTypes.Output, 3)]
  public class TowerManager : MonoBehaviour, IFlowInterface
  {
    private const int PIN_ID_INITIALIZE = 0;
    private const int PIN_ID_REFRESH = 1;
    private const int PIN_ID_RECOVER_UNIT = 2;
    private const int PIN_ID_RECOVER_COIN_NOT_ENOUGH = 3;
    private const int PIN_ID_RESET_UNIT = 4;
    private const int PIN_ID_RESET_COIN_NOT_ENOUGH = 5;
    private const int PIN_ID_RESET_END = 6;
    private const string VARIABLE_KEY_EVENT_URL = "CAPTION_TOWER_EVENT_DETAIL";
    [SerializeField]
    private GameObject RecoverTimer;
    [SerializeField]
    private Text RecoverFreeTime;
    [SerializeField]
    private Text RecoverCost;
    [SerializeField]
    private Text RecoverCostFree;
    [SerializeField]
    private Button RecoverButton;
    [SerializeField]
    private Text AliveUnits;
    [SerializeField]
    private Button DetailButton;
    [SerializeField]
    private Button CurrentButton;
    [SerializeField]
    private Button ChallengeButton;
    [SerializeField]
    private Button ResetButton;
    [SerializeField]
    private ImageArray TimerH10;
    [SerializeField]
    private ImageArray TimerH1;
    [SerializeField]
    private ImageArray TimerM10;
    [SerializeField]
    private ImageArray TimerM1;
    [SerializeField]
    private ImageArray TimerS10;
    [SerializeField]
    private ImageArray TimerS1;
    [SerializeField]
    private TowerQuestList mTowerQuestList;
    [SerializeField]
    private TowerQuestInfo mTowerQuestInfo;
    [SerializeField]
    private ScrollAutoFit mScrollAutoFit;
    [SerializeField]
    private CanvasGroup mCanvasGroup;
    [SerializeField]
    private Text ResetText;
    [SerializeField]
    private Text ResetTextFree;
    [SerializeField]
    private GameObject RankingButton;
    [SerializeField]
    private GameObject StatusButton;
    private TowerParam mTowerParam;
    private long mRecoverTime;
    private float mRefreshInterval;
    private bool initialized;
    private GameObject mConfirmBox;
    private bool is_reset;

    public TowerManager()
    {
      base.\u002Ector();
    }

    private void Initialize()
    {
      this.AddClickListener(this.RecoverButton, new Action(this.OnClick_RecoverButton));
      this.AddClickListener(this.DetailButton, new Action(this.OnClick_Detail));
      this.AddClickListener(this.ChallengeButton, new Action(this.OnClick_Challenge));
      this.AddClickListener(this.CurrentButton, new Action(this.OnClick_Current));
      this.AddClickListener(this.ResetButton, new Action(this.OnClick_Reset));
      // ISSUE: method pointer
      this.mScrollAutoFit.OnScrollBegin.AddListener(new UnityAction((object) this, __methodptr(OnScrollBegin)));
      this.mTowerParam = MonoSingleton<GameManager>.Instance.FindTower(GlobalVars.SelectedTowerID);
      this.mRecoverTime = MonoSingleton<GameManager>.Instance.TowerResuponse.rtime;
      if (this.mTowerParam != null && Object.op_Inequality((Object) this.RankingButton, (Object) null))
        this.RankingButton.SetActive(this.mTowerParam.is_view_ranking);
      if (this.mTowerParam != null && Object.op_Inequality((Object) this.StatusButton, (Object) null))
        this.StatusButton.SetActive(this.mTowerParam.is_view_ranking);
      if (this.mTowerParam != null && !string.IsNullOrEmpty(this.mTowerParam.eventURL))
        FlowNode_Variable.Set("CAPTION_TOWER_EVENT_DETAIL", this.mTowerParam.eventURL);
      this.initialized = true;
      if (!this.is_reset)
        return;
      this.mTowerQuestList.ScrollToCurrentFloor(MonoSingleton<GameManager>.Instance.FindFirstTowerFloor(GlobalVars.SelectedTowerID));
      this.RefreshUI();
      this.mTowerQuestInfo.Refresh();
      this.is_reset = false;
      this.StartCoroutine(this.CheckLoadIcon());
    }

    [DebuggerHidden]
    private IEnumerator CheckLoadIcon()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new TowerManager.\u003CCheckLoadIcon\u003Ec__IteratorD3() { \u003C\u003Ef__this = this };
    }

    private void OnScrollBegin()
    {
      ((Selectable) this.ChallengeButton).set_interactable(false);
    }

    private void AddClickListener(Button button, Action clickListener)
    {
      if (Object.op_Equality((Object) button, (Object) null))
        return;
      // ISSUE: method pointer
      ((UnityEvent) button.get_onClick()).AddListener(new UnityAction((object) clickListener, __methodptr(Invoke)));
    }

    private void RemoveClickListener(Button button, Action clickListener)
    {
      if (Object.op_Equality((Object) button, (Object) null))
        return;
      // ISSUE: method pointer
      ((UnityEvent) button.get_onClick()).RemoveListener(new UnityAction((object) clickListener, __methodptr(Invoke)));
    }

    private void OnClick_RecoverButton()
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      TowerManager.\u003COnClick_RecoverButton\u003Ec__AnonStorey272 buttonCAnonStorey272 = new TowerManager.\u003COnClick_RecoverButton\u003Ec__AnonStorey272();
      // ISSUE: reference to a compiler-generated field
      buttonCAnonStorey272.\u003C\u003Ef__this = this;
      if (Object.op_Inequality((Object) this.mConfirmBox, (Object) null))
        return;
      // ISSUE: reference to a compiler-generated field
      buttonCAnonStorey272.cost = MonoSingleton<GameManager>.Instance.TowerResuponse.CalcRecoverCost();
      // ISSUE: reference to a compiler-generated field
      if (MonoSingleton<GameManager>.Instance.Player.Coin < buttonCAnonStorey272.cost)
      {
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 3);
      }
      else
      {
        this.SetCanvasGroupIntaractable(false);
        // ISSUE: reference to a compiler-generated field
        string text = LocalizedText.Get("sys.MSG_TOWER_RECOVER", new object[1]{ (object) buttonCAnonStorey272.cost });
        // ISSUE: reference to a compiler-generated method
        // ISSUE: reference to a compiler-generated method
        this.mConfirmBox = UIUtility.ConfirmBoxTitle(string.Empty, text, new UIUtility.DialogResultEvent(buttonCAnonStorey272.\u003C\u003Em__2EB), new UIUtility.DialogResultEvent(buttonCAnonStorey272.\u003C\u003Em__2EC), (GameObject) null, true, -1);
      }
    }

    private void SetCanvasGroupIntaractable(bool value)
    {
      if (!Object.op_Inequality((Object) this.mCanvasGroup, (Object) null))
        return;
      this.mCanvasGroup.set_interactable(value);
    }

    private void OnClick_Detail()
    {
    }

    private void OnClick_Challenge()
    {
    }

    private void OnClick_Current()
    {
      this.mTowerQuestList.ScrollToCurrentFloor();
    }

    private void OnClick_Reset()
    {
      if (Object.op_Inequality((Object) this.mConfirmBox, (Object) null))
        return;
      int resetCost = (int) MonoSingleton<GameManager>.Instance.TowerResuponse.reset_cost;
      if (MonoSingleton<GameManager>.Instance.Player.Coin < resetCost)
      {
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 5);
      }
      else
      {
        this.SetCanvasGroupIntaractable(false);
        this.mConfirmBox = UIUtility.ConfirmBox(LocalizedText.Get("sys.TOWER_RESET_CHECK", new object[1]
        {
          (object) resetCost
        }), (UIUtility.DialogResultEvent) (go =>
        {
          ((Selectable) this.RecoverButton).set_interactable(false);
          this.mConfirmBox = (GameObject) null;
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 4);
          this.SetCanvasGroupIntaractable(true);
          this.is_reset = true;
        }), (UIUtility.DialogResultEvent) (go =>
        {
          this.mConfirmBox = (GameObject) null;
          this.SetCanvasGroupIntaractable(true);
        }), (GameObject) null, true, -1);
      }
    }

    public void Activated(int pinID)
    {
      if (pinID == 0)
      {
        this.Initialize();
      }
      else
      {
        if (pinID != 1)
          return;
        this.RefreshUI();
      }
    }

    private void Update()
    {
      if (!this.initialized)
        return;
      this.mRefreshInterval -= Time.get_unscaledDeltaTime();
      if ((double) this.mRefreshInterval > 0.0)
        return;
      this.SetRecoverText();
      this.mRefreshInterval = 1f;
    }

    private void RefreshUI()
    {
      QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(GlobalVars.SelectedQuestID);
      this.mRecoverTime = MonoSingleton<GameManager>.Instance.TowerResuponse.rtime;
      if (quest != null && Object.op_Inequality((Object) this.ChallengeButton, (Object) null))
      {
        DataSource.Bind<QuestParam>(((Component) this.ChallengeButton).get_gameObject(), quest);
        ((Selectable) this.ChallengeButton).set_interactable(quest.IsQuestCondition() && quest.state != QuestStates.Cleared);
        GameParameter.UpdateAll(((Component) this.ChallengeButton).get_gameObject());
      }
      if (Object.op_Inequality((Object) this.RecoverButton, (Object) null))
      {
        TowerResuponse towerResuponse = MonoSingleton<GameManager>.Instance.TowerResuponse;
        bool flag = towerResuponse.GetAvailableUnits().Count > 0 && towerResuponse.ExistDamagedUnit() || towerResuponse.GetDiedUnitNum() > 0;
        ((Selectable) this.RecoverButton).set_interactable(flag && !towerResuponse.is_reset);
        if (Object.op_Inequality((Object) this.RecoverTimer, (Object) null))
          this.RecoverTimer.SetActive(flag);
      }
      if (Object.op_Inequality((Object) this.ResetButton, (Object) null))
      {
        TowerResuponse towerResuponse = MonoSingleton<GameManager>.Instance.TowerResuponse;
        ((Component) this.ResetButton).get_gameObject().SetActive(towerResuponse.is_reset);
        ((Component) this.ChallengeButton).get_gameObject().SetActive(!towerResuponse.is_reset);
        this.ResetText.set_text(towerResuponse.reset_cost.ToString());
        bool flag = (int) towerResuponse.reset_cost == 0;
        ((Component) this.ResetText).get_gameObject().SetActive(!flag);
        ((Component) this.ResetTextFree).get_gameObject().SetActive(flag);
      }
      this.SetAliveUnitsText();
      this.SetRecoverText();
    }

    private void SetAliveUnitsText()
    {
      if (Object.op_Equality((Object) this.AliveUnits, (Object) null))
        return;
      TowerResuponse towerResuponse = MonoSingleton<GameManager>.Instance.TowerResuponse;
      List<UnitData> availableUnits = towerResuponse.GetAvailableUnits();
      int diedUnitNum = towerResuponse.GetDiedUnitNum();
      this.AliveUnits.set_text(string.Format("{0}/{1}", (object) Mathf.Max(0, availableUnits.Count - diedUnitNum), (object) availableUnits.Count));
    }

    private void SetRecoverText()
    {
      int num = MonoSingleton<GameManager>.Instance.TowerResuponse.CalcRecoverCost();
      if (Object.op_Inequality((Object) this.RecoverCost, (Object) null))
      {
        bool flag = num == 0;
        ((Component) this.RecoverCost).get_gameObject().SetActive(!flag);
        ((Component) this.RecoverCostFree).get_gameObject().SetActive(flag);
        if (num > 0)
          this.RecoverCost.set_text(num.ToString());
      }
      if (!Object.op_Inequality((Object) this.RecoverTimer, (Object) null))
        return;
      TimeSpan timeSpan = TimeManager.FromUnixTime(this.mRecoverTime).AddMinutes(-1.0) - TimeManager.ServerTime;
      if (timeSpan.TotalMinutes < 0.0)
      {
        this.RecoverTimer.SetActive(false);
      }
      else
      {
        this.RecoverTimer.SetActive(true);
        this.UpdateTimer(timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
      }
    }

    private void UpdateTimerText(int hours, int minutes, int seconds)
    {
      this.RecoverFreeTime.set_text(string.Format("{0:00}:{1:00}:{2:00}", (object) hours, (object) minutes, (object) seconds));
    }

    private void UpdateTimer(int hours, int minutes, int seconds)
    {
      if (hours < 0)
        hours = 0;
      if (minutes < 0)
        minutes = 0;
      if (seconds < 0)
        seconds = 0;
      hours %= 60;
      minutes %= 60;
      seconds %= 60;
      int num1 = hours / 10;
      int num2 = hours % 10;
      int num3 = minutes / 10;
      int num4 = minutes % 10;
      int num5 = seconds / 10;
      int num6 = seconds % 10;
      this.TimerH10.ImageIndex = num1 >= 10 ? 0 : num1;
      this.TimerH1.ImageIndex = num2 >= 10 ? 0 : num2;
      this.TimerM10.ImageIndex = num3 >= 10 ? 0 : num3;
      this.TimerM1.ImageIndex = num4 >= 10 ? 0 : num4;
      this.TimerS10.ImageIndex = num5 >= 10 ? 0 : num5;
      this.TimerS1.ImageIndex = num6 >= 10 ? 0 : num6;
    }
  }
}
