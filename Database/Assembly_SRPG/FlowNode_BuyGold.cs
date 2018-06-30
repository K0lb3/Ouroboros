namespace SRPG
{
    using GR;
    using System;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    [NodeType("System/BuyGold", 0x7fe5), Pin(3, "購入回数制限", 1, 3), Pin(2, "コインが足りない", 1, 2), Pin(1, "Success", 1, 1), Pin(0, "Request", 0, 0)]
    public class FlowNode_BuyGold : FlowNode_Network
    {
        public static GameObject ConfirmBoxObj;
        public bool Confirm;
        public bool ShowResult;
        [CompilerGenerated]
        private static UIUtility.DialogResultEvent <>f__am$cache3;
        [CompilerGenerated]
        private static UIUtility.DialogResultEvent <>f__am$cache4;
        [CompilerGenerated]
        private static UIUtility.DialogResultEvent <>f__am$cache5;

        static FlowNode_BuyGold()
        {
        }

        public FlowNode_BuyGold()
        {
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private static void <OnSuccess>m__186(GameObject go)
        {
        }

        [CompilerGenerated]
        private static void <OutOfBuyCount>m__185(GameObject go)
        {
        }

        [CompilerGenerated]
        private static void <OutOfCoin>m__184(GameObject go)
        {
        }

        private void Failure()
        {
            base.set_enabled(0);
            base.ActivateOutputLinks(3);
            return;
        }

        private int getRequiredCoin()
        {
            FixParam param;
            return MonoSingleton<GameManager>.Instance.MasterParam.FixParam.BuyGoldCost;
        }

        public override void OnActivate(int pinID)
        {
            object[] objArray1;
            FixParam param;
            string str;
            if (pinID != null)
            {
                goto Label_00C0;
            }
            if (base.get_enabled() == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            if (this.Confirm == null)
            {
                goto Label_00BA;
            }
            param = MonoSingleton<GameManager>.Instance.MasterParam.FixParam;
            objArray1 = new object[] { (OInt) param.BuyGoldCost, (OInt) param.BuyGoldAmount, (int) MonoSingleton<GameManager>.Instance.Player.PaidCoin, (int) (MonoSingleton<GameManager>.Instance.Player.FreeCoin + MonoSingleton<GameManager>.Instance.Player.ComCoin) };
            ConfirmBoxObj = UIUtility.ConfirmBox(LocalizedText.Get("sys.BUYGOLD", objArray1), new UIUtility.DialogResultEvent(this.OnBuy), null, null, 0, -1, null, null);
            goto Label_00C0;
        Label_00BA:
            this.SendRequest();
        Label_00C0:
            return;
        }

        private void OnBuy(GameObject go)
        {
            this.SendRequest();
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            object[] objArray1;
            WebAPI.JSON_BodyResponse<Json_PlayerDataAll> response;
            PlayerData.EDeserializeFlags flags;
            FixParam param;
            Network.EErrCode code;
            if (Network.IsError == null)
            {
                goto Label_0062;
            }
            code = Network.ErrCode;
            if (code == 0x1130)
            {
                goto Label_002B;
            }
            if (code == 0x1131)
            {
                goto Label_0043;
            }
            goto Label_005B;
        Label_002B:
            if (this.ShowResult == null)
            {
                goto Label_003C;
            }
            this.OutOfCoin();
        Label_003C:
            this.OnBack();
            return;
        Label_0043:
            if (this.ShowResult == null)
            {
                goto Label_0054;
            }
            this.OutOfBuyCount();
        Label_0054:
            this.OnBack();
            return;
        Label_005B:
            this.OnRetry();
            return;
        Label_0062:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            if (response.body != null)
            {
                goto Label_0092;
            }
            this.OnRetry();
            return;
        Label_0092:
            flags = 0;
            flags |= 2;
            flags |= 1;
            if (MonoSingleton<GameManager>.Instance.Player.Deserialize(response.body.player, flags) != null)
            {
                goto Label_00C3;
            }
            this.OnRetry();
            return;
        Label_00C3:
            Network.RemoveAPI();
            MyMetaps.TrackSpendCoin("BuyGold", this.getRequiredCoin());
            if (this.ShowResult == null)
            {
                goto Label_0161;
            }
            param = MonoSingleton<GameManager>.Instance.MasterParam.FixParam;
            objArray1 = new object[] { (OInt) param.BuyGoldCost, (OInt) param.BuyGoldAmount };
            if (<>f__am$cache5 != null)
            {
                goto Label_0139;
            }
            <>f__am$cache5 = new UIUtility.DialogResultEvent(FlowNode_BuyGold.<OnSuccess>m__186);
        Label_0139:
            UIUtility.SystemMessage(null, LocalizedText.Get("sys.GOLDBOUGHT", objArray1), <>f__am$cache5, null, 0, -1);
            MonoSingleton<GameManager>.Instance.Player.OnGoldChange(param.BuyGoldAmount);
        Label_0161:
            this.Success();
            return;
        }

        private void OutOfBuyCount()
        {
            if (<>f__am$cache4 != null)
            {
                goto Label_0023;
            }
            <>f__am$cache4 = new UIUtility.DialogResultEvent(FlowNode_BuyGold.<OutOfBuyCount>m__185);
        Label_0023:
            UIUtility.NegativeSystemMessage(null, LocalizedText.Get("sys.GOLD_BUY_LIMIT"), <>f__am$cache4, null, 0, -1);
            return;
        }

        private void OutOfCoin()
        {
            object[] objArray1;
            FixParam param;
            param = MonoSingleton<GameManager>.Instance.MasterParam.FixParam;
            objArray1 = new object[] { (OInt) param.BuyGoldCost, (OInt) param.BuyGoldAmount };
            if (<>f__am$cache3 != null)
            {
                goto Label_0055;
            }
            <>f__am$cache3 = new UIUtility.DialogResultEvent(FlowNode_BuyGold.<OutOfCoin>m__184);
        Label_0055:
            UIUtility.NegativeSystemMessage(null, LocalizedText.Get("sys.OUTOFFREECOIN", objArray1), <>f__am$cache3, null, 0, -1);
            return;
        }

        private void SendRequest()
        {
            int num;
            GameManager manager;
            int num2;
            int num3;
            num = MonoSingleton<GameManager>.Instance.Player.FreeCoin + MonoSingleton<GameManager>.Instance.Player.ComCoin;
            if (num >= this.getRequiredCoin())
            {
                goto Label_0046;
            }
            if (this.ShowResult == null)
            {
                goto Label_003D;
            }
            this.OutOfCoin();
        Label_003D:
            base.ActivateOutputLinks(2);
            return;
        Label_0046:
            manager = MonoSingleton<GameManager>.Instance;
            if (manager.MasterParam.GetVipBuyGoldLimit(manager.Player.VipRank) > manager.Player.GoldBuyNum)
            {
                goto Label_0095;
            }
            if (this.ShowResult == null)
            {
                goto Label_0085;
            }
            this.OutOfBuyCount();
        Label_0085:
            base.set_enabled(0);
            base.ActivateOutputLinks(3);
            return;
        Label_0095:
            num3 = this.getRequiredCoin();
            base.set_enabled(1);
            base.ExecRequest(new ReqBuyGold(num3, new Network.ResponseCallback(this.ResponseCallback)));
            return;
        }

        private void Success()
        {
            GameManager manager;
            MonoSingleton<GameManager>.Instance.Player.OnBuyGold();
            manager = MonoSingleton<GameManager>.Instance;
            manager.OnAbilityRankUpCountChange(manager.Player.AbilityRankUpCountNum);
            base.set_enabled(0);
            base.ActivateOutputLinks(1);
            return;
        }
    }
}

