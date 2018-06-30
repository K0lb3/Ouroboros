namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;

    [NodeType("System/ReqReadTips", 0x7fe5), Pin(0, "TIPS既読", 0, 0), Pin(10, "成功", 1, 10)]
    public class FlowNode_ReqReadTips : FlowNode_Network
    {
        private const int PIN_ID_REQUEST = 0;
        private const int PIN_ID_SUCCESS = 10;

        public FlowNode_ReqReadTips()
        {
            base..ctor();
            return;
        }

        public override unsafe void OnActivate(int pinID)
        {
            string str;
            string str2;
            string str3;
            int num;
            if (pinID == null)
            {
                goto Label_000D;
            }
            goto Label_006F;
        Label_000D:
            str = GlobalVars.LastReadTips;
            if (MonoSingleton<GameManager>.Instance.Tips.Contains(str) == null)
            {
                goto Label_0032;
            }
            base.ActivateOutputLinks(10);
            return;
        Label_0032:
            MonoSingleton<GameManager>.Instance.Player.OnReadTips(str);
            MonoSingleton<GameManager>.Instance.ServerSyncTrophyExecStart(&str2, &str3);
            base.ExecRequest(new ReqReadTips(str, str2, str3, new Network.ResponseCallback(this.ResponseCallback)));
        Label_006F:
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<Json_ReturnTips> response;
            List<string> list;
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_ReturnTips>>(&www.text);
            if (response.body.tips == null)
            {
                goto Label_004F;
            }
            list = MonoSingleton<GameManager>.Instance.Tips;
            if (list.Contains(response.body.tips) != null)
            {
                goto Label_004F;
            }
            list.Add(response.body.tips);
        Label_004F:
            MonoSingleton<GameManager>.Instance.ServerSyncTrophyExecEnd(www);
            base.ActivateOutputLinks(10);
            Network.RemoveAPI();
            return;
        }
    }
}

