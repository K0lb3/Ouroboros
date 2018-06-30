namespace SRPG
{
    using GR;
    using System;

    [NodeType("System/RequestShopItems", 0x7fe5), Pin(0, "Request", 0, 0), Pin(1, "Success", 1, 1)]
    public class FlowNode_RequestShopItems : FlowNode_Network
    {
        private EShopType mShopType;

        public FlowNode_RequestShopItems()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            string str;
            if (pinID != null)
            {
                goto Label_008C;
            }
            if (base.get_enabled() == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            if (Network.Mode != null)
            {
                goto Label_0086;
            }
            this.mShopType = GlobalVars.ShopType;
            str = ((EShopType) this.mShopType).ToString();
            if (this.mShopType != 11)
            {
                goto Label_0062;
            }
            base.ExecRequest(new ReqItemGuerrillaShop(str, new Network.ResponseCallback(this.ResponseCallback)));
            goto Label_007A;
        Label_0062:
            base.ExecRequest(new ReqItemShop(str, new Network.ResponseCallback(this.ResponseCallback)));
        Label_007A:
            base.set_enabled(1);
            goto Label_008C;
        Label_0086:
            this.Success();
        Label_008C:
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<Json_ShopResponse> response;
            ShopData data;
            Network.EErrCode code;
            if (Network.IsError == null)
            {
                goto Label_0017;
            }
            code = Network.ErrCode;
            this.OnRetry();
            return;
        Label_0017:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_ShopResponse>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            if (response.body != null)
            {
                goto Label_0047;
            }
            this.OnRetry();
            return;
        Label_0047:
            Network.RemoveAPI();
            data = MonoSingleton<GameManager>.Instance.Player.GetShopData(this.mShopType);
            if (data != null)
            {
                goto Label_006E;
            }
            data = new ShopData();
        Label_006E:
            if (data.Deserialize(response.body) != null)
            {
                goto Label_0086;
            }
            this.OnFailed();
            return;
        Label_0086:
            MonoSingleton<GameManager>.Instance.Player.SetShopData(this.mShopType, data);
            this.Success();
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

