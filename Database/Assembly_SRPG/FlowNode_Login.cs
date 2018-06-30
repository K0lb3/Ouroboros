namespace SRPG
{
    using GR;
    using System;
    using System.Linq;

    [Pin(6, "指定の日時までログイン不可", 1, 0x10), Pin(4, "Reset to Title", 1, 13), Pin(3, "Success To ReqBtlCom", 1, 12), Pin(2, "Success To SetName", 1, 11), Pin(1, "Success To PlayNew", 1, 10), Pin(10, "Start", 0, 0), NodeType("System/Login", 0x7fe5), Pin(5, "無期限ログイン不可", 1, 15)]
    public class FlowNode_Login : FlowNode_Network
    {
        public FlowNode_Login()
        {
            base..ctor();
            return;
        }

        private void Failure()
        {
            base.set_enabled(0);
            base.ActivateOutputLinks(4);
            return;
        }

        public override void OnActivate(int pinID)
        {
            if (pinID != 10)
            {
                goto Label_0084;
            }
            if (Network.Mode != null)
            {
                goto Label_007E;
            }
            if (MonoSingleton<GameManager>.Instance.IsRelogin == null)
            {
                goto Label_003D;
            }
            base.ExecRequest(new ReqReLogin(new Network.ResponseCallback(this.ResponseCallback)));
            goto Label_0072;
        Label_003D:
            MonoSingleton<GameManager>.Instance.Player.ClearUnits();
            MonoSingleton<GameManager>.Instance.Player.ClearItems();
            base.ExecRequest(new ReqLogin(new Network.ResponseCallback(this.ResponseCallback)));
        Label_0072:
            base.set_enabled(1);
            goto Label_0084;
        Label_007E:
            this.Success();
        Label_0084:
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<Json_PlayerDataAll> response;
            int num;
            GameManager manager;
            long num2;
            Exception exception;
            Exception exception2;
            PlayerData data;
            if (Network.IsError == null)
            {
                goto Label_0011;
            }
            this.OnFailed();
            return;
        Label_0011:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            Network.RemoveAPI();
            if (response.body != null)
            {
                goto Label_0078;
            }
            base.set_enabled(0);
            PlayerPrefsUtility.SetInt(PlayerPrefsUtility.HOME_LASTACCESS_PLAYER_LV, 0, 0);
            PlayerPrefsUtility.SetInt(PlayerPrefsUtility.HOME_LASTACCESS_VIP_LV, 0, 0);
            MonoSingleton<GameManager>.Instance.Player.ClearTrophies();
            base.ActivateOutputLinks(1);
            return;
        Label_0078:
            GlobalVars.CustomerID = response.body.cuid;
            num = response.body.status;
            if (num == null)
            {
                goto Label_00DF;
            }
            GlobalVars.BanStatus = response.body.status;
            if (num != 1)
            {
                goto Label_00BE;
            }
            base.ActivateOutputLinks(5);
            goto Label_00D7;
        Label_00BE:
            if (response.body.status <= 1)
            {
                goto Label_00D7;
            }
            base.ActivateOutputLinks(6);
        Label_00D7:
            base.set_enabled(0);
            return;
        Label_00DF:
            manager = MonoSingleton<GameManager>.Instance;
            num2 = Network.LastConnectionTime;
            manager.Player.LoginDate = TimeManager.FromUnixTime(num2);
            manager.Player.TutorialFlags = response.body.tut;
            if (manager.IsRelogin == null)
            {
                goto Label_026F;
            }
        Label_011D:
            try
            {
                if (response.body.player == null)
                {
                    goto Label_013E;
                }
                manager.Deserialize(response.body.player);
            Label_013E:
                if (response.body.items == null)
                {
                    goto Label_015F;
                }
                manager.Deserialize(response.body.items);
            Label_015F:
                if (response.body.units == null)
                {
                    goto Label_0180;
                }
                manager.Deserialize(response.body.units);
            Label_0180:
                if (response.body.parties == null)
                {
                    goto Label_01A1;
                }
                manager.Deserialize(response.body.parties);
            Label_01A1:
                if (response.body.notify == null)
                {
                    goto Label_01C2;
                }
                manager.Deserialize(response.body.notify);
            Label_01C2:
                if (response.body.artifacts == null)
                {
                    goto Label_01E4;
                }
                manager.Deserialize(response.body.artifacts, 0);
            Label_01E4:
                if (response.body.skins == null)
                {
                    goto Label_0205;
                }
                manager.Deserialize(response.body.skins);
            Label_0205:
                if (response.body.vs == null)
                {
                    goto Label_0226;
                }
                manager.Deserialize(response.body.vs);
            Label_0226:
                if (response.body.tips == null)
                {
                    goto Label_024C;
                }
                manager.Tips = Enumerable.ToList<string>(response.body.tips);
            Label_024C:
                goto Label_026A;
            }
            catch (Exception exception1)
            {
            Label_0251:
                exception = exception1;
                DebugUtility.LogException(exception);
                this.Failure();
                goto Label_0442;
            }
        Label_026A:
            goto Label_033C;
        Label_026F:
            try
            {
                manager.Deserialize(response.body.player);
                manager.Deserialize(response.body.items);
                manager.Deserialize(response.body.units);
                manager.Deserialize(response.body.parties);
                manager.Deserialize(response.body.notify);
                manager.Deserialize(response.body.artifacts, 0);
                manager.Deserialize(response.body.skins);
                manager.Deserialize(response.body.vs);
                if (response.body.tips == null)
                {
                    goto Label_031E;
                }
                manager.Tips = Enumerable.ToList<string>(response.body.tips);
            Label_031E:
                goto Label_033C;
            }
            catch (Exception exception3)
            {
            Label_0323:
                exception2 = exception3;
                DebugUtility.LogException(exception2);
                this.Failure();
                goto Label_0442;
            }
        Label_033C:
            base.set_enabled(0);
            GlobalVars.BtlID.Set(response.body.player.btlid);
            if (string.IsNullOrEmpty(response.body.player.btltype) != null)
            {
                goto Label_0391;
            }
            GlobalVars.QuestType = QuestParam.ToQuestType(response.body.player.btltype);
        Label_0391:
            GameUtility.Config_OkyakusamaCode = manager.Player.OkyakusamaCode;
            if (PlayerPrefsUtility.HasKey(PlayerPrefsUtility.HOME_LASTACCESS_PLAYER_LV) != null)
            {
                goto Label_03CB;
            }
            PlayerPrefsUtility.SetInt(PlayerPrefsUtility.HOME_LASTACCESS_PLAYER_LV, MonoSingleton<GameManager>.Instance.Player.Lv, 0);
        Label_03CB:
            if (PlayerPrefsUtility.HasKey(PlayerPrefsUtility.HOME_LASTACCESS_VIP_LV) != null)
            {
                goto Label_03F5;
            }
            PlayerPrefsUtility.SetInt(PlayerPrefsUtility.HOME_LASTACCESS_VIP_LV, MonoSingleton<GameManager>.Instance.Player.VipRank, 0);
        Label_03F5:
            manager.PostLogin();
            data = MonoSingleton<GameManager>.Instance.Player;
            if (data == null)
            {
                goto Label_041A;
            }
            MyGrowthPush.registCustomerId(data.OkyakusamaCode);
        Label_041A:
            base.ActivateOutputLinks((string.IsNullOrEmpty(response.body.player.name) == null) ? 3 : 2);
        Label_0442:
            return;
        }

        private void Success()
        {
            base.set_enabled(0);
            base.ActivateOutputLinks(3);
            return;
        }
    }
}

