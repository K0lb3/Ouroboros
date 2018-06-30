namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;

    public class ShopHomeIcon : MonoBehaviour
    {
        public GameObject ShopIcon;
        public GameObject GuerrillaIcon;

        public ShopHomeIcon()
        {
            base..ctor();
            return;
        }

        private void Start()
        {
            bool flag;
            flag = MonoSingleton<GameManager>.Instance.Player.IsGuerrillaShopOpen();
            if ((this.ShopIcon != null) == null)
            {
                goto Label_0030;
            }
            this.ShopIcon.SetActive(flag == 0);
        Label_0030:
            if ((this.GuerrillaIcon != null) == null)
            {
                goto Label_004D;
            }
            this.GuerrillaIcon.SetActive(flag);
        Label_004D:
            return;
        }
    }
}

