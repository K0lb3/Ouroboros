namespace SRPG
{
    using GR;
    using System;

    [Pin(2, "Maintenance", 1, 2), NodeType("Request/ReqBlackList(ブロックリスト取得)", 0x7fe5), Pin(0, "Request", 0, 0), Pin(1, "Success", 1, 1)]
    public class FlowNode_ReqChatBlackList : FlowNode_Network
    {
        public int GetLimit;
        public bool IsGetOnly;

        public FlowNode_ReqChatBlackList()
        {
            this.GetLimit = 10;
            base..ctor();
            return;
        }

        private void ChatMaintenance()
        {
            BlackList list;
            if ((this == null) == null)
            {
                goto Label_000D;
            }
            return;
        Label_000D:
            base.set_enabled(0);
            list = base.get_gameObject().GetComponent<BlackList>();
            if ((list != null) == null)
            {
                goto Label_0037;
            }
            list.RefreshMaintenanceMessage(Network.ErrMsg);
        Label_0037:
            Network.RemoveAPI();
            Network.ResetError();
            base.ActivateOutputLinks(2);
            return;
        }

        public override void OnActivate(int pinID)
        {
            string str;
            int num;
            if (pinID != null)
            {
                goto Label_005B;
            }
            str = FlowNode_Variable.Get("BLACKLIST_OFFSET");
            num = (string.IsNullOrEmpty(str) == null) ? int.Parse(str) : 1;
            base.ExecRequest(new ReqChatBlackList(num, this.GetLimit, new Network.ResponseCallback(this.ResponseCallback)));
            if ((this == null) == null)
            {
                goto Label_0054;
            }
            return;
        Label_0054:
            base.set_enabled(0);
        Label_005B:
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<JSON_ChatBlackList> response;
            ChatBlackList list;
            ChatBlackListParam param;
            ChatBlackListParam[] paramArray;
            int num;
            BlackList list2;
            Network.EErrCode code;
            if (Network.IsError == null)
            {
                goto Label_002A;
            }
            if (Network.ErrCode == 0xc9)
            {
                goto Label_0022;
            }
            goto Label_0029;
        Label_0022:
            this.ChatMaintenance();
            return;
        Label_0029:
            return;
        Label_002A:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<JSON_ChatBlackList>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            Network.RemoveAPI();
            list = new ChatBlackList();
            list.Deserialize(response.body);
            if (list != null)
            {
                goto Label_0066;
            }
            return;
        Label_0066:
            GlobalVars.BlockList.Clear();
            if (this.IsGetOnly == null)
            {
                goto Label_00B4;
            }
            paramArray = list.lists;
            num = 0;
            goto Label_00A5;
        Label_008A:
            param = paramArray[num];
            GlobalVars.BlockList.Add(param.uid);
            num += 1;
        Label_00A5:
            if (num < ((int) paramArray.Length))
            {
                goto Label_008A;
            }
            goto Label_00D6;
        Label_00B4:
            list2 = base.get_gameObject().GetComponent<BlackList>();
            if ((list2 != null) == null)
            {
                goto Label_00D6;
            }
            list2.BList = list;
        Label_00D6:
            this.Success();
            return;
        }

        private void Success()
        {
            if ((this == null) == null)
            {
                goto Label_000D;
            }
            return;
        Label_000D:
            base.set_enabled(0);
            base.ActivateOutputLinks(1);
            return;
        }
    }
}

