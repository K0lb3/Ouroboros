namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class LimitedShopSetItemListElement : MonoBehaviour
    {
        public Text itemName;
        public GameObject ItemIcon;
        public GameObject ItemDetailWindow;
        public GameObject ArtifactIcon;
        public GameObject ArtifactDetailWindow;
        public GameObject ConceptCard;
        public GameObject ConceptCardDetailWindow;
        private GameObject mDetailWindow;
        private LimitedShopItem mLimitedShopItem;
        private ItemData mItemData;
        private SRPG.ArtifactParam mArtifactParam;
        private ConceptCardData mConceptCardData;

        public LimitedShopSetItemListElement()
        {
            base..ctor();
            return;
        }

        public void OnClickDetailArtifact()
        {
            ArtifactData data;
            Json_Artifact artifact;
            if ((this.mDetailWindow != null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            this.mDetailWindow = Object.Instantiate<GameObject>(this.ArtifactDetailWindow);
            data = new ArtifactData();
            artifact = new Json_Artifact();
            artifact.iname = this.mArtifactParam.iname;
            artifact.rare = this.mArtifactParam.rareini;
            data.Deserialize(artifact);
            DataSource.Bind<ArtifactData>(this.mDetailWindow, data);
            return;
        }

        public void OnClickDetailConceptCard()
        {
            if ((this.mDetailWindow != null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            GlobalVars.SelectedConceptCardData.Set(this.mConceptCardData);
            this.mDetailWindow = Object.Instantiate<GameObject>(this.ConceptCardDetailWindow);
            return;
        }

        public void OnClickDetailItem()
        {
            ItemData data;
            if ((this.mDetailWindow != null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            this.mDetailWindow = Object.Instantiate<GameObject>(this.ItemDetailWindow);
            data = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemID(this.mItemData.Param.iname);
            DataSource.Bind<ItemData>(this.mDetailWindow, data);
            DataSource.Bind<ItemParam>(this.mDetailWindow, MonoSingleton<GameManager>.Instance.GetItemParam(this.mItemData.Param.iname));
            DataSource.Bind<LimitedShopItem>(this.mDetailWindow, this.mLimitedShopItem);
            return;
        }

        public void SetShopItemDesc(Json_ShopItemDesc item)
        {
            this.ItemIcon.SetActive(0);
            this.ArtifactIcon.SetActive(0);
            this.ConceptCard.SetActive(0);
            if (item.IsItem == null)
            {
                goto Label_0040;
            }
            this.ItemIcon.SetActive(1);
            goto Label_0093;
        Label_0040:
            if (item.IsArtifact == null)
            {
                goto Label_005C;
            }
            this.ArtifactIcon.SetActive(1);
            goto Label_0093;
        Label_005C:
            if (item.IsConceptCard == null)
            {
                goto Label_0078;
            }
            this.ConceptCard.SetActive(1);
            goto Label_0093;
        Label_0078:
            DebugUtility.LogError(string.Format("不明な商品タイプが設定されています (item.iname({0}) => {1})", item.iname, item.itype));
        Label_0093:
            if (this.mLimitedShopItem != null)
            {
                goto Label_00A9;
            }
            this.mLimitedShopItem = new LimitedShopItem();
        Label_00A9:
            this.mLimitedShopItem.num = item.num;
            this.mLimitedShopItem.iname = item.iname;
            return;
        }

        public void SetupConceptCard(ConceptCardData conceptCardData)
        {
            ConceptCardIcon icon;
            this.mConceptCardData = conceptCardData;
            if ((this.ConceptCard == null) == null)
            {
                goto Label_0023;
            }
            DebugUtility.LogError("ConceptCard == null");
            return;
        Label_0023:
            icon = this.ConceptCard.GetComponentInChildren<ConceptCardIcon>();
            if ((icon != null) == null)
            {
                goto Label_0042;
            }
            icon.Setup(conceptCardData);
        Label_0042:
            return;
        }

        public ItemData itemData
        {
            get
            {
                return this.mItemData;
            }
            set
            {
                DataSource.Bind<ItemData>(base.get_gameObject(), value);
                this.mItemData = value;
                return;
            }
        }

        public SRPG.ArtifactParam ArtifactParam
        {
            get
            {
                return this.mArtifactParam;
            }
            set
            {
                DataSource.Bind<SRPG.ArtifactParam>(base.get_gameObject(), value);
                this.mArtifactParam = value;
                return;
            }
        }
    }
}

