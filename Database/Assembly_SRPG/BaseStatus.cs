namespace SRPG
{
    using System;
    using System.Reflection;
    using System.Runtime.InteropServices;

    public class BaseStatus
    {
        public static readonly int MAX_BATTLE_BONUS;
        public StatusParam param;
        public ElementParam element_assist;
        public ElementParam element_resist;
        public EnchantParam enchant_assist;
        public EnchantParam enchant_resist;
        public BattleBonusParam bonus;

        static BaseStatus()
        {
            MAX_BATTLE_BONUS = (int) Enum.GetNames(typeof(BattleBonus)).Length;
            return;
        }

        public BaseStatus()
        {
            this.param = new StatusParam();
            this.element_assist = new ElementParam();
            this.element_resist = new ElementParam();
            this.enchant_assist = new EnchantParam();
            this.enchant_resist = new EnchantParam();
            this.bonus = new BattleBonusParam();
            base..ctor();
            return;
        }

        public BaseStatus(BaseStatus src)
        {
            this.param = new StatusParam();
            this.element_assist = new ElementParam();
            this.element_resist = new ElementParam();
            this.enchant_assist = new EnchantParam();
            this.enchant_resist = new EnchantParam();
            this.bonus = new BattleBonusParam();
            base..ctor();
            src.CopyTo(this);
            return;
        }

        public void Add(BaseStatus src)
        {
            this.param.Add(src.param);
            this.element_assist.Add(src.element_assist);
            this.element_resist.Add(src.element_resist);
            this.enchant_assist.Add(src.enchant_assist);
            this.enchant_resist.Add(src.enchant_resist);
            this.bonus.Add(src.bonus);
            return;
        }

        public void AddConvRate(BaseStatus scale, BaseStatus base_status)
        {
            this.param.AddConvRate(scale.param, base_status.param);
            this.element_assist.AddConvRate(scale.element_assist, base_status.element_assist);
            this.element_resist.AddConvRate(scale.element_resist, base_status.element_resist);
            this.enchant_assist.AddConvRate(scale.enchant_assist, base_status.enchant_assist);
            this.enchant_resist.AddConvRate(scale.enchant_resist, base_status.enchant_resist);
            this.bonus.AddConvRate(scale.bonus, base_status.bonus);
            return;
        }

        public void AddRate(BaseStatus src)
        {
            this.param.AddRate(src.param);
            this.element_assist.AddRate(src.element_assist);
            this.element_resist.AddRate(src.element_resist);
            this.enchant_assist.AddRate(src.enchant_assist);
            this.enchant_resist.AddRate(src.enchant_resist);
            this.bonus.AddRate(src.bonus);
            return;
        }

        public void AddRate(StatusParam src)
        {
            this.param.AddRate(src);
            return;
        }

        public void ChoiceHighest(BaseStatus scale, BaseStatus base_status)
        {
            this.param.ChoiceHighest(scale.param, base_status.param);
            this.element_assist.ChoiceHighest(scale.element_assist, base_status.element_assist);
            this.element_resist.ChoiceHighest(scale.element_resist, base_status.element_resist);
            this.enchant_assist.ChoiceHighest(scale.enchant_assist, base_status.enchant_assist);
            this.enchant_resist.ChoiceHighest(scale.enchant_resist, base_status.enchant_resist);
            this.bonus.ChoiceHighest(scale.bonus, base_status.bonus);
            return;
        }

        public void ChoiceLowest(BaseStatus scale, BaseStatus base_status)
        {
            this.param.ChoiceLowest(scale.param, base_status.param);
            this.element_assist.ChoiceLowest(scale.element_assist, base_status.element_assist);
            this.element_resist.ChoiceLowest(scale.element_resist, base_status.element_resist);
            this.enchant_assist.ChoiceLowest(scale.enchant_assist, base_status.enchant_assist);
            this.enchant_resist.ChoiceLowest(scale.enchant_resist, base_status.enchant_resist);
            this.bonus.ChoiceLowest(scale.bonus, base_status.bonus);
            return;
        }

        public void Clear()
        {
            this.param.Clear();
            this.element_assist.Clear();
            this.element_resist.Clear();
            this.enchant_assist.Clear();
            this.enchant_resist.Clear();
            this.bonus.Clear();
            return;
        }

        public void CopyTo(BaseStatus dsc)
        {
            this.param.CopyTo(dsc.param);
            this.element_assist.CopyTo(dsc.element_assist);
            this.element_resist.CopyTo(dsc.element_resist);
            this.enchant_assist.CopyTo(dsc.enchant_assist);
            this.enchant_resist.CopyTo(dsc.enchant_resist);
            this.bonus.CopyTo(dsc.bonus);
            return;
        }

        public void Div(int div_val)
        {
            this.param.Div(div_val);
            this.element_assist.Div(div_val);
            this.element_resist.Div(div_val);
            this.enchant_assist.Div(div_val);
            this.enchant_resist.Div(div_val);
            this.bonus.Div(div_val);
            return;
        }

        public void Mul(int mul_val)
        {
            this.param.Mul(mul_val);
            this.element_assist.Mul(mul_val);
            this.element_resist.Mul(mul_val);
            this.enchant_assist.Mul(mul_val);
            this.enchant_resist.Mul(mul_val);
            this.bonus.Mul(mul_val);
            return;
        }

        public void ReplaceHighest(BaseStatus comp)
        {
            this.param.ReplceHighest(comp.param);
            this.element_assist.ReplceHighest(comp.element_assist);
            this.element_resist.ReplceHighest(comp.element_resist);
            this.enchant_assist.ReplceHighest(comp.enchant_assist);
            this.enchant_resist.ReplceHighest(comp.enchant_resist);
            this.bonus.ReplceHighest(comp.bonus);
            return;
        }

        public void ReplaceLowest(BaseStatus comp)
        {
            this.param.ReplceLowest(comp.param);
            this.element_assist.ReplceLowest(comp.element_assist);
            this.element_resist.ReplceLowest(comp.element_resist);
            this.enchant_assist.ReplceLowest(comp.enchant_assist);
            this.enchant_resist.ReplceLowest(comp.enchant_resist);
            this.bonus.ReplceLowest(comp.bonus);
            return;
        }

        public void Sub(BaseStatus src)
        {
            this.param.Sub(src.param);
            this.element_assist.Sub(src.element_assist);
            this.element_resist.Sub(src.element_resist);
            this.enchant_assist.Sub(src.enchant_assist);
            this.enchant_resist.Sub(src.enchant_resist);
            this.bonus.Sub(src.bonus);
            return;
        }

        public void SubConvRate(BaseStatus scale, BaseStatus base_status)
        {
            this.param.SubConvRate(scale.param, base_status.param);
            this.element_assist.SubConvRate(scale.element_assist, base_status.element_assist);
            this.element_resist.SubConvRate(scale.element_resist, base_status.element_resist);
            this.enchant_assist.SubConvRate(scale.enchant_assist, base_status.enchant_assist);
            this.enchant_resist.SubConvRate(scale.enchant_resist, base_status.enchant_resist);
            this.bonus.SubConvRate(scale.bonus, base_status.bonus);
            return;
        }

        public void Swap(BaseStatus src, bool is_rev)
        {
            this.param.Swap(src.param, is_rev);
            this.element_assist.Swap(src.element_assist, is_rev);
            this.element_resist.Swap(src.element_resist, is_rev);
            this.enchant_assist.Swap(src.enchant_assist, is_rev);
            this.enchant_resist.Swap(src.enchant_resist, is_rev);
            this.bonus.Swap(src.bonus, is_rev);
            return;
        }

        public OInt this[StatusTypes type]
        {
            get
            {
                return this.param[type];
            }
            set
            {
                this.param[type] = value;
                return;
            }
        }

        public OInt this[EnchantCategory category, EElement element]
        {
            get
            {
                return ((category != null) ? this.element_resist[element] : this.element_assist[element]);
            }
            set
            {
                if (category != null)
                {
                    goto Label_001D;
                }
                this.element_assist[element] = value;
                goto Label_002F;
            Label_001D:
                this.element_resist[element] = value;
            Label_002F:
                return;
            }
        }

        public OInt this[EnchantCategory category, EnchantTypes type]
        {
            get
            {
                return ((category != null) ? this.enchant_resist[type] : this.enchant_assist[type]);
            }
            set
            {
                if (category != null)
                {
                    goto Label_001D;
                }
                this.enchant_assist[type] = value;
                goto Label_002F;
            Label_001D:
                this.enchant_resist[type] = value;
            Label_002F:
                return;
            }
        }

        public OInt this[BattleBonus type]
        {
            get
            {
                return this.bonus[type];
            }
            set
            {
                this.bonus[type] = value;
                return;
            }
        }

        public OInt this[ParamTypes type]
        {
            get
            {
                ParamTypes types;
                types = type;
                switch ((types - 1))
                {
                    case 0:
                        goto Label_026F;

                    case 1:
                        goto Label_0277;

                    case 2:
                        goto Label_027F;

                    case 3:
                        goto Label_0287;

                    case 4:
                        goto Label_028F;

                    case 5:
                        goto Label_0297;

                    case 6:
                        goto Label_029F;

                    case 7:
                        goto Label_02A7;

                    case 8:
                        goto Label_02AF;

                    case 9:
                        goto Label_02B7;

                    case 10:
                        goto Label_02BF;

                    case 11:
                        goto Label_02C8;

                    case 12:
                        goto Label_02D1;

                    case 13:
                        goto Label_02DA;

                    case 14:
                        goto Label_02E3;

                    case 15:
                        goto Label_062A;

                    case 0x10:
                        goto Label_0632;

                    case 0x11:
                        goto Label_063A;

                    case 0x12:
                        goto Label_02EC;

                    case 0x13:
                        goto Label_02F5;

                    case 20:
                        goto Label_02FE;

                    case 0x15:
                        goto Label_0307;

                    case 0x16:
                        goto Label_0310;

                    case 0x17:
                        goto Label_0319;

                    case 0x18:
                        goto Label_0322;

                    case 0x19:
                        goto Label_032B;

                    case 0x1a:
                        goto Label_0334;

                    case 0x1b:
                        goto Label_033D;

                    case 0x1c:
                        goto Label_0346;

                    case 0x1d:
                        goto Label_034F;

                    case 30:
                        goto Label_0358;

                    case 0x1f:
                        goto Label_0361;

                    case 0x20:
                        goto Label_036A;

                    case 0x21:
                        goto Label_0373;

                    case 0x22:
                        goto Label_037D;

                    case 0x23:
                        goto Label_0387;

                    case 0x24:
                        goto Label_0391;

                    case 0x25:
                        goto Label_039B;

                    case 0x26:
                        goto Label_03A5;

                    case 0x27:
                        goto Label_03AF;

                    case 40:
                        goto Label_03B9;

                    case 0x29:
                        goto Label_03C3;

                    case 0x2a:
                        goto Label_03CD;

                    case 0x2b:
                        goto Label_03D7;

                    case 0x2c:
                        goto Label_03E1;

                    case 0x2d:
                        goto Label_03EB;

                    case 0x2e:
                        goto Label_03F5;

                    case 0x2f:
                        goto Label_07DA;

                    case 0x30:
                        goto Label_048B;

                    case 0x31:
                        goto Label_0494;

                    case 50:
                        goto Label_049D;

                    case 0x33:
                        goto Label_04A6;

                    case 0x34:
                        goto Label_04AF;

                    case 0x35:
                        goto Label_04B8;

                    case 0x36:
                        goto Label_04C1;

                    case 0x37:
                        goto Label_04CA;

                    case 0x38:
                        goto Label_04D3;

                    case 0x39:
                        goto Label_04DC;

                    case 0x3a:
                        goto Label_04E5;

                    case 0x3b:
                        goto Label_04EE;

                    case 60:
                        goto Label_04F7;

                    case 0x3d:
                        goto Label_0500;

                    case 0x3e:
                        goto Label_0509;

                    case 0x3f:
                        goto Label_0512;

                    case 0x40:
                        goto Label_051C;

                    case 0x41:
                        goto Label_0526;

                    case 0x42:
                        goto Label_0530;

                    case 0x43:
                        goto Label_053A;

                    case 0x44:
                        goto Label_0544;

                    case 0x45:
                        goto Label_054E;

                    case 70:
                        goto Label_0558;

                    case 0x47:
                        goto Label_0562;

                    case 0x48:
                        goto Label_056C;

                    case 0x49:
                        goto Label_0576;

                    case 0x4a:
                        goto Label_0580;

                    case 0x4b:
                        goto Label_058A;

                    case 0x4c:
                        goto Label_0594;

                    case 0x4d:
                        goto Label_07DA;

                    case 0x4e:
                        goto Label_0642;

                    case 0x4f:
                        goto Label_064A;

                    case 80:
                        goto Label_0652;

                    case 0x51:
                        goto Label_0696;

                    case 0x52:
                        goto Label_069F;

                    case 0x53:
                        goto Label_06A8;

                    case 0x54:
                        goto Label_065A;

                    case 0x55:
                        goto Label_0662;

                    case 0x56:
                        goto Label_066A;

                    case 0x57:
                        goto Label_0672;

                    case 0x58:
                        goto Label_067B;

                    case 0x59:
                        goto Label_0684;

                    case 90:
                        goto Label_068D;

                    case 0x5b:
                        goto Label_06B1;

                    case 0x5c:
                        goto Label_06BA;

                    case 0x5d:
                        goto Label_06C3;

                    case 0x5e:
                        goto Label_06CC;

                    case 0x5f:
                        goto Label_06D5;

                    case 0x60:
                        goto Label_06DE;

                    case 0x61:
                        goto Label_06E7;

                    case 0x62:
                        goto Label_06F0;

                    case 0x63:
                        goto Label_06F9;

                    case 100:
                        goto Label_0702;

                    case 0x65:
                        goto Label_070B;

                    case 0x66:
                        goto Label_03FF;

                    case 0x67:
                        goto Label_059E;

                    case 0x68:
                        goto Label_0409;

                    case 0x69:
                        goto Label_05A8;

                    case 0x6a:
                        goto Label_0714;

                    case 0x6b:
                        goto Label_071D;

                    case 0x6c:
                        goto Label_0726;

                    case 0x6d:
                        goto Label_072F;

                    case 110:
                        goto Label_0738;

                    case 0x6f:
                        goto Label_0741;

                    case 0x70:
                        goto Label_074A;

                    case 0x71:
                        goto Label_0753;

                    case 0x72:
                        goto Label_075C;

                    case 0x73:
                        goto Label_0765;

                    case 0x74:
                        goto Label_076E;

                    case 0x75:
                        goto Label_0777;

                    case 0x76:
                        goto Label_0780;

                    case 0x77:
                        goto Label_0789;

                    case 120:
                        goto Label_0792;

                    case 0x79:
                        goto Label_079B;

                    case 0x7a:
                        goto Label_0413;

                    case 0x7b:
                        goto Label_041D;

                    case 0x7c:
                        goto Label_05B2;

                    case 0x7d:
                        goto Label_05BC;

                    case 0x7e:
                        goto Label_0427;

                    case 0x7f:
                        goto Label_0431;

                    case 0x80:
                        goto Label_05C6;

                    case 0x81:
                        goto Label_05D0;

                    case 130:
                        goto Label_043B;

                    case 0x83:
                        goto Label_0445;

                    case 0x84:
                        goto Label_044F;

                    case 0x85:
                        goto Label_0459;

                    case 0x86:
                        goto Label_0463;

                    case 0x87:
                        goto Label_046D;

                    case 0x88:
                        goto Label_05DA;

                    case 0x89:
                        goto Label_05E4;

                    case 0x8a:
                        goto Label_05EE;

                    case 0x8b:
                        goto Label_05F8;

                    case 140:
                        goto Label_0602;

                    case 0x8d:
                        goto Label_060C;

                    case 0x8e:
                        goto Label_07A4;

                    case 0x8f:
                        goto Label_07AD;

                    case 0x90:
                        goto Label_07B6;

                    case 0x91:
                        goto Label_07BF;

                    case 0x92:
                        goto Label_07C8;

                    case 0x93:
                        goto Label_07D1;

                    case 0x94:
                        goto Label_0477;

                    case 0x95:
                        goto Label_0481;

                    case 150:
                        goto Label_0616;

                    case 0x97:
                        goto Label_0620;
                }
                goto Label_07DA;
            Label_026F:
                return this[0];
            Label_0277:
                return this[0];
            Label_027F:
                return this[1];
            Label_0287:
                return this[2];
            Label_028F:
                return this[3];
            Label_0297:
                return this[4];
            Label_029F:
                return this[5];
            Label_02A7:
                return this[6];
            Label_02AF:
                return this[7];
            Label_02B7:
                return this[8];
            Label_02BF:
                return this[9];
            Label_02C8:
                return this[10];
            Label_02D1:
                return this[11];
            Label_02DA:
                return this[12];
            Label_02E3:
                return this[13];
            Label_02EC:
                return this[0, 1];
            Label_02F5:
                return this[0, 2];
            Label_02FE:
                return this[0, 3];
            Label_0307:
                return this[0, 4];
            Label_0310:
                return this[0, 5];
            Label_0319:
                return this[0, 6];
            Label_0322:
                return this[0, 0];
            Label_032B:
                return this[0, 1];
            Label_0334:
                return this[0, 2];
            Label_033D:
                return this[0, 3];
            Label_0346:
                return this[0, 4];
            Label_034F:
                return this[0, 5];
            Label_0358:
                return this[0, 6];
            Label_0361:
                return this[0, 7];
            Label_036A:
                return this[0, 8];
            Label_0373:
                return this[0, 9];
            Label_037D:
                return this[0, 10];
            Label_0387:
                return this[0, 11];
            Label_0391:
                return this[0, 12];
            Label_039B:
                return this[0, 13];
            Label_03A5:
                return this[0, 14];
            Label_03AF:
                return this[0, 15];
            Label_03B9:
                return this[0, 0x10];
            Label_03C3:
                return this[0, 0x11];
            Label_03CD:
                return this[0, 0x12];
            Label_03D7:
                return this[0, 0x13];
            Label_03E1:
                return this[0, 20];
            Label_03EB:
                return this[0, 0x15];
            Label_03F5:
                return this[0, 0x16];
            Label_03FF:
                return this[0, 0x17];
            Label_0409:
                return this[0, 0x18];
            Label_0413:
                return this[0, 0x19];
            Label_041D:
                return this[0, 0x1a];
            Label_0427:
                return this[0, 0x1b];
            Label_0431:
                return this[0, 0x1c];
            Label_043B:
                return this[0, 0x1d];
            Label_0445:
                return this[0, 30];
            Label_044F:
                return this[0, 0x1f];
            Label_0459:
                return this[0, 0x20];
            Label_0463:
                return this[0, 0x21];
            Label_046D:
                return this[0, 0x22];
            Label_0477:
                return this[0, 0x23];
            Label_0481:
                return this[0, 0x24];
            Label_048B:
                return this[1, 1];
            Label_0494:
                return this[1, 2];
            Label_049D:
                return this[1, 3];
            Label_04A6:
                return this[1, 4];
            Label_04AF:
                return this[1, 5];
            Label_04B8:
                return this[1, 6];
            Label_04C1:
                return this[1, 0];
            Label_04CA:
                return this[1, 1];
            Label_04D3:
                return this[1, 2];
            Label_04DC:
                return this[1, 3];
            Label_04E5:
                return this[1, 4];
            Label_04EE:
                return this[1, 5];
            Label_04F7:
                return this[1, 6];
            Label_0500:
                return this[1, 7];
            Label_0509:
                return this[1, 8];
            Label_0512:
                return this[1, 9];
            Label_051C:
                return this[1, 10];
            Label_0526:
                return this[1, 11];
            Label_0530:
                return this[1, 12];
            Label_053A:
                return this[1, 13];
            Label_0544:
                return this[1, 14];
            Label_054E:
                return this[1, 15];
            Label_0558:
                return this[1, 0x10];
            Label_0562:
                return this[1, 0x11];
            Label_056C:
                return this[1, 0x12];
            Label_0576:
                return this[1, 0x13];
            Label_0580:
                return this[1, 20];
            Label_058A:
                return this[1, 0x15];
            Label_0594:
                return this[1, 0x16];
            Label_059E:
                return this[1, 0x17];
            Label_05A8:
                return this[1, 0x18];
            Label_05B2:
                return this[1, 0x19];
            Label_05BC:
                return this[1, 0x1a];
            Label_05C6:
                return this[1, 0x1b];
            Label_05D0:
                return this[1, 0x1c];
            Label_05DA:
                return this[1, 0x1d];
            Label_05E4:
                return this[1, 30];
            Label_05EE:
                return this[1, 0x1f];
            Label_05F8:
                return this[1, 0x20];
            Label_0602:
                return this[1, 0x21];
            Label_060C:
                return this[1, 0x22];
            Label_0616:
                return this[1, 0x23];
            Label_0620:
                return this[1, 0x24];
            Label_062A:
                return this[0];
            Label_0632:
                return this[1];
            Label_063A:
                return this[2];
            Label_0642:
                return this[3];
            Label_064A:
                return this[4];
            Label_0652:
                return this[5];
            Label_065A:
                return this[6];
            Label_0662:
                return this[7];
            Label_066A:
                return this[8];
            Label_0672:
                return this[9];
            Label_067B:
                return this[10];
            Label_0684:
                return this[11];
            Label_068D:
                return this[12];
            Label_0696:
                return this[13];
            Label_069F:
                return this[14];
            Label_06A8:
                return this[15];
            Label_06B1:
                return this[0x10];
            Label_06BA:
                return this[0x11];
            Label_06C3:
                return this[0x12];
            Label_06CC:
                return this[0x13];
            Label_06D5:
                return this[20];
            Label_06DE:
                return this[0x15];
            Label_06E7:
                return this[0x16];
            Label_06F0:
                return this[0x17];
            Label_06F9:
                return this[0x18];
            Label_0702:
                return this[0x19];
            Label_070B:
                return this[0x1a];
            Label_0714:
                return this[0x1b];
            Label_071D:
                return this[0x1c];
            Label_0726:
                return this[0x1d];
            Label_072F:
                return this[30];
            Label_0738:
                return this[0x1f];
            Label_0741:
                return this[0x20];
            Label_074A:
                return this[0x21];
            Label_0753:
                return this[0x22];
            Label_075C:
                return this[0x23];
            Label_0765:
                return this[0x24];
            Label_076E:
                return this[0x25];
            Label_0777:
                return this[0x26];
            Label_0780:
                return this[0x27];
            Label_0789:
                return this[40];
            Label_0792:
                return this[0x29];
            Label_079B:
                return this[0x2a];
            Label_07A4:
                return this[0x2b];
            Label_07AD:
                return this[0x2c];
            Label_07B6:
                return this[0x2d];
            Label_07BF:
                return this[0x2e];
            Label_07C8:
                return this[0x2f];
            Label_07D1:
                return this[0x30];
            Label_07DA:;
            Label_07DF:
                return 0;
            }
        }
    }
}

