namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    [Pin(0, "Request", 0, 0), Pin(11, "Period", 1, 11), NodeType("System/RequestEventShopItems", 0x7fe5), Pin(1, "Success", 1, 1)]
    public class FlowNode_RequestEventShopItems : FlowNode_Network
    {
        public const string ErrorWindowPrefabPath = "e/UI/NetworkErrorWindowEx";

        public FlowNode_RequestEventShopItems()
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
            base.ExecRequest(new ReqEventShopItemList(GlobalVars.EventShopItem.shops.gname, new Network.ResponseCallback(this.ResponseCallback)));
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
            WebAPI.JSON_BodyResponse<Json_EventShopResponse> response;
            List<JSON_EventShopItemListSet> list;
            EventShopData data;
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
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_EventShopResponse>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            if (response.body != null)
            {
                goto Label_005E;
            }
            this.OnRetry();
            return;
        Label_005E:
            list = new List<JSON_EventShopItemListSet>(response.body.shopitems);
            response.body.shopitems = list.ToArray();
            Network.RemoveAPI();
            data = MonoSingleton<GameManager>.Instance.Player.GetEventShopData();
            if (data != null)
            {
                goto Label_00A1;
            }
            data = new EventShopData();
        Label_00A1:
            if (data.Deserialize(response.body) != null)
            {
                goto Label_00B9;
            }
            this.OnFailed();
            return;
        Label_00B9:
            MonoSingleton<GameManager>.Instance.Player.SetEventShopData(data);
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

