namespace SRPG
{
    using GR;
    using System;

    [Pin(0, "ReqHealAp", 0, 0), NodeType("System/ReqHealAp", 0x7fe5), Pin(1, "Success", 1, 1)]
    public class FlowNode_ReqHealAp : FlowNode_Network
    {
        public FlowNode_ReqHealAp()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            ItemData data;
            HealAp ap;
            if (pinID != null)
            {
                goto Label_0060;
            }
            if (base.get_enabled() == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            data = DataSource.FindDataOfClass<ItemData>(base.get_gameObject(), null);
            ap = base.get_gameObject().GetComponent<HealAp>();
            if (data == null)
            {
                goto Label_0060;
            }
            base.set_enabled(1);
            base.ExecRequest(new ReqHealAp(data.UniqueID, ap.bar.UseItemNum, new Network.ResponseCallback(this.ResponseCallback)));
        Label_0060:
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<Json_ArenaPlayerDataAll> response;
            HealAp ap;
            int num;
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_ArenaPlayerDataAll>>(&www.text);
            if (response.body != null)
            {
                goto Label_001F;
            }
            this.OnRetry();
            return;
        Label_001F:
            Network.RemoveAPI();
            MonoSingleton<GameManager>.Instance.Player.Deserialize(response.body.player);
            MonoSingleton<GameManager>.Instance.Player.Deserialize(response.body.items);
            base.get_gameObject().GetComponent<HealAp>().now_ap.set_text(&MonoSingleton<GameManager>.Instance.Player.Stamina.ToString());
            MonoSingleton<MySound>.Instance.PlaySEOneShot("SE_0011", 0f);
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

