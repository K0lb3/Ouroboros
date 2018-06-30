namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    [Pin(1, "Refresh", 0, 1)]
    public class ShopBuyUpdateConfirmWindow : MonoBehaviour, IFlowInterface
    {
        public Text Title;
        public Text Message;
        public Text DecideText;
        public Text CancelText;

        public ShopBuyUpdateConfirmWindow()
        {
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
            if (pinID != 1)
            {
                goto Label_000D;
            }
            this.Refresh();
        Label_000D:
            return;
        }

        private void Refresh()
        {
            base.set_enabled(1);
            return;
        }

        private void Start()
        {
            ShopParam param;
            string str;
            string str2;
            ItemParam param2;
            int num;
            ESaleType type;
            if ((this.Title != null) == null)
            {
                goto Label_0026;
            }
            this.Title.set_text(LocalizedText.Get("sys.UPDATE_ITEMLIST_TITLE"));
        Label_0026:
            if ((this.Message != null) == null)
            {
                goto Label_0199;
            }
            switch (MonoSingleton<GameManager>.Instance.MasterParam.GetShopParam(GlobalVars.ShopType).UpdateCostType)
            {
                case 0:
                    goto Label_007C;

                case 1:
                    goto Label_0144;

                case 2:
                    goto Label_0097;

                case 3:
                    goto Label_00B2;

                case 4:
                    goto Label_00E8;

                case 5:
                    goto Label_00CD;

                case 6:
                    goto Label_0103;
            }
            goto Label_0144;
        Label_007C:
            str = LocalizedText.Get("sys.GOLD");
            str2 = LocalizedText.Get("sys.GOLD");
            goto Label_015F;
        Label_0097:
            str = LocalizedText.Get("sys.TOUR_COIN");
            str2 = LocalizedText.Get("sys.ITEM_TANI_1");
            goto Label_015F;
        Label_00B2:
            str = LocalizedText.Get("sys.ARENA_COIN");
            str2 = LocalizedText.Get("sys.ITEM_TANI_1");
            goto Label_015F;
        Label_00CD:
            str = LocalizedText.Get("sys.MULTI_COIN");
            str2 = LocalizedText.Get("sys.ITEM_TANI_1");
            goto Label_015F;
        Label_00E8:
            str = LocalizedText.Get("sys.PIECE_POINT");
            str2 = LocalizedText.Get("sys.ITEM_TANI_3");
            goto Label_015F;
        Label_0103:
            param2 = MonoSingleton<GameManager>.Instance.GetItemParam(GlobalVars.EventShopItem.shop_cost_iname);
            str = (param2 == null) ? LocalizedText.Get("sys.EVENT_COIN") : param2.name;
            str2 = LocalizedText.Get("sys.ITEM_TANI_1");
            goto Label_015F;
        Label_0144:
            str = LocalizedText.Get("sys.COIN");
            str2 = LocalizedText.Get("sys.ITEM_TANI_1");
        Label_015F:
            num = MonoSingleton<GameManager>.Instance.Player.GetShopUpdateCost(GlobalVars.ShopType, 0);
            this.Message.set_text(string.Format(LocalizedText.Get("sys.UPDATE_ITEMLIST_MESSAGE"), str, (int) num, str2));
        Label_0199:
            if ((this.DecideText != null) == null)
            {
                goto Label_01BF;
            }
            this.DecideText.set_text(LocalizedText.Get("sys.CMD_YES"));
        Label_01BF:
            if ((this.CancelText != null) == null)
            {
                goto Label_01E5;
            }
            this.CancelText.set_text(LocalizedText.Get("sys.CMD_NO"));
        Label_01E5:
            this.Refresh();
            return;
        }
    }
}

