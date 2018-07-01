// Decompiled with JetBrains decompiler
// Type: SRPG.TowerManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

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
  [FlowNode.Pin(8, "現階層のミッションの進捗取得する？", FlowNode.PinTypes.Input, 8)]
  [FlowNode.Pin(9, "ミッションの進捗の取得が必要", FlowNode.PinTypes.Output, 9)]
  [FlowNode.Pin(0, "初期化", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(7, "初期化（完了）", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(1, "更新", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(2, "ユニット回復 処理へ", FlowNode.PinTypes.Output, 3)]
  [FlowNode.Pin(12, "ミッションの進捗の取得した（完了）", FlowNode.PinTypes.Output, 12)]
  [FlowNode.Pin(3, "ユニット回復（幻晶石不足）", FlowNode.PinTypes.Output, 4)]
  [FlowNode.Pin(11, "ミッションの進捗の取得した", FlowNode.PinTypes.Input, 11)]
  [FlowNode.Pin(10, "ミッションの進捗の取得は不要", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(4, "一階から再挑戦 処理へ", FlowNode.PinTypes.Output, 5)]
  [FlowNode.Pin(5, "一階から再挑戦（幻晶石不足）", FlowNode.PinTypes.Output, 6)]
  [FlowNode.Pin(6, "一階から再挑戦（完了）", FlowNode.PinTypes.Output, 7)]
  public class TowerManager : MonoBehaviour, IFlowInterface
  {
    private const int PIN_ID_INITIALIZE = 0;
    private const int PIN_ID_REFRESH = 1;
    private const int PIN_ID_RECOVER_UNIT = 2;
    private const int PIN_ID_RECOVER_COIN_NOT_ENOUGH = 3;
    private const int PIN_ID_RESET_TOWER = 4;
    private const int PIN_ID_RESET_TOWER_COIN_NOT_ENOUGH = 5;
    private const int PIN_ID_RESET_TOWER_END = 6;
    private const int PIN_ID_INITIALIZE_END = 7;
    private const int PIN_ID_CHECK_REQUIRED_PROGRESS_REQUEST = 8;
    private const int PIN_ID_REQUIRED_PROGRESS_REQUEST = 9;
    private const int PIN_ID_UNREQUIRE_PROGRESS_REQUEST = 10;
    private const int PIN_ID_SET_REQUIRE_PROGRESS_RECIEVED = 11;
    private const int PIN_ID_SET_REQUIRE_PROGRESS_RECIEVED_END = 12;
    private const string VARIABLE_KEY_EVENT_URL = "CAPTION_TOWER_EVENT_DETAIL";
    [HeaderBar("▼階層表示用のリスト")]
    [SerializeField]
    private TowerQuestList mTowerQuestList;
    [HeaderBar("▼階層の詳細表示用オブジェクト")]
    [SerializeField]
    private TowerQuestInfo mTowerQuestInfo;
    [SerializeField]
    [HeaderBar("▼生存ユニット")]
    private Text AliveUnits;
    [SerializeField]
    [HeaderBar("▼無料ユニット回復までの時間表示用の親")]
    private GameObject RecoverTimer;
    [SerializeField]
    private Text RecoverFreeTime;
    [SerializeField]
    private Text RecoverCost;
    [SerializeField]
    private Text RecoverCostFree;
    [SerializeField]
    private Button RecoverButton;
    [HeaderBar("▼無料ユニット回復までの時間表示用")]
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
    [HeaderBar("▼ボタン類")]
    private Button DetailButton;
    [SerializeField]
    private Button CurrentButton;
    [SerializeField]
    private Button ChallengeButton;
    [SerializeField]
    private Button ResetButton;
    [SerializeField]
    private Button MissionButton;
    [SerializeField]
    private GameObject RankingButton;
    [SerializeField]
    private GameObject StatusButton;
    [HeaderBar("▼１階から再挑戦")]
    [SerializeField]
    private Text ResetText;
    [SerializeField]
    private Text ResetTextFree;
    [SerializeField]
    [HeaderBar("▼制御用")]
    private CanvasGroup mCanvasGroup;
    [SerializeField]
    private ScrollAutoFit mScrollAutoFit;
    private TowerParam mTowerParam;
    private long mRecoverTime;
    private float mRefreshInterval;
    private bool initialized;
    private GameObject mConfirmBox;
    private bool is_reset;
    private bool m_IsRequireMissionProgressUpdate;
    private TowerManager.MissionProgressRequestState m_MissionProgressState;

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
      if (this.mTowerParam != null && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.RankingButton, (UnityEngine.Object) null))
        this.RankingButton.SetActive(this.mTowerParam.is_view_ranking);
      if (this.mTowerParam != null && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.StatusButton, (UnityEngine.Object) null))
        this.StatusButton.SetActive(this.mTowerParam.is_view_ranking);
      if (this.mTowerParam != null && !string.IsNullOrEmpty(this.mTowerParam.eventURL))
        FlowNode_Variable.Set("CAPTION_TOWER_EVENT_DETAIL", this.mTowerParam.eventURL);
      this.initialized = true;
      if (this.is_reset)
      {
        this.mTowerQuestList.ScrollToCurrentFloor(MonoSingleton<GameManager>.Instance.FindFirstTowerFloor(GlobalVars.SelectedTowerID));
        this.RefreshUI();
        this.mTowerQuestInfo.Refresh();
        this.is_reset = false;
        this.StartCoroutine(this.CheckLoadIcon());
      }
      MonoSingleton<GameManager>.Instance.Player.UpdateTowerTrophyStates();
    }

    [DebuggerHidden]
    private IEnumerator CheckLoadIcon()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new TowerManager.\u003CCheckLoadIcon\u003Ec__Iterator13E()
      {
        \u003C\u003Ef__this = this
      };
    }

    private void OnScrollBegin()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ChallengeButton, (UnityEngine.Object) null))
        ((Selectable) this.ChallengeButton).set_interactable(false);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.MissionButton, (UnityEngine.Object) null))
        return;
      ((Selectable) this.MissionButton).set_interactable(false);
    }

    private void AddClickListener(Button button, Action clickListener)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) button, (UnityEngine.Object) null))
        return;
      // ISSUE: method pointer
      ((UnityEvent) button.get_onClick()).AddListener(new UnityAction((object) clickListener, __methodptr(Invoke)));
    }

    private void RemoveClickListener(Button button, Action clickListener)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) button, (UnityEngine.Object) null))
        return;
      // ISSUE: method pointer
      ((UnityEvent) button.get_onClick()).RemoveListener(new UnityAction((object) clickListener, __methodptr(Invoke)));
    }

    private void OnClick_RecoverButton()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mConfirmBox, (UnityEngine.Object) null))
        return;
      int cost = MonoSingleton<GameManager>.Instance.TowerResuponse.CalcRecoverCost();
      if (MonoSingleton<GameManager>.Instance.Player.Coin < cost)
      {
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 3);
      }
      else
      {
        this.SetCanvasGroupIntaractable(false);
        string text = LocalizedText.Get("sys.MSG_TOWER_RECOVER", new object[1]
        {
          (object) cost
        });
        this.mConfirmBox = UIUtility.ConfirmBoxTitle(string.Empty, text, (UIUtility.DialogResultEvent) (go =>
        {
          ((Selectable) this.RecoverButton).set_interactable(false);
          this.mConfirmBox = (GameObject) null;
          cost = MonoSingleton<GameManager>.Instance.TowerResuponse.CalcRecoverCost();
          DataSource.Bind<TowerRecoverData>(((Component) this).get_gameObject(), new TowerRecoverData(GlobalVars.SelectedTowerID, cost));
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 2);
          this.SetCanvasGroupIntaractable(true);
        }), (UIUtility.DialogResultEvent) (go =>
        {
          this.mConfirmBox = (GameObject) null;
          this.SetCanvasGroupIntaractable(true);
        }), (GameObject) null, true, -1, (string) null, (string) null);
      }
    }

    private void SetCanvasGroupIntaractable(bool value)
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mCanvasGroup, (UnityEngine.Object) null))
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
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mConfirmBox, (UnityEngine.Object) null))
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
          ((Selectable) this.ResetButton).set_interactable(false);
          this.mConfirmBox = (GameObject) null;
          this.UpdateMissionProgressRequestState(MonoSingleton<GameManager>.Instance.FindFirstTowerFloor(GlobalVars.SelectedTowerID));
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 4);
          this.SetCanvasGroupIntaractable(true);
          this.is_reset = true;
        }), (UIUtility.DialogResultEvent) (go =>
        {
          this.mConfirmBox = (GameObject) null;
          this.SetCanvasGroupIntaractable(true);
        }), (GameObject) null, true, -1, (string) null, (string) null);
      }
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 0:
          this.Initialize();
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 7);
          break;
        case 1:
          this.RefreshUI();
          break;
        case 8:
          if (this.m_MissionProgressState == TowerManager.MissionProgressRequestState.NotInitialized)
            this.UpdateMissionProgressRequestState(MonoSingleton<GameManager>.Instance.FindFirstTowerFloor(GlobalVars.SelectedTowerID));
          if (this.m_MissionProgressState == TowerManager.MissionProgressRequestState.RequireProgressRequest)
          {
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 9);
            break;
          }
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 10);
          break;
        case 11:
          this.m_MissionProgressState = TowerManager.MissionProgressRequestState.ReceivedProgress;
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 12);
          break;
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
      if (quest != null && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ChallengeButton, (UnityEngine.Object) null))
      {
        DataSource.Bind<QuestParam>(((Component) this.ChallengeButton).get_gameObject(), quest);
        ((Selectable) this.ChallengeButton).set_interactable(quest.IsQuestCondition() && quest.state != QuestStates.Cleared);
        GameParameter.UpdateAll(((Component) this.ChallengeButton).get_gameObject());
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.RecoverButton, (UnityEngine.Object) null))
      {
        TowerResuponse towerResuponse = MonoSingleton<GameManager>.Instance.TowerResuponse;
        bool flag = towerResuponse.GetAvailableUnits().Count > 0 && towerResuponse.ExistDamagedUnit() || towerResuponse.GetDiedUnitNum() > 0;
        ((Selectable) this.RecoverButton).set_interactable(flag && !towerResuponse.is_reset);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.RecoverTimer, (UnityEngine.Object) null))
          this.RecoverTimer.SetActive(flag);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ResetButton, (UnityEngine.Object) null))
      {
        TowerResuponse towerResuponse = MonoSingleton<GameManager>.Instance.TowerResuponse;
        ((Component) this.ResetButton).get_gameObject().SetActive(towerResuponse.is_reset);
        ((Selectable) this.ResetButton).set_interactable(towerResuponse.is_reset);
        ((Component) this.ChallengeButton).get_gameObject().SetActive(!towerResuponse.is_reset);
        this.ResetText.set_text(towerResuponse.reset_cost.ToString());
        bool flag = towerResuponse.reset_cost == (short) 0;
        ((Component) this.ResetText).get_gameObject().SetActive(!flag);
        ((Component) this.ResetTextFree).get_gameObject().SetActive(flag);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.MissionButton, (UnityEngine.Object) null))
        ((Selectable) this.MissionButton).set_interactable(true);
      this.SetAliveUnitsText();
      this.SetRecoverText();
    }

    private void SetAliveUnitsText()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.AliveUnits, (UnityEngine.Object) null))
        return;
      TowerResuponse towerResuponse = MonoSingleton<GameManager>.Instance.TowerResuponse;
      List<UnitData> availableUnits = towerResuponse.GetAvailableUnits();
      int diedUnitNum = towerResuponse.GetDiedUnitNum();
      this.AliveUnits.set_text(string.Format("{0}/{1}", (object) Mathf.Max(0, availableUnits.Count - diedUnitNum), (object) availableUnits.Count));
    }

    private void SetRecoverText()
    {
      int num = MonoSingleton<GameManager>.Instance.TowerResuponse.CalcRecoverCost();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.RecoverCost, (UnityEngine.Object) null))
      {
        bool flag = num == 0;
        ((Component) this.RecoverCost).get_gameObject().SetActive(!flag);
        ((Component) this.RecoverCostFree).get_gameObject().SetActive(flag);
        if (num > 0)
          this.RecoverCost.set_text(num.ToString());
      }
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.RecoverTimer, (UnityEngine.Object) null))
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

    private void UpdateMissionProgressRequestState(TowerFloorParam floorParam)
    {
      if (!string.IsNullOrEmpty(floorParam.mission))
        this.m_MissionProgressState = TowerManager.MissionProgressRequestState.RequireProgressRequest;
      else
        this.m_MissionProgressState = TowerManager.MissionProgressRequestState.UnrequireProgressRequest;
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

    private enum MissionProgressRequestState
    {
      NotInitialized,
      RequireProgressRequest,
      UnrequireProgressRequest,
      ReceivedProgress,
    }
  }
}
