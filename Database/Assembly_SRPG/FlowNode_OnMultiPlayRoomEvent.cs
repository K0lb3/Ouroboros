namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    [Pin(0xc9, "Ignore Full Off", 0, 4), Pin(10, "OnRoomParam", 1, 14), Pin(50, "OnRoomPassChanged", 1, 15), AddComponentMenu(""), Pin(8, "OnRoomFullMember", 1, 12), Pin(7, "OnRoomCreatorOut", 1, 11), NodeType("Multi/OnMultiPlayRoomEvent", 0xe57f), Pin(100, "Ignore On", 0, 1), Pin(6, "OnRoomCommentChanged", 1, 10), Pin(0x65, "Ignore Off", 0, 2), Pin(200, "Ignore Full On", 0, 3), Pin(9, "OnRoomOnlyMember", 1, 13), Pin(300, "Reset", 0, 0x10), Pin(1, "OnDisconnected", 1, 5), Pin(2, "OnPlayerChanged", 1, 6), Pin(3, "OnAllPlayerReady", 1, 7), Pin(4, "OnAllPlayerNotReady", 1, 8), Pin(5, "OnRoomClosed", 1, 9)]
    public class FlowNode_OnMultiPlayRoomEvent : FlowNodePersistent
    {
        private const int PIN_INPUT_IGNORE_ON = 100;
        private const int PIN_INPUT_IGNORE_OFF = 0x65;
        private const int PIN_INPUT_IGNORE_ON_FULL = 200;
        private const int PIN_INPUT_IGNORE_OFF_FULL = 0xc9;
        private const int PIN_INPUT_RESET = 300;
        private const int PIN_OUTPUT_ON_DISCONNECTED = 1;
        private const int PIN_OUTPUT_ON_PLAYER_CHANGED = 2;
        private const int PIN_OUTPUT_ON_ALL_PLAYERS_READY = 3;
        private const int PIN_OUTPUT_ON_ALL_PLAYER_NOT_READY = 4;
        private const int PIN_OUTPUT_ON_ROOM_CLOSED = 5;
        private const int PIN_OUTPUT_ON_ROOM_COMMENT_CHANGED = 6;
        private const int PIN_OUTPUT_ON_ROOM_CREATOR_OUT = 7;
        private const int PIN_OUTPUT_ON_ROOM_FULL_MEMBER = 8;
        private const int PIN_OUTPUT_ON_ROOM_ONLY_MEMBER = 9;
        private const int PIN_OUTPUT_ON_ROOM_PARAM = 10;
        private const int PIN_OUTPUT_ON_ROOM_PASS_CHANGED = 50;
        private List<MyPhoton.MyPlayer> mPlayers;
        private string mRoomPass;
        private string mRoomComment;
        private string mQuestName;
        private bool mIgnore;
        private bool mIgnoreFullMember;
        private int mMemberCnt;
        [CompilerGenerated]
        private static Predicate<MyPhoton.MyPlayer> <>f__am$cache7;

        public FlowNode_OnMultiPlayRoomEvent()
        {
            this.mRoomPass = string.Empty;
            this.mRoomComment = string.Empty;
            this.mQuestName = string.Empty;
            this.mIgnoreFullMember = 1;
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private static bool <Update>m__1B6(MyPhoton.MyPlayer p)
        {
            return (p.playerID == 1);
        }

        public override void OnActivate(int pinID)
        {
            int num;
            num = pinID;
            if (num == 100)
            {
                goto Label_0038;
            }
            if (num == 0x65)
            {
                goto Label_0044;
            }
            if (num == 200)
            {
                goto Label_0050;
            }
            if (num == 0xc9)
            {
                goto Label_005C;
            }
            if (num == 300)
            {
                goto Label_0068;
            }
            goto Label_0073;
        Label_0038:
            this.mIgnore = 1;
            goto Label_0073;
        Label_0044:
            this.mIgnore = 0;
            goto Label_0073;
        Label_0050:
            this.mIgnoreFullMember = 1;
            goto Label_0073;
        Label_005C:
            this.mIgnoreFullMember = 0;
            goto Label_0073;
        Label_0068:
            this.Reset();
        Label_0073:
            return;
        }

        private void Reset()
        {
            this.mPlayers = null;
            this.mRoomPass = string.Empty;
            this.mRoomComment = string.Empty;
            this.mQuestName = string.Empty;
            this.mMemberCnt = 0;
            return;
        }

        private void Start()
        {
            this.mIgnore = 0;
            this.mIgnoreFullMember = 1;
            this.mQuestName = GlobalVars.SelectedQuestID;
            return;
        }

        private unsafe void Update()
        {
            MyPhoton photon;
            List<MyPhoton.MyPlayer> list;
            bool flag;
            bool flag2;
            bool flag3;
            MyPhoton.MyRoom room;
            JSON_MyPhotonRoomParam param;
            string str;
            string str2;
            bool flag4;
            int num;
            JSON_MyPhotonPlayerParam[] paramArray;
            int num2;
            bool flag5;
            MyPhoton.MyPlayer player;
            List<MyPhoton.MyPlayer>.Enumerator enumerator;
            JSON_MyPhotonPlayerParam param2;
            int num3;
            photon = PunMonoSingleton<MyPhoton>.Instance;
            if (this.mIgnore == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            if (photon.CurrentState == 4)
            {
                goto Label_0038;
            }
            if (photon.CurrentState == null)
            {
                goto Label_002F;
            }
            photon.Disconnect();
        Label_002F:
            base.ActivateOutputLinks(1);
            return;
        Label_0038:
            list = photon.GetRoomPlayerList();
            flag = GlobalVars.SelectedMultiPlayRoomType == 0;
            flag2 = (GlobalVars.SelectedMultiPlayRoomType != 1) ? 0 : (GlobalVars.SelectedMultiPlayVersusType == 2);
            flag3 = GlobalVars.SelectedMultiPlayRoomType == 2;
            if (<>f__am$cache7 != null)
            {
                goto Label_0082;
            }
            <>f__am$cache7 = new Predicate<MyPhoton.MyPlayer>(FlowNode_OnMultiPlayRoomEvent.<Update>m__1B6);
        Label_0082:
            if ((list.Find(<>f__am$cache7) != null) || (((flag == null) && (flag2 == null)) && (flag3 == null)))
            {
                goto Label_00AD;
            }
            base.ActivateOutputLinks(7);
            return;
        Label_00AD:
            room = photon.GetCurrentRoom();
            if (photon.IsUpdateRoomProperty == null)
            {
                goto Label_00DC;
            }
            if (room.start == null)
            {
                goto Label_00D5;
            }
            base.ActivateOutputLinks(5);
            return;
        Label_00D5:
            photon.IsUpdateRoomProperty = 0;
        Label_00DC:
            param = JSON_MyPhotonRoomParam.Parse(room.json);
            str = (param != null) ? param.comment : string.Empty;
            if (this.mRoomComment.Equals(str) != null)
            {
                goto Label_0128;
            }
            DebugUtility.Log("change room comment");
            base.ActivateOutputLinks(6);
        Label_0128:
            this.mRoomComment = str;
            str2 = (param != null) ? param.passCode : string.Empty;
            if (this.mRoomPass.Equals(str2) != null)
            {
                goto Label_016F;
            }
            DebugUtility.Log("change room pass");
            base.ActivateOutputLinks(50);
        Label_016F:
            this.mRoomPass = str2;
            flag4 = 0;
            if (list != null)
            {
                goto Label_018B;
            }
            photon.Disconnect();
            goto Label_0239;
        Label_018B:
            if (this.mPlayers != null)
            {
                goto Label_019E;
            }
            flag4 = 1;
            goto Label_0239;
        Label_019E:
            if (this.mPlayers.Count == list.Count)
            {
                goto Label_01BC;
            }
            flag4 = 1;
            goto Label_0239;
        Label_01BC:
            num = 0;
            goto Label_0227;
        Label_01C4:
            if (this.mPlayers[num].playerID == list[num].playerID)
            {
                goto Label_01F0;
            }
            flag4 = 1;
            goto Label_0239;
        Label_01F0:
            if (this.mPlayers[num].json.Equals(list[num].json) != null)
            {
                goto Label_0221;
            }
            flag4 = 1;
            goto Label_0239;
        Label_0221:
            num += 1;
        Label_0227:
            if (num < this.mPlayers.Count)
            {
                goto Label_01C4;
            }
        Label_0239:
            if (string.IsNullOrEmpty(this.mQuestName) != null)
            {
                goto Label_027F;
            }
            if (this.mQuestName.Equals(param.iname) != null)
            {
                goto Label_027F;
            }
            DebugUtility.Log("change quest iname" + param.iname);
            base.ActivateOutputLinks(10);
        Label_027F:
            this.mQuestName = param.iname;
            if (flag4 == null)
            {
                goto Label_039B;
            }
            this.mPlayers = new List<MyPhoton.MyPlayer>(list);
            base.ActivateOutputLinks(2);
            if (photon.IsOldestPlayer() == null)
            {
                goto Label_0306;
            }
            paramArray = new JSON_MyPhotonPlayerParam[list.Count];
            num2 = 0;
            goto Label_02E4;
        Label_02C7:
            paramArray[num2] = JSON_MyPhotonPlayerParam.Parse(list[num2].json);
            num2 += 1;
        Label_02E4:
            if (num2 < ((int) paramArray.Length))
            {
                goto Label_02C7;
            }
            param.players = paramArray;
            photon.SetRoomParam(param.Serialize());
        Label_0306:
            flag5 = 1;
            enumerator = this.mPlayers.GetEnumerator();
        Label_0316:
            try
            {
                goto Label_0360;
            Label_031B:
                player = &enumerator.Current;
                param2 = JSON_MyPhotonPlayerParam.Parse(player.json);
                if (param2.state == null)
                {
                    goto Label_0358;
                }
                if (param2.state == 4)
                {
                    goto Label_0358;
                }
                if (param2.state != 5)
                {
                    goto Label_0360;
                }
            Label_0358:
                flag5 = 0;
                goto Label_036C;
            Label_0360:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_031B;
                }
            Label_036C:
                goto Label_037E;
            }
            finally
            {
            Label_0371:
                ((List<MyPhoton.MyPlayer>.Enumerator) enumerator).Dispose();
            }
        Label_037E:
            if (flag5 == null)
            {
                goto Label_0392;
            }
            base.ActivateOutputLinks(3);
            goto Label_039A;
        Label_0392:
            base.ActivateOutputLinks(4);
        Label_039A:
            return;
        Label_039B:
            num3 = photon.GetRoomPlayerList().Count;
            if (num3 != 1)
            {
                goto Label_03C6;
            }
            if (this.mMemberCnt == num3)
            {
                goto Label_03C6;
            }
            base.ActivateOutputLinks(9);
        Label_03C6:
            this.mMemberCnt = photon.GetRoomPlayerList().Count;
            if (this.mIgnoreFullMember == null)
            {
                goto Label_03E3;
            }
            return;
        Label_03E3:
            if ((photon.GetCurrentRoom().maxPlayers - 1) != num3)
            {
                goto Label_03FF;
            }
            base.ActivateOutputLinks(8);
        Label_03FF:
            return;
        }
    }
}

