namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    [Pin(100, "Set", 0, 0), Pin(200, "Out", 1, 0), NodeType("Multi/Award", 0x7fe5)]
    public class FlowNode_VersusAward : FlowNode
    {
        private readonly int ROOM_MAX_PLAYERCNT;
        public GameObject BindObj;
        public bool MyPlayer;

        public FlowNode_VersusAward()
        {
            this.ROOM_MAX_PLAYERCNT = 2;
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            GameManager manager;
            JSON_MyPhotonPlayerParam param;
            MyPhoton.MyRoom room;
            JSON_MyPhotonRoomParam param2;
            List<SRPG.MyPhoton.MyPlayer> list;
            SRPG.MyPhoton.MyPlayer player;
            <OnActivate>c__AnonStorey27F storeyf;
            if (pinID != 100)
            {
                goto Label_00F3;
            }
            manager = MonoSingleton<GameManager>.Instance;
            param = null;
            if (manager.AudienceMode == null)
            {
                goto Label_0077;
            }
            room = manager.AudienceRoom;
            if (room == null)
            {
                goto Label_00E1;
            }
            param2 = JSON_MyPhotonRoomParam.Parse(room.json);
            if (((param2 == null) || (param2.players == null)) || (((int) param2.players.Length) < this.ROOM_MAX_PLAYERCNT))
            {
                goto Label_00E1;
            }
            param = param2.players[(this.MyPlayer == null) ? 1 : 0];
            goto Label_00E1;
        Label_0077:
            storeyf = new <OnActivate>c__AnonStorey27F();
            storeyf.pt = PunMonoSingleton<MyPhoton>.Instance;
            list = storeyf.pt.GetRoomPlayerList();
            if (list == null)
            {
                goto Label_00E1;
            }
            if (this.MyPlayer == null)
            {
                goto Label_00B7;
            }
            param = JSON_MyPhotonPlayerParam.Create(0, 0);
            goto Label_00E1;
        Label_00B7:
            player = list.Find(new Predicate<SRPG.MyPhoton.MyPlayer>(storeyf.<>m__1D1));
            if (player == null)
            {
                goto Label_00E1;
            }
            param = JSON_MyPhotonPlayerParam.Parse(player.json);
        Label_00E1:
            if (param == null)
            {
                goto Label_00F3;
            }
            DataSource.Bind<JSON_MyPhotonPlayerParam>(this.BindObj, param);
        Label_00F3:
            base.ActivateOutputLinks(200);
            return;
        }

        [CompilerGenerated]
        private sealed class <OnActivate>c__AnonStorey27F
        {
            internal MyPhoton pt;

            public <OnActivate>c__AnonStorey27F()
            {
                base..ctor();
                return;
            }

            internal bool <>m__1D1(MyPhoton.MyPlayer p)
            {
                return ((p.playerID == this.pt.GetMyPlayer().playerID) == 0);
            }
        }
    }
}

