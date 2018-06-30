namespace SRPG
{
    using System;

    [Pin(1, "Success", 1, 0), NodeType("Multi/MultiPlayUpdateRoomComment", 0x7fe5), Pin(0x65, "Update", 0, 0), Pin(2, "Failure", 1, 0)]
    public class FlowNode_MultiPlayUpdateRoomComment : FlowNode
    {
        public FlowNode_MultiPlayUpdateRoomComment()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            MyPhoton photon;
            MyPhoton.MyRoom room;
            JSON_MyPhotonRoomParam param;
            if (pinID != 0x65)
            {
                goto Label_00F0;
            }
            photon = PunMonoSingleton<MyPhoton>.Instance;
            if (photon.IsOldestPlayer() != null)
            {
                goto Label_002C;
            }
            DebugUtility.Log("I'm not room owner");
            base.ActivateOutputLinks(2);
            return;
        Label_002C:
            room = photon.GetCurrentRoom();
            if (room != null)
            {
                goto Label_004C;
            }
            DebugUtility.Log("CurrentRoom is null");
            base.ActivateOutputLinks(2);
            return;
        Label_004C:
            param = JSON_MyPhotonRoomParam.Parse(room.json);
            if (param != null)
            {
                goto Label_0071;
            }
            DebugUtility.Log("no roomParam");
            base.ActivateOutputLinks(2);
            return;
        Label_0071:
            GlobalVars.SelectedMultiPlayRoomComment = GlobalVars.EditMultiPlayRoomComment;
            param.passCode = GlobalVars.EditMultiPlayRoomPassCode;
            param.comment = GlobalVars.EditMultiPlayRoomComment;
            if (MyMsgInput.isLegal(param.comment) != null)
            {
                goto Label_00B4;
            }
            DebugUtility.Log("comment is not legal");
            base.ActivateOutputLinks(2);
            return;
        Label_00B4:
            DebugUtility.Log("comment:" + param.comment);
            photon.SetRoomParam(param.Serialize());
            PlayerPrefsUtility.SetString(PlayerPrefsUtility.ROOM_COMMENT_KEY, param.comment, 0);
            base.ActivateOutputLinks(1);
        Label_00F0:
            return;
        }
    }
}

