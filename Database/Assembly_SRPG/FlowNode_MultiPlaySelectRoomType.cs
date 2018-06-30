namespace SRPG
{
    using System;

    [Pin(2, "SelectTower", 0, 2), Pin(100, "Raid", 1, 100), Pin(0x65, "Versus", 1, 0x65), Pin(0, "SelectRaid", 0, 0), Pin(1, "SelectVersus", 0, 1), Pin(3, "SelectRankMatch", 0, 3), Pin(0x66, "Tower", 1, 0x66), Pin(0x67, "RankMatch", 1, 0x67), NodeType("Multi/MultiPlaySelectRoomType", 0x7fe5), Pin(10, "Test", 0, 10)]
    public class FlowNode_MultiPlaySelectRoomType : FlowNode
    {
        private const int PIN_IN_SELECT_RAID = 0;
        private const int PIN_IN_SELECT_VERSUS = 1;
        private const int PIN_IN_SELECT_TOWER = 2;
        private const int PIN_IN_SELECT_RANKMATCH = 3;
        private const int PIN_IN_TEST = 10;
        private const int PIN_OUT_RAID = 100;
        private const int PIN_OUT_VERSUS = 0x65;
        private const int PIN_OUT_TOWER = 0x66;
        private const int PIN_OUT_RANKMATCH = 0x67;

        public FlowNode_MultiPlaySelectRoomType()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            int num;
            JSON_MyPhotonRoomParam.EType type;
            num = pinID;
            switch (num)
            {
                case 0:
                    goto Label_001D;

                case 1:
                    goto Label_0032;

                case 2:
                    goto Label_0047;

                case 3:
                    goto Label_005C;
            }
            goto Label_0071;
        Label_001D:
            DebugUtility.Log("raid selected");
            GlobalVars.SelectedMultiPlayRoomType = 0;
            goto Label_0071;
        Label_0032:
            DebugUtility.Log("versus selected");
            GlobalVars.SelectedMultiPlayRoomType = 1;
            goto Label_0071;
        Label_0047:
            DebugUtility.Log("tower selected");
            GlobalVars.SelectedMultiPlayRoomType = 2;
            goto Label_0071;
        Label_005C:
            DebugUtility.Log("rank match selected");
            GlobalVars.SelectedMultiPlayRoomType = 3;
        Label_0071:
            switch (GlobalVars.SelectedMultiPlayRoomType)
            {
                case 0:
                    goto Label_0092;

                case 1:
                    goto Label_00A0;

                case 2:
                    goto Label_00AE;

                case 3:
                    goto Label_00BC;
            }
            goto Label_00CA;
        Label_0092:
            base.ActivateOutputLinks(100);
            goto Label_00D8;
        Label_00A0:
            base.ActivateOutputLinks(0x65);
            goto Label_00D8;
        Label_00AE:
            base.ActivateOutputLinks(0x66);
            goto Label_00D8;
        Label_00BC:
            base.ActivateOutputLinks(0x67);
            goto Label_00D8;
        Label_00CA:
            base.ActivateOutputLinks(100);
        Label_00D8:
            return;
        }
    }
}

