namespace SRPG
{
    using System;
    using UnityEngine;

    public class ShopTitle : MonoBehaviour
    {
        public ImageArray IamgeArray;

        public ShopTitle()
        {
            base..ctor();
            return;
        }

        private void Start()
        {
            EShopType type;
            EShopType type2;
            type2 = GlobalVars.ShopType;
            switch (type2)
            {
                case 0:
                    goto Label_0027;

                case 1:
                    goto Label_0038;

                case 2:
                    goto Label_0049;
            }
            if (type2 == 11)
            {
                goto Label_005A;
            }
            goto Label_006B;
        Label_0027:
            this.IamgeArray.ImageIndex = 0;
            goto Label_007C;
        Label_0038:
            this.IamgeArray.ImageIndex = 1;
            goto Label_007C;
        Label_0049:
            this.IamgeArray.ImageIndex = 2;
            goto Label_007C;
        Label_005A:
            this.IamgeArray.ImageIndex = 3;
            goto Label_007C;
        Label_006B:
            base.get_gameObject().SetActive(0);
        Label_007C:
            return;
        }
    }
}

