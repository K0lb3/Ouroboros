namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;

    [Pin(0, "Request", 0, 0), Pin(1, "Success", 1, 1), NodeType("System/ReqArtifactEnforce", 0x7fe5)]
    public class FlowNode_ReqArtifactEnforce : FlowNode_Network
    {
        public FlowNode_ReqArtifactEnforce()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            long num;
            Dictionary<string, int> dictionary;
            if (pinID != null)
            {
                goto Label_0072;
            }
            num = GlobalVars.SelectedArtifactUniqueID;
            dictionary = GlobalVars.UsedArtifactExpItems;
            if (Network.Mode != null)
            {
                goto Label_006C;
            }
            if (num < 1L)
            {
                goto Label_0035;
            }
            if (dictionary.Count >= 1)
            {
                goto Label_0047;
            }
        Label_0035:
            base.set_enabled(0);
            this.Success();
            goto Label_0067;
        Label_0047:
            base.set_enabled(1);
            base.ExecRequest(new ReqArtifactEnforce(num, dictionary, new Network.ResponseCallback(this.ResponseCallback)));
        Label_0067:
            goto Label_0072;
        Label_006C:
            this.Success();
        Label_0072:
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
            if (Network.ErrCode == 0x232a)
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
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.items);
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.artifacts, 0);
                goto Label_00BA;
            }
            catch (Exception exception1)
            {
            Label_00A3:
                exception = exception1;
                DebugUtility.LogException(exception);
                this.OnRetry();
                goto Label_00C5;
            }
        Label_00BA:
            Network.RemoveAPI();
            this.Success();
        Label_00C5:
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

