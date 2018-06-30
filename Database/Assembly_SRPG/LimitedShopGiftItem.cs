namespace SRPG
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class LimitedShopGiftItem : MonoBehaviour
    {
        public Text Amount;
        public Text Name;
        public GameObject ItemIcon;
        public GameObject ArtifactIcon;
        public ConceptCardIcon m_ConceptCardIcon;

        public LimitedShopGiftItem()
        {
            base..ctor();
            return;
        }

        public unsafe void SetShopItemInfo(Json_ShopItemDesc shop_item_desc, string name, int amount)
        {
            int num;
            this.ItemIcon.SetActive(0);
            this.ArtifactIcon.SetActive(0);
            this.m_ConceptCardIcon.get_gameObject().SetActive(0);
            if (shop_item_desc.IsItem == null)
            {
                goto Label_0045;
            }
            this.ItemIcon.SetActive(1);
            goto Label_007D;
        Label_0045:
            if (shop_item_desc.IsArtifact == null)
            {
                goto Label_0061;
            }
            this.ArtifactIcon.SetActive(1);
            goto Label_007D;
        Label_0061:
            if (shop_item_desc.IsConceptCard == null)
            {
                goto Label_007D;
            }
            this.m_ConceptCardIcon.get_gameObject().SetActive(1);
        Label_007D:
            num = shop_item_desc.num * amount;
            this.Amount.set_text(&num.ToString());
            this.Name.set_text(name);
            return;
        }
    }
}

