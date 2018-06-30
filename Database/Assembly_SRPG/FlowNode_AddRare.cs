namespace SRPG
{
    using GR;
    using System;

    [Pin(0, "Request", 0, 0), Pin(1, "Success", 1, 1), NodeType("System/AddRare", 0x7fe5)]
    public class FlowNode_AddRare : FlowNode_Network
    {
        public FlowNode_AddRare()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            long num;
            if (pinID != null)
            {
                goto Label_0044;
            }
            if (Network.Mode != 1)
            {
                goto Label_0018;
            }
            this.Success();
            return;
        Label_0018:
            num = GlobalVars.SelectedUnitUniqueID;
            base.ExecRequest(new ReqUnitRare(num, new Network.ResponseCallback(this.ResponseCallback), null, null));
            base.set_enabled(1);
        Label_0044:
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<Json_PlayerDataAll> response;
            Exception exception;
            Network.EErrCode code;
            if (Network.IsError == null)
            {
                goto Label_002E;
            }
            if (Network.ErrCode == 0x7d0)
            {
                goto Label_0020;
            }
            goto Label_0027;
        Label_0020:
            this.OnFailed();
            return;
        Label_0027:
            this.OnRetry();
            return;
        Label_002E:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            if (response.body != null)
            {
                goto Label_005E;
            }
            this.OnRetry();
            return;
        Label_005E:
            try
            {
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.player);
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.items);
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.units);
                goto Label_00B9;
            }
            catch (Exception exception1)
            {
            Label_00A2:
                exception = exception1;
                DebugUtility.LogException(exception);
                this.OnRetry();
                goto Label_00C4;
            }
        Label_00B9:
            Network.RemoveAPI();
            this.Success();
        Label_00C4:
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

