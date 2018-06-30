namespace SRPG
{
    using System;
    using UnityEngine;

    public class TowerRewardIcon : MonoBehaviour
    {
        public GameParameter ItemIcon;
        public RawImage_Transparent BaseImage;
        public BitmapText NumText;
        public Texture GoldImage;
        public Texture CoinImage;
        public Texture ArenaCoinImage;
        public Texture MultiCoinImage;
        public Texture KakeraCoinImage;

        public TowerRewardIcon()
        {
            base..ctor();
            return;
        }

        public unsafe void Refresh()
        {
            TowerRewardItem item;
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
            switch (item.type)
            {
                case 0:
                    goto Label_0064;

                case 1:
                    goto Label_0080;

                case 2:
                    goto Label_0096;

                case 3:
                    goto Label_00AC;

                case 4:
                    goto Label_00C2;

                case 5:
                    goto Label_00D8;

                case 6:
                    goto Label_00EE;
            }
            goto Label_00F3;
        Label_0064:
            this.ItemIcon.set_enabled(1);
            this.ItemIcon.UpdateValue();
            goto Label_00F8;
        Label_0080:
            this.BaseImage.set_texture(this.GoldImage);
            goto Label_00F8;
        Label_0096:
            this.BaseImage.set_texture(this.CoinImage);
            goto Label_00F8;
        Label_00AC:
            this.BaseImage.set_texture(this.ArenaCoinImage);
            goto Label_00F8;
        Label_00C2:
            this.BaseImage.set_texture(this.MultiCoinImage);
            goto Label_00F8;
        Label_00D8:
            this.BaseImage.set_texture(this.KakeraCoinImage);
            goto Label_00F8;
        Label_00EE:
            goto Label_00F8;
        Label_00F3:;
        Label_00F8:
            return;
        }
    }
}

