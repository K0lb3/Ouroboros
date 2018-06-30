namespace SRPG
{
    using System;

    [Pin(300, "AudienceOn", 0, 300), NodeType("Multi/UpdateRoomParam", 0x7fe5), Pin(0x65, "Reflesh", 0, 0x65), Pin(100, "Update", 0, 100), Pin(0x12d, "AudienceOff", 0, 0x12d), Pin(200, "Finish", 1, 200)]
    public class FlowNode_UpdateVersusRoom : FlowNode
    {
        public FlowNode_UpdateVersusRoom()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            MyPhoton photon;
            MyPhoton.MyRoom room;
            JSON_MyPhotonRoomParam param;
            MyPhoton photon2;
            MyPhoton.MyRoom room2;
            JSON_MyPhotonRoomParam param2;
            MyPhoton photon3;
            MyPhoton.MyRoom room3;
            JSON_MyPhotonRoomParam param3;
            MyPhoton photon4;
            MyPhoton.MyRoom room4;
            JSON_MyPhotonRoomParam param4;
            if (pinID != 100)
            {
                goto Label_0067;
            }
            photon = PunMonoSingleton<MyPhoton>.Instance;
            if ((photon != null) == null)
            {
                goto Label_0056;
            }
            if (photon.IsOldestPlayer() == null)
            {
                goto Label_0056;
            }
            room = photon.GetCurrentRoom();
            if (room == null)
            {
                goto Label_0056;
            }
            param = JSON_MyPhotonRoomParam.Parse(room.json);
            param.iname = GlobalVars.SelectedQuestID;
            photon.SetRoomParam(param.Serialize());
        Label_0056:
            base.ActivateOutputLinks(200);
            goto Label_015C;
        Label_0067:
            if (pinID != 0x65)
            {
                goto Label_00AF;
            }
            photon2 = PunMonoSingleton<MyPhoton>.Instance;
            if ((photon2 != null) == null)
            {
                goto Label_015C;
            }
            room2 = photon2.GetCurrentRoom();
            if (room2 == null)
            {
                goto Label_015C;
            }
            GlobalVars.SelectedQuestID = JSON_MyPhotonRoomParam.Parse(room2.json).iname;
            goto Label_015C;
        Label_00AF:
            if (pinID != 300)
            {
                goto Label_0108;
            }
            photon3 = PunMonoSingleton<MyPhoton>.Instance;
            if ((photon3 != null) == null)
            {
                goto Label_015C;
            }
            room3 = photon3.GetCurrentRoom();
            if (room3 == null)
            {
                goto Label_015C;
            }
            param3 = JSON_MyPhotonRoomParam.Parse(room3.json);
            param3.audience = 1;
            photon3.SetRoomParam(param3.Serialize());
            goto Label_015C;
        Label_0108:
            if (pinID != 0x12d)
            {
                goto Label_015C;
            }
            photon4 = PunMonoSingleton<MyPhoton>.Instance;
            if ((photon4 != null) == null)
            {
                goto Label_015C;
            }
            room4 = photon4.GetCurrentRoom();
            if (room4 == null)
            {
                goto Label_015C;
            }
            param4 = JSON_MyPhotonRoomParam.Parse(room4.json);
            param4.audience = 0;
            photon4.SetRoomParam(param4.Serialize());
        Label_015C:
            base.ActivateOutputLinks(200);
            return;
        }
    }
}

