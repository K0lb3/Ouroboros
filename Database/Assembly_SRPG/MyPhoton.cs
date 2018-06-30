namespace SRPG
{
    using ExitGames.Client.Photon;
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;
    using UnityEngine;

    public class MyPhoton : PunMonoSingleton<MyPhoton>
    {
        private const string STARTED_ROOM = "start";
        private const string BATTLESTART_ROOM = "battle";
        private const string DRAFT_BATTLESTART_ROOM = "draft";
        public static readonly int MAX_PLAYER_NUM;
        public static readonly int TIMEOUT_SECOND;
        public static readonly int SEND_RATE;
        private MyState mState;
        private bool mIsRoomListUpdated;
        private bool mIsUpdateRoomProperty;
        private bool mIsUpdatePlayerProperty;
        private MyError mError;
        private float mDelaySec;
        private NetworkReachability mNetworkReach;
        private List<MyEvent> mEvents;
        private int mSendRoomMessageID;
        private List<JSON_MyPhotonPlayerParam> mPlayersStarted;
        [CompilerGenerated]
        private float <TimeOutSec>k__BackingField;
        [CompilerGenerated]
        private bool <SendRoomMessageFlush>k__BackingField;
        [CompilerGenerated]
        private bool <DisconnectIfSendRoomMessageFailed>k__BackingField;
        [CompilerGenerated]
        private bool <SortRoomMessage>k__BackingField;
        [CompilerGenerated]
        private string <CurrentAppID>k__BackingField;
        [CompilerGenerated]
        private bool <UseEncrypt>k__BackingField;
        [CompilerGenerated]
        private int <MyPlayerIndex>k__BackingField;
        [CompilerGenerated]
        private bool <IsMultiPlay>k__BackingField;
        [CompilerGenerated]
        private bool <IsMultiVersus>k__BackingField;
        [CompilerGenerated]
        private bool <IsRankMatch>k__BackingField;
        [CompilerGenerated]
        private static Comparison<MyEvent> <>f__am$cache17;
        [CompilerGenerated]
        private static Func<RoomInfo, Guid> <>f__am$cache18;

        static MyPhoton()
        {
            MAX_PLAYER_NUM = 10;
            TIMEOUT_SECOND = 30;
            SEND_RATE = 30;
            return;
        }

        public MyPhoton()
        {
            this.mDelaySec = -1f;
            this.mEvents = new List<MyEvent>();
            this.mPlayersStarted = new List<JSON_MyPhotonPlayerParam>();
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private static Guid <JoinRandomRoom>m__216(RoomInfo room)
        {
            return Guid.NewGuid();
        }

        [CompilerGenerated]
        private static int <OnEventHandler>m__214(MyEvent a, MyEvent b)
        {
            return (a.sendID - b.sendID);
        }

        public void AddMyPlayerParam(string key, object param)
        {
            MyPlayer player;
            Hashtable hashtable;
            if (this.GetMyPlayer() == null)
            {
                goto Label_0046;
            }
            hashtable = PhotonNetwork.player.CustomProperties;
            if (hashtable.ContainsKey(key) != null)
            {
                goto Label_0031;
            }
            hashtable.Add(key, param);
            goto Label_0039;
        Label_0031:
            hashtable.set_Item(key, param);
        Label_0039:
            PhotonNetwork.player.SetCustomProperties(hashtable, null, 0);
        Label_0046:
            return;
        }

        public bool AddRoomParam(string key, string param)
        {
            Room room;
            Hashtable hashtable;
            Hashtable hashtable2;
            if (this.mState == 4)
            {
                goto Label_0015;
            }
            this.mError = 2;
            return 0;
        Label_0015:
            room = PhotonNetwork.room;
            if (room != null)
            {
                goto Label_0023;
            }
            return 0;
        Label_0023:
            hashtable = room.CustomProperties;
            hashtable2 = new Hashtable();
            if (hashtable == null)
            {
                goto Label_005A;
            }
            hashtable2.Add("json", hashtable.get_Item("json"));
            if (hashtable.ContainsKey(key) == null)
            {
                goto Label_005A;
            }
            return 0;
        Label_005A:
            hashtable2.Add(key, GameUtility.Object2Binary<string>(param));
            room.SetCustomProperties(null, null, 0);
            room.SetCustomProperties(hashtable2, null, 0);
            return 1;
        }

        public void BattleStartRoom()
        {
            Hashtable hashtable;
            Hashtable hashtable2;
            if (this.mState == 4)
            {
                goto Label_0014;
            }
            this.mError = 2;
            return;
        Label_0014:
            if (this.IsHost() != null)
            {
                goto Label_0020;
            }
            return;
        Label_0020:
            hashtable2 = new Hashtable();
            hashtable2.Add("battle", (bool) 1);
            hashtable2.Add("draft", (bool) 1);
            hashtable = hashtable2;
            PhotonNetwork.room.SetCustomProperties(hashtable, null, 0);
            return;
        }

        public bool CheckTowerRoomIsBattle(string roomname)
        {
            List<MyRoom> list;
            MyRoom room;
            JSON_MyPhotonRoomParam param;
            string str;
            int num;
            <CheckTowerRoomIsBattle>c__AnonStorey2AA storeyaa;
            storeyaa = new <CheckTowerRoomIsBattle>c__AnonStorey2AA();
            storeyaa.roomname = roomname;
            if (this.CurrentState == 2)
            {
                goto Label_001D;
            }
            return 0;
        Label_001D:
            room = this.GetRoomList().Find(new Predicate<MyRoom>(storeyaa.<>m__21D));
            if (room != null)
            {
                goto Label_0040;
            }
            return 0;
        Label_0040:
            if (room.battle != null)
            {
                goto Label_004D;
            }
            return 0;
        Label_004D:
            Debug.Log("Room Name : " + storeyaa.roomname + " Room is found");
            if (string.IsNullOrEmpty(room.json) == null)
            {
                goto Label_007A;
            }
            return 1;
        Label_007A:
            param = JSON_MyPhotonRoomParam.Parse(room.json);
            if (param == null)
            {
                goto Label_0097;
            }
            if (param.players != null)
            {
                goto Label_0099;
            }
        Label_0097:
            return 1;
        Label_0099:
            str = MonoSingleton<GameManager>.Instance.Player.FUID;
            num = 0;
            goto Label_00D2;
        Label_00B1:
            if ((param.players[num].FUID == str) == null)
            {
                goto Label_00CC;
            }
            return 1;
        Label_00CC:
            num += 1;
        Label_00D2:
            if (num < ((int) param.players.Length))
            {
                goto Label_00B1;
            }
            Debug.LogError("Room is not battle.");
            return 0;
        }

        public bool CloseRoom()
        {
            byte[] buffer;
            Hashtable hashtable;
            Hashtable hashtable2;
            Hashtable hashtable3;
            if (this.mState == 4)
            {
                goto Label_0015;
            }
            this.mError = 2;
            return 0;
        Label_0015:
            buffer = null;
            if (PhotonNetwork.room.CustomProperties.Count <= 0)
            {
                goto Label_005A;
            }
            hashtable = PhotonNetwork.room.CustomProperties;
            if (hashtable == null)
            {
                goto Label_005A;
            }
            if (hashtable.Count <= 0)
            {
                goto Label_005A;
            }
            buffer = (byte[]) hashtable.get_Item("json");
        Label_005A:
            hashtable3 = new Hashtable();
            hashtable3.Add("json", buffer);
            hashtable3.Add("start", (bool) 1);
            hashtable3.Add("battle", (bool) 1);
            hashtable3.Add("draft", (bool) 0);
            hashtable2 = hashtable3;
            PhotonNetwork.room.SetCustomProperties(hashtable2, null, 0);
            if (GlobalVars.SelectedMultiPlayRoomType != null)
            {
                goto Label_00C3;
            }
            PhotonNetwork.room.IsVisible = 0;
        Label_00C3:
            return 1;
        }

        public unsafe bool CreateRoom(string roomName, string roomJson, string playerJson, int plv, string uid, int score, int type)
        {
            string[] textArray1;
            RoomOptions options;
            int num;
            int num2;
            bool flag;
            Hashtable hashtable;
            if (this.mState == 2)
            {
                goto Label_0015;
            }
            this.mError = 2;
            return 0;
        Label_0015:
            PhotonNetwork.player.SetCustomProperties(null, null, 0);
            PhotonNetwork.SetPlayerCustomProperties(null);
            this.SetMyPlayerParam(playerJson);
            options = new RoomOptions();
            options.MaxPlayers = 3;
            options.IsVisible = 1;
            options.IsOpen = 1;
            hashtable = new Hashtable();
            hashtable.Add("json", GameUtility.Object2Binary<string>(roomJson));
            hashtable.Add("name", roomName);
            hashtable.Add("start", (bool) 0);
            hashtable.Add("battle", (bool) 0);
            hashtable.Add("draft", (bool) 0);
            options.CustomRoomProperties = hashtable;
            textArray1 = new string[] { "json", "name", "start", "draft" };
            options.CustomRoomPropertiesForLobby = textArray1;
            num = (int) options.CustomRoomPropertiesForLobby.Length;
            num2 = ((int) options.CustomRoomPropertiesForLobby.Length) + 7;
            Array.Resize<string>(&options.CustomRoomPropertiesForLobby, num2);
            options.CustomRoomProperties.Add("MatchType", MonoSingleton<GameManager>.Instance.GetVersusKey(3));
            options.CustomRoomPropertiesForLobby[num++] = "MatchType";
            options.CustomRoomProperties.Add("lobby", "vs");
            options.CustomRoomPropertiesForLobby[num++] = "lobby";
            options.CustomRoomProperties.Add("Audience", "0");
            options.CustomRoomPropertiesForLobby[num++] = "Audience";
            options.CustomRoomProperties.Add("plv", (int) plv);
            options.CustomRoomPropertiesForLobby[num++] = "plv";
            options.CustomRoomProperties.Add("score", (int) score);
            options.CustomRoomPropertiesForLobby[num++] = "score";
            options.CustomRoomProperties.Add("type", (int) type);
            options.CustomRoomPropertiesForLobby[num++] = "type";
            options.CustomRoomProperties.Add("uid", uid);
            options.CustomRoomPropertiesForLobby[num++] = "uid";
            flag = PhotonNetwork.CreateRoom(roomName, options, null);
            if (flag == null)
            {
                goto Label_0228;
            }
            this.mState = 3;
            goto Label_022F;
        Label_0228:
            this.mError = 1;
        Label_022F:
            return flag;
        }

        public unsafe bool CreateRoom(int maxPlayerNum, string roomName, string roomJson, string playerJson, string MatchKey, int floor, int plv, string luid, string uid, int audMax, bool isTower)
        {
            string[] textArray1;
            RoomOptions options;
            int num;
            int num2;
            string[] strArray;
            StringBuilder builder;
            int num3;
            int num4;
            int num5;
            bool flag;
            Hashtable hashtable;
            if (this.mState == 2)
            {
                goto Label_0015;
            }
            this.mError = 2;
            return 0;
        Label_0015:
            PhotonNetwork.player.SetCustomProperties(null, null, 0);
            PhotonNetwork.SetPlayerCustomProperties(null);
            this.SetMyPlayerParam(playerJson);
            options = new RoomOptions();
            options.MaxPlayers = (byte) maxPlayerNum;
            options.IsVisible = (GlobalVars.SelectedMultiPlayRoomType != 1) ? 1 : 0;
            options.IsOpen = 1;
            hashtable = new Hashtable();
            hashtable.Add("json", GameUtility.Object2Binary<string>(roomJson));
            hashtable.Add("name", roomName);
            hashtable.Add("start", (bool) 0);
            hashtable.Add("battle", (bool) 0);
            hashtable.Add("draft", (bool) 0);
            options.CustomRoomProperties = hashtable;
            textArray1 = new string[] { "json", "name", "start", "battle", "draft" };
            options.CustomRoomPropertiesForLobby = textArray1;
            if ((isTower == null) || (string.IsNullOrEmpty(MatchKey) != null))
            {
                goto Label_02E3;
            }
            num = (int) options.CustomRoomPropertiesForLobby.Length;
            num2 = ((int) options.CustomRoomPropertiesForLobby.Length) + 3;
            num2 = (floor == -1) ? num2 : (num2 += 1);
            num2 = ((GlobalVars.BlockList == null) || (GlobalVars.BlockList.Count <= 0)) ? num2 : (num2 += 1);
            num2 = (string.IsNullOrEmpty(uid) != null) ? num2 : (num2 += 1);
            Array.Resize<string>(&options.CustomRoomPropertiesForLobby, num2);
            options.CustomRoomProperties.Add("lobby", "tower");
            options.CustomRoomPropertiesForLobby[((int) options.CustomRoomPropertiesForLobby.Length) - 1] = "lobby";
            options.CustomRoomProperties.Add("MatchType", MatchKey);
            options.CustomRoomPropertiesForLobby[num++] = "MatchType";
            options.CustomRoomProperties.Add("Lock", (bool) (GlobalVars.EditMultiPlayRoomPassCode != "0"));
            options.CustomRoomPropertiesForLobby[num++] = "Lock";
            if (floor == -1)
            {
                goto Label_022D;
            }
            options.CustomRoomProperties.Add("floor", (int) floor);
            options.CustomRoomPropertiesForLobby[num++] = "floor";
        Label_022D:
            strArray = GlobalVars.BlockList.ToArray();
            if ((strArray == null) || (((int) strArray.Length) <= 0))
            {
                goto Label_02AF;
            }
            builder = new StringBuilder();
            num3 = 0;
            goto Label_027D;
        Label_0256:
            if (num3 <= 0)
            {
                goto Label_026B;
            }
            builder.Append(",");
        Label_026B:
            builder.Append(strArray[num3]);
            num3 += 1;
        Label_027D:
            if (num3 < ((int) strArray.Length))
            {
                goto Label_0256;
            }
            options.CustomRoomProperties.Add("BlockList", builder.ToString());
            options.CustomRoomPropertiesForLobby[num++] = "BlockList";
        Label_02AF:
            if (string.IsNullOrEmpty(uid) != null)
            {
                goto Label_0544;
            }
            options.CustomRoomProperties.Add("uid", uid);
            options.CustomRoomPropertiesForLobby[num++] = "uid";
            goto Label_0544;
        Label_02E3:
            if (string.IsNullOrEmpty(MatchKey) != null)
            {
                goto Label_0504;
            }
            num4 = (int) options.CustomRoomPropertiesForLobby.Length;
            num5 = ((int) options.CustomRoomPropertiesForLobby.Length) + 3;
            num5 = (plv == -1) ? num5 : (num5 += 1);
            num5 = (floor == -1) ? num5 : (num5 += 1);
            num5 = (string.IsNullOrEmpty(luid) != null) ? num5 : (num5 += 1);
            num5 = (string.IsNullOrEmpty(uid) != null) ? num5 : (num5 += 1);
            num5 = (audMax == -1) ? num5 : (num5 += 1);
            Array.Resize<string>(&options.CustomRoomPropertiesForLobby, num5);
            options.CustomRoomProperties.Add("MatchType", MatchKey);
            options.CustomRoomPropertiesForLobby[num4++] = "MatchType";
            options.CustomRoomProperties.Add("lobby", "vs");
            options.CustomRoomPropertiesForLobby[num4++] = "lobby";
            options.CustomRoomProperties.Add("Audience", "0");
            options.CustomRoomPropertiesForLobby[num4++] = "Audience";
            if (plv == -1)
            {
                goto Label_0439;
            }
            options.CustomRoomProperties.Add("plv", (int) plv);
            options.CustomRoomPropertiesForLobby[num4++] = "plv";
        Label_0439:
            if (floor == -1)
            {
                goto Label_046B;
            }
            options.CustomRoomProperties.Add("floor", (int) floor);
            options.CustomRoomPropertiesForLobby[num4++] = "floor";
        Label_046B:
            if (string.IsNullOrEmpty(luid) != null)
            {
                goto Label_049C;
            }
            options.CustomRoomProperties.Add("luid", luid);
            options.CustomRoomPropertiesForLobby[num4++] = "luid";
        Label_049C:
            if (string.IsNullOrEmpty(uid) != null)
            {
                goto Label_04CD;
            }
            options.CustomRoomProperties.Add("uid", uid);
            options.CustomRoomPropertiesForLobby[num4++] = "uid";
        Label_04CD:
            if (audMax <= 0)
            {
                goto Label_0544;
            }
            options.CustomRoomProperties.Add("AudienceMax", (int) audMax);
            options.CustomRoomPropertiesForLobby[num4++] = "AudienceMax";
            goto Label_0544;
        Label_0504:
            options.CustomRoomProperties.Add("lobby", "coop");
            Array.Resize<string>(&options.CustomRoomPropertiesForLobby, ((int) options.CustomRoomPropertiesForLobby.Length) + 1);
            options.CustomRoomPropertiesForLobby[((int) options.CustomRoomPropertiesForLobby.Length) - 1] = "lobby";
        Label_0544:
            flag = PhotonNetwork.CreateRoom(roomName, options, null);
            if (flag == null)
            {
                goto Label_0561;
            }
            this.mState = 3;
            goto Label_0568;
        Label_0561:
            this.mError = 1;
        Label_0568:
            return flag;
        }

        public void Disconnect()
        {
            this.Log("call Disconnect().");
            this.mEvents.Clear();
            if (this.CurrentState == null)
            {
                goto Label_0026;
            }
            PhotonNetwork.Disconnect();
        Label_0026:
            return;
        }

        public void EnableKeepAlive(bool flag)
        {
            if (PhotonNetwork.isMessageQueueRunning == flag)
            {
                goto Label_0021;
            }
            this.Log("[PUN]KeepAlive changed to:" + ((bool) flag));
        Label_0021:
            PhotonNetwork.isMessageQueueRunning = flag;
            return;
        }

        public MyPlayer FindPlayer(List<MyPlayer> players, int playerID, int playerIndex)
        {
            MyPlayer player;
            <FindPlayer>c__AnonStorey2A9 storeya;
            storeya = new <FindPlayer>c__AnonStorey2A9();
            storeya.playerID = playerID;
            storeya.playerIndex = playerIndex;
            player = null;
            if (players == null)
            {
                goto Label_007A;
            }
            player = players.Find(new Predicate<MyPlayer>(storeya.<>m__219));
            if (player != null)
            {
                goto Label_0048;
            }
            player = players.Find(new Predicate<MyPlayer>(storeya.<>m__21A));
        Label_0048:
            if (player != null)
            {
                goto Label_0061;
            }
            player = players.Find(new Predicate<MyPlayer>(storeya.<>m__21B));
        Label_0061:
            if (player != null)
            {
                goto Label_007A;
            }
            player = players.Find(new Predicate<MyPlayer>(storeya.<>m__21C));
        Label_007A:
            return player;
        }

        public void ForceCloseRoom()
        {
            byte[] buffer;
            Hashtable hashtable;
            Hashtable hashtable2;
            Hashtable hashtable3;
            if (this.mState == 4)
            {
                goto Label_000D;
            }
            return;
        Label_000D:
            PhotonNetwork.room.IsOpen = 0;
            buffer = null;
            if (PhotonNetwork.room.CustomProperties.Count <= 0)
            {
                goto Label_005D;
            }
            hashtable = PhotonNetwork.room.CustomProperties;
            if (hashtable == null)
            {
                goto Label_005D;
            }
            if (hashtable.Count <= 0)
            {
                goto Label_005D;
            }
            buffer = (byte[]) hashtable.get_Item("json");
        Label_005D:
            hashtable3 = new Hashtable();
            hashtable3.Add("json", buffer);
            hashtable3.Add("start", (bool) 0);
            hashtable3.Add("battle", (bool) 0);
            hashtable3.Add("draft", (bool) 0);
            hashtable2 = hashtable3;
            PhotonNetwork.room.SetCustomProperties(null, null, 0);
            PhotonNetwork.room.SetCustomProperties(hashtable2, null, 0);
            return;
        }

        private int GetCryptKey()
        {
            MyRoom room;
            int num;
            char ch;
            string str;
            int num2;
            room = this.GetCurrentRoom();
            if (room == null)
            {
                goto Label_001D;
            }
            if (string.IsNullOrEmpty(room.name) == null)
            {
                goto Label_0020;
            }
        Label_001D:
            return 0x7b;
        Label_0020:
            num = 0;
            str = room.name;
            num2 = 0;
            goto Label_0044;
        Label_0031:
            ch = str[num2];
            num += ch;
            num2 += 1;
        Label_0044:
            if (num2 < str.Length)
            {
                goto Label_0031;
            }
            return num;
        }

        public unsafe MyRoom GetCurrentRoom()
        {
            Room room;
            MyRoom room2;
            Hashtable hashtable;
            room = PhotonNetwork.room;
            room2 = new MyRoom();
            if (room == null)
            {
                goto Label_00F4;
            }
            room2.name = room.Name;
            room2.playerCount = room.PlayerCount;
            room2.maxPlayers = room.MaxPlayers;
            room2.open = room.IsOpen;
            room2.start = 0;
            hashtable = room.CustomProperties;
            if (hashtable == null)
            {
                goto Label_00F4;
            }
            if (hashtable.ContainsKey("json") == null)
            {
                goto Label_0082;
            }
            GameUtility.Binary2Object<string>(&room2.json, (byte[]) hashtable.get_Item("json"));
        Label_0082:
            if (hashtable.ContainsKey("start") == null)
            {
                goto Label_00A8;
            }
            room2.start = (bool) hashtable.get_Item("start");
        Label_00A8:
            if (hashtable.ContainsKey("battle") == null)
            {
                goto Label_00CE;
            }
            room2.battle = (bool) hashtable.get_Item("battle");
        Label_00CE:
            if (hashtable.ContainsKey("draft") == null)
            {
                goto Label_00F4;
            }
            room2.draft = (bool) hashtable.get_Item("draft");
        Label_00F4:
            return room2;
        }

        public List<MyEvent> GetEvents()
        {
            return this.mEvents;
        }

        public unsafe MyPlayer GetMyPlayer()
        {
            Hashtable hashtable;
            MyPlayer player;
            hashtable = PhotonNetwork.player.CustomProperties;
            player = new MyPlayer();
            player.photonPlayerID = PhotonNetwork.player.ID;
            if (hashtable == null)
            {
                goto Label_0075;
            }
            if (hashtable.Count <= 0)
            {
                goto Label_0075;
            }
            GameUtility.Binary2Object<string>(&player.json, (byte[]) hashtable.get_Item("json"));
            if (hashtable.ContainsKey("resumeID") == null)
            {
                goto Label_0075;
            }
            player.resumeID = (int) hashtable.get_Item("resumeID");
        Label_0075:
            return player;
        }

        public List<JSON_MyPhotonPlayerParam> GetMyPlayersStarted()
        {
            return this.mPlayersStarted;
        }

        public unsafe int GetOldestPlayer()
        {
            int num;
            List<MyPlayer> list;
            MyPlayer player;
            List<MyPlayer>.Enumerator enumerator;
            if (this.mState == 4)
            {
                goto Label_000E;
            }
            return 0;
        Label_000E:
            num = 0;
            enumerator = this.GetRoomPlayerList().GetEnumerator();
        Label_001E:
            try
            {
                goto Label_004F;
            Label_0023:
                player = &enumerator.Current;
                if (player.playerID < num)
                {
                    goto Label_003D;
                }
                if (num != null)
                {
                    goto Label_004F;
                }
            Label_003D:
                if (player.start == null)
                {
                    goto Label_004F;
                }
                num = player.playerID;
            Label_004F:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0023;
                }
                goto Label_006C;
            }
            finally
            {
            Label_0060:
                ((List<MyPlayer>.Enumerator) enumerator).Dispose();
            }
        Label_006C:
            return num;
        }

        public unsafe List<MyRoom> GetRoomList()
        {
            List<MyRoom> list;
            RoomInfo info;
            RoomInfo[] infoArray;
            int num;
            MyRoom room;
            Hashtable hashtable;
            string str;
            list = new List<MyRoom>();
            infoArray = PhotonNetwork.GetRoomList();
            num = 0;
            goto Label_0198;
        Label_0013:
            info = infoArray[num];
            room = new MyRoom();
            room.name = info.Name;
            room.playerCount = info.PlayerCount;
            room.maxPlayers = info.MaxPlayers;
            room.open = info.IsOpen;
            hashtable = info.CustomProperties;
            if (hashtable == null)
            {
                goto Label_018C;
            }
            if (hashtable.Count <= 0)
            {
                goto Label_018C;
            }
            GameUtility.Binary2Object<string>(&room.json, (byte[]) hashtable.get_Item("json"));
            if (hashtable.ContainsKey("lobby") == null)
            {
                goto Label_00B5;
            }
            room.lobby = (string) hashtable.get_Item("lobby");
        Label_00B5:
            if (hashtable.ContainsKey("Audience") == null)
            {
                goto Label_00E8;
            }
            int.TryParse(hashtable.get_Item("Audience").ToString(), &room.audience);
        Label_00E8:
            if (hashtable.ContainsKey("AudienceMax") == null)
            {
                goto Label_0111;
            }
            room.audienceMax = (int) hashtable.get_Item("AudienceMax");
        Label_0111:
            if (hashtable.ContainsKey("start") == null)
            {
                goto Label_013A;
            }
            room.start = (bool) hashtable.get_Item("start");
        Label_013A:
            if (hashtable.ContainsKey("battle") == null)
            {
                goto Label_0163;
            }
            room.battle = (bool) hashtable.get_Item("battle");
        Label_0163:
            if (hashtable.ContainsKey("draft") == null)
            {
                goto Label_018C;
            }
            room.draft = (bool) hashtable.get_Item("draft");
        Label_018C:
            list.Add(room);
            num += 1;
        Label_0198:
            if (num < ((int) infoArray.Length))
            {
                goto Label_0013;
            }
            return list;
        }

        public unsafe string GetRoomParam(string key)
        {
            Room room;
            Hashtable hashtable;
            object obj2;
            string str;
            if (this.mState == 4)
            {
                goto Label_0015;
            }
            this.mError = 2;
            return null;
        Label_0015:
            room = PhotonNetwork.room;
            if (room != null)
            {
                goto Label_0023;
            }
            return null;
        Label_0023:
            hashtable = room.CustomProperties;
            if (hashtable == null)
            {
                goto Label_0073;
            }
            obj2 = null;
            if (hashtable.TryGetValue(key, &obj2) == null)
            {
                goto Label_0073;
            }
            if (obj2.ToString().IndexOf("players") != -1)
            {
                goto Label_006C;
            }
            str = string.Empty;
            GameUtility.Binary2Object<string>(&str, (byte[]) obj2);
            return str;
        Label_006C:
            return (string) obj2;
        Label_0073:
            return null;
        }

        public unsafe List<MyPlayer> GetRoomPlayerList()
        {
            List<MyPlayer> list;
            PhotonPlayer player;
            PhotonPlayer[] playerArray;
            int num;
            MyPlayer player2;
            Hashtable hashtable;
            list = new List<MyPlayer>();
            playerArray = PhotonNetwork.playerList;
            num = 0;
            goto Label_00D9;
        Label_0013:
            player = playerArray[num];
            player2 = new MyPlayer();
            player2.playerID = player.ID;
            hashtable = player.CustomProperties;
            if (hashtable == null)
            {
                goto Label_00CD;
            }
            if (hashtable.Count <= 0)
            {
                goto Label_00CD;
            }
            GameUtility.Binary2Object<string>(&player2.json, (byte[]) hashtable.get_Item("json"));
            if (hashtable.ContainsKey("resumeID") == null)
            {
                goto Label_008E;
            }
            player2.resumeID = (int) hashtable.get_Item("resumeID");
        Label_008E:
            if (hashtable.ContainsKey("BattleStart") == null)
            {
                goto Label_00B7;
            }
            player2.start = (bool) hashtable.get_Item("BattleStart");
        Label_00B7:
            if (hashtable.ContainsKey("Logger") == null)
            {
                goto Label_00CD;
            }
            goto Label_00D5;
        Label_00CD:
            list.Add(player2);
        Label_00D5:
            num += 1;
        Label_00D9:
            if (num < ((int) playerArray.Length))
            {
                goto Label_0013;
            }
            return list;
        }

        public string GetTrafficState()
        {
            object[] objArray1;
            int num;
            TrafficStats stats;
            TrafficStats stats2;
            num = SupportClass.GetTickCount() - PhotonNetwork.networkingPeer.get_TimestampOfLastSocketReceive();
            stats = PhotonNetwork.networkingPeer.get_TrafficStatsOutgoing();
            stats2 = PhotonNetwork.networkingPeer.get_TrafficStatsIncoming();
            objArray1 = new object[] { "lastrecv:", (int) num, " og:", stats.ToString(), " ic:", stats2.ToString() };
            return string.Concat(objArray1);
        }

        protected override void Initialize()
        {
            PhotonLagSimulationGui gui;
            Object.DontDestroyOnLoad(this);
            if (GameUtility.IsDebugBuild == null)
            {
                goto Label_0023;
            }
            base.get_gameObject().AddComponent<PhotonLagSimulationGui>().Visible = 0;
        Label_0023:
            PhotonNetwork.logLevel = 0;
            PhotonNetwork.OnEventCall = (PhotonNetwork.EventCallback) Delegate.Combine(PhotonNetwork.OnEventCall, new PhotonNetwork.EventCallback(this.OnEventHandler));
            PhotonNetwork.CrcCheckEnabled = 1;
            PhotonNetwork.QuickResends = 3;
            PhotonNetwork.MaxResendsBeforeDisconnect = 7;
            PhotonNetwork.logLevel = 0;
            PhotonNetwork.sendRate = SEND_RATE;
            this.UseEncrypt = 1;
            this.TimeOutSec = (float) TIMEOUT_SECOND;
            return;
        }

        public bool IsBattle(string roomname)
        {
            bool flag;
            List<MyRoom> list;
            int num;
            JSON_MyPhotonRoomParam param;
            bool flag2;
            string str;
            int num2;
            flag = 0;
            if (this.CurrentState == 2)
            {
                goto Label_0010;
            }
            return 0;
        Label_0010:
            list = this.GetRoomList();
            num = 0;
            goto Label_0116;
        Label_001E:
            if ((list[num].lobby != "tower") == null)
            {
                goto Label_003E;
            }
            goto Label_0112;
        Label_003E:
            if (list[num].name.Equals(roomname) != null)
            {
                goto Label_005A;
            }
            goto Label_0112;
        Label_005A:
            flag = list[num].battle;
            Debug.Log("Room Name : " + roomname + " Room is found");
            if (string.IsNullOrEmpty(list[num].json) != null)
            {
                goto Label_0122;
            }
            param = JSON_MyPhotonRoomParam.Parse(list[num].json);
            if (param == null)
            {
                goto Label_0122;
            }
            if (param.players == null)
            {
                goto Label_0122;
            }
            flag2 = 0;
            str = MonoSingleton<GameManager>.Instance.Player.FUID;
            num2 = 0;
            goto Label_00F9;
        Label_00D1:
            if ((param.players[num2].FUID == str) == null)
            {
                goto Label_00F3;
            }
            flag2 = 1;
            goto Label_0108;
        Label_00F3:
            num2 += 1;
        Label_00F9:
            if (num2 < ((int) param.players.Length))
            {
                goto Label_00D1;
            }
        Label_0108:
            flag &= flag2;
            goto Label_0122;
        Label_0112:
            num += 1;
        Label_0116:
            if (num < list.Count)
            {
                goto Label_001E;
            }
        Label_0122:
            return flag;
        }

        public bool IsConnected()
        {
            return PhotonNetwork.connected;
        }

        public bool IsConnectedInRoom()
        {
            if (PhotonNetwork.connected == null)
            {
                goto Label_0018;
            }
            if (this.CurrentState != 4)
            {
                goto Label_0018;
            }
            return 1;
        Label_0018:
            return 0;
        }

        public bool IsCreatedRoom()
        {
            MyPlayer player;
            if (this.mState == 4)
            {
                goto Label_000E;
            }
            return 0;
        Label_000E:
            return (this.GetMyPlayer().playerID == 1);
        }

        public bool IsDisconnected()
        {
            if (this.ConnectState != null)
            {
                goto Label_000D;
            }
            return 1;
        Label_000D:
            return 0;
        }

        public unsafe bool IsHost()
        {
            MyPlayer player;
            List<MyPlayer> list;
            int num;
            MyPlayer player2;
            List<MyPlayer>.Enumerator enumerator;
            bool flag;
            if (this.mState == 4)
            {
                goto Label_000E;
            }
            return 0;
        Label_000E:
            player = this.GetMyPlayer();
            list = this.GetRoomPlayerList();
            num = player.photonPlayerID;
            enumerator = list.GetEnumerator();
        Label_002B:
            try
            {
                goto Label_004C;
            Label_0030:
                player2 = &enumerator.Current;
                if (player2.photonPlayerID >= num)
                {
                    goto Label_004C;
                }
                flag = 0;
                goto Label_006C;
            Label_004C:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0030;
                }
                goto Label_006A;
            }
            finally
            {
            Label_005D:
                ((List<MyPlayer>.Enumerator) enumerator).Dispose();
            }
        Label_006A:
            return 1;
        Label_006C:
            return flag;
        }

        public unsafe bool IsHost(int playerID)
        {
            List<MyPlayer> list;
            MyPlayer player;
            List<MyPlayer>.Enumerator enumerator;
            bool flag;
            if (this.mState == 4)
            {
                goto Label_000E;
            }
            return 0;
        Label_000E:
            enumerator = this.GetRoomPlayerList().GetEnumerator();
        Label_001C:
            try
            {
                goto Label_003C;
            Label_0021:
                player = &enumerator.Current;
                if (player.playerID >= playerID)
                {
                    goto Label_003C;
                }
                flag = 0;
                goto Label_005B;
            Label_003C:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0021;
                }
                goto Label_0059;
            }
            finally
            {
            Label_004D:
                ((List<MyPlayer>.Enumerator) enumerator).Dispose();
            }
        Label_0059:
            return 1;
        Label_005B:
            return flag;
        }

        public bool IsOldestPlayer()
        {
            MyPlayer player;
            if (this.mState == 4)
            {
                goto Label_000E;
            }
            return 0;
        Label_000E:
            player = this.GetMyPlayer();
            return this.IsOldestPlayer(player.playerID);
        }

        public unsafe bool IsOldestPlayer(int playerID)
        {
            bool flag;
            List<MyPlayer> list;
            MyPlayer player;
            List<MyPlayer>.Enumerator enumerator;
            bool flag2;
            if (this.mState == 4)
            {
                goto Label_000E;
            }
            return 0;
        Label_000E:
            flag = 0;
            enumerator = this.GetRoomPlayerList().GetEnumerator();
        Label_001E:
            try
            {
                goto Label_004D;
            Label_0023:
                player = &enumerator.Current;
                if (player.playerID >= playerID)
                {
                    goto Label_003F;
                }
                flag2 = 0;
                goto Label_006C;
            Label_003F:
                if (player.playerID != playerID)
                {
                    goto Label_004D;
                }
                flag = 1;
            Label_004D:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0023;
                }
                goto Label_006A;
            }
            finally
            {
            Label_005E:
                ((List<MyPlayer>.Enumerator) enumerator).Dispose();
            }
        Label_006A:
            return flag;
        Label_006C:
            return flag2;
        }

        public bool IsResume()
        {
            MyPlayer player;
            Hashtable hashtable;
            if (this.GetMyPlayer() == null)
            {
                goto Label_003F;
            }
            hashtable = PhotonNetwork.player.CustomProperties;
            if (hashtable == null)
            {
                goto Label_003F;
            }
            if (hashtable.ContainsKey("resume") == null)
            {
                goto Label_003F;
            }
            return (bool) hashtable.get_Item("resume");
        Label_003F:
            return 0;
        }

        public unsafe bool JoinRandomRoom(byte maxplayer, string playerJson, string VersusHash, string roomName, int floor, int pass, string myuid)
        {
            char[] chArray1;
            bool flag;
            string str;
            bool flag2;
            RoomInfo[] infoArray;
            List<RoomInfo> list;
            List<string> list2;
            RoomInfo info;
            RoomInfo[] infoArray2;
            int num;
            Hashtable hashtable;
            string str2;
            List<string> list3;
            RoomInfo info2;
            List<RoomInfo>.Enumerator enumerator;
            Hashtable hashtable2;
            Hashtable hashtable3;
            <JoinRandomRoom>c__AnonStorey2A6 storeya;
            <JoinRandomRoom>c__AnonStorey2A7 storeya2;
            Hashtable hashtable4;
            storeya = new <JoinRandomRoom>c__AnonStorey2A6();
            storeya.myuid = myuid;
            PhotonNetwork.player.SetCustomProperties(null, null, 0);
            PhotonNetwork.SetPlayerCustomProperties(null);
            this.SetMyPlayerParam(playerJson);
            flag = 0;
            if (floor == -1)
            {
                goto Label_02DF;
            }
            str = string.Empty;
            flag2 = 0;
            infoArray = PhotonNetwork.GetRoomList();
            list = new List<RoomInfo>();
            list2 = new List<string>();
            infoArray2 = infoArray;
            num = 0;
            goto Label_01AC;
        Label_005B:
            info = infoArray2[num];
            list2.Clear();
            hashtable = info.CustomProperties;
            if (hashtable.ContainsKey("MatchType") == null)
            {
                goto Label_01A6;
            }
            if ((VersusHash == ((string) hashtable.get_Item("MatchType"))) == null)
            {
                goto Label_01A6;
            }
            if (hashtable.ContainsKey("BlockList") == null)
            {
                goto Label_0107;
            }
            str2 = (string) hashtable.get_Item("BlockList");
            if (string.IsNullOrEmpty(str2) != null)
            {
                goto Label_0107;
            }
            chArray1 = new char[] { 0x2c };
            list2.AddRange(str2.Split(chArray1));
            if (list2.FindIndex(new Predicate<string>(storeya.<>m__215)) == -1)
            {
                goto Label_0107;
            }
            goto Label_01A6;
        Label_0107:
            if (hashtable.ContainsKey("start") == null)
            {
                goto Label_0133;
            }
            if (((bool) hashtable.get_Item("start")) == null)
            {
                goto Label_0133;
            }
            goto Label_01A6;
        Label_0133:
            if (floor == -1)
            {
                goto Label_0169;
            }
            if (hashtable.ContainsKey("floor") == null)
            {
                goto Label_0169;
            }
            if (((int) hashtable.get_Item("floor")) == floor)
            {
                goto Label_0169;
            }
            goto Label_01A6;
        Label_0169:
            if (pass == -1)
            {
                goto Label_019D;
            }
            if (hashtable.ContainsKey("Lock") == null)
            {
                goto Label_019D;
            }
            if (((bool) hashtable.get_Item("Lock")) == null)
            {
                goto Label_019D;
            }
            goto Label_01A6;
        Label_019D:
            list.Add(info);
        Label_01A6:
            num += 1;
        Label_01AC:
            if (num < ((int) infoArray2.Length))
            {
                goto Label_005B;
            }
            if (list == null)
            {
                goto Label_02CD;
            }
            if (list.Count <= 0)
            {
                goto Label_02CD;
            }
            if (<>f__am$cache18 != null)
            {
                goto Label_01EA;
            }
            <>f__am$cache18 = new Func<RoomInfo, Guid>(MyPhoton.<JoinRandomRoom>m__216);
        Label_01EA:
            list = new List<RoomInfo>(Enumerable.OrderBy<RoomInfo, Guid>(list.ToArray(), <>f__am$cache18));
            if (GlobalVars.BlockList == null)
            {
                goto Label_02BD;
            }
            if (GlobalVars.BlockList.Count <= 0)
            {
                goto Label_02BD;
            }
            list3 = GlobalVars.BlockList;
            enumerator = list.GetEnumerator();
        Label_0225:
            try
            {
                goto Label_029A;
            Label_022A:
                info2 = &enumerator.Current;
                hashtable2 = info2.CustomProperties;
                if (hashtable2.ContainsKey("uid") == null)
                {
                    goto Label_029A;
                }
                storeya2 = new <JoinRandomRoom>c__AnonStorey2A7();
                storeya2.target_uid = (string) hashtable2.get_Item("uid");
                if (list3.FindIndex(new Predicate<string>(storeya2.<>m__217)) == -1)
                {
                    goto Label_028B;
                }
                goto Label_029A;
            Label_028B:
                flag2 = 1;
                str = info2.Name;
                goto Label_02A6;
            Label_029A:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_022A;
                }
            Label_02A6:
                goto Label_02B8;
            }
            finally
            {
            Label_02AB:
                ((List<RoomInfo>.Enumerator) enumerator).Dispose();
            }
        Label_02B8:
            goto Label_02CD;
        Label_02BD:
            flag2 = 1;
            str = list[0].Name;
        Label_02CD:
            if (flag2 == null)
            {
                goto Label_0361;
            }
            flag = PhotonNetwork.JoinRoom(str);
            goto Label_0361;
        Label_02DF:
            hashtable4 = new Hashtable();
            hashtable4.Add("MatchType", VersusHash);
            hashtable4.Add("start", (bool) 0);
            hashtable3 = hashtable4;
            if (string.IsNullOrEmpty(roomName) != null)
            {
                goto Label_0323;
            }
            hashtable3.Add("name", roomName);
        Label_0323:
            if (floor == -1)
            {
                goto Label_033E;
            }
            hashtable3.Add("floor", (int) floor);
        Label_033E:
            if (pass == -1)
            {
                goto Label_0358;
            }
            hashtable3.Add("Lock", (bool) 0);
        Label_0358:
            flag = PhotonNetwork.JoinRandomRoom(hashtable3, maxplayer);
        Label_0361:
            if (flag == null)
            {
                goto Label_0373;
            }
            this.mState = 3;
            goto Label_037A;
        Label_0373:
            this.mError = 1;
        Label_037A:
            return flag;
        }

        public unsafe bool JoinRankMatchRoomCheckParam(string playerJson, int lv, int lvRange, string myuid, int score, int scoreRangeMax, int scoreRangeMin, int type, string[] exclude_uids)
        {
            string str;
            bool flag;
            RoomInfo[] infoArray;
            List<RoomInfo> list;
            string str2;
            RoomInfo info;
            RoomInfo[] infoArray2;
            int num;
            Hashtable hashtable;
            string str3;
            bool flag2;
            string str4;
            string[] strArray;
            int num2;
            int num3;
            int num4;
            RoomInfo info2;
            List<RoomInfo>.Enumerator enumerator;
            Hashtable hashtable2;
            int num5;
            int num6;
            int num7;
            int num8;
            RoomInfo info3;
            List<RoomInfo>.Enumerator enumerator2;
            Hashtable hashtable3;
            int num9;
            int num10;
            int num11;
            int num12;
            RoomInfo info4;
            List<RoomInfo>.Enumerator enumerator3;
            Hashtable hashtable4;
            int num13;
            int num14;
            int num15;
            bool flag3;
            <JoinRankMatchRoomCheckParam>c__AnonStorey2A8 storeya;
            storeya = new <JoinRankMatchRoomCheckParam>c__AnonStorey2A8();
            storeya.score = score;
            if (this.mState == 2)
            {
                goto Label_0025;
            }
            this.mError = 2;
            return 0;
        Label_0025:
            str = string.Empty;
            flag = 0;
            PhotonNetwork.player.SetCustomProperties(null, null, 0);
            PhotonNetwork.SetPlayerCustomProperties(null);
            this.SetMyPlayerParam(playerJson);
            infoArray = PhotonNetwork.GetRoomList();
            list = new List<RoomInfo>();
            str2 = MonoSingleton<GameManager>.Instance.GetVersusKey(3);
            infoArray2 = infoArray;
            num = 0;
            goto Label_0177;
        Label_006B:
            info = infoArray2[num];
            hashtable = info.CustomProperties;
            if (hashtable.ContainsKey("MatchType") == null)
            {
                goto Label_0171;
            }
            if ((str2 == ((string) hashtable.get_Item("MatchType"))) == null)
            {
                goto Label_0171;
            }
            if (hashtable.ContainsKey("uid") == null)
            {
                goto Label_0127;
            }
            if (exclude_uids == null)
            {
                goto Label_0127;
            }
            if (((int) exclude_uids.Length) <= 0)
            {
                goto Label_0127;
            }
            str3 = (string) hashtable.get_Item("uid");
            flag2 = 0;
            strArray = exclude_uids;
            num2 = 0;
            goto Label_0110;
        Label_00ED:
            str4 = strArray[num2];
            if (string.Compare(str3, str4) != null)
            {
                goto Label_010A;
            }
            flag2 = 1;
            goto Label_011B;
        Label_010A:
            num2 += 1;
        Label_0110:
            if (num2 < ((int) strArray.Length))
            {
                goto Label_00ED;
            }
        Label_011B:
            if (flag2 == null)
            {
                goto Label_0127;
            }
            goto Label_0171;
        Label_0127:
            if (hashtable.ContainsKey("score") != null)
            {
                goto Label_013D;
            }
            goto Label_0171;
        Label_013D:
            if (hashtable.ContainsKey("start") == null)
            {
                goto Label_0169;
            }
            if (((bool) hashtable.get_Item("start")) == null)
            {
                goto Label_0169;
            }
            goto Label_0171;
        Label_0169:
            list.Add(info);
        Label_0171:
            num += 1;
        Label_0177:
            if (num < ((int) infoArray2.Length))
            {
                goto Label_006B;
            }
            list.Sort(new Comparison<RoomInfo>(storeya.<>m__218));
            if (scoreRangeMin == -1)
            {
                goto Label_0283;
            }
            num3 = storeya.score - scoreRangeMin;
            num4 = storeya.score + scoreRangeMin;
            enumerator = list.GetEnumerator();
        Label_01BD:
            try
            {
                goto Label_0265;
            Label_01C2:
                info2 = &enumerator.Current;
                hashtable2 = info2.CustomProperties;
                if (hashtable2.ContainsKey("score") == null)
                {
                    goto Label_0265;
                }
                num5 = (int) hashtable2.get_Item("score");
                if (num3 > num5)
                {
                    goto Label_0265;
                }
                if (num5 > num4)
                {
                    goto Label_0265;
                }
                if (lvRange == -1)
                {
                    goto Label_0256;
                }
                if (hashtable2.ContainsKey("plv") == null)
                {
                    goto Label_0256;
                }
                num6 = lv - lvRange;
                num7 = lv + lvRange;
                num8 = (int) hashtable2.get_Item("plv");
                if (num8 >= num6)
                {
                    goto Label_0256;
                }
                if (num7 >= num8)
                {
                    goto Label_0256;
                }
                goto Label_0265;
            Label_0256:
                flag = 1;
                str = info2.Name;
                goto Label_0271;
            Label_0265:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_01C2;
                }
            Label_0271:
                goto Label_0283;
            }
            finally
            {
            Label_0276:
                ((List<RoomInfo>.Enumerator) enumerator).Dispose();
            }
        Label_0283:
            if (flag != null)
            {
                goto Label_034E;
            }
            enumerator2 = list.GetEnumerator();
        Label_0291:
            try
            {
                goto Label_0330;
            Label_0296:
                info3 = &enumerator2.Current;
                hashtable3 = info3.CustomProperties;
                if (hashtable3.ContainsKey("type") == null)
                {
                    goto Label_0330;
                }
                num9 = (int) hashtable3.get_Item("type");
                if (num9 != type)
                {
                    goto Label_0330;
                }
                if (lvRange == -1)
                {
                    goto Label_0321;
                }
                if (hashtable3.ContainsKey("plv") == null)
                {
                    goto Label_0321;
                }
                num10 = lv - lvRange;
                num11 = lv + lvRange;
                num12 = (int) hashtable3.get_Item("plv");
                if (num12 >= num10)
                {
                    goto Label_0321;
                }
                if (num11 >= num12)
                {
                    goto Label_0321;
                }
                goto Label_0330;
            Label_0321:
                flag = 1;
                str = info3.Name;
                goto Label_033C;
            Label_0330:
                if (&enumerator2.MoveNext() != null)
                {
                    goto Label_0296;
                }
            Label_033C:
                goto Label_034E;
            }
            finally
            {
            Label_0341:
                ((List<RoomInfo>.Enumerator) enumerator2).Dispose();
            }
        Label_034E:
            if (flag != null)
            {
                goto Label_03F4;
            }
            if (scoreRangeMax == -1)
            {
                goto Label_03F4;
            }
            enumerator3 = list.GetEnumerator();
        Label_0364:
            try
            {
                goto Label_03D6;
            Label_0369:
                info4 = &enumerator3.Current;
                hashtable4 = info4.CustomProperties;
                if (lvRange == -1)
                {
                    goto Label_03C7;
                }
                if (hashtable4.ContainsKey("plv") == null)
                {
                    goto Label_03C7;
                }
                num13 = lv - lvRange;
                num14 = lv + lvRange;
                num15 = (int) hashtable4.get_Item("plv");
                if (num15 >= num13)
                {
                    goto Label_03C7;
                }
                if (num14 >= num15)
                {
                    goto Label_03C7;
                }
                goto Label_03D6;
            Label_03C7:
                flag = 1;
                str = info4.Name;
                goto Label_03E2;
            Label_03D6:
                if (&enumerator3.MoveNext() != null)
                {
                    goto Label_0369;
                }
            Label_03E2:
                goto Label_03F4;
            }
            finally
            {
            Label_03E7:
                ((List<RoomInfo>.Enumerator) enumerator3).Dispose();
            }
        Label_03F4:
            flag3 = 0;
            if (flag == null)
            {
                goto Label_041F;
            }
            flag3 = PhotonNetwork.JoinRoom(str);
            if (flag3 == null)
            {
                goto Label_0418;
            }
            this.mState = 3;
            goto Label_041F;
        Label_0418:
            this.mError = 1;
        Label_041F:
            return flag3;
        }

        public bool JoinRoom(string roomName, string playerJson, bool isResume)
        {
            bool flag;
            Hashtable hashtable;
            Hashtable hashtable2;
            if (this.mState == 2)
            {
                goto Label_0015;
            }
            this.mError = 2;
            return 0;
        Label_0015:
            PhotonNetwork.player.SetCustomProperties(null, null, 0);
            PhotonNetwork.SetPlayerCustomProperties(null);
            this.SetMyPlayerParam(playerJson);
            if (isResume == null)
            {
                goto Label_0041;
            }
            flag = PhotonNetwork.JoinRoom(roomName);
            goto Label_006E;
        Label_0041:
            hashtable2 = new Hashtable();
            hashtable2.Add("name", roomName);
            hashtable2.Add("start", (bool) 0);
            hashtable = hashtable2;
            flag = PhotonNetwork.JoinRandomRoom(hashtable, 0);
        Label_006E:
            if (flag == null)
            {
                goto Label_0080;
            }
            this.mState = 3;
            goto Label_0087;
        Label_0080:
            this.mError = 1;
        Label_0087:
            return flag;
        }

        public unsafe bool JoinRoomCheckParam(string VersusHash, string playerJson, int lvRange, int floorRange, int lv, int floor, string lastuid, string myuid)
        {
            string str;
            bool flag;
            RoomInfo[] infoArray;
            List<RoomInfo> list;
            RoomInfo info;
            RoomInfo[] infoArray2;
            int num;
            Hashtable hashtable;
            string str2;
            string str3;
            int num2;
            int num3;
            RoomInfo info2;
            List<RoomInfo>.Enumerator enumerator;
            Hashtable hashtable2;
            int num4;
            int num5;
            RoomInfo info3;
            List<RoomInfo>.Enumerator enumerator2;
            Hashtable hashtable3;
            int num6;
            int num7;
            int num8;
            RoomInfo info4;
            List<RoomInfo>.Enumerator enumerator3;
            Hashtable hashtable4;
            int num9;
            bool flag2;
            if (this.mState == 2)
            {
                goto Label_0015;
            }
            this.mError = 2;
            return 0;
        Label_0015:
            str = string.Empty;
            flag = 0;
            PhotonNetwork.player.SetCustomProperties(null, null, 0);
            PhotonNetwork.SetPlayerCustomProperties(null);
            this.SetMyPlayerParam(playerJson);
            infoArray = PhotonNetwork.GetRoomList();
            list = new List<RoomInfo>();
            infoArray2 = infoArray;
            num = 0;
            goto Label_014B;
        Label_004E:
            info = infoArray2[num];
            hashtable = info.CustomProperties;
            if (hashtable.ContainsKey("MatchType") == null)
            {
                goto Label_0145;
            }
            if ((VersusHash == ((string) hashtable.get_Item("MatchType"))) == null)
            {
                goto Label_0145;
            }
            if (hashtable.ContainsKey("uid") == null)
            {
                goto Label_00CE;
            }
            if (string.IsNullOrEmpty(lastuid) != null)
            {
                goto Label_00CE;
            }
            str2 = (string) hashtable.get_Item("uid");
            if (string.Compare(str2, lastuid) != null)
            {
                goto Label_00CE;
            }
            goto Label_0145;
        Label_00CE:
            if (hashtable.ContainsKey("luid") == null)
            {
                goto Label_0111;
            }
            if (string.IsNullOrEmpty(myuid) != null)
            {
                goto Label_0111;
            }
            str3 = (string) hashtable.get_Item("luid");
            if (string.Compare(str3, myuid) != null)
            {
                goto Label_0111;
            }
            goto Label_0145;
        Label_0111:
            if (hashtable.ContainsKey("start") == null)
            {
                goto Label_013D;
            }
            if (((bool) hashtable.get_Item("start")) == null)
            {
                goto Label_013D;
            }
            goto Label_0145;
        Label_013D:
            list.Add(info);
        Label_0145:
            num += 1;
        Label_014B:
            if (num < ((int) infoArray2.Length))
            {
                goto Label_004E;
            }
            if (lvRange == -1)
            {
                goto Label_0218;
            }
            num2 = lv - lvRange;
            num3 = lv + lvRange;
            enumerator = list.GetEnumerator();
        Label_0171:
            try
            {
                goto Label_01FA;
            Label_0176:
                info2 = &enumerator.Current;
                hashtable2 = info2.CustomProperties;
                if (hashtable2.ContainsKey("plv") == null)
                {
                    goto Label_01FA;
                }
                if (hashtable2.ContainsKey("floor") == null)
                {
                    goto Label_01FA;
                }
                num4 = (int) hashtable2.get_Item("floor");
                num5 = (int) hashtable2.get_Item("plv");
                if (num2 > num5)
                {
                    goto Label_01FA;
                }
                if (num5 > num3)
                {
                    goto Label_01FA;
                }
                if (num4 != floor)
                {
                    goto Label_01FA;
                }
                str = info2.Name;
                flag = 1;
                goto Label_0206;
            Label_01FA:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0176;
                }
            Label_0206:
                goto Label_0218;
            }
            finally
            {
            Label_020B:
                ((List<RoomInfo>.Enumerator) enumerator).Dispose();
            }
        Label_0218:
            if (flag != null)
            {
                goto Label_0293;
            }
            enumerator2 = list.GetEnumerator();
        Label_0226:
            try
            {
                goto Label_0275;
            Label_022B:
                info3 = &enumerator2.Current;
                hashtable3 = info3.CustomProperties;
                if (hashtable3.ContainsKey("floor") == null)
                {
                    goto Label_0275;
                }
                if (floor != ((int) hashtable3.get_Item("floor")))
                {
                    goto Label_0275;
                }
                str = info3.Name;
                flag = 1;
                goto Label_0281;
            Label_0275:
                if (&enumerator2.MoveNext() != null)
                {
                    goto Label_022B;
                }
            Label_0281:
                goto Label_0293;
            }
            finally
            {
            Label_0286:
                ((List<RoomInfo>.Enumerator) enumerator2).Dispose();
            }
        Label_0293:
            if (floorRange == -1)
            {
                goto Label_0353;
            }
            if (flag != null)
            {
                goto Label_0353;
            }
            num6 = 1;
            goto Label_034A;
        Label_02A9:
            num7 = floor - num6;
            num8 = floor + num6;
            enumerator3 = list.GetEnumerator();
        Label_02BF:
            try
            {
                goto Label_031B;
            Label_02C4:
                info4 = &enumerator3.Current;
                hashtable4 = info4.CustomProperties;
                if (hashtable4.ContainsKey("floor") == null)
                {
                    goto Label_031B;
                }
                num9 = (int) hashtable4.get_Item("floor");
                if (num7 > num9)
                {
                    goto Label_031B;
                }
                if (num9 > num8)
                {
                    goto Label_031B;
                }
                str = info4.Name;
                flag = 1;
                goto Label_0327;
            Label_031B:
                if (&enumerator3.MoveNext() != null)
                {
                    goto Label_02C4;
                }
            Label_0327:
                goto Label_0339;
            }
            finally
            {
            Label_032C:
                ((List<RoomInfo>.Enumerator) enumerator3).Dispose();
            }
        Label_0339:
            if (flag == null)
            {
                goto Label_0344;
            }
            goto Label_0353;
        Label_0344:
            num6 += 1;
        Label_034A:
            if (num6 <= floorRange)
            {
                goto Label_02A9;
            }
        Label_0353:
            flag2 = 0;
            if (flag == null)
            {
                goto Label_037E;
            }
            flag2 = PhotonNetwork.JoinRoom(str);
            if (flag2 == null)
            {
                goto Label_0377;
            }
            this.mState = 3;
            goto Label_037E;
        Label_0377:
            this.mError = 1;
        Label_037E:
            return flag2;
        }

        public bool LeaveRoom()
        {
            bool flag;
            if (this.mState == 4)
            {
                goto Label_0015;
            }
            this.mError = 2;
            return 0;
        Label_0015:
            flag = PhotonNetwork.LeaveRoom();
            if (flag == null)
            {
                goto Label_002D;
            }
            this.mState = 5;
            goto Label_0034;
        Label_002D:
            this.mError = 1;
        Label_0034:
            return flag;
        }

        private void Log(string str)
        {
            if (GameUtility.IsDebugBuild != null)
            {
                goto Label_000B;
            }
            return;
        Label_000B:
            DebugUtility.Log(str);
            return;
        }

        private void LogError(string str)
        {
            if (GameUtility.IsDebugBuild != null)
            {
                goto Label_000B;
            }
            return;
        Label_000B:
            DebugUtility.LogError(str);
            return;
        }

        private void LogWarning(string str)
        {
            if (GameUtility.IsDebugBuild != null)
            {
                goto Label_000B;
            }
            return;
        Label_000B:
            DebugUtility.LogWarning(str);
            return;
        }

        public override void OnConnectedToMaster()
        {
            this.Log("Joined Default Lobby.");
            this.mState = 2;
            this.mEvents.Clear();
            this.IsRoomListUpdated = 0;
            this.Log(PhotonNetwork.ServerAddress);
            return;
        }

        public override void OnConnectedToPhoton()
        {
            this.Log("Connected to Photon Server");
            this.Log(PhotonNetwork.ServerAddress);
            return;
        }

        public override void OnConnectionFail(DisconnectCause cause)
        {
            this.Log("ConnectionFail." + ((DisconnectCause) cause).ToString());
            if (cause == 0x410)
            {
                goto Label_0031;
            }
            if (cause != 0x411)
            {
                goto Label_0038;
            }
        Label_0031:
            this.mError = 3;
        Label_0038:
            if (cause != 0x412)
            {
                goto Label_004A;
            }
            this.mError = 5;
        Label_004A:
            this.mState = 0;
            return;
        }

        public override void OnDisconnectedFromPhoton()
        {
            object[] objArray1;
            objArray1 = new object[] { "DisconnectedFromPhoton. LostPacket:", (int) PhotonNetwork.PacketLossByCrcCheck, " MaxResendsBeforeDisconnect:", (int) PhotonNetwork.MaxResendsBeforeDisconnect, " ResentReliableCommands", (int) PhotonNetwork.ResentReliableCommands };
            this.Log(string.Concat(objArray1));
            this.mState = 0;
            return;
        }

        private void OnEventHandler(byte eventCode, object content, int senderID)
        {
            Hashtable hashtable;
            string str;
            int num;
            byte[] buffer;
            byte[] buffer2;
            byte[] buffer3;
            MyEvent event2;
            hashtable = (Hashtable) content;
            str = null;
            if (hashtable.ContainsKey("s") == null)
            {
                goto Label_0087;
            }
            num = (int) hashtable.get_Item("s");
            if (num != null)
            {
                goto Label_0056;
            }
            if (hashtable.ContainsKey("m") == null)
            {
                goto Label_0087;
            }
            str = (string) hashtable.get_Item("m");
            goto Label_0087;
        Label_0056:
            if (hashtable.ContainsKey("m") == null)
            {
                goto Label_0087;
            }
            buffer = (byte[]) hashtable.get_Item("m");
            str = MyEncrypt.Decrypt(num + this.GetCryptKey(), buffer, 1);
        Label_0087:
            buffer2 = null;
            if (hashtable.ContainsKey("bm") == null)
            {
                goto Label_00B5;
            }
            buffer3 = (byte[]) hashtable.get_Item("bm");
            buffer2 = MyEncrypt.Decrypt(buffer3);
        Label_00B5:
            event2 = new MyEvent();
            event2.code = eventCode;
            event2.playerID = senderID;
            event2.json = str;
            event2.binary = buffer2;
            if (hashtable.ContainsKey("sq") == null)
            {
                goto Label_0104;
            }
            event2.sendID = (int) hashtable.get_Item("sq");
        Label_0104:
            this.mEvents.Add(event2);
            if (this.SortRoomMessage == null)
            {
                goto Label_0144;
            }
            if (<>f__am$cache17 != null)
            {
                goto Label_013A;
            }
            <>f__am$cache17 = new Comparison<MyEvent>(MyPhoton.<OnEventHandler>m__214);
        Label_013A:
            this.mEvents.Sort(<>f__am$cache17);
        Label_0144:
            this.Log("OnEventHandler: " + ((int) senderID) + ((string) hashtable.get_Item("msg")));
            return;
        }

        public override void OnFailedToConnectToPhoton(DisconnectCause cause)
        {
            this.Log("FailedToConnectToPhoton." + ((DisconnectCause) cause).ToString());
            if (cause == 0x410)
            {
                goto Label_0031;
            }
            if (cause != 0x411)
            {
                goto Label_0038;
            }
        Label_0031:
            this.mError = 3;
        Label_0038:
            if (cause != 0x412)
            {
                goto Label_004A;
            }
            this.mError = 5;
        Label_004A:
            this.mState = 0;
            return;
        }

        public override void OnJoinedLobby()
        {
            this.Log("Joined Lobby.");
            this.mState = 2;
            this.mEvents.Clear();
            this.IsRoomListUpdated = 0;
            this.Log(PhotonNetwork.ServerAddress);
            return;
        }

        public override void OnJoinedRoom()
        {
            this.Log("Joined Room.");
            this.mEvents.Clear();
            this.mState = 4;
            this.mSendRoomMessageID = 0;
            return;
        }

        public override void OnLeftRoom()
        {
            this.Log("Left Room.");
            this.mState = 2;
            this.mEvents.Clear();
            return;
        }

        public override void OnPhotonCreateRoomFailed(object[] codeAndMsg)
        {
            int num;
            string str;
            this.Log("Create Room failed.");
            if (codeAndMsg == null)
            {
                goto Label_0027;
            }
            if (((int) codeAndMsg.Length) < 2)
            {
                goto Label_0027;
            }
            if ((codeAndMsg[0] as IConvertible) != null)
            {
                goto Label_003E;
            }
        Label_0027:
            this.mError = 1;
            this.Log("codeAndMsg is null");
            goto Label_008B;
        Label_003E:
            if (((IConvertible) codeAndMsg[0]).ToInt32(null) != 0x7ffe)
            {
                goto Label_0064;
            }
            this.mError = 6;
            goto Label_006B;
        Label_0064:
            this.mError = 1;
        Label_006B:
            str = (string) codeAndMsg[1];
            if (str == null)
            {
                goto Label_008B;
            }
            this.Log("err:" + str);
        Label_008B:
            this.mState = 2;
            return;
        }

        public override unsafe void OnPhotonCustomRoomPropertiesChanged(Hashtable propertiesThatChanged)
        {
            MyRoom room;
            string str;
            int num;
            JSON_MyPhotonRoomParam param;
            this.Log("Update Room Property");
            this.mIsUpdateRoomProperty = 1;
            if (propertiesThatChanged.ContainsKey("Audience") == null)
            {
                goto Label_0088;
            }
            if (this.IsOldestPlayer() == null)
            {
                goto Label_0088;
            }
            room = this.GetCurrentRoom();
            if (room == null)
            {
                goto Label_0088;
            }
            str = propertiesThatChanged.get_Item("Audience").ToString();
            num = 0;
            int.TryParse(str, &num);
            param = JSON_MyPhotonRoomParam.Parse(room.json);
            if (param == null)
            {
                goto Label_0088;
            }
            if (num == param.audienceNum)
            {
                goto Label_0088;
            }
            param.audienceNum = num;
            this.SetRoomParam(param.Serialize());
        Label_0088:
            return;
        }

        public override void OnPhotonJoinRoomFailed(object[] codeAndMsg)
        {
            int num;
            string str;
            this.Log("Join Room failed.");
            if (codeAndMsg == null)
            {
                goto Label_0027;
            }
            if (((int) codeAndMsg.Length) < 2)
            {
                goto Label_0027;
            }
            if ((codeAndMsg[0] as IConvertible) != null)
            {
                goto Label_0033;
            }
        Label_0027:
            this.mError = 1;
            goto Label_00AF;
        Label_0033:
            num = ((IConvertible) codeAndMsg[0]).ToInt32(null);
            if (num != 0x7ffd)
            {
                goto Label_0059;
            }
            this.mError = 7;
            goto Label_008F;
        Label_0059:
            if (num != 0x7ff6)
            {
                goto Label_0070;
            }
            this.mError = 8;
            goto Label_008F;
        Label_0070:
            if (num != 0x7ffc)
            {
                goto Label_0088;
            }
            this.mError = 9;
            goto Label_008F;
        Label_0088:
            this.mError = 1;
        Label_008F:
            str = (string) codeAndMsg[1];
            if (str == null)
            {
                goto Label_00AF;
            }
            this.Log("err:" + str);
        Label_00AF:
            this.mState = 2;
            return;
        }

        public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
        {
            Hashtable hashtable;
            if (GlobalVars.SelectedMultiPlayRoomType != 1)
            {
                goto Label_0047;
            }
            if (PhotonNetwork.isMasterClient == null)
            {
                goto Label_0047;
            }
            hashtable = newPlayer.CustomProperties;
            if (hashtable == null)
            {
                goto Label_0047;
            }
            if (hashtable.ContainsKey("Logger") == null)
            {
                goto Label_0047;
            }
            if (PhotonNetwork.room == null)
            {
                goto Label_0047;
            }
            PhotonNetwork.room.IsVisible = 1;
        Label_0047:
            this.Log("Join other player to your room. playerID:" + ((int) newPlayer.ID));
            return;
        }

        public override void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer)
        {
            this.Log("Leave other player from your room. playerID:" + ((int) otherPlayer.ID));
            this.IsUpdatePlayerProperty = 1;
            return;
        }

        public override void OnPhotonPlayerPropertiesChanged(object[] playerAndupdateProps)
        {
            this.IsUpdatePlayerProperty = 1;
            return;
        }

        public override void OnPhotonRandomJoinFailed(object[] codeAndMsg)
        {
            int num;
            string str;
            this.Log("Join Room failed.");
            if (codeAndMsg == null)
            {
                goto Label_0027;
            }
            if (((int) codeAndMsg.Length) < 2)
            {
                goto Label_0027;
            }
            if ((codeAndMsg[0] as IConvertible) != null)
            {
                goto Label_0033;
            }
        Label_0027:
            this.mError = 1;
            goto Label_0080;
        Label_0033:
            if (((IConvertible) codeAndMsg[0]).ToInt32(null) != 0x7ff8)
            {
                goto Label_0059;
            }
            this.mError = 8;
            goto Label_0060;
        Label_0059:
            this.mError = 1;
        Label_0060:
            str = (string) codeAndMsg[1];
            if (str == null)
            {
                goto Label_0080;
            }
            this.Log("err:" + str);
        Label_0080:
            this.mState = 2;
            return;
        }

        public override void OnReceivedRoomListUpdate()
        {
            this.Log("Room List Updated.");
            this.mIsRoomListUpdated = 1;
            return;
        }

        public override unsafe void OnWebRpcResponse(OperationResponse response)
        {
            object[] objArray2;
            object[] objArray1;
            WebRpcResponse response2;
            Dictionary<string, object> dictionary;
            KeyValuePair<string, object> pair;
            Dictionary<string, object>.Enumerator enumerator;
            this.Log("WebRpc:" + response.ToStringFull());
            if (response.ReturnCode == null)
            {
                goto Label_00E9;
            }
            response2 = new WebRpcResponse(response);
            if (response2.ReturnCode == null)
            {
                goto Label_007C;
            }
            objArray1 = new object[] { "WebRPC '", response2.Name, "' に失敗しました. Error: ", (int) response2.ReturnCode, " Message: ", response2.DebugMessage };
            this.Log(string.Concat(objArray1));
        Label_007C:
            enumerator = response2.Parameters.GetEnumerator();
        Label_008A:
            try
            {
                goto Label_00CC;
            Label_008F:
                pair = &enumerator.Current;
                objArray2 = new object[] { "Key:", &pair.Key, "/ Value:", &pair.Value };
                this.Log(string.Concat(objArray2));
            Label_00CC:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_008F;
                }
                goto Label_00E9;
            }
            finally
            {
            Label_00DD:
                ((Dictionary<string, object>.Enumerator) enumerator).Dispose();
            }
        Label_00E9:
            return;
        }

        public bool OpenRoom(bool isvisible, bool isstarted)
        {
            byte[] buffer;
            Hashtable hashtable;
            Hashtable hashtable2;
            Hashtable hashtable3;
            if (this.mState == 4)
            {
                goto Label_0015;
            }
            this.mError = 2;
            return 0;
        Label_0015:
            PhotonNetwork.room.IsOpen = 1;
            PhotonNetwork.room.IsVisible = isvisible;
            buffer = null;
            if (PhotonNetwork.room.CustomProperties.Count <= 0)
            {
                goto Label_0070;
            }
            hashtable = PhotonNetwork.room.CustomProperties;
            if (hashtable == null)
            {
                goto Label_0070;
            }
            if (hashtable.Count <= 0)
            {
                goto Label_0070;
            }
            buffer = (byte[]) hashtable.get_Item("json");
        Label_0070:
            hashtable3 = new Hashtable();
            hashtable3.Add("json", buffer);
            hashtable3.Add("start", (bool) isstarted);
            hashtable3.Add("battle", (bool) isstarted);
            hashtable2 = hashtable3;
            PhotonNetwork.room.SetCustomProperties(null, null, 0);
            PhotonNetwork.room.SetCustomProperties(hashtable2, null, 0);
            return 1;
        }

        protected override void Release()
        {
        }

        public void Reset()
        {
            if (this.mState == null)
            {
                goto Label_0011;
            }
            this.Disconnect();
        Label_0011:
            this.MyPlayerIndex = 0;
            this.IsMultiPlay = 0;
            this.IsMultiVersus = 0;
            this.IsRankMatch = 0;
            this.mPlayersStarted.Clear();
            return;
        }

        public void ResetLastError()
        {
            this.mError = 0;
            return;
        }

        public MyRoom SearchRoom(int roomID)
        {
            MyRoom room;
            int num;
            List<MyRoom> list;
            int num2;
            JSON_MyPhotonRoomParam param;
            room = null;
            num = GlobalVars.SelectedMultiPlayRoomID;
            if (this.CurrentState == 2)
            {
                goto Label_0016;
            }
            return null;
        Label_0016:
            list = this.GetRoomList();
            num2 = 0;
            goto Label_00B3;
        Label_0024:
            if ((list[num2].lobby != "vs") == null)
            {
                goto Label_0044;
            }
            goto Label_00AF;
        Label_0044:
            if (list[num2].name.IndexOf("_friend") != -1)
            {
                goto Label_0065;
            }
            goto Label_00AF;
        Label_0065:
            if (string.IsNullOrEmpty(list[num2].json) != null)
            {
                goto Label_00AF;
            }
            param = JSON_MyPhotonRoomParam.Parse(list[num2].json);
            if (param == null)
            {
                goto Label_00AF;
            }
            if (param.roomid != num)
            {
                goto Label_00AF;
            }
            room = list[num2];
            goto Label_00BF;
        Label_00AF:
            num2 += 1;
        Label_00B3:
            if (num2 < list.Count)
            {
                goto Label_0024;
            }
        Label_00BF:
            return room;
        }

        public void SendFlush()
        {
            PhotonNetwork.SendOutgoingCommands();
            return;
        }

        public bool SendRoomMessage(bool reliable, string msg, SEND_TYPE eventcode)
        {
            int num;
            Hashtable hashtable;
            byte[] buffer;
            bool flag;
            Hashtable hashtable2;
            if (this.mState == 4)
            {
                goto Label_000E;
            }
            return 0;
        Label_000E:
            num = 0;
            hashtable = null;
            if (num != null)
            {
                goto Label_0046;
            }
            hashtable2 = new Hashtable();
            hashtable2.Add("s", (int) num);
            hashtable2.Add("m", msg);
            hashtable = hashtable2;
            goto Label_007F;
        Label_0046:
            buffer = MyEncrypt.Encrypt(num + this.GetCryptKey(), msg, 1);
            hashtable2 = new Hashtable();
            hashtable2.Add("s", (int) num);
            hashtable2.Add("m", buffer);
            hashtable = hashtable2;
        Label_007F:
            if (this.SortRoomMessage == null)
            {
                goto Label_00AE;
            }
            hashtable.Add("sq", (int) this.mSendRoomMessageID);
            this.mSendRoomMessageID += 1;
        Label_00AE:
            flag = PhotonNetwork.RaiseEvent(eventcode, hashtable, reliable, null);
            if (this.DisconnectIfSendRoomMessageFailed == null)
            {
                goto Label_00E3;
            }
            if (flag != null)
            {
                goto Label_00E3;
            }
            this.Disconnect();
            this.mError = 10;
            DebugUtility.LogError("SendRoomMessage failed!");
            return 0;
        Label_00E3:
            return flag;
        }

        public bool SendRoomMessageBinary(bool reliable, byte[] msg, SEND_TYPE eventcode, bool isWrite)
        {
            Hashtable hashtable;
            byte[] buffer;
            bool flag;
            Hashtable hashtable2;
            if (this.mState == 4)
            {
                goto Label_000E;
            }
            return 0;
        Label_000E:
            hashtable = null;
            buffer = MyEncrypt.Encrypt(msg);
            hashtable2 = new Hashtable();
            hashtable2.Add("bm", buffer);
            hashtable = hashtable2;
            if (this.SortRoomMessage == null)
            {
                goto Label_005A;
            }
            hashtable.Add("sq", (int) this.mSendRoomMessageID);
            this.mSendRoomMessageID += 1;
        Label_005A:
            flag = PhotonNetwork.RaiseEvent(eventcode, hashtable, reliable, null);
            if (this.DisconnectIfSendRoomMessageFailed == null)
            {
                goto Label_008F;
            }
            if (flag != null)
            {
                goto Label_008F;
            }
            this.Disconnect();
            this.mError = 10;
            DebugUtility.LogError("SendRoomMessage failed!");
            return 0;
        Label_008F:
            return flag;
        }

        public void SetMyPlayerParam(string json)
        {
            Hashtable hashtable;
            Hashtable hashtable2;
            hashtable2 = new Hashtable();
            hashtable2.Add("json", GameUtility.Object2Binary<string>(json));
            hashtable = hashtable2;
            PhotonNetwork.player.SetCustomProperties(hashtable, null, 0);
            return;
        }

        public void SetResumeMyPlayer(int playerID)
        {
            MyPlayer player;
            byte[] buffer;
            Hashtable hashtable;
            Hashtable hashtable2;
            Hashtable hashtable3;
            if (this.GetMyPlayer() == null)
            {
                goto Label_0089;
            }
            buffer = null;
            hashtable = PhotonNetwork.player.CustomProperties;
            if (hashtable == null)
            {
                goto Label_0041;
            }
            if (hashtable.ContainsKey("json") == null)
            {
                goto Label_0041;
            }
            buffer = (byte[]) hashtable.get_Item("json");
        Label_0041:
            hashtable3 = new Hashtable();
            hashtable3.Add("json", buffer);
            hashtable3.Add("resume", (bool) 1);
            hashtable3.Add("resumeID", (int) playerID);
            hashtable2 = hashtable3;
            PhotonNetwork.player.SetCustomProperties(hashtable2, null, 0);
        Label_0089:
            return;
        }

        public bool SetRoomParam(string json)
        {
            Hashtable hashtable;
            Room room;
            Hashtable hashtable2;
            if (this.mState == 4)
            {
                goto Label_0015;
            }
            this.mError = 2;
            return 0;
        Label_0015:
            hashtable2 = new Hashtable();
            hashtable2.Add("json", GameUtility.Object2Binary<string>(json));
            hashtable = hashtable2;
            if (PhotonNetwork.room != null)
            {
                goto Label_0043;
            }
            this.mError = 2;
            return 0;
        Label_0043:
            PhotonNetwork.room.SetCustomProperties(hashtable, null, 0);
            return 1;
        }

        public bool SetRoomParam(string key, string param)
        {
            Room room;
            Hashtable hashtable;
            Hashtable hashtable2;
            if (this.mState == 4)
            {
                goto Label_0015;
            }
            this.mError = 2;
            return 0;
        Label_0015:
            room = PhotonNetwork.room;
            if (room != null)
            {
                goto Label_0023;
            }
            return 0;
        Label_0023:
            hashtable2 = new Hashtable();
            hashtable2.Add(key, param);
            hashtable = hashtable2;
            room.SetCustomProperties(null, null, 0);
            room.SetCustomProperties(hashtable, null, 0);
            return 1;
        }

        public bool StartConnect(string appID, bool autoJoin, string ver)
        {
            bool flag;
            if (this.mState == null)
            {
                goto Label_0014;
            }
            this.mError = 2;
            return 0;
        Label_0014:
            this.CurrentAppID = appID;
            PhotonNetwork.autoJoinLobby = autoJoin;
            PhotonNetwork.PhotonServerSettings.AppID = appID;
            PhotonNetwork.PhotonServerSettings.Protocol = 1;
            PhotonNetwork.player.NickName = MonoSingleton<GameManager>.Instance.DeviceId;
            flag = PhotonNetwork.ConnectUsingSettings(ver);
            if (flag == null)
            {
                goto Label_006A;
            }
            this.mState = 1;
            PhotonNetwork.NetworkStatisticsEnabled = 1;
            goto Label_0078;
        Label_006A:
            this.mState = 0;
            this.mError = 1;
        Label_0078:
            this.Log("StartConnect:" + ((bool) flag));
            this.IsUpdatePlayerProperty = 0;
            return flag;
        }

        private void Update()
        {
            object[] objArray1;
            NetworkReachability reachability;
            int num;
            if (this.mState != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            reachability = Application.get_internetReachability();
            if (this.mState == null)
            {
                goto Label_0067;
            }
            if (reachability == this.mNetworkReach)
            {
                goto Label_0067;
            }
            if (this.mNetworkReach == null)
            {
                goto Label_0067;
            }
            objArray1 = new object[] { "internet reach change to ", (NetworkReachability) reachability, "\n", this.GetTrafficState() };
            this.LogWarning(string.Concat(objArray1));
        Label_0067:
            this.mNetworkReach = reachability;
            if (this.mState == 4)
            {
                goto Label_008D;
            }
            this.mDelaySec = -1f;
            this.mSendRoomMessageID = 0;
            return;
        Label_008D:
            num = SupportClass.GetTickCount() - PhotonNetwork.networkingPeer.get_TimestampOfLastSocketReceive();
            if (((float) num) >= (this.TimeOutSec * 1000f))
            {
                goto Label_00BD;
            }
            this.mDelaySec = -1f;
            return;
        Label_00BD:
            if (this.mDelaySec >= 0f)
            {
                goto Label_00F3;
            }
            this.mDelaySec = 0f;
            this.LogWarning(PhotonNetwork.NetworkStatisticsToString() + "\n" + this.GetTrafficState());
        Label_00F3:
            this.mDelaySec += Time.get_deltaTime();
            if (this.mDelaySec >= this.TimeOutSec)
            {
                goto Label_0117;
            }
            return;
        Label_0117:
            this.LogWarning("maybe connection lost.");
            this.LogWarning(PhotonNetwork.NetworkStatisticsToString() + "\n" + this.GetTrafficState());
            this.Disconnect();
            this.mError = 4;
            return;
        }

        public bool UpdateRoomParam(string key, object param)
        {
            Room room;
            Hashtable hashtable;
            if (this.mState == 4)
            {
                goto Label_0015;
            }
            this.mError = 2;
            return 0;
        Label_0015:
            room = PhotonNetwork.room;
            if (room != null)
            {
                goto Label_0023;
            }
            return 0;
        Label_0023:
            hashtable = room.CustomProperties;
            if (hashtable == null)
            {
                goto Label_0051;
            }
            if (hashtable.ContainsKey(key) == null)
            {
                goto Label_0049;
            }
            hashtable.set_Item(key, param);
            goto Label_0051;
        Label_0049:
            hashtable.Add(key, param);
        Label_0051:
            room.SetCustomProperties(null, null, 0);
            room.SetCustomProperties(hashtable, null, 0);
            return 1;
        }

        public MyState CurrentState
        {
            get
            {
                return this.mState;
            }
        }

        public bool IsRoomListUpdated
        {
            get
            {
                return this.mIsRoomListUpdated;
            }
            set
            {
                this.mIsRoomListUpdated = value;
                return;
            }
        }

        public bool IsUpdateRoomProperty
        {
            get
            {
                return this.mIsUpdateRoomProperty;
            }
            set
            {
                this.mIsUpdateRoomProperty = value;
                return;
            }
        }

        public bool IsUpdatePlayerProperty
        {
            get
            {
                return this.mIsUpdatePlayerProperty;
            }
            set
            {
                this.mIsUpdatePlayerProperty = value;
                return;
            }
        }

        public MyError LastError
        {
            get
            {
                return this.mError;
            }
        }

        public float TimeOutSec
        {
            [CompilerGenerated]
            get
            {
                return this.<TimeOutSec>k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this.<TimeOutSec>k__BackingField = value;
                return;
            }
        }

        public bool SendRoomMessageFlush
        {
            [CompilerGenerated]
            get
            {
                return this.<SendRoomMessageFlush>k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this.<SendRoomMessageFlush>k__BackingField = value;
                return;
            }
        }

        public bool DisconnectIfSendRoomMessageFailed
        {
            [CompilerGenerated]
            get
            {
                return this.<DisconnectIfSendRoomMessageFailed>k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this.<DisconnectIfSendRoomMessageFailed>k__BackingField = value;
                return;
            }
        }

        public bool SortRoomMessage
        {
            [CompilerGenerated]
            get
            {
                return this.<SortRoomMessage>k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this.<SortRoomMessage>k__BackingField = value;
                return;
            }
        }

        public PeerStateValue ConnectState
        {
            get
            {
                return PhotonNetwork.networkingPeer.get_PeerState();
            }
        }

        public string CurrentAppID
        {
            [CompilerGenerated]
            get
            {
                return this.<CurrentAppID>k__BackingField;
            }
            [CompilerGenerated]
            private set
            {
                this.<CurrentAppID>k__BackingField = value;
                return;
            }
        }

        public bool UseEncrypt
        {
            [CompilerGenerated]
            get
            {
                return this.<UseEncrypt>k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this.<UseEncrypt>k__BackingField = value;
                return;
            }
        }

        public int MyPlayerIndex
        {
            [CompilerGenerated]
            get
            {
                return this.<MyPlayerIndex>k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this.<MyPlayerIndex>k__BackingField = value;
                return;
            }
        }

        public bool IsMultiPlay
        {
            [CompilerGenerated]
            get
            {
                return this.<IsMultiPlay>k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this.<IsMultiPlay>k__BackingField = value;
                return;
            }
        }

        public bool IsMultiVersus
        {
            [CompilerGenerated]
            get
            {
                return this.<IsMultiVersus>k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this.<IsMultiVersus>k__BackingField = value;
                return;
            }
        }

        public bool IsRankMatch
        {
            [CompilerGenerated]
            get
            {
                return this.<IsRankMatch>k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this.<IsRankMatch>k__BackingField = value;
                return;
            }
        }

        [CompilerGenerated]
        private sealed class <CheckTowerRoomIsBattle>c__AnonStorey2AA
        {
            internal string roomname;

            public <CheckTowerRoomIsBattle>c__AnonStorey2AA()
            {
                base..ctor();
                return;
            }

            internal bool <>m__21D(MyPhoton.MyRoom r)
            {
                return (((r.lobby == "tower") == null) ? 0 : (r.name == this.roomname));
            }
        }

        [CompilerGenerated]
        private sealed class <FindPlayer>c__AnonStorey2A9
        {
            internal int playerID;
            internal int playerIndex;

            public <FindPlayer>c__AnonStorey2A9()
            {
                base..ctor();
                return;
            }

            internal bool <>m__219(MyPhoton.MyPlayer p)
            {
                return (p.playerID == this.playerID);
            }

            internal bool <>m__21A(MyPhoton.MyPlayer p)
            {
                return (p.photonPlayerID == this.playerID);
            }

            internal bool <>m__21B(MyPhoton.MyPlayer p)
            {
                return (p.playerID == this.playerIndex);
            }

            internal bool <>m__21C(MyPhoton.MyPlayer p)
            {
                return (p.photonPlayerID == this.playerIndex);
            }
        }

        [CompilerGenerated]
        private sealed class <JoinRandomRoom>c__AnonStorey2A6
        {
            internal string myuid;

            public <JoinRandomRoom>c__AnonStorey2A6()
            {
                base..ctor();
                return;
            }

            internal bool <>m__215(string id)
            {
                return (id == this.myuid);
            }
        }

        [CompilerGenerated]
        private sealed class <JoinRandomRoom>c__AnonStorey2A7
        {
            internal string target_uid;

            public <JoinRandomRoom>c__AnonStorey2A7()
            {
                base..ctor();
                return;
            }

            internal bool <>m__217(string uid)
            {
                return (uid == this.target_uid);
            }
        }

        [CompilerGenerated]
        private sealed class <JoinRankMatchRoomCheckParam>c__AnonStorey2A8
        {
            internal int score;

            public <JoinRankMatchRoomCheckParam>c__AnonStorey2A8()
            {
                base..ctor();
                return;
            }

            internal int <>m__218(RoomInfo a, RoomInfo b)
            {
                int num;
                int num2;
                Hashtable hashtable;
                num = 0;
                num2 = 0;
                hashtable = a.CustomProperties;
                if (hashtable.ContainsKey("score") == null)
                {
                    goto Label_0038;
                }
                num = Math.Abs(this.score - ((int) hashtable.get_Item("score")));
            Label_0038:
                hashtable = b.CustomProperties;
                if (hashtable.ContainsKey("score") == null)
                {
                    goto Label_006C;
                }
                num2 = Math.Abs(this.score - ((int) hashtable.get_Item("score")));
            Label_006C:
                return (num - num2);
            }
        }

        public enum MyError
        {
            NOP,
            UNKNOWN,
            ILLEGAL_STATE,
            TIMEOUT,
            TIMEOUT2,
            FULL_CLIENTS,
            ROOM_NAME_DUPLICATED,
            ROOM_IS_FULL,
            ROOM_IS_NOT_EXIST,
            ROOM_IS_NOT_OPEN,
            RAISE_EVENT_FAILED,
            NUM
        }

        public class MyEvent
        {
            public MyPhoton.SEND_TYPE code;
            public int playerID;
            public string json;
            public byte[] binary;
            public int sendID;

            public MyEvent()
            {
                base..ctor();
                return;
            }
        }

        public class MyPlayer
        {
            public int photonPlayerID;
            public int resumeID;
            public string json;
            public bool start;

            public MyPlayer()
            {
                this.resumeID = -1;
                this.json = string.Empty;
                base..ctor();
                return;
            }

            public int playerID
            {
                get
                {
                    return ((this.resumeID == -1) ? this.photonPlayerID : this.resumeID);
                }
                set
                {
                    this.photonPlayerID = value;
                    return;
                }
            }
        }

        public class MyRoom
        {
            public string name;
            public int playerCount;
            public int maxPlayers;
            public bool open;
            public bool start;
            public bool battle;
            public bool draft;
            public string json;
            public string lobby;
            public int audience;
            public int audienceMax;

            public MyRoom()
            {
                this.name = string.Empty;
                this.maxPlayers = 1;
                this.open = 1;
                this.json = string.Empty;
                this.lobby = string.Empty;
                base..ctor();
                return;
            }
        }

        public enum MyState
        {
            NOP,
            CONNECTING,
            LOBBY,
            JOINING,
            ROOM,
            LEAVING,
            NUM
        }

        public enum SEND_TYPE : byte
        {
            Normal = 0,
            Resume = 1,
            Sync = 2
        }
    }
}

