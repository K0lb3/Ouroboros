namespace SRPG
{
    using GR;
    using System;

    [NodeType("System/ReqCoinConvert", 0x7fe5), Pin(10, "次に進む", 1, 10), Pin(4, "コインはコンバートされなかった", 1, 4), Pin(3, "旧コインのコンバートが実行された", 1, 3), Pin(2, "新コインのコンバートが実行された", 1, 2), Pin(1, "新旧コインのコンバートが実行された", 1, 1), Pin(0, "Request", 0, 0)]
    public class FlowNode_ReqCoinConvert : FlowNode_Network
    {
        private const int CONVERTED_NEW_OLD = 1;
        private const int CONVERTED_NEW = 2;
        private const int CONVERTED_OLD = 3;
        private const int DO_NOTHING = 4;
        private const int NEXT = 10;

        public FlowNode_ReqCoinConvert()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            if (pinID != null)
            {
                goto Label_001D;
            }
            base.ExecRequest(new ReqCoinConvert(new Network.ResponseCallback(this.ResponseCallback)));
        Label_001D:
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<JSON_ConvertedCoin> response;
            JSON_ConvertedCoin coin;
            GlobalVars.SummonCoinInfo info;
            GlobalVars.SummonCoinInfo info2;
            if (Network.IsError == null)
            {
                goto Label_0011;
            }
            this.OnRetry();
            return;
        Label_0011:
            Network.RemoveAPI();
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<JSON_ConvertedCoin>>(&www.text);
            if (response != null)
            {
                goto Label_0032;
            }
            base.ActivateOutputLinks(1);
            return;
        Label_0032:
            coin = response.body;
            info = new GlobalVars.SummonCoinInfo();
            info.ConvertedSummonCoin = coin.oldcoin.coin;
            info.ReceivedStone = coin.oldcoin.stone;
            info.ConvertedDate = (long) coin.oldcoin.convdate;
            GlobalVars.OldSummonCoinInfo = info;
            info2 = new GlobalVars.SummonCoinInfo();
            info2.ConvertedSummonCoin = coin.newcoin.coin;
            info2.ReceivedStone = coin.newcoin.stone;
            info2.SummonCoinStock = coin.newcoin.stock;
            info2.Period = (long) coin.newcoin.period;
            info2.ConvertedDate = (long) coin.newcoin.convdate;
            GlobalVars.NewSummonCoinInfo = info2;
            if (coin.item == null)
            {
                goto Label_010A;
            }
            if (((int) coin.item.Length) <= 0)
            {
                goto Label_010A;
            }
            MonoSingleton<GameManager>.Instance.Player.Deserialize(coin.item);
        Label_010A:
            if (info.ConvertedSummonCoin <= 0)
            {
                goto Label_012F;
            }
            if (info2.ConvertedSummonCoin <= 0)
            {
                goto Label_012F;
            }
            base.ActivateOutputLinks(1);
            goto Label_0169;
        Label_012F:
            if (info.ConvertedSummonCoin <= 0)
            {
                goto Label_0148;
            }
            base.ActivateOutputLinks(3);
            goto Label_0169;
        Label_0148:
            if (info2.ConvertedSummonCoin <= 0)
            {
                goto Label_0161;
            }
            base.ActivateOutputLinks(2);
            goto Label_0169;
        Label_0161:
            base.ActivateOutputLinks(4);
        Label_0169:
            base.ActivateOutputLinks(10);
            return;
        }

        private class JSON_ConvertedCoin
        {
            public FlowNode_ReqCoinConvert.JSON_OldCoin oldcoin;
            public FlowNode_ReqCoinConvert.JSON_NewCoin newcoin;
            public Json_Item[] item;

            public JSON_ConvertedCoin()
            {
                base..ctor();
                return;
            }
        }

        private class JSON_NewCoin
        {
            public int coin;
            public int stone;
            public int stock;
            public int period;
            public int convdate;

            public JSON_NewCoin()
            {
                base..ctor();
                return;
            }
        }

        private class JSON_OldCoin
        {
            public int coin;
            public int stone;
            public int convdate;

            public JSON_OldCoin()
            {
                base..ctor();
                return;
            }
        }
    }
}

