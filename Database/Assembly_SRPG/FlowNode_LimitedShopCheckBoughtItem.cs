namespace SRPG
{
    using GR;
    using System;
    using System.Linq;
    using System.Runtime.CompilerServices;

    [NodeType("SRPG/LimitedShopCheckBoughtItem", 0x7fe5), Pin(1, "", 0, 1), Pin(10, "SetItem", 1, 10), Pin(11, "Item", 1, 11), Pin(12, "Error", 1, 12)]
    public class FlowNode_LimitedShopCheckBoughtItem : FlowNode
    {
        public FlowNode_LimitedShopCheckBoughtItem()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            if (pinID != 1)
            {
                goto Label_000D;
            }
            this.SetResult();
        Label_000D:
            return;
        }

        private void SetResult()
        {
            PlayerData data;
            LimitedShopData data2;
            LimitedShopItem item;
            <SetResult>c__AnonStorey26B storeyb;
            storeyb = new <SetResult>c__AnonStorey26B();
            data2 = MonoSingleton<GameManager>.Instance.Player.GetLimitedShopData();
            if (data2 == null)
            {
                goto Label_002F;
            }
            if (data2.items.Count > 0)
            {
                goto Label_0039;
            }
        Label_002F:
            base.ActivateOutputLinks(12);
            return;
        Label_0039:
            storeyb.shopdata_index = GlobalVars.ShopBuyIndex;
            item = Enumerable.FirstOrDefault<LimitedShopItem>(data2.items, new Func<LimitedShopItem, bool>(storeyb.<>m__1A3));
            if (item != null)
            {
                goto Label_006C;
            }
            base.ActivateOutputLinks(12);
            return;
        Label_006C:
            if (item.IsSet == null)
            {
                goto Label_0085;
            }
            base.ActivateOutputLinks(10);
            goto Label_008E;
        Label_0085:
            base.ActivateOutputLinks(11);
        Label_008E:
            return;
        }

        [CompilerGenerated]
        private sealed class <SetResult>c__AnonStorey26B
        {
            internal int shopdata_index;

            public <SetResult>c__AnonStorey26B()
            {
                base..ctor();
                return;
            }

            internal bool <>m__1A3(LimitedShopItem item)
            {
                return (item.id == this.shopdata_index);
            }
        }
    }
}

