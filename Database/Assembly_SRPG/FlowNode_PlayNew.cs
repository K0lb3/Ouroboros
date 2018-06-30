namespace SRPG
{
    using GR;
    using System;

    [Pin(1, "Success", 1, 10), NodeType("System/PlayNew", 0x7fe5), Pin(2, "Reset to Title", 1, 11), Pin(10, "Start", 0, 0)]
    public class FlowNode_PlayNew : FlowNode_Network
    {
        public bool IsDebug;

        public FlowNode_PlayNew()
        {
            base..ctor();
            return;
        }

        private void Failure()
        {
            base.set_enabled(0);
            base.ActivateOutputLinks(2);
            return;
        }

        public override void OnActivate(int pinID)
        {
            if (pinID != 10)
            {
                goto Label_0041;
            }
            if (Network.Mode != null)
            {
                goto Label_003B;
            }
            base.ExecRequest(new ReqPlayNew(this.IsDebug, new Network.ResponseCallback(this.ResponseCallback)));
            base.set_enabled(1);
            goto Label_0041;
        Label_003B:
            this.Success();
        Label_0041:
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<Json_PlayerDataAll> response;
            GameManager manager;
            Exception exception;
            Network.EErrCode code;
            if (Network.IsError == null)
            {
                goto Label_002E;
            }
            if (Network.ErrCode == 0x514)
            {
                goto Label_0020;
            }
            goto Label_0027;
        Label_0020:
            this.OnRetry();
            return;
        Label_0027:
            this.OnFailed();
            return;
        Label_002E:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            Network.RemoveAPI();
            if (response.body != null)
            {
                goto Label_0063;
            }
            this.Failure();
            return;
        Label_0063:
            manager = MonoSingleton<GameManager>.Instance;
        Label_0069:
            try
            {
                manager.Deserialize(response.body.player);
                manager.Deserialize(response.body.units);
                manager.Deserialize(response.body.items);
                if (manager.Deserialize(response.body.mails) != null)
                {
                    goto Label_00BD;
                }
                this.Failure();
                goto Label_0149;
            Label_00BD:
                manager.Deserialize(response.body.parties);
                manager.Deserialize(response.body.friends);
                manager.Deserialize(response.body.notify);
                manager.Deserialize(response.body.skins);
                goto Label_011D;
            }
            catch (Exception exception1)
            {
            Label_0106:
                exception = exception1;
                DebugUtility.LogException(exception);
                this.Failure();
                goto Label_0149;
            }
        Label_011D:
            GameUtility.Config_OkyakusamaCode = manager.Player.OkyakusamaCode;
            GlobalVars.CustomerID = manager.Player.CUID;
            manager.PostLogin();
            this.Success();
        Label_0149:
            return;
        }

        public void SetDebug(bool check)
        {
            this.IsDebug = check;
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

