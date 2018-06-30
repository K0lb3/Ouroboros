namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;

    [Pin(1, "Request(SelectPageIndex)", 0, 1), NodeType("System/ReqChatChannelList", 0x7fe5), Pin(0, "Request", 0, 0), Pin(10, "Success", 1, 10)]
    public class FlowNode_ReqChanelList : FlowNode_Network
    {
        public int ChannelLimit;

        public FlowNode_ReqChanelList()
        {
            this.ChannelLimit = 20;
            base..ctor();
            return;
        }

        public override unsafe void OnActivate(int pinID)
        {
            GameManager manager;
            ChatChannelMasterParam[] paramArray;
            List<int> list;
            int num;
            int num2;
            int num3;
            int num4;
            int num5;
            if (pinID < 0)
            {
                goto Label_0111;
            }
            paramArray = MonoSingleton<GameManager>.Instance.GetChatChannelMaster();
            list = new List<int>();
            num = 0;
            num2 = GlobalVars.CurrentChatChannel;
            if (pinID != null)
            {
                goto Label_006C;
            }
            num = ((num2 % this.ChannelLimit) != null) ? (num2 / this.ChannelLimit) : ((num2 - 1) / this.ChannelLimit);
            FlowNode_Variable.Set("SelectChannelPage", &num.ToString());
            goto Label_007C;
        Label_006C:
            num = int.Parse(FlowNode_Variable.Get("SelectChannelPage"));
        Label_007C:
            num3 = this.ChannelLimit * num;
            num3 = (num3 <= ((int) paramArray.Length)) ? num3 : 0;
            num4 = this.ChannelLimit * (num + 1);
            num4 = (num4 <= ((int) paramArray.Length)) ? num4 : ((int) paramArray.Length);
            num5 = num3;
            goto Label_00E4;
        Label_00C5:
            if (num5 >= ((int) paramArray.Length))
            {
                goto Label_00DE;
            }
            list.Add(paramArray[num5].id);
        Label_00DE:
            num5 += 1;
        Label_00E4:
            if (num5 < num4)
            {
                goto Label_00C5;
            }
            base.ExecRequest(new ReqChatChannelList(list.ToArray(), new Network.ResponseCallback(this.ResponseCallback)));
            base.set_enabled(1);
        Label_0111:
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<JSON_ChatChannel> response;
            ChatChannel channel;
            ChatChannelWindow window;
            Network.EErrCode code;
            if (Network.IsError == null)
            {
                goto Label_0011;
            }
            code = Network.ErrCode;
            return;
        Label_0011:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<JSON_ChatChannel>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            Network.RemoveAPI();
            channel = new ChatChannel();
            channel.Deserialize(response.body);
            if (channel != null)
            {
                goto Label_004D;
            }
            return;
        Label_004D:
            window = base.get_gameObject().GetComponent<ChatChannelWindow>();
            if ((window != null) == null)
            {
                goto Label_006C;
            }
            window.Channel = channel;
        Label_006C:
            this.Success();
            return;
        }

        private void Success()
        {
            base.set_enabled(0);
            base.ActivateOutputLinks(10);
            return;
        }
    }
}

