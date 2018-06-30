namespace SRPG
{
    using GR;
    using System;

    [Pin(7, "TowerError", 1, 7), NodeType("System/Quest/TowerStart", 0x7fe5)]
    public class FlowNode_TowerStartQuest : FlowNode_StartQuest
    {
        private long btlID;

        public FlowNode_TowerStartQuest()
        {
            base..ctor();
            return;
        }

        public bool Error()
        {
            Network.EErrCode code;
            if (Network.IsError != null)
            {
                goto Label_000C;
            }
            return 0;
        Label_000C:
            if (Network.ErrCode != 0x2013)
            {
                goto Label_0071;
            }
            if (base.mResume == null)
            {
                goto Label_0069;
            }
            GlobalVars.BtlID.Set(this.btlID);
            CriticalSection.Leave(4);
            Network.RequestResult = 3;
            if (Network.IsImmediateMode == null)
            {
                goto Label_004E;
            }
            return 1;
        Label_004E:
            Network.RemoveAPI();
            Network.ResetError();
            base.set_enabled(0);
            base.ActivateOutputLinks(7);
            return 1;
        Label_0069:
            this.OnFailed();
            return 1;
        Label_0071:
            code = Network.ErrCode;
            switch ((code - 0xce4))
            {
                case 0:
                    goto Label_00AE;

                case 1:
                    goto Label_00B6;

                case 2:
                    goto Label_0093;

                case 3:
                    goto Label_00BE;
            }
        Label_0093:
            if (code == 0xdac)
            {
                goto Label_00C6;
            }
            if (code == 0xe74)
            {
                goto Label_00CE;
            }
            goto Label_00D6;
        Label_00AE:
            this.OnBack();
            return 1;
        Label_00B6:
            this.OnBack();
            return 1;
        Label_00BE:
            this.OnBack();
            return 1;
        Label_00C6:
            this.OnFailed();
            return 1;
        Label_00CE:
            this.OnFailed();
            return 1;
        Label_00D6:
            return TowerErrorHandle.Error(this);
        }

        public override void OnActivate(int pinID)
        {
            GameManager manager;
            MyPhoton photon;
            PlayerPartyTypes types;
            PartyData data;
            TowerFloorParam param;
            MonoSingleton<GameManager>.Instance.AudienceMode = 0;
            manager = MonoSingleton<GameManager>.Instance;
            photon = PunMonoSingleton<MyPhoton>.Instance;
            photon.IsMultiPlay = 0;
            photon.IsMultiVersus = 0;
            manager.IsVSCpuBattle = 0;
            if ((((pinID != null) && (pinID != 100)) && ((pinID != 200) && (pinID != 500))) && ((pinID != 700) && (pinID != 0x3e8)))
            {
                goto Label_00A6;
            }
            photon.IsMultiPlay = ((pinID == 100) || (pinID == 200)) ? 1 : (pinID == 500);
            photon.IsMultiVersus = pinID == 200;
            manager.IsVSCpuBattle = pinID == 700;
        Label_00A6:
            if (pinID != 10)
            {
                goto Label_00B8;
            }
            base.mResume = 1;
            pinID = 0;
        Label_00B8:
            if (pinID != null)
            {
                goto Label_0204;
            }
            if (base.get_enabled() == null)
            {
                goto Label_00CA;
            }
            return;
        Label_00CA:
            base.set_enabled(1);
            CriticalSection.Enter(4);
            types = 6;
            GlobalVars.SelectedPartyIndex.Set(types);
            MonoSingleton<GameManager>.Instance.Player.SetPartyCurrentIndex(types);
            PunMonoSingleton<MyPhoton>.Instance.IsMultiPlay = 0;
            PunMonoSingleton<MyPhoton>.Instance.IsMultiVersus = 0;
            PunMonoSingleton<MyPhoton>.Instance.IsRankMatch = 0;
            if (base.mResume == null)
            {
                goto Label_015E;
            }
            this.btlID = GlobalVars.BtlID;
            GlobalVars.BtlID.Set(0L);
            base.ExecRequest(new ReqTowerBtlComResume(this.btlID, new Network.ResponseCallback(this.ResponseCallback)));
            goto Label_0204;
        Label_015E:
            if (string.IsNullOrEmpty(base.QuestID) != null)
            {
                goto Label_0183;
            }
            GlobalVars.SelectedQuestID = base.QuestID;
            GlobalVars.SelectedFriendID = string.Empty;
        Label_0183:
            base.mStartingQuest = manager.FindQuest(GlobalVars.SelectedQuestID);
            if (base.PlayOffline != null)
            {
                goto Label_0204;
            }
            if (Network.Mode != null)
            {
                goto Label_0204;
            }
            data = manager.Player.FindPartyOfType(types);
            if (base.mStartingQuest.type != 7)
            {
                goto Label_0204;
            }
            param = MonoSingleton<GameManager>.Instance.FindTowerFloor(base.mStartingQuest.iname);
            base.ExecRequest(new ReqBtlTowerComReq(param.tower_id, param.iname, data, new Network.ResponseCallback(this.ResponseCallback)));
        Label_0204:
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            Json_TowerStartQuest quest;
            WebAPI.JSON_BodyResponse<Json_TowerResume> response;
            WebAPI.JSON_BodyResponse<Json_TowerStartQuest> response2;
            BattleCore.Json_Battle battle;
            QuestParam param;
            int num;
            bool flag;
            int num2;
            int num3;
            if (this.Error() == null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            quest = null;
            response = null;
            if (base.mResume == null)
            {
                goto Label_00BE;
            }
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_TowerResume>>(&www.text);
            if (response.body.pdeck == null)
            {
                goto Label_0052;
            }
            MonoSingleton<GameManager>.Instance.TowerResuponse.Deserialize(response.body.pdeck);
        Label_0052:
            if (response.body.edeck == null)
            {
                goto Label_007C;
            }
            MonoSingleton<GameManager>.Instance.TowerResuponse.Deserialize(response.body.edeck);
        Label_007C:
            quest = new Json_TowerStartQuest();
            quest.btlinfo = response.body.btlinfo;
            quest.btlid = response.body.btlid;
            MonoSingleton<GameManager>.Instance.TowerResuponse.round = response.body.round;
        Label_00BE:
            response2 = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_TowerStartQuest>>(&www.text);
            if (response2.body != null)
            {
                goto Label_00DD;
            }
            this.OnRetry();
            return;
        Label_00DD:
            if (quest != null)
            {
                goto Label_00EA;
            }
            quest = response2.body;
        Label_00EA:
            if ((base.mResume == null) || (response != null))
            {
                goto Label_010C;
            }
            quest.btlinfo = response.body.btlinfo;
        Label_010C:
            Network.RemoveAPI();
            battle = new BattleCore.Json_Battle();
            battle.btlid = quest.btlid;
            battle.btlinfo = quest.btlinfo;
            if (battle.btlinfo == null)
            {
                goto Label_0150;
            }
            battle.btlinfo.qid = quest.btlinfo.floor_iname;
        Label_0150:
            param = MonoSingleton<GameManager>.Instance.FindQuest(battle.btlinfo.qid);
            if (param == null)
            {
                goto Label_0215;
            }
            if (response2.body.missions == null)
            {
                goto Label_01C8;
            }
            num = 0;
            goto Label_01B4;
        Label_0186:
            flag = (response2.body.missions[num] != 1) ? 0 : 1;
            param.SetMissionFlag(num, flag);
            num += 1;
        Label_01B4:
            if (num < ((int) response2.body.missions.Length))
            {
                goto Label_0186;
            }
        Label_01C8:
            if (response2.body.missions_val == null)
            {
                goto Label_0215;
            }
            num2 = 0;
            goto Label_0201;
        Label_01E0:
            num3 = response2.body.missions_val[num2];
            param.SetMissionValue(num2, num3);
            num2 += 1;
        Label_0201:
            if (num2 < ((int) response2.body.missions_val.Length))
            {
                goto Label_01E0;
            }
        Label_0215:
            base.StartCoroutine(base.StartScene(battle));
            return;
        }

        private void Start()
        {
        }

        private void Update()
        {
        }

        private class Json_TowerBtlInfo : BattleCore.Json_BtlInfo
        {
            public int manage_id;
            public string tower_iname;
            public string floor_iname;

            public Json_TowerBtlInfo()
            {
                base..ctor();
                return;
            }

            public override RandDeckResult[] GetDeck()
            {
                return base.lot_enemies;
            }
        }

        private class Json_TowerResume
        {
            public long btlid;
            public FlowNode_TowerStartQuest.Json_TowerBtlInfo btlinfo;
            public int status;
            public JSON_ReqTowerResuponse.Json_TowerPlayerUnit[] pdeck;
            public JSON_ReqTowerResuponse.Json_TowerEnemyUnit[] edeck;
            public byte round;
            public int[] missions;
            public int[] missions_val;

            public Json_TowerResume()
            {
                base..ctor();
                return;
            }
        }

        private class Json_TowerStartQuest
        {
            public long btlid;
            public FlowNode_TowerStartQuest.Json_TowerBtlInfo btlinfo;
            public int[] missions;
            public int[] missions_val;

            public Json_TowerStartQuest()
            {
                base..ctor();
                return;
            }
        }
    }
}

