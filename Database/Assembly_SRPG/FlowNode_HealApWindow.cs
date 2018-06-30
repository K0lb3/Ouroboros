namespace SRPG
{
    using GR;
    using System;

    [Pin(0x69, "HealCoin", 1, 0x69), Pin(0x68, "Quest", 0, 0x68), Pin(0x67, "Home", 0, 0x67), NodeType("UI/HealApWindow", 0x7fe5)]
    public class FlowNode_HealApWindow : FlowNode_GUI
    {
        private bool mIsQuest;
        private HealAp mHealAp;

        public FlowNode_HealApWindow()
        {
            base..ctor();
            return;
        }

        public void HealCoin()
        {
            base.ActivateOutputLinks(0x69);
            return;
        }

        public override void OnActivate(int pinID)
        {
            if (pinID == 0x67)
            {
                goto Label_0010;
            }
            if (pinID != 0x68)
            {
                goto Label_001F;
            }
        Label_0010:
            this.mIsQuest = pinID == 0x68;
            pinID = 100;
        Label_001F:
            base.OnActivate(pinID);
            return;
        }

        protected override void OnCreatePinActive()
        {
            bool flag;
            if (MonoSingleton<GameManager>.Instance.Player.IsHaveHealAPItems() == null)
            {
                goto Label_0044;
            }
            base.OnCreatePinActive();
            this.mHealAp = base.Instance.GetComponentInChildren<HealAp>();
            this.mHealAp.Refresh(this.mIsQuest, this);
            goto Label_004A;
        Label_0044:
            this.HealCoin();
        Label_004A:
            return;
        }
    }
}

