namespace SRPG
{
    using GR;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    [Pin(8, "現階層のミッションの進捗取得する？", 0, 8), Pin(9, "ミッションの進捗の取得が必要", 1, 9), Pin(0, "初期化", 0, 0), Pin(7, "初期化（完了）", 1, 1), Pin(1, "更新", 0, 2), Pin(2, "ユニット回復 処理へ", 1, 3), Pin(12, "ミッションの進捗の取得した（完了）", 1, 12), Pin(3, "ユニット回復（幻晶石不足）", 1, 4), Pin(11, "ミッションの進捗の取得した", 0, 11), Pin(10, "ミッションの進捗の取得は不要", 1, 10), Pin(4, "一階から再挑戦 処理へ", 1, 5), Pin(5, "一階から再挑戦（幻晶石不足）", 1, 6), Pin(6, "一階から再挑戦（完了）", 1, 7)]
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
        [HeaderBar("▼階層表示用のリスト"), SerializeField]
        private TowerQuestList mTowerQuestList;
        [HeaderBar("▼階層の詳細表示用オブジェクト"), SerializeField]
        private TowerQuestInfo mTowerQuestInfo;
        [SerializeField, HeaderBar("▼生存ユニット")]
        private Text AliveUnits;
        [SerializeField, HeaderBar("▼無料ユニット回復までの時間表示用の親")]
        private GameObject RecoverTimer;
        [SerializeField]
        private Text RecoverFreeTime;
        [SerializeField]
        private Text RecoverCost;
        [SerializeField]
        private Text RecoverCostFree;
        [SerializeField]
        private Button RecoverButton;
        [HeaderBar("▼無料ユニット回復までの時間表示用"), SerializeField]
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
        [SerializeField, HeaderBar("▼ボタン類")]
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
        [HeaderBar("▼１階から再挑戦"), SerializeField]
        private Text ResetText;
        [SerializeField]
        private Text ResetTextFree;
        [SerializeField, HeaderBar("▼制御用")]
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
        private MissionProgressRequestState m_MissionProgressState;

        public TowerManager()
        {
            this.mRefreshInterval = 1f;
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private void <OnClick_Reset>m__421(GameObject go)
        {
            TowerFloorParam param;
            this.RecoverButton.set_interactable(0);
            this.ResetButton.set_interactable(0);
            this.mConfirmBox = null;
            param = MonoSingleton<GameManager>.Instance.FindFirstTowerFloor(GlobalVars.SelectedTowerID);
            this.UpdateMissionProgressRequestState(param);
            FlowNode_GameObject.ActivateOutputLinks(this, 4);
            this.SetCanvasGroupIntaractable(1);
            this.is_reset = 1;
            return;
        }

        [CompilerGenerated]
        private void <OnClick_Reset>m__422(GameObject go)
        {
            this.mConfirmBox = null;
            this.SetCanvasGroupIntaractable(1);
            return;
        }

        public void Activated(int pinID)
        {
            TowerFloorParam param;
            if (pinID != null)
            {
                goto Label_0018;
            }
            this.Initialize();
            FlowNode_GameObject.ActivateOutputLinks(this, 7);
            goto Label_0090;
        Label_0018:
            if (pinID != 1)
            {
                goto Label_002A;
            }
            this.RefreshUI();
            goto Label_0090;
        Label_002A:
            if (pinID != 8)
            {
                goto Label_0079;
            }
            if (this.m_MissionProgressState != null)
            {
                goto Label_0053;
            }
            param = MonoSingleton<GameManager>.Instance.FindFirstTowerFloor(GlobalVars.SelectedTowerID);
            this.UpdateMissionProgressRequestState(param);
        Label_0053:
            if (this.m_MissionProgressState != 1)
            {
                goto Label_006C;
            }
            FlowNode_GameObject.ActivateOutputLinks(this, 9);
            goto Label_0074;
        Label_006C:
            FlowNode_GameObject.ActivateOutputLinks(this, 10);
        Label_0074:
            goto Label_0090;
        Label_0079:
            if (pinID != 11)
            {
                goto Label_0090;
            }
            this.m_MissionProgressState = 3;
            FlowNode_GameObject.ActivateOutputLinks(this, 12);
        Label_0090:
            return;
        }

        private void AddClickListener(Button button, Action clickListener)
        {
            if ((button == null) == null)
            {
                goto Label_000D;
            }
            return;
        Label_000D:
            button.get_onClick().AddListener(new UnityAction(clickListener, this.Invoke));
            return;
        }

        [DebuggerHidden]
        private IEnumerator CheckLoadIcon()
        {
            <CheckLoadIcon>c__Iterator13E iteratore;
            iteratore = new <CheckLoadIcon>c__Iterator13E();
            iteratore.<>f__this = this;
            return iteratore;
        }

        private void Initialize()
        {
            TowerFloorParam param;
            this.AddClickListener(this.RecoverButton, new Action(this.OnClick_RecoverButton));
            this.AddClickListener(this.DetailButton, new Action(this.OnClick_Detail));
            this.AddClickListener(this.ChallengeButton, new Action(this.OnClick_Challenge));
            this.AddClickListener(this.CurrentButton, new Action(this.OnClick_Current));
            this.AddClickListener(this.ResetButton, new Action(this.OnClick_Reset));
            this.mScrollAutoFit.OnScrollBegin.AddListener(new UnityAction(this, this.OnScrollBegin));
            this.mTowerParam = MonoSingleton<GameManager>.Instance.FindTower(GlobalVars.SelectedTowerID);
            this.mRecoverTime = MonoSingleton<GameManager>.Instance.TowerResuponse.rtime;
            if (this.mTowerParam == null)
            {
                goto Label_00F0;
            }
            if ((this.RankingButton != null) == null)
            {
                goto Label_00F0;
            }
            this.RankingButton.SetActive(this.mTowerParam.is_view_ranking);
        Label_00F0:
            if (this.mTowerParam == null)
            {
                goto Label_0122;
            }
            if ((this.StatusButton != null) == null)
            {
                goto Label_0122;
            }
            this.StatusButton.SetActive(this.mTowerParam.is_view_ranking);
        Label_0122:
            if (this.mTowerParam == null)
            {
                goto Label_0157;
            }
            if (string.IsNullOrEmpty(this.mTowerParam.eventURL) != null)
            {
                goto Label_0157;
            }
            FlowNode_Variable.Set("CAPTION_TOWER_EVENT_DETAIL", this.mTowerParam.eventURL);
        Label_0157:
            this.initialized = 1;
            if (this.is_reset == null)
            {
                goto Label_01AA;
            }
            param = MonoSingleton<GameManager>.Instance.FindFirstTowerFloor(GlobalVars.SelectedTowerID);
            this.mTowerQuestList.ScrollToCurrentFloor(param);
            this.RefreshUI();
            this.mTowerQuestInfo.Refresh();
            this.is_reset = 0;
            base.StartCoroutine(this.CheckLoadIcon());
        Label_01AA:
            MonoSingleton<GameManager>.Instance.Player.UpdateTowerTrophyStates();
            return;
        }

        private void OnClick_Challenge()
        {
        }

        private void OnClick_Current()
        {
            this.mTowerQuestList.ScrollToCurrentFloor();
            return;
        }

        private void OnClick_Detail()
        {
        }

        private void OnClick_RecoverButton()
        {
            object[] objArray1;
            string str;
            <OnClick_RecoverButton>c__AnonStorey3AB storeyab;
            storeyab = new <OnClick_RecoverButton>c__AnonStorey3AB();
            storeyab.<>f__this = this;
            if ((this.mConfirmBox != null) == null)
            {
                goto Label_001F;
            }
            return;
        Label_001F:
            storeyab.cost = MonoSingleton<GameManager>.Instance.TowerResuponse.CalcRecoverCost();
            if (MonoSingleton<GameManager>.Instance.Player.Coin >= storeyab.cost)
            {
                goto Label_005A;
            }
            FlowNode_GameObject.ActivateOutputLinks(this, 3);
            goto Label_00AE;
        Label_005A:
            this.SetCanvasGroupIntaractable(0);
            objArray1 = new object[] { (int) storeyab.cost };
            str = LocalizedText.Get("sys.MSG_TOWER_RECOVER", objArray1);
            this.mConfirmBox = UIUtility.ConfirmBoxTitle(string.Empty, str, new UIUtility.DialogResultEvent(storeyab.<>m__41F), new UIUtility.DialogResultEvent(storeyab.<>m__420), null, 1, -1, null, null);
        Label_00AE:
            return;
        }

        private void OnClick_Reset()
        {
            object[] objArray1;
            int num;
            string str;
            if ((this.mConfirmBox != null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            num = MonoSingleton<GameManager>.Instance.TowerResuponse.reset_cost;
            if (MonoSingleton<GameManager>.Instance.Player.Coin >= num)
            {
                goto Label_0043;
            }
            FlowNode_GameObject.ActivateOutputLinks(this, 5);
            goto Label_008D;
        Label_0043:
            this.SetCanvasGroupIntaractable(0);
            objArray1 = new object[] { (int) num };
            str = LocalizedText.Get("sys.TOWER_RESET_CHECK", objArray1);
            this.mConfirmBox = UIUtility.ConfirmBox(str, new UIUtility.DialogResultEvent(this.<OnClick_Reset>m__421), new UIUtility.DialogResultEvent(this.<OnClick_Reset>m__422), null, 1, -1, null, null);
        Label_008D:
            return;
        }

        private void OnScrollBegin()
        {
            if ((this.ChallengeButton != null) == null)
            {
                goto Label_001D;
            }
            this.ChallengeButton.set_interactable(0);
        Label_001D:
            if ((this.MissionButton != null) == null)
            {
                goto Label_003A;
            }
            this.MissionButton.set_interactable(0);
        Label_003A:
            return;
        }

        private unsafe void RefreshUI()
        {
            QuestParam param;
            TowerResuponse resuponse;
            List<UnitData> list;
            bool flag;
            TowerResuponse resuponse2;
            bool flag2;
            param = MonoSingleton<GameManager>.Instance.FindQuest(GlobalVars.SelectedQuestID);
            this.mRecoverTime = MonoSingleton<GameManager>.Instance.TowerResuponse.rtime;
            if ((param == null) || ((this.ChallengeButton != null) == null))
            {
                goto Label_0082;
            }
            DataSource.Bind<QuestParam>(this.ChallengeButton.get_gameObject(), param);
            this.ChallengeButton.set_interactable((param.IsQuestCondition() == null) ? 0 : ((param.state == 2) == 0));
            GameParameter.UpdateAll(this.ChallengeButton.get_gameObject());
        Label_0082:
            if ((this.RecoverButton != null) == null)
            {
                goto Label_0103;
            }
            resuponse = MonoSingleton<GameManager>.Instance.TowerResuponse;
            flag = ((resuponse.GetAvailableUnits().Count > 0) && (resuponse.ExistDamagedUnit() != null)) ? 1 : (resuponse.GetDiedUnitNum() > 0);
            this.RecoverButton.set_interactable((flag == null) ? 0 : (resuponse.is_reset == 0));
            if ((this.RecoverTimer != null) == null)
            {
                goto Label_0103;
            }
            this.RecoverTimer.SetActive(flag);
        Label_0103:
            if ((this.ResetButton != null) == null)
            {
                goto Label_01AD;
            }
            resuponse2 = MonoSingleton<GameManager>.Instance.TowerResuponse;
            this.ResetButton.get_gameObject().SetActive(resuponse2.is_reset);
            this.ResetButton.set_interactable(resuponse2.is_reset);
            this.ChallengeButton.get_gameObject().SetActive(resuponse2.is_reset == 0);
            this.ResetText.set_text(&resuponse2.reset_cost.ToString());
            flag2 = resuponse2.reset_cost == 0;
            this.ResetText.get_gameObject().SetActive(flag2 == 0);
            this.ResetTextFree.get_gameObject().SetActive(flag2);
        Label_01AD:
            if ((this.MissionButton != null) == null)
            {
                goto Label_01CA;
            }
            this.MissionButton.set_interactable(1);
        Label_01CA:
            this.SetAliveUnitsText();
            this.SetRecoverText();
            return;
        }

        private void RemoveClickListener(Button button, Action clickListener)
        {
            if ((button == null) == null)
            {
                goto Label_000D;
            }
            return;
        Label_000D:
            button.get_onClick().RemoveListener(new UnityAction(clickListener, this.Invoke));
            return;
        }

        private void SetAliveUnitsText()
        {
            TowerResuponse resuponse;
            List<UnitData> list;
            int num;
            string str;
            if ((this.AliveUnits == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            resuponse = MonoSingleton<GameManager>.Instance.TowerResuponse;
            list = resuponse.GetAvailableUnits();
            num = resuponse.GetDiedUnitNum();
            str = "{0}/{1}";
            this.AliveUnits.set_text(string.Format(str, (int) Mathf.Max(0, list.Count - num), (int) list.Count));
            return;
        }

        private void SetCanvasGroupIntaractable(bool value)
        {
            if ((this.mCanvasGroup != null) == null)
            {
                goto Label_001D;
            }
            this.mCanvasGroup.set_interactable(value);
        Label_001D:
            return;
        }

        private unsafe void SetRecoverText()
        {
            int num;
            bool flag;
            DateTime time;
            DateTime time2;
            TimeSpan span;
            int num2;
            int num3;
            int num4;
            DateTime time3;
            num = MonoSingleton<GameManager>.Instance.TowerResuponse.CalcRecoverCost();
            if ((this.RecoverCost != null) == null)
            {
                goto Label_0064;
            }
            flag = num == 0;
            this.RecoverCost.get_gameObject().SetActive(flag == 0);
            this.RecoverCostFree.get_gameObject().SetActive(flag);
            if (num <= 0)
            {
                goto Label_0064;
            }
            this.RecoverCost.set_text(&num.ToString());
        Label_0064:
            if ((this.RecoverTimer != null) == null)
            {
                goto Label_00FB;
            }
            time = TimeManager.ServerTime;
            span = &TimeManager.FromUnixTime(this.mRecoverTime).AddMinutes(-1.0) - time;
            if (&span.TotalMinutes >= 0.0)
            {
                goto Label_00C8;
            }
            this.RecoverTimer.SetActive(0);
            goto Label_00FB;
        Label_00C8:
            this.RecoverTimer.SetActive(1);
            num2 = &span.Hours;
            num3 = &span.Minutes;
            num4 = &span.Seconds;
            this.UpdateTimer(num2, num3, num4);
        Label_00FB:
            return;
        }

        private void Update()
        {
            if (this.initialized != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            this.mRefreshInterval -= Time.get_unscaledDeltaTime();
            if (this.mRefreshInterval > 0f)
            {
                goto Label_003F;
            }
            this.SetRecoverText();
            this.mRefreshInterval = 1f;
        Label_003F:
            return;
        }

        private void UpdateMissionProgressRequestState(TowerFloorParam floorParam)
        {
            if (string.IsNullOrEmpty(floorParam.mission) != null)
            {
                goto Label_001C;
            }
            this.m_MissionProgressState = 1;
            goto Label_0023;
        Label_001C:
            this.m_MissionProgressState = 2;
        Label_0023:
            return;
        }

        private void UpdateTimer(int hours, int minutes, int seconds)
        {
            int num;
            int num2;
            int num3;
            int num4;
            int num5;
            int num6;
            if (hours >= 0)
            {
                goto Label_000A;
            }
            hours = 0;
        Label_000A:
            if (minutes >= 0)
            {
                goto Label_0014;
            }
            minutes = 0;
        Label_0014:
            if (seconds >= 0)
            {
                goto Label_001E;
            }
            seconds = 0;
        Label_001E:
            hours = hours % 60;
            minutes = minutes % 60;
            seconds = seconds % 60;
            num = hours / 10;
            num2 = hours % 10;
            num3 = minutes / 10;
            num4 = minutes % 10;
            num5 = seconds / 10;
            num6 = seconds % 10;
            this.TimerH10.ImageIndex = (num >= 10) ? 0 : num;
            this.TimerH1.ImageIndex = (num2 >= 10) ? 0 : num2;
            this.TimerM10.ImageIndex = (num3 >= 10) ? 0 : num3;
            this.TimerM1.ImageIndex = (num4 >= 10) ? 0 : num4;
            this.TimerS10.ImageIndex = (num5 >= 10) ? 0 : num5;
            this.TimerS1.ImageIndex = (num6 >= 10) ? 0 : num6;
            return;
        }

        private void UpdateTimerText(int hours, int minutes, int seconds)
        {
            string str;
            str = string.Format("{0:00}:{1:00}:{2:00}", (int) hours, (int) minutes, (int) seconds);
            this.RecoverFreeTime.set_text(str);
            return;
        }

        [CompilerGenerated]
        private sealed class <CheckLoadIcon>c__Iterator13E : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal int $PC;
            internal object $current;
            internal TowerManager <>f__this;

            public <CheckLoadIcon>c__Iterator13E()
            {
                base..ctor();
                return;
            }

            [DebuggerHidden]
            public void Dispose()
            {
                this.$PC = -1;
                return;
            }

            public bool MoveNext()
            {
                uint num;
                bool flag;
                num = this.$PC;
                this.$PC = -1;
                switch (num)
                {
                    case 0:
                        goto Label_0021;

                    case 1:
                        goto Label_0039;
                }
                goto Label_0056;
            Label_0021:
                goto Label_0039;
            Label_0026:
                this.$current = null;
                this.$PC = 1;
                goto Label_0058;
            Label_0039:
                if (AssetManager.IsLoading != null)
                {
                    goto Label_0026;
                }
                FlowNode_GameObject.ActivateOutputLinks(this.<>f__this, 6);
                this.$PC = -1;
            Label_0056:
                return 0;
            Label_0058:
                return 1;
                return flag;
            }

            [DebuggerHidden]
            public void Reset()
            {
                throw new NotSupportedException();
            }

            object IEnumerator<object>.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.$current;
                }
            }

            object IEnumerator.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.$current;
                }
            }
        }

        [CompilerGenerated]
        private sealed class <OnClick_RecoverButton>c__AnonStorey3AB
        {
            internal int cost;
            internal TowerManager <>f__this;

            public <OnClick_RecoverButton>c__AnonStorey3AB()
            {
                base..ctor();
                return;
            }

            internal void <>m__41F(GameObject go)
            {
                this.<>f__this.RecoverButton.set_interactable(0);
                this.<>f__this.mConfirmBox = null;
                this.cost = MonoSingleton<GameManager>.Instance.TowerResuponse.CalcRecoverCost();
                DataSource.Bind<TowerRecoverData>(this.<>f__this.get_gameObject(), new TowerRecoverData(GlobalVars.SelectedTowerID, this.cost));
                FlowNode_GameObject.ActivateOutputLinks(this.<>f__this, 2);
                this.<>f__this.SetCanvasGroupIntaractable(1);
                return;
            }

            internal void <>m__420(GameObject go)
            {
                this.<>f__this.mConfirmBox = null;
                this.<>f__this.SetCanvasGroupIntaractable(1);
                return;
            }
        }

        private enum MissionProgressRequestState
        {
            NotInitialized,
            RequireProgressRequest,
            UnrequireProgressRequest,
            ReceivedProgress
        }
    }
}

