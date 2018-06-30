namespace SRPG
{
    using GR;
    using System;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    [Pin(0, "Request", 0, 0), Pin(3, "スタミナ満タン", 1, 3), Pin(1, "Success", 1, 1), Pin(5, "購入回数制限", 1, 5), Pin(4, "コインが足りなかった", 1, 4), NodeType("System/BuyStamina", 0x7fe5)]
    public class FlowNode_BuyStamina : FlowNode_Network
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
        [CompilerGenerated]
        private static UIUtility.DialogResultEvent <>f__am$cache6;

        static FlowNode_BuyStamina()
        {
        }

        public FlowNode_BuyStamina()
        {
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private static void <OnSuccess>m__18F(GameObject go)
        {
        }

        [CompilerGenerated]
        private static void <OutOfBuyCount>m__18E(GameObject go)
        {
        }

        [CompilerGenerated]
        private static void <OutOfCoin>m__18C(GameObject go)
        {
        }

        [CompilerGenerated]
        private static void <StaminaFull>m__18D(GameObject go)
        {
        }

        public override void OnActivate(int pinID)
        {
            object[] objArray1;
            PlayerData data;
            FixParam param;
            string str;
            if (pinID != null)
            {
                goto Label_008E;
            }
            if (base.get_enabled() == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            data = MonoSingleton<GameManager>.Instance.Player;
            if (this.Confirm == null)
            {
                goto Label_0088;
            }
            param = MonoSingleton<GameManager>.Instance.MasterParam.FixParam;
            objArray1 = new object[] { (int) data.GetStaminaRecoveryCost(0), (OInt) param.StaminaAdd };
            ConfirmBoxObj = UIUtility.ConfirmBox(LocalizedText.Get("sys.RESET_STAMINA", objArray1), new UIUtility.DialogResultEvent(this.OnBuy), null, null, 0, -1, null, null);
            goto Label_008E;
        Label_0088:
            this.SendRequest();
        Label_008E:
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
            int num;
            PlayerData.EDeserializeFlags flags;
            FixParam param;
            Network.EErrCode code;
            if (Network.IsError == null)
            {
                goto Label_0041;
            }
            if (Network.ErrCode == 0xb54)
            {
                goto Label_0022;
            }
            goto Label_003A;
        Label_0022:
            if (this.ShowResult == null)
            {
                goto Label_0033;
            }
            this.OutOfCoin();
        Label_0033:
            this.OnBack();
            return;
        Label_003A:
            this.OnRetry();
            return;
        Label_0041:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            if (response.body != null)
            {
                goto Label_0071;
            }
            this.OnRetry();
            return;
        Label_0071:
            num = MonoSingleton<GameManager>.Instance.Player.GetStaminaRecoveryCost(1);
            flags = 0;
            flags |= 2;
            flags |= 4;
            if (MonoSingleton<GameManager>.Instance.Player.Deserialize(response.body.player, flags) != null)
            {
                goto Label_00B3;
            }
            this.OnRetry();
            return;
        Label_00B3:
            Network.RemoveAPI();
            MyMetaps.TrackSpendCoin("BuyStamina", num);
            if (this.ShowResult == null)
            {
                goto Label_0124;
            }
            param = MonoSingleton<GameManager>.Instance.MasterParam.FixParam;
            objArray1 = new object[] { (OInt) param.StaminaAdd };
            if (<>f__am$cache6 != null)
            {
                goto Label_0116;
            }
            <>f__am$cache6 = new UIUtility.DialogResultEvent(FlowNode_BuyStamina.<OnSuccess>m__18F);
        Label_0116:
            UIUtility.SystemMessage(null, LocalizedText.Get("sys.STAMINARECOVERED", objArray1), <>f__am$cache6, null, 0, -1);
        Label_0124:
            this.Success();
            return;
        }

        private void OutOfBuyCount()
        {
            if (<>f__am$cache5 != null)
            {
                goto Label_0023;
            }
            <>f__am$cache5 = new UIUtility.DialogResultEvent(FlowNode_BuyStamina.<OutOfBuyCount>m__18E);
        Label_0023:
            UIUtility.NegativeSystemMessage(null, LocalizedText.Get("sys.STAMINA_BUY_LIMIT"), <>f__am$cache5, null, 0, -1);
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
            <>f__am$cache3 = new UIUtility.DialogResultEvent(FlowNode_BuyStamina.<OutOfCoin>m__18C);
        Label_0055:
            UIUtility.NegativeSystemMessage(null, LocalizedText.Get("sys.OUTOFCOIN", objArray1), <>f__am$cache3, null, 0, -1);
            return;
        }

        private void SendRequest()
        {
            PlayerData data;
            GameManager manager;
            int num;
            data = MonoSingleton<GameManager>.Instance.Player;
            if (data.StaminaStockCap > data.Stamina)
            {
                goto Label_003D;
            }
            if (this.ShowResult == null)
            {
                goto Label_002D;
            }
            this.StaminaFull();
        Label_002D:
            base.set_enabled(0);
            base.ActivateOutputLinks(3);
            return;
        Label_003D:
            if (data.Coin >= data.GetStaminaRecoveryCost(0))
            {
                goto Label_0070;
            }
            if (this.ShowResult == null)
            {
                goto Label_0060;
            }
            this.OutOfCoin();
        Label_0060:
            base.set_enabled(0);
            base.ActivateOutputLinks(4);
            return;
        Label_0070:
            if (MonoSingleton<GameManager>.Instance.MasterParam.GetVipBuyStaminaLimit(data.VipRank) > data.StaminaBuyNum)
            {
                goto Label_00B5;
            }
            if (this.ShowResult == null)
            {
                goto Label_00A5;
            }
            this.OutOfBuyCount();
        Label_00A5:
            base.set_enabled(0);
            base.ActivateOutputLinks(5);
            return;
        Label_00B5:
            if (Network.Mode != null)
            {
                goto Label_00E2;
            }
            base.ExecRequest(new ReqItemAddStmPaid(new Network.ResponseCallback(this.ResponseCallback)));
            base.set_enabled(1);
            goto Label_00FC;
        Label_00E2:
            data.DEBUG_CONSUME_COIN(data.GetStaminaRecoveryCost(0));
            data.DEBUG_REPAIR_STAMINA();
            this.Success();
        Label_00FC:
            return;
        }

        private void StaminaFull()
        {
            if (<>f__am$cache4 != null)
            {
                goto Label_0023;
            }
            <>f__am$cache4 = new UIUtility.DialogResultEvent(FlowNode_BuyStamina.<StaminaFull>m__18D);
        Label_0023:
            UIUtility.SystemMessage(null, LocalizedText.Get("sys.STAMINAFULL"), <>f__am$cache4, null, 0, -1);
            return;
        }

        private void Success()
        {
            base.set_enabled(0);
            MonoSingleton<MySound>.Instance.PlaySEOneShot("SE_0011", 0f);
            base.ActivateOutputLinks(1);
            return;
        }
    }
}

