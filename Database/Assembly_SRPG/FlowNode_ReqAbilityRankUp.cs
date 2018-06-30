namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;

    [Pin(1, "Success", 1, 1), NodeType("System/ReqAbilityRankUp", 0x7fe5), Pin(0, "Request", 0, 0)]
    public class FlowNode_ReqAbilityRankUp : FlowNode_Network
    {
        public FlowNode_ReqAbilityRankUp()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            Dictionary<long, int> dictionary;
            if (pinID != null)
            {
                goto Label_006A;
            }
            if (Network.Mode != null)
            {
                goto Label_0064;
            }
            dictionary = GlobalVars.AbilitiesRankUp;
            if (dictionary.Count >= 1)
            {
                goto Label_0034;
            }
            base.set_enabled(0);
            this.Success();
            goto Label_005F;
        Label_0034:
            base.set_enabled(1);
            base.ExecRequest(new ReqAbilityRankUp(dictionary, new Network.ResponseCallback(this.ResponseCallback), null, null));
            GlobalVars.AbilitiesRankUp.Clear();
        Label_005F:
            goto Label_006A;
        Label_0064:
            this.Success();
        Label_006A:
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
            if (Network.ErrCode == 0x898)
            {
                goto Label_0020;
            }
            goto Label_0027;
        Label_0020:
            this.OnBack();
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
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.units);
                goto Label_00A4;
            }
            catch (Exception exception1)
            {
            Label_008D:
                exception = exception1;
                DebugUtility.LogException(exception);
                this.OnRetry();
                goto Label_00AF;
            }
        Label_00A4:
            Network.RemoveAPI();
            this.Success();
        Label_00AF:
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

