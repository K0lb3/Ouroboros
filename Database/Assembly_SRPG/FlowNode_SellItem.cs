namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;

    [Pin(1, "Request", 0, 0), NodeType("System/SellItem", 0x7fe5), Pin(100, "Success", 1, 10), Pin(2, "RequestConvert", 0, 1)]
    public class FlowNode_SellItem : FlowNode_Network
    {
        public FlowNode_SellItem()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            PlayerData data;
            int num;
            SellItem item;
            Dictionary<long, int> dictionary;
            List<SellItem> list;
            int num2;
            long num3;
            int num4;
            if (pinID == 1)
            {
                goto Label_000E;
            }
            if (pinID != 2)
            {
                goto Label_0136;
            }
        Label_000E:
            if (base.get_enabled() == null)
            {
                goto Label_001A;
            }
            return;
        Label_001A:
            data = MonoSingleton<GameManager>.Instance.Player;
            if (Network.Mode != 1)
            {
                goto Label_00BB;
            }
            num = 0;
            goto Label_008A;
        Label_0037:
            item = GlobalVars.SellItemList[num];
            data.GainGold(item.item.Sell * item.num);
            data.GainItem(item.item.Param.iname, -item.num);
            item.num = 0;
            item.index = -1;
            num += 1;
        Label_008A:
            if (num < GlobalVars.SellItemList.Count)
            {
                goto Label_0037;
            }
            GlobalVars.SelectSellItem = null;
            GlobalVars.SellItemList.Clear();
            GlobalVars.SellItemList = null;
            this.Success();
            goto Label_0136;
        Label_00BB:
            dictionary = new Dictionary<long, int>();
            list = GlobalVars.SellItemList;
            num2 = 0;
            goto Label_0105;
        Label_00D0:
            num3 = list[num2].item.UniqueID;
            num4 = list[num2].num;
            dictionary[num3] = num4;
            num2 += 1;
        Label_0105:
            if (num2 < list.Count)
            {
                goto Label_00D0;
            }
            base.ExecRequest(new ReqItemSell(dictionary, pinID == 2, new Network.ResponseCallback(this.ResponseCallback)));
            base.set_enabled(1);
        Label_0136:
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<Json_PlayerDataAll> response;
            Exception exception;
            int num;
            int num2;
            Network.EErrCode code;
            if (Network.IsError == null)
            {
                goto Label_0057;
            }
            code = Network.ErrCode;
            if (code == 0xaf0)
            {
                goto Label_002E;
            }
            if (code == 0xaf1)
            {
                goto Label_0035;
            }
            goto Label_0050;
        Label_002E:
            this.OnBack();
            return;
        Label_0035:
            if (GlobalVars.SellItemList == null)
            {
                goto Label_0049;
            }
            GlobalVars.SellItemList.Clear();
        Label_0049:
            this.OnFailed();
            return;
        Label_0050:
            this.OnRetry();
            return;
        Label_0057:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
        Label_0075:
            try
            {
                if (response.body != null)
                {
                    goto Label_0086;
                }
                throw new InvalidJSONException();
            Label_0086:
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.player);
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.items);
                goto Label_00C7;
            }
            catch (Exception exception1)
            {
            Label_00B5:
                exception = exception1;
                base.OnRetry(exception);
                goto Label_012B;
            }
        Label_00C7:
            num = 0;
            if (GlobalVars.SellItemList == null)
            {
                goto Label_0106;
            }
            num2 = 0;
            goto Label_00F6;
        Label_00DA:
            num += GlobalVars.SellItemList[num2].item.Sell;
            num2 += 1;
        Label_00F6:
            if (num2 < GlobalVars.SellItemList.Count)
            {
                goto Label_00DA;
            }
        Label_0106:
            Network.RemoveAPI();
            GlobalVars.SellItemList.Clear();
            MonoSingleton<GameManager>.Instance.Player.OnGoldChange(num);
            this.Success();
        Label_012B:
            return;
        }

        private void Success()
        {
            base.set_enabled(0);
            base.ActivateOutputLinks(100);
            return;
        }
    }
}

