namespace SRPG
{
    using GR;
    using Gsc.App;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    [Pin(0x17, "MultiTowerRoom", 0, 0x17), Pin(0x2af8, "NotChallengeFloor", 1, 0x2af8), Pin(0x2710, "VersusFaildSeasonGift", 1, 0x2710), Pin(0x2328, "VersusNotPhotonID", 1, 0x2328), Pin(0x1f40, "VersusFailRoomID", 1, 0x1f40), Pin(0x1b58, "VersusNotLineRoom", 1, 0x1b58), Pin(0x1770, "MultiMaintenance", 1, 0x1770), Pin(0x1388, "NoMatchVersion", 1, 0x1388), Pin(0x1326, "RepelledBlockList", 1, 0x1326), Pin(0x1324, "NoRoom", 1, 0x1324), Pin(0x12c2, "OutOfDateQuest", 1, 0x12c2), Pin(0x12c1, "IllegalComment", 1, 0x12c1), Pin(0x12c0, "FailedMakeRoom", 1, 0x12c0), Pin(0x65, "Failure", 1, 0x65), Pin(100, "Success", 1, 100), Pin(80, "Lobby", 0, 80), Pin(0x47, "RankMatchCreateRoom", 0, 0x47), Pin(70, "RankMatchStatus", 0, 70), Pin(0x48, "RankMatchStart", 0, 0x48), Pin(60, "MultiRoom_WithGPS", 0, 60), Pin(0x38, "PassLock", 0, 0x38), Pin(0x37, "PassRelease", 0, 0x37), Pin(0x33, "RoomJoinInvitation", 0, 0x33), Pin(50, "RoomInvitation", 0, 50), Pin(0x21, "VersusDraftPartySelected", 0, 0x21), Pin(0x20, "VersusDraftUnitSelected", 0, 0x20), Pin(0x1f, "VersusDraftUnitList", 0, 0x1f), Pin(30, "MultiTowerRoomJoinList", 0, 30), Pin(0x1d, "MultiTowerInRoom", 0, 0x1d), Pin(0x1c, "MultiTowerJoinInvitation", 0, 0x1c), Pin(0x1b, "MultiTowerStatus", 0, 0x1b), Pin(0x1a, "MultiTowerRoomUpdate", 0, 0x1a), Pin(0x19, "MultiTowerRoomJoinID", 0, 0x19), Pin(0x18, "MultiTowerRoomJoin", 0, 0x18), Pin(0x16, "MultiTowerRoomMake", 0, 0x16), Pin(0x15, "MultiTowerAutoCreate", 0, 0x15), Pin(20, "VersusFriendScore", 0, 20), Pin(0x13, "VersusRecvSeasonGift", 0, 0x13), Pin(0x12, "VersusStatus", 0, 0x12), Pin(0x11, "VersusLineJoin", 0, 0x11), Pin(0x10, "VersusLineMake", 0, 0x10), Pin(15, "VersusLineReq", 0, 15), Pin(14, "VersusReset", 0, 14), Pin(13, "VersusRoomUpdate", 0, 13), Pin(12, "VersusRoomJoinID", 0, 12), Pin(11, "VersusCreateRoom", 0, 11), Pin(10, "VersusStart", 0, 10), Pin(9, "CheckVersion", 0, 9), Pin(8, "RoomJoinID", 0, 8), Pin(7, "RoomUpdate", 0, 7), Pin(6, "RoomJoinLINE", 0, 6), Pin(5, "RoomLINE", 0, 5), Pin(4, "RoomMakeLINE", 0, 4), Pin(3, "RoomJoin", 0, 3), Pin(2, "Room", 0, 2), Pin(0, "RoomMake", 0, 0), NodeType("Multi/MultiPlayAPI", 0x7fe5)]
    public class FlowNode_MultiPlayAPI : FlowNode_Network
    {
        private const int PIN_IN_VERSUS_CREATE_ROOM = 11;
        private const int PIN_IN_VERSUS_LINE_REQ = 15;
        private const int PIN_IN_VERSUS_LINE_MAKE = 0x10;
        private const int PIN_IN_VERSUS_LINE_JOIN = 0x11;
        private const int PIN_IN_VERSUS_STATUS = 0x12;
        private const int PIN_IN_MULTI_TOWER_ROOM_JOIN_LIST = 30;
        private const int PIN_IN_VERSUS_DRAFT_UNIT_LIST = 0x1f;
        private const int PIN_IN_VERSUS_DRAFT_UNIT_SELECTED = 0x20;
        private const int PIN_IN_VERSUS_DRAFT_PARTY_SELECTED = 0x21;
        private const int PIN_IN_RANKMATCH_START = 0x48;
        private const int PIN_IN_RANKMATCH_STATUS = 70;
        private const int PIN_IN_RANKMATCH_CREATE_ROOM = 0x47;
        private const int PIN_IN_VERSUS_LOBBY = 80;
        public static float RoomMakeTime;
        public static ReqMPRoom.Response RoomList;
        public static readonly int ROOMID_VALIDATE_MIN;
        public static readonly int ROOMID_VALIDATE_MAX;
        private readonly int MULTI_TOWER_UNIT_MAX;
        [CompilerGenerated]
        private EAPI <API>k__BackingField;

        static FlowNode_MultiPlayAPI()
        {
            ROOMID_VALIDATE_MAX = 0x1869f;
            return;
        }

        public FlowNode_MultiPlayAPI()
        {
            this.MULTI_TOWER_UNIT_MAX = 6;
            base..ctor();
            return;
        }

        private void Failure()
        {
            DebugUtility.Log("MultiPlayAPI failure");
            if ((this == null) == null)
            {
                goto Label_0017;
            }
            return;
        Label_0017:
            base.set_enabled(0);
            base.ActivateOutputLinks(0x65);
            return;
        }

        [DebuggerHidden]
        private IEnumerator GetPhotonRoomList(string fuid)
        {
            <GetPhotonRoomList>c__IteratorBC rbc;
            rbc = new <GetPhotonRoomList>c__IteratorBC();
            rbc.fuid = fuid;
            rbc.<$>fuid = fuid;
            rbc.<>f__this = this;
            return rbc;
        }

        [DebuggerHidden]
        private IEnumerator GetPhotonRoomName()
        {
            <GetPhotonRoomName>c__IteratorBD rbd;
            rbd = new <GetPhotonRoomName>c__IteratorBD();
            rbd.<>f__this = this;
            return rbd;
        }

        private bool IsMultiAreaQuest()
        {
            bool flag;
            GameManager manager;
            QuestParam param;
            flag = 0;
            if (string.IsNullOrEmpty(GlobalVars.SelectedQuestID) != null)
            {
                goto Label_0030;
            }
            param = MonoSingleton<GameManager>.Instance.FindQuest(GlobalVars.SelectedQuestID);
            if (param == null)
            {
                goto Label_0030;
            }
            flag = param.IsMultiAreaQuest;
        Label_0030:
            return flag;
        }

        public override unsafe void OnActivate(int pinID)
        {
            object[] objArray2;
            object[] objArray1;
            string str;
            bool flag;
            string str2;
            int num;
            bool flag2;
            bool flag3;
            int num2;
            bool flag4;
            int num3;
            TimeSpan span;
            Vector2 vector;
            string str3;
            string str4;
            int num4;
            GameManager manager;
            Vector2 vector2;
            List<string> list;
            bool flag5;
            int num5;
            int num6;
            bool flag6;
            string str5;
            int num7;
            string str6;
            GameManager manager2;
            GameManager manager3;
            MyPhoton photon;
            List<MyPhoton.MyPlayer> list2;
            int num8;
            MyPhoton.MyRoom room;
            JSON_MyPhotonRoomParam param;
            List<MultiTowerFloorParam> list3;
            MyPhoton.MyPlayer player;
            JSON_MyPhotonPlayerParam param2;
            MyPhoton photon2;
            GameManager manager4;
            GameManager manager5;
            Guid guid;
            <OnActivate>c__AnonStorey26C storeyc;
            if (((pinID != null) && (pinID != 4)) && (pinID != 0x16))
            {
                goto Label_0318;
            }
            this.ResetPlacementParam();
            str = string.Empty;
            if (PlayerPrefsUtility.HasKey(PlayerPrefsUtility.ROOM_COMMENT_KEY) == null)
            {
                goto Label_0045;
            }
            str = PlayerPrefsUtility.GetString(PlayerPrefsUtility.ROOM_COMMENT_KEY, string.Empty);
            goto Label_0050;
        Label_0045:
            str = LocalizedText.Get("sys.DEFAULT_ROOM_COMMENT");
        Label_0050:
            if (string.IsNullOrEmpty(str) == null)
            {
                goto Label_0066;
            }
            str = LocalizedText.Get("sys.DEFAULT_ROOM_COMMENT");
        Label_0066:
            if (MyMsgInput.isLegal(str) != null)
            {
                goto Label_0088;
            }
            DebugUtility.Log("comment is not legal");
            base.ActivateOutputLinks(0x12c1);
            return;
        Label_0088:
            GlobalVars.ResumeMultiplayPlayerID = 0;
            GlobalVars.ResumeMultiplaySeatID = 0;
            GlobalVars.SelectedMultiPlayRoomComment = str;
            DebugUtility.Log("MakeRoom Comment:" + GlobalVars.SelectedMultiPlayRoomComment);
            flag = 0;
            if (pinID != 4)
            {
                goto Label_0126;
            }
            GlobalVars.SelectedQuestID = FlowNode_OnUrlSchemeLaunch.LINEParam_decided.iname;
            GlobalVars.SelectedMultiPlayRoomType = FlowNode_OnUrlSchemeLaunch.LINEParam_decided.type;
            GlobalVars.EditMultiPlayRoomPassCode = "0";
            flag = 1;
            objArray1 = new object[] { "MakeRoom for LINE Quest:", GlobalVars.SelectedQuestID, " Type:", (JSON_MyPhotonRoomParam.EType) GlobalVars.SelectedMultiPlayRoomType, " PassCodeHash:", GlobalVars.SelectedMultiPlayRoomPassCodeHash };
            DebugUtility.Log(string.Concat(objArray1));
        Label_0126:
            GlobalVars.EditMultiPlayRoomPassCode = "0";
            str2 = FlowNode_Variable.Get("MultiPlayPasscode");
            if (string.IsNullOrEmpty(str2) != null)
            {
                goto Label_0161;
            }
            num = 0;
            if (int.TryParse(str2, &num) == null)
            {
                goto Label_0161;
            }
            GlobalVars.EditMultiPlayRoomPassCode = &num.ToString();
        Label_0161:
            flag2 = flag;
            RoomMakeTime = Time.get_realtimeSinceStartup();
            flag3 = GlobalVars.SelectedMultiPlayLimit & (GlobalVars.EditMultiPlayRoomPassCode == "0");
            num2 = ((flag2 == null) && (flag3 != null)) ? GlobalVars.MultiPlayJoinUnitLv : 0;
            flag4 = ((flag2 == null) && (flag3 != null)) ? GlobalVars.MultiPlayClearOnly : 0;
            num3 = GlobalVars.SelectedMultiTowerFloor;
            if (Network.Mode == null)
            {
                goto Label_0262;
            }
            span = DateTime.Now - new DateTime(0x7b2, 1, 1, 0, 0, 0, 0);
            GlobalVars.SelectedMultiPlayRoomID = (int) &span.TotalSeconds;
            GlobalVars.SelectedMultiPlayPhotonAppID = string.Empty;
            GlobalVars.SelectedMultiPlayRoomName = &Guid.NewGuid().ToString();
            objArray2 = new object[] { "MakeRoom RoomID:", (int) GlobalVars.SelectedMultiPlayRoomID, " AppID:", GlobalVars.SelectedMultiPlayPhotonAppID, " Name:", GlobalVars.SelectedMultiPlayRoomName };
            DebugUtility.Log(string.Concat(objArray2));
            this.Success();
            return;
        Label_0262:
            MultiInvitationSendWindow.ClearInvited();
            base.set_enabled(1);
            this.API = 0;
            if (this.IsMultiAreaQuest() == null)
            {
                goto Label_02B8;
            }
            vector = GlobalVars.Location;
            base.ExecRequest(new ReqMultiAreaRoomMake(GlobalVars.SelectedQuestID, str, GlobalVars.EditMultiPlayRoomPassCode, flag2, flag3, num2, flag4, vector, new Network.ResponseCallback(this.ResponseCallback)));
            goto Label_0313;
        Label_02B8:
            if (pinID == 0x16)
            {
                goto Label_02EF;
            }
            base.ExecRequest(new ReqMPRoomMake(GlobalVars.SelectedQuestID, str, GlobalVars.EditMultiPlayRoomPassCode, flag2, flag3, num2, flag4, new Network.ResponseCallback(this.ResponseCallback)));
            goto Label_0313;
        Label_02EF:
            base.ExecRequest(new ReqMultiTwRoomMake(GlobalVars.SelectedMultiTowerID, str, GlobalVars.EditMultiPlayRoomPassCode, num3, new Network.ResponseCallback(this.ResponseCallback)));
        Label_0313:
            goto Label_12C8;
        Label_0318:
            if (((pinID != 2) && (pinID != 5)) && (((pinID != 0x17) && (pinID != 50)) && (pinID != 60)))
            {
                goto Label_0482;
            }
            str3 = null;
            if (pinID != 5)
            {
                goto Label_0359;
            }
            str3 = FlowNode_OnUrlSchemeLaunch.LINEParam_decided.creatorFUID;
            goto Label_0368;
        Label_0359:
            if (pinID != 50)
            {
                goto Label_0368;
            }
            str3 = GlobalVars.MultiInvitationRoomOwner;
        Label_0368:
            DebugUtility.Log("ListRoom FUID:" + str3);
            if (Network.Mode == null)
            {
                goto Label_0393;
            }
            base.StartCoroutine(this.GetPhotonRoomList(str3));
            return;
        Label_0393:
            RoomList = null;
            str4 = string.Empty;
            if (pinID != 2)
            {
                goto Label_03AE;
            }
            str4 = GlobalVars.SelectedQuestID;
        Label_03AE:
            num4 = GlobalVars.SelectedMultiTowerFloor;
            base.set_enabled(1);
            this.API = 1;
            if ((pinID != 60) && (this.IsMultiAreaQuest() == null))
            {
                goto Label_0435;
            }
            manager = MonoSingleton<GameManager>.Instance;
            vector2 = GlobalVars.Location;
            list = new List<string>();
            if (string.IsNullOrEmpty(str4) != null)
            {
                goto Label_0405;
            }
            list.Add(str4);
            goto Label_040E;
        Label_0405:
            list = manager.GetMultiAreaQuestList();
        Label_040E:
            base.ExecRequest(new ReqMultiAreaRoom(str3, list.ToArray(), vector2, new Network.ResponseCallback(this.ResponseCallback)));
            goto Label_047D;
        Label_0435:
            if (pinID == 0x17)
            {
                goto Label_045D;
            }
            base.ExecRequest(new ReqMPRoom(str3, str4, new Network.ResponseCallback(this.ResponseCallback)));
            goto Label_047D;
        Label_045D:
            base.ExecRequest(new ReqMultiTwRoom(str3, GlobalVars.SelectedMultiTowerID, num4, new Network.ResponseCallback(this.ResponseCallback)));
        Label_047D:
            goto Label_12C8;
        Label_0482:
            if (((pinID != 3) && (pinID != 6)) && ((pinID != 0x18) && (pinID != 0x33)))
            {
                goto Label_06E9;
            }
            if (((RoomList != null) && (RoomList.rooms != null)) && (((int) RoomList.rooms.Length) > 0))
            {
                goto Label_04D2;
            }
            this.Failure();
            return;
        Label_04D2:
            this.ResetPlacementParam();
            flag5 = 0;
            if (pinID != 6)
            {
                goto Label_0540;
            }
            if ((((int) RoomList.rooms.Length) == 1) && (RoomList.rooms[0] != null))
            {
                goto Label_050C;
            }
            this.Failure();
            return;
        Label_050C:
            GlobalVars.SelectedMultiPlayRoomID = RoomList.rooms[0].roomid;
            DebugUtility.Log("JoinRoom for LINE RoomID:" + ((int) GlobalVars.SelectedMultiPlayRoomID));
            goto Label_05A8;
        Label_0540:
            if (pinID != 0x33)
            {
                goto Label_05A8;
            }
            if ((((int) RoomList.rooms.Length) == 1) && (RoomList.rooms[0] != null))
            {
                goto Label_0572;
            }
            this.Failure();
            return;
        Label_0572:
            flag5 = GlobalVars.MultiInvitationRoomLocked;
            GlobalVars.SelectedMultiPlayRoomID = RoomList.rooms[0].roomid;
            DebugUtility.Log("JoinRoom for Invitation RoomID:" + ((int) GlobalVars.SelectedMultiPlayRoomID));
        Label_05A8:
            GlobalVars.ResumeMultiplayPlayerID = 0;
            GlobalVars.ResumeMultiplaySeatID = 0;
            GlobalVars.SelectedQuestID = null;
            num5 = 0;
            goto Label_0642;
        Label_05C2:
            if (RoomList.rooms[num5].quest == null)
            {
                goto Label_063C;
            }
            if (string.IsNullOrEmpty(RoomList.rooms[num5].quest.iname) == null)
            {
                goto Label_05FF;
            }
            goto Label_063C;
        Label_05FF:
            if (RoomList.rooms[num5].roomid != GlobalVars.SelectedMultiPlayRoomID)
            {
                goto Label_063C;
            }
            GlobalVars.SelectedQuestID = RoomList.rooms[num5].quest.iname;
            goto Label_0655;
        Label_063C:
            num5 += 1;
        Label_0642:
            if (num5 < ((int) RoomList.rooms.Length))
            {
                goto Label_05C2;
            }
        Label_0655:
            if (string.IsNullOrEmpty(GlobalVars.SelectedQuestID) == null)
            {
                goto Label_066B;
            }
            this.Failure();
            return;
        Label_066B:
            if (Network.Mode == null)
            {
                goto Label_0683;
            }
            base.StartCoroutine(this.GetPhotonRoomName());
            return;
        Label_0683:
            num6 = GlobalVars.SelectedMultiTowerFloor;
            base.set_enabled(1);
            this.API = 2;
            if (pinID == 0x18)
            {
                goto Label_06C3;
            }
            base.ExecRequest(new ReqMPRoomJoin(GlobalVars.SelectedMultiPlayRoomID, new Network.ResponseCallback(this.ResponseCallback), flag5));
            goto Label_06E4;
        Label_06C3:
            base.ExecRequest(new ReqMultiTwRoomJoin(GlobalVars.SelectedMultiPlayRoomID, new Network.ResponseCallback(this.ResponseCallback), flag5, num6, 0));
        Label_06E4:
            goto Label_12C8;
        Label_06E9:
            if (((pinID != 7) && (pinID != 0x1a)) && ((pinID != 0x37) && (pinID != 0x38)))
            {
                goto Label_0833;
            }
            if (Network.Mode == null)
            {
                goto Label_0719;
            }
            this.Success();
            return;
        Label_0719:
            if ((string.IsNullOrEmpty(GlobalVars.EditMultiPlayRoomComment) != null) || (MyMsgInput.isLegal(GlobalVars.EditMultiPlayRoomComment) != null))
            {
                goto Label_074E;
            }
            DebugUtility.Log("comment is not legal");
            base.ActivateOutputLinks(0x12c1);
            return;
        Label_074E:
            if ((pinID != 0x1a) || (string.IsNullOrEmpty(GlobalVars.EditMultiPlayRoomComment) == null))
            {
                goto Label_076F;
            }
            GlobalVars.EditMultiPlayRoomComment = GlobalVars.SelectedMultiPlayRoomComment;
        Label_076F:
            if (pinID != 0x37)
            {
                goto Label_0790;
            }
            GlobalVars.EditMultiPlayRoomPassCode = "0";
            GlobalVars.EditMultiPlayRoomComment = GlobalVars.SelectedMultiPlayRoomComment;
            goto Label_07AC;
        Label_0790:
            if (pinID != 0x38)
            {
                goto Label_07AC;
            }
            GlobalVars.EditMultiPlayRoomPassCode = "1";
            GlobalVars.EditMultiPlayRoomComment = GlobalVars.SelectedMultiPlayRoomComment;
        Label_07AC:
            base.set_enabled(1);
            this.API = 3;
            if ((GlobalVars.SelectedMultiPlayRoomType == 2) != null)
            {
                goto Label_07F6;
            }
            base.ExecRequest(new ReqMPRoomUpdate(GlobalVars.SelectedMultiPlayRoomID, GlobalVars.EditMultiPlayRoomComment, GlobalVars.EditMultiPlayRoomPassCode, new Network.ResponseCallback(this.ResponseCallback)));
            goto Label_082E;
        Label_07F6:
            str5 = GlobalVars.SelectedMultiTowerID;
            num7 = GlobalVars.SelectedMultiTowerFloor;
            base.ExecRequest(new ReqMultiTwRoomUpdate(GlobalVars.SelectedMultiPlayRoomID, GlobalVars.EditMultiPlayRoomComment, GlobalVars.EditMultiPlayRoomPassCode, str5, num7, new Network.ResponseCallback(this.ResponseCallback)));
        Label_082E:
            goto Label_12C8;
        Label_0833:
            if (((pinID != 8) && (pinID != 0x19)) && ((pinID != 0x1c) && (pinID != 30)))
            {
                goto Label_0946;
            }
            if (Network.Mode == null)
            {
                goto Label_0863;
            }
            this.Failure();
            return;
        Label_0863:
            if ((GlobalVars.SelectedMultiPlayRoomID >= ROOMID_VALIDATE_MIN) && (GlobalVars.SelectedMultiPlayRoomID <= ROOMID_VALIDATE_MAX))
            {
                goto Label_0888;
            }
            this.Failure();
            return;
        Label_0888:
            this.ResetPlacementParam();
            GlobalVars.ResumeMultiplayPlayerID = 0;
            GlobalVars.ResumeMultiplaySeatID = 0;
            base.set_enabled(1);
            this.API = 2;
            if (((pinID == 0x19) || (pinID == 0x1c)) || (pinID == 30))
            {
                goto Label_08E2;
            }
            base.ExecRequest(new ReqMPRoomJoin(GlobalVars.SelectedMultiPlayRoomID, new Network.ResponseCallback(this.ResponseCallback), 1));
            goto Label_0941;
        Label_08E2:
            if (pinID != 30)
            {
                goto Label_0916;
            }
            this.API = 14;
            base.ExecRequest(new ReqMultiTwRoomJoin(GlobalVars.SelectedMultiPlayRoomID, new Network.ResponseCallback(this.ResponseCallback), 0, 0, 0));
            goto Label_0941;
        Label_0916:
            this.API = 14;
            base.ExecRequest(new ReqMultiTwRoomJoin(GlobalVars.SelectedMultiPlayRoomID, new Network.ResponseCallback(this.ResponseCallback), 1, 0, pinID == 0x1c));
        Label_0941:
            goto Label_12C8;
        Label_0946:
            if (pinID != 9)
            {
                goto Label_0989;
            }
            if (Network.Mode == null)
            {
                goto Label_095F;
            }
            this.Success();
            return;
        Label_095F:
            base.set_enabled(1);
            this.API = 4;
            base.ExecRequest(new ReqMPVersion(new Network.ResponseCallback(this.ResponseCallback)));
            goto Label_12C8;
        Label_0989:
            if (pinID != 10)
            {
                goto Label_09FC;
            }
            if (Network.Mode == null)
            {
                goto Label_09A2;
            }
            this.Success();
            return;
        Label_09A2:
            GlobalVars.SelectedMultiPlayRoomName = string.Empty;
            GlobalVars.VersusRoomReuse = 0;
            GlobalVars.ResumeMultiplayPlayerID = 0;
            GlobalVars.ResumeMultiplaySeatID = 0;
            GlobalVars.MultiPlayVersusKey = MonoSingleton<GameManager>.Instance.GetVersusKey(GlobalVars.SelectedMultiPlayVersusType);
            base.set_enabled(1);
            this.API = 5;
            base.ExecRequest(new ReqVersusStart(new Network.ResponseCallback(this.ResponseCallback)));
            goto Label_12C8;
        Label_09FC:
            if (pinID != 11)
            {
                goto Label_0B03;
            }
            if (Network.Mode == null)
            {
                goto Label_0A15;
            }
            this.Success();
            return;
        Label_0A15:
            str6 = string.Empty;
            if (PlayerPrefsUtility.HasKey(PlayerPrefsUtility.ROOM_COMMENT_KEY) == null)
            {
                goto Label_0A41;
            }
            str6 = PlayerPrefsUtility.GetString(PlayerPrefsUtility.ROOM_COMMENT_KEY, string.Empty);
            goto Label_0A4D;
        Label_0A41:
            str6 = LocalizedText.Get("sys.DEFAULT_ROOM_COMMENT");
        Label_0A4D:
            if (string.IsNullOrEmpty(str6) == null)
            {
                goto Label_0A65;
            }
            str6 = LocalizedText.Get("sys.DEFAULT_ROOM_COMMENT");
        Label_0A65:
            if (MyMsgInput.isLegal(str6) != null)
            {
                goto Label_0A88;
            }
            DebugUtility.Log("comment is not legal");
            base.ActivateOutputLinks(0x12c1);
            return;
        Label_0A88:
            if (((GlobalVars.SelectedMultiPlayVersusType != 2) || (MonoSingleton<GameManager>.Instance.VSDraftType == null)) || (GlobalVars.IsVersusDraftMode == null))
            {
                goto Label_0ABB;
            }
            GlobalVars.SelectedQuestID = MonoSingleton<GameManager>.Instance.VSDraftQuestId;
        Label_0ABB:
            RoomMakeTime = Time.get_realtimeSinceStartup();
            GlobalVars.SelectedMultiPlayRoomComment = str6;
            base.set_enabled(1);
            this.API = 6;
            base.ExecRequest(new ReqVersusMake(GlobalVars.SelectedMultiPlayVersusType, str6, GlobalVars.SelectedQuestID, 0, new Network.ResponseCallback(this.ResponseCallback)));
            goto Label_12C8;
        Label_0B03:
            if (pinID != 12)
            {
                goto Label_0B70;
            }
            if (Network.Mode == null)
            {
                goto Label_0B1C;
            }
            this.Failure();
            return;
        Label_0B1C:
            if ((GlobalVars.SelectedMultiPlayRoomID >= ROOMID_VALIDATE_MIN) && (GlobalVars.SelectedMultiPlayRoomID <= ROOMID_VALIDATE_MAX))
            {
                goto Label_0B41;
            }
            this.Failure();
            return;
        Label_0B41:
            base.set_enabled(1);
            this.API = 7;
            base.ExecRequest(new ReqVersusRoomJoin(GlobalVars.SelectedMultiPlayRoomID, new Network.ResponseCallback(this.ResponseCallback)));
            goto Label_12C8;
        Label_0B70:
            if (pinID != 13)
            {
                goto Label_0BE8;
            }
            if (Network.Mode == null)
            {
                goto Label_0B89;
            }
            this.Success();
            return;
        Label_0B89:
            if (MyMsgInput.isLegal(GlobalVars.EditMultiPlayRoomComment) != null)
            {
                goto Label_0BAF;
            }
            DebugUtility.Log("comment is not legal");
            base.ActivateOutputLinks(0x12c1);
            return;
        Label_0BAF:
            base.set_enabled(1);
            this.API = 3;
            base.ExecRequest(new ReqVersusRoomUpdate(GlobalVars.SelectedMultiPlayRoomID, GlobalVars.EditMultiPlayRoomComment, GlobalVars.SelectedQuestID, new Network.ResponseCallback(this.ResponseCallback)));
            goto Label_12C8;
        Label_0BE8:
            if (pinID != 14)
            {
                goto Label_0C2B;
            }
            GlobalVars.SelectedMultiPlayRoomName = string.Empty;
            GlobalVars.VersusRoomReuse = 0;
            GlobalVars.ResumeMultiplayPlayerID = 0;
            GlobalVars.ResumeMultiplaySeatID = 0;
            GlobalVars.MultiPlayVersusKey = MonoSingleton<GameManager>.Instance.GetVersusKey(GlobalVars.SelectedMultiPlayVersusType);
            this.Success();
            goto Label_12C8;
        Label_0C2B:
            if (pinID != 15)
            {
                goto Label_0C73;
            }
            if (Network.Mode == null)
            {
                goto Label_0C44;
            }
            this.Success();
            return;
        Label_0C44:
            base.set_enabled(1);
            this.API = 8;
            base.ExecRequest(new ReqVersusLine(GlobalVars.SelectedMultiPlayRoomName, new Network.ResponseCallback(this.ResponseCallback)));
            goto Label_12C8;
        Label_0C73:
            if (pinID != 0x10)
            {
                goto Label_0CCC;
            }
            if (Network.Mode == null)
            {
                goto Label_0C8C;
            }
            this.Success();
            return;
        Label_0C8C:
            GlobalVars.VersusRoomReuse = 0;
            RoomMakeTime = Time.get_realtimeSinceStartup();
            base.set_enabled(1);
            this.API = 9;
            base.ExecRequest(new ReqVersusLineMake(GlobalVars.SelectedMultiPlayRoomName, new Network.ResponseCallback(this.ResponseCallback)));
            goto Label_12C8;
        Label_0CCC:
            if (pinID != 0x11)
            {
                goto Label_0D48;
            }
            if (Network.Mode == null)
            {
                goto Label_0CE5;
            }
            this.Failure();
            return;
        Label_0CE5:
            GlobalVars.SelectedMultiPlayRoomID = FlowNode_OnUrlSchemeLaunch.LINEParam_decided.roomid;
            if ((GlobalVars.SelectedMultiPlayRoomID >= ROOMID_VALIDATE_MIN) && (GlobalVars.SelectedMultiPlayRoomID <= ROOMID_VALIDATE_MAX))
            {
                goto Label_0D19;
            }
            this.Failure();
            return;
        Label_0D19:
            base.set_enabled(1);
            this.API = 7;
            base.ExecRequest(new ReqVersusRoomJoin(GlobalVars.SelectedMultiPlayRoomID, new Network.ResponseCallback(this.ResponseCallback)));
            goto Label_12C8;
        Label_0D48:
            if (pinID != 0x12)
            {
                goto Label_0D9B;
            }
            if (Network.Mode == null)
            {
                goto Label_0D61;
            }
            this.Failure();
            return;
        Label_0D61:
            MonoSingleton<GameManager>.Instance.IsVSCpuBattle = 0;
            base.set_enabled(1);
            this.API = 10;
            base.ExecRequest(new ReqVersusStatus(new Network.ResponseCallback(this.ResponseCallback)));
            goto Label_12C8;
        Label_0D9B:
            if (pinID != 0x13)
            {
                goto Label_0DDF;
            }
            if (Network.Mode == null)
            {
                goto Label_0DB4;
            }
            this.Failure();
            return;
        Label_0DB4:
            base.set_enabled(1);
            this.API = 11;
            base.ExecRequest(new ReqVersusSeason(new Network.ResponseCallback(this.ResponseCallback)));
            goto Label_12C8;
        Label_0DDF:
            if (pinID != 20)
            {
                goto Label_0E23;
            }
            if (Network.Mode == null)
            {
                goto Label_0DF8;
            }
            this.Failure();
            return;
        Label_0DF8:
            base.set_enabled(1);
            this.API = 12;
            base.ExecRequest(new ReqVersusFriendScore(new Network.ResponseCallback(this.ResponseCallback)));
            goto Label_12C8;
        Label_0E23:
            if (pinID != 0x15)
            {
                goto Label_1051;
            }
            manager3 = MonoSingleton<GameManager>.Instance;
            photon = PunMonoSingleton<MyPhoton>.Instance;
            if (((photon != null) == null) || (photon.IsConnectedInRoom() == null))
            {
                goto Label_1046;
            }
            list2 = photon.GetRoomPlayerList();
            if (list2.Count <= 1)
            {
                goto Label_0F02;
            }
            storeyc = new <OnActivate>c__AnonStorey26C();
            num8 = (photon.IsHost() == null) ? 0 : 1;
            storeyc.target_player = JSON_MyPhotonPlayerParam.Parse(list2[num8].json);
            if (storeyc.target_player == null)
            {
                goto Label_0F02;
            }
            if (GlobalVars.BlockList == null)
            {
                goto Label_0F02;
            }
            if (GlobalVars.BlockList.Count <= 0)
            {
                goto Label_0F02;
            }
            if (GlobalVars.BlockList.FindIndex(new Predicate<string>(storeyc.<>m__1A7)) == -1)
            {
                goto Label_0F02;
            }
            if ((this == null) == null)
            {
                goto Label_0EEE;
            }
            return;
        Label_0EEE:
            base.set_enabled(0);
            base.ActivateOutputLinks(0x1326);
            return;
        Label_0F02:
            GlobalVars.CreateAutoMultiTower = 1;
            if (photon.IsCreatedRoom() == null)
            {
                goto Label_0FC8;
            }
            photon.OpenRoom(1, 0);
            room = photon.GetCurrentRoom();
            if (room == null)
            {
                goto Label_0FC8;
            }
            param = JSON_MyPhotonRoomParam.Parse(room.json);
            if (param == null)
            {
                goto Label_0FC8;
            }
            list3 = manager3.GetMTAllFloorParam(GlobalVars.SelectedMultiTowerID);
            if (list3 == null)
            {
                goto Label_0F70;
            }
            GlobalVars.SelectedMultiTowerFloor = Mathf.Min(list3.Count, GlobalVars.SelectedMultiTowerFloor + 1);
        Label_0F70:
            param.challegedMTFloor = GlobalVars.SelectedMultiTowerFloor;
            param.iname = GlobalVars.SelectedMultiTowerID + "_" + &param.challegedMTFloor.ToString();
            photon.SetRoomParam(param.Serialize());
            if (MultiPlayAPIRoom.IsLocked(param.passCode) == null)
            {
                goto Label_0FC8;
            }
            GlobalVars.EditMultiPlayRoomPassCode = "1";
        Label_0FC8:
            photon.AddMyPlayerParam("BattleStart", (bool) 0);
            photon.AddMyPlayerParam("resume", (bool) 0);
            player = photon.GetMyPlayer();
            if (player == null)
            {
                goto Label_103B;
            }
            param2 = JSON_MyPhotonPlayerParam.Parse(player.json);
            if (param2 == null)
            {
                goto Label_103B;
            }
            param2.mtChallengeFloor = manager3.GetMTChallengeFloor();
            param2.mtClearedFloor = manager3.GetMTClearedMaxFloor();
            photon.SetMyPlayerParam(param2.Serialize());
        Label_103B:
            this.Success();
            goto Label_104C;
        Label_1046:
            this.Failure();
        Label_104C:
            goto Label_12C8;
        Label_1051:
            if (pinID != 0x1b)
            {
                goto Label_109A;
            }
            if (Network.Mode == null)
            {
                goto Label_106A;
            }
            this.Failure();
            return;
        Label_106A:
            base.set_enabled(1);
            this.API = 13;
            base.ExecRequest(new ReqMultiTwStatus(GlobalVars.SelectedMultiTowerID, new Network.ResponseCallback(this.ResponseCallback)));
            goto Label_12C8;
        Label_109A:
            if (pinID != 0x1d)
            {
                goto Label_10D8;
            }
            photon2 = PunMonoSingleton<MyPhoton>.Instance;
            if ((photon2 != null) == null)
            {
                goto Label_10CD;
            }
            if (photon2.IsConnectedInRoom() == null)
            {
                goto Label_10CD;
            }
            this.Success();
            goto Label_10D3;
        Label_10CD:
            this.Failure();
        Label_10D3:
            goto Label_12C8;
        Label_10D8:
            if (pinID != 0x1f)
            {
                goto Label_1121;
            }
            if (Network.Mode == null)
            {
                goto Label_10F1;
            }
            this.Failure();
            return;
        Label_10F1:
            base.set_enabled(1);
            this.API = 0x11;
            base.ExecRequest(new ReqVersusDraft(GlobalVars.SelectedMultiPlayRoomName, new Network.ResponseCallback(this.ResponseCallback)));
            goto Label_12C8;
        Label_1121:
            if (pinID != 0x20)
            {
                goto Label_1159;
            }
            base.set_enabled(1);
            this.API = 0x12;
            base.ExecRequest(new ReqVersusDraftSelect(GlobalVars.SelectedMultiPlayRoomName, new Network.ResponseCallback(this.ResponseCallback)));
            goto Label_12C8;
        Label_1159:
            if (pinID != 0x21)
            {
                goto Label_1196;
            }
            base.set_enabled(1);
            this.API = 0x13;
            base.ExecRequest(new ReqVersusDraftParty(GlobalVars.SelectedMultiPlayRoomName, VersusDraftList.DraftID, new Network.ResponseCallback(this.ResponseCallback)));
            goto Label_12C8;
        Label_1196:
            if (pinID != 0x48)
            {
                goto Label_11E9;
            }
            if (Network.Mode == null)
            {
                goto Label_11AF;
            }
            this.Failure();
            return;
        Label_11AF:
            MonoSingleton<GameManager>.Instance.IsVSCpuBattle = 0;
            base.set_enabled(1);
            this.API = 15;
            base.ExecRequest(new ReqRankMatchStart(new Network.ResponseCallback(this.ResponseCallback)));
            goto Label_12C8;
        Label_11E9:
            if (pinID != 70)
            {
                goto Label_123C;
            }
            if (Network.Mode == null)
            {
                goto Label_1202;
            }
            this.Failure();
            return;
        Label_1202:
            MonoSingleton<GameManager>.Instance.IsVSCpuBattle = 0;
            base.set_enabled(1);
            this.API = 0x10;
            base.ExecRequest(new ReqRankMatchStatus(new Network.ResponseCallback(this.ResponseCallback)));
            goto Label_12C8;
        Label_123C:
            if (pinID != 0x47)
            {
                goto Label_1289;
            }
            if (Network.Mode == null)
            {
                goto Label_1255;
            }
            this.Success();
            return;
        Label_1255:
            RoomMakeTime = Time.get_realtimeSinceStartup();
            base.set_enabled(1);
            this.API = 6;
            base.ExecRequest(new ReqRankMatchMake(new Network.ResponseCallback(this.ResponseCallback)));
            goto Label_12C8;
        Label_1289:
            if (pinID != 80)
            {
                goto Label_12C8;
            }
            if (Network.Mode == null)
            {
                goto Label_12A2;
            }
            this.Failure();
            return;
        Label_12A2:
            base.set_enabled(1);
            this.API = 20;
            base.ExecRequest(new ReqVersusLobby(new Network.ResponseCallback(this.ResponseCallback)));
        Label_12C8:
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            object[] objArray1;
            string str;
            WebAPI.JSON_BodyResponse<ReqMPRoomMake.Response> response;
            WebAPI.JSON_BodyResponse<ReqMPRoom.Response> response2;
            WebAPI.JSON_BodyResponse<ReqMPRoomJoin.Response> response3;
            WebAPI.JSON_BodyResponse<ReqMPVersion.Response> response4;
            WebAPI.JSON_BodyResponse<ReqVersusStart.Response> response5;
            GameManager manager;
            WebAPI.JSON_BodyResponse<ReqVersusMake.Response> response6;
            WebAPI.JSON_BodyResponse<ReqVersusRoomJoin.Response> response7;
            WebAPI.JSON_BodyResponse<ReqVersusStatus.Response> response8;
            GameManager manager2;
            PlayerData data;
            int num;
            ReqVersusStatus.StreakStatus status;
            STREAK_JUDGE streak_judge;
            WebAPI.JSON_BodyResponse<ReqVersusSeason.Response> response9;
            GameManager manager3;
            PlayerData data2;
            WebAPI.JSON_BodyResponse<ReqVersusFriendScore.Response> response10;
            GameManager manager4;
            WebAPI.JSON_BodyResponse<ReqMultiTwStatus.Response> response11;
            GameManager manager5;
            WebAPI.JSON_BodyResponse<ReqMultiTwRoomJoin.Response> response12;
            GameManager manager6;
            MultiTowerFloorParam param;
            QuestParam param2;
            WebAPI.JSON_BodyResponse<ReqRankMatchStart.Response> response13;
            GameManager manager7;
            PlayerData data3;
            int num2;
            long num3;
            long num4;
            WebAPI.JSON_BodyResponse<ReqRankMatchStatus.Response> response14;
            GameManager manager8;
            long num5;
            long num6;
            GameManager manager9;
            List<VersusDraftUnitParam> list;
            VersusDraftUnitParam param3;
            WebAPI.JSON_BodyResponse<ReqVersusDraftSelect.Response> response15;
            WebAPI.JSON_BodyResponse<ReqVersusLobby.Response> response16;
            GameManager manager10;
            long num7;
            long num8;
            VERSUS_TYPE versus_type;
            STREAK_JUDGE streak_judge2;
            long num9;
            <OnSuccess>c__AnonStorey26D storeyd;
            <OnSuccess>c__AnonStorey26E storeye;
            DebugUtility.Log("OnSuccess");
            if (Network.IsError == null)
            {
                goto Label_02FD;
            }
            if (Network.ErrCode != 0x12c0)
            {
                goto Label_004E;
            }
            Network.RemoveAPI();
            Network.ResetError();
            if ((this == null) == null)
            {
                goto Label_003A;
            }
            return;
        Label_003A:
            base.set_enabled(0);
            base.ActivateOutputLinks(0x12c0);
            return;
        Label_004E:
            if (Network.ErrCode == 0x12c1)
            {
                goto Label_006C;
            }
            if (Network.ErrCode != 0x2719)
            {
                goto Label_00BA;
            }
        Label_006C:
            if (this.API != null)
            {
                goto Label_008F;
            }
            str = LocalizedText.Get("sys.DEFAULT_ROOM_COMMENT");
            PlayerPrefsUtility.SetString(PlayerPrefsUtility.ROOM_COMMENT_KEY, str, 0);
        Label_008F:
            Network.RemoveAPI();
            Network.ResetError();
            if ((this == null) == null)
            {
                goto Label_00A6;
            }
            return;
        Label_00A6:
            base.set_enabled(0);
            base.ActivateOutputLinks(0x12c1);
            return;
        Label_00BA:
            if (Network.ErrCode != 0xce5)
            {
                goto Label_00F4;
            }
            Network.RemoveAPI();
            Network.ResetError();
            if ((this == null) == null)
            {
                goto Label_00E0;
            }
            return;
        Label_00E0:
            base.set_enabled(0);
            base.ActivateOutputLinks(0x12c2);
            return;
        Label_00F4:
            if (Network.ErrCode == 0x1324)
            {
                goto Label_0112;
            }
            if (Network.ErrCode != 0x271c)
            {
                goto Label_013D;
            }
        Label_0112:
            Network.RemoveAPI();
            Network.ResetError();
            if ((this == null) == null)
            {
                goto Label_0129;
            }
            return;
        Label_0129:
            base.set_enabled(0);
            base.ActivateOutputLinks(0x1324);
            return;
        Label_013D:
            if (Network.ErrCode == 0xe78)
            {
                goto Label_015B;
            }
            if (Network.ErrCode != 0x2718)
            {
                goto Label_0186;
            }
        Label_015B:
            Network.RemoveAPI();
            Network.ResetError();
            if ((this == null) == null)
            {
                goto Label_0172;
            }
            return;
        Label_0172:
            base.set_enabled(0);
            base.ActivateOutputLinks(0x1388);
            return;
        Label_0186:
            if (Network.ErrCode == 0xca)
            {
                goto Label_01C2;
            }
            if (Network.ErrCode == 0xcb)
            {
                goto Label_01C2;
            }
            if (Network.ErrCode == 0xce)
            {
                goto Label_01C2;
            }
            if (Network.ErrCode != 0xcd)
            {
                goto Label_01E8;
            }
        Label_01C2:
            Network.RemoveAPI();
            if ((this == null) == null)
            {
                goto Label_01D4;
            }
            return;
        Label_01D4:
            base.set_enabled(0);
            base.ActivateOutputLinks(0x1770);
            return;
        Label_01E8:
            if (Network.ErrCode != 0x2713)
            {
                goto Label_021D;
            }
            Network.RemoveAPI();
            if ((this == null) == null)
            {
                goto Label_0209;
            }
            return;
        Label_0209:
            base.set_enabled(0);
            base.ActivateOutputLinks(0x1b58);
            return;
        Label_021D:
            if (Network.ErrCode != 0x2714)
            {
                goto Label_0252;
            }
            Network.RemoveAPI();
            if ((this == null) == null)
            {
                goto Label_023E;
            }
            return;
        Label_023E:
            base.set_enabled(0);
            base.ActivateOutputLinks(0x1f40);
            return;
        Label_0252:
            if (Network.ErrCode != 0x2717)
            {
                goto Label_0287;
            }
            Network.RemoveAPI();
            if ((this == null) == null)
            {
                goto Label_0273;
            }
            return;
        Label_0273:
            base.set_enabled(0);
            base.ActivateOutputLinks(0x2328);
            return;
        Label_0287:
            if (Network.ErrCode != 0x271e)
            {
                goto Label_02BC;
            }
            Network.RemoveAPI();
            if ((this == null) == null)
            {
                goto Label_02A8;
            }
            return;
        Label_02A8:
            base.set_enabled(0);
            base.ActivateOutputLinks(0x2710);
            return;
        Label_02BC:
            if (Network.ErrCode != 0x1326)
            {
                goto Label_02F6;
            }
            Network.RemoveAPI();
            Network.ResetError();
            if ((this == null) == null)
            {
                goto Label_02E2;
            }
            return;
        Label_02E2:
            base.set_enabled(0);
            base.ActivateOutputLinks(0x1326);
            return;
        Label_02F6:
            this.OnFailed();
            return;
        Label_02FD:
            if (this.API != null)
            {
                goto Label_03B2;
            }
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqMPRoomMake.Response>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            if (response.body != null)
            {
                goto Label_0338;
            }
            this.OnFailed();
            return;
        Label_0338:
            GlobalVars.SelectedMultiPlayRoomID = response.body.roomid;
            GlobalVars.SelectedMultiPlayPhotonAppID = response.body.app_id;
            GlobalVars.SelectedMultiPlayRoomName = response.body.token;
            objArray1 = new object[] { "MakeRoom RoomID:", (int) GlobalVars.SelectedMultiPlayRoomID, " AppID:", GlobalVars.SelectedMultiPlayPhotonAppID, " Name:", GlobalVars.SelectedMultiPlayRoomName };
            DebugUtility.Log(string.Concat(objArray1));
            goto Label_125E;
        Label_03B2:
            if (this.API != 1)
            {
                goto Label_0455;
            }
            response2 = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqMPRoom.Response>>(&www.text);
            DebugUtility.Assert((response2 == null) == 0, "res == null");
            if (response2.body != null)
            {
                goto Label_03EE;
            }
            this.OnFailed();
            return;
        Label_03EE:
            RoomList = response2.body;
            if (RoomList != null)
            {
                goto Label_0412;
            }
            DebugUtility.Log("ListRoom null");
            goto Label_0450;
        Label_0412:
            if (RoomList.rooms != null)
            {
                goto Label_0430;
            }
            DebugUtility.Log("ListRoom rooms null");
            goto Label_0450;
        Label_0430:
            DebugUtility.Log("ListRoom num:" + ((int) ((int) RoomList.rooms.Length)));
        Label_0450:
            goto Label_125E;
        Label_0455:
            if (this.API != 2)
            {
                goto Label_051A;
            }
            response3 = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqMPRoomJoin.Response>>(&www.text);
            DebugUtility.Assert((response3 == null) == 0, "res == null");
            if (response3.body != null)
            {
                goto Label_0491;
            }
            this.OnFailed();
            return;
        Label_0491:
            if (response3.body.quest == null)
            {
                goto Label_04BB;
            }
            if (string.IsNullOrEmpty(response3.body.quest.iname) == null)
            {
                goto Label_04C2;
            }
        Label_04BB:
            this.OnFailed();
            return;
        Label_04C2:
            GlobalVars.SelectedQuestID = response3.body.quest.iname;
            GlobalVars.SelectedMultiPlayPhotonAppID = response3.body.app_id;
            GlobalVars.SelectedMultiPlayRoomName = response3.body.token;
            DebugUtility.Log("JoinRoom  AppID:" + GlobalVars.SelectedMultiPlayPhotonAppID + " Name:" + GlobalVars.SelectedMultiPlayRoomName);
            goto Label_125E;
        Label_051A:
            if (this.API != 3)
            {
                goto Label_052B;
            }
            goto Label_125E;
        Label_052B:
            if (this.API != 4)
            {
                goto Label_0586;
            }
            response4 = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqMPVersion.Response>>(&www.text);
            DebugUtility.Assert((response4 == null) == 0, "res == null");
            if (response4.body != null)
            {
                goto Label_056A;
            }
            this.OnFailed();
            return;
        Label_056A:
            BootLoader.GetAccountManager().SetDeviceId(null, response4.body.device_id);
            goto Label_125E;
        Label_0586:
            if (this.API != 5)
            {
                goto Label_0676;
            }
            response5 = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqVersusStart.Response>>(&www.text);
            DebugUtility.Assert((response5 == null) == 0, "res == null");
            if (response5.body != null)
            {
                goto Label_05C5;
            }
            this.OnFailed();
            return;
        Label_05C5:
            manager = MonoSingleton<GameManager>.Instance;
            GlobalVars.SelectedMultiPlayPhotonAppID = response5.body.app_id;
            switch (GlobalVars.SelectedMultiPlayVersusType)
            {
                case 0:
                    goto Label_05FC;

                case 1:
                    goto Label_0653;

                case 2:
                    goto Label_0638;
            }
            goto Label_0653;
        Label_05FC:
            if (manager.VSDraftType != 1)
            {
                goto Label_061D;
            }
            GlobalVars.SelectedQuestID = MonoSingleton<GameManager>.Instance.VSDraftQuestId;
            goto Label_0633;
        Label_061D:
            GlobalVars.SelectedQuestID = response5.body.maps.free;
        Label_0633:
            goto Label_0653;
        Label_0638:
            GlobalVars.SelectedQuestID = response5.body.maps.friend;
        Label_0653:
            DebugUtility.Log("MakeRoom RoomID: AppID:" + GlobalVars.SelectedMultiPlayPhotonAppID + "/ QuestID:" + GlobalVars.SelectedQuestID);
            goto Label_125E;
        Label_0676:
            if (this.API != 6)
            {
                goto Label_06F1;
            }
            response6 = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqVersusMake.Response>>(&www.text);
            DebugUtility.Assert((response6 == null) == 0, "res == null");
            if (response6.body != null)
            {
                goto Label_06B5;
            }
            this.OnFailed();
            return;
        Label_06B5:
            GlobalVars.SelectedMultiPlayRoomID = response6.body.roomid;
            GlobalVars.SelectedMultiPlayRoomName = response6.body.token;
            if (GlobalVars.SelectedMultiPlayVersusType != 2)
            {
                goto Label_125E;
            }
            GlobalVars.EditMultiPlayRoomPassCode = "1";
            goto Label_125E;
        Label_06F1:
            if (this.API != 7)
            {
                goto Label_07BE;
            }
            response7 = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqVersusRoomJoin.Response>>(&www.text);
            DebugUtility.Assert((response7 == null) == 0, "res == null");
            if (response7.body != null)
            {
                goto Label_0730;
            }
            this.OnFailed();
            return;
        Label_0730:
            if (response7.body.quest == null)
            {
                goto Label_075C;
            }
            if (string.IsNullOrEmpty(response7.body.quest.iname) == null)
            {
                goto Label_0763;
            }
        Label_075C:
            this.OnFailed();
            return;
        Label_0763:
            GlobalVars.SelectedQuestID = response7.body.quest.iname;
            GlobalVars.SelectedMultiPlayPhotonAppID = response7.body.app_id;
            GlobalVars.SelectedMultiPlayRoomName = response7.body.token;
            DebugUtility.Log("JoinRoom  AppID:" + GlobalVars.SelectedMultiPlayPhotonAppID + " Name:" + GlobalVars.SelectedMultiPlayRoomName);
            goto Label_125E;
        Label_07BE:
            if (this.API != 8)
            {
                goto Label_07CF;
            }
            goto Label_125E;
        Label_07CF:
            if (this.API != 9)
            {
                goto Label_07E1;
            }
            goto Label_125E;
        Label_07E1:
            if (this.API != 10)
            {
                goto Label_0A7C;
            }
            response8 = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqVersusStatus.Response>>(&www.text);
            if (response8 != null)
            {
                goto Label_080A;
            }
            this.OnFailed();
            return;
        Label_080A:
            manager2 = MonoSingleton<GameManager>.Instance;
            manager2.Player.SetTowerMatchInfo(response8.body.floor, response8.body.key, response8.body.wincnt, (response8.body.is_give_season_gift == 0) == 0);
            manager2.VersusTowerMatchBegin = string.IsNullOrEmpty(response8.body.vstower_id) == 0;
            manager2.VersusTowerMatchReceipt = (response8.body.is_season_gift == 0) == 0;
            manager2.VersusTowerMatchName = response8.body.tower_iname;
            manager2.VersusTowerMatchEndAt = response8.body.end_at;
            manager2.VersusCoinRemainCnt = response8.body.daycnt;
            manager2.VersusLastUid = response8.body.last_enemyuid;
            manager2.IsVSFirstWinRewardRecived = (response8.body.is_firstwin == 0) == 0;
            GlobalVars.SelectedQuestID = response8.body.vstower_id;
            if (response8.body.streakwins == null)
            {
                goto Label_09B2;
            }
            num = 0;
            goto Label_099D;
        Label_091A:
            status = response8.body.streakwins[num];
            streak_judge2 = manager2.SearchVersusJudgeType(status.schedule_id, -1L);
            if (streak_judge2 == null)
            {
                goto Label_0976;
            }
            if (streak_judge2 == 1)
            {
                goto Label_0955;
            }
            goto Label_0997;
        Label_0955:
            manager2.VS_StreakWinCnt_Now = status.num;
            manager2.VS_StreakWinCnt_Best = status.best;
            goto Label_0997;
        Label_0976:
            manager2.VS_StreakWinCnt_NowAllPriod = status.num;
            manager2.VS_StreakWinCnt_BestAllPriod = status.best;
        Label_0997:
            num += 1;
        Label_099D:
            if (num < ((int) response8.body.streakwins.Length))
            {
                goto Label_091A;
            }
        Label_09B2:
            if (response8.body.enabletime == null)
            {
                goto Label_0A53;
            }
            manager2.VSFreeExpiredTime = response8.body.enabletime.expired;
            manager2.VSFreeNextTime = response8.body.enabletime.next;
            manager2.VSDraftType = response8.body.enabletime.draft_type;
            manager2.mVersusEnableId = response8.body.enabletime.schedule_id;
            manager2.VSDraftId = response8.body.enabletime.draft_id;
            manager2.VSDraftQuestId = response8.body.enabletime.iname;
        Label_0A53:
            manager2.Player.UpdateVersusTowerTrophyStates(response8.body.tower_iname, response8.body.floor);
            goto Label_125E;
        Label_0A7C:
            if (this.API != 11)
            {
                goto Label_0ADF;
            }
            response9 = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqVersusSeason.Response>>(&www.text);
            if (response9 != null)
            {
                goto Label_0AA5;
            }
            this.OnFailed();
            return;
        Label_0AA5:
            data2 = MonoSingleton<GameManager>.Instance.Player;
            data2.VersusSeazonGiftReceipt = 0;
            data2.UnreadMailPeriod |= response9.body.unreadmail == 1;
            goto Label_125E;
        Label_0ADF:
            if (this.API != 12)
            {
                goto Label_0B28;
            }
            response10 = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqVersusFriendScore.Response>>(&www.text);
            if (response10 != null)
            {
                goto Label_0B08;
            }
            this.OnFailed();
            return;
        Label_0B08:
            MonoSingleton<GameManager>.Instance.Deserialize(response10.body.friends);
            goto Label_125E;
        Label_0B28:
            if (this.API != 13)
            {
                goto Label_0B8D;
            }
            response11 = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqMultiTwStatus.Response>>(&www.text);
            Debug.Log(&www.text);
            if (response11 != null)
            {
                goto Label_0B5D;
            }
            this.OnFailed();
            return;
        Label_0B5D:
            GlobalVars.SelectedMultiPlayPhotonAppID = response11.body.appid;
            MonoSingleton<GameManager>.Instance.Deserialize(response11.body.floors);
            goto Label_125E;
        Label_0B8D:
            if (this.API != 14)
            {
                goto Label_0CF1;
            }
            response12 = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqMultiTwRoomJoin.Response>>(&www.text);
            DebugUtility.Assert((response12 == null) == 0, "res == null");
            if (response12.body != null)
            {
                goto Label_0BCD;
            }
            this.OnFailed();
            return;
        Label_0BCD:
            if (response12.body.quest == null)
            {
                goto Label_0BF9;
            }
            if (string.IsNullOrEmpty(response12.body.quest.iname) == null)
            {
                goto Label_0C00;
            }
        Label_0BF9:
            this.OnFailed();
            return;
        Label_0C00:
            manager6 = MonoSingleton<GameManager>.Instance;
            if (manager6.GetMTChallengeFloor() >= response12.body.quest.floor)
            {
                goto Label_0C4A;
            }
            Network.RemoveAPI();
            if ((this == null) == null)
            {
                goto Label_0C36;
            }
            return;
        Label_0C36:
            base.set_enabled(0);
            base.ActivateOutputLinks(0x2af8);
            return;
        Label_0C4A:
            GlobalVars.SelectedMultiTowerID = response12.body.quest.iname;
            GlobalVars.SelectedMultiTowerFloor = response12.body.quest.floor;
            GlobalVars.SelectedMultiPlayPhotonAppID = response12.body.app_id;
            GlobalVars.SelectedMultiPlayRoomName = response12.body.token;
            param = manager6.GetMTFloorParam(GlobalVars.SelectedMultiTowerID, GlobalVars.SelectedMultiTowerFloor);
            if (param == null)
            {
                goto Label_0CCE;
            }
            param2 = param.GetQuestParam();
            if (param2 == null)
            {
                goto Label_0CCE;
            }
            GlobalVars.SelectedQuestID = param2.iname;
        Label_0CCE:
            DebugUtility.Log("JoinRoom  AppID:" + GlobalVars.SelectedMultiPlayPhotonAppID + " Name:" + GlobalVars.SelectedMultiPlayRoomName);
            goto Label_125E;
        Label_0CF1:
            if (this.API != 15)
            {
                goto Label_0E73;
            }
            response13 = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqRankMatchStart.Response>>(&www.text);
            if (response13 != null)
            {
                goto Label_0D1A;
            }
            this.OnFailed();
            return;
        Label_0D1A:
            manager7 = MonoSingleton<GameManager>.Instance;
            data3 = manager7.Player;
            num2 = 0;
            if (response13.body.streakwin == null)
            {
                goto Label_0D51;
            }
            num2 = response13.body.streakwin.num;
        Label_0D51:
            data3.SetRankMatchInfo(response13.body.rank, response13.body.score, response13.body.type, response13.body.bp, num2, response13.body.wincnt, response13.body.losecnt);
            manager7.RankMatchScheduleId = response13.body.schedule_id;
            GlobalVars.SelectedMultiPlayPhotonAppID = response13.body.app_id;
            num9 = 0L;
            manager7.RankMatchNextTime = num9;
            manager7.RankMatchExpiredTime = num9;
            if (response13.body.enabletime == null)
            {
                goto Label_0E55;
            }
            manager7.RankMatchExpiredTime = response13.body.enabletime.expired;
            manager7.RankMatchNextTime = response13.body.enabletime.next;
            GlobalVars.SelectedQuestID = response13.body.enabletime.iname;
            num3 = manager7.RankMatchExpiredTime;
            num4 = TimeManager.FromDateTime(TimeManager.ServerTime);
            manager7.RankMatchBegin = num4 < num3;
        Label_0E55:
            manager7.RankMatchMatchedEnemies = response13.body.enemies;
            GlobalVars.IsVersusDraftMode = 0;
            goto Label_125E;
        Label_0E73:
            if (this.API != 0x10)
            {
                goto Label_0F5D;
            }
            response14 = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqRankMatchStatus.Response>>(&www.text);
            if (response14 != null)
            {
                goto Label_0E9C;
            }
            this.OnFailed();
            return;
        Label_0E9C:
            manager8 = MonoSingleton<GameManager>.Instance;
            manager8.RankMatchScheduleId = response14.body.schedule_id;
            manager8.RankMatchRankingStatus = response14.body.RankingStatus;
            num9 = 0L;
            manager8.RankMatchNextTime = num9;
            manager8.RankMatchExpiredTime = num9;
            if (response14.body.enabletime == null)
            {
                goto Label_125E;
            }
            manager8.RankMatchExpiredTime = response14.body.enabletime.expired;
            manager8.RankMatchNextTime = response14.body.enabletime.next;
            GlobalVars.SelectedQuestID = response14.body.enabletime.iname;
            num5 = manager8.RankMatchExpiredTime;
            num6 = TimeManager.FromDateTime(TimeManager.ServerTime);
            manager8.RankMatchBegin = num6 < num5;
            goto Label_125E;
        Label_0F5D:
            if (this.API != 0x11)
            {
                goto Label_1113;
            }
            storeyd = new <OnSuccess>c__AnonStorey26D();
            storeyd.res = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqVersusDraft.Response>>(&www.text);
            if (storeyd.res != null)
            {
                goto Label_0F97;
            }
            this.OnFailed();
            return;
        Label_0F97:
            manager9 = MonoSingleton<GameManager>.Instance;
            list = manager9.GetVersusDraftUnits(manager9.VSDraftId);
            if (list == null)
            {
                goto Label_0FC3;
            }
            if (list.Count >= 0x10)
            {
                goto Label_0FD4;
            }
        Label_0FC3:
            Debug.LogError("VersusDraftUnits below than 16. It needs above than 16.");
            this.Failure();
            return;
        Label_0FD4:
            if (storeyd.res.body.draft_units == null)
            {
                goto Label_1004;
            }
            if (((int) storeyd.res.body.draft_units.Length) == 0x10)
            {
                goto Label_1015;
            }
        Label_1004:
            Debug.LogError("The number of VersusDraftUnit is not 16.");
            this.Failure();
            return;
        Label_1015:
            VersusDraftList.VersusDraftUnitList = new List<VersusDraftUnitParam>();
            storeye = new <OnSuccess>c__AnonStorey26E();
            storeye.<>f__ref$621 = storeyd;
            storeye.i = 0;
            goto Label_10D6;
        Label_103C:
            param3 = list.Find(new Predicate<VersusDraftUnitParam>(storeye.<>m__1A8));
            if (param3 != null)
            {
                goto Label_1092;
            }
            Debug.LogError("Selecting ID invalid: " + ((long) storeyd.res.body.draft_units[storeye.i].id));
            this.Failure();
            return;
        Label_1092:
            param3.IsHidden = storeyd.res.body.draft_units[storeye.i].secret == 1;
            VersusDraftList.VersusDraftUnitList.Add(param3);
            storeye.i += 1;
        Label_10D6:
            if (storeye.i < ((int) storeyd.res.body.draft_units.Length))
            {
                goto Label_103C;
            }
            VersusDraftList.VersusDraftTurnOwn = storeyd.res.body.turn_own == 1;
            goto Label_125E;
        Label_1113:
            if (this.API != 0x12)
            {
                goto Label_1152;
            }
            response15 = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqVersusDraftSelect.Response>>(&www.text);
            if (response15 != null)
            {
                goto Label_113C;
            }
            this.OnFailed();
            return;
        Label_113C:
            VersusDraftList.DraftID = response15.body.draft_id;
            goto Label_125E;
        Label_1152:
            if (this.API != 20)
            {
                goto Label_125E;
            }
            response16 = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqVersusLobby.Response>>(&www.text);
            if (response16 != null)
            {
                goto Label_117B;
            }
            this.OnFailed();
            return;
        Label_117B:
            manager10 = MonoSingleton<GameManager>.Instance;
            manager10.RankMatchScheduleId = response16.body.rankmatch_schedule_id;
            manager10.RankMatchRankingStatus = response16.body.RankMatchRankingStatus;
            num9 = 0L;
            manager10.RankMatchNextTime = num9;
            manager10.RankMatchExpiredTime = num9;
            if (response16.body.rankmatch_enabletime == null)
            {
                goto Label_1237;
            }
            manager10.RankMatchExpiredTime = response16.body.rankmatch_enabletime.expired;
            manager10.RankMatchNextTime = response16.body.rankmatch_enabletime.next;
            GlobalVars.SelectedQuestID = response16.body.rankmatch_enabletime.iname;
            num7 = manager10.RankMatchExpiredTime;
            num8 = TimeManager.FromDateTime(TimeManager.ServerTime);
            manager10.RankMatchBegin = num8 < num7;
        Label_1237:
            manager10.VSDraftType = response16.body.draft_type;
            manager10.mVersusEnableId = (long) response16.body.draft_schedule_id;
        Label_125E:
            Network.RemoveAPI();
            this.Success();
            return;
        }

        private void ResetPlacementParam()
        {
            int num;
            num = 0;
            goto Label_0023;
        Label_0007:
            PlayerPrefsUtility.SetInt(PlayerPrefsUtility.MULTITW_ID_KEY + ((int) num), num, 0);
            num += 1;
        Label_0023:
            if (num < this.MULTI_TOWER_UNIT_MAX)
            {
                goto Label_0007;
            }
            PlayerPrefsUtility.Save();
            return;
        }

        private void Success()
        {
            DebugUtility.Log("MultiPlayAPI success");
            if ((this == null) == null)
            {
                goto Label_0017;
            }
            return;
        Label_0017:
            base.set_enabled(0);
            base.ActivateOutputLinks(100);
            return;
        }

        private EAPI API
        {
            [CompilerGenerated]
            get
            {
                return this.<API>k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this.<API>k__BackingField = value;
                return;
            }
        }

        [CompilerGenerated]
        private sealed class <GetPhotonRoomList>c__IteratorBC : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal MyPhoton <pt>__0;
            internal MyPhoton.MyState <state>__1;
            internal List<MyPhoton.MyRoom> <rooms>__2;
            internal int <i>__3;
            internal JSON_MyPhotonRoomParam <param>__4;
            internal string fuid;
            internal MultiPlayAPIRoom <room>__5;
            internal int $PC;
            internal object $current;
            internal string <$>fuid;
            internal FlowNode_MultiPlayAPI <>f__this;

            public <GetPhotonRoomList>c__IteratorBC()
            {
                base..ctor();
                return;
            }

            [DebuggerHidden]
            public void Dispose()
            {
                this.$PC = -1;
                return;
            }

            public bool MoveNext()
            {
                uint num;
                MyPhoton.MyState state;
                bool flag;
                num = this.$PC;
                this.$PC = -1;
                switch (num)
                {
                    case 0:
                        goto Label_0025;

                    case 1:
                        goto Label_00A2;

                    case 2:
                        goto Label_0115;
                }
                goto Label_0319;
            Label_0025:
                this.<pt>__0 = PunMonoSingleton<MyPhoton>.Instance;
                this.<state>__1 = this.<pt>__0.CurrentState;
                if (this.<state>__1 == 2)
                {
                    goto Label_00A2;
                }
                this.<pt>__0.Disconnect();
                if (this.<pt>__0.StartConnect(null, 0, "1.0") != null)
                {
                    goto Label_00A2;
                }
                this.<pt>__0.Disconnect();
                this.<>f__this.Failure();
                goto Label_0319;
                goto Label_00A2;
            Label_008F:
                this.$current = null;
                this.$PC = 1;
                goto Label_031B;
            Label_00A2:
                if ((this.<state>__1 = this.<pt>__0.CurrentState) == 1)
                {
                    goto Label_008F;
                }
                if (this.<state>__1 == 2)
                {
                    goto Label_0115;
                }
                DebugUtility.Log("state:" + ((MyPhoton.MyState) this.<state>__1));
                this.<pt>__0.Disconnect();
                this.<>f__this.Failure();
                goto Label_0319;
                goto Label_0115;
            Label_0102:
                this.$current = null;
                this.$PC = 2;
                goto Label_031B;
            Label_0115:
                if (this.<pt>__0.IsRoomListUpdated == null)
                {
                    goto Label_0102;
                }
                FlowNode_MultiPlayAPI.RoomList = new ReqMPRoom.Response();
                this.<rooms>__2 = this.<pt>__0.GetRoomList();
                if (this.<rooms>__2 != null)
                {
                    goto Label_0166;
                }
                this.<pt>__0.Disconnect();
                this.<>f__this.Failure();
                goto Label_0319;
            Label_0166:
                FlowNode_MultiPlayAPI.RoomList.rooms = new MultiPlayAPIRoom[this.<rooms>__2.Count];
                this.<i>__3 = 0;
                goto Label_02C5;
            Label_018C:
                if (this.<rooms>__2[this.<i>__3] == null)
                {
                    goto Label_02B7;
                }
                if (string.IsNullOrEmpty(this.<rooms>__2[this.<i>__3].json) == null)
                {
                    goto Label_01C7;
                }
                goto Label_02B7;
            Label_01C7:
                this.<param>__4 = JSON_MyPhotonRoomParam.Parse(this.<rooms>__2[this.<i>__3].json);
                if (this.<param>__4 != null)
                {
                    goto Label_01F8;
                }
                goto Label_02B7;
            Label_01F8:
                if (string.IsNullOrEmpty(this.fuid) != null)
                {
                    goto Label_0228;
                }
                if (this.fuid.Equals(this.<param>__4.creatorFUID) != null)
                {
                    goto Label_0228;
                }
                goto Label_02B7;
            Label_0228:
                this.<room>__5 = new MultiPlayAPIRoom();
                this.<room>__5.roomid = this.<param>__4.roomid;
                this.<room>__5.comment = this.<param>__4.comment;
                this.<room>__5.quest = new MultiPlayAPIRoom.Quest();
                this.<room>__5.quest.iname = this.<param>__4.iname;
                this.<room>__5.pwd_hash = this.<param>__4.passCode;
                FlowNode_MultiPlayAPI.RoomList.rooms[this.<i>__3] = this.<room>__5;
            Label_02B7:
                this.<i>__3 += 1;
            Label_02C5:
                if (this.<i>__3 < ((int) FlowNode_MultiPlayAPI.RoomList.rooms.Length))
                {
                    goto Label_018C;
                }
                DebugUtility.Log("ListRoom num:" + ((int) ((int) FlowNode_MultiPlayAPI.RoomList.rooms.Length)));
                this.<pt>__0.Disconnect();
                this.<>f__this.Success();
                this.$PC = -1;
            Label_0319:
                return 0;
            Label_031B:
                return 1;
                return flag;
            }

            [DebuggerHidden]
            public void Reset()
            {
                throw new NotSupportedException();
            }

            object IEnumerator<object>.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.$current;
                }
            }

            object IEnumerator.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.$current;
                }
            }
        }

        [CompilerGenerated]
        private sealed class <GetPhotonRoomName>c__IteratorBD : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal MyPhoton <pt>__0;
            internal MyPhoton.MyState <state>__1;
            internal List<MyPhoton.MyRoom> <rooms>__2;
            internal List<MyPhoton.MyRoom>.Enumerator <$s_534>__3;
            internal MyPhoton.MyRoom <room>__4;
            internal JSON_MyPhotonRoomParam <roomParam>__5;
            internal int $PC;
            internal object $current;
            internal FlowNode_MultiPlayAPI <>f__this;

            public <GetPhotonRoomName>c__IteratorBD()
            {
                base..ctor();
                return;
            }

            [DebuggerHidden]
            public void Dispose()
            {
                this.$PC = -1;
                return;
            }

            public unsafe bool MoveNext()
            {
                uint num;
                MyPhoton.MyState state;
                bool flag;
                num = this.$PC;
                this.$PC = -1;
                switch (num)
                {
                    case 0:
                        goto Label_0025;

                    case 1:
                        goto Label_00A2;

                    case 2:
                        goto Label_0115;
                }
                goto Label_0250;
            Label_0025:
                this.<pt>__0 = PunMonoSingleton<MyPhoton>.Instance;
                this.<state>__1 = this.<pt>__0.CurrentState;
                if (this.<state>__1 == 2)
                {
                    goto Label_00A2;
                }
                this.<pt>__0.Disconnect();
                if (this.<pt>__0.StartConnect(null, 0, "1.0") != null)
                {
                    goto Label_00A2;
                }
                this.<pt>__0.Disconnect();
                this.<>f__this.Failure();
                goto Label_0250;
                goto Label_00A2;
            Label_008F:
                this.$current = null;
                this.$PC = 1;
                goto Label_0252;
            Label_00A2:
                if ((this.<state>__1 = this.<pt>__0.CurrentState) == 1)
                {
                    goto Label_008F;
                }
                if (this.<state>__1 == 2)
                {
                    goto Label_0115;
                }
                DebugUtility.Log("state:" + ((MyPhoton.MyState) this.<state>__1));
                this.<pt>__0.Disconnect();
                this.<>f__this.Failure();
                goto Label_0250;
                goto Label_0115;
            Label_0102:
                this.$current = null;
                this.$PC = 2;
                goto Label_0252;
            Label_0115:
                if (this.<pt>__0.IsRoomListUpdated == null)
                {
                    goto Label_0102;
                }
                this.<rooms>__2 = this.<pt>__0.GetRoomList();
                if (this.<rooms>__2 != null)
                {
                    goto Label_015C;
                }
                this.<pt>__0.Disconnect();
                this.<>f__this.Failure();
                goto Label_0250;
            Label_015C:
                this.<$s_534>__3 = this.<rooms>__2.GetEnumerator();
            Label_016D:
                try
                {
                    goto Label_020D;
                Label_0172:
                    this.<room>__4 = &this.<$s_534>__3.Current;
                    this.<roomParam>__5 = JSON_MyPhotonRoomParam.Parse(this.<room>__4.json);
                    if (this.<roomParam>__5 != null)
                    {
                        goto Label_01A9;
                    }
                    goto Label_020D;
                Label_01A9:
                    if (this.<roomParam>__5.roomid != GlobalVars.SelectedMultiPlayRoomID)
                    {
                        goto Label_020D;
                    }
                    GlobalVars.SelectedMultiPlayPhotonAppID = null;
                    GlobalVars.SelectedMultiPlayRoomName = this.<room>__4.name;
                    DebugUtility.Log("JoinRoom  AppID:" + GlobalVars.SelectedMultiPlayPhotonAppID + " Name:" + GlobalVars.SelectedMultiPlayRoomName);
                    this.<pt>__0.Disconnect();
                    this.<>f__this.Success();
                    goto Label_0250;
                Label_020D:
                    if (&this.<$s_534>__3.MoveNext() != null)
                    {
                        goto Label_0172;
                    }
                    goto Label_0233;
                }
                finally
                {
                Label_0222:
                    ((List<MyPhoton.MyRoom>.Enumerator) this.<$s_534>__3).Dispose();
                }
            Label_0233:
                this.<pt>__0.Disconnect();
                this.<>f__this.Failure();
                this.$PC = -1;
            Label_0250:
                return 0;
            Label_0252:
                return 1;
                return flag;
            }

            [DebuggerHidden]
            public void Reset()
            {
                throw new NotSupportedException();
            }

            object IEnumerator<object>.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.$current;
                }
            }

            object IEnumerator.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.$current;
                }
            }
        }

        [CompilerGenerated]
        private sealed class <OnActivate>c__AnonStorey26C
        {
            internal JSON_MyPhotonPlayerParam target_player;

            public <OnActivate>c__AnonStorey26C()
            {
                base..ctor();
                return;
            }

            internal bool <>m__1A7(string uid)
            {
                return (uid == this.target_player.UID);
            }
        }

        [CompilerGenerated]
        private sealed class <OnSuccess>c__AnonStorey26D
        {
            internal WebAPI.JSON_BodyResponse<ReqVersusDraft.Response> res;

            public <OnSuccess>c__AnonStorey26D()
            {
                base..ctor();
                return;
            }
        }

        [CompilerGenerated]
        private sealed class <OnSuccess>c__AnonStorey26E
        {
            internal int i;
            internal FlowNode_MultiPlayAPI.<OnSuccess>c__AnonStorey26D <>f__ref$621;

            public <OnSuccess>c__AnonStorey26E()
            {
                base..ctor();
                return;
            }

            internal bool <>m__1A8(VersusDraftUnitParam vdup)
            {
                return (vdup.DraftUnitId == this.<>f__ref$621.res.body.draft_units[this.i].id);
            }
        }

        private enum EAPI
        {
            MAKE,
            ROOM,
            JOIN,
            UPDATE,
            VERSION,
            VERSUS_START,
            VERSUS_MAKE,
            VERSUS_JOIN,
            VERSUS_LINE_REQ,
            VERSUS_LINE_MAKE,
            VERSUS_STATUS,
            VERSUS_SEASON,
            VERSUS_FRIEND,
            MT_STATUS,
            MT_JOIN,
            RANKMATCH_START,
            RANKMATCH_STATUS,
            VERSUS_DRAFT,
            VERSUS_DRAFT_SELECTED,
            VERSUS_DRAFT_PARTY,
            VERSUS_LOBBY
        }
    }
}

