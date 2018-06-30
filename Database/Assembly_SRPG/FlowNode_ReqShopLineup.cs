namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;

    [Pin(0, "Request", 0, 0), NodeType("System/ReqShopLineup", 0x7fe5), Pin(1, "Success", 1, 1)]
    public class FlowNode_ReqShopLineup : FlowNode_Network
    {
        private EShopType mShopType;
        [SerializeField]
        private GameObject Target;

        public FlowNode_ReqShopLineup()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            string str;
            if (pinID != null)
            {
                goto Label_008F;
            }
            if (base.get_enabled() == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            if (GlobalVars.ShopType != 9)
            {
                goto Label_0033;
            }
            str = GlobalVars.EventShopItem.shops.gname;
            goto Label_0070;
        Label_0033:
            if (GlobalVars.ShopType != 10)
            {
                goto Label_0054;
            }
            str = GlobalVars.LimitedShopItem.shops.gname;
            goto Label_0070;
        Label_0054:
            this.mShopType = GlobalVars.ShopType;
            str = ((EShopType) this.mShopType).ToString();
        Label_0070:
            base.ExecRequest(new ReqShopLineup(str, new Network.ResponseCallback(this.ResponseCallback)));
            base.set_enabled(1);
        Label_008F:
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<Json_ShopLineup> response;
            ShopLineupWindow window;
            Network.EErrCode code;
            if (Network.IsError == null)
            {
                goto Label_0017;
            }
            code = Network.ErrCode;
            this.OnRetry();
            return;
        Label_0017:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_ShopLineup>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            if (response.body != null)
            {
                goto Label_0047;
            }
            this.OnRetry();
            return;
        Label_0047:
            Network.RemoveAPI();
            if ((this.Target == null) == null)
            {
                goto Label_005E;
            }
            return;
        Label_005E:
            window = this.Target.GetComponent<ShopLineupWindow>();
            if ((window == null) == null)
            {
                goto Label_0077;
            }
            return;
        Label_0077:
            window.SetItemInames(response.body.shopitems);
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

