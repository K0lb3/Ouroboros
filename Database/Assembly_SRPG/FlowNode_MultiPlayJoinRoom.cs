namespace SRPG
{
    using ExitGames.Client.Photon;
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    [Pin(200, "VersusCreateRoom", 0, 200), Pin(5, "FullMember", 1, 0), Pin(4, "IllegalQuest", 1, 0), Pin(3, "FailureLobby", 1, 0), Pin(2, "Failure", 1, 0), Pin(1, "Success", 1, 0), NodeType("Multi/MultiPlayJoinRoom", 0x7fe5), Pin(0x65, "CreateRoom", 0, 0x65), Pin(0x66, "JoinRoom", 0, 0x66), Pin(0x12e, "MTResumeJoinRoom", 0, 0x12e), Pin(0x12d, "MTJoinRoom", 0, 0x12d), Pin(300, "MTCreateRoom", 0, 300), Pin(0xcc, "RankMatchCreateRoom", 0, 0xcc), Pin(0xcb, "RankMatchJoinRoom", 0, 0xcb), Pin(0xca, "VersusTowerJoinRoom", 0, 0xca), Pin(0xc9, "VersusJoinRoom", 0, 0xc9), Pin(0x67, "CreateOrJoinLINE", 0, 0x67)]
    public class FlowNode_MultiPlayJoinRoom : FlowNode
    {
        private const int INPUT_PIN_VERSUS_CREATE_ROOM = 200;
        private const int INPUT_PIN_VERSUS_JOIN_ROOM = 0xc9;
        private const int INPUT_PIN_RANK_MATCH_JOIN_ROOM = 0xcb;
        private const int INPUT_PIN_RANK_MATCH_CREATE_ROOM = 0xcc;
        private const int RANKMATCH_SCORE_RANGE_MAX = 900;
        private const int RANKMATCH_SCORE_RANGE_MIN = 100;
        private StateMachine<FlowNode_MultiPlayJoinRoom> mStateMachine;
        private JSON_MyPhotonPlayerParam mJoinPlayerParam;
        private readonly byte VERSUS_PLAYER_MAX;
        [CompilerGenerated]
        private bool <IsLINE>k__BackingField;

        public FlowNode_MultiPlayJoinRoom()
        {
            this.VERSUS_PLAYER_MAX = 3;
            base..ctor();
            return;
        }

        private void Failure()
        {
            MyPhoton photon;
            base.set_enabled(0);
            photon = PunMonoSingleton<MyPhoton>.Instance;
            if (photon.CurrentState == null)
            {
                goto Label_001E;
            }
            photon.Disconnect();
        Label_001E:
            base.ActivateOutputLinks(2);
            DebugUtility.Log("Create/Join Room Failure.");
            return;
        }

        private void FailureFullMember()
        {
            base.set_enabled(0);
            base.ActivateOutputLinks(5);
            DebugUtility.Log("Join Room FullMember.");
            return;
        }

        private void FailureLobby()
        {
            MyPhoton photon;
            base.set_enabled(0);
            photon = PunMonoSingleton<MyPhoton>.Instance;
            if (photon.CurrentState == 2)
            {
                goto Label_002C;
            }
            photon.Disconnect();
            base.ActivateOutputLinks(2);
            goto Label_0034;
        Label_002C:
            base.ActivateOutputLinks(3);
        Label_0034:
            DebugUtility.Log("Create/Join Room FailureLobby.");
            return;
        }

        public void GotoState<StateType>() where StateType: State<FlowNode_MultiPlayJoinRoom>, new()
        {
            if (this.mStateMachine == null)
            {
                goto Label_0016;
            }
            this.mStateMachine.GotoState<StateType>();
        Label_0016:
            return;
        }

        private void IllegalQuest()
        {
            base.set_enabled(0);
            base.ActivateOutputLinks(4);
            DebugUtility.Log("Create/Join Room IllegalQuest.");
            return;
        }

        public override void OnActivate(int pinID)
        {
            object[] objArray2;
            object[] objArray1;
            if (pinID != 0x65)
            {
                goto Label_003C;
            }
            DebugUtility.Log("Start Create Room");
            base.set_enabled(1);
            this.IsLINE = 0;
            this.mStateMachine = new StateMachine<FlowNode_MultiPlayJoinRoom>(this);
            this.mStateMachine.GotoState<State_CreateRoom>();
            goto Label_0380;
        Label_003C:
            if (pinID != 0x66)
            {
                goto Label_0078;
            }
            DebugUtility.Log("Start Join Room");
            base.set_enabled(1);
            this.IsLINE = 0;
            this.mStateMachine = new StateMachine<FlowNode_MultiPlayJoinRoom>(this);
            this.mStateMachine.GotoState<State_JoinRoom>();
            goto Label_0380;
        Label_0078:
            if (pinID != 0x67)
            {
                goto Label_018D;
            }
            DebugUtility.Log("Start Create/Join Room LINE");
            base.set_enabled(1);
            this.IsLINE = 1;
            if (JSON_MyPhotonRoomParam.GetMyCreatorFUID().Equals(FlowNode_OnUrlSchemeLaunch.LINEParam_decided.creatorFUID) == null)
            {
                goto Label_0117;
            }
            objArray1 = new object[] { "Creating LINERoom iname:", GlobalVars.SelectedQuestID, " type:", (JSON_MyPhotonRoomParam.EType) GlobalVars.SelectedMultiPlayRoomType, " creatorFUID:", FlowNode_OnUrlSchemeLaunch.LINEParam_decided.creatorFUID };
            DebugUtility.Log(string.Concat(objArray1));
            this.mStateMachine = new StateMachine<FlowNode_MultiPlayJoinRoom>(this);
            this.mStateMachine.GotoState<State_CreateRoom>();
            goto Label_0188;
        Label_0117:
            objArray2 = new object[] { "Joining LINERoom iname:", GlobalVars.SelectedQuestID, " type:", (JSON_MyPhotonRoomParam.EType) GlobalVars.SelectedMultiPlayRoomType, " creatorFUID:", FlowNode_OnUrlSchemeLaunch.LINEParam_decided.creatorFUID, " name:", GlobalVars.SelectedMultiPlayRoomName };
            DebugUtility.Log(string.Concat(objArray2));
            this.mStateMachine = new StateMachine<FlowNode_MultiPlayJoinRoom>(this);
            this.mStateMachine.GotoState<State_JoinRoom>();
        Label_0188:
            goto Label_0380;
        Label_018D:
            if (pinID != 200)
            {
                goto Label_01CC;
            }
            DebugUtility.Log("Start Versus Create Room");
            base.set_enabled(1);
            this.IsLINE = 0;
            this.mStateMachine = new StateMachine<FlowNode_MultiPlayJoinRoom>(this);
            this.mStateMachine.GotoState<State_VersusCreate>();
            goto Label_0380;
        Label_01CC:
            if (pinID != 0xc9)
            {
                goto Label_020B;
            }
            DebugUtility.Log("Start Versus Join Room");
            base.set_enabled(1);
            this.IsLINE = 0;
            this.mStateMachine = new StateMachine<FlowNode_MultiPlayJoinRoom>(this);
            this.mStateMachine.GotoState<State_VersusJoin>();
            goto Label_0380;
        Label_020B:
            if (pinID != 0xca)
            {
                goto Label_024A;
            }
            DebugUtility.Log("Start Versus Rank Join Room");
            base.set_enabled(1);
            this.IsLINE = 0;
            this.mStateMachine = new StateMachine<FlowNode_MultiPlayJoinRoom>(this);
            this.mStateMachine.GotoState<State_VersusTowerJoin>();
            goto Label_0380;
        Label_024A:
            if (pinID != 0xcb)
            {
                goto Label_0289;
            }
            DebugUtility.Log("Start Rank Match Join Room");
            base.set_enabled(1);
            this.IsLINE = 0;
            this.mStateMachine = new StateMachine<FlowNode_MultiPlayJoinRoom>(this);
            this.mStateMachine.GotoState<State_VersusRankJoin>();
            goto Label_0380;
        Label_0289:
            if (pinID != 0xcc)
            {
                goto Label_02C8;
            }
            DebugUtility.Log("Start Rank Match Create Room");
            base.set_enabled(1);
            this.IsLINE = 0;
            this.mStateMachine = new StateMachine<FlowNode_MultiPlayJoinRoom>(this);
            this.mStateMachine.GotoState<State_VersusRankCreate>();
            goto Label_0380;
        Label_02C8:
            if (pinID != 300)
            {
                goto Label_0307;
            }
            DebugUtility.Log("Start MultiTower CreateRoom");
            base.set_enabled(1);
            this.IsLINE = 0;
            this.mStateMachine = new StateMachine<FlowNode_MultiPlayJoinRoom>(this);
            this.mStateMachine.GotoState<State_TowerCreate>();
            goto Label_0380;
        Label_0307:
            if (pinID != 0x12d)
            {
                goto Label_0346;
            }
            DebugUtility.Log("Start MultiTower JoinRoom");
            base.set_enabled(1);
            this.IsLINE = 0;
            this.mStateMachine = new StateMachine<FlowNode_MultiPlayJoinRoom>(this);
            this.mStateMachine.GotoState<State_TowerJoin>();
            goto Label_0380;
        Label_0346:
            if (pinID != 0x12e)
            {
                goto Label_0380;
            }
            DebugUtility.Log("Start MultiTower Resume JoinRoom");
            base.set_enabled(1);
            this.IsLINE = 0;
            this.mStateMachine = new StateMachine<FlowNode_MultiPlayJoinRoom>(this);
            this.mStateMachine.GotoState<State_ResumeTowerJoinRoom>();
        Label_0380:
            return;
        }

        private void Success()
        {
            base.set_enabled(0);
            base.ActivateOutputLinks(1);
            DebugUtility.Log("Create/Join Room Success.");
            return;
        }

        private void Update()
        {
            if (this.mStateMachine == null)
            {
                goto Label_0016;
            }
            this.mStateMachine.Update();
        Label_0016:
            return;
        }

        private bool IsLINE
        {
            [CompilerGenerated]
            get
            {
                return this.<IsLINE>k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this.<IsLINE>k__BackingField = value;
                return;
            }
        }

        private class State_CreateRoom : State<FlowNode_MultiPlayJoinRoom>
        {
            public State_CreateRoom()
            {
                base..ctor();
                return;
            }

            public override void Begin(FlowNode_MultiPlayJoinRoom self)
            {
                object[] objArray1;
                MyPhoton photon;
                QuestParam param;
                JSON_MyPhotonRoomParam param2;
                photon = PunMonoSingleton<MyPhoton>.Instance;
                param = MonoSingleton<GameManager>.Instance.FindQuest(GlobalVars.SelectedQuestID);
                if ((((param != null) && (param.IsMulti != null)) && ((param.playerNum >= 1) && (param.unitNum >= 1))) && (((param.unitNum <= 11) && (param.map != null)) && (param.map.Count > 0)))
                {
                    goto Label_0092;
                }
                DebugUtility.Log("illegal iname:" + GlobalVars.SelectedQuestID);
                self.IllegalQuest();
                return;
            Label_0092:
                DebugUtility.Log("CreateRoom quest:" + param.iname + " desc:" + param.name);
                self.mJoinPlayerParam = JSON_MyPhotonPlayerParam.Create(0, 0);
                if (self.mJoinPlayerParam != null)
                {
                    goto Label_00D1;
                }
                self.FailureLobby();
                return;
            Label_00D1:
                param2 = new JSON_MyPhotonRoomParam();
                param2.creatorName = MonoSingleton<GameManager>.Instance.Player.Name;
                param2.creatorLV = MonoSingleton<GameManager>.Instance.Player.CalcLevel();
                param2.creatorFUID = JSON_MyPhotonRoomParam.GetMyCreatorFUID();
                param2.roomid = GlobalVars.SelectedMultiPlayRoomID;
                param2.comment = GlobalVars.SelectedMultiPlayRoomComment;
                param2.passCode = GlobalVars.EditMultiPlayRoomPassCode;
                param2.iname = GlobalVars.SelectedQuestID;
                param2.type = GlobalVars.SelectedMultiPlayRoomType;
                param2.isLINE = (self.IsLINE == null) ? 0 : 1;
                param2.unitlv = (GlobalVars.SelectedMultiPlayLimit == null) ? 0 : GlobalVars.MultiPlayJoinUnitLv;
                objArray1 = new object[] { 
                    "create isLINE:", (int) param2.isLINE, " iname:", param2.iname, " roomid:", (int) param2.roomid, " appID:", GlobalVars.SelectedMultiPlayPhotonAppID, " token:", GlobalVars.SelectedMultiPlayRoomName, " comment:", param2.comment, " pass:", param2.passCode, " type:", (int) param2.type,
                    " json:", param2.Serialize()
                };
                DebugUtility.Log(string.Concat(objArray1));
                if (photon.CreateRoom(param.playerNum, GlobalVars.SelectedMultiPlayRoomName, param2.Serialize(), self.mJoinPlayerParam.Serialize(), null, -1, -1, null, null, -1, 0) != null)
                {
                    goto Label_0270;
                }
                self.FailureLobby();
                return;
            Label_0270:
                return;
            }

            public override void End(FlowNode_MultiPlayJoinRoom self)
            {
            }

            public override void Update(FlowNode_MultiPlayJoinRoom self)
            {
                MyPhoton photon;
                MyPhoton.MyState state;
                float num;
                if (self.get_enabled() != null)
                {
                    goto Label_000C;
                }
                return;
            Label_000C:
                photon = PunMonoSingleton<MyPhoton>.Instance;
                state = photon.CurrentState;
                if (state != 3)
                {
                    goto Label_0021;
                }
                return;
            Label_0021:
                if (state != 2)
                {
                    goto Label_005F;
                }
                DebugUtility.Log("[PUN]create room failed, back to lobby.");
                if (photon.LastError == null)
                {
                    goto Label_0058;
                }
                DebugUtility.Log(((MyPhoton.MyError) photon.LastError).ToString());
                photon.ResetLastError();
            Label_0058:
                self.Failure();
                return;
            Label_005F:
                if (state == 4)
                {
                    goto Label_0077;
                }
                self.Failure();
                DebugUtility.Log("[PUN]create room failed, error.");
                return;
            Label_0077:
                num = Time.get_realtimeSinceStartup() - FlowNode_MultiPlayAPI.RoomMakeTime;
                if (num <= 25f)
                {
                    goto Label_009F;
                }
                self.Failure();
                DebugUtility.Log("[PUN]create room too late, give up.");
                return;
            Label_009F:
                self.GotoState<FlowNode_MultiPlayJoinRoom.State_DecidePlayerIndex>();
                return;
            }
        }

        private class State_DecidePlayerIndex : State<FlowNode_MultiPlayJoinRoom>
        {
            public State_DecidePlayerIndex()
            {
                base..ctor();
                return;
            }

            public override void Begin(FlowNode_MultiPlayJoinRoom self)
            {
            }

            public override void End(FlowNode_MultiPlayJoinRoom self)
            {
            }

            public override unsafe void Update(FlowNode_MultiPlayJoinRoom self)
            {
                object[] objArray2;
                object[] objArray1;
                MyPhoton photon;
                MyPhoton.MyState state;
                MyPhoton.MyPlayer player;
                List<MyPhoton.MyPlayer> list;
                MyPhoton.MyPlayer player2;
                List<MyPhoton.MyPlayer>.Enumerator enumerator;
                JSON_MyPhotonPlayerParam param;
                MyPhoton.MyRoom room;
                bool[] flagArray;
                int num;
                MyPhoton.MyPlayer player3;
                List<MyPhoton.MyPlayer>.Enumerator enumerator2;
                JSON_MyPhotonPlayerParam param2;
                int num2;
                int num3;
                PhotonPlayer player4;
                PhotonPlayer[] playerArray;
                int num4;
                string str;
                MyPhoton.MyPlayer player5;
                Hashtable hashtable;
                photon = PunMonoSingleton<MyPhoton>.Instance;
                if (photon.CurrentState == 4)
                {
                    goto Label_001B;
                }
                self.Failure();
                return;
            Label_001B:
                player = photon.GetMyPlayer();
                list = photon.GetRoomPlayerList();
                enumerator = list.GetEnumerator();
            Label_0031:
                try
                {
                    goto Label_00AE;
                Label_0036:
                    player2 = &enumerator.Current;
                    param = JSON_MyPhotonPlayerParam.Parse(player2.json);
                    if (player2.playerID >= player.playerID)
                    {
                        goto Label_00AE;
                    }
                    if (param.playerIndex > 0)
                    {
                        goto Label_00AE;
                    }
                    objArray1 = new object[] { "[PUN]waiting for player index turn...", (int) player2.playerID, " me:", (int) player.playerID };
                    DebugUtility.Log(string.Concat(objArray1));
                    goto Label_032F;
                Label_00AE:
                    if (&enumerator.MoveNext() != null)
                    {
                        goto Label_0036;
                    }
                    goto Label_00CC;
                }
                finally
                {
                Label_00BF:
                    ((List<MyPhoton.MyPlayer>.Enumerator) enumerator).Dispose();
                }
            Label_00CC:
                room = photon.GetCurrentRoom();
                if (room.maxPlayers != null)
                {
                    goto Label_0108;
                }
                Debug.LogError("Invalid Room:" + room.name);
                PhotonNetwork.room.IsOpen = 0;
                self.Failure();
                return;
            Label_0108:
                flagArray = new bool[room.maxPlayers];
                num = 0;
                goto Label_012A;
            Label_011E:
                flagArray[num] = 0;
                num += 1;
            Label_012A:
                if (num < ((int) flagArray.Length))
                {
                    goto Label_011E;
                }
                enumerator2 = list.GetEnumerator();
            Label_013D:
                try
                {
                    goto Label_01CB;
                Label_0142:
                    player3 = &enumerator2.Current;
                    param2 = JSON_MyPhotonPlayerParam.Parse(player3.json);
                    if (player3.playerID >= player.playerID)
                    {
                        goto Label_01CB;
                    }
                    if (param2.playerIndex <= 0)
                    {
                        goto Label_01CB;
                    }
                    flagArray[param2.playerIndex - 1] = 1;
                    objArray2 = new object[] { "[PUN]player index ", (int) param2.playerIndex, " is used. (room:", (int) room.maxPlayers, ")" };
                    DebugUtility.Log(string.Concat(objArray2));
                Label_01CB:
                    if (&enumerator2.MoveNext() != null)
                    {
                        goto Label_0142;
                    }
                    goto Label_01E9;
                }
                finally
                {
                Label_01DC:
                    ((List<MyPhoton.MyPlayer>.Enumerator) enumerator2).Dispose();
                }
            Label_01E9:
                num2 = 0;
                goto Label_0321;
            Label_01F1:
                if (flagArray[num2] == null)
                {
                    goto Label_0200;
                }
                goto Label_031B;
            Label_0200:
                num3 = num2 + 1;
                DebugUtility.Log("[PUN]empty player index found: " + ((int) num3));
                if (photon.IsMultiVersus == null)
                {
                    goto Label_02CD;
                }
                if (num3 < 3)
                {
                    goto Label_02CD;
                }
                playerArray = PhotonNetwork.playerList;
                num4 = 0;
                goto Label_02B8;
            Label_023E:
                player4 = playerArray[num4];
                player5 = new MyPhoton.MyPlayer();
                player5.playerID = player4.ID;
                hashtable = player4.CustomProperties;
                if (hashtable == null)
                {
                    goto Label_02B2;
                }
                if (hashtable.Count <= 0)
                {
                    goto Label_02B2;
                }
                if (hashtable.ContainsKey("json") == null)
                {
                    goto Label_02B2;
                }
                GameUtility.Binary2Object<string>(&str, (byte[]) hashtable.get_Item("json"));
                Debug.Log("player json : " + str);
            Label_02B2:
                num4 += 1;
            Label_02B8:
                if (num4 < ((int) playerArray.Length))
                {
                    goto Label_023E;
                }
                Debug.LogError("MultiVersus is playerindex over : 3");
            Label_02CD:
                self.mJoinPlayerParam.playerID = player.playerID;
                self.mJoinPlayerParam.playerIndex = num3;
                self.mJoinPlayerParam.UpdateMultiTowerPlacement(1);
                photon.SetMyPlayerParam(self.mJoinPlayerParam.Serialize());
                photon.MyPlayerIndex = num3;
                self.Success();
                goto Label_032F;
            Label_031B:
                num2 += 1;
            Label_0321:
                if (num2 < room.maxPlayers)
                {
                    goto Label_01F1;
                }
            Label_032F:
                return;
            }
        }

        private class State_JoinRoom : State<FlowNode_MultiPlayJoinRoom>
        {
            public State_JoinRoom()
            {
                base..ctor();
                return;
            }

            public override void Begin(FlowNode_MultiPlayJoinRoom self)
            {
                object[] objArray1;
                MyPhoton photon;
                QuestParam param;
                photon = PunMonoSingleton<MyPhoton>.Instance;
                param = null;
                if (string.IsNullOrEmpty(GlobalVars.SelectedMultiPlayRoomName) == null)
                {
                    goto Label_001E;
                }
                self.FailureLobby();
                return;
            Label_001E:
                param = MonoSingleton<GameManager>.Instance.FindQuest(GlobalVars.SelectedQuestID);
                if (param != null)
                {
                    goto Label_004F;
                }
                DebugUtility.Log("illegal iname:" + GlobalVars.SelectedQuestID);
                self.IllegalQuest();
                return;
            Label_004F:
                if (GlobalVars.SelectedMultiPlayRoomType != null)
                {
                    goto Label_0080;
                }
                if (param.IsVersus == null)
                {
                    goto Label_006F;
                }
                GlobalVars.SelectedMultiPlayRoomType = 1;
                goto Label_0080;
            Label_006F:
                if (param.IsMultiTower == null)
                {
                    goto Label_0080;
                }
                GlobalVars.SelectedMultiPlayRoomType = 2;
            Label_0080:
                self.mJoinPlayerParam = JSON_MyPhotonPlayerParam.Create(0, 0);
                if (self.mJoinPlayerParam != null)
                {
                    goto Label_009F;
                }
                self.FailureLobby();
                return;
            Label_009F:
                objArray1 = new object[] { "Joining name:", GlobalVars.SelectedMultiPlayRoomName, " pnum:", (OShort) param.playerNum, " unum:", (OShort) param.unitNum };
                DebugUtility.Log(string.Concat(objArray1));
                if (photon.JoinRoom(GlobalVars.SelectedMultiPlayRoomName, self.mJoinPlayerParam.Serialize(), (GlobalVars.ResumeMultiplayPlayerID == 0) == 0) != null)
                {
                    goto Label_0132;
                }
                DebugUtility.Log("error:" + ((MyPhoton.MyError) photon.LastError));
                self.FailureLobby();
                return;
            Label_0132:
                return;
            }

            public override void End(FlowNode_MultiPlayJoinRoom self)
            {
            }

            public override void Update(FlowNode_MultiPlayJoinRoom self)
            {
                MyPhoton photon;
                MyPhoton.MyState state;
                if (self.get_enabled() != null)
                {
                    goto Label_000C;
                }
                return;
            Label_000C:
                photon = PunMonoSingleton<MyPhoton>.Instance;
                state = photon.CurrentState;
                if (state != 3)
                {
                    goto Label_0021;
                }
                return;
            Label_0021:
                if (state != 2)
                {
                    goto Label_0060;
                }
                DebugUtility.Log("[PUN]joining failed, back to lobby." + ((MyPhoton.MyError) photon.LastError));
                if (photon.LastError != 7)
                {
                    goto Label_0059;
                }
                self.FailureFullMember();
                goto Label_005F;
            Label_0059:
                self.FailureLobby();
            Label_005F:
                return;
            Label_0060:
                if (state != 4)
                {
                    goto Label_006E;
                }
                self.GotoState<FlowNode_MultiPlayJoinRoom.State_DecidePlayerIndex>();
                return;
            Label_006E:
                self.Failure();
                DebugUtility.Log("[PUN]joining failed, error.");
                return;
            }
        }

        private class State_ResumeTowerJoinRoom : State<FlowNode_MultiPlayJoinRoom>
        {
            public State_ResumeTowerJoinRoom()
            {
                base..ctor();
                return;
            }

            public override void Begin(FlowNode_MultiPlayJoinRoom self)
            {
                object[] objArray1;
                MyPhoton photon;
                QuestParam param;
                photon = PunMonoSingleton<MyPhoton>.Instance;
                param = null;
                if (string.IsNullOrEmpty(GlobalVars.SelectedMultiPlayRoomName) == null)
                {
                    goto Label_001E;
                }
                self.FailureLobby();
                return;
            Label_001E:
                param = MonoSingleton<GameManager>.Instance.FindQuest(GlobalVars.SelectedQuestID);
                if (param == null)
                {
                    goto Label_008F;
                }
                if (param.IsMulti == null)
                {
                    goto Label_008F;
                }
                if (param.playerNum < 1)
                {
                    goto Label_008F;
                }
                if (param.unitNum < 1)
                {
                    goto Label_008F;
                }
                if (param.unitNum > 11)
                {
                    goto Label_008F;
                }
                if (param.map == null)
                {
                    goto Label_008F;
                }
                if (param.map.Count > 0)
                {
                    goto Label_00AA;
                }
            Label_008F:
                DebugUtility.Log("illegal iname:" + GlobalVars.SelectedQuestID);
                self.IllegalQuest();
                return;
            Label_00AA:
                if (photon.CheckTowerRoomIsBattle(GlobalVars.SelectedMultiPlayRoomName) != null)
                {
                    goto Label_00C1;
                }
                self.FailureLobby();
                return;
            Label_00C1:
                self.mJoinPlayerParam = JSON_MyPhotonPlayerParam.Create(0, 0);
                if (self.mJoinPlayerParam != null)
                {
                    goto Label_00E0;
                }
                self.FailureLobby();
                return;
            Label_00E0:
                objArray1 = new object[] { "Joining name:", GlobalVars.SelectedMultiPlayRoomName, " pnum:", (OShort) param.playerNum, " unum:", (OShort) param.unitNum };
                DebugUtility.Log(string.Concat(objArray1));
                if (photon.JoinRoom(GlobalVars.SelectedMultiPlayRoomName, self.mJoinPlayerParam.Serialize(), (GlobalVars.ResumeMultiplayPlayerID == 0) == 0) != null)
                {
                    goto Label_0173;
                }
                DebugUtility.Log("error:" + ((MyPhoton.MyError) photon.LastError));
                self.FailureLobby();
                return;
            Label_0173:
                return;
            }

            public override void End(FlowNode_MultiPlayJoinRoom self)
            {
            }

            public override void Update(FlowNode_MultiPlayJoinRoom self)
            {
                MyPhoton photon;
                MyPhoton.MyState state;
                if (self.get_enabled() != null)
                {
                    goto Label_000C;
                }
                return;
            Label_000C:
                photon = PunMonoSingleton<MyPhoton>.Instance;
                state = photon.CurrentState;
                if (state != 3)
                {
                    goto Label_0021;
                }
                return;
            Label_0021:
                if (state != 2)
                {
                    goto Label_0060;
                }
                DebugUtility.Log("[PUN]joining failed, back to lobby." + ((MyPhoton.MyError) photon.LastError));
                if (photon.LastError != 7)
                {
                    goto Label_0059;
                }
                self.FailureFullMember();
                goto Label_005F;
            Label_0059:
                self.FailureLobby();
            Label_005F:
                return;
            Label_0060:
                if (state != 4)
                {
                    goto Label_006E;
                }
                self.GotoState<FlowNode_MultiPlayJoinRoom.State_DecidePlayerIndex>();
                return;
            Label_006E:
                self.Failure();
                DebugUtility.Log("[PUN]joining failed, error.");
                return;
            }
        }

        private class State_TowerCreate : State<FlowNode_MultiPlayJoinRoom>
        {
            public State_TowerCreate()
            {
                base..ctor();
                return;
            }

            public override void Begin(FlowNode_MultiPlayJoinRoom self)
            {
                MyPhoton photon;
                JSON_MyPhotonRoomParam param;
                QuestParam param2;
                MultiTowerFloorParam param3;
                int num;
                string str;
                photon = PunMonoSingleton<MyPhoton>.Instance;
                self.mJoinPlayerParam = JSON_MyPhotonPlayerParam.Create(0, 0);
                if (self.mJoinPlayerParam != null)
                {
                    goto Label_0025;
                }
                self.FailureLobby();
                return;
            Label_0025:
                param = new JSON_MyPhotonRoomParam();
                param.creatorName = MonoSingleton<GameManager>.Instance.Player.Name;
                param.creatorLV = MonoSingleton<GameManager>.Instance.Player.CalcLevel();
                param.creatorFUID = JSON_MyPhotonRoomParam.GetMyCreatorFUID();
                param.roomid = GlobalVars.SelectedMultiPlayRoomID;
                param.comment = GlobalVars.SelectedMultiPlayRoomComment;
                param.passCode = GlobalVars.EditMultiPlayRoomPassCode;
                param.iname = GlobalVars.SelectedQuestID;
                param.type = GlobalVars.SelectedMultiPlayRoomType;
                param.isLINE = (self.IsLINE == null) ? 0 : 1;
                param2 = MonoSingleton<GameManager>.Instance.FindQuest(GlobalVars.SelectedQuestID);
                param3 = MonoSingleton<GameManager>.Instance.GetMTFloorParam(param2.iname);
                num = GlobalVars.SelectedMultiTowerFloor;
                str = MonoSingleton<GameManager>.Instance.DeviceId;
                if (photon.CreateRoom(param2.playerNum, GlobalVars.SelectedMultiPlayRoomName, param.Serialize(), self.mJoinPlayerParam.Serialize(), param3.tower_id, num, -1, null, str, -1, 1) != null)
                {
                    goto Label_0124;
                }
                self.FailureLobby();
                return;
            Label_0124:
                return;
            }

            public override void End(FlowNode_MultiPlayJoinRoom self)
            {
            }

            public override void Update(FlowNode_MultiPlayJoinRoom self)
            {
                MyPhoton photon;
                MyPhoton.MyState state;
                float num;
                if (self.get_enabled() != null)
                {
                    goto Label_000C;
                }
                return;
            Label_000C:
                photon = PunMonoSingleton<MyPhoton>.Instance;
                state = photon.CurrentState;
                if (state != 3)
                {
                    goto Label_0021;
                }
                return;
            Label_0021:
                if (state != 2)
                {
                    goto Label_005F;
                }
                DebugUtility.Log("[PUN]create room failed, back to lobby.");
                if (photon.LastError == null)
                {
                    goto Label_0058;
                }
                DebugUtility.Log(((MyPhoton.MyError) photon.LastError).ToString());
                photon.ResetLastError();
            Label_0058:
                self.Failure();
                return;
            Label_005F:
                if (state == 4)
                {
                    goto Label_0077;
                }
                self.Failure();
                DebugUtility.Log("[PUN]create room failed, error.");
                return;
            Label_0077:
                num = Time.get_realtimeSinceStartup() - FlowNode_MultiPlayAPI.RoomMakeTime;
                if (num <= 25f)
                {
                    goto Label_009F;
                }
                self.Failure();
                DebugUtility.Log("[PUN]create room too late, give up.");
                return;
            Label_009F:
                self.GotoState<FlowNode_MultiPlayJoinRoom.State_DecidePlayerIndex>();
                return;
            }
        }

        private class State_TowerJoin : State<FlowNode_MultiPlayJoinRoom>
        {
            public State_TowerJoin()
            {
                base..ctor();
                return;
            }

            public override void Begin(FlowNode_MultiPlayJoinRoom self)
            {
                MyPhoton photon;
                GameManager manager;
                QuestParam param;
                MultiTowerFloorParam param2;
                photon = PunMonoSingleton<MyPhoton>.Instance;
                self.mJoinPlayerParam = JSON_MyPhotonPlayerParam.Create(0, 0);
                if (self.mJoinPlayerParam != null)
                {
                    goto Label_0025;
                }
                self.FailureLobby();
                return;
            Label_0025:
                manager = MonoSingleton<GameManager>.Instance;
                param = manager.FindQuest(GlobalVars.SelectedQuestID);
                param2 = manager.GetMTFloorParam(GlobalVars.SelectedQuestID);
                if (photon.JoinRandomRoom((byte) param.playerNum, self.mJoinPlayerParam.Serialize(), param2.tower_id, null, GlobalVars.SelectedMultiTowerFloor, 1, self.mJoinPlayerParam.UID) != null)
                {
                    goto Label_009E;
                }
                DebugUtility.Log("error:" + ((MyPhoton.MyError) photon.LastError));
                self.FailureLobby();
                return;
            Label_009E:
                return;
            }

            public override void End(FlowNode_MultiPlayJoinRoom self)
            {
            }

            public override void Update(FlowNode_MultiPlayJoinRoom self)
            {
                MyPhoton photon;
                MyPhoton.MyState state;
                photon = PunMonoSingleton<MyPhoton>.Instance;
                state = photon.CurrentState;
                if (self.get_enabled() != null)
                {
                    goto Label_0019;
                }
                return;
            Label_0019:
                if (state != 3)
                {
                    goto Label_0021;
                }
                return;
            Label_0021:
                if (state != 2)
                {
                    goto Label_0060;
                }
                DebugUtility.Log("[PUN]joining failed, back to lobby." + ((MyPhoton.MyError) photon.LastError));
                if (photon.LastError != 7)
                {
                    goto Label_0059;
                }
                self.FailureFullMember();
                goto Label_005F;
            Label_0059:
                self.FailureLobby();
            Label_005F:
                return;
            Label_0060:
                if (state != 4)
                {
                    goto Label_007E;
                }
                GlobalVars.SelectedMultiPlayRoomName = photon.GetCurrentRoom().name;
                self.GotoState<FlowNode_MultiPlayJoinRoom.State_DecidePlayerIndex>();
                return;
            Label_007E:
                self.Failure();
                return;
            }
        }

        private class State_VersusCreate : State<FlowNode_MultiPlayJoinRoom>
        {
            public State_VersusCreate()
            {
                base..ctor();
                return;
            }

            public override void Begin(FlowNode_MultiPlayJoinRoom self)
            {
                GameManager manager;
                MyPhoton photon;
                JSON_MyPhotonRoomParam param;
                int num;
                int num2;
                int num3;
                string str;
                string str2;
                manager = MonoSingleton<GameManager>.Instance;
                photon = PunMonoSingleton<MyPhoton>.Instance;
                self.mJoinPlayerParam = JSON_MyPhotonPlayerParam.Create(0, 0);
                if (self.mJoinPlayerParam != null)
                {
                    goto Label_002B;
                }
                self.FailureLobby();
                return;
            Label_002B:
                param = new JSON_MyPhotonRoomParam();
                param.creatorName = MonoSingleton<GameManager>.Instance.Player.Name;
                param.creatorLV = MonoSingleton<GameManager>.Instance.Player.CalcLevel();
                param.creatorFUID = JSON_MyPhotonRoomParam.GetMyCreatorFUID();
                param.roomid = GlobalVars.SelectedMultiPlayRoomID;
                param.comment = GlobalVars.SelectedMultiPlayRoomComment;
                param.passCode = GlobalVars.EditMultiPlayRoomPassCode;
                param.iname = GlobalVars.SelectedQuestID;
                param.type = GlobalVars.SelectedMultiPlayRoomType;
                param.isLINE = (self.IsLINE == null) ? 0 : 1;
                param.vsmode = (manager.GetVSMode(-1L) != null) ? 1 : 0;
                param.draft_type = (GlobalVars.IsVersusDraftMode == null) ? 0 : 1;
                num = -1;
                num2 = -1;
                num3 = MonoSingleton<GameManager>.Instance.MasterParam.FixParam.AudienceMax;
                str = null;
                str2 = null;
                if (GlobalVars.SelectedMultiPlayVersusType != 1)
                {
                    goto Label_0147;
                }
                num = param.creatorLV;
                num2 = MonoSingleton<GameManager>.Instance.Player.VersusTowerFloor;
                str = MonoSingleton<GameManager>.Instance.DeviceId;
                str2 = MonoSingleton<GameManager>.Instance.VersusLastUid;
            Label_0147:
                if (photon.CreateRoom(self.VERSUS_PLAYER_MAX, GlobalVars.SelectedMultiPlayRoomName, param.Serialize(), self.mJoinPlayerParam.Serialize(), GlobalVars.MultiPlayVersusKey, num2, num, str2, str, num3, 0) != null)
                {
                    goto Label_0184;
                }
                self.FailureLobby();
                return;
            Label_0184:
                return;
            }

            public override void End(FlowNode_MultiPlayJoinRoom self)
            {
            }

            public override void Update(FlowNode_MultiPlayJoinRoom self)
            {
                MyPhoton photon;
                MyPhoton.MyState state;
                float num;
                if (self.get_enabled() != null)
                {
                    goto Label_000C;
                }
                return;
            Label_000C:
                photon = PunMonoSingleton<MyPhoton>.Instance;
                state = photon.CurrentState;
                if (state != 3)
                {
                    goto Label_0021;
                }
                return;
            Label_0021:
                if (state != 2)
                {
                    goto Label_005F;
                }
                DebugUtility.Log("[PUN]create room failed, back to lobby.");
                if (photon.LastError == null)
                {
                    goto Label_0058;
                }
                DebugUtility.Log(((MyPhoton.MyError) photon.LastError).ToString());
                photon.ResetLastError();
            Label_0058:
                self.Failure();
                return;
            Label_005F:
                if (state == 4)
                {
                    goto Label_0077;
                }
                self.Failure();
                DebugUtility.Log("[PUN]create room failed, error.");
                return;
            Label_0077:
                num = Time.get_realtimeSinceStartup() - FlowNode_MultiPlayAPI.RoomMakeTime;
                if (num <= 25f)
                {
                    goto Label_009F;
                }
                self.Failure();
                DebugUtility.Log("[PUN]create room too late, give up.");
                return;
            Label_009F:
                self.GotoState<FlowNode_MultiPlayJoinRoom.State_DecidePlayerIndex>();
                return;
            }
        }

        private class State_VersusJoin : State<FlowNode_MultiPlayJoinRoom>
        {
            public State_VersusJoin()
            {
                base..ctor();
                return;
            }

            public override void Begin(FlowNode_MultiPlayJoinRoom self)
            {
                MyPhoton photon;
                photon = PunMonoSingleton<MyPhoton>.Instance;
                self.mJoinPlayerParam = JSON_MyPhotonPlayerParam.Create(0, 0);
                if (self.mJoinPlayerParam != null)
                {
                    goto Label_0025;
                }
                self.FailureLobby();
                return;
            Label_0025:
                if (photon.JoinRandomRoom(self.VERSUS_PLAYER_MAX, self.mJoinPlayerParam.Serialize(), GlobalVars.MultiPlayVersusKey, GlobalVars.SelectedMultiPlayRoomName, -1, -1, null) != null)
                {
                    goto Label_006F;
                }
                DebugUtility.Log("error:" + ((MyPhoton.MyError) photon.LastError));
                self.FailureLobby();
                return;
            Label_006F:
                return;
            }

            public override void End(FlowNode_MultiPlayJoinRoom self)
            {
            }

            public override void Update(FlowNode_MultiPlayJoinRoom self)
            {
                MyPhoton photon;
                MyPhoton.MyState state;
                photon = PunMonoSingleton<MyPhoton>.Instance;
                state = photon.CurrentState;
                if (self.get_enabled() != null)
                {
                    goto Label_0019;
                }
                return;
            Label_0019:
                if (state != 3)
                {
                    goto Label_0021;
                }
                return;
            Label_0021:
                if (state != 2)
                {
                    goto Label_0060;
                }
                DebugUtility.Log("[PUN]joining failed, back to lobby." + ((MyPhoton.MyError) photon.LastError));
                if (photon.LastError != 7)
                {
                    goto Label_0059;
                }
                self.FailureFullMember();
                goto Label_005F;
            Label_0059:
                self.FailureLobby();
            Label_005F:
                return;
            Label_0060:
                if (state != 4)
                {
                    goto Label_007E;
                }
                GlobalVars.SelectedMultiPlayRoomName = photon.GetCurrentRoom().name;
                self.GotoState<FlowNode_MultiPlayJoinRoom.State_DecidePlayerIndex>();
                return;
            Label_007E:
                self.Failure();
                return;
            }
        }

        private class State_VersusRankCreate : State<FlowNode_MultiPlayJoinRoom>
        {
            public State_VersusRankCreate()
            {
                base..ctor();
                return;
            }

            public override void Begin(FlowNode_MultiPlayJoinRoom self)
            {
                GameManager manager;
                MyPhoton photon;
                JSON_MyPhotonRoomParam param;
                int num;
                string str;
                int num2;
                int num3;
                manager = MonoSingleton<GameManager>.Instance;
                photon = PunMonoSingleton<MyPhoton>.Instance;
                self.mJoinPlayerParam = JSON_MyPhotonPlayerParam.Create(0, 0);
                if (self.mJoinPlayerParam != null)
                {
                    goto Label_002B;
                }
                self.FailureLobby();
                return;
            Label_002B:
                param = new JSON_MyPhotonRoomParam();
                param.creatorName = MonoSingleton<GameManager>.Instance.Player.Name;
                param.creatorLV = MonoSingleton<GameManager>.Instance.Player.CalcLevel();
                param.creatorFUID = JSON_MyPhotonRoomParam.GetMyCreatorFUID();
                param.roomid = GlobalVars.SelectedMultiPlayRoomID;
                param.comment = GlobalVars.SelectedMultiPlayRoomComment;
                param.passCode = GlobalVars.EditMultiPlayRoomPassCode;
                param.iname = GlobalVars.SelectedQuestID;
                param.type = GlobalVars.SelectedMultiPlayRoomType;
                param.isLINE = (self.IsLINE == null) ? 0 : 1;
                param.vsmode = (manager.GetVSMode(-1L) != null) ? 1 : 0;
                num = param.creatorLV;
                str = MonoSingleton<GameManager>.Instance.DeviceId;
                num2 = MonoSingleton<GameManager>.Instance.Player.RankMatchScore;
                num3 = MonoSingleton<GameManager>.Instance.Player.RankMatchClass;
                if (photon.CreateRoom(GlobalVars.SelectedMultiPlayRoomName, param.Serialize(), self.mJoinPlayerParam.Serialize(), num, str, num2, num3) != null)
                {
                    goto Label_0133;
                }
                self.FailureLobby();
                return;
            Label_0133:
                return;
            }

            public override void End(FlowNode_MultiPlayJoinRoom self)
            {
            }

            public override void Update(FlowNode_MultiPlayJoinRoom self)
            {
                MyPhoton photon;
                MyPhoton.MyState state;
                float num;
                if (self.get_enabled() != null)
                {
                    goto Label_000C;
                }
                return;
            Label_000C:
                photon = PunMonoSingleton<MyPhoton>.Instance;
                state = photon.CurrentState;
                if (state != 3)
                {
                    goto Label_0021;
                }
                return;
            Label_0021:
                if (state != 2)
                {
                    goto Label_005F;
                }
                DebugUtility.Log("[PUN]create room failed, back to lobby.");
                if (photon.LastError == null)
                {
                    goto Label_0058;
                }
                DebugUtility.Log(((MyPhoton.MyError) photon.LastError).ToString());
                photon.ResetLastError();
            Label_0058:
                self.Failure();
                return;
            Label_005F:
                if (state == 4)
                {
                    goto Label_0077;
                }
                self.Failure();
                DebugUtility.Log("[PUN]create room failed, error.");
                return;
            Label_0077:
                num = Time.get_realtimeSinceStartup() - FlowNode_MultiPlayAPI.RoomMakeTime;
                if (num <= 25f)
                {
                    goto Label_009F;
                }
                self.Failure();
                DebugUtility.Log("[PUN]create room too late, give up.");
                return;
            Label_009F:
                self.GotoState<FlowNode_MultiPlayJoinRoom.State_DecidePlayerIndex>();
                return;
            }
        }

        private class State_VersusRankJoin : State<FlowNode_MultiPlayJoinRoom>
        {
            public State_VersusRankJoin()
            {
                base..ctor();
                return;
            }

            public override void Begin(FlowNode_MultiPlayJoinRoom self)
            {
                MyPhoton photon;
                int num;
                int num2;
                int num3;
                int num4;
                string str;
                photon = PunMonoSingleton<MyPhoton>.Instance;
                self.mJoinPlayerParam = JSON_MyPhotonPlayerParam.Create(0, 0);
                if (self.mJoinPlayerParam != null)
                {
                    goto Label_0025;
                }
                self.FailureLobby();
                return;
            Label_0025:
                num = -1;
                num2 = MonoSingleton<GameManager>.Instance.Player.CalcLevel();
                num3 = MonoSingleton<GameManager>.Instance.Player.RankMatchScore;
                num4 = MonoSingleton<GameManager>.Instance.Player.RankMatchClass;
                str = MonoSingleton<GameManager>.Instance.DeviceId;
                if (photon.JoinRankMatchRoomCheckParam(self.mJoinPlayerParam.Serialize(), num2, num, str, num3, 900, 100, num4, MonoSingleton<GameManager>.Instance.RankMatchMatchedEnemies) != null)
                {
                    goto Label_00B3;
                }
                DebugUtility.Log("error:" + ((MyPhoton.MyError) photon.LastError));
                self.FailureLobby();
                return;
            Label_00B3:
                return;
            }

            public override void End(FlowNode_MultiPlayJoinRoom self)
            {
            }

            public override void Update(FlowNode_MultiPlayJoinRoom self)
            {
                MyPhoton photon;
                MyPhoton.MyState state;
                photon = PunMonoSingleton<MyPhoton>.Instance;
                state = photon.CurrentState;
                if (self.get_enabled() != null)
                {
                    goto Label_0019;
                }
                return;
            Label_0019:
                if (state != 3)
                {
                    goto Label_0021;
                }
                return;
            Label_0021:
                if (state != 2)
                {
                    goto Label_0060;
                }
                DebugUtility.Log("[PUN]joining failed, back to lobby." + ((MyPhoton.MyError) photon.LastError));
                if (photon.LastError != 7)
                {
                    goto Label_0059;
                }
                self.FailureFullMember();
                goto Label_005F;
            Label_0059:
                self.FailureLobby();
            Label_005F:
                return;
            Label_0060:
                if (state != 4)
                {
                    goto Label_007E;
                }
                GlobalVars.SelectedMultiPlayRoomName = photon.GetCurrentRoom().name;
                self.GotoState<FlowNode_MultiPlayJoinRoom.State_DecidePlayerIndex>();
                return;
            Label_007E:
                self.Failure();
                return;
            }
        }

        private class State_VersusTowerJoin : State<FlowNode_MultiPlayJoinRoom>
        {
            public State_VersusTowerJoin()
            {
                base..ctor();
                return;
            }

            public override unsafe void Begin(FlowNode_MultiPlayJoinRoom self)
            {
                MyPhoton photon;
                int num;
                int num2;
                int num3;
                int num4;
                string str;
                string str2;
                photon = PunMonoSingleton<MyPhoton>.Instance;
                self.mJoinPlayerParam = JSON_MyPhotonPlayerParam.Create(0, 0);
                if (self.mJoinPlayerParam != null)
                {
                    goto Label_0025;
                }
                self.FailureLobby();
                return;
            Label_0025:
                num = -1;
                num2 = -1;
                num3 = MonoSingleton<GameManager>.Instance.Player.CalcLevel();
                num4 = MonoSingleton<GameManager>.Instance.Player.VersusTowerFloor;
                str = MonoSingleton<GameManager>.Instance.VersusLastUid;
                str2 = MonoSingleton<GameManager>.Instance.DeviceId;
                MonoSingleton<GameManager>.Instance.GetRankMatchCondition(&num, &num2);
                if (photon.JoinRoomCheckParam(GlobalVars.MultiPlayVersusKey, self.mJoinPlayerParam.Serialize(), num, num2, num3, num4, str, str2) != null)
                {
                    goto Label_00B5;
                }
                DebugUtility.Log("error:" + ((MyPhoton.MyError) photon.LastError));
                self.FailureLobby();
                return;
            Label_00B5:
                return;
            }

            public override void End(FlowNode_MultiPlayJoinRoom self)
            {
            }

            public override void Update(FlowNode_MultiPlayJoinRoom self)
            {
                MyPhoton photon;
                MyPhoton.MyState state;
                photon = PunMonoSingleton<MyPhoton>.Instance;
                state = photon.CurrentState;
                if (self.get_enabled() != null)
                {
                    goto Label_0019;
                }
                return;
            Label_0019:
                if (state != 3)
                {
                    goto Label_0021;
                }
                return;
            Label_0021:
                if (state != 2)
                {
                    goto Label_0060;
                }
                DebugUtility.Log("[PUN]joining failed, back to lobby." + ((MyPhoton.MyError) photon.LastError));
                if (photon.LastError != 7)
                {
                    goto Label_0059;
                }
                self.FailureFullMember();
                goto Label_005F;
            Label_0059:
                self.FailureLobby();
            Label_005F:
                return;
            Label_0060:
                if (state != 4)
                {
                    goto Label_007E;
                }
                GlobalVars.SelectedMultiPlayRoomName = photon.GetCurrentRoom().name;
                self.GotoState<FlowNode_MultiPlayJoinRoom.State_DecidePlayerIndex>();
                return;
            Label_007E:
                self.Failure();
                return;
            }
        }
    }
}

