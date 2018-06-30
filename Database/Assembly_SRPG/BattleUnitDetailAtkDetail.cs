namespace SRPG
{
    using System;
    using System.Text;
    using UnityEngine;
    using UnityEngine.UI;

    public class BattleUnitDetailAtkDetail : MonoBehaviour
    {
        public ImageArray ImageAtkDetail;
        public ImageArray ImageFluct;
        public GameObject GoResist;
        public GameObject GoAvoid;
        public Text TextValue;
        private static string[] mStrAtkDetails;
        private static string[] mStrTypes;

        static BattleUnitDetailAtkDetail()
        {
            string[] textArray2;
            string[] textArray1;
            textArray1 = new string[] { string.Empty, "quest.BUD_AD_SLASH", "quest.BUD_AD_STAB", "quest.BUD_AD_BLOW", "quest.BUD_AD_SHOT", "quest.BUD_AD_MAGIC", "quest.BUD_AD_JUMP", "quest.BUD_AD_ALL_HIT", "quest.BUD_AD_ALL_AVOID", "quest.BUD_AD_ALL_RECOVER" };
            mStrAtkDetails = textArray1;
            textArray2 = new string[] { "quest.BUD_AD_ASSIST", "quest.BUD_AD_RESIST", "quest.BUD_AD_AVOID" };
            mStrTypes = textArray2;
            return;
        }

        public BattleUnitDetailAtkDetail()
        {
            base..ctor();
            return;
        }

        private void AddUpDownText(StringBuilder sb, BattleUnitDetail.eBudFluct fluct)
        {
            BattleUnitDetail.eBudFluct fluct2;
            if (sb != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            fluct2 = fluct;
            switch ((fluct2 - 1))
            {
                case 0:
                    goto Label_0044;

                case 1:
                    goto Label_0044;

                case 2:
                    goto Label_0044;

                case 3:
                    goto Label_002E;

                case 4:
                    goto Label_002E;

                case 5:
                    goto Label_002E;
            }
            goto Label_005A;
        Label_002E:
            sb.Append(LocalizedText.Get("quest.BUD_AD_UP"));
            goto Label_005A;
        Label_0044:
            sb.Append(LocalizedText.Get("quest.BUD_AD_DW"));
        Label_005A:
            return;
        }

        public void SetAll(eAllType all_type, BattleUnitDetail.eBudFluct fluct)
        {
            int num;
            int num2;
            StringBuilder builder;
            if (this.ImageAtkDetail == null)
            {
                goto Label_0038;
            }
            num = all_type;
            if (num < 0)
            {
                goto Label_0038;
            }
            if (num >= ((int) this.ImageAtkDetail.Images.Length))
            {
                goto Label_0038;
            }
            this.ImageAtkDetail.ImageIndex = num;
        Label_0038:
            if (this.GoResist == null)
            {
                goto Label_0054;
            }
            this.GoResist.SetActive(0);
        Label_0054:
            if (this.GoAvoid == null)
            {
                goto Label_0070;
            }
            this.GoAvoid.SetActive(0);
        Label_0070:
            if (this.ImageFluct == null)
            {
                goto Label_00A1;
            }
            num2 = fluct;
            if (num2 >= ((int) this.ImageFluct.Images.Length))
            {
                goto Label_00A1;
            }
            this.ImageFluct.ImageIndex = num2;
        Label_00A1:
            if (this.TextValue == null)
            {
                goto Label_00EC;
            }
            builder = new StringBuilder(0x40);
            builder.Length = 0;
            builder.Append(LocalizedText.Get(mStrAtkDetails[all_type]));
            this.AddUpDownText(builder, fluct);
            this.TextValue.set_text(builder.ToString());
        Label_00EC:
            return;
        }

        public void SetAtkDetail(AttackDetailTypes atk_detail, eType type, BattleUnitDetail.eBudFluct fluct)
        {
            int num;
            int num2;
            StringBuilder builder;
            eType type2;
            if (this.ImageAtkDetail == null)
            {
                goto Label_0038;
            }
            num = atk_detail;
            if (num < 0)
            {
                goto Label_0038;
            }
            if (num >= ((int) this.ImageAtkDetail.Images.Length))
            {
                goto Label_0038;
            }
            this.ImageAtkDetail.ImageIndex = num;
        Label_0038:
            if (this.GoResist == null)
            {
                goto Label_0054;
            }
            this.GoResist.SetActive(0);
        Label_0054:
            if (this.GoAvoid == null)
            {
                goto Label_0070;
            }
            this.GoAvoid.SetActive(0);
        Label_0070:
            type2 = type;
            if (type2 == 1)
            {
                goto Label_0085;
            }
            if (type2 == 2)
            {
                goto Label_00A6;
            }
            goto Label_00C7;
        Label_0085:
            if (this.GoResist == null)
            {
                goto Label_00C7;
            }
            this.GoResist.SetActive(1);
            goto Label_00C7;
        Label_00A6:
            if (this.GoAvoid == null)
            {
                goto Label_00C7;
            }
            this.GoAvoid.SetActive(1);
        Label_00C7:
            if (this.ImageFluct == null)
            {
                goto Label_00F8;
            }
            num2 = fluct;
            if (num2 >= ((int) this.ImageFluct.Images.Length))
            {
                goto Label_00F8;
            }
            this.ImageFluct.ImageIndex = num2;
        Label_00F8:
            if (this.TextValue == null)
            {
                goto Label_0156;
            }
            builder = new StringBuilder(0x40);
            builder.Length = 0;
            builder.Append(LocalizedText.Get(mStrAtkDetails[atk_detail]));
            builder.Append(LocalizedText.Get(mStrTypes[type]));
            this.AddUpDownText(builder, fluct);
            this.TextValue.set_text(builder.ToString());
        Label_0156:
            return;
        }

        public enum eAllType
        {
            HIT = 7,
            AVOID = 8,
            RECOVER = 9,
            MAX = 10,
            MIN = 7
        }

        public enum eType
        {
            ASSIST,
            RESIST,
            AVOID,
            MAX
        }
    }
}

