namespace SRPG
{
    using GR;
    using Gsc.App;
    using System;
    using System.Collections.Generic;

    [Pin(1, "Start", 0, 0), Pin(10, "Success", 1, 10), NodeType("System/ReqLoginPack", 0x7fe5)]
    public class FlowNode_ReqLoginPack : FlowNode_Network
    {
        public FlowNode_ReqLoginPack()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            if (pinID != 1)
            {
                goto Label_0044;
            }
            if (Network.Mode != null)
            {
                goto Label_003E;
            }
            base.ExecRequest(new ReqLoginPack(new Network.ResponseCallback(this.ResponseCallback), MonoSingleton<GameManager>.Instance.IsRelogin));
            base.set_enabled(1);
            goto Label_0044;
        Label_003E:
            this.Success();
        Label_0044:
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<JSON_ReqLoginPackResponse> response;
            GameManager manager;
            if (Network.IsError == null)
            {
                goto Label_0011;
            }
            this.OnRetry();
            return;
        Label_0011:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<JSON_ReqLoginPackResponse>>(&www.text);
            if (response.body != null)
            {
                goto Label_0030;
            }
            this.OnRetry();
            return;
        Label_0030:
            manager = MonoSingleton<GameManager>.Instance;
            manager.Player.ResetQuestChallenges();
            manager.ResetJigenQuests();
            if (manager.Deserialize(response.body.quests) != null)
            {
                goto Label_0064;
            }
            this.OnFailed();
            return;
        Label_0064:
            if (manager.IsRelogin == null)
            {
                goto Label_009B;
            }
            manager.Player.DeleteTrophies(response.body.trophyprogs);
            manager.Player.DeleteTrophies(response.body.bingoprogs);
        Label_009B:
            LoginNewsInfo.SetPubInfo(response.body.pubinfo);
            this.reflectTrophyProgs(response.body.trophyprogs);
            this.reflectTrophyProgs(response.body.bingoprogs);
            this.reflectLoginTrophyProgs();
            if (response.body.channel == null)
            {
                goto Label_00F8;
            }
            GlobalVars.CurrentChatChannel.Set(response.body.channel);
        Label_00F8:
            if (response.body.support == null)
            {
                goto Label_011D;
            }
            GlobalVars.SelectedSupportUnitUniqueID.Set(response.body.support);
        Label_011D:
            if (string.IsNullOrEmpty(response.body.device_id) != null)
            {
                goto Label_0148;
            }
            BootLoader.GetAccountManager().SetDeviceId(null, response.body.device_id);
        Label_0148:
            if (response.body.is_pending != 1)
            {
                goto Label_016D;
            }
            FlowNode_Variable.Set("REDRAW_GACHA_PENDING", "1");
            goto Label_017C;
        Label_016D:
            FlowNode_Variable.Set("REDRAW_GACHA_PENDING", "0");
        Label_017C:
            FlowNode_Variable.Set("SHOW_CHAPTER", "0");
            Network.RemoveAPI();
            manager.Player.OnLogin();
            manager.IsRelogin = 0;
            this.Success();
            return;
        }

        private void reflectLoginTrophyProgs()
        {
            GameManager manager;
            PlayerData data;
            int num;
            TrophyObjective[] objectiveArray;
            int num2;
            TrophyState state;
            manager = MonoSingleton<GameManager>.Instance;
            data = manager.Player;
            num = data.LoginCount;
            objectiveArray = manager.MasterParam.GetTrophiesOfType(0x17);
            if (objectiveArray != null)
            {
                goto Label_0029;
            }
            return;
        Label_0029:
            num2 = 0;
            goto Label_0091;
        Label_0031:
            if (objectiveArray[num2] != null)
            {
                goto Label_003F;
            }
            goto Label_008B;
        Label_003F:
            state = data.GetTrophyCounter(objectiveArray[num2].Param, 0);
            if (state == null)
            {
                goto Label_008B;
            }
            if (((state.Count == null) | (((int) state.Count.Length) < 1)) == null)
            {
                goto Label_0079;
            }
            goto Label_008B;
        Label_0079:
            state.Count[0] = num;
            state.IsDirty = 1;
        Label_008B:
            num2 += 1;
        Label_0091:
            if (num2 < ((int) objectiveArray.Length))
            {
                goto Label_0031;
            }
            return;
        }

        private void reflectTrophyProgs(JSON_TrophyProgress[] trophy_progs)
        {
            Dictionary<int, List<JSON_TrophyProgress>> dictionary;
            GameManager manager;
            int num;
            JSON_TrophyProgress progress;
            TrophyParam param;
            int num2;
            if (trophy_progs != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            dictionary = new Dictionary<int, List<JSON_TrophyProgress>>();
            manager = MonoSingleton<GameManager>.Instance;
            num = 0;
            goto Label_00D1;
        Label_001A:
            progress = trophy_progs[num];
            if (progress != null)
            {
                goto Label_0029;
            }
            goto Label_00CD;
        Label_0029:
            param = manager.MasterParam.GetTrophy(progress.iname);
            if (param != null)
            {
                goto Label_005D;
            }
            DebugUtility.LogWarning("存在しないミッション:" + progress.iname);
            goto Label_00CD;
        Label_005D:
            if (TrophyConditionTypesEx.IsExtraClear(param.Objectives[0].type) == null)
            {
                goto Label_00AF;
            }
            num2 = param.Objectives[0].type;
            if (dictionary.ContainsKey(num2) != null)
            {
                goto Label_009F;
            }
            dictionary[num2] = new List<JSON_TrophyProgress>();
        Label_009F:
            dictionary[num2].Add(trophy_progs[num]);
        Label_00AF:
            manager.Player.RegistTrophyStateDictByProg(manager.MasterParam.GetTrophy(progress.iname), progress);
        Label_00CD:
            num += 1;
        Label_00D1:
            if (num < ((int) trophy_progs.Length))
            {
                goto Label_001A;
            }
            manager.Player.CreateInheritingExtraTrophy(dictionary);
            return;
        }

        private void Success()
        {
            base.set_enabled(0);
            base.ActivateOutputLinks(10);
            return;
        }

        public class JSON_ReqLoginPackResponse
        {
            public JSON_QuestProgress[] quests;
            public JSON_TrophyProgress[] trophyprogs;
            public JSON_TrophyProgress[] bingoprogs;
            public Json_ChatChannelMasterParam[] channels;
            public int channel;
            public long support;
            public string device_id;
            public int is_pending;
            public LoginNewsInfo.JSON_PubInfo pubinfo;

            public JSON_ReqLoginPackResponse()
            {
                base..ctor();
                return;
            }
        }
    }
}

