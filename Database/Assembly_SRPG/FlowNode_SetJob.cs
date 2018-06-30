namespace SRPG
{
    using GR;
    using System;

    [Pin(0, "Request", 0, 0), NodeType("System/SetJob", 0x7fe5), Pin(1, "Success", 1, 1)]
    public class FlowNode_SetJob : FlowNode_Network
    {
        public FlowNode_SetJob()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            long num;
            UnitData data;
            JobData data2;
            if (pinID != null)
            {
                goto Label_007B;
            }
            if (Network.Mode != null)
            {
                goto Label_0075;
            }
            num = GlobalVars.SelectedUnitUniqueID;
            data2 = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(num).CurrentJob;
            if (data2 == null)
            {
                goto Label_0044;
            }
            if (data2.IsActivated != null)
            {
                goto Label_004B;
            }
        Label_0044:
            this.Success();
            return;
        Label_004B:
            base.ExecRequest(new ReqUnitJob(num, data2.UniqueID, new Network.ResponseCallback(this.ResponseCallback)));
            base.set_enabled(1);
            goto Label_007B;
        Label_0075:
            this.Success();
        Label_007B:
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<Json_PlayerDataAll> response;
            Exception exception;
            Network.EErrCode code;
            if (Network.IsError == null)
            {
                goto Label_003B;
            }
            switch ((Network.ErrCode - 0x8fc))
            {
                case 0:
                    goto Label_002D;

                case 1:
                    goto Label_002D;

                case 2:
                    goto Label_002D;
            }
            goto Label_0034;
        Label_002D:
            this.OnFailed();
            return;
        Label_0034:
            this.OnRetry();
            return;
        Label_003B:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
        Label_0059:
            try
            {
                if (response.body != null)
                {
                    goto Label_006A;
                }
                throw new InvalidJSONException();
            Label_006A:
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.player);
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.units);
                goto Label_00AB;
            }
            catch (Exception exception1)
            {
            Label_0099:
                exception = exception1;
                base.OnRetry(exception);
                goto Label_00B6;
            }
        Label_00AB:
            Network.RemoveAPI();
            this.Success();
        Label_00B6:
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

