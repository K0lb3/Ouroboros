namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;

    [Pin(1, "Request", 0, 0), Pin(100, "Success", 1, 10), NodeType("System/SellPiece", 0x7fe5)]
    public class FlowNode_SellPiece : FlowNode_Network
    {
        public FlowNode_SellPiece()
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
            if (pinID != 1)
            {
                goto Label_0129;
            }
            if (base.get_enabled() == null)
            {
                goto Label_0013;
            }
            return;
        Label_0013:
            data = MonoSingleton<GameManager>.Instance.Player;
            if (Network.Mode != 1)
            {
                goto Label_00B2;
            }
            num = 0;
            goto Label_008D;
        Label_0030:
            item = GlobalVars.ConvertAwakePieceList[num];
            data.GainPiecePoint(item.item.RarityParam.PieceToPoint * item.num);
            data.GainItem(item.item.Param.iname, -item.num);
            item.num = 0;
            item.index = -1;
            num += 1;
        Label_008D:
            if (num < GlobalVars.ConvertAwakePieceList.Count)
            {
                goto Label_0030;
            }
            GlobalVars.ConvertAwakePieceList.Clear();
            this.Success();
            goto Label_0129;
        Label_00B2:
            dictionary = new Dictionary<long, int>();
            list = GlobalVars.ConvertAwakePieceList;
            num2 = 0;
            goto Label_00FC;
        Label_00C7:
            num3 = list[num2].item.UniqueID;
            num4 = list[num2].num;
            dictionary[num3] = num4;
            num2 += 1;
        Label_00FC:
            if (num2 < list.Count)
            {
                goto Label_00C7;
            }
            base.ExecRequest(new ReqSellPiece(dictionary, new Network.ResponseCallback(this.ResponseCallback)));
            base.set_enabled(1);
        Label_0129:
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
            if (Network.ErrCode == 0xaf0)
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
        Label_004C:
            try
            {
                if (response.body != null)
                {
                    goto Label_005D;
                }
                throw new InvalidJSONException();
            Label_005D:
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.player);
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.items);
                goto Label_009E;
            }
            catch (Exception exception1)
            {
            Label_008C:
                exception = exception1;
                base.OnRetry(exception);
                goto Label_00B3;
            }
        Label_009E:
            Network.RemoveAPI();
            GlobalVars.ConvertAwakePieceList.Clear();
            this.Success();
        Label_00B3:
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

