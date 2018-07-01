// Decompiled with JetBrains decompiler
// Type: SRPG.SkillParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;

namespace SRPG
{
  public class SkillParam
  {
    public static readonly int MAX_PARAMTYPES = Enum.GetNames(typeof (ParamTypes)).Length;
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

    public bool Deserialize(JSON_SkillParam json)
    {
      if (json == null)
        return false;
      this.iname = json.iname;
      this.name = json.name;
      this.expr = json.expr;
      this.motion = json.motnm;
      this.effect = json.effnm;
      this.defend_effect = json.effdef;
      this.weapon = json.weapon;
      this.tokkou = json.tktag;
      this.tk_rate = json.tkrate;
      this.type = (ESkillType) json.type;
      this.timing = (ESkillTiming) json.timing;
      this.condition = (ESkillCondition) json.cond;
      this.target = (ESkillTarget) json.target;
      this.line_type = (ELineType) json.line;
      this.lvcap = (OInt) json.cap;
      this.cost = (OInt) json.cost;
      this.count = (OInt) json.count;
      this.rate = (OInt) json.rate;
      this.select_range = (ESelectType) json.sran;
      this.range_min = (OInt) json.rangemin;
      this.range_max = (OInt) json.range;
      this.select_scope = (ESelectType) json.ssco;
      this.scope = (OInt) json.scope;
      this.effect_height = (OInt) json.eff_h;
      this.back_defrate = (OInt) json.bdb;
      this.side_defrate = (OInt) json.sdb;
      this.ignore_defense_rate = (OInt) json.idr;
      this.job = json.job;
      this.SceneName = json.scn;
      this.ComboNum = (OInt) json.combo_num;
      this.ComboDamageRate = (OInt) (100 - Math.Abs(json.combo_rate));
      this.IsCritical = (OBool) (json.is_cri != 0);
      this.JewelDamageType = (JewelDamageTypes) json.jdtype;
      this.JewelDamageValue = (OInt) json.jdv;
      this.IsJewelAbsorb = (OBool) (json.jdabs != 0);
      this.DuplicateCount = (OInt) json.dupli;
      this.CollaboMainId = json.cs_main_id;
      this.CollaboHeight = (OInt) json.cs_height;
      this.KnockBackRate = (OInt) json.kb_rate;
      this.KnockBackVal = (OInt) json.kb_val;
      this.KnockBackDir = (eKnockBackDir) json.kb_dir;
      this.KnockBackDs = (eKnockBackDs) json.kb_ds;
      this.DamageDispType = (eDamageDispType) json.dmg_dt;
      this.ReplaceTargetIdLists = (List<string>) null;
      if (json.rp_tgt_ids != null)
      {
        this.ReplaceTargetIdLists = new List<string>();
        foreach (string rpTgtId in json.rp_tgt_ids)
          this.ReplaceTargetIdLists.Add(rpTgtId);
      }
      this.ReplaceChangeIdLists = (List<string>) null;
      if (json.rp_chg_ids != null && this.ReplaceTargetIdLists != null)
      {
        this.ReplaceChangeIdLists = new List<string>();
        foreach (string rpChgId in json.rp_chg_ids)
          this.ReplaceChangeIdLists.Add(rpChgId);
      }
      if (this.ReplaceTargetIdLists != null && this.ReplaceChangeIdLists != null && this.ReplaceTargetIdLists.Count != this.ReplaceChangeIdLists.Count)
      {
        this.ReplaceTargetIdLists.Clear();
        this.ReplaceChangeIdLists.Clear();
      }
      this.AbilityReplaceTargetIdLists = (List<string>) null;
      if (json.ab_rp_tgt_ids != null)
      {
        this.AbilityReplaceTargetIdLists = new List<string>();
        foreach (string abRpTgtId in json.ab_rp_tgt_ids)
          this.AbilityReplaceTargetIdLists.Add(abRpTgtId);
      }
      this.AbilityReplaceChangeIdLists = (List<string>) null;
      if (json.ab_rp_chg_ids != null && this.AbilityReplaceTargetIdLists != null)
      {
        this.AbilityReplaceChangeIdLists = new List<string>();
        foreach (string abRpChgId in json.ab_rp_chg_ids)
          this.AbilityReplaceChangeIdLists.Add(abRpChgId);
      }
      if (this.AbilityReplaceTargetIdLists != null && this.AbilityReplaceChangeIdLists != null && this.AbilityReplaceTargetIdLists.Count != this.AbilityReplaceChangeIdLists.Count)
      {
        this.AbilityReplaceTargetIdLists.Clear();
        this.AbilityReplaceChangeIdLists.Clear();
      }
      this.CollaboVoiceId = json.cs_voice;
      this.CollaboVoicePlayDelayFrame = json.cs_vp_df;
      this.TeleportType = (eTeleportType) json.tl_type;
      this.TeleportTarget = (ESkillTarget) json.tl_target;
      this.TeleportHeight = json.tl_height;
      this.TeleportIsMove = json.tl_is_mov != 0;
      this.TrickId = json.tr_id;
      this.TrickSetType = (eTrickSetType) json.tr_set;
      this.BreakObjId = json.bo_id;
      this.MapEffectDesc = json.me_desc;
      this.WeatherRate = json.wth_rate;
      this.WeatherId = json.wth_id;
      this.ElementSpcAtkRate = json.elem_tk;
      this.MaxDamageValue = json.max_dmg;
      this.CutInConceptCardId = json.ci_cc_id;
      this.JudgeHpVal = json.jhp_val;
      this.JudgeHpCalc = (SkillParamCalcTypes) json.jhp_calc;
      this.AcFromAbilId = json.ac_fr_ab_id;
      this.AcToAbilId = json.ac_to_ab_id;
      this.AcTurn = json.ac_turn;
      this.EffectHitTargetNumRate = (OInt) json.eff_htnrate;
      this.AbsorbAndGive = (eAbsorbAndGive) json.aag;
      this.TargetEx = (eSkillTargetEx) json.target_ex;
      this.JumpSpcAtkRate = json.jmp_tk;
      this.flags = (SkillFlags) 0;
      if (json.cutin != 0)
        this.flags |= SkillFlags.ExecuteCutin;
      if (json.isbtl != 0)
        this.flags |= SkillFlags.ExecuteInBattle;
      if (json.chran != 0)
        this.flags |= SkillFlags.EnableChangeRange;
      if (json.sonoba != 0)
        this.flags |= SkillFlags.SelfTargetSelect;
      if (json.pierce != 0)
        this.flags |= SkillFlags.PierceAttack;
      if (json.hbonus != 0)
        this.flags |= SkillFlags.EnableHeightRangeBonus;
      if (json.ehpa != 0)
        this.flags |= SkillFlags.EnableHeightParamAdjust;
      if (json.utgt != 0)
        this.flags |= SkillFlags.EnableUnitLockTarget;
      if (json.ctbreak != 0)
        this.flags |= SkillFlags.CastBreak;
      if (json.mpatk != 0)
        this.flags |= SkillFlags.JewelAttack;
      if (json.fhit != 0)
        this.flags |= SkillFlags.ForceHit;
      if (json.suicide != 0)
        this.flags |= SkillFlags.Suicide;
      if (json.sub_actuate != 0)
        this.flags |= SkillFlags.SubActuate;
      if (json.is_fixed != 0)
        this.flags |= SkillFlags.FixedDamage;
      if (json.f_ulock != 0)
        this.flags |= SkillFlags.ForceUnitLock;
      if (json.ad_react != 0)
        this.flags |= SkillFlags.AllDamageReaction;
      if (json.ig_elem != 0)
        this.flags |= SkillFlags.IgnoreElement;
      if (json.is_pre_apply != 0)
        this.flags |= SkillFlags.PrevApply;
      if (json.jhp_over != 0)
        this.flags |= SkillFlags.JudgeHpOver;
      if (json.is_mhm_dmg != 0)
        this.flags |= SkillFlags.MhmDamage;
      if (json.ac_is_self != 0)
        this.flags |= SkillFlags.AcSelf;
      if (json.ac_is_reset != 0)
        this.flags |= SkillFlags.AcReset;
      if (json.is_htndiv != 0)
        this.flags |= SkillFlags.HitTargetNumDiv;
      if (json.is_no_ccc != 0)
        this.flags |= SkillFlags.NoChargeCalcCT;
      if (json.jmpbreak != 0)
        this.flags |= SkillFlags.JumpBreak;
      this.hp_cost = (OInt) json.hp_cost;
      this.hp_cost_rate = (OInt) Math.Min(Math.Max(json.hp_cost_rate, 0), 100);
      this.random_hit_rate = (OInt) json.rhit;
      this.effect_type = (SkillEffectTypes) json.eff_type;
      this.effect_calc = (SkillParamCalcTypes) json.eff_calc;
      this.effect_rate = new SkillRankUpValue();
      this.effect_rate.ini = (OInt) json.eff_rate_ini;
      this.effect_rate.max = (OInt) json.eff_rate_max;
      this.effect_value = new SkillRankUpValue();
      this.effect_value.ini = (OInt) json.eff_val_ini;
      this.effect_value.max = (OInt) json.eff_val_max;
      this.effect_range = new SkillRankUpValue();
      this.effect_range.ini = (OInt) json.eff_range_ini;
      this.effect_range.max = (OInt) json.eff_range_max;
      this.effect_hprate = (OInt) json.eff_hprate;
      this.effect_mprate = (OInt) json.eff_mprate;
      this.effect_dead_rate = (OInt) json.eff_durate;
      this.effect_lvrate = (OInt) json.eff_lvrate;
      this.attack_type = (AttackTypes) json.atk_type;
      this.attack_detail = (AttackDetailTypes) json.atk_det;
      this.element_type = (EElement) json.elem;
      this.element_value = (SkillRankUpValue) null;
      if (this.element_type != EElement.None)
      {
        this.element_value = new SkillRankUpValue();
        this.element_value.ini = (OInt) json.elem_ini;
        this.element_value.max = (OInt) json.elem_max;
      }
      this.cast_type = (ECastTypes) json.ct_type;
      this.cast_speed = (SkillRankUpValue) null;
      if (this.type == ESkillType.Skill && (json.ct_spd_ini != 0 || json.ct_spd_max != 0))
      {
        this.cast_speed = new SkillRankUpValue();
        this.cast_speed.ini = (OInt) json.ct_spd_ini;
        this.cast_speed.max = (OInt) json.ct_spd_max;
      }
      this.absorb_damage_rate = (OInt) json.abs_d_rate;
      this.reaction_damage_type = (DamageTypes) json.react_d_type;
      this.reaction_det_lists = (List<AttackDetailTypes>) null;
      if (json.react_dets != null)
      {
        this.reaction_det_lists = new List<AttackDetailTypes>();
        foreach (AttackDetailTypes reactDet in json.react_dets)
          this.reaction_det_lists.Add(reactDet);
      }
      this.control_ct_rate = (SkillRankUpValue) null;
      this.control_ct_value = (SkillRankUpValue) null;
      if (this.control_ct_calc == SkillParamCalcTypes.Fixed || json.ct_val_ini != 0 || json.ct_val_max != 0)
      {
        this.control_ct_rate = new SkillRankUpValue();
        this.control_ct_rate.ini = (OInt) json.ct_rate_ini;
        this.control_ct_rate.max = (OInt) json.ct_rate_max;
        this.control_ct_value = new SkillRankUpValue();
        this.control_ct_value.ini = (OInt) json.ct_val_ini;
        this.control_ct_value.max = (OInt) json.ct_val_max;
        this.control_ct_calc = (SkillParamCalcTypes) json.ct_calc;
      }
      this.target_buff_iname = json.t_buff;
      this.target_cond_iname = json.t_cond;
      this.self_buff_iname = json.s_buff;
      this.self_cond_iname = json.s_cond;
      this.shield_type = (ShieldTypes) json.shield_type;
      this.shield_damage_type = (DamageTypes) json.shield_d_type;
      this.shield_turn = (SkillRankUpValue) null;
      this.shield_value = (SkillRankUpValue) null;
      if (this.shield_type != ShieldTypes.None && this.shield_damage_type != DamageTypes.None)
      {
        this.shield_turn = new SkillRankUpValue();
        this.shield_turn.ini = (OInt) json.shield_turn_ini;
        this.shield_turn.max = (OInt) json.shield_turn_max;
        this.shield_value = new SkillRankUpValue();
        this.shield_value.ini = (OInt) json.shield_ini;
        this.shield_value.max = (OInt) json.shield_max;
        if (json.shield_reset != 0)
          this.flags |= SkillFlags.ShieldReset;
      }
      if (this.reaction_damage_type != DamageTypes.None || this.shield_damage_type != DamageTypes.None)
      {
        this.control_damage_rate = new SkillRankUpValue();
        this.control_damage_rate.ini = (OInt) json.ctrl_d_rate_ini;
        this.control_damage_rate.max = (OInt) json.ctrl_d_rate_max;
        this.control_damage_value = new SkillRankUpValue();
        this.control_damage_value.ini = (OInt) json.ctrl_d_ini;
        this.control_damage_value.max = (OInt) json.ctrl_d_max;
        this.control_damage_calc = (SkillParamCalcTypes) json.ctrl_d_calc;
      }
      SkillEffectTypes effectType = this.effect_type;
      switch (effectType)
      {
        case SkillEffectTypes.Teleport:
        case SkillEffectTypes.Changing:
        case SkillEffectTypes.Throw:
          this.scope = (OInt) 0;
          this.select_scope = ESelectType.Cross;
          break;
        case SkillEffectTypes.RateDamage:
          if (this.attack_type == AttackTypes.None)
          {
            this.attack_type = AttackTypes.PhyAttack;
            break;
          }
          break;
        default:
          if (effectType == SkillEffectTypes.Attack || effectType == SkillEffectTypes.ReflectDamage || effectType == SkillEffectTypes.RateDamageCurrent)
            goto case SkillEffectTypes.RateDamage;
          else
            break;
      }
      if (this.select_range == ESelectType.Laser)
      {
        this.select_scope = ESelectType.Laser;
        this.scope = (OInt) Math.Max((int) this.scope, 1);
      }
      else
      {
        switch (this.select_range)
        {
          case ESelectType.LaserSpread:
            this.select_scope = ESelectType.LaserSpread;
            break;
          case ESelectType.LaserWide:
            this.select_scope = ESelectType.LaserWide;
            break;
          case ESelectType.LaserTwin:
            this.select_scope = ESelectType.LaserTwin;
            break;
          case ESelectType.LaserTriple:
            this.select_scope = ESelectType.LaserTriple;
            break;
        }
        switch (this.select_scope)
        {
          case ESelectType.LaserSpread:
          case ESelectType.LaserWide:
          case ESelectType.LaserTwin:
          case ESelectType.LaserTriple:
            this.scope = (OInt) 1;
            break;
        }
      }
      if (this.TeleportType != eTeleportType.None)
      {
        if (!this.IsTargetGridNoUnit && this.TeleportType != eTeleportType.BeforeSkill)
          this.target = ESkillTarget.GridNoUnit;
        if (this.IsTargetTeleport)
        {
          if (this.IsCastSkill())
            this.cast_speed = (SkillRankUpValue) null;
          if ((int) this.scope != 0)
            this.scope = (OInt) 0;
        }
      }
      if (this.IsTargetValidGrid && !this.IsTrickSkill())
        this.target = ESkillTarget.GridNoUnit;
      if (this.timing == ESkillTiming.Auto && this.effect_type == SkillEffectTypes.Attack)
        this.effect_type = SkillEffectTypes.Buff;
      return true;
    }

    public static void UpdateReplaceSkill(List<SkillParam> sp_lists)
    {
      using (List<SkillParam>.Enumerator enumerator = sp_lists.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          SkillParam sp = enumerator.Current;
          if (sp.ReplaceChangeIdLists != null && sp.ReplaceChangeIdLists.Count > 0 && (sp.ReplaceTargetIdLists != null && sp.ReplaceTargetIdLists.Count > 0) && sp.ReplaceChangeIdLists.Count == sp.ReplaceTargetIdLists.Count)
          {
            for (int idx = 0; idx < sp.ReplaceChangeIdLists.Count; ++idx)
            {
              SkillParam skillParam1 = sp_lists.Find((Predicate<SkillParam>) (skill => skill.iname == sp.ReplaceChangeIdLists[idx]));
              SkillParam skillParam2 = sp_lists.Find((Predicate<SkillParam>) (skill => skill.iname == sp.ReplaceTargetIdLists[idx]));
              if (skillParam1 != null && skillParam2 != null && string.IsNullOrEmpty(skillParam1.ReplacedTargetId))
                skillParam1.ReplacedTargetId = skillParam2.iname;
            }
          }
        }
      }
    }

    public bool IsDamagedSkill()
    {
      if (this.effect_type == SkillEffectTypes.Attack || this.effect_type == SkillEffectTypes.ReflectDamage || (this.effect_type == SkillEffectTypes.RateDamage || this.effect_type == SkillEffectTypes.RateDamageCurrent))
        return this.attack_type != AttackTypes.None;
      return false;
    }

    public bool IsHealSkill()
    {
      if (this.effect_type != SkillEffectTypes.Heal)
        return this.effect_type == SkillEffectTypes.RateHeal;
      return true;
    }

    public bool IsReactionSkill()
    {
      return this.type == ESkillType.Reaction;
    }

    public bool IsEnableChangeRange()
    {
      return (this.flags & SkillFlags.EnableChangeRange) != (SkillFlags) 0;
    }

    public bool IsEnableHeightRangeBonus()
    {
      if (SkillParam.IsTypeLaser(this.select_range) || SkillParam.IsTypeLaser(this.select_scope))
        return false;
      return (this.flags & SkillFlags.EnableHeightRangeBonus) != (SkillFlags) 0;
    }

    public bool IsEnableHeightParamAdjust()
    {
      return (this.flags & SkillFlags.EnableHeightParamAdjust) != (SkillFlags) 0;
    }

    public bool IsPierce()
    {
      return (this.flags & SkillFlags.PierceAttack) != (SkillFlags) 0;
    }

    public bool IsJewelAttack()
    {
      return (this.flags & SkillFlags.JewelAttack) != (SkillFlags) 0;
    }

    public bool IsForceHit()
    {
      return (this.flags & SkillFlags.ForceHit) != (SkillFlags) 0;
    }

    public bool IsSuicide()
    {
      return (this.flags & SkillFlags.Suicide) != (SkillFlags) 0;
    }

    public bool IsSubActuate()
    {
      return (this.flags & SkillFlags.SubActuate) != (SkillFlags) 0;
    }

    public bool IsFixedDamage()
    {
      return (this.flags & SkillFlags.FixedDamage) != (SkillFlags) 0;
    }

    public bool IsForceUnitLock()
    {
      return (this.flags & SkillFlags.ForceUnitLock) != (SkillFlags) 0;
    }

    public bool IsAllDamageReaction()
    {
      return (this.flags & SkillFlags.AllDamageReaction) != (SkillFlags) 0;
    }

    public bool IsShieldReset()
    {
      return (this.flags & SkillFlags.ShieldReset) != (SkillFlags) 0;
    }

    public bool IsIgnoreElement()
    {
      return (this.flags & SkillFlags.IgnoreElement) != (SkillFlags) 0;
    }

    public bool IsPrevApply()
    {
      return (this.flags & SkillFlags.PrevApply) != (SkillFlags) 0;
    }

    public bool IsMhmDamage()
    {
      return (this.flags & SkillFlags.MhmDamage) != (SkillFlags) 0;
    }

    public bool IsAcSelf()
    {
      return (this.flags & SkillFlags.AcSelf) != (SkillFlags) 0;
    }

    public bool IsAcReset()
    {
      return (this.flags & SkillFlags.AcReset) != (SkillFlags) 0;
    }

    public bool IsHitTargetNumDiv()
    {
      return (this.flags & SkillFlags.HitTargetNumDiv) != (SkillFlags) 0;
    }

    public bool IsNoChargeCalcCT()
    {
      return (this.flags & SkillFlags.NoChargeCalcCT) != (SkillFlags) 0;
    }

    public bool IsJumpBreak()
    {
      return (this.flags & SkillFlags.JumpBreak) != (SkillFlags) 0;
    }

    public bool IsEnableUnitLockTarget()
    {
      return (this.flags & SkillFlags.EnableUnitLockTarget) != (SkillFlags) 0;
    }

    public bool IsCastBreak()
    {
      return (this.flags & SkillFlags.CastBreak) != (SkillFlags) 0;
    }

    public bool IsCastSkill()
    {
      return this.cast_speed != null;
    }

    public bool IsCutin()
    {
      return (this.flags & SkillFlags.ExecuteCutin) != (SkillFlags) 0;
    }

    public bool IsMapSkill()
    {
      return !this.IsBattleSkill();
    }

    public bool IsBattleSkill()
    {
      return (this.flags & SkillFlags.ExecuteInBattle) != (SkillFlags) 0;
    }

    public bool IsAreaSkill()
    {
      if ((int) this.scope <= 0)
        return this.select_scope == ESelectType.All;
      return true;
    }

    public bool IsAllEffect()
    {
      return this.select_scope == ESelectType.All;
    }

    public bool IsLongRangeSkill()
    {
      if ((int) this.range_max > 1)
        return true;
      if ((int) this.range_max == 0)
        return false;
      if (this.select_range != ESelectType.Diamond)
        return this.select_range == ESelectType.All;
      return true;
    }

    public bool IsSupportSkill()
    {
      return this.effect_type == SkillEffectTypes.Buff || this.effect_type == SkillEffectTypes.Debuff;
    }

    public bool IsConditionSkill()
    {
      return this.effect_type == SkillEffectTypes.CureCondition || this.effect_type == SkillEffectTypes.FailCondition || this.effect_type == SkillEffectTypes.DisableCondition;
    }

    public bool IsTrickSkill()
    {
      return this.effect_type == SkillEffectTypes.SetTrick;
    }

    public bool IsTransformSkill()
    {
      switch (this.effect_type)
      {
        case SkillEffectTypes.TransformUnit:
        case SkillEffectTypes.TransformUnitTakeOverHP:
          return true;
        default:
          return false;
      }
    }

    public bool IsSetBreakObjSkill()
    {
      return this.effect_type == SkillEffectTypes.SetBreakObj;
    }

    public bool IsChangeWeatherSkill()
    {
      return this.effect_type == SkillEffectTypes.ChangeWeather;
    }

    public bool IsJudgeHp(Unit unit)
    {
      if (unit == null || this.condition != ESkillCondition.JudgeHP)
        return false;
      int hp = (int) unit.CurrentStatus.param.hp;
      int num = this.JudgeHpCalc == SkillParamCalcTypes.Scale ? (int) unit.MaximumStatus.param.hp * this.JudgeHpVal / 100 : this.JudgeHpVal;
      if ((this.flags & SkillFlags.JudgeHpOver) != (SkillFlags) 0)
        return hp >= num;
      return hp <= num;
    }

    public bool IsSelfTargetSelect()
    {
      if ((int) this.range_min > 0)
        return false;
      return (this.flags & SkillFlags.SelfTargetSelect) != (SkillFlags) 0;
    }

    public bool IsAdvantage()
    {
      switch (this.effect_type)
      {
        case SkillEffectTypes.Attack:
        case SkillEffectTypes.Debuff:
        case SkillEffectTypes.RateDamage:
        case SkillEffectTypes.RateDamageCurrent:
          return false;
        case SkillEffectTypes.FailCondition:
          return this.target == ESkillTarget.Self || this.target == ESkillTarget.SelfSide;
        case SkillEffectTypes.SetTrick:
          if (!string.IsNullOrEmpty(this.TrickId))
          {
            TrickParam trickParam = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetTrickParam(this.TrickId);
            if (trickParam != null && trickParam.DamageType == eTrickDamageType.DAMAGE)
              return false;
            break;
          }
          break;
      }
      return true;
    }

    public int CalcCurrentRankValue(int rank, int rankcap, SkillRankUpValue param)
    {
      if (param != null)
        return this.CalcCurrentRankValue(rank, rankcap, (int) param.ini, (int) param.max);
      return 0;
    }

    public int CalcCurrentRankValue(int rank, int rankcap, int ini, int max)
    {
      int num1 = rankcap - 1;
      int num2 = rank - 1;
      if (ini == max || num2 < 1 || num1 < 1)
        return ini;
      if (num2 >= num1)
        return max;
      int num3 = (max - ini) * 100 / num1;
      return ini + num3 * num2 / 100;
    }

    public static int CalcSkillEffectValue(SkillParamCalcTypes calctype, int skillval, int target)
    {
      switch (calctype)
      {
        case SkillParamCalcTypes.Add:
          return target + skillval;
        case SkillParamCalcTypes.Scale:
          return target + target * skillval / 100;
        default:
          return skillval;
      }
    }

    public bool IsTargetGridNoUnit
    {
      get
      {
        return this.target == ESkillTarget.GridNoUnit;
      }
    }

    public bool IsTargetValidGrid
    {
      get
      {
        return this.target == ESkillTarget.ValidGrid;
      }
    }

    public static bool IsTypeLaser(ESelectType type)
    {
      return new List<ESelectType>((IEnumerable<ESelectType>) new ESelectType[5]
      {
        ESelectType.Laser,
        ESelectType.LaserSpread,
        ESelectType.LaserWide,
        ESelectType.LaserTwin,
        ESelectType.LaserTriple
      }).Contains(type);
    }

    public bool IsSkillCountNoLimit
    {
      get
      {
        return (int) this.count == 0;
      }
    }

    public bool IsReactionDet(AttackDetailTypes atk_detail_type)
    {
      return this.reaction_det_lists == null || this.reaction_det_lists.Count == 0 || this.reaction_det_lists.Contains(atk_detail_type);
    }

    public bool IsTargetTeleport
    {
      get
      {
        if (this.TeleportType == eTeleportType.BeforeSkill)
          return !this.IsTargetGridNoUnit;
        return false;
      }
    }

    public static bool IsAagTypeGive(eAbsorbAndGive aag)
    {
      switch (aag)
      {
        case eAbsorbAndGive.Give:
        case eAbsorbAndGive.GiveDiv:
        case eAbsorbAndGive.Same:
        case eAbsorbAndGive.SameDiv:
          return true;
        default:
          return false;
      }
    }

    public static bool IsAagTypeSame(eAbsorbAndGive aag)
    {
      switch (aag)
      {
        case eAbsorbAndGive.Same:
        case eAbsorbAndGive.SameDiv:
          return true;
        default:
          return false;
      }
    }

    public static bool IsAagTypeDiv(eAbsorbAndGive aag)
    {
      switch (aag)
      {
        case eAbsorbAndGive.GiveDiv:
        case eAbsorbAndGive.SameDiv:
          return true;
        default:
          return false;
      }
    }
  }
}
