namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;

    [Pin(0x66, "IAP", 1, 0), NodeType("System/CheckStamina", 0x7fe5), Pin(0, "In", 0, 0), Pin(100, "Pass", 1, 0), Pin(0x65, "Restore", 1, 0), Pin(0x67, "HEALAP", 1, 0)]
    public class FlowNode_CheckStamina : FlowNode
    {
        public const int PINID_IN = 0;
        public const int PINID_PASS = 100;
        public const int PINID_RESTORE = 0x65;
        public const int PINID_IAP = 0x66;
        public const int PINID_HEALAP = 0x67;
        public string DebugQuestID;

        public FlowNode_CheckStamina()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            GameManager manager;
            QuestParam param;
            int num;
            int num2;
            if (pinID == null)
            {
                goto Label_000D;
            }
            goto Label_0089;
        Label_000D:
            manager = MonoSingleton<GameManager>.Instance;
            param = this.SelectedQuest;
            if (param != null)
            {
                goto Label_003A;
            }
            DebugUtility.LogError("QuestNotFound \"" + GlobalVars.SelectedQuestID + "\" ");
            return;
        Label_003A:
            num = param.RequiredApWithPlayerLv(manager.Player.Lv, 1);
            if (GlobalVars.RaidNum <= 0)
            {
                goto Label_0060;
            }
            num *= GlobalVars.RaidNum;
        Label_0060:
            if (num > manager.Player.Stamina)
            {
                goto Label_007B;
            }
            base.ActivateOutputLinks(100);
            return;
        Label_007B:
            base.ActivateOutputLinks(0x67);
        Label_0089:
            return;
        }

        private void OnBuyCoin(GameObject go)
        {
            base.ActivateOutputLinks(0x66);
            return;
        }

        private void OnCancel(GameObject go)
        {
            GlobalVars.RaidNum = 0;
            return;
        }

        private void OnRestoreStamina(GameObject go)
        {
            string str;
            if (this.RestoreCost > MonoSingleton<GameManager>.Instance.Player.Coin)
            {
                goto Label_0028;
            }
            base.ActivateOutputLinks(0x65);
            goto Label_0062;
        Label_0028:
            UIUtility.ConfirmBox(string.Format(LocalizedText.Get("sys.OUT_OF_STAMINA_BUYCOIN"), new object[0]), new UIUtility.DialogResultEvent(this.OnBuyCoin), new UIUtility.DialogResultEvent(this.OnCancel), null, 0, -1, null, null);
        Label_0062:
            return;
        }

        private int RestoreCost
        {
            get
            {
                return MonoSingleton<GameManager>.Instance.Player.GetStaminaRecoveryCost(0);
            }
        }

        private QuestParam SelectedQuest
        {
            get
            {
                QuestParam param;
                if (string.IsNullOrEmpty(this.DebugQuestID) != null)
                {
                    goto Label_0021;
                }
                return MonoSingleton<GameManager>.Instance.FindQuest(this.DebugQuestID);
            Label_0021:
                return MonoSingleton<GameManager>.Instance.FindQuest(GlobalVars.SelectedQuestID);
            }
        }
    }
}

