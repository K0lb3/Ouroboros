namespace SRPG
{
    using GR;
    using System;

    [Pin(0, "Request", 0, 0), Pin(1, "Success", 1, 1), Pin(0x70a, "NoMatchVersion", 1, 0x70a), Pin(0x70b, "MultiMaintenance", 1, 0x70b), NodeType("System/UpdateParty", 0x7fe5), Pin(0x708, "NoUnit", 1, 0x708), Pin(0x709, "Illegal", 1, 0x709)]
    public class FlowNode_UpdateParty : FlowNode_Network
    {
        public bool SetCurrent;

        public FlowNode_UpdateParty()
        {
            this.SetCurrent = 1;
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            PlayerData data;
            PartyData data2;
            int num;
            bool flag;
            int num2;
            long num3;
            bool flag2;
            bool flag3;
            MyPhoton photon;
            GameUtility.EScene scene;
            if (pinID != null)
            {
                goto Label_0146;
            }
            data = MonoSingleton<GameManager>.Instance.Player;
            data.AutoSetLeaderUnit();
            if (this.SetCurrent == null)
            {
                goto Label_0032;
            }
            data.SetPartyCurrentIndex(GlobalVars.SelectedPartyIndex);
        Label_0032:
            data2 = MonoSingleton<GameManager>.Instance.Player.GetPartyCurrent();
            num = 0;
            goto Label_00A0;
        Label_0049:
            flag = 0;
            num2 = 0;
            goto Label_007C;
        Label_0053:
            if (data.Partys[num].GetUnitUniqueID(num2) == null)
            {
                goto Label_0076;
            }
            flag = 1;
            goto Label_0089;
        Label_0076:
            num2 += 1;
        Label_007C:
            if (num2 < data2.MAX_UNIT)
            {
                goto Label_0053;
            }
        Label_0089:
            if (flag != null)
            {
                goto Label_009C;
            }
            base.ActivateOutputLinks(0x708);
            return;
        Label_009C:
            num += 1;
        Label_00A0:
            if (num < data.Partys.Count)
            {
                goto Label_0049;
            }
            if (Network.Mode != 1)
            {
                goto Label_00C3;
            }
            this.Success();
            return;
        Label_00C3:
            flag2 = 0;
            flag3 = 0;
            photon = PunMonoSingleton<MyPhoton>.Instance;
            scene = GameUtility.GetCurrentScene();
            if ((photon != null) == null)
            {
                goto Label_0123;
            }
            if (scene == 4)
            {
                goto Label_00F8;
            }
            if (photon.IsResume() == null)
            {
                goto Label_0106;
            }
        Label_00F8:
            flag2 = photon.IsOldestPlayer();
            goto Label_0123;
        Label_0106:
            if (scene == 6)
            {
                goto Label_011A;
            }
            if (photon.IsResume() == null)
            {
                goto Label_0123;
            }
        Label_011A:
            flag3 = photon.IsOldestPlayer();
        Label_0123:
            base.set_enabled(1);
            base.ExecRequest(new ReqParty(new Network.ResponseCallback(this.ResponseCallback), flag2, 1, flag3));
        Label_0146:
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<Json_PlayerDataAll> response;
            Exception exception;
            if (Network.IsError == null)
            {
                goto Label_00E6;
            }
            if (Network.ErrCode != 0x708)
            {
                goto Label_0037;
            }
            Network.RemoveAPI();
            Network.ResetError();
            base.set_enabled(0);
            base.ActivateOutputLinks(0x708);
            return;
        Label_0037:
            if (Network.ErrCode != 0x709)
            {
                goto Label_0064;
            }
            Network.RemoveAPI();
            Network.ResetError();
            base.set_enabled(0);
            base.ActivateOutputLinks(0x709);
            return;
        Label_0064:
            if (Network.ErrCode != 0xe78)
            {
                goto Label_0091;
            }
            Network.RemoveAPI();
            Network.ResetError();
            base.set_enabled(0);
            base.ActivateOutputLinks(0x70a);
            return;
        Label_0091:
            if (Network.ErrCode == 0xca)
            {
                goto Label_00CD;
            }
            if (Network.ErrCode == 0xcb)
            {
                goto Label_00CD;
            }
            if (Network.ErrCode == 0xce)
            {
                goto Label_00CD;
            }
            if (Network.ErrCode != 0xcd)
            {
                goto Label_00E6;
            }
        Label_00CD:
            Network.RemoveAPI();
            base.set_enabled(0);
            base.ActivateOutputLinks(0x70b);
            return;
        Label_00E6:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
        Label_0104:
            try
            {
                if (response.body != null)
                {
                    goto Label_0115;
                }
                throw new InvalidJSONException();
            Label_0115:
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.player);
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.parties);
                goto Label_0156;
            }
            catch (Exception exception1)
            {
            Label_0144:
                exception = exception1;
                base.OnRetry(exception);
                goto Label_0161;
            }
        Label_0156:
            Network.RemoveAPI();
            this.Success();
        Label_0161:
            return;
        }

        private void Success()
        {
            base.set_enabled(0);
            base.ActivateOutputLinks(1);
            return;
        }
    }
}

