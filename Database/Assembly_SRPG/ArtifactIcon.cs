namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class ArtifactIcon : BaseIcon
    {
        public RawImage Icon;
        public Image Rarity;
        public Image RarityMax;
        public Text RarityText;
        public Text RarityMaxText;
        public Image Frame;
        public Text Lv;
        public Text LvCap;
        public Text PreLvCap;
        public Slider LvGauge;
        public Slider ExpGauge;
        public Slider PieceGauge;
        public Image Category;
        public GameObject Owner;
        public Image OwnerIcon;
        public Text DecCost;
        public Text DecKakeraNum;
        public Text TransmuteCost;
        public GameObject NotRarityUp;
        public GameObject CanRarityUp;
        public bool ForceMask;
        public InstanceTypes InstanceType;
        public int DeriveTriggerIndex;
        [NonSerialized]
        public GameObject IndexBadge;
        private int mLastLv;
        private int mLastLvCap;
        private int mLastExpNum;
        public GameObject RarityUp;
        public GameObject CanCreate;
        public Image RarityUpBack;
        public Image CanCreateBack;
        public Image CanCreateGauge;
        public Image DefaultGauge;
        public Image DefaultBack;
        public Text RarityUpCost;
        public Text PieceNum;
        public Image[] NotCreateGrayIcon;
        public RawImage[] NotCreateGrayRawIcon;
        public GameObject Favorite;
        public GameObject LockMask;

        public ArtifactIcon()
        {
            this.mLastLv = -1;
            this.mLastLvCap = -1;
            this.mLastExpNum = -1;
            base..ctor();
            return;
        }

        private void OnDisable()
        {
            GameManager manager;
            manager = MonoSingleton<GameManager>.GetInstanceDirect();
            if ((manager != null) == null)
            {
                goto Label_002F;
            }
            if ((this.Icon != null) == null)
            {
                goto Label_002F;
            }
            manager.CancelTextureLoadRequest(this.Icon);
        Label_002F:
            return;
        }

        private unsafe bool SetOwnerIcon(GameManager gm, ArtifactData data)
        {
            UnitData data2;
            JobData data3;
            SpriteSheet sheet;
            ItemParam param;
            if (gm.Player.FindOwner(data, &data2, &data3) == null)
            {
                goto Label_005C;
            }
            sheet = AssetManager.Load<SpriteSheet>("ItemIcon/small");
            param = gm.GetItemParam(data2.UnitParam.piece);
            if ((this.OwnerIcon != null) == null)
            {
                goto Label_005A;
            }
            this.OwnerIcon.set_sprite(sheet.GetSprite(param.icon));
        Label_005A:
            return 1;
        Label_005C:
            return 0;
        }

        private void Start()
        {
            this.UpdateValue();
            return;
        }

        public override unsafe void UpdateValue()
        {
            ArtifactData data;
            ArtifactParam param;
            GameManager manager;
            SkillAbilityDeriveParam param2;
            RarityParam param3;
            int num;
            int num2;
            ItemData data2;
            RarityParam param4;
            string str;
            int num3;
            int num4;
            bool flag;
            bool flag2;
            ItemData data3;
            RarityParam param5;
            int num5;
            int num6;
            int num7;
            int num8;
            bool flag3;
            int num9;
            RarityParam param6;
            int num10;
            ItemData data4;
            GameSettings settings;
            GameSettings settings2;
            GameSettings settings3;
            GameSettings settings4;
            bool flag4;
            OInt num11;
            OInt num12;
            float num13;
            int num14;
            int num15;
            int num16;
            ArtifactTypes types;
            int num17;
            data = null;
            param = null;
            manager = MonoSingleton<GameManager>.Instance;
            if (this.InstanceType != null)
            {
                goto Label_0027;
            }
            data = DataSource.FindDataOfClass<ArtifactData>(base.get_gameObject(), null);
            goto Label_0096;
        Label_0027:
            if (this.InstanceType != 2)
            {
                goto Label_0058;
            }
            param2 = DataSource.FindDataOfClass<SkillAbilityDeriveParam>(base.get_gameObject(), null);
            if (param2 == null)
            {
                goto Label_0096;
            }
            param = param2.GetTriggerArtifactParam(this.DeriveTriggerIndex);
            goto Label_0096;
        Label_0058:
            if (this.InstanceType != 3)
            {
                goto Label_0089;
            }
            data = DataSource.FindDataOfClass<ArtifactData>(base.get_gameObject(), null);
            if (data != null)
            {
                goto Label_0096;
            }
            param = DataSource.FindDataOfClass<ArtifactParam>(base.get_gameObject(), null);
            goto Label_0096;
        Label_0089:
            param = DataSource.FindDataOfClass<ArtifactParam>(base.get_gameObject(), null);
        Label_0096:
            if ((this.Lv != null) == null)
            {
                goto Label_0115;
            }
            if (data == null)
            {
                goto Label_0104;
            }
            if (data.Lv == this.mLastLv)
            {
                goto Label_00EE;
            }
            this.mLastLv = data.Lv;
            this.Lv.set_text(&data.Lv.ToString());
        Label_00EE:
            this.Lv.get_gameObject().SetActive(1);
            goto Label_0115;
        Label_0104:
            this.Lv.get_gameObject().SetActive(0);
        Label_0115:
            if ((this.PreLvCap != null) == null)
            {
                goto Label_0194;
            }
            if ((data == null) || (data.Rarity <= 0))
            {
                goto Label_0183;
            }
            param3 = MonoSingleton<GameManager>.Instance.GetRarityParam(data.Rarity - 1);
            this.PreLvCap.set_text(&param3.ArtifactLvCap.ToString());
            this.PreLvCap.get_gameObject().SetActive(1);
            goto Label_0194;
        Label_0183:
            this.PreLvCap.get_gameObject().SetActive(0);
        Label_0194:
            if ((this.LvCap != null) == null)
            {
                goto Label_0213;
            }
            if (data == null)
            {
                goto Label_0202;
            }
            if (data.LvCap == this.mLastLvCap)
            {
                goto Label_01EC;
            }
            this.mLastLvCap = data.LvCap;
            this.LvCap.set_text(&data.LvCap.ToString());
        Label_01EC:
            this.LvCap.get_gameObject().SetActive(1);
            goto Label_0213;
        Label_0202:
            this.LvCap.get_gameObject().SetActive(0);
        Label_0213:
            if ((this.LvGauge != null) == null)
            {
                goto Label_02A0;
            }
            if (data == null)
            {
                goto Label_028F;
            }
            if (data.Exp == this.mLastExpNum)
            {
                goto Label_0279;
            }
            this.LvGauge.set_minValue(1f);
            this.LvGauge.set_maxValue((float) data.LvCap);
            this.LvGauge.set_value((float) data.Lv);
        Label_0279:
            this.LvGauge.get_gameObject().SetActive(1);
            goto Label_02A0;
        Label_028F:
            this.LvGauge.get_gameObject().SetActive(0);
        Label_02A0:
            if ((this.ExpGauge != null) == null)
            {
                goto Label_037F;
            }
            if (data == null)
            {
                goto Label_036E;
            }
            if (data.Exp == this.mLastExpNum)
            {
                goto Label_0358;
            }
            if (data.Lv < data.LvCap)
            {
                goto Label_0319;
            }
            this.ExpGauge.set_minValue(0f);
            num13 = 1f;
            this.ExpGauge.set_value(num13);
            this.ExpGauge.set_maxValue(num13);
            goto Label_0358;
        Label_0319:
            num = data.GetShowExp();
            num2 = data.GetNextExp();
            this.ExpGauge.set_minValue(0f);
            this.ExpGauge.set_maxValue((float) (num + num2));
            this.ExpGauge.set_value((float) num);
        Label_0358:
            this.ExpGauge.get_gameObject().SetActive(1);
            goto Label_037F;
        Label_036E:
            this.ExpGauge.get_gameObject().SetActive(0);
        Label_037F:
            if ((this.PieceGauge != null) == null)
            {
                goto Label_042E;
            }
            if (param == null)
            {
                goto Label_041D;
            }
            this.PieceGauge.set_minValue(0f);
            data2 = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemID(param.kakera);
            param4 = MonoSingleton<GameManager>.Instance.GetRarityParam(param.rareini);
            this.PieceGauge.set_maxValue((float) param4.ArtifactCreatePieceNum);
            this.PieceGauge.set_value((float) ((data2 == null) ? 0 : data2.Num));
            this.PieceGauge.get_gameObject().SetActive(1);
            goto Label_042E;
        Label_041D:
            this.PieceGauge.get_gameObject().SetActive(0);
        Label_042E:
            if ((this.Icon != null) == null)
            {
                goto Label_048F;
            }
            if ((data == null) && (param == null))
            {
                goto Label_0477;
            }
            str = AssetPath.ArtifactIcon((data == null) ? param : data.ArtifactParam);
            manager.ApplyTextureAsync(this.Icon, str);
            goto Label_048F;
        Label_0477:
            manager.CancelTextureLoadRequest(this.Icon);
            this.Icon.set_texture(null);
        Label_048F:
            num3 = 0;
            num4 = 0;
            if (data == null)
            {
                goto Label_04BA;
            }
            num3 = data.Rarity;
            num4 = data.RarityCap;
            goto Label_04D0;
        Label_04BA:
            if (param == null)
            {
                goto Label_04D0;
            }
            num3 = param.rareini;
            num4 = param.raremax;
        Label_04D0:
            if ((data == null) && (param == null))
            {
                goto Label_0B26;
            }
            flag = (data == null) ? 0 : (data.CheckEnableRarityUp() == 0);
            if ((this.RarityUp != null) == null)
            {
                goto Label_050E;
            }
            this.RarityUp.SetActive(flag);
        Label_050E:
            if (((this.RarityUpBack != null) == null) || ((this.DefaultBack != null) == null))
            {
                goto Label_054D;
            }
            this.RarityUpBack.set_enabled(flag);
            this.DefaultBack.set_enabled(flag == 0);
        Label_054D:
            flag2 = 0;
            if (param == null)
            {
                goto Label_05A8;
            }
            data3 = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemID(param.kakera);
            if (data3 == null)
            {
                goto Label_05A5;
            }
            param5 = MonoSingleton<GameManager>.Instance.GetRarityParam(param.rareini);
            flag2 = (data3.Num < param5.ArtifactCreatePieceNum) == 0;
            goto Label_05A8;
        Label_05A5:
            flag2 = 0;
        Label_05A8:
            if ((this.CanCreate != null) == null)
            {
                goto Label_05C6;
            }
            this.CanCreate.SetActive(flag2);
        Label_05C6:
            if (((this.CanCreateBack != null) == null) || ((this.DefaultBack != null) == null))
            {
                goto Label_0605;
            }
            this.CanCreateBack.set_enabled(flag2);
            this.DefaultBack.set_enabled(flag2 == 0);
        Label_0605:
            if (((this.CanCreateGauge != null) == null) || ((this.DefaultBack != null) == null))
            {
                goto Label_0644;
            }
            this.CanCreateGauge.set_enabled(flag2);
            this.DefaultBack.set_enabled(flag2 == 0);
        Label_0644:
            if ((this.NotCreateGrayIcon == null) || (((int) this.NotCreateGrayIcon.Length) <= 0))
            {
                goto Label_06C9;
            }
            if (flag2 == null)
            {
                goto Label_0699;
            }
            num5 = 0;
            goto Label_0685;
        Label_066C:
            this.NotCreateGrayIcon[num5].set_color(Color.get_white());
            num5 += 1;
        Label_0685:
            if (num5 < ((int) this.NotCreateGrayIcon.Length))
            {
                goto Label_066C;
            }
            goto Label_06C9;
        Label_0699:
            num6 = 0;
            goto Label_06BA;
        Label_06A1:
            this.NotCreateGrayIcon[num6].set_color(Color.get_cyan());
            num6 += 1;
        Label_06BA:
            if (num6 < ((int) this.NotCreateGrayIcon.Length))
            {
                goto Label_06A1;
            }
        Label_06C9:
            if ((this.NotCreateGrayRawIcon == null) || (((int) this.NotCreateGrayRawIcon.Length) <= 0))
            {
                goto Label_074E;
            }
            if (flag2 == null)
            {
                goto Label_071E;
            }
            num7 = 0;
            goto Label_070A;
        Label_06F1:
            this.NotCreateGrayRawIcon[num7].set_color(Color.get_white());
            num7 += 1;
        Label_070A:
            if (num7 < ((int) this.NotCreateGrayRawIcon.Length))
            {
                goto Label_06F1;
            }
            goto Label_074E;
        Label_071E:
            num8 = 0;
            goto Label_073F;
        Label_0726:
            this.NotCreateGrayRawIcon[num8].set_color(Color.get_cyan());
            num8 += 1;
        Label_073F:
            if (num8 < ((int) this.NotCreateGrayRawIcon.Length))
            {
                goto Label_0726;
            }
        Label_074E:
            if (((data == null) || ((this.NotRarityUp != null) == null)) || ((this.CanRarityUp != null) == null))
            {
                goto Label_07AD;
            }
            flag3 = data.Rarity == data.RarityCap;
            this.NotRarityUp.SetActive(flag3);
            this.CanRarityUp.SetActive(flag3 == 0);
        Label_07AD:
            if ((data == null) || ((this.RarityUpCost != null) == null))
            {
                goto Label_07DE;
            }
            num9 = data.GetKakeraNeedNum();
            this.RarityUpCost.set_text(&num9.ToString());
        Label_07DE:
            if ((param == null) || ((this.TransmuteCost != null) == null))
            {
                goto Label_0827;
            }
            num10 = MonoSingleton<GameManager>.Instance.GetRarityParam(param.rareini).ArtifactCreatePieceNum;
            this.TransmuteCost.set_text(&num10.ToString());
        Label_0827:
            if ((this.PieceNum != null) == null)
            {
                goto Label_08EC;
            }
            data4 = (data == null) ? MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemID(param.kakera) : MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemParam(data.Kakera);
            if (data4 == null)
            {
                goto Label_08CC;
            }
            this.PieceNum.set_text(&data4.Num.ToString());
            if ((data == null) || (data.CheckEnableRarityUp() != null))
            {
                goto Label_08B7;
            }
            this.PieceNum.set_color(Color.get_yellow());
            goto Label_08C7;
        Label_08B7:
            this.PieceNum.set_color(Color.get_white());
        Label_08C7:
            goto Label_08EC;
        Label_08CC:
            this.PieceNum.set_text("0");
            this.PieceNum.set_color(Color.get_white());
        Label_08EC:
            if ((this.Rarity != null) == null)
            {
                goto Label_0936;
            }
            settings = GameSettings.Instance;
            if (((settings != null) == null) || (num3 >= ((int) settings.ArtifactIcon_Rarity.Length)))
            {
                goto Label_0936;
            }
            this.Rarity.set_sprite(settings.ArtifactIcon_Rarity[num3]);
        Label_0936:
            if ((this.RarityMax != null) == null)
            {
                goto Label_0980;
            }
            settings2 = GameSettings.Instance;
            if (((settings2 != null) == null) || (num4 >= ((int) settings2.ArtifactIcon_RarityBG.Length)))
            {
                goto Label_0980;
            }
            this.RarityMax.set_sprite(settings2.ArtifactIcon_RarityBG[num4]);
        Label_0980:
            if ((this.RarityText != null) == null)
            {
                goto Label_09A9;
            }
            num15 = num3 + 1;
            this.RarityText.set_text(&num15.ToString());
        Label_09A9:
            if ((this.RarityMaxText != null) == null)
            {
                goto Label_09D2;
            }
            num16 = num4 + 1;
            this.RarityMaxText.set_text(&num16.ToString());
        Label_09D2:
            if ((this.Frame != null) == null)
            {
                goto Label_0A1C;
            }
            settings3 = GameSettings.Instance;
            if (((settings3 != null) == null) || (num3 >= ((int) settings3.ArtifactIcon_Frames.Length)))
            {
                goto Label_0A1C;
            }
            this.Frame.set_sprite(settings3.ArtifactIcon_Frames[num3]);
        Label_0A1C:
            if ((this.Category != null) == null)
            {
                goto Label_0ACA;
            }
            settings4 = GameSettings.Instance;
            if (((settings4 != null) == null) || ((data == null) && (param == null)))
            {
                goto Label_0ACA;
            }
            types = (data == null) ? param.type : data.ArtifactParam.type;
            switch ((types - 1))
            {
                case 0:
                    goto Label_0AB3;

                case 1:
                    goto Label_0A85;

                case 2:
                    goto Label_0A9C;
            }
            goto Label_0ACA;
        Label_0A85:
            this.Category.set_sprite(settings4.ArtifactIcon_Armor);
            goto Label_0ACA;
        Label_0A9C:
            this.Category.set_sprite(settings4.ArtifactIcon_Misc);
            goto Label_0ACA;
        Label_0AB3:
            this.Category.set_sprite(settings4.ArtifactIcon_Weapon);
        Label_0ACA:
            if ((this.DecKakeraNum != null) == null)
            {
                goto Label_0AF5;
            }
            this.DecKakeraNum.set_text(&data.GetKakeraChangeNum().ToString());
        Label_0AF5:
            if ((this.DecCost != null) == null)
            {
                goto Label_0B9A;
            }
            this.DecCost.set_text(&data.RarityParam.ArtifactChangeCost.ToString());
            goto Label_0B9A;
        Label_0B26:
            if ((this.Rarity != null) == null)
            {
                goto Label_0B43;
            }
            this.Rarity.set_sprite(null);
        Label_0B43:
            if ((this.RarityMax != null) == null)
            {
                goto Label_0B60;
            }
            this.RarityMax.set_sprite(null);
        Label_0B60:
            if ((this.Frame != null) == null)
            {
                goto Label_0B7D;
            }
            this.Frame.set_sprite(null);
        Label_0B7D:
            if ((this.Category != null) == null)
            {
                goto Label_0B9A;
            }
            this.Category.set_sprite(null);
        Label_0B9A:
            flag4 = 0;
            if ((this.Owner != null) == null)
            {
                goto Label_0BE1;
            }
            if (data == null)
            {
                goto Label_0BD5;
            }
            if (this.SetOwnerIcon(manager, data) == null)
            {
                goto Label_0BD5;
            }
            this.Owner.SetActive(1);
            flag4 = 1;
            goto Label_0BE1;
        Label_0BD5:
            this.Owner.SetActive(0);
        Label_0BE1:
            if ((this.Favorite != null) == null)
            {
                goto Label_0C23;
            }
            if (data == null)
            {
                goto Label_0C17;
            }
            if (data.IsFavorite == null)
            {
                goto Label_0C17;
            }
            this.Favorite.SetActive(1);
            flag4 = 1;
            goto Label_0C23;
        Label_0C17:
            this.Favorite.SetActive(0);
        Label_0C23:
            if (this.ForceMask == null)
            {
                goto Label_0C31;
            }
            flag4 = 1;
        Label_0C31:
            if ((this.LockMask != null) == null)
            {
                goto Label_0C4F;
            }
            this.LockMask.SetActive(flag4);
        Label_0C4F:
            if (data == null)
            {
                goto Label_0C61;
            }
            this.mLastExpNum = data.Exp;
        Label_0C61:
            return;
        }

        public enum InstanceTypes
        {
            ArtifactData,
            ArtifactParam,
            SkillAbilityDeriveParam,
            ArtifactDataOrParam
        }
    }
}

