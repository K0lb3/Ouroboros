namespace SRPG
{
    using System;

    [Pin(200, "CreateQuest", 0, 200), NodeType("UI/GUIRanking", 0x7fe5), Pin(0xc9, "CreateArena", 0, 0xc9), Pin(0xca, "CreateTowerMatch", 0, 0xca)]
    public class FlowNode_GUIRanking : FlowNode_GUI
    {
        private UsageRateRanking.ViewInfoType type;

        public FlowNode_GUIRanking()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            if (pinID != 200)
            {
                goto Label_0012;
            }
            this.type = 0;
        Label_0012:
            if (pinID != 0xc9)
            {
                goto Label_0024;
            }
            this.type = 1;
        Label_0024:
            if (pinID != 0xca)
            {
                goto Label_0036;
            }
            this.type = 2;
        Label_0036:
            if (pinID == 200)
            {
                goto Label_0057;
            }
            if (pinID == 0xc9)
            {
                goto Label_0057;
            }
            if (pinID != 0xca)
            {
                goto Label_005B;
            }
        Label_0057:
            pinID = 100;
        Label_005B:
            base.OnActivate(pinID);
            return;
        }

        protected override void OnInstanceCreate()
        {
            UsageRateRanking ranking;
            base.OnInstanceCreate();
            ranking = base.Instance.GetComponentInChildren<UsageRateRanking>();
            if ((ranking == null) == null)
            {
                goto Label_001F;
            }
            return;
        Label_001F:
            ranking.OnChangedToggle(this.type);
            return;
        }
    }
}

