namespace SRPG
{
    using System;

    [Pin(0x66, "UpdatePass", 0, 0), Pin(2, "Failure", 1, 0), Pin(1, "Success", 1, 0), NodeType("Multi/UpdateFloor", 0x7fe5), Pin(0x65, "Update", 0, 0)]
    public class FlowNode_MultiTowerUpdateFloor : FlowNode
    {
        public FlowNode_MultiTowerUpdateFloor()
        {
            base..ctor();
            return;
        }

        public override unsafe void OnActivate(int pinID)
        {
            MyPhoton photon;
            MyPhoton.MyRoom room;
            JSON_MyPhotonRoomParam param;
            MyPhoton photon2;
            MyPhoton.MyRoom room2;
            JSON_MyPhotonRoomParam param2;
            if (pinID != 0x65)
            {
                goto Label_00D7;
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
            param.challegedMTFloor = GlobalVars.SelectedMultiTowerFloor;
            param.iname = GlobalVars.SelectedMultiTowerID + "_" + &param.challegedMTFloor.ToString();
            photon.SetRoomParam(param.Serialize());
            photon.UpdateRoomParam("floor", (int) GlobalVars.SelectedMultiTowerFloor);
            GlobalVars.SelectedQuestID = param.iname;
            base.ActivateOutputLinks(1);
            goto Label_0175;
        Label_00D7:
            if (pinID != 0x66)
            {
                goto Label_0175;
            }
            photon2 = PunMonoSingleton<MyPhoton>.Instance;
            if (photon2.IsOldestPlayer() != null)
            {
                goto Label_0103;
            }
            DebugUtility.Log("I'm not room owner");
            base.ActivateOutputLinks(2);
            return;
        Label_0103:
            room2 = photon2.GetCurrentRoom();
            if (room2 != null)
            {
                goto Label_0125;
            }
            DebugUtility.Log("CurrentRoom is null");
            base.ActivateOutputLinks(2);
            return;
        Label_0125:
            if (JSON_MyPhotonRoomParam.Parse(room2.json) != null)
            {
                goto Label_014D;
            }
            DebugUtility.Log("no roomParam");
            base.ActivateOutputLinks(2);
            return;
        Label_014D:
            photon2.UpdateRoomParam("Lock", (bool) (GlobalVars.EditMultiPlayRoomPassCode != "0"));
            base.ActivateOutputLinks(1);
        Label_0175:
            return;
        }
    }
}

