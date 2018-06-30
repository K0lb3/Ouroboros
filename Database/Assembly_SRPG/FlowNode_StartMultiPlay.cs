namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    [Pin(2, "Failure", 1, 3), Pin(0x65, "ResumeStart", 0, 1), Pin(100, "Start", 0, 0), NodeType("Multi/StartMultiPlay", 0x7fe5), Pin(1, "Success", 1, 2)]
    public class FlowNode_StartMultiPlay : FlowNode
    {
        private StateMachine<FlowNode_StartMultiPlay> mStateMachine;

        public FlowNode_StartMultiPlay()
        {
            base..ctor();
            return;
        }

        private void Failure()
        {
            this.mStateMachine = null;
            base.set_enabled(0);
            base.ActivateOutputLinks(2);
            DebugUtility.Log("StartMultiPlay failure");
            return;
        }

        private void FailureStartMulti()
        {
            MyPhoton photon;
            MyPhoton.MyRoom room;
            JSON_MyPhotonRoomParam param;
            photon = PunMonoSingleton<MyPhoton>.Instance;
            if (((photon != null) == null) || (photon.IsOldestPlayer() == null))
            {
                goto Label_0069;
            }
            room = photon.GetCurrentRoom();
            if (room == null)
            {
                goto Label_0060;
            }
            param = (string.IsNullOrEmpty(room.json) == null) ? JSON_MyPhotonRoomParam.Parse(room.json) : null;
            param.started = 0;
            photon.SetRoomParam(param.Serialize());
        Label_0060:
            photon.OpenRoom(1, 0);
        Label_0069:
            this.Failure();
            return;
        }

        public void GotoState<StateType>() where StateType: State<FlowNode_StartMultiPlay>, new()
        {
            if (this.mStateMachine == null)
            {
                goto Label_0016;
            }
            this.mStateMachine.GotoState<StateType>();
        Label_0016:
            return;
        }

        public override void OnActivate(int pinID)
        {
            if (pinID != 100)
            {
                goto Label_0037;
            }
            if (this.mStateMachine == null)
            {
                goto Label_0014;
            }
            return;
        Label_0014:
            this.mStateMachine = new StateMachine<FlowNode_StartMultiPlay>(this);
            this.mStateMachine.GotoState<State_Start>();
            base.set_enabled(1);
            goto Label_0069;
        Label_0037:
            if (pinID != 0x65)
            {
                goto Label_0069;
            }
            if (this.mStateMachine == null)
            {
                goto Label_004B;
            }
            return;
        Label_004B:
            this.mStateMachine = new StateMachine<FlowNode_StartMultiPlay>(this);
            this.mStateMachine.GotoState<State_ResumeStart>();
            base.set_enabled(1);
        Label_0069:
            return;
        }

        private void Success()
        {
            this.mStateMachine = null;
            base.set_enabled(0);
            base.ActivateOutputLinks(1);
            DebugUtility.Log("StartMultiPlay success");
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

        public class PlayerList
        {
            public JSON_MyPhotonPlayerParam[] players;

            public PlayerList()
            {
                base..ctor();
                return;
            }

            public string Serialize()
            {
                string str;
                bool flag;
                JSON_MyPhotonPlayerParam param;
                JSON_MyPhotonPlayerParam[] paramArray;
                int num;
                str = "{\"players\":[";
                if (this.players == null)
                {
                    goto Label_005D;
                }
                flag = 1;
                paramArray = this.players;
                num = 0;
                goto Label_0053;
            Label_0022:
                param = paramArray[num];
                if (flag == null)
                {
                    goto Label_0034;
                }
                flag = 0;
                goto Label_0040;
            Label_0034:
                str = str + ",";
            Label_0040:
                str = str + param.Serialize();
                num += 1;
            Label_0053:
                if (num < ((int) paramArray.Length))
                {
                    goto Label_0022;
                }
            Label_005D:
                return (str + "]}");
            }
        }

        public class RecvData
        {
            public int senderPlayerID;
            public int version;
            public Json_MyPhotonPlayerBinaryParam[] playerList;
            public string playerListJson;

            public RecvData()
            {
                base..ctor();
                return;
            }
        }

        private class State_GameStart : State<FlowNode_StartMultiPlay>
        {
            private FlowNode_StartMultiPlay.RecvData mSend;
            private List<FlowNode_StartMultiPlay.RecvData> mRecvList;
            private float mWait;
            private bool mConfirm;
            private float mStartWait;
            [CompilerGenerated]
            private static Comparison<JSON_MyPhotonPlayerParam> <>f__am$cache5;

            public State_GameStart()
            {
                this.mSend = new FlowNode_StartMultiPlay.RecvData();
                this.mRecvList = new List<FlowNode_StartMultiPlay.RecvData>();
                base..ctor();
                return;
            }

            [CompilerGenerated]
            private static int <Update>m__1CD(JSON_MyPhotonPlayerParam a, JSON_MyPhotonPlayerParam b)
            {
                return (a.playerIndex - b.playerIndex);
            }

            public override unsafe void Begin(FlowNode_StartMultiPlay self)
            {
                MyPhoton photon;
                List<MyPhoton.MyPlayer> list;
                MyPhoton.MyPlayer player;
                List<MyPhoton.MyPlayer>.Enumerator enumerator;
                JSON_MyPhotonPlayerParam param;
                enumerator = PunMonoSingleton<MyPhoton>.Instance.GetRoomPlayerList().GetEnumerator();
            Label_0014:
                try
                {
                    goto Label_0046;
                Label_0019:
                    player = &enumerator.Current;
                    if (JSON_MyPhotonPlayerParam.Parse(player.json).state == 2)
                    {
                        goto Label_0046;
                    }
                    self.FailureStartMulti();
                    goto Label_0063;
                Label_0046:
                    if (&enumerator.MoveNext() != null)
                    {
                        goto Label_0019;
                    }
                    goto Label_0063;
                }
                finally
                {
                Label_0057:
                    ((List<MyPhoton.MyPlayer>.Enumerator) enumerator).Dispose();
                }
            Label_0063:
                return;
            }

            public override void End(FlowNode_StartMultiPlay self)
            {
            }

            public override unsafe void Update(FlowNode_StartMultiPlay self)
            {
                MyPhoton photon;
                MyPhoton.MyState state;
                MyPhoton.MyRoom room;
                JSON_MyPhotonRoomParam param;
                List<MyPhoton.MyPlayer> list;
                MyPhoton.MyPlayer player;
                List<MyPhoton.MyPlayer>.Enumerator enumerator;
                JSON_MyPhotonPlayerParam param2;
                MyPhoton.MyPlayer player2;
                List<JSON_MyPhotonPlayerParam> list2;
                int num;
                JSON_MyPhotonPlayerParam param3;
                FlowNode_StartMultiPlay.PlayerList list3;
                Json_MyPhotonPlayerBinaryParam[] paramArray;
                int num2;
                byte[] buffer;
                List<MyPhoton.MyEvent> list4;
                int num3;
                FlowNode_StartMultiPlay.RecvData data;
                List<MyPhoton.MyPlayer>.Enumerator enumerator2;
                bool flag;
                FlowNode_StartMultiPlay.RecvData data2;
                List<FlowNode_StartMultiPlay.RecvData>.Enumerator enumerator3;
                int num4;
                List<JSON_MyPhotonPlayerParam> list5;
                FlowNode_StartMultiPlay.PlayerList list6;
                JSON_MyPhotonPlayerParam param4;
                JSON_MyPhotonPlayerParam[] paramArray2;
                int num5;
                JSON_MyPhotonPlayerParam param5;
                <Update>c__AnonStorey27E storeye;
                photon = PunMonoSingleton<MyPhoton>.Instance;
                if (photon.CurrentState == 4)
                {
                    goto Label_001B;
                }
                self.Failure();
                return;
            Label_001B:
                room = photon.GetCurrentRoom();
                if (room != null)
                {
                    goto Label_002F;
                }
                self.Failure();
                return;
            Label_002F:
                param = (string.IsNullOrEmpty(room.json) == null) ? JSON_MyPhotonRoomParam.Parse(room.json) : null;
                if (param != null)
                {
                    goto Label_005E;
                }
                self.Failure();
                return;
            Label_005E:
                if (param.started != null)
                {
                    goto Label_007D;
                }
                param.started = 1;
                photon.SetRoomParam(param.Serialize());
            Label_007D:
                if (this.mStartWait <= 0f)
                {
                    goto Label_00EE;
                }
                this.mStartWait -= Time.get_deltaTime();
                if (this.mStartWait <= 0f)
                {
                    goto Label_00B0;
                }
                return;
            Label_00B0:
                GlobalVars.SelectedQuestID = param.iname;
                GlobalVars.SelectedFriendID = null;
                GlobalVars.SelectedFriend = null;
                GlobalVars.SelectedSupport.Set(null);
                self.Success();
                DebugUtility.Log("StartMultiPlay: " + param.Serialize());
                return;
            Label_00EE:
                if (this.mWait <= 0f)
                {
                    goto Label_0111;
                }
                this.mWait -= Time.get_deltaTime();
                return;
            Label_0111:
                list = photon.GetRoomPlayerList();
                if (param.type == 1)
                {
                    goto Label_0131;
                }
                if (param.type != 3)
                {
                    goto Label_0145;
                }
            Label_0131:
                if (list.Count != 1)
                {
                    goto Label_0145;
                }
                self.FailureStartMulti();
                return;
            Label_0145:
                if (this.mConfirm == null)
                {
                    goto Label_01C9;
                }
                enumerator = list.GetEnumerator();
            Label_0159:
                try
                {
                    goto Label_019F;
                Label_015E:
                    player = &enumerator.Current;
                    param2 = JSON_MyPhotonPlayerParam.Parse(player.json);
                    if (param2.state == 3)
                    {
                        goto Label_0187;
                    }
                    goto Label_0681;
                Label_0187:
                    if (param2.state >= 2)
                    {
                        goto Label_019F;
                    }
                    self.Failure();
                    goto Label_0681;
                Label_019F:
                    if (&enumerator.MoveNext() != null)
                    {
                        goto Label_015E;
                    }
                    goto Label_01BD;
                }
                finally
                {
                Label_01B0:
                    ((List<MyPhoton.MyPlayer>.Enumerator) enumerator).Dispose();
                }
            Label_01BD:
                this.mStartWait = 0.1f;
                return;
            Label_01C9:
                player2 = photon.GetMyPlayer();
                if (this.mRecvList.Count > 0)
                {
                    goto Label_031E;
                }
                this.mSend.senderPlayerID = player2.photonPlayerID;
                this.mSend.playerListJson = null;
                list2 = new List<JSON_MyPhotonPlayerParam>();
                num = 0;
                goto Label_0233;
            Label_020F:
                param3 = JSON_MyPhotonPlayerParam.Parse(list[num].json);
                list2.Add(param3);
                num += 1;
            Label_0233:
                if (num < list.Count)
                {
                    goto Label_020F;
                }
                if (<>f__am$cache5 != null)
                {
                    goto Label_025B;
                }
                <>f__am$cache5 = new Comparison<JSON_MyPhotonPlayerParam>(FlowNode_StartMultiPlay.State_GameStart.<Update>m__1CD);
            Label_025B:
                list2.Sort(<>f__am$cache5);
                list3 = new FlowNode_StartMultiPlay.PlayerList();
                list3.players = list2.ToArray();
                paramArray = new Json_MyPhotonPlayerBinaryParam[(int) list3.players.Length];
                num2 = 0;
                goto Label_02C5;
            Label_0292:
                list3.players[num2].CreateJsonUnitData();
                paramArray[num2] = new Json_MyPhotonPlayerBinaryParam();
                paramArray[num2].Set(list3.players[num2]);
                num2 += 1;
            Label_02C5:
                if (num2 < ((int) list3.players.Length))
                {
                    goto Label_0292;
                }
                this.mSend.playerList = paramArray;
                buffer = GameUtility.Object2Binary<FlowNode_StartMultiPlay.RecvData>(this.mSend);
                photon.SendRoomMessageBinary(1, buffer, 2, 0);
                this.mRecvList.Add(this.mSend);
                this.mSend.playerListJson = list3.Serialize();
            Label_031E:
                list4 = photon.GetEvents();
                num3 = list4.Count - 1;
                goto Label_042B;
            Label_0336:
                data = null;
                if (GameUtility.Binary2Object<FlowNode_StartMultiPlay.RecvData>(&data, list4[num3].binary) != null)
                {
                    goto Label_0377;
                }
                DebugUtility.LogError("[PUN] started player list version error: " + list4[num3].json);
                photon.Disconnect();
                return;
            Label_0377:
                if (data == null)
                {
                    goto Label_0395;
                }
                if (data.version >= this.mSend.version)
                {
                    goto Label_03B9;
                }
            Label_0395:
                DebugUtility.LogError("[PUN] started player list version error: " + list4[num3].json);
                photon.Disconnect();
                return;
            Label_03B9:
                if (data.version <= this.mSend.version)
                {
                    goto Label_03D5;
                }
                goto Label_0425;
            Label_03D5:
                data.senderPlayerID = list4[num3].playerID;
                DebugUtility.Log("[PUN] recv started player list: " + list4[num3].json);
                this.mRecvList.Add(data);
                list4.Remove(list4[num3]);
            Label_0425:
                num3 -= 1;
            Label_042B:
                if (num3 >= 0)
                {
                    goto Label_0336;
                }
                storeye = new <Update>c__AnonStorey27E();
                enumerator2 = list.GetEnumerator();
            Label_0443:
                try
                {
                    goto Label_0479;
                Label_0448:
                    storeye.p = &enumerator2.Current;
                    if (this.mRecvList.FindIndex(new Predicate<FlowNode_StartMultiPlay.RecvData>(storeye.<>m__1CE)) >= 0)
                    {
                        goto Label_0479;
                    }
                    goto Label_0681;
                Label_0479:
                    if (&enumerator2.MoveNext() != null)
                    {
                        goto Label_0448;
                    }
                    goto Label_0497;
                }
                finally
                {
                Label_048A:
                    ((List<MyPhoton.MyPlayer>.Enumerator) enumerator2).Dispose();
                }
            Label_0497:
                flag = 1;
                enumerator3 = this.mRecvList.GetEnumerator();
            Label_04A7:
                try
                {
                    goto Label_0530;
                Label_04AC:
                    data2 = &enumerator3.Current;
                    if (((int) data2.playerList.Length) != ((int) this.mSend.playerList.Length))
                    {
                        goto Label_0528;
                    }
                    num4 = 0;
                    goto Label_0503;
                Label_04D8:
                    if (Json_MyPhotonPlayerBinaryParam.IsEqual(data2.playerList[num4], this.mSend.playerList[num4]) != null)
                    {
                        goto Label_04FD;
                    }
                    flag = 0;
                Label_04FD:
                    num4 += 1;
                Label_0503:
                    if (num4 < ((int) this.mSend.playerList.Length))
                    {
                        goto Label_04D8;
                    }
                    if (flag != null)
                    {
                        goto Label_0530;
                    }
                    goto Label_053C;
                    goto Label_0530;
                Label_0528:
                    flag = 0;
                    goto Label_053C;
                Label_0530:
                    if (&enumerator3.MoveNext() != null)
                    {
                        goto Label_04AC;
                    }
                Label_053C:
                    goto Label_054E;
                }
                finally
                {
                Label_0541:
                    ((List<FlowNode_StartMultiPlay.RecvData>.Enumerator) enumerator3).Dispose();
                }
            Label_054E:
                if (flag != null)
                {
                    goto Label_059E;
                }
                DebugUtility.Log("[PUN] started player list is not equal. ver:" + ((int) this.mSend.version));
                this.mRecvList.Clear();
                this.mSend.version += 1;
                this.mWait = 1f;
                return;
            Label_059E:
                DebugUtility.Log("[PUN]started player list decided. ver:" + ((int) this.mSend.version));
                list5 = photon.GetMyPlayersStarted();
                list5.Clear();
                paramArray2 = JSONParser.parseJSONObject<FlowNode_StartMultiPlay.PlayerList>(this.mSend.playerListJson).players;
                num5 = 0;
                goto Label_060C;
            Label_05EF:
                param4 = paramArray2[num5];
                param4.SetupUnits();
                list5.Add(param4);
                num5 += 1;
            Label_060C:
                if (num5 < ((int) paramArray2.Length))
                {
                    goto Label_05EF;
                }
                if (photon.IsOldestPlayer() == null)
                {
                    goto Label_0639;
                }
                photon.UpdateRoomParam("started", this.mSend.playerListJson);
            Label_0639:
                if (list4.Count <= 0)
                {
                    goto Label_0650;
                }
                DebugUtility.LogError("[PUN] event must be empty.");
            Label_0650:
                list4.Clear();
                param5 = JSON_MyPhotonPlayerParam.Parse(player2.json);
                param5.state = 3;
                photon.SetMyPlayerParam(param5.Serialize());
                this.mConfirm = 1;
            Label_0681:
                return;
            }

            [CompilerGenerated]
            private sealed class <Update>c__AnonStorey27E
            {
                internal MyPhoton.MyPlayer p;

                public <Update>c__AnonStorey27E()
                {
                    base..ctor();
                    return;
                }

                internal bool <>m__1CE(FlowNode_StartMultiPlay.RecvData r)
                {
                    return (r.senderPlayerID == this.p.photonPlayerID);
                }
            }
        }

        private class State_ResumeStart : State<FlowNode_StartMultiPlay>
        {
            [CompilerGenerated]
            private static Comparison<JSON_MyPhotonPlayerParam> <>f__am$cache0;

            public State_ResumeStart()
            {
                base..ctor();
                return;
            }

            [CompilerGenerated]
            private static int <Update>m__1CC(JSON_MyPhotonPlayerParam a, JSON_MyPhotonPlayerParam b)
            {
                return (a.playerIndex - b.playerIndex);
            }

            public override void Begin(FlowNode_StartMultiPlay self)
            {
            }

            public override void End(FlowNode_StartMultiPlay self)
            {
            }

            public override unsafe void Update(FlowNode_StartMultiPlay self)
            {
                MyPhoton photon;
                MyPhoton.MyState state;
                MyPhoton.MyRoom room;
                MyPhoton.MyPlayer player;
                JSON_MyPhotonPlayerParam param;
                JSON_MyPhotonRoomParam param2;
                List<MyPhoton.MyPlayer> list;
                List<JSON_MyPhotonPlayerParam> list2;
                int num;
                JSON_MyPhotonPlayerParam param3;
                List<JSON_MyPhotonPlayerParam> list3;
                string str;
                FlowNode_StartMultiPlay.PlayerList list4;
                JSON_MyPhotonPlayerParam param4;
                JSON_MyPhotonPlayerParam[] paramArray;
                int num2;
                JSON_MyPhotonPlayerParam param5;
                List<JSON_MyPhotonPlayerParam>.Enumerator enumerator;
                DebugUtility.Log("StartMultiPlay State_ResumeStart Update");
                photon = PunMonoSingleton<MyPhoton>.Instance;
                if (photon.CurrentState == 4)
                {
                    goto Label_0025;
                }
                self.Failure();
                return;
            Label_0025:
                room = photon.GetCurrentRoom();
                if (room != null)
                {
                    goto Label_0039;
                }
                self.Failure();
                return;
            Label_0039:
                param = JSON_MyPhotonPlayerParam.Parse(photon.GetMyPlayer().json);
                if (param.state == 2)
                {
                    goto Label_006F;
                }
                param.state = 2;
                photon.SetMyPlayerParam(param.Serialize());
            Label_006F:
                param2 = (string.IsNullOrEmpty(room.json) == null) ? JSON_MyPhotonRoomParam.Parse(room.json) : null;
                if (param2 != null)
                {
                    goto Label_00A0;
                }
                self.Failure();
                return;
            Label_00A0:
                GlobalVars.SelectedQuestID = param2.iname;
                GlobalVars.SelectedFriendID = null;
                GlobalVars.SelectedFriend = null;
                GlobalVars.SelectedSupport.Set(null);
                self.Success();
                DebugUtility.Log("StartMultiPlay: " + param2.Serialize());
                list = photon.GetRoomPlayerList();
                list2 = new List<JSON_MyPhotonPlayerParam>();
                num = 0;
                goto Label_012F;
            Label_00F6:
                param3 = JSON_MyPhotonPlayerParam.Parse(list[num].json);
                param3.playerID = list[num].playerID;
                list2.Add(param3);
                num += 1;
            Label_012F:
                if (num < list.Count)
                {
                    goto Label_00F6;
                }
                if (<>f__am$cache0 != null)
                {
                    goto Label_0157;
                }
                <>f__am$cache0 = new Comparison<JSON_MyPhotonPlayerParam>(FlowNode_StartMultiPlay.State_ResumeStart.<Update>m__1CC);
            Label_0157:
                list2.Sort(<>f__am$cache0);
                list3 = photon.GetMyPlayersStarted();
                list3.Clear();
                str = photon.GetRoomParam("started");
                if (str == null)
                {
                    goto Label_01CB;
                }
                paramArray = JSONParser.parseJSONObject<FlowNode_StartMultiPlay.PlayerList>(str).players;
                num2 = 0;
                goto Label_01BB;
            Label_019E:
                param4 = paramArray[num2];
                param4.SetupUnits();
                list3.Add(param4);
                num2 += 1;
            Label_01BB:
                if (num2 < ((int) paramArray.Length))
                {
                    goto Label_019E;
                }
                goto Label_0210;
            Label_01CB:
                enumerator = list2.GetEnumerator();
            Label_01D4:
                try
                {
                    goto Label_01F2;
                Label_01D9:
                    param5 = &enumerator.Current;
                    param5.SetupUnits();
                    list3.Add(param5);
                Label_01F2:
                    if (&enumerator.MoveNext() != null)
                    {
                        goto Label_01D9;
                    }
                    goto Label_0210;
                }
                finally
                {
                Label_0203:
                    ((List<JSON_MyPhotonPlayerParam>.Enumerator) enumerator).Dispose();
                }
            Label_0210:
                param.state = 3;
                photon.SetMyPlayerParam(param.Serialize());
                photon.SetResumeMyPlayer(GlobalVars.ResumeMultiplayPlayerID);
                photon.MyPlayerIndex = GlobalVars.ResumeMultiplaySeatID;
                return;
            }
        }

        private class State_Start : State<FlowNode_StartMultiPlay>
        {
            private int mPlayerNum;

            public State_Start()
            {
                base..ctor();
                return;
            }

            public override void Begin(FlowNode_StartMultiPlay self)
            {
                MyPhoton photon;
                MyPhoton.MyRoom room;
                room = PunMonoSingleton<MyPhoton>.Instance.GetCurrentRoom();
                if (room == null)
                {
                    goto Label_001F;
                }
                this.mPlayerNum = room.playerCount;
            Label_001F:
                return;
            }

            public override void End(FlowNode_StartMultiPlay self)
            {
            }

            public override unsafe void Update(FlowNode_StartMultiPlay self)
            {
                MyPhoton photon;
                MyPhoton.MyState state;
                MyPhoton.MyRoom room;
                bool flag;
                List<MyPhoton.MyPlayer> list;
                MyPhoton.MyPlayer player;
                List<MyPhoton.MyPlayer>.Enumerator enumerator;
                JSON_MyPhotonPlayerParam param;
                MyPhoton.MyPlayer player2;
                JSON_MyPhotonPlayerParam param2;
                DebugUtility.Log("StartMultiPlay State_Start Update");
                photon = PunMonoSingleton<MyPhoton>.Instance;
                if (photon.CurrentState == 4)
                {
                    goto Label_0025;
                }
                self.Failure();
                return;
            Label_0025:
                room = photon.GetCurrentRoom();
                if (room != null)
                {
                    goto Label_0039;
                }
                self.Failure();
                return;
            Label_0039:
                if (this.mPlayerNum == room.playerCount)
                {
                    goto Label_0051;
                }
                self.FailureStartMulti();
                return;
            Label_0051:
                if (photon.IsOldestPlayer() != null)
                {
                    goto Label_006E;
                }
                if (room.start != null)
                {
                    goto Label_006E;
                }
                self.Failure();
                return;
            Label_006E:
                flag = 1;
                enumerator = photon.GetRoomPlayerList().GetEnumerator();
            Label_0081:
                try
                {
                    goto Label_00F5;
                Label_0086:
                    player = &enumerator.Current;
                    param = JSON_MyPhotonPlayerParam.Parse(player.json);
                    if (param.state == 2)
                    {
                        goto Label_00F5;
                    }
                    flag = 0;
                    if (photon.IsOldestPlayer() == null)
                    {
                        goto Label_00E6;
                    }
                    if (photon.IsOldestPlayer(param.playerID) == null)
                    {
                        goto Label_00CE;
                    }
                    goto Label_00F5;
                Label_00CE:
                    if (param.state == 1)
                    {
                        goto Label_00E6;
                    }
                    self.FailureStartMulti();
                    goto Label_01A6;
                Label_00E6:
                    DebugUtility.Log("StartMultiPlay State_Start Update allStart is false");
                    goto Label_0101;
                Label_00F5:
                    if (&enumerator.MoveNext() != null)
                    {
                        goto Label_0086;
                    }
                Label_0101:
                    goto Label_0113;
                }
                finally
                {
                Label_0106:
                    ((List<MyPhoton.MyPlayer>.Enumerator) enumerator).Dispose();
                }
            Label_0113:
                if (flag == null)
                {
                    goto Label_012E;
                }
                DebugUtility.Log("StartMultiPlay State_Start Update change state to game start");
                self.GotoState<FlowNode_StartMultiPlay.State_GameStart>();
                goto Label_01A6;
            Label_012E:
                if (room.start == null)
                {
                    goto Label_018A;
                }
                DebugUtility.Log("StartMultiPlay State_Start Update room is closed");
                param2 = JSON_MyPhotonPlayerParam.Parse(photon.GetMyPlayer().json);
                if (param2.state == 2)
                {
                    goto Label_01A6;
                }
                param2.state = 2;
                photon.SetMyPlayerParam(param2.Serialize());
                DebugUtility.Log("StartMultiPlay State_Start Update update my state");
                goto Label_01A6;
            Label_018A:
                if (photon.IsOldestPlayer() == null)
                {
                    goto Label_01A6;
                }
                DebugUtility.Log("StartMultiPlay State_Start Update close room");
                photon.CloseRoom();
            Label_01A6:
                return;
            }
        }
    }
}

