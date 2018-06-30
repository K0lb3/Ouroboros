namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    [Pin(3, "コイン交換所", 0, 3), Pin(1, "通常ショップ", 0, 1), Pin(4, "魂の交換所", 0, 4), Pin(2, "秘密の店", 0, 2)]
    public class ShopMenu : MonoBehaviour, IFlowInterface
    {
        public Button ShopButton;
        public Button GuerrillaShopButton;
        public Button LimitedShopButton;
        public Button CoinShopButton;
        public Button KakeraShopButton;
        [Space(10f)]
        public GameObject ActiveShop;
        public GameObject ActiveGuerrilla;
        public GameObject ActiveLimited;
        public GameObject ActiveCoin;
        public GameObject ActiveKakera;
        public GameObject GuerrillaBallon;

        public ShopMenu()
        {
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
            bool flag;
            bool flag2;
            bool flag3;
            bool flag4;
            int num;
            flag = 0;
            flag2 = 0;
            flag3 = 0;
            flag4 = 0;
            num = pinID;
            switch ((num - 1))
            {
                case 0:
                    goto Label_0029;

                case 1:
                    goto Label_0030;

                case 2:
                    goto Label_0037;

                case 3:
                    goto Label_003E;
            }
            goto Label_0045;
        Label_0029:
            flag = 1;
            goto Label_0045;
        Label_0030:
            flag2 = 1;
            goto Label_0045;
        Label_0037:
            flag3 = 1;
            goto Label_0045;
        Label_003E:
            flag4 = 1;
        Label_0045:
            if ((this.ShopButton != null) == null)
            {
                goto Label_0076;
            }
            this.ShopButton.get_gameObject().SetActive(1);
            this.ShopButton.set_interactable(flag == 0);
        Label_0076:
            if ((this.ActiveShop != null) == null)
            {
                goto Label_0093;
            }
            this.ActiveShop.SetActive(flag);
        Label_0093:
            if ((this.GuerrillaBallon != null) == null)
            {
                goto Label_00BE;
            }
            this.GuerrillaBallon.SetActive(MonoSingleton<GameManager>.Instance.Player.IsGuerrillaShopOpen());
        Label_00BE:
            if ((this.LimitedShopButton != null) == null)
            {
                goto Label_00DE;
            }
            this.LimitedShopButton.set_interactable(flag2 == 0);
        Label_00DE:
            if ((this.ActiveLimited != null) == null)
            {
                goto Label_00FB;
            }
            this.ActiveLimited.SetActive(flag2);
        Label_00FB:
            if ((this.CoinShopButton != null) == null)
            {
                goto Label_011B;
            }
            this.CoinShopButton.set_interactable(flag3 == 0);
        Label_011B:
            if ((this.ActiveCoin != null) == null)
            {
                goto Label_0138;
            }
            this.ActiveCoin.SetActive(flag3);
        Label_0138:
            if ((this.KakeraShopButton != null) == null)
            {
                goto Label_0158;
            }
            this.KakeraShopButton.set_interactable(flag4 == 0);
        Label_0158:
            if ((this.ActiveKakera != null) == null)
            {
                goto Label_0175;
            }
            this.ActiveKakera.SetActive(flag4);
        Label_0175:
            return;
        }
    }
}

