namespace SRPG
{
    using GR;
    using System;

    [Pin(0x1770, "MultiResumeMaintenance", 1, 0x1770), NodeType("Multi/MultiPlayResume", 0x7fe5), Pin(200, "ResumeMulti", 0, 0), Pin(0xc9, "ResumeMultiQuest", 0, 1), Pin(0xca, "ResumeVersus", 0, 2), Pin(0xcb, "ResumeMultiTower", 0, 3), Pin(300, "Success", 1, 4), Pin(0x12d, "Failure", 1, 5), Pin(0x1388, "NoMatchVersion", 1, 0x1388), Pin(0x1b58, "ClosedRoom", 1, 0x1b58), Pin(0x1f40, "NotResume", 1, 0x1f40)]
    public class FlowNode_MultiPlayResume : FlowNode_StartQuest
    {
        public static Json_BtlInfo_Multi BtlInfo;
        public RESUME_TYPE mType;

        static FlowNode_MultiPlayResume()
        {
        }

        public FlowNode_MultiPlayResume()
        {
            base..ctor();
            return;
        }

        private void Failure()
        {
            DebugUtility.Log("MultiPlay Resume Failure");
            base.set_enabled(0);
            base.ActivateOutputLinks(0x12d);
            return;
        }

        public override void OnActivate(int pinID)
        {
            GameManager manager;
            MyPhoton photon;
            MyPhoton.MyRoom room;
            BattleCore.Json_Battle battle;
            if (pinID != 200)
            {
                goto Label_0049;
            }
            if (Network.Mode == null)
            {
                goto Label_001C;
            }
            this.Failure();
            return;
        Label_001C:
            base.set_enabled(1);
            base.ExecRequest(new ReqMultiPlayResume(GlobalVars.BtlID, new Network.ResponseCallback(this.ResponseCallback)));
            goto Label_01EE;
        Label_0049:
            if (pinID != 0xc9)
            {
                goto Label_0121;
            }
            CriticalSection.Enter(4);
            manager = MonoSingleton<GameManager>.Instance;
            base.mStartingQuest = manager.FindQuest(GlobalVars.SelectedQuestID);
            if (string.IsNullOrEmpty(base.QuestID) != null)
            {
                goto Label_0096;
            }
            GlobalVars.SelectedQuestID = base.QuestID;
            GlobalVars.SelectedFriendID = string.Empty;
        Label_0096:
            if (BtlInfo == null)
            {
                goto Label_01EE;
            }
            photon = PunMonoSingleton<MyPhoton>.Instance;
            if ((photon != null) == null)
            {
                goto Label_00ED;
            }
            room = photon.GetCurrentRoom();
            if (room == null)
            {
                goto Label_00D7;
            }
            if (room.battle != null)
            {
                goto Label_00D7;
            }
            this.Failure();
            CriticalSection.Leave(4);
            return;
        Label_00D7:
            photon.IsMultiPlay = 1;
            photon.IsMultiVersus = this.mType == 1;
        Label_00ED:
            battle = new BattleCore.Json_Battle();
            battle.btlinfo = BtlInfo;
            battle.btlid = GlobalVars.BtlID;
            base.StartCoroutine(base.StartScene(battle));
            goto Label_01EE;
        Label_0121:
            if (pinID != 0xca)
            {
                goto Label_016A;
            }
            if (Network.Mode == null)
            {
                goto Label_013D;
            }
            this.Failure();
            return;
        Label_013D:
            base.set_enabled(1);
            base.ExecRequest(new ReqVersusResume(GlobalVars.BtlID, new Network.ResponseCallback(this.ResponseCallback)));
            goto Label_01EE;
        Label_016A:
            if (pinID != 0xcb)
            {
                goto Label_01E7;
            }
            if (Network.Mode == null)
            {
                goto Label_0186;
            }
            this.Failure();
            return;
        Label_0186:
            if (GlobalVars.BtlID == null)
            {
                goto Label_01CE;
            }
            if (GlobalVars.QuestType != 12)
            {
                goto Label_01CE;
            }
            base.set_enabled(1);
            base.ExecRequest(new ReqMultiTwResume(GlobalVars.BtlID, new Network.ResponseCallback(this.ResponseCallback)));
            goto Label_01E2;
        Label_01CE:
            base.set_enabled(0);
            base.ActivateOutputLinks(0x1f40);
            return;
        Label_01E2:
            goto Label_01EE;
        Label_01E7:
            base.OnActivate(pinID);
        Label_01EE:
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<ReqMultiPlayResume.Response> response;
            WebAPI.JSON_BodyResponse<ReqVersusResume.Response> response2;
            GameManager manager;
            WebAPI.JSON_BodyResponse<ReqMultiTwResume.Response> response3;
            MultiTowerFloorParam param;
            DebugUtility.Log("OnSuccess");
            if (Network.IsError == null)
            {
                goto Label_00E8;
            }
            if (Network.ErrCode != 0xe78)
            {
                goto Label_0041;
            }
            Network.RemoveAPI();
            Network.ResetError();
            base.set_enabled(0);
            base.ActivateOutputLinks(0x1388);
            return;
        Label_0041:
            if (Network.ErrCode == 0xca)
            {
                goto Label_007D;
            }
            if (Network.ErrCode == 0xcb)
            {
                goto Label_007D;
            }
            if (Network.ErrCode == 0xcd)
            {
                goto Label_007D;
            }
            if (Network.ErrCode != 0xce)
            {
                goto Label_0096;
            }
        Label_007D:
            Network.RemoveAPI();
            base.set_enabled(0);
            base.ActivateOutputLinks(0x1770);
            return;
        Label_0096:
            if (Network.ErrCode == 0x1324)
            {
                goto Label_00C3;
            }
            if (Network.ErrCode == 0x271c)
            {
                goto Label_00C3;
            }
            if (Network.ErrCode != 0x2ee2)
            {
                goto Label_00E1;
            }
        Label_00C3:
            Network.RemoveAPI();
            Network.ResetError();
            base.set_enabled(0);
            base.ActivateOutputLinks(0x1b58);
            return;
        Label_00E1:
            this.OnFailed();
            return;
        Label_00E8:
            if (this.mType != null)
            {
                goto Label_01A1;
            }
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqMultiPlayResume.Response>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            if (response.body != null)
            {
                goto Label_0123;
            }
            this.OnFailed();
            return;
        Label_0123:
            GlobalVars.SelectedQuestID = response.body.quest.iname;
            GlobalVars.SelectedMultiPlayPhotonAppID = response.body.app_id;
            GlobalVars.SelectedMultiPlayRoomName = response.body.token;
            GlobalVars.ResumeMultiplayPlayerID = int.Parse(response.body.btlinfo.plid);
            GlobalVars.ResumeMultiplaySeatID = int.Parse(response.body.btlinfo.seat);
            BtlInfo = response.body.btlinfo;
            goto Label_0414;
        Label_01A1:
            if (this.mType != 1)
            {
                goto Label_02F1;
            }
            response2 = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqVersusResume.Response>>(&www.text);
            DebugUtility.Assert((response2 == null) == 0, "res == null");
            if (response2.body != null)
            {
                goto Label_01DD;
            }
            this.OnFailed();
            return;
        Label_01DD:
            GlobalVars.SelectedQuestID = response2.body.quest.iname;
            GlobalVars.SelectedMultiPlayPhotonAppID = response2.body.app_id;
            GlobalVars.SelectedMultiPlayRoomName = response2.body.token;
            GlobalVars.ResumeMultiplayPlayerID = int.Parse(response2.body.btlinfo.plid);
            GlobalVars.ResumeMultiplaySeatID = int.Parse(response2.body.btlinfo.seat);
            if (string.Compare(response2.body.type, ((VERSUS_TYPE) 0).ToString().ToLower()) != null)
            {
                goto Label_0276;
            }
            GlobalVars.SelectedMultiPlayVersusType = 0;
            goto Label_02DC;
        Label_0276:
            if (string.Compare(response2.body.type, ((VERSUS_TYPE) 1).ToString().ToLower()) != null)
            {
                goto Label_02B1;
            }
            MonoSingleton<GameManager>.Instance.VersusTowerMatchBegin = 1;
            GlobalVars.SelectedMultiPlayVersusType = 1;
            goto Label_02DC;
        Label_02B1:
            if (string.Compare(response2.body.type, ((VERSUS_TYPE) 2).ToString().ToLower()) != null)
            {
                goto Label_02DC;
            }
            GlobalVars.SelectedMultiPlayVersusType = 2;
        Label_02DC:
            BtlInfo = response2.body.btlinfo;
            goto Label_0414;
        Label_02F1:
            if (this.mType != 2)
            {
                goto Label_0414;
            }
            manager = MonoSingleton<GameManager>.Instance;
            response3 = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqMultiTwResume.Response>>(&www.text);
            DebugUtility.Assert((response3 == null) == 0, "res == null");
            if (response3.body != null)
            {
                goto Label_0333;
            }
            this.OnFailed();
            return;
        Label_0333:
            GlobalVars.SelectedMultiTowerID = response3.body.btlinfo.qid;
            GlobalVars.SelectedMultiTowerFloor = response3.body.btlinfo.floor;
            GlobalVars.SelectedMultiPlayPhotonAppID = response3.body.app_id;
            GlobalVars.SelectedMultiPlayRoomName = response3.body.token;
            GlobalVars.ResumeMultiplayPlayerID = int.Parse(response3.body.btlinfo.plid);
            GlobalVars.ResumeMultiplaySeatID = int.Parse(response3.body.btlinfo.seat);
            GlobalVars.SelectedMultiPlayRoomID = response3.body.btlinfo.roomid;
            param = manager.GetMTFloorParam(GlobalVars.SelectedMultiTowerID, GlobalVars.SelectedMultiTowerFloor);
            if (param == null)
            {
                goto Label_03F0;
            }
            GlobalVars.SelectedQuestID = param.GetQuestParam().iname;
        Label_03F0:
            BtlInfo = response3.body.btlinfo;
            BtlInfo.multi_floor = BtlInfo.floor;
        Label_0414:
            Network.RemoveAPI();
            base.set_enabled(0);
            base.ActivateOutputLinks(300);
            return;
        }

        public enum RESUME_TYPE
        {
            MULTI,
            VERSUS,
            TOWER
        }
    }
}

