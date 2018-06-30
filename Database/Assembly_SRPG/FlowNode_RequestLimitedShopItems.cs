namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;

    [NodeType("System/RequestLimitedShopItems", 0x7fe5), Pin(0, "Request", 0, 0), Pin(1, "Success", 1, 1), Pin(11, "Period", 1, 11)]
    public class FlowNode_RequestLimitedShopItems : FlowNode_Network
    {
        public const string ErrorWindowPrefabPath = "e/UI/NetworkErrorWindowEx";

        public FlowNode_RequestLimitedShopItems()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            if (pinID != null)
            {
                goto Label_0054;
            }
            if (base.get_enabled() == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            if (Network.Mode != null)
            {
                goto Label_004E;
            }
            base.ExecRequest(new ReqLimitedShopItemList(GlobalVars.LimitedShopItem.shops.gname, new Network.ResponseCallback(this.ResponseCallback)));
            base.set_enabled(1);
            goto Label_0054;
        Label_004E:
            this.Success();
        Label_0054:
            return;
        }

        private void OnPeriod()
        {
            this.Period();
            Network.RemoveAPI();
            Network.ResetError();
            base.set_enabled(0);
            base.ActivateOutputLinks(11);
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<Json_LimitedShopResponse> response;
            LimitedShopData data;
            Network.EErrCode code;
            if (Network.IsError == null)
            {
                goto Label_002E;
            }
            if (Network.ErrCode == 0x1133)
            {
                goto Label_0020;
            }
            goto Label_0027;
        Label_0020:
            this.OnPeriod();
            return;
        Label_0027:
            this.OnRetry();
            return;
        Label_002E:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_LimitedShopResponse>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            if (response.body != null)
            {
                goto Label_005E;
            }
            this.OnRetry();
            return;
        Label_005E:
            Network.RemoveAPI();
            data = MonoSingleton<GameManager>.Instance.Player.GetLimitedShopData();
            if (data != null)
            {
                goto Label_007F;
            }
            data = new LimitedShopData();
        Label_007F:
            if (data.Deserialize(response.body) != null)
            {
                goto Label_0097;
            }
            this.OnFailed();
            return;
        Label_0097:
            MonoSingleton<GameManager>.Instance.Player.SetLimitedShopData(data);
            this.Success();
            return;
        }

        private void Period()
        {
            NetworkErrorWindowEx ex;
            if (Network.IsImmediateMode == null)
            {
                goto Label_000B;
            }
            return;
        Label_000B:
            Object.Instantiate<NetworkErrorWindowEx>(Resources.Load<NetworkErrorWindowEx>("e/UI/NetworkErrorWindowEx")).Body = Network.ErrMsg;
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

