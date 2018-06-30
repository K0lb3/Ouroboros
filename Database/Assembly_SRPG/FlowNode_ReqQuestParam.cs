namespace SRPG
{
    using GR;
    using System;

    [Pin(1, "Success", 1, 1), Pin(2, "Failed", 1, 2), Pin(0, "Request", 0, 0), NodeType("System/ReqQuestParam", 0x7fe5)]
    public class FlowNode_ReqQuestParam : FlowNode_Network
    {
        public FlowNode_ReqQuestParam()
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
            if (pinID != null)
            {
                goto Label_0057;
            }
            if (Network.Mode != null)
            {
                goto Label_0051;
            }
            if (GameUtility.Config_UseAssetBundles.Value != null)
            {
                goto Label_0051;
            }
            if (GameUtility.Config_UseLocalData.Value != null)
            {
                goto Label_0051;
            }
            base.ExecRequest(new ReqQuestParam(new Network.ResponseCallback(this.ResponseCallback)));
            base.set_enabled(1);
            goto Label_0057;
        Label_0051:
            this.Success();
        Label_0057:
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<Json_QuestList> response;
            Exception exception;
            Network.EErrCode code;
            if (Network.IsError == null)
            {
                goto Label_0017;
            }
            code = Network.ErrCode;
            this.OnFailed();
            return;
        Label_0017:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_QuestList>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            Network.RemoveAPI();
        Label_003A:
            try
            {
                MonoSingleton<GameManager>.Instance.Deserialize(response.body);
                goto Label_0066;
            }
            catch (Exception exception1)
            {
            Label_004F:
                exception = exception1;
                DebugUtility.LogException(exception);
                this.Failure();
                goto Label_006C;
            }
        Label_0066:
            this.Success();
        Label_006C:
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

