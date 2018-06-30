namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class ConceptCardIcon : MonoBehaviour
    {
        [SerializeField]
        private RawImage mIconImage;
        [SerializeField]
        private Text mNameText;
        [SerializeField]
        private Text mLevelTitleText;
        [SerializeField]
        private Text mLevelText;
        [SerializeField]
        private Text mLevelCapText;
        [SerializeField]
        private Image mRarityImage;
        [SerializeField]
        private ImageArray mRarityFrame;
        [SerializeField]
        private Text mTrustText;
        [SerializeField]
        private GameObject mFavorite;
        [SerializeField]
        private GameObject mOwner;
        [SerializeField]
        private Image mOwnerIcon;
        [SerializeField]
        private GameObject mSameCardIcon;
        [SerializeField]
        private GameObject mExistSwitchOn;
        [SerializeField]
        private GameObject mExistSwitchOff;
        [SerializeField]
        private Text mCardNum;
        [SerializeField]
        private GameObject mNotSale;
        private ConceptCardData mConceptCard;

        public ConceptCardIcon()
        {
            base..ctor();
            return;
        }

        public UnitData GetOwner()
        {
            if (this.mConceptCard != null)
            {
                goto Label_000D;
            }
            return null;
        Label_000D:
            return this.mConceptCard.GetOwner();
        }

        public void Refresh()
        {
            DataSource.Bind<ConceptCardData>(base.get_gameObject(), this.mConceptCard);
            GameParameter.UpdateAll(base.get_gameObject());
            this.RefreshExistImage();
            this.RefreshIconImage();
            this.RefreshIconParam();
            return;
        }

        private void RefreshExistImage()
        {
            bool flag;
            flag = (this.mConceptCard == null) == 0;
            if ((this.mExistSwitchOn != null) == null)
            {
                goto Label_002A;
            }
            this.mExistSwitchOn.SetActive(flag);
        Label_002A:
            if ((this.mExistSwitchOff != null) == null)
            {
                goto Label_004A;
            }
            this.mExistSwitchOff.SetActive(flag == 0);
        Label_004A:
            return;
        }

        private void RefreshIconImage()
        {
            string str;
            if (this.mConceptCard == null)
            {
                goto Label_001C;
            }
            if ((this.mIconImage == null) == null)
            {
                goto Label_002D;
            }
        Label_001C:
            MonoSingleton<GameManager>.Instance.CancelTextureLoadRequest(this.mIconImage);
            return;
        Label_002D:
            str = AssetPath.ConceptCardIcon(this.mConceptCard.Param);
            MonoSingleton<GameManager>.Instance.ApplyTextureAsync(this.mIconImage, str);
            return;
        }

        private void RefreshIconParam()
        {
            UnitData data;
            if ((this.mRarityFrame != null) == null)
            {
                goto Label_0022;
            }
            this.mRarityFrame.get_gameObject().SetActive(0);
        Label_0022:
            if (this.mConceptCard != null)
            {
                goto Label_0094;
            }
            if ((this.mLevelTitleText != null) == null)
            {
                goto Label_004F;
            }
            this.mLevelTitleText.get_gameObject().SetActive(0);
        Label_004F:
            if ((this.mLevelText != null) == null)
            {
                goto Label_0071;
            }
            this.mLevelText.get_gameObject().SetActive(0);
        Label_0071:
            if ((this.mLevelCapText != null) == null)
            {
                goto Label_0093;
            }
            this.mLevelCapText.get_gameObject().SetActive(0);
        Label_0093:
            return;
        Label_0094:
            this.SetNameText(this.mConceptCard.Param.name);
            this.SetLevelText(this.mConceptCard.Lv);
            this.SetLevelCapText(this.mConceptCard.CurrentLvCap);
            this.SetTrustText(this.mConceptCard.Trust);
            this.SetRarityImaget(this.mConceptCard.Param.rare);
            this.SetFavorite(this.mConceptCard.Favorite);
            this.SetRarityFrame(this.mConceptCard.Rarity);
            if ((this.mOwner != null) == null)
            {
                goto Label_0171;
            }
            data = this.GetOwner();
            if (data == null)
            {
                goto Label_0165;
            }
            this.mOwner.SetActive(1);
            this.SetOwnerIcon(this.mOwnerIcon, data);
            goto Label_0171;
        Label_0165:
            this.mOwner.SetActive(0);
        Label_0171:
            this.SetSameCardIcon();
            return;
        }

        public void ResetIcon()
        {
            this.mConceptCard = null;
            this.mIconImage.set_texture(null);
            this.Refresh();
            return;
        }

        public unsafe void SetCardNum(int num)
        {
            if ((this.mCardNum == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            this.mCardNum.set_text(&num.ToString());
            return;
        }

        public void SetFavorite(bool favorite)
        {
            if ((this.mFavorite == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            this.mFavorite.SetActive(favorite);
            return;
        }

        public unsafe void SetLevelCapText(int lvcap)
        {
            if ((this.mLevelCapText == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            this.mLevelCapText.set_text(&lvcap.ToString());
            this.mLevelCapText.get_gameObject().SetActive(1);
            if ((this.mLevelTitleText != null) == null)
            {
                goto Label_0057;
            }
            this.mLevelTitleText.get_gameObject().SetActive(1);
        Label_0057:
            return;
        }

        public unsafe void SetLevelText(int lv)
        {
            if ((this.mLevelText == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            this.mLevelText.set_text(&lv.ToString());
            this.mLevelText.get_gameObject().SetActive(1);
            if ((this.mLevelTitleText != null) == null)
            {
                goto Label_0057;
            }
            this.mLevelTitleText.get_gameObject().SetActive(1);
        Label_0057:
            return;
        }

        public void SetNameText(string name)
        {
            if ((this.mNameText == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            this.mNameText.set_text(name.ToString());
            return;
        }

        public void SetNoRewardTrustText()
        {
            if ((this.mTrustText == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            this.mTrustText.set_text("---");
            return;
        }

        public void SetNotSellFlag(bool flag)
        {
            if ((this.mNotSale == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            this.mNotSale.SetActive(flag);
            return;
        }

        public void SetOwnerIcon(Image OwnerIcon, UnitData ownerUnit)
        {
            SpriteSheet sheet;
            ItemParam param;
            if ((OwnerIcon == null) == null)
            {
                goto Label_000D;
            }
            return;
        Label_000D:
            sheet = AssetManager.Load<SpriteSheet>("ItemIcon/small");
            param = MonoSingleton<GameManager>.Instance.GetItemParam(ownerUnit.UnitParam.piece);
            OwnerIcon.set_sprite(sheet.GetSprite(param.icon));
            return;
        }

        public void SetRarityFrame(int rarity)
        {
            GameSettings settings;
            if ((this.mRarityFrame == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            this.mRarityFrame.get_gameObject().SetActive(1);
            settings = GameSettings.Instance;
            if ((settings == null) == null)
            {
                goto Label_0036;
            }
            return;
        Label_0036:
            this.mRarityFrame.set_sprite(settings.GetConceptCardFrame(rarity));
            return;
        }

        public void SetRarityImaget(int rare)
        {
            GameSettings settings;
            if ((this.mRarityImage == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            this.mRarityImage.set_sprite(null);
            settings = GameSettings.Instance;
            if ((settings != null) == null)
            {
                goto Label_0051;
            }
            if (((int) settings.ConceptCardIcon_Rarity.Length) <= 0)
            {
                goto Label_0051;
            }
            this.mRarityImage.set_sprite(settings.ConceptCardIcon_Rarity[rare]);
        Label_0051:
            return;
        }

        public void SetSameCardIcon()
        {
            bool flag;
            if ((this.mSameCardIcon == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            this.mSameCardIcon.SetActive(0);
            if ((ConceptCardManager.Instance == null) == null)
            {
                goto Label_002F;
            }
            return;
        Label_002F:
            if (ConceptCardManager.Instance.SelectedConceptCardData != null)
            {
                goto Label_003F;
            }
            return;
        Label_003F:
            if (ConceptCardManager.Instance.IsEnhanceListActive != null)
            {
                goto Label_004F;
            }
            return;
        Label_004F:
            flag = this.mConceptCard.Param.iname == ConceptCardManager.Instance.SelectedConceptCardData.Param.iname;
            this.mSameCardIcon.SetActive(flag);
            return;
        }

        public void SetTrustText(int trust)
        {
            if ((this.mTrustText == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            ConceptCardManager.SubstituteTrustFormat(this.mConceptCard, this.mTrustText, trust, 0);
            return;
        }

        public void Setup(ConceptCardData card)
        {
            this.mConceptCard = card;
            if (card == null)
            {
                goto Label_0018;
            }
            this.Refresh();
            goto Label_001E;
        Label_0018:
            this.ResetIcon();
        Label_001E:
            return;
        }

        public ConceptCardData ConceptCard
        {
            get
            {
                return this.mConceptCard;
            }
        }
    }
}

