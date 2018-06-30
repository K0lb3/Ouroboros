namespace SRPG
{
    using GR;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Text;
    using UnityEngine;

    [Pin(9, "NoAudienceData", 1, 0x12), Pin(7, "MatchSuccess", 1, 0x10), Pin(6, "ColoRankModify", 1, 15), Pin(5, "NetworkSuccess", 1, 14), Pin(4, "MultiMaintenance", 1, 13), Pin(3, "NoMatchVersion", 1, 12), Pin(2, "Failed", 1, 11), Pin(1, "Started", 1, 10), Pin(0x3e8, "LoadOrdeal", 0, 7), Pin(30, "AudienceStart", 0, 6), Pin(20, "AudienceConnect", 0, 5), Pin(10, "Resume", 0, 4), Pin(500, "LoadMultiTower", 0, 3), Pin(250, "LoadRankMatch", 0, 2), Pin(200, "LoadVersus", 0, 2), Pin(100, "LoadMultiPlay", 0, 1), Pin(0, "Load", 0, 0), NodeType("System/Quest/Start", 0x7fe5), Pin(400, "NotGpsQuest", 1, 0x15), Pin(600, "NotRoomMT", 1, 0x16), Pin(700, "LoadVersusCpu", 0, 0x17), Pin(800, "QuestResumeFailed_IsRehash", 1, 0x18), Pin(0x12d, "AudienceFailedMax", 1, 20), Pin(300, "AudienceFailed", 1, 0x13), Pin(8, "NoRoom", 1, 0x11)]
    public class FlowNode_StartQuest : FlowNode_Network
    {
        private const int PIN_IN_LOAD_VERSUS = 200;
        private const int PIN_IN_LOAD_RANK_MATCH = 250;
        [HideInInspector]
        public string QuestID;
        public bool ReplaceScene;
        [HideInInspector]
        public bool PlayOffline;
        protected bool mResume;
        [HideInInspector]
        public RestorePoints RestorePoint;
        public bool SetRestorePoint;
        private BattleCore.Json_Battle mQuestData;
        protected QuestParam mStartingQuest;
        public int mReqID;
        private float mConnectTime;

        public FlowNode_StartQuest()
        {
            this.mReqID = -1;
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private void <OnSuccess>m__1AE(GameObject go)
        {
            CriticalSection.Leave(4);
            base.ActivateOutputLinks(800);
            return;
        }

        public override void OnActivate(int pinID)
        {
            GameManager manager;
            long num;
            PlayerPartyTypes types;
            PartyData data;
            int num2;
            bool flag;
            bool flag2;
            int num3;
            int num4;
            string str;
            List<string> list;
            VersusStatusData data2;
            int num5;
            MyPhoton.MyPlayer player;
            List<JSON_MyPhotonPlayerParam> list2;
            MyPhoton.MyRoom room;
            int num6;
            JSON_MyPhotonPlayerParam param;
            PlayerPartyTypes types2;
            PartyData data3;
            int num7;
            long num8;
            UnitData data4;
            int num9;
            UnitData data5;
            List<JSON_MyPhotonPlayerParam> list3;
            int num10;
            int num11;
            JSON_MyPhotonPlayerParam param2;
            int num12;
            MyPhoton.MyRoom room2;
            VersusAudienceManager manager2;
            AudienceStartParam param3;
            BattleCore.Json_Battle battle;
            <OnActivate>c__AnonStorey270 storey;
            <OnActivate>c__AnonStorey271 storey2;
            storey = new <OnActivate>c__AnonStorey270();
            manager = MonoSingleton<GameManager>.Instance;
            storey.pt = PunMonoSingleton<MyPhoton>.Instance;
            manager.AudienceMode = 0;
            this.mReqID = pinID;
            storey.pt.IsMultiPlay = 0;
            storey.pt.IsMultiVersus = 0;
            manager.IsVSCpuBattle = 0;
            storey.pt.IsRankMatch = 0;
            if ((((pinID != null) && (pinID != 100)) && ((pinID != 200) && (pinID != 250))) && (((pinID != 500) && (pinID != 700)) && (pinID != 0x3e8)))
            {
                goto Label_0116;
            }
            storey.pt.IsMultiPlay = (((pinID == 100) || (pinID == 200)) || (pinID == 250)) ? 1 : (pinID == 500);
            storey.pt.IsMultiVersus = (pinID == 200) ? 1 : (pinID == 250);
            manager.IsVSCpuBattle = pinID == 700;
            storey.pt.IsRankMatch = pinID == 250;
            pinID = 0;
        Label_0116:
            if (pinID != 10)
            {
                goto Label_0128;
            }
            this.mResume = 1;
            pinID = 0;
        Label_0128:
            if (pinID != null)
            {
                goto Label_06F4;
            }
            if (base.get_enabled() == null)
            {
                goto Label_013A;
            }
            return;
        Label_013A:
            base.set_enabled(1);
            CriticalSection.Enter(4);
            if (this.mResume == null)
            {
                goto Label_0186;
            }
            num = GlobalVars.BtlID;
            GlobalVars.BtlID.Set(0L);
            base.ExecRequest(new ReqBtlComResume(num, new Network.ResponseCallback(this.ResponseCallback)));
            goto Label_06EF;
        Label_0186:
            this.mStartingQuest = manager.FindQuest(GlobalVars.SelectedQuestID);
            types = this.QuestToPartyIndex(this.mStartingQuest.type);
            if (string.IsNullOrEmpty(this.QuestID) != null)
            {
                goto Label_01CE;
            }
            GlobalVars.SelectedQuestID = this.QuestID;
            GlobalVars.SelectedFriendID = string.Empty;
        Label_01CE:
            if ((this.PlayOffline != null) || (Network.Mode != null))
            {
                goto Label_06E1;
            }
            if (this.mStartingQuest.type != 2)
            {
                goto Label_020F;
            }
            base.ActivateOutputLinks(5);
            base.StartCoroutine(this.StartScene(null));
            goto Label_06DC;
        Label_020F:
            if (this.mStartingQuest.type != 15)
            {
                goto Label_024D;
            }
            base.ExecRequest(new ReqBtlOrdealReq(this.mStartingQuest.iname, GlobalVars.OrdealSupports, new Network.ResponseCallback(this.ResponseCallback)));
            goto Label_06DC;
        Label_024D:
            data = manager.Player.FindPartyOfType(types);
            num2 = manager.Player.Partys.IndexOf(data);
            flag = 0;
            flag2 = 0;
            num3 = -1;
            num4 = -1;
            str = string.Empty;
            list = new List<string>();
            data2 = null;
            num5 = 0;
            if ((storey.pt != null) == null)
            {
                goto Label_04E2;
            }
            flag = storey.pt.IsMultiPlay;
            flag2 = storey.pt.IsOldestPlayer();
            num3 = storey.pt.MyPlayerIndex;
            player = storey.pt.GetMyPlayer();
            if (player == null)
            {
                goto Label_02E7;
            }
            num4 = player.playerID;
        Label_02E7:
            if (storey.pt.IsMultiVersus == null)
            {
                goto Label_0484;
            }
            list2 = storey.pt.GetMyPlayersStarted();
            room = storey.pt.GetCurrentRoom();
            num6 = (room == null) ? 1 : room.playerCount;
            param = list2.Find(new Predicate<JSON_MyPhotonPlayerParam>(storey.<>m__1AC));
            if (param == null)
            {
                goto Label_0350;
            }
            str = param.UID;
        Label_0350:
            if (GlobalVars.IsVersusDraftMode != null)
            {
                goto Label_0424;
            }
            if ((string.IsNullOrEmpty(str) == null) && (num6 != 1))
            {
                goto Label_0375;
            }
            this.OnVersusNoPlayer();
            return;
        Label_0375:
            types2 = 7;
            if (storey.pt.IsRankMatch == null)
            {
                goto Label_038D;
            }
            types2 = 10;
        Label_038D:
            data3 = manager.Player.Partys[types2];
            if (data3 == null)
            {
                goto Label_04E2;
            }
            data2 = new VersusStatusData();
            num7 = 0;
            goto Label_0411;
        Label_03B7:
            num8 = data3.GetUnitUniqueID(num7);
            if (data3.GetUnitUniqueID(num7) != null)
            {
                goto Label_03D5;
            }
            goto Label_040B;
        Label_03D5:
            data4 = manager.Player.FindUnitDataByUniqueID(num8);
            if (data4 == null)
            {
                goto Label_040B;
            }
            data2.Add(data4.Status.param, data4.GetCombination());
            num5 += 1;
        Label_040B:
            num7 += 1;
        Label_0411:
            if (num7 < data3.MAX_UNIT)
            {
                goto Label_03B7;
            }
            goto Label_047F;
        Label_0424:
            data2 = new VersusStatusData();
            num9 = 0;
            goto Label_046E;
        Label_0433:
            data5 = VersusDraftList.VersusDraftPartyUnits[num9];
            if (data5 == null)
            {
                goto Label_0468;
            }
            data2.Add(data5.Status.param, data5.GetCombination());
            num5 += 1;
        Label_0468:
            num9 += 1;
        Label_046E:
            if (num9 < VersusDraftList.VersusDraftPartyUnits.Count)
            {
                goto Label_0433;
            }
        Label_047F:
            goto Label_04E2;
        Label_0484:
            list3 = storey.pt.GetMyPlayersStarted();
            num10 = 0;
            goto Label_04D4;
        Label_049A:
            if (list3[num10].playerIndex == storey.pt.MyPlayerIndex)
            {
                goto Label_04CE;
            }
            list.Add(list3[num10].UID);
        Label_04CE:
            num10 += 1;
        Label_04D4:
            if (num10 < list3.Count)
            {
                goto Label_049A;
            }
        Label_04E2:
            if (this.mReqID != 200)
            {
                goto Label_05BC;
            }
            if (GlobalVars.IsVersusDraftMode == null)
            {
                goto Label_0584;
            }
            storey2 = new <OnActivate>c__AnonStorey271();
            num11 = 0;
            storey2.player = PunMonoSingleton<MyPhoton>.Instance.GetMyPlayer();
            param2 = storey.pt.GetMyPlayersStarted().Find(new Predicate<JSON_MyPhotonPlayerParam>(storey2.<>m__1AD));
            if (param2 == null)
            {
                goto Label_0547;
            }
            num11 = param2.draft_id;
        Label_0547:
            base.ExecRequest(new ReqVersus(this.mStartingQuest.iname, num4, num3, str, data2, num5, new Network.ResponseCallback(this.ResponseCallback), GlobalVars.SelectedMultiPlayVersusType, VersusDraftList.DraftID, num11));
            goto Label_05B7;
        Label_0584:
            base.ExecRequest(new ReqVersus(this.mStartingQuest.iname, num4, num3, str, data2, num5, new Network.ResponseCallback(this.ResponseCallback), GlobalVars.SelectedMultiPlayVersusType, 0, 0));
        Label_05B7:
            goto Label_06DC;
        Label_05BC:
            if (this.mReqID != 250)
            {
                goto Label_05F9;
            }
            base.ExecRequest(new ReqRankMatch(this.mStartingQuest.iname, num4, num3, str, new Network.ResponseCallback(this.ResponseCallback)));
            goto Label_06DC;
        Label_05F9:
            if (this.mReqID != 500)
            {
                goto Label_063D;
            }
            base.ExecRequest(new ReqBtlMultiTwReq(this.mStartingQuest.iname, num2, num4, num3, list.ToArray(), new Network.ResponseCallback(this.ResponseCallback)));
            goto Label_06DC;
        Label_063D:
            if (this.mReqID != 700)
            {
                goto Label_0697;
            }
            num12 = (GlobalVars.VersusCpu == null) ? 1 : GlobalVars.VersusCpu.Get().Deck;
            base.ExecRequest(new ReqVersusCpu(this.mStartingQuest.iname, num12, new Network.ResponseCallback(this.ResponseCallback)));
            goto Label_06DC;
        Label_0697:
            base.ExecRequest(new ReqBtlComReq(this.mStartingQuest.iname, GlobalVars.SelectedFriendID, GlobalVars.SelectedSupport.Get(), new Network.ResponseCallback(this.ResponseCallback), flag, num2, flag2, num4, num3, GlobalVars.Location, GlobalVars.SelectedRankingQuestParam));
        Label_06DC:
            goto Label_06EF;
        Label_06E1:
            base.StartCoroutine(this.StartScene(null));
        Label_06EF:
            goto Label_0837;
        Label_06F4:
            if (pinID != 20)
            {
                goto Label_071D;
            }
            if (manager.AudienceRoom == null)
            {
                goto Label_0837;
            }
            base.StartCoroutine(this.StartAudience());
            goto Label_0837;
        Label_071D:
            if (pinID != 30)
            {
                goto Label_0837;
            }
            if (Network.IsError == null)
            {
                goto Label_0741;
            }
            base.ActivateOutputLinks(300);
            Network.ResetError();
            return;
        Label_0741:
            if (Network.IsStreamConnecting != null)
            {
                goto Label_075D;
            }
            Network.ResetError();
            base.ActivateOutputLinks(300);
            return;
        Label_075D:
            manager2 = manager.AudienceManager;
            manager2.AddStartQuest();
            if (manager2.GetStartedParam() == null)
            {
                goto Label_0802;
            }
            if (manager2.GetStartedParam().btlinfo == null)
            {
                goto Label_07C8;
            }
            battle = new BattleCore.Json_Battle();
            battle.btlinfo = manager2.GetStartedParam().btlinfo;
            CriticalSection.Enter(4);
            manager.AudienceMode = 1;
            base.StartCoroutine(this.StartScene(battle));
            goto Label_07FD;
        Label_07C8:
            if (manager2.IsRetryError == null)
            {
                goto Label_07F4;
            }
            DebugUtility.LogError("Not Exist btlInfo");
            Network.Abort();
            base.ActivateOutputLinks(300);
            goto Label_07FD;
        Label_07F4:
            base.ActivateOutputLinks(9);
        Label_07FD:
            goto Label_0837;
        Label_0802:
            if (manager2.IsRetryError == null)
            {
                goto Label_082E;
            }
            DebugUtility.LogError("Not Exist StartParam");
            Network.Abort();
            base.ActivateOutputLinks(300);
            goto Label_0837;
        Label_082E:
            base.ActivateOutputLinks(9);
        Label_0837:
            return;
        }

        public override void OnBack()
        {
            CriticalSection.Leave(4);
            base.OnBack();
            return;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            return;
        }

        public void OnMismatchVersion()
        {
            base.set_enabled(0);
            CriticalSection.Leave(4);
            Network.RemoveAPI();
            Network.ResetError();
            base.ActivateOutputLinks(3);
            return;
        }

        public void OnMultiMaintenance()
        {
            base.set_enabled(0);
            CriticalSection.Leave(4);
            Network.RemoveAPI();
            base.ActivateOutputLinks(4);
            return;
        }

        private void OnSceneAwake(GameObject scene)
        {
            SceneBattle battle;
            battle = scene.GetComponent<SceneBattle>();
            if ((battle != null) == null)
            {
                goto Label_0056;
            }
            CriticalSection.Leave(4);
            CriticalSection.Leave(4);
            SceneAwakeObserver.RemoveListener(new SceneAwakeObserver.SceneEvent(this.OnSceneAwake));
            battle.StartQuest(this.mStartingQuest.iname, this.mQuestData);
            base.set_enabled(0);
            base.ActivateOutputLinks(1);
        Label_0056:
            return;
        }

        private void OnSceneLoad(GameObject sceneRoot)
        {
            SceneAwakeObserver.RemoveListener(new SceneAwakeObserver.SceneEvent(this.OnSceneLoad));
            CriticalSection.Leave(4);
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            string str;
            WebAPI.JSON_BodyResponse<BattleCore.Json_Battle> response;
            Network.EErrCode code;
            if (Network.IsError == null)
            {
                goto Label_020A;
            }
            code = Network.ErrCode;
            switch ((code - 0x2710))
            {
                case 0:
                    goto Label_019E;

                case 1:
                    goto Label_01A5;

                case 2:
                    goto Label_01AC;

                case 3:
                    goto Label_0064;

                case 4:
                    goto Label_0064;

                case 5:
                    goto Label_0064;

                case 6:
                    goto Label_01B3;

                case 7:
                    goto Label_0064;

                case 8:
                    goto Label_0064;

                case 9:
                    goto Label_0064;

                case 10:
                    goto Label_0064;

                case 11:
                    goto Label_01BA;

                case 12:
                    goto Label_0064;

                case 13:
                    goto Label_01C1;

                case 14:
                    goto Label_0064;

                case 15:
                    goto Label_01C8;

                case 0x10:
                    goto Label_01CF;

                case 0x11:
                    goto Label_01D6;
            }
        Label_0064:
            switch ((code - 0xed8))
            {
                case 0:
                    goto Label_0166;

                case 1:
                    goto Label_016D;

                case 2:
                    goto Label_0174;

                case 3:
                    goto Label_017B;

                case 4:
                    goto Label_0182;

                case 5:
                    goto Label_0189;
            }
            switch ((code - 0xca))
            {
                case 0:
                    goto Label_0197;

                case 1:
                    goto Label_0197;

                case 2:
                    goto Label_00A8;

                case 3:
                    goto Label_0197;

                case 4:
                    goto Label_0197;
            }
        Label_00A8:
            switch ((code - 0xce4))
            {
                case 0:
                    goto Label_0143;

                case 1:
                    goto Label_014A;

                case 2:
                    goto Label_00C4;

                case 3:
                    goto Label_0151;
            }
        Label_00C4:
            switch ((code - 0x2ee1))
            {
                case 0:
                    goto Label_01DD;

                case 1:
                    goto Label_00DC;

                case 2:
                    goto Label_01E4;
            }
        Label_00DC:
            if (code == 0x191)
            {
                goto Label_0118;
            }
            if (code == 0xcec)
            {
                goto Label_011F;
            }
            if (code == 0xdac)
            {
                goto Label_0158;
            }
            if (code == 0xe74)
            {
                goto Label_015F;
            }
            if (code == 0xe78)
            {
                goto Label_0190;
            }
            goto Label_0203;
        Label_0118:
            this.OnBack();
            return;
        Label_011F:
            CriticalSection.Leave(4);
            Network.RemoveAPI();
            Network.ResetError();
            base.ActivateOutputLinks(400);
            base.set_enabled(0);
            return;
        Label_0143:
            this.OnBack();
            return;
        Label_014A:
            this.OnBack();
            return;
        Label_0151:
            this.OnBack();
            return;
        Label_0158:
            this.OnFailed();
            return;
        Label_015F:
            this.OnFailed();
            return;
        Label_0166:
            this.OnBack();
            return;
        Label_016D:
            this.OnBack();
            return;
        Label_0174:
            this.OnFailed();
            return;
        Label_017B:
            this.OnBack();
            return;
        Label_0182:
            this.OnFailed();
            return;
        Label_0189:
            this.OnBack();
            return;
        Label_0190:
            this.OnMismatchVersion();
            return;
        Label_0197:
            this.OnMultiMaintenance();
            return;
        Label_019E:
            this.OnFailed();
            return;
        Label_01A5:
            this.OnFailed();
            return;
        Label_01AC:
            this.OnFailed();
            return;
        Label_01B3:
            this.OnFailed();
            return;
        Label_01BA:
            this.OnFailed();
            return;
        Label_01C1:
            this.OnFailed();
            return;
        Label_01C8:
            this.OnFailed();
            return;
        Label_01CF:
            this.OnFailed();
            return;
        Label_01D6:
            this.OnFailed();
            return;
        Label_01DD:
            this.OnFailed();
            return;
        Label_01E4:
            CriticalSection.Leave(4);
            Network.RemoveAPI();
            base.set_enabled(0);
            base.ActivateOutputLinks(600);
            return;
        Label_0203:
            this.OnRetry();
            return;
        Label_020A:
            if (this.mReqID != 30)
            {
                goto Label_0225;
            }
            Network.RemoveAPI();
            base.ActivateOutputLinks(5);
            return;
        Label_0225:
            str = &www.text;
            DebugMenu.Log("API", "StartQuest:" + &www.text);
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<BattleCore.Json_Battle>>(&www.text);
            if (response.body != null)
            {
                goto Label_0267;
            }
            this.OnRetry();
            return;
        Label_0267:
            Network.RemoveAPI();
            if (response.body.is_rehash == null)
            {
                goto Label_02B6;
            }
            GlobalVars.BtlID.Set(response.body.btlid);
            UIUtility.SystemMessage(string.Empty, LocalizedText.Get("sys.FAILED_RESUMEQUEST"), new UIUtility.DialogResultEvent(this.<OnSuccess>m__1AE), null, 0, -1);
            return;
        Label_02B6:
            base.ActivateOutputLinks(5);
            this.SetVersusAudienceParam(str);
            base.StartCoroutine(this.StartScene(response.body));
            return;
        }

        public void OnVersusNoPlayer()
        {
            MyPhoton photon;
            photon = PunMonoSingleton<MyPhoton>.Instance;
            if ((photon != null) == null)
            {
                goto Label_0026;
            }
            if (photon.IsOldestPlayer() == null)
            {
                goto Label_0026;
            }
            photon.OpenRoom(1, 0);
        Label_0026:
            base.set_enabled(0);
            CriticalSection.Leave(4);
            Network.RemoveAPI();
            base.ActivateOutputLinks(2);
            return;
        }

        public PlayerPartyTypes QuestToPartyIndex(QuestTypes type)
        {
            PlayerPartyTypes types;
            QuestTypes types2;
            types2 = type;
            switch (types2)
            {
                case 0:
                    goto Label_008B;

                case 1:
                    goto Label_0058;

                case 2:
                    goto Label_005F;

                case 3:
                    goto Label_008B;

                case 4:
                    goto Label_0051;

                case 5:
                    goto Label_008B;

                case 6:
                    goto Label_0066;

                case 7:
                    goto Label_006D;

                case 8:
                    goto Label_0074;

                case 9:
                    goto Label_0074;

                case 10:
                    goto Label_008B;

                case 11:
                    goto Label_0051;

                case 12:
                    goto Label_008B;

                case 13:
                    goto Label_008B;

                case 14:
                    goto Label_0058;

                case 15:
                    goto Label_0083;

                case 0x10:
                    goto Label_007B;
            }
            goto Label_008B;
        Label_0051:
            types = 1;
            goto Label_0092;
        Label_0058:
            types = 2;
            goto Label_0092;
        Label_005F:
            types = 3;
            goto Label_0092;
        Label_0066:
            types = 5;
            goto Label_0092;
        Label_006D:
            types = 6;
            goto Label_0092;
        Label_0074:
            types = 7;
            goto Label_0092;
        Label_007B:
            types = 10;
            goto Label_0092;
        Label_0083:
            types = 9;
            goto Label_0092;
        Label_008B:
            types = 0;
        Label_0092:
            return types;
        }

        private void SetVersusAudienceParam(string text)
        {
            MyPhoton photon;
            int num;
            StringBuilder builder;
            string str;
            string str2;
            photon = PunMonoSingleton<MyPhoton>.Instance;
            if (photon.IsMultiVersus == null)
            {
                goto Label_00A7;
            }
            if (photon.IsOldestPlayer() == null)
            {
                goto Label_00A1;
            }
            num = text.IndexOf("\"btlinfo\"");
            if (num == -1)
            {
                goto Label_00A1;
            }
            builder = new StringBuilder();
            str = text.Substring(num);
            str2 = photon.GetRoomParam("started");
            if (string.IsNullOrEmpty(str2) != null)
            {
                goto Label_00A1;
            }
            builder.Append(str2);
            builder.Length -= 1;
            builder.Append(",");
            builder.Append(str);
            builder.Length -= 1;
            photon.SetRoomParam("started", builder.ToString());
        Label_00A1:
            photon.BattleStartRoom();
        Label_00A7:
            return;
        }

        [DebuggerHidden]
        private IEnumerator StartAudience()
        {
            <StartAudience>c__IteratorBF rbf;
            rbf = new <StartAudience>c__IteratorBF();
            rbf.<>f__this = this;
            return rbf;
        }

        [DebuggerHidden]
        protected IEnumerator StartScene(BattleCore.Json_Battle json)
        {
            <StartScene>c__IteratorBE rbe;
            rbe = new <StartScene>c__IteratorBE();
            rbe.json = json;
            rbe.<$>json = json;
            rbe.<>f__this = this;
            return rbe;
        }

        [CompilerGenerated]
        private sealed class <OnActivate>c__AnonStorey270
        {
            internal MyPhoton pt;

            public <OnActivate>c__AnonStorey270()
            {
                base..ctor();
                return;
            }

            internal bool <>m__1AC(JSON_MyPhotonPlayerParam p)
            {
                return ((p.playerIndex == this.pt.MyPlayerIndex) == 0);
            }
        }

        [CompilerGenerated]
        private sealed class <OnActivate>c__AnonStorey271
        {
            internal MyPhoton.MyPlayer player;

            public <OnActivate>c__AnonStorey271()
            {
                base..ctor();
                return;
            }

            internal bool <>m__1AD(JSON_MyPhotonPlayerParam p)
            {
                return ((p.playerID == this.player.playerID) == 0);
            }
        }

        [CompilerGenerated]
        private sealed class <StartAudience>c__IteratorBF : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal GameManager <gm>__0;
            internal VersusAudienceManager <audience>__1;
            internal MyPhoton.MyRoom <room>__2;
            internal bool <IsAbort>__3;
            internal int $PC;
            internal object $current;
            internal FlowNode_StartQuest <>f__this;

            public <StartAudience>c__IteratorBF()
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
                Network.EErrCode code;
                bool flag;
                num = this.$PC;
                this.$PC = -1;
                switch (num)
                {
                    case 0:
                        goto Label_002D;

                    case 1:
                        goto Label_00D4;

                    case 2:
                        goto Label_017F;

                    case 3:
                        goto Label_01AB;

                    case 4:
                        goto Label_02C8;
                }
                goto Label_02CF;
            Label_002D:
                this.<gm>__0 = MonoSingleton<GameManager>.Instance;
                this.<audience>__1 = this.<gm>__0.AudienceManager;
                this.<room>__2 = this.<gm>__0.AudienceRoom;
                this.<IsAbort>__3 = 0;
                if (this.<room>__2 == null)
                {
                    goto Label_029D;
                }
                this.<>f__this.mConnectTime = 0f;
                this.<audience>__1.Reset();
                this.<>f__this.ExecRequest(new ReqVersusAudience(GlobalVars.SelectedMultiPlayPhotonAppID, "1.0_1.81", this.<room>__2.name, null, this.<audience>__1.Logger));
                goto Label_00D4;
            Label_00BD:
                this.$current = new WaitForEndOfFrame();
                this.$PC = 1;
                goto Label_02D1;
            Label_00D4:
                if (Network.IsStreamConnecting == null)
                {
                    goto Label_00BD;
                }
                this.<audience>__1.ResetTime();
                this.<>f__this.mConnectTime = 0f;
                goto Label_017F;
            Label_00FE:
                this.<>f__this.mConnectTime += Time.get_deltaTime();
                if (Network.IsError != null)
                {
                    goto Label_0129;
                }
                if (Network.IsStreamConnecting != null)
                {
                    goto Label_0138;
                }
            Label_0129:
                DebugUtility.LogError("Network Error");
                goto Label_018F;
            Label_0138:
                if (this.<>f__this.mConnectTime < VersusAudienceManager.CONNECTTIME_MAX)
                {
                    goto Label_0168;
                }
                Network.Abort();
                this.<IsAbort>__3 = 1;
                DebugUtility.LogError("Time out");
                goto Label_018F;
            Label_0168:
                this.$current = new WaitForEndOfFrame();
                this.$PC = 2;
                goto Label_02D1;
            Label_017F:
                if (this.<audience>__1.IsConnected == null)
                {
                    goto Label_00FE;
                }
            Label_018F:
                this.$current = new WaitForSeconds(0.5f);
                this.$PC = 3;
                goto Label_02D1;
            Label_01AB:
                if (Network.IsError == null)
                {
                    goto Label_0236;
                }
                code = Network.ErrCode;
                if (code == 0x1324)
                {
                    goto Label_01D6;
                }
                if (code == 0x1325)
                {
                    goto Label_01F7;
                }
                goto Label_021C;
            Label_01D6:
                this.<>f__this.ActivateOutputLinks(8);
                Network.ResetError();
                DebugUtility.LogError("Error:NoRoom");
                goto Label_0231;
            Label_01F7:
                this.<>f__this.ActivateOutputLinks(0x12d);
                Network.ResetError();
                DebugUtility.LogError("Error:NoWatching");
                goto Label_0231;
            Label_021C:
                this.<>f__this.OnFailed();
                Network.ResetError();
            Label_0231:
                goto Label_0298;
            Label_0236:
                if (Network.IsStreamConnecting != null)
                {
                    goto Label_0265;
                }
                this.<>f__this.ActivateOutputLinks(300);
                Network.ResetError();
                DebugUtility.LogError("Error:Disconnect");
                goto Label_0298;
            Label_0265:
                if (this.<IsAbort>__3 == null)
                {
                    goto Label_028B;
                }
                this.<>f__this.ActivateOutputLinks(300);
                Network.ResetError();
                goto Label_0298;
            Label_028B:
                this.<>f__this.ActivateOutputLinks(5);
            Label_0298:
                goto Label_02B5;
            Label_029D:
                this.<>f__this.ActivateOutputLinks(10);
                DebugUtility.LogError("Error:NoParam");
            Label_02B5:
                this.$current = null;
                this.$PC = 4;
                goto Label_02D1;
            Label_02C8:
                this.$PC = -1;
            Label_02CF:
                return 0;
            Label_02D1:
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
        private sealed class <StartScene>c__IteratorBE : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal BattleCore.Json_Battle json;
            internal GameManager <gm>__0;
            internal MultiTowerFloorParam <mtParam>__1;
            internal PlayerPartyTypes <partyType>__2;
            internal List<SupportData> <supports>__3;
            internal BattleCore.Json_BtlOrdeal[] <ordeals>__4;
            internal BattleCore.Json_BtlOrdeal[] <$s_545>__5;
            internal int <$s_546>__6;
            internal BattleCore.Json_BtlOrdeal <ordeal>__7;
            internal SupportData <support>__8;
            internal SupportData <support>__9;
            internal FlowNode_StartQuest.QuestLauncher <ql>__10;
            internal int $PC;
            internal object $current;
            internal BattleCore.Json_Battle <$>json;
            internal FlowNode_StartQuest <>f__this;

            public <StartScene>c__IteratorBE()
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
                bool flag;
                num = this.$PC;
                this.$PC = -1;
                switch (num)
                {
                    case 0:
                        goto Label_0021;

                    case 1:
                        goto Label_0488;
                }
                goto Label_048F;
            Label_0021:
                ProgressWindow.OpenQuestLoadScreen(null, null);
                this.<>f__this.mQuestData = this.json;
                if (this.<>f__this.mQuestData == null)
                {
                    goto Label_030E;
                }
                this.<gm>__0 = MonoSingleton<GameManager>.Instance;
                Debug.Log("SEED:" + ((int) this.<>f__this.mQuestData.btlinfo.seed));
                if (this.<>f__this.mQuestData.btlinfo.multi_floor == null)
                {
                    goto Label_0107;
                }
                this.<mtParam>__1 = this.<gm>__0.GetMTFloorParam(this.<>f__this.mQuestData.btlinfo.qid, this.<>f__this.mQuestData.btlinfo.multi_floor);
                this.<>f__this.mStartingQuest = this.<mtParam>__1.GetQuestParam();
                GlobalVars.SelectedMultiTowerFloor = this.<>f__this.mQuestData.btlinfo.multi_floor;
                goto Label_0131;
            Label_0107:
                this.<>f__this.mStartingQuest = MonoSingleton<GameManager>.Instance.FindQuest(this.<>f__this.mQuestData.btlinfo.qid);
            Label_0131:
                this.<partyType>__2 = this.<>f__this.QuestToPartyIndex(this.<>f__this.mStartingQuest.type);
                GlobalVars.SelectedPartyIndex.Set(this.<partyType>__2);
                MonoSingleton<GameManager>.Instance.Player.SetPartyCurrentIndex(this.<partyType>__2);
                if (this.<>f__this.mStartingQuest.type != 15)
                {
                    goto Label_027F;
                }
                this.<supports>__3 = new List<SupportData>();
                this.<ordeals>__4 = this.json.btlinfo.ordeals;
                this.<$s_545>__5 = this.<ordeals>__4;
                this.<$s_546>__6 = 0;
                goto Label_022A;
            Label_01C7:
                this.<ordeal>__7 = this.<$s_545>__5[this.<$s_546>__6];
                if (this.<ordeal>__7.help == null)
                {
                    goto Label_021C;
                }
                this.<support>__8 = new SupportData();
                this.<support>__8.Deserialize(this.<ordeal>__7.help);
                this.<supports>__3.Add(this.<support>__8);
            Label_021C:
                this.<$s_546>__6 += 1;
            Label_022A:
                if (this.<$s_546>__6 < ((int) this.<$s_545>__5.Length))
                {
                    goto Label_01C7;
                }
                GlobalVars.OrdealSupports = this.<supports>__3;
                if (this.<supports>__3.Count <= 0)
                {
                    goto Label_0274;
                }
                GlobalVars.SelectedFriendID = this.<supports>__3[0].FUID;
                goto Label_027A;
            Label_0274:
                GlobalVars.SelectedFriendID = null;
            Label_027A:
                goto Label_030E;
            Label_027F:
                if (this.json.btlinfo.help != null)
                {
                    goto Label_029F;
                }
                GlobalVars.SelectedFriendID = null;
                goto Label_02B9;
            Label_029F:
                GlobalVars.SelectedFriendID = this.json.btlinfo.help.fuid;
            Label_02B9:
                if (string.IsNullOrEmpty(GlobalVars.SelectedFriendID) != null)
                {
                    goto Label_0303;
                }
                this.<support>__9 = new SupportData();
                this.<support>__9.Deserialize(this.json.btlinfo.help);
                GlobalVars.SelectedSupport.Set(this.<support>__9);
                goto Label_030E;
            Label_0303:
                GlobalVars.SelectedSupport.Set(null);
            Label_030E:
                if (MonoSingleton<GameManager>.Instance.AudienceMode != null)
                {
                    goto Label_033C;
                }
                MonoSingleton<GameManager>.Instance.Player.OnQuestStart(this.<>f__this.mStartingQuest.iname);
            Label_033C:
                if (GlobalVars.SelectedRankingQuestParam == null)
                {
                    goto Label_038B;
                }
                if (GlobalVars.ReqEventPageListType != 3)
                {
                    goto Label_038B;
                }
                if ((GlobalVars.SelectedRankingQuestParam.iname == this.<>f__this.mStartingQuest.iname) == null)
                {
                    goto Label_0380;
                }
                GlobalVars.RankingQuestSelected = 1;
                goto Label_0386;
            Label_0380:
                GlobalVars.RankingQuestSelected = 0;
            Label_0386:
                goto Label_0391;
            Label_038B:
                GlobalVars.RankingQuestSelected = 0;
            Label_0391:
                CriticalSection.Enter(4);
                if (this.<>f__this.SetRestorePoint == null)
                {
                    goto Label_03B7;
                }
                HomeWindow.SetRestorePoint(this.<>f__this.RestorePoint);
            Label_03B7:
                if (this.<>f__this.ReplaceScene == null)
                {
                    goto Label_0447;
                }
                this.<ql>__10 = new FlowNode_StartQuest.QuestLauncher();
                this.<ql>__10.Quest = this.<>f__this.mStartingQuest;
                this.<ql>__10.InitData = this.<>f__this.mQuestData;
                this.<ql>__10.Resume = this.<>f__this.mResume;
                SceneAwakeObserver.AddListener(new SceneAwakeObserver.SceneEvent(this.<ql>__10.OnSceneAwake));
                AssetManager.LoadSceneImmediate("Battle", this.<>f__this.ReplaceScene == 0);
                goto Label_0475;
            Label_0447:
                SceneAwakeObserver.AddListener(new SceneAwakeObserver.SceneEvent(this.<>f__this.OnSceneAwake));
                AssetManager.LoadSceneImmediate("Battle", this.<>f__this.ReplaceScene == 0);
            Label_0475:
                this.$current = null;
                this.$PC = 1;
                goto Label_0491;
            Label_0488:
                this.$PC = -1;
            Label_048F:
                return 0;
            Label_0491:
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

        private class QuestLauncher
        {
            public QuestParam Quest;
            public BattleCore.Json_Battle InitData;
            public bool Resume;

            public QuestLauncher()
            {
                base..ctor();
                return;
            }

            public void OnSceneAwake(GameObject scene)
            {
                SceneBattle battle;
                battle = scene.GetComponent<SceneBattle>();
                if ((battle != null) == null)
                {
                    goto Label_0047;
                }
                CriticalSection.Leave(4);
                CriticalSection.Leave(4);
                SceneAwakeObserver.RemoveListener(new SceneAwakeObserver.SceneEvent(this.OnSceneAwake));
                battle.StartQuest(this.Quest.iname, this.InitData);
            Label_0047:
                return;
            }
        }
    }
}

