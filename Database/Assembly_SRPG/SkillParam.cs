namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class SkillParam
    {
        public static readonly int MAX_PARAMTYPES;
        public string iname;
        public string name;
        public string expr;
        public string motion;
        public string effect;
        public string defend_effect;
        public string weapon;
        public string tokkou;
        public int tk_rate;
        public ESkillType type;
        public ESkillTiming timing;
        public ESkillCondition condition;
        public OInt lvcap;
        public OInt cost;
        public OInt count;
        public OInt rate;
        public OInt back_defrate;
        public OInt side_defrate;
        public OInt ignore_defense_rate;
        public ELineType line_type;
        public ESelectType select_range;
        public OInt range_min;
        public OInt range_max;
        public ESelectType select_scope;
        public OInt scope;
        public OInt effect_height;
        public SkillFlags flags;
        public OInt hp_cost_rate;
        public OInt hp_cost;
        public OInt random_hit_rate;
        public ECastTypes cast_type;
        public SkillRankUpValue cast_speed;
        public ESkillTarget target;
        public SkillEffectTypes effect_type;
        public SkillRankUpValue effect_rate;
        public SkillRankUpValue effect_value;
        public SkillRankUpValue effect_range;
        public SkillParamCalcTypes effect_calc;
        public OInt effect_hprate;
        public OInt effect_mprate;
        public OInt effect_dead_rate;
        public OInt effect_lvrate;
        public OInt absorb_damage_rate;
        public EElement element_type;
        public SkillRankUpValue element_value;
        public AttackTypes attack_type;
        public AttackDetailTypes attack_detail;
        public DamageTypes reaction_damage_type;
        public List<AttackDetailTypes> reaction_det_lists;
        public SkillRankUpValue control_damage_rate;
        public SkillRankUpValue control_damage_value;
        public SkillParamCalcTypes control_damage_calc;
        public SkillRankUpValue control_ct_rate;
        public SkillRankUpValue control_ct_value;
        public SkillParamCalcTypes control_ct_calc;
        public string target_buff_iname;
        public string target_cond_iname;
        public string self_buff_iname;
        public string self_cond_iname;
        public ShieldTypes shield_type;
        public DamageTypes shield_damage_type;
        public SkillRankUpValue shield_turn;
        public SkillRankUpValue shield_value;
        public string job;
        public OInt ComboNum;
        public OInt ComboDamageRate;
        public OBool IsCritical;
        public JewelDamageTypes JewelDamageType;
        public OInt JewelDamageValue;
        public OBool IsJewelAbsorb;
        public OInt DuplicateCount;
        public string SceneName;
        public string CollaboMainId;
        public OInt CollaboHeight;
        public OInt KnockBackRate;
        public OInt KnockBackVal;
        public eKnockBackDir KnockBackDir;
        public eKnockBackDs KnockBackDs;
        public eDamageDispType DamageDispType;
        public eTeleportType TeleportType;
        public ESkillTarget TeleportTarget;
        public int TeleportHeight;
        public bool TeleportIsMove;
        public List<string> ReplaceTargetIdLists;
        public List<string> ReplaceChangeIdLists;
        public List<string> AbilityReplaceTargetIdLists;
        public List<string> AbilityReplaceChangeIdLists;
        public string CollaboVoiceId;
        public int CollaboVoicePlayDelayFrame;
        public string ReplacedTargetId;
        public string TrickId;
        public eTrickSetType TrickSetType;
        public string BreakObjId;
        public string MapEffectDesc;
        public int WeatherRate;
        public string WeatherId;
        public int ElementSpcAtkRate;
        public int MaxDamageValue;
        public string CutInConceptCardId;
        public int JudgeHpVal;
        public SkillParamCalcTypes JudgeHpCalc;
        public string AcFromAbilId;
        public string AcToAbilId;
        public int AcTurn;
        public OInt EffectHitTargetNumRate;
        public eAbsorbAndGive AbsorbAndGive;
        public eSkillTargetEx TargetEx;
        public int JumpSpcAtkRate;

        static SkillParam()
        {
            MAX_PARAMTYPES = (int) Enum.GetNames(typeof(ParamTypes)).Length;
            return;
        }

        public SkillParam()
        {
            base..ctor();
            return;
        }

        public int CalcCurrentRankValue(int rank, int rankcap, SkillRankUpValue param)
        {
            return ((param == null) ? 0 : this.CalcCurrentRankValue(rank, rankcap, param.ini, param.max));
        }

        public int CalcCurrentRankValue(int rank, int rankcap, int ini, int max)
        {
            int num;
            int num2;
            int num3;
            int num4;
            num = rankcap - 1;
            num2 = rank - 1;
            if (ini != max)
            {
                goto Label_0012;
            }
            return ini;
        Label_0012:
            if (num2 < 1)
            {
                goto Label_0020;
            }
            if (num >= 1)
            {
                goto Label_0022;
            }
        Label_0020:
            return ini;
        Label_0022:
            if (num2 < num)
            {
                goto Label_002C;
            }
            return max;
        Label_002C:
            num3 = ((max - ini) * 100) / num;
            num4 = ini + ((num3 * num2) / 100);
            return num4;
        }

        public static int CalcSkillEffectValue(SkillParamCalcTypes calctype, int skillval, int target)
        {
            SkillParamCalcTypes types;
            types = calctype;
            switch (types)
            {
                case 0:
                    goto Label_0019;

                case 1:
                    goto Label_001D;

                case 2:
                    goto Label_0026;
            }
            goto Label_0026;
        Label_0019:
            return (target + skillval);
        Label_001D:
            return (target + ((target * skillval) / 100));
        Label_0026:;
        Label_002B:
            return skillval;
        }

        public bool Deserialize(JSON_SkillParam json)
        {
            string str;
            string[] strArray;
            int num;
            string str2;
            string[] strArray2;
            int num2;
            string str3;
            string[] strArray3;
            int num3;
            string str4;
            string[] strArray4;
            int num4;
            int num5;
            int[] numArray;
            int num6;
            SkillEffectTypes types;
            ESelectType type;
            if (json != null)
            {
                goto Label_0008;
            }
            return 0;
        Label_0008:
            this.iname = json.iname;
            this.name = json.name;
            this.expr = json.expr;
            this.motion = json.motnm;
            this.effect = json.effnm;
            this.defend_effect = json.effdef;
            this.weapon = json.weapon;
            this.tokkou = json.tktag;
            this.tk_rate = json.tkrate;
            this.type = json.type;
            this.timing = json.timing;
            this.condition = json.cond;
            this.target = json.target;
            this.line_type = json.line;
            this.lvcap = json.cap;
            this.cost = json.cost;
            this.count = json.count;
            this.rate = json.rate;
            this.select_range = json.sran;
            this.range_min = json.rangemin;
            this.range_max = json.range;
            this.select_scope = json.ssco;
            this.scope = json.scope;
            this.effect_height = json.eff_h;
            this.back_defrate = json.bdb;
            this.side_defrate = json.sdb;
            this.ignore_defense_rate = json.idr;
            this.job = json.job;
            this.SceneName = json.scn;
            this.ComboNum = json.combo_num;
            this.ComboDamageRate = 100 - Math.Abs(json.combo_rate);
            this.IsCritical = (json.is_cri == 0) == 0;
            this.JewelDamageType = json.jdtype;
            this.JewelDamageValue = json.jdv;
            this.IsJewelAbsorb = (json.jdabs == 0) == 0;
            this.DuplicateCount = json.dupli;
            this.CollaboMainId = json.cs_main_id;
            this.CollaboHeight = json.cs_height;
            this.KnockBackRate = json.kb_rate;
            this.KnockBackVal = json.kb_val;
            this.KnockBackDir = json.kb_dir;
            this.KnockBackDs = json.kb_ds;
            this.DamageDispType = json.dmg_dt;
            this.ReplaceTargetIdLists = null;
            if (json.rp_tgt_ids == null)
            {
                goto Label_02CC;
            }
            this.ReplaceTargetIdLists = new List<string>();
            strArray = json.rp_tgt_ids;
            num = 0;
            goto Label_02C3;
        Label_02AF:
            str = strArray[num];
            this.ReplaceTargetIdLists.Add(str);
            num += 1;
        Label_02C3:
            if (num < ((int) strArray.Length))
            {
                goto Label_02AF;
            }
        Label_02CC:
            this.ReplaceChangeIdLists = null;
            if (json.rp_chg_ids == null)
            {
                goto Label_0327;
            }
            if (this.ReplaceTargetIdLists == null)
            {
                goto Label_0327;
            }
            this.ReplaceChangeIdLists = new List<string>();
            strArray2 = json.rp_chg_ids;
            num2 = 0;
            goto Label_031C;
        Label_0304:
            str2 = strArray2[num2];
            this.ReplaceChangeIdLists.Add(str2);
            num2 += 1;
        Label_031C:
            if (num2 < ((int) strArray2.Length))
            {
                goto Label_0304;
            }
        Label_0327:
            if (this.ReplaceTargetIdLists == null)
            {
                goto Label_036E;
            }
            if (this.ReplaceChangeIdLists == null)
            {
                goto Label_036E;
            }
            if (this.ReplaceTargetIdLists.Count == this.ReplaceChangeIdLists.Count)
            {
                goto Label_036E;
            }
            this.ReplaceTargetIdLists.Clear();
            this.ReplaceChangeIdLists.Clear();
        Label_036E:
            this.AbilityReplaceTargetIdLists = null;
            if (json.ab_rp_tgt_ids == null)
            {
                goto Label_03C0;
            }
            this.AbilityReplaceTargetIdLists = new List<string>();
            strArray3 = json.ab_rp_tgt_ids;
            num3 = 0;
            goto Label_03B5;
        Label_039B:
            str3 = strArray3[num3];
            this.AbilityReplaceTargetIdLists.Add(str3);
            num3 += 1;
        Label_03B5:
            if (num3 < ((int) strArray3.Length))
            {
                goto Label_039B;
            }
        Label_03C0:
            this.AbilityReplaceChangeIdLists = null;
            if (json.ab_rp_chg_ids == null)
            {
                goto Label_041D;
            }
            if (this.AbilityReplaceTargetIdLists == null)
            {
                goto Label_041D;
            }
            this.AbilityReplaceChangeIdLists = new List<string>();
            strArray4 = json.ab_rp_chg_ids;
            num4 = 0;
            goto Label_0412;
        Label_03F8:
            str4 = strArray4[num4];
            this.AbilityReplaceChangeIdLists.Add(str4);
            num4 += 1;
        Label_0412:
            if (num4 < ((int) strArray4.Length))
            {
                goto Label_03F8;
            }
        Label_041D:
            if (this.AbilityReplaceTargetIdLists == null)
            {
                goto Label_0464;
            }
            if (this.AbilityReplaceChangeIdLists == null)
            {
                goto Label_0464;
            }
            if (this.AbilityReplaceTargetIdLists.Count == this.AbilityReplaceChangeIdLists.Count)
            {
                goto Label_0464;
            }
            this.AbilityReplaceTargetIdLists.Clear();
            this.AbilityReplaceChangeIdLists.Clear();
        Label_0464:
            this.CollaboVoiceId = json.cs_voice;
            this.CollaboVoicePlayDelayFrame = json.cs_vp_df;
            this.TeleportType = json.tl_type;
            this.TeleportTarget = json.tl_target;
            this.TeleportHeight = json.tl_height;
            this.TeleportIsMove = (json.tl_is_mov == 0) == 0;
            this.TrickId = json.tr_id;
            this.TrickSetType = json.tr_set;
            this.BreakObjId = json.bo_id;
            this.MapEffectDesc = json.me_desc;
            this.WeatherRate = json.wth_rate;
            this.WeatherId = json.wth_id;
            this.ElementSpcAtkRate = json.elem_tk;
            this.MaxDamageValue = json.max_dmg;
            this.CutInConceptCardId = json.ci_cc_id;
            this.JudgeHpVal = json.jhp_val;
            this.JudgeHpCalc = json.jhp_calc;
            this.AcFromAbilId = json.ac_fr_ab_id;
            this.AcToAbilId = json.ac_to_ab_id;
            this.AcTurn = json.ac_turn;
            this.EffectHitTargetNumRate = json.eff_htnrate;
            this.AbsorbAndGive = json.aag;
            this.TargetEx = json.target_ex;
            this.JumpSpcAtkRate = json.jmp_tk;
            this.flags = 0;
            if (json.cutin == null)
            {
                goto Label_05B0;
            }
            this.flags |= 0x10;
        Label_05B0:
            if (json.isbtl == null)
            {
                goto Label_05CA;
            }
            this.flags |= 0x20;
        Label_05CA:
            if (json.chran == null)
            {
                goto Label_05E3;
            }
            this.flags |= 2;
        Label_05E3:
            if (json.sonoba == null)
            {
                goto Label_05FC;
            }
            this.flags |= 8;
        Label_05FC:
            if (json.pierce == null)
            {
                goto Label_0615;
            }
            this.flags |= 4;
        Label_0615:
            if (json.hbonus == null)
            {
                goto Label_062F;
            }
            this.flags |= 0x40;
        Label_062F:
            if (json.ehpa == null)
            {
                goto Label_064C;
            }
            this.flags |= 0x80;
        Label_064C:
            if (json.utgt == null)
            {
                goto Label_0669;
            }
            this.flags |= 0x100;
        Label_0669:
            if (json.ctbreak == null)
            {
                goto Label_0686;
            }
            this.flags |= 0x200;
        Label_0686:
            if (json.mpatk == null)
            {
                goto Label_06A3;
            }
            this.flags |= 0x400;
        Label_06A3:
            if (json.fhit == null)
            {
                goto Label_06C0;
            }
            this.flags |= 0x800;
        Label_06C0:
            if (json.suicide == null)
            {
                goto Label_06DD;
            }
            this.flags |= 0x1000;
        Label_06DD:
            if (json.sub_actuate == null)
            {
                goto Label_06FA;
            }
            this.flags |= 0x2000;
        Label_06FA:
            if (json.is_fixed == null)
            {
                goto Label_0717;
            }
            this.flags |= 0x4000;
        Label_0717:
            if (json.f_ulock == null)
            {
                goto Label_0734;
            }
            this.flags |= 0x8000;
        Label_0734:
            if (json.ad_react == null)
            {
                goto Label_0751;
            }
            this.flags |= 0x10000;
        Label_0751:
            if (json.ig_elem == null)
            {
                goto Label_076E;
            }
            this.flags |= 0x40000;
        Label_076E:
            if (json.is_pre_apply == null)
            {
                goto Label_078B;
            }
            this.flags |= 0x80000;
        Label_078B:
            if (json.jhp_over == null)
            {
                goto Label_07A8;
            }
            this.flags |= 0x100000;
        Label_07A8:
            if (json.is_mhm_dmg == null)
            {
                goto Label_07C5;
            }
            this.flags |= 0x200000;
        Label_07C5:
            if (json.ac_is_self == null)
            {
                goto Label_07E2;
            }
            this.flags |= 0x400000;
        Label_07E2:
            if (json.ac_is_reset == null)
            {
                goto Label_07FF;
            }
            this.flags |= 0x800000;
        Label_07FF:
            if (json.is_htndiv == null)
            {
                goto Label_081C;
            }
            this.flags |= 0x1000000;
        Label_081C:
            if (json.is_no_ccc == null)
            {
                goto Label_0839;
            }
            this.flags |= 0x2000000;
        Label_0839:
            if (json.jmpbreak == null)
            {
                goto Label_0856;
            }
            this.flags |= 0x4000000;
        Label_0856:
            this.hp_cost = json.hp_cost;
            this.hp_cost_rate = Math.Min(Math.Max(json.hp_cost_rate, 0), 100);
            this.random_hit_rate = json.rhit;
            this.effect_type = json.eff_type;
            this.effect_calc = json.eff_calc;
            this.effect_rate = new SkillRankUpValue();
            this.effect_rate.ini = json.eff_rate_ini;
            this.effect_rate.max = json.eff_rate_max;
            this.effect_value = new SkillRankUpValue();
            this.effect_value.ini = json.eff_val_ini;
            this.effect_value.max = json.eff_val_max;
            this.effect_range = new SkillRankUpValue();
            this.effect_range.ini = json.eff_range_ini;
            this.effect_range.max = json.eff_range_max;
            this.effect_hprate = json.eff_hprate;
            this.effect_mprate = json.eff_mprate;
            this.effect_dead_rate = json.eff_durate;
            this.effect_lvrate = json.eff_lvrate;
            this.attack_type = json.atk_type;
            this.attack_detail = json.atk_det;
            this.element_type = json.elem;
            this.element_value = null;
            if (this.element_type == null)
            {
                goto Label_0A04;
            }
            this.element_value = new SkillRankUpValue();
            this.element_value.ini = json.elem_ini;
            this.element_value.max = json.elem_max;
        Label_0A04:
            this.cast_type = json.ct_type;
            this.cast_speed = null;
            if (this.type != 1)
            {
                goto Label_0A70;
            }
            if (json.ct_spd_ini != null)
            {
                goto Label_0A39;
            }
            if (json.ct_spd_max == null)
            {
                goto Label_0A70;
            }
        Label_0A39:
            this.cast_speed = new SkillRankUpValue();
            this.cast_speed.ini = json.ct_spd_ini;
            this.cast_speed.max = json.ct_spd_max;
        Label_0A70:
            this.absorb_damage_rate = json.abs_d_rate;
            this.reaction_damage_type = json.react_d_type;
            this.reaction_det_lists = null;
            if (json.react_dets == null)
            {
                goto Label_0ADF;
            }
            this.reaction_det_lists = new List<AttackDetailTypes>();
            numArray = json.react_dets;
            num6 = 0;
            goto Label_0AD4;
        Label_0ABA:
            num5 = numArray[num6];
            this.reaction_det_lists.Add(num5);
            num6 += 1;
        Label_0AD4:
            if (num6 < ((int) numArray.Length))
            {
                goto Label_0ABA;
            }
        Label_0ADF:
            this.control_ct_rate = null;
            this.control_ct_value = null;
            if (this.control_ct_calc == 2)
            {
                goto Label_0B0F;
            }
            if (json.ct_val_ini != null)
            {
                goto Label_0B0F;
            }
            if (json.ct_val_max == null)
            {
                goto Label_0B89;
            }
        Label_0B0F:
            this.control_ct_rate = new SkillRankUpValue();
            this.control_ct_rate.ini = json.ct_rate_ini;
            this.control_ct_rate.max = json.ct_rate_max;
            this.control_ct_value = new SkillRankUpValue();
            this.control_ct_value.ini = json.ct_val_ini;
            this.control_ct_value.max = json.ct_val_max;
            this.control_ct_calc = json.ct_calc;
        Label_0B89:
            this.target_buff_iname = json.t_buff;
            this.target_cond_iname = json.t_cond;
            this.self_buff_iname = json.s_buff;
            this.self_cond_iname = json.s_cond;
            this.shield_type = json.shield_type;
            this.shield_damage_type = json.shield_d_type;
            this.shield_turn = null;
            this.shield_value = null;
            if (this.shield_type == null)
            {
                goto Label_0C80;
            }
            if (this.shield_damage_type == null)
            {
                goto Label_0C80;
            }
            this.shield_turn = new SkillRankUpValue();
            this.shield_turn.ini = json.shield_turn_ini;
            this.shield_turn.max = json.shield_turn_max;
            this.shield_value = new SkillRankUpValue();
            this.shield_value.ini = json.shield_ini;
            this.shield_value.max = json.shield_max;
            if (json.shield_reset == null)
            {
                goto Label_0C80;
            }
            this.flags |= 0x20000;
        Label_0C80:
            if (this.reaction_damage_type != null)
            {
                goto Label_0C96;
            }
            if (this.shield_damage_type == null)
            {
                goto Label_0D10;
            }
        Label_0C96:
            this.control_damage_rate = new SkillRankUpValue();
            this.control_damage_rate.ini = json.ctrl_d_rate_ini;
            this.control_damage_rate.max = json.ctrl_d_rate_max;
            this.control_damage_value = new SkillRankUpValue();
            this.control_damage_value.ini = json.ctrl_d_ini;
            this.control_damage_value.max = json.ctrl_d_max;
            this.control_damage_calc = json.ctrl_d_calc;
        Label_0D10:
            types = this.effect_type;
            switch ((types - 0x11))
            {
                case 0:
                    goto Label_0D70;

                case 1:
                    goto Label_0D70;

                case 2:
                    goto Label_0D3A;

                case 3:
                    goto Label_0D59;

                case 4:
                    goto Label_0D3A;

                case 5:
                    goto Label_0D70;
            }
        Label_0D3A:
            if (types == 2)
            {
                goto Label_0D59;
            }
            if (types == 9)
            {
                goto Label_0D59;
            }
            if (types == 0x1c)
            {
                goto Label_0D59;
            }
            goto Label_0D88;
        Label_0D59:
            if (this.attack_type != null)
            {
                goto Label_0D88;
            }
            this.attack_type = 1;
            goto Label_0D88;
        Label_0D70:
            this.scope = 0;
            this.select_scope = 0;
        Label_0D88:
            if (this.select_range != 3)
            {
                goto Label_0DBC;
            }
            this.select_scope = 3;
            this.scope = Math.Max(this.scope, 1);
            goto Label_0E57;
        Label_0DBC:
            switch ((this.select_range - 9))
            {
                case 0:
                    goto Label_0DE7;

                case 1:
                    goto Label_0DF4;

                case 2:
                    goto Label_0E1B;

                case 3:
                    goto Label_0E01;

                case 4:
                    goto Label_0E0E;
            }
            goto Label_0E1B;
        Label_0DE7:
            this.select_scope = 9;
            goto Label_0E1B;
        Label_0DF4:
            this.select_scope = 10;
            goto Label_0E1B;
        Label_0E01:
            this.select_scope = 12;
            goto Label_0E1B;
        Label_0E0E:
            this.select_scope = 13;
        Label_0E1B:
            switch ((this.select_scope - 9))
            {
                case 0:
                    goto Label_0E46;

                case 1:
                    goto Label_0E46;

                case 2:
                    goto Label_0E57;

                case 3:
                    goto Label_0E46;

                case 4:
                    goto Label_0E46;
            }
            goto Label_0E57;
        Label_0E46:
            this.scope = 1;
        Label_0E57:
            if (this.TeleportType == null)
            {
                goto Label_0EB9;
            }
            if (this.IsTargetGridNoUnit != null)
            {
                goto Label_0E80;
            }
            if (this.TeleportType == 2)
            {
                goto Label_0E80;
            }
            this.target = 5;
        Label_0E80:
            if (this.IsTargetTeleport == null)
            {
                goto Label_0EB9;
            }
            if (this.IsCastSkill() == null)
            {
                goto Label_0E9D;
            }
            this.cast_speed = null;
        Label_0E9D:
            if (this.scope == null)
            {
                goto Label_0EB9;
            }
            this.scope = 0;
        Label_0EB9:
            if (this.IsTargetValidGrid == null)
            {
                goto Label_0ED6;
            }
            if (this.IsTrickSkill() != null)
            {
                goto Label_0ED6;
            }
            this.target = 5;
        Label_0ED6:
            if (this.timing != 8)
            {
                goto Label_0EF5;
            }
            if (this.effect_type != 2)
            {
                goto Label_0EF5;
            }
            this.effect_type = 5;
        Label_0EF5:
            return 1;
        }

        public static bool IsAagTypeDiv(eAbsorbAndGive aag)
        {
            eAbsorbAndGive give;
            give = aag;
            switch ((give - 3))
            {
                case 0:
                    goto Label_001B;

                case 1:
                    goto Label_001D;

                case 2:
                    goto Label_001B;
            }
            goto Label_001D;
        Label_001B:
            return 1;
        Label_001D:
            return 0;
        }

        public static bool IsAagTypeGive(eAbsorbAndGive aag)
        {
            eAbsorbAndGive give;
            give = aag;
            switch ((give - 2))
            {
                case 0:
                    goto Label_001F;

                case 1:
                    goto Label_001F;

                case 2:
                    goto Label_001F;

                case 3:
                    goto Label_001F;
            }
            goto Label_0021;
        Label_001F:
            return 1;
        Label_0021:
            return 0;
        }

        public static bool IsAagTypeSame(eAbsorbAndGive aag)
        {
            eAbsorbAndGive give;
            give = aag;
            if (give == 4)
            {
                goto Label_0015;
            }
            if (give == 5)
            {
                goto Label_0015;
            }
            goto Label_0017;
        Label_0015:
            return 1;
        Label_0017:
            return 0;
        }

        public bool IsAcReset()
        {
            return (((this.flags & 0x800000) == 0) == 0);
        }

        public bool IsAcSelf()
        {
            return (((this.flags & 0x400000) == 0) == 0);
        }

        public bool IsAdvantage()
        {
            TrickParam param;
            SkillEffectTypes types;
            types = this.effect_type;
            if (types == 2)
            {
                goto Label_003A;
            }
            if (types == 6)
            {
                goto Label_003A;
            }
            if (types == 11)
            {
                goto Label_003C;
            }
            if (types == 20)
            {
                goto Label_003A;
            }
            if (types == 0x18)
            {
                goto Label_0057;
            }
            if (types == 0x1c)
            {
                goto Label_003A;
            }
            goto Label_0096;
        Label_003A:
            return 0;
        Label_003C:
            if (this.target == null)
            {
                goto Label_0053;
            }
            if (this.target != 1)
            {
                goto Label_0055;
            }
        Label_0053:
            return 1;
        Label_0055:
            return 0;
        Label_0057:
            if (string.IsNullOrEmpty(this.TrickId) != null)
            {
                goto Label_0096;
            }
            param = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetTrickParam(this.TrickId);
            if (param == null)
            {
                goto Label_0096;
            }
            if (param.DamageType != 1)
            {
                goto Label_0096;
            }
            return 0;
        Label_0096:
            return 1;
        }

        public bool IsAllDamageReaction()
        {
            return (((this.flags & 0x10000) == 0) == 0);
        }

        public bool IsAllEffect()
        {
            return (this.select_scope == 4);
        }

        public bool IsAreaSkill()
        {
            return ((this.scope > 0) ? 1 : (this.select_scope == 4));
        }

        public bool IsBattleSkill()
        {
            return (((this.flags & 0x20) == 0) == 0);
        }

        public bool IsCastBreak()
        {
            return (((this.flags & 0x200) == 0) == 0);
        }

        public bool IsCastSkill()
        {
            return ((this.cast_speed == null) == 0);
        }

        public bool IsChangeWeatherSkill()
        {
            if (this.effect_type != 0x1b)
            {
                goto Label_000F;
            }
            return 1;
        Label_000F:
            return 0;
        }

        public bool IsConditionSkill()
        {
            if (this.effect_type == 12)
            {
                goto Label_0027;
            }
            if (this.effect_type == 11)
            {
                goto Label_0027;
            }
            if (this.effect_type != 13)
            {
                goto Label_0029;
            }
        Label_0027:
            return 1;
        Label_0029:
            return 0;
        }

        public bool IsCutin()
        {
            return (((this.flags & 0x10) == 0) == 0);
        }

        public bool IsDamagedSkill()
        {
            if (this.effect_type == 2)
            {
                goto Label_0033;
            }
            if (this.effect_type == 9)
            {
                goto Label_0033;
            }
            if (this.effect_type == 20)
            {
                goto Label_0033;
            }
            if (this.effect_type != 0x1c)
            {
                goto Label_0040;
            }
        Label_0033:
            return ((this.attack_type == 0) == 0);
        Label_0040:
            return 0;
        }

        public bool IsEnableChangeRange()
        {
            return (((this.flags & 2) == 0) == 0);
        }

        public bool IsEnableHeightParamAdjust()
        {
            return (((this.flags & 0x80) == 0) == 0);
        }

        public bool IsEnableHeightRangeBonus()
        {
            if (IsTypeLaser(this.select_range) != null)
            {
                goto Label_0020;
            }
            if (IsTypeLaser(this.select_scope) == null)
            {
                goto Label_0022;
            }
        Label_0020:
            return 0;
        Label_0022:
            return (((this.flags & 0x40) == 0) == 0);
        }

        public bool IsEnableUnitLockTarget()
        {
            return (((this.flags & 0x100) == 0) == 0);
        }

        public bool IsFixedDamage()
        {
            return (((this.flags & 0x4000) == 0) == 0);
        }

        public bool IsForceHit()
        {
            return (((this.flags & 0x800) == 0) == 0);
        }

        public bool IsForceUnitLock()
        {
            return (((this.flags & 0x8000) == 0) == 0);
        }

        public bool IsHealSkill()
        {
            return ((this.effect_type == 4) ? 1 : (this.effect_type == 0x13));
        }

        public bool IsHitTargetNumDiv()
        {
            return (((this.flags & 0x1000000) == 0) == 0);
        }

        public bool IsIgnoreElement()
        {
            return (((this.flags & 0x40000) == 0) == 0);
        }

        public bool IsJewelAttack()
        {
            return (((this.flags & 0x400) == 0) == 0);
        }

        public bool IsJudgeHp(Unit unit)
        {
            int num;
            int num2;
            int num3;
            SkillParamCalcTypes types;
            if (unit == null)
            {
                goto Label_0012;
            }
            if (this.condition == 5)
            {
                goto Label_0014;
            }
        Label_0012:
            return 0;
        Label_0014:
            num = unit.CurrentStatus.param.hp;
            num2 = unit.MaximumStatus.param.hp;
            num3 = 0;
            if (this.JudgeHpCalc == 1)
            {
                goto Label_0055;
            }
            goto Label_0066;
        Label_0055:
            num3 = (num2 * this.JudgeHpVal) / 100;
            goto Label_0072;
        Label_0066:
            num3 = this.JudgeHpVal;
        Label_0072:
            if ((this.flags & 0x100000) == null)
            {
                goto Label_008B;
            }
            return ((num < num3) == 0);
        Label_008B:
            return ((num > num3) == 0);
        }

        public bool IsJumpBreak()
        {
            return (((this.flags & 0x4000000) == 0) == 0);
        }

        public bool IsLongRangeSkill()
        {
            if (this.range_max <= 1)
            {
                goto Label_0013;
            }
            return 1;
        Label_0013:
            if (this.range_max != null)
            {
                goto Label_0025;
            }
            return 0;
        Label_0025:
            return ((this.select_range == 1) ? 1 : (this.select_range == 4));
        }

        public bool IsMapSkill()
        {
            return (this.IsBattleSkill() == 0);
        }

        public bool IsMhmDamage()
        {
            return (((this.flags & 0x200000) == 0) == 0);
        }

        public bool IsNoChargeCalcCT()
        {
            return (((this.flags & 0x2000000) == 0) == 0);
        }

        public bool IsPierce()
        {
            return (((this.flags & 4) == 0) == 0);
        }

        public bool IsPrevApply()
        {
            return (((this.flags & 0x80000) == 0) == 0);
        }

        public bool IsReactionDet(AttackDetailTypes atk_detail_type)
        {
            if (this.reaction_det_lists == null)
            {
                goto Label_001B;
            }
            if (this.reaction_det_lists.Count != null)
            {
                goto Label_001D;
            }
        Label_001B:
            return 1;
        Label_001D:
            if (this.reaction_det_lists.Contains(atk_detail_type) == null)
            {
                goto Label_0030;
            }
            return 1;
        Label_0030:
            return 0;
        }

        public bool IsReactionSkill()
        {
            return (this.type == 4);
        }

        public bool IsSelfTargetSelect()
        {
            if (this.range_min <= 0)
            {
                goto Label_0013;
            }
            return 0;
        Label_0013:
            return (((this.flags & 8) == 0) == 0);
        }

        public bool IsSetBreakObjSkill()
        {
            if (this.effect_type != 0x1a)
            {
                goto Label_000F;
            }
            return 1;
        Label_000F:
            return 0;
        }

        public bool IsShieldReset()
        {
            return (((this.flags & 0x20000) == 0) == 0);
        }

        public bool IsSubActuate()
        {
            return (((this.flags & 0x2000) == 0) == 0);
        }

        public bool IsSuicide()
        {
            return (((this.flags & 0x1000) == 0) == 0);
        }

        public bool IsSupportSkill()
        {
            if (this.effect_type == 5)
            {
                goto Label_0018;
            }
            if (this.effect_type != 6)
            {
                goto Label_001A;
            }
        Label_0018:
            return 1;
        Label_001A:
            return 0;
        }

        public bool IsTransformSkill()
        {
            SkillEffectTypes types;
            types = this.effect_type;
            if (types == 0x19)
            {
                goto Label_001C;
            }
            if (types == 0x1d)
            {
                goto Label_001C;
            }
            goto Label_001E;
        Label_001C:
            return 1;
        Label_001E:
            return 0;
        }

        public bool IsTrickSkill()
        {
            if (this.effect_type != 0x18)
            {
                goto Label_000F;
            }
            return 1;
        Label_000F:
            return 0;
        }

        public static bool IsTypeLaser(ESelectType type)
        {
            ESelectType[] typeArray1;
            List<ESelectType> list;
            typeArray1 = new ESelectType[] { 3, 9, 10, 12, 13 };
            list = new List<ESelectType>(typeArray1);
            if (list.Contains(type) == null)
            {
                goto Label_0032;
            }
            return 1;
        Label_0032:
            return 0;
        }

        public static unsafe void UpdateReplaceSkill(List<SkillParam> sp_lists)
        {
            List<SkillParam>.Enumerator enumerator;
            SkillParam param;
            SkillParam param2;
            <UpdateReplaceSkill>c__AnonStorey2ED storeyed;
            <UpdateReplaceSkill>c__AnonStorey2EE storeyee;
            storeyed = new <UpdateReplaceSkill>c__AnonStorey2ED();
            enumerator = sp_lists.GetEnumerator();
        Label_000D:
            try
            {
                goto Label_0132;
            Label_0012:
                storeyed.sp = &enumerator.Current;
                if (storeyed.sp.ReplaceChangeIdLists == null)
                {
                    goto Label_0132;
                }
                if (storeyed.sp.ReplaceChangeIdLists.Count <= 0)
                {
                    goto Label_0132;
                }
                if (storeyed.sp.ReplaceTargetIdLists == null)
                {
                    goto Label_0132;
                }
                if (storeyed.sp.ReplaceTargetIdLists.Count <= 0)
                {
                    goto Label_0132;
                }
                if (storeyed.sp.ReplaceChangeIdLists.Count == storeyed.sp.ReplaceTargetIdLists.Count)
                {
                    goto Label_0095;
                }
                goto Label_0132;
            Label_0095:
                storeyee = new <UpdateReplaceSkill>c__AnonStorey2EE();
                storeyee.<>f__ref$749 = storeyed;
                storeyee.idx = 0;
                goto Label_0116;
            Label_00B1:
                param = sp_lists.Find(new Predicate<SkillParam>(storeyee.<>m__26F));
                param2 = sp_lists.Find(new Predicate<SkillParam>(storeyee.<>m__270));
                if (param == null)
                {
                    goto Label_0106;
                }
                if (param2 != null)
                {
                    goto Label_00EA;
                }
                goto Label_0106;
            Label_00EA:
                if (string.IsNullOrEmpty(param.ReplacedTargetId) == null)
                {
                    goto Label_0106;
                }
                param.ReplacedTargetId = param2.iname;
            Label_0106:
                storeyee.idx += 1;
            Label_0116:
                if (storeyee.idx < storeyed.sp.ReplaceChangeIdLists.Count)
                {
                    goto Label_00B1;
                }
            Label_0132:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0012;
                }
                goto Label_014F;
            }
            finally
            {
            Label_0143:
                ((List<SkillParam>.Enumerator) enumerator).Dispose();
            }
        Label_014F:
            return;
        }

        public bool IsTargetGridNoUnit
        {
            get
            {
                return (this.target == 5);
            }
        }

        public bool IsTargetValidGrid
        {
            get
            {
                return (this.target == 6);
            }
        }

        public bool IsSkillCountNoLimit
        {
            get
            {
                return (this.count == 0);
            }
        }

        public bool IsTargetTeleport
        {
            get
            {
                return ((this.TeleportType != 2) ? 0 : (this.IsTargetGridNoUnit == 0));
            }
        }

        [CompilerGenerated]
        private sealed class <UpdateReplaceSkill>c__AnonStorey2ED
        {
            internal SkillParam sp;

            public <UpdateReplaceSkill>c__AnonStorey2ED()
            {
                base..ctor();
                return;
            }
        }

        [CompilerGenerated]
        private sealed class <UpdateReplaceSkill>c__AnonStorey2EE
        {
            internal int idx;
            internal SkillParam.<UpdateReplaceSkill>c__AnonStorey2ED <>f__ref$749;

            public <UpdateReplaceSkill>c__AnonStorey2EE()
            {
                base..ctor();
                return;
            }

            internal bool <>m__26F(SkillParam skill)
            {
                return (skill.iname == this.<>f__ref$749.sp.ReplaceChangeIdLists[this.idx]);
            }

            internal bool <>m__270(SkillParam skill)
            {
                return (skill.iname == this.<>f__ref$749.sp.ReplaceTargetIdLists[this.idx]);
            }
        }
    }
}

