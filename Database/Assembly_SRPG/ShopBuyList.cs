namespace SRPG
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class ShopBuyList : MonoBehaviour
    {
        public GameObject amount;
        public GameObject day_reset;
        public GameObject limit;
        public GameObject icon_set;
        public GameObject sold_count;
        public GameObject no_limited_price;
        public Text amount_text;
        public GameObject discount_low;
        public GameObject discount_middle;
        public GameObject discount_high;
        [HeaderBar("▼アイテム表示用")]
        public GameObject item_name;
        public GameObject item_icon;
        [HeaderBar("▼武具表示用")]
        public GameObject artifact_name;
        public GameObject artifact_icon;
        [HeaderBar("▼真理念装表示用")]
        public GameObject conceptCard_name;
        public ConceptCardIcon conceptCard_icon;
        private SRPG.ShopItem mShopItem;

        public ShopBuyList()
        {
            base..ctor();
            return;
        }

        public void SetupConceptCard(ConceptCardData conceptCardData)
        {
            if ((this.conceptCard_icon == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            this.conceptCard_icon.Setup(conceptCardData);
            return;
        }

        public unsafe void SetupDiscountUI()
        {
            int num;
            DiscountGrade grade;
            GameObject obj2;
            bool flag;
            bool flag2;
            bool flag3;
            SerializeValueBehaviour behaviour;
            Text text;
            if (this.mShopItem != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            num = this.mShopItem.discount;
            grade = 0;
            if (num <= 0)
            {
                goto Label_0044;
            }
            grade = 1;
            if (num < 40)
            {
                goto Label_003A;
            }
            if (num >= 70)
            {
                goto Label_003A;
            }
            grade = 2;
            goto Label_0044;
        Label_003A:
            if (num < 70)
            {
                goto Label_0044;
            }
            grade = 3;
        Label_0044:
            obj2 = null;
            if ((this.discount_low != null) == null)
            {
                goto Label_0075;
            }
            flag = grade == 1;
            this.discount_low.SetActive(flag);
            if (flag == null)
            {
                goto Label_0075;
            }
            obj2 = this.discount_low;
        Label_0075:
            if ((this.discount_middle != null) == null)
            {
                goto Label_00A7;
            }
            flag2 = grade == 2;
            this.discount_middle.SetActive(flag2);
            if (flag2 == null)
            {
                goto Label_00A7;
            }
            obj2 = this.discount_middle;
        Label_00A7:
            if ((this.discount_high != null) == null)
            {
                goto Label_00D9;
            }
            flag3 = grade == 3;
            this.discount_high.SetActive(flag3);
            if (flag3 == null)
            {
                goto Label_00D9;
            }
            obj2 = this.discount_high;
        Label_00D9:
            if ((obj2 != null) == null)
            {
                goto Label_0128;
            }
            behaviour = obj2.GetComponent<SerializeValueBehaviour>();
            if ((behaviour != null) == null)
            {
                goto Label_0128;
            }
            text = behaviour.list.GetUILabel("text");
            if ((text != null) == null)
            {
                goto Label_0128;
            }
            text.set_text(&num.ToString());
        Label_0128:
            return;
        }

        public SRPG.ShopItem ShopItem
        {
            get
            {
                return this.mShopItem;
            }
            set
            {
                int num;
                this.mShopItem = value;
                GameUtility.SetGameObjectActive(this.day_reset, this.mShopItem.is_reset);
                GameUtility.SetGameObjectActive(this.limit, this.mShopItem.is_reset == 0);
                GameUtility.SetGameObjectActive(this.icon_set, this.mShopItem.saleType == 7);
                GameUtility.SetGameObjectActive(this.sold_count, this.mShopItem.IsNotLimited == 0);
                GameUtility.SetGameObjectActive(this.no_limited_price, this.mShopItem.IsNotLimited);
                GameUtility.SetGameObjectActive(this.amount, this.mShopItem.IsSet == 0);
                if (this.mShopItem.IsItem != null)
                {
                    goto Label_00B7;
                }
                if (this.mShopItem.IsSet == null)
                {
                    goto Label_0104;
                }
            Label_00B7:
                GameUtility.SetGameObjectActive(this.item_name, 1);
                GameUtility.SetGameObjectActive(this.item_icon, 1);
                GameUtility.SetGameObjectActive(this.artifact_name, 0);
                GameUtility.SetGameObjectActive(this.artifact_icon, 0);
                GameUtility.SetGameObjectActive(this.conceptCard_name, 0);
                GameUtility.SetGameObjectActive(this.conceptCard_icon, 0);
                goto Label_01B9;
            Label_0104:
                if (this.mShopItem.IsArtifact == null)
                {
                    goto Label_0161;
                }
                GameUtility.SetGameObjectActive(this.item_name, 0);
                GameUtility.SetGameObjectActive(this.item_icon, 0);
                GameUtility.SetGameObjectActive(this.artifact_name, 1);
                GameUtility.SetGameObjectActive(this.artifact_icon, 1);
                GameUtility.SetGameObjectActive(this.conceptCard_name, 0);
                GameUtility.SetGameObjectActive(this.conceptCard_icon, 0);
                goto Label_01B9;
            Label_0161:
                if (this.mShopItem.IsConceptCard == null)
                {
                    goto Label_01B9;
                }
                GameUtility.SetGameObjectActive(this.item_name, 0);
                GameUtility.SetGameObjectActive(this.item_icon, 0);
                GameUtility.SetGameObjectActive(this.artifact_name, 0);
                GameUtility.SetGameObjectActive(this.artifact_icon, 0);
                GameUtility.SetGameObjectActive(this.conceptCard_name, 1);
                GameUtility.SetGameObjectActive(this.conceptCard_icon, 1);
            Label_01B9:
                if ((this.amount_text != null) == null)
                {
                    goto Label_01E8;
                }
                this.amount_text.set_text(&this.mShopItem.remaining_num.ToString());
            Label_01E8:
                return;
            }
        }

        public enum DiscountGrade : byte
        {
            None = 0,
            Low = 1,
            Middle = 2,
            High = 3
        }
    }
}

