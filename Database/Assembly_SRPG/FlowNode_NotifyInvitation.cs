namespace SRPG
{
    using System;
    using UnityEngine;

    [NodeType("System/Notify/Invitation", 0x7fe5), Pin(100, "通知監視開始", 0, 100), Pin(0xbf, "通知チェック", 1, 190), Pin(120, "通知監視リセット", 0, 120), Pin(110, "通知監視停止", 0, 110)]
    public class FlowNode_NotifyInvitation : FlowNodePersistent
    {
        public const int INPUT_NOTIFYMONITOR_START = 100;
        public const int INPUT_NOTIFYMONITOR_STOP = 110;
        public const int INPUT_NOTIFYMONITOR_RESET = 120;
        public const int OUTPUT_NOTIFY_GO = 0xbf;
        public float m_Interval;
        private bool m_Monitor;
        private float m_Time;

        public FlowNode_NotifyInvitation()
        {
            this.m_Interval = 10f;
            base..ctor();
            return;
        }

        protected override void Awake()
        {
            base.Awake();
            return;
        }

        private bool CanNotifyGo()
        {
            BattleCore core;
            if ((HomeWindow.Current == null) == null)
            {
                goto Label_0022;
            }
            if ((SceneBattle.Instance == null) == null)
            {
                goto Label_0022;
            }
            return 0;
        Label_0022:
            if ((HomeWindow.Current != null) == null)
            {
                goto Label_0043;
            }
            if (HomeWindow.Current.IsSceneChanging == null)
            {
                goto Label_0043;
            }
            return 0;
        Label_0043:
            if ((SceneBattle.Instance != null) == null)
            {
                goto Label_0076;
            }
            core = SceneBattle.Instance.Battle;
            if (core == null)
            {
                goto Label_0076;
            }
            if (string.IsNullOrEmpty(core.QuestID) == null)
            {
                goto Label_0076;
            }
            return 0;
        Label_0076:
            return 1;
        }

        public override void OnActivate(int pinID)
        {
            if (pinID != 100)
            {
                goto Label_002D;
            }
            MultiInvitationBadge.isValid = 0;
            this.m_Monitor = 1;
            this.m_Time = this.m_Interval;
            base.set_enabled(1);
            goto Label_0066;
        Label_002D:
            if (pinID != 110)
            {
                goto Label_0053;
            }
            this.m_Monitor = 0;
            this.m_Time = 0f;
            base.set_enabled(0);
            goto Label_0066;
        Label_0053:
            if (pinID != 120)
            {
                goto Label_0066;
            }
            this.m_Time = 0f;
        Label_0066:
            return;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            return;
        }

        private void Start()
        {
        }

        private void Update()
        {
            if (NotifyList.hasInstance != null)
            {
                goto Label_000B;
            }
            return;
        Label_000B:
            if (this.m_Monitor == null)
            {
                goto Label_007B;
            }
            if (this.m_Interval >= 10f)
            {
                goto Label_0031;
            }
            this.m_Interval = 10f;
        Label_0031:
            this.m_Time += Time.get_deltaTime();
            if (this.m_Time <= this.m_Interval)
            {
                goto Label_0086;
            }
            if (this.CanNotifyGo() == null)
            {
                goto Label_0086;
            }
            this.m_Time = 0f;
            base.ActivateOutputLinks(0xbf);
            goto Label_0086;
        Label_007B:
            this.m_Time = 0f;
        Label_0086:
            return;
        }
    }
}

