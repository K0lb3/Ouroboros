namespace SRPG
{
    using System;
    using System.Runtime.CompilerServices;

    [Pin(0x4b1, "クエスト敗北", 1, 0), Pin(0x10cc, "中断復帰開始通知", 1, 0), Pin(0xfc0, "他人切断通知強制終了", 1, 0), Pin(0x1005, "スタンプWindow終了通知", 0, 0), Pin(0x1004, "スタンプWindow開始通知", 0, 0), Pin(0x1195, "強制勝利ウィンドウ閉じ", 0, 0), Pin(0x1194, "強制勝利", 1, 0), Pin(0x1131, "同期待ち中終了", 1, 0), Pin(0x1130, "同期待ち中開始", 1, 0), Pin(0x10d0, "自身の復帰通知強制終了", 1, 0), Pin(0x10cf, "中断復帰通知強制終了", 1, 0), Pin(0x10ce, "自身の復帰完了通知", 1, 0), Pin(0x10cd, "中断復帰終了通知", 0, 0), Pin(0xfa1, "制限時間隠す", 1, 0), Pin(0xfa0, "制限時間表示", 1, 0), Pin(0x515, "マップ終了", 1, 0), Pin(0x514, "マップ開始", 1, 0), Pin(0x4b2, "クエスト中断", 1, 0), Pin(0xfbf, "他人切断通知終了", 0, 0), Pin(0xfbe, "他人切断通知開始", 1, 0), NodeType("Battle_MultiPlay/Events"), Pin(0x424, "自分ユニット行動開始", 1, 0), Pin(0x425, "他人ユニット行動開始", 1, 0), Pin(0x426, "敵ユニット行動開始", 1, 0), Pin(0x42e, "自分ユニット行動終了", 1, 0), Pin(0x42f, "他人ユニット行動終了", 1, 0), Pin(0x430, "敵ユニット行動終了", 1, 0), Pin(0x44c, "復活選択開始", 1, 0), Pin(0x44d, "コンティニュー選択開始", 1, 0), Pin(0xfab, "思考中隠す", 1, 0), Pin(0xfaa, "思考中表示", 1, 0), Pin(0xfd2, "自分切断", 1, 0), Pin(0x1069, "操作時間延長表示", 1, 0), Pin(0x456, "復活選択待ち表示開始", 1, 0), Pin(0x457, "復活選択待ち表示終了", 1, 0), Pin(0x460, "コンティニュー選択待ち開始", 1, 0), Pin(0x461, "コンティニュー選択待ち終了", 1, 0), Pin(0x4b0, "クエスト勝利", 1, 0), Pin(0x125e, "観戦モード強制終了確認完了", 0, 0), Pin(0x125d, "観戦モード強制終了", 1, 0), Pin(0x125c, "観戦モード", 1, 0), Pin(0x11f9, "対戦終了済み終了", 0, 0), Pin(0x11f8, "対戦終了済み", 1, 0), Pin(0x1196, "強制勝利強制終了", 1, 0)]
    public class FlowNode_BattleUI_MultiPlay : FlowNodePersistent
    {
        public float inputSec;
        public bool CheckRandCheat;
        [CompilerGenerated]
        private bool <StampWindowIsOpened>k__BackingField;

        public FlowNode_BattleUI_MultiPlay()
        {
            this.inputSec = 20f;
            this.CheckRandCheat = 1;
            base..ctor();
            return;
        }

        public void HideInputTimeLimit()
        {
            base.ActivateOutputLinks(0xfa1);
            return;
        }

        public void HideThinking()
        {
            base.ActivateOutputLinks(0xfab);
            return;
        }

        public void HideWaitContinue()
        {
            base.ActivateOutputLinks(0x461);
            return;
        }

        public void HideWaitRevive()
        {
            base.ActivateOutputLinks(0x457);
            return;
        }

        public override void OnActivate(int pinID)
        {
            SceneBattle battle;
            SceneBattle battle2;
            SceneBattle battle3;
            SceneBattle battle4;
            SceneBattle battle5;
            if (pinID != 0xfbf)
            {
                goto Label_0029;
            }
            battle = SceneBattle.Instance;
            if ((battle != null) == null)
            {
                goto Label_0115;
            }
            battle.CurrentNotifyDisconnectedPlayer = null;
            goto Label_0115;
        Label_0029:
            if (pinID != 0x1004)
            {
                goto Label_0040;
            }
            this.StampWindowIsOpened = 1;
            goto Label_0115;
        Label_0040:
            if (pinID != 0x1005)
            {
                goto Label_0057;
            }
            this.StampWindowIsOpened = 0;
            goto Label_0115;
        Label_0057:
            if (pinID != 0x10cd)
            {
                goto Label_0080;
            }
            battle2 = SceneBattle.Instance;
            if ((battle2 != null) == null)
            {
                goto Label_0115;
            }
            battle2.CurrentResumePlayer = null;
            goto Label_0115;
        Label_0080:
            if (pinID != 0x1195)
            {
                goto Label_00C5;
            }
            battle3 = SceneBattle.Instance;
            if ((battle3 != null) == null)
            {
                goto Label_0115;
            }
            battle3.Battle.IsVSForceWinComfirm = 1;
            if (GlobalVars.SelectedMultiPlayRoomType != 3)
            {
                goto Label_0115;
            }
            battle3.Battle.IsVSForceWin = 1;
            goto Label_0115;
        Label_00C5:
            if (pinID != 0x11f9)
            {
                goto Label_00EE;
            }
            battle4 = SceneBattle.Instance;
            if ((battle4 != null) == null)
            {
                goto Label_0115;
            }
            battle4.AlreadyEndBattle = 1;
            goto Label_0115;
        Label_00EE:
            if (pinID != 0x125e)
            {
                goto Label_0115;
            }
            battle5 = SceneBattle.Instance;
            if ((battle5 != null) == null)
            {
                goto Label_0115;
            }
            battle5.AudienceForceEnd = 1;
        Label_0115:
            return;
        }

        public void OnAlreadyFinish()
        {
            base.ActivateOutputLinks(0x11f8);
            return;
        }

        public void OnAudienceForceEnd()
        {
            base.ActivateOutputLinks(0x125d);
            return;
        }

        public void OnAudienceMode()
        {
            base.ActivateOutputLinks(0x125c);
            return;
        }

        public void OnEnemyUnitEnd()
        {
            base.ActivateOutputLinks(0x430);
            return;
        }

        public void OnEnemyUnitStart()
        {
            base.ActivateOutputLinks(0x426);
            return;
        }

        public void OnExtInput()
        {
            base.ActivateOutputLinks(0x1069);
            return;
        }

        public void OnForceWin()
        {
            base.ActivateOutputLinks(0x1194);
            return;
        }

        public void OnForceWinClose()
        {
            base.ActivateOutputLinks(0x1196);
            return;
        }

        public void OnMapEnd()
        {
            base.ActivateOutputLinks(0x515);
            return;
        }

        public void OnMapStart()
        {
            base.ActivateOutputLinks(0x514);
            return;
        }

        public void OnMyDisconnected()
        {
            base.ActivateOutputLinks(0xfd2);
            return;
        }

        public void OnMyPlayerResume()
        {
            base.ActivateOutputLinks(0x10ce);
            return;
        }

        public void OnMyPlayerResumeClose()
        {
            base.ActivateOutputLinks(0x10d0);
            return;
        }

        public void OnMyUnitEnd()
        {
            base.ActivateOutputLinks(0x42e);
            return;
        }

        public void OnMyUnitStart()
        {
            base.ActivateOutputLinks(0x424);
            return;
        }

        public void OnOtherDisconnected()
        {
            base.ActivateOutputLinks(0xfbe);
            return;
        }

        public void OnOtherDisconnectedForce()
        {
            base.ActivateOutputLinks(0xfc0);
            return;
        }

        public void OnOtherPlayerResume()
        {
            base.ActivateOutputLinks(0x10cc);
            return;
        }

        public void OnOtherPlayerResumeClose()
        {
            base.ActivateOutputLinks(0x10cf);
            return;
        }

        public void OnOtherPlayerSyncEnd()
        {
            base.ActivateOutputLinks(0x1131);
            return;
        }

        public void OnOtherPlayerSyncStart()
        {
            base.ActivateOutputLinks(0x1130);
            return;
        }

        public void OnOtherUnitEnd()
        {
            base.ActivateOutputLinks(0x42f);
            return;
        }

        public void OnOtherUnitStart()
        {
            base.ActivateOutputLinks(0x425);
            return;
        }

        public void OnQuestLose()
        {
            base.ActivateOutputLinks(0x4b1);
            return;
        }

        public void OnQuestRetreat()
        {
            base.ActivateOutputLinks(0x4b2);
            return;
        }

        public void OnQuestWin()
        {
            base.ActivateOutputLinks(0x4b0);
            return;
        }

        public void ShowInputTimeLimit()
        {
            base.ActivateOutputLinks(0xfa0);
            return;
        }

        public void ShowThinking()
        {
            base.ActivateOutputLinks(0xfaa);
            return;
        }

        public void ShowWaitContinue()
        {
            base.ActivateOutputLinks(0x460);
            return;
        }

        public void ShowWaitRevive()
        {
            base.ActivateOutputLinks(0x456);
            return;
        }

        public void StartSelectContinue()
        {
            base.ActivateOutputLinks(0x44d);
            return;
        }

        public void StartSelectRevive()
        {
            base.ActivateOutputLinks(0x44c);
            return;
        }

        public bool StampWindowIsOpened
        {
            [CompilerGenerated]
            get
            {
                return this.<StampWindowIsOpened>k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this.<StampWindowIsOpened>k__BackingField = value;
                return;
            }
        }

        public BattleStamp StampWindow
        {
            get
            {
                BattleStamp[] stampArray;
                stampArray = base.get_gameObject().GetComponentsInChildren<BattleStamp>(1);
            Label_001C:
                return (((stampArray != null) && (((int) stampArray.Length) > 0)) ? stampArray[0] : null);
            }
        }
    }
}

