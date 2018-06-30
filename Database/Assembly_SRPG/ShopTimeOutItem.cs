namespace SRPG
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class ShopTimeOutItem : MonoBehaviour
    {
        public Text Amount;
        public Text Name;
        public GameObject ItemIcon;
        public GameObject ArtifactIcon;
        public ConceptCardIcon m_ConceptCardIcon;

        public ShopTimeOutItem()
        {
            base..ctor();
            return;
        }

        public unsafe void SetShopItemInfo(ShopItem shop_item, string name)
        {
            this.ItemIcon.SetActive(0);
            this.ArtifactIcon.SetActive(0);
            this.m_ConceptCardIcon.get_gameObject().SetActive(0);
            if (shop_item.IsItem == null)
            {
                goto Label_0045;
            }
            this.ItemIcon.SetActive(1);
            goto Label_008E;
        Label_0045:
            if (shop_item.IsArtifact == null)
            {
                goto Label_0061;
            }
            this.ArtifactIcon.SetActive(1);
            goto Label_008E;
        Label_0061:
            if (shop_item.IsConceptCard == null)
            {
                goto Label_0082;
            }
            this.m_ConceptCardIcon.get_gameObject().SetActive(1);
            goto Label_008E;
        Label_0082:
            this.ItemIcon.SetActive(1);
        Label_008E:
            this.Amount.set_text(&shop_item.num.ToString());
            this.Name.set_text(name);
            return;
        }
    }
}

