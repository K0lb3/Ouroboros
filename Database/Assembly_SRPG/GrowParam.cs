namespace SRPG
{
    using System;
    using System.Collections.Generic;

    public class GrowParam
    {
        public string type;
        public List<GrowSample> curve;

        public GrowParam()
        {
            base..ctor();
            return;
        }

        public unsafe void CalcLevelCurveStatus(int rank, ref BaseStatus result, UnitParam.Status ini_status, UnitParam.Status max_status)
        {
            int num;
            int num2;
            BaseStatus status;
            BaseStatus status2;
            int num3;
            long num4;
            int num5;
            long num6;
            StatusParam param;
            StatusParam param2;
            StatusParam param3;
            int num7;
            long num8;
            EnchantParam param4;
            EnchantParam param5;
            EnchantParam param6;
            int num9;
            long num10;
            StatusParam param7;
            StatusTypes types;
            OInt num11;
            num = this.GetLevelCap() - 1;
            num2 = rank - 1;
            *(result).bonus.Clear();
            *(result).enchant_assist.Clear();
            *(result).enchant_resist.Clear();
            *(result).element_assist.Clear();
            *(result).element_resist.Clear();
            ini_status.param.CopyTo(*(result).param);
            if (ini_status.enchant_resist == null)
            {
                goto Label_0078;
            }
            ini_status.enchant_resist.CopyTo(*(result).enchant_resist);
        Label_0078:
            if ((num2 >= 1) && (num >= 1))
            {
                goto Label_0087;
            }
            return;
        Label_0087:
            if (num2 < num)
            {
                goto Label_00C1;
            }
            max_status.param.CopyTo(*(result).param);
            if (max_status.enchant_resist == null)
            {
                goto Label_00C0;
            }
            max_status.enchant_resist.CopyTo(*(result).enchant_resist);
        Label_00C0:
            return;
        Label_00C1:
            status = new BaseStatus();
            status2 = new BaseStatus();
            num3 = 0;
            goto Label_039D;
        Label_00D5:
            num4 = (long) (this.curve[num3].lv - 1);
            num5 = num3;
            goto Label_011F;
        Label_00FA:
            num4 -= (long) this.curve[num5 - 1].lv;
            num5 -= 1;
        Label_011F:
            if (num5 > 0)
            {
                goto Label_00FA;
            }
            num6 = (((long) num2) >= num4) ? num4 : ((long) num2);
            param = ini_status.param;
            param2 = max_status.param;
            param3 = this.curve[num3].status.param;
            param3.CopyTo(status.param);
            status.param.Sub(status2.param);
            status2.param.Add(param3);
            num7 = 0;
            goto Label_021E;
        Label_0198:
            num8 = (long) (((param2[num7] - param[num7]) * status.param[num7]) / 100);
            if (num8 == null)
            {
                goto Label_0218;
            }
            num11 = param7[types];
            (param7 = *(result).param)[types = num7] = num11 + ((int) ((((0x186a0L * num8) / num4) * num6) / 0x186a0L));
        Label_0218:
            num7 += 1;
        Label_021E:
            if (num7 < status.param.Length)
            {
                goto Label_0198;
            }
            if (ini_status.enchant_resist == null)
            {
                goto Label_0359;
            }
            if (max_status.enchant_resist == null)
            {
                goto Label_0359;
            }
            param4 = ini_status.enchant_resist;
            param5 = max_status.enchant_resist;
            param6 = this.curve[num3].status.enchant_resist;
            param6.CopyTo(status.enchant_resist);
            status.enchant_resist.Sub(status2.enchant_resist);
            status2.enchant_resist.Add(param6);
            num9 = 0;
            goto Label_0345;
        Label_02A4:
            num10 = (long) (((*(&(param5.values[num9])) - *(&(param4.values[num9]))) * *(&(status.enchant_resist.values[num9]))) / 100);
            if (num10 == null)
            {
                goto Label_033F;
            }
            *(&(*(result).enchant_resist.values[num9])) += (int) ((((0x186a0L * num10) / num4) * num6) / 0x186a0L);
        Label_033F:
            num9 += 1;
        Label_0345:
            if (num9 < ((int) status.enchant_resist.values.Length))
            {
                goto Label_02A4;
            }
        Label_0359:
            if (rank > this.curve[num3].lv)
            {
                goto Label_037B;
            }
            goto Label_03AF;
        Label_037B:
            num2 = (rank - this.curve[num3].lv) - 1;
            num3 += 1;
        Label_039D:
            if (num3 < this.curve.Count)
            {
                goto Label_00D5;
            }
        Label_03AF:
            return;
        }

        public int CalcLevelCurveValue(int rank, int ini, int max)
        {
            int num;
            int num2;
            int num3;
            int num4;
            int num5;
            int num6;
            long num7;
            int num8;
            long num9;
            long num10;
            num = this.GetLevelCap() - 1;
            num2 = rank - 1;
            if (ini != max)
            {
                goto Label_0016;
            }
            return ini;
        Label_0016:
            if ((num2 >= 1) && (num >= 1))
            {
                goto Label_0026;
            }
            return ini;
        Label_0026:
            if (num2 < num)
            {
                goto Label_002F;
            }
            return max;
        Label_002F:
            num3 = 0;
            num4 = 0;
            num5 = ini;
            num6 = 0;
            goto Label_0136;
        Label_003E:
            num7 = (long) (this.curve[num6].lv - 1);
            num8 = num6;
            goto Label_0088;
        Label_0063:
            num7 -= (long) this.curve[num8 - 1].lv;
            num8 -= 1;
        Label_0088:
            if (num8 > 0)
            {
                goto Label_0063;
            }
            num9 = (((long) num2) >= num7) ? num7 : ((long) num2);
            num3 = this.curve[num6].scale - num4;
            num4 += num3;
            num10 = (long) (((max - ini) * num3) / 100);
            if (num10 == null)
            {
                goto Label_00F2;
            }
            num5 += (int) ((((0x186a0L * num10) / num7) * num9) / 0x186a0L);
        Label_00F2:
            if (rank > this.curve[num6].lv)
            {
                goto Label_0114;
            }
            goto Label_0148;
        Label_0114:
            num2 = (rank - this.curve[num6].lv) - 1;
            num6 += 1;
        Label_0136:
            if (num6 < this.curve.Count)
            {
                goto Label_003E;
            }
        Label_0148:
            return num5;
        }

        public bool Deserialize(JSON_GrowParam json)
        {
            int num;
            int num2;
            GrowSample sample;
            if (json != null)
            {
                goto Label_0008;
            }
            return 0;
        Label_0008:
            this.type = json.type;
            if (json.curve == null)
            {
                goto Label_0990;
            }
            num = (int) json.curve.Length;
            this.curve = new List<GrowSample>(num);
            num2 = 0;
            goto Label_0989;
        Label_003B:
            sample = new GrowSample();
            sample.lv = json.curve[num2].lv;
            sample.scale = json.curve[num2].val;
            sample.status.param.hp = json.curve[num2].hp;
            sample.status.param.mp = json.curve[num2].mp;
            sample.status.param.atk = json.curve[num2].atk;
            sample.status.param.def = json.curve[num2].def;
            sample.status.param.mag = json.curve[num2].mag;
            sample.status.param.mnd = json.curve[num2].mnd;
            sample.status.param.dex = json.curve[num2].dex;
            sample.status.param.spd = json.curve[num2].spd;
            sample.status.param.cri = json.curve[num2].cri;
            sample.status.param.luk = json.curve[num2].luk;
            sample.status.element_assist.fire = json.curve[num2].afi;
            sample.status.element_assist.water = json.curve[num2].awa;
            sample.status.element_assist.wind = json.curve[num2].awi;
            sample.status.element_assist.thunder = json.curve[num2].ath;
            sample.status.element_assist.shine = json.curve[num2].ash;
            sample.status.element_assist.dark = json.curve[num2].ada;
            sample.status.element_resist.fire = json.curve[num2].rfi;
            sample.status.element_resist.water = json.curve[num2].rwa;
            sample.status.element_resist.wind = json.curve[num2].rwi;
            sample.status.element_resist.thunder = json.curve[num2].rth;
            sample.status.element_resist.shine = json.curve[num2].rsh;
            sample.status.element_resist.dark = json.curve[num2].rda;
            sample.status.enchant_assist.poison = json.curve[num2].apo;
            sample.status.enchant_assist.paralyse = json.curve[num2].apa;
            sample.status.enchant_assist.stun = json.curve[num2].ast;
            sample.status.enchant_assist.sleep = json.curve[num2].asl;
            sample.status.enchant_assist.charm = json.curve[num2].ach;
            sample.status.enchant_assist.stone = json.curve[num2].asn;
            sample.status.enchant_assist.blind = json.curve[num2].abl;
            sample.status.enchant_assist.notskl = json.curve[num2].ans;
            sample.status.enchant_assist.notmov = json.curve[num2].anm;
            sample.status.enchant_assist.notatk = json.curve[num2].ana;
            sample.status.enchant_assist.zombie = json.curve[num2].azo;
            sample.status.enchant_assist.death = json.curve[num2].ade;
            sample.status.enchant_assist.knockback = json.curve[num2].akn;
            sample.status.enchant_assist.berserk = json.curve[num2].abe;
            sample.status.enchant_assist.resist_buff = json.curve[num2].abf;
            sample.status.enchant_assist.resist_debuff = json.curve[num2].adf;
            sample.status.enchant_assist.stop = json.curve[num2].acs;
            sample.status.enchant_assist.fast = json.curve[num2].acu;
            sample.status.enchant_assist.slow = json.curve[num2].acd;
            sample.status.enchant_assist.donsoku = json.curve[num2].ado;
            sample.status.enchant_assist.rage = json.curve[num2].ara;
            sample.status.enchant_assist.dec_ct = json.curve[num2].adc;
            sample.status.enchant_assist.inc_ct = json.curve[num2].aic;
            sample.status.enchant_resist.poison = json.curve[num2].rpo;
            sample.status.enchant_resist.paralyse = json.curve[num2].rpa;
            sample.status.enchant_resist.stun = json.curve[num2].rst;
            sample.status.enchant_resist.sleep = json.curve[num2].rsl;
            sample.status.enchant_resist.charm = json.curve[num2].rch;
            sample.status.enchant_resist.stone = json.curve[num2].rsn;
            sample.status.enchant_resist.blind = json.curve[num2].rbl;
            sample.status.enchant_resist.notskl = json.curve[num2].rns;
            sample.status.enchant_resist.notmov = json.curve[num2].rnm;
            sample.status.enchant_resist.notatk = json.curve[num2].rna;
            sample.status.enchant_resist.zombie = json.curve[num2].rzo;
            sample.status.enchant_resist.death = json.curve[num2].rde;
            sample.status.enchant_resist.knockback = json.curve[num2].rkn;
            sample.status.enchant_resist.berserk = json.curve[num2].rbe;
            sample.status.enchant_resist.resist_buff = json.curve[num2].rbf;
            sample.status.enchant_resist.resist_debuff = json.curve[num2].rdf;
            sample.status.enchant_resist.stop = json.curve[num2].rcs;
            sample.status.enchant_resist.fast = json.curve[num2].rcu;
            sample.status.enchant_resist.slow = json.curve[num2].rcd;
            sample.status.enchant_resist.donsoku = json.curve[num2].rdo;
            sample.status.enchant_resist.rage = json.curve[num2].rra;
            sample.status.enchant_resist.dec_ct = json.curve[num2].rdc;
            sample.status.enchant_resist.inc_ct = json.curve[num2].ric;
            this.curve.Add(sample);
            num2 += 1;
        Label_0989:
            if (num2 < num)
            {
                goto Label_003B;
            }
        Label_0990:
            return 1;
        }

        public int GetLevelCap()
        {
            return (((this.curve == null) || (this.curve.Count <= 0)) ? 0 : this.curve[this.curve.Count - 1].lv);
        }
    }
}

