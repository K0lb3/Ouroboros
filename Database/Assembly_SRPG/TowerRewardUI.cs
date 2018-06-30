namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class TowerRewardUI : MonoBehaviour
    {
        public GameParameter ItemIcon;
        public RawImage_Transparent BaseImage;
        public BitmapText NumText;
        public Texture GoldImage;
        public Texture CoinImage;
        public Texture ArenaCoinImage;
        public Texture MultiCoinImage;
        public Texture KakeraCoinImage;
        public Text ItemName;
        public Text ItemNameNumTex;
        public GameObject ItemFrameObj;

        public TowerRewardUI()
        {
            base..ctor();
            return;
        }

        public unsafe void Refresh()
        {
            TowerRewardItem item;
            ItemParam param;
            ArtifactParam param2;
            TowerRewardItem.RewardType type;
            this.ItemIcon.set_enabled(0);
            item = DataSource.FindDataOfClass<TowerRewardItem>(base.get_gameObject(), null);
            if (item != null)
            {
                goto Label_0020;
            }
            return;
        Label_0020:
            this.NumText.text = &item.num.ToString();
            if ((this.ItemFrameObj != null) == null)
            {
                goto Label_0053;
            }
            this.ItemFrameObj.SetActive(1);
        Label_0053:
            switch (item.type)
            {
                case 0:
                    goto Label_0081;

                case 1:
                    goto Label_00D6;

                case 2:
                    goto Label_0112;

                case 3:
                    goto Label_014E;

                case 4:
                    goto Label_018A;

                case 5:
                    goto Label_01C6;

                case 6:
                    goto Label_0202;
            }
            goto Label_0262;
        Label_0081:
            this.ItemIcon.set_enabled(1);
            this.ItemIcon.UpdateValue();
            param = MonoSingleton<GameManager>.Instance.GetItemParam(item.iname);
            if (param == null)
            {
                goto Label_0267;
            }
            if ((this.ItemName != null) == null)
            {
                goto Label_0267;
            }
            this.ItemName.set_text(param.name);
            goto Label_0267;
        Label_00D6:
            this.BaseImage.set_texture(this.GoldImage);
            if ((this.ItemName != null) == null)
            {
                goto Label_0267;
            }
            this.ItemName.set_text(LocalizedText.Get("sys.GOLD"));
            goto Label_0267;
        Label_0112:
            this.BaseImage.set_texture(this.CoinImage);
            if ((this.ItemName != null) == null)
            {
                goto Label_0267;
            }
            this.ItemName.set_text(LocalizedText.Get("sys.COIN"));
            goto Label_0267;
        Label_014E:
            this.BaseImage.set_texture(this.ArenaCoinImage);
            if ((this.ItemName != null) == null)
            {
                goto Label_0267;
            }
            this.ItemName.set_text(LocalizedText.Get("sys.ARENA_COIN"));
            goto Label_0267;
        Label_018A:
            this.BaseImage.set_texture(this.MultiCoinImage);
            if ((this.ItemName != null) == null)
            {
                goto Label_0267;
            }
            this.ItemName.set_text(LocalizedText.Get("sys.MULTI_COIN"));
            goto Label_0267;
        Label_01C6:
            this.BaseImage.set_texture(this.KakeraCoinImage);
            if ((this.ItemName != null) == null)
            {
                goto Label_0267;
            }
            this.ItemName.set_text(LocalizedText.Get("sys.KakeraCoin"));
            goto Label_0267;
        Label_0202:
            if ((this.ItemFrameObj != null) == null)
            {
                goto Label_021F;
            }
            this.ItemFrameObj.SetActive(0);
        Label_021F:
            param2 = MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(item.iname);
            if (param2 == null)
            {
                goto Label_0267;
            }
            if ((this.ItemName != null) == null)
            {
                goto Label_0267;
            }
            this.ItemName.set_text(param2.name);
            goto Label_0267;
        Label_0262:;
        Label_0267:
            return;
        }
    }
}

