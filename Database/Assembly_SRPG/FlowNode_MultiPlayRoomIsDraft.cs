namespace SRPG
{
    using GR;
    using System;

    [Pin(2, "No", 1, 2), Pin(0, "Check", 0, 0), Pin(1, "Yes", 1, 1), NodeType("Multi/MultiPlayRoomIsDraft", 0x7fe5)]
    public class FlowNode_MultiPlayRoomIsDraft : FlowNode
    {
        private const int PIN_INPUT_CHECK = 0;
        private const int PIN_OUTPUT_YES = 1;
        private const int PIN_OUTPUT_NO = 2;

        public FlowNode_MultiPlayRoomIsDraft()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            GameManager manager;
            MyPhoton photon;
            MyPhoton.MyRoom room;
            JSON_MyPhotonRoomParam param;
            if (pinID != null)
            {
                goto Label_008E;
            }
            manager = MonoSingleton<GameManager>.Instance;
            photon = PunMonoSingleton<MyPhoton>.Instance;
            if ((photon == null) == null)
            {
                goto Label_0027;
            }
            base.ActivateOutputLinks(2);
            return;
        Label_0027:
            if (manager.AudienceMode == null)
            {
                goto Label_003E;
            }
            room = manager.AudienceRoom;
            goto Label_0045;
        Label_003E:
            room = photon.GetCurrentRoom();
        Label_0045:
            if (room != null)
            {
                goto Label_0054;
            }
            base.ActivateOutputLinks(2);
            return;
        Label_0054:
            param = (room != null) ? JSON_MyPhotonRoomParam.Parse(room.json) : null;
            if (param == null)
            {
                goto Label_007D;
            }
            if (param.draft_type != null)
            {
                goto Label_0086;
            }
        Label_007D:
            base.ActivateOutputLinks(2);
            return;
        Label_0086:
            base.ActivateOutputLinks(1);
        Label_008E:
            return;
        }
    }
}

