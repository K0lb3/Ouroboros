SkillParamCalcTypes = ['Add', 'Scale', 'Fixed', ]
SkillRankUpValue = ['ini', 'max']
SkillEffectTargets = ['Target', 'Self', ]
SkillEffectTypes = [
    "None",
    "Equipment",
    "Attack",
    "Defend",
    "Heal",
    "Buff",
    "Debuff",
    "Revive",
    "Shield",
    "ReflectDamage",
    "DamageControl",
    "FailCondition",
    "CureCondition",
    "DisableCondition",
    "GemsGift",
    "GemsIncDec",
    "Guard",
    "Teleport",
    "Changing",
    "RateHeal",
    "RateDamage",
    "PerfectAvoid",
    "Throw",
    "EffReplace",
    "SetTrick",
    "TransformUnit",
    "SetBreakObj",
    "ChangeWeather",
    "RateDamageCurrent",
]
SkillCategory = ["Damage", "Heal", "Support",
                 "CureCondition", "FailCondition", "DisableCondition", ]
SkillLockTypes = ["None", "PartyMemberUpper2", "PartyMemberLower",
                  "EnemyMemberUpper", "EnemyMemberLower", "HpUpper", "HpLower", "OnGrid", ]
SkillFlags = {
    1: 'EnableRankUp',
    2: 'EnableChangeRange',
    4: 'PierceAttack',
    8: 'SelfTargetSelect',
    16: 'ExecuteCutin',
    32: 'ExecuteInBattle',
    64: 'EnableHeightRangeBonus',
    128: 'EnableHeightParamAdjust',
    256: 'EnableUnitLockTarget',
    512: 'CastBreak',
    1024: 'JewelAttack',
    2048: 'ForceHit',
    4096: 'Suicide',
    8192: 'SubActuate',
    16384: 'FixedDamage',
    32768: 'ForceUnitLock',
    65536: 'AllDamageReaction',
    131072: 'ShieldReset',
    262144: 'IgnoreElement',
    524288: 'PrevApply',
}

ESkillType = ["Attack", "Skill", "Passive", "Item", "Reaction"]
ESkillTiming = ["Used", "Passive", "Wait", "Dead", "DamageCalculate",
                "DamageControl", "Reaction", "FirstReaction", "Auto"]
ESkillCondition = ["None",    "Dying",    "MapEffect",    "Weather",]
ESkillTarget = ["Self","SelfSide","EnemySide","UnitAll","NotSelf","GridNoUnit","ValidGrid"]
ELineType=["None","Direct","Curved","Stab",]
ESelectType=["Cross","Diamond","Square","Laser","All","Wall","WallPlus","Bishop","Victory","LaserSpread","LaserWide","Horse","LaserTwin","LaserTriple","SquareOutline",]
JewelDamageType=["None","Calc","Scale","Fixed",]
eKnockBackDir=["Back","Forward","Left","Right"]
eKnockBackDs=["Target","Self","Grid",]
eTeleportType=["None","Only","BeforeSkill","AfterSkill"]
eTrickSetType=["GridNoUnit","GridAll"]
AttackTypes=["None","PhyAttack","MagAttack"]
AttackDetailTypes=["None","Slash","Stab","Blow","Shot","Magic","Jump","MAX",]
EElement=["None","Fire","Water","Wind","Thunder","Shine","Dark"]
ECastTypes=["Chant","Charge","Jump"]
DamageTypes=["None","TotalDamage","PhyDamage","MagDamage"]
ShieldTypes=["None","UseCount","Hp","Limitter","MAX"]

def convert_raw_skill(skl,loc):
  SkillKey = {
      "iname"	:	"iname",
      "name"	:	"name",
      "expr"	:	"expr",
      "motion"	:	"motnm",
      "effect"	:	"effnm",
      "defend_effect"	:	"effdef",
      "weapon"	:	"weapon",
      "tokkou"	:	"tktag",
      "tk_rate"	:	"tkrate",
      "lvcap"	:	"cap",
      "cost"	:	"cost",
      "count"	:	"count",
      "rate"	:	"rate",
      "range_min"	:	"rangemin",
      "range_max"	:	"range",
      "scope"	:	"scope",
      "effect_height"	:	"eff_h",
      "back_defrate"	:	"bdb",
      "side_defrate"	:	"sdb",
      "ignore_defense_rate"	:	"idr",
      "job"	:	"job",
      "SceneName"	:	"scn",
      "ComboNum"	:	"combo_num",
      "JewelDamageValue"	:	"jdv",
      "DuplicateCount"	:	"dupli",
      "CollaboMainId"	:	"cs_main_id",
      "CollaboHeight"	:	"cs_height",
      "KnockBackRate"	:	"kb_rate",
      "KnockBackVal"	:	"kb_val",
      "CollaboVoiceId"	:	"cs_voice",
      "CollaboVoicePlayDelayFrame"	:	"cs_vp_df",
      "TeleportHeight"	:	"tl_height",
      "TeleportIsMove"	:	"tl_is_mov",
      "TrickId"	:	"tr_id",
      "BreakObjId"	:	"bo_id",
      "MapEffectDesc"	:	"me_desc",
      "WeatherRate"	:	"wth_rate",
      "WeatherId"	:	"wth_id",
      "ElementSpcAtkRate"	:	"elem_tk",
      "MaxDamageValue"	:	"max_dmg",
      "random_hit_rate"	:	"rhit",
      "hp_cost"	:	"hp_cost",
      "effect_rate.ini"	:	"eff_rate_ini",
      "effect_rate.max"	:	"eff_rate_max",
      "effect_value.ini"	:	"eff_val_ini",
      "effect_value.max"	:	"eff_val_max",
      "effect_range.ini"	:	"eff_range_ini",
      "effect_range.max"	:	"eff_range_max",
      "effect_hprate"	:	"eff_hprate",
      "effect_mprate"	:	"eff_mprate",
      "effect_dead_rate"	:	"eff_durate",
      "effect_lvrate"	:	"eff_lvrate",
      "element_value.ini"	:	"elem_ini",
      "element_value.max"	:	"elem_max",
      "cast_speed.ini"	:	"ct_spd_ini",
      "cast_speed.max"	:	"ct_spd_max",
      "absorb_damage_rate"	:	"abs_d_rate",
      "control_ct_rate.ini"	:	"ct_rate_ini",
      "control_ct_rate.max"	:	"ct_rate_max",
      "control_ct_value.ini"	:	"ct_val_ini",
      "control_ct_value.max"	:	"ct_val_max",
      "target_buff_iname"	:	"t_buff",
      "target_cond_iname"	:	"t_cond",
      "self_buff_iname"	:	"s_buff",
      "self_cond_iname"	:	"s_cond",
      "shield_turn.ini"	:	"shield_turn_ini",
      "shield_turn.max"	:	"shield_turn_max",
      "shield_value.ini"	:	"shield_ini",
      "shield_value.max"	:	"shield_max",
      "control_damage_rate.ini"	:	"ctrl_d_rate_ini",
      "control_damage_rate.max"	:	"ctrl_d_rate_max",
      "control_damage_value.ini"	:	"ctrl_d_ini",
      "control_damage_value.max"	:	"ctrl_d_max",
  }

  skill={
    new:skl[old]
    for new,old in SkillKey.items()
    if old in skl
    }

  if "type" in skl:
    skill["type"] = ESkillType[skl["type"]]
  if "timing" in skl:
    skill["timing"] = ESkillTiming[skl["timing"]]
  if "cond" in skl:
    skill["condition"] = ESkillCondition[skl["cond"]]
  if "target" in skl:
    skill["target"] = ESkillTarget[skl["target"]]
  if "line" in skl:
    skill["line_type"] = ELineType[skl["line"]]
  if "sran" in skl:
    skill["select_range"] = ESelectType[skl["sran"]]
  if"ssco" in skl:
    skill["select_scope"] = ESelectType[skl["ssco"]]
  if "combo_rate" in skl:
    skill["ComboDamageRate"] = 100 - skl['combo_rate']
  if "is_cri" in skl:
    skill["IsCritical"] = skl['is_cri']!=0
  if "jdtype" in skl:
    skill["JewelDamageType"] = JewelDamageTypes[skl["jdtype"]]
  if "jdabs" in skl:
    skill["IsJewelAbsorb"] =  skl[jdabs] != 0
  if "kb_dir" in skl:
    skill["KnockBackDir"] = eKnockBackDir[skl["kb_dir"]]
  if "kb_ds" in skl:
    skill["KnockBackDs"] = eKnockBackDs[skl["kb_ds"]]
  if "tl_type" in skl:
    skill["TeleportType"] = eTeleportType[skl["tl_type"]]
  if "tl_target" in skl:
    skill["TeleportTarget"] = ESkillTarget[skl["tl_target"]]
  if "tr_set" in skl:
    skill["TrickSetType"] = eTrickSetType[skl["tr_set"]]
  if "hp_cost_rate" in skl:
    skill["hp_cost_rate"] = min(max(skl["hp_cost_rate"], 0), 100)
  if "eff_type" in skl:
    skill["effect_type"] = SkillEffectTypes[skl["eff_type"]]
  if "eff_calc" in skl:
    skill["effect_calc"] = SkillParamCalcTypes[skl["eff_calc"]]
  if "atk_type" in skl:
    skill["attack_type"] = AttackTypes[skl["atk_type"]]
  if "atk_det" in skl:
    skill["attack_detail"] = AttackDetailTypes[skl["atk_det"]]
  if "elem" in skl:
    skill["element_type"] = EElement[skl["elem"]]
  if "ct_type" in skl:
    skill["cast_type"] = ECastTypes[skl["ct_type"]]
  if "react_d_type" in skl:
    skill["reaction_damage_type"] = DamageTypes[skl["react_d_type"]]
  if "ct_calc" in skl:
    skill["control_ct_calc"] = SkillParamCalcTypes[skl["ct_calc"]]
  if "shield_type" in skl:
    skill["shield_type"] = ShieldTypes[skl["shield_type"]]
  if "shield_d_type" in skl:
    skill["shield_damage_type"] = DamageTypes[skl["shield_d_type"]]

  if skill['iname'] in loc:
    skill.update(loc[skill['iname']])
  return skill

'''	
	"effect_rate = new SkillRankUpValue()",
	"effect_value = new SkillRankUpValue()",
	"effect_range = new SkillRankUpValue()",
	"shield_turn = new SkillRankUpValue()",
	"shield_value = new SkillRankUpValue()",
	"element_value = new SkillRankUpValue()",
	"cast_speed = new SkillRankUpValue()",
	"control_ct_rate = new SkillRankUpValue()",
	"control_ct_value = new SkillRankUpValue()",
	"control_damage_rate = new SkillRankUpValue()",
	"control_damage_value = new SkillRankUpValue()",
	"control_damage_calc"] = SkillParamCalcTypes[skl[""]]
if"ctrl_d_calc",
'''

'''
        SkillEffectTypes effectType = this.effect_type",
        switch (effectType)
        {
          case SkillEffectTypes.Teleport:
          case SkillEffectTypes.Changing:
          case SkillEffectTypes.Throw:
        "scope"] = OInt) 0",
        "select_scope = ESelectType.Cross",
            break",
          case SkillEffectTypes.RateDamage:
            if (this.attack_type == AttackTypes.None)
            {
          "attack_type = AttackTypes.PhyAttack",
              break",
            }
            break",
          default:
            if (effectType == SkillEffectTypes.Attack || effectType == SkillEffectTypes.ReflectDamage || effectType == SkillEffectTypes.RateDamageCurrent)
              goto case SkillEffectTypes.RateDamage",
            else
              break",
        }
        if (this.select_range == ESelectType.Laser)
        {
      "select_scope = ESelectType.Laser",
      "scope"] = OInt) Math.Max((int) this.scope, 1)",
        }
        else
        {
          switch (this.select_range)
          {
            case ESelectType.LaserSpread:
          "select_scope = ESelectType.LaserSpread",
              break",
            case ESelectType.LaserWide:
          "select_scope = ESelectType.LaserWide",
              break",
            case ESelectType.LaserTwin:
          "select_scope = ESelectType.LaserTwin",
              break",
            case ESelectType.LaserTriple:
          "select_scope = ESelectType.LaserTriple",
              break",
          }
          switch (this.select_scope)
          {
            case ESelectType.LaserSpread:
            case ESelectType.LaserWide:
            case ESelectType.LaserTwin:
            case ESelectType.LaserTriple:
          "scope"] = OInt) 1",
              break",
          }
        }
        if (this.TeleportType != eTeleportType.None)
        {
          if (!this.IsTargetGridNoUnit && this.TeleportType != eTeleportType.BeforeSkill)
        "target = ESkillTarget.GridNoUnit",
          if (this.IsTargetTeleport)
          {
            if (this.IsCastSkill())
          "cast_speed"] = SkillRankUpValue) null",
            if ((int) this.scope != 0)
          "scope"] = OInt) 0",
          }
        }
        if (this.IsTargetValidGrid && !this.IsTrickSkill())
      "target = ESkillTarget.GridNoUnit",
        if (this.timing == ESkillTiming.Auto && this.effect_type == SkillEffectTypes.Attack)
      "effect_type = SkillEffectTypes.Buff",
        return true",
      }
'''


'''
      public bool IsDamagedSkill()
      {
        if (this.effect_type == SkillEffectTypes.Attack || this.effect_type == SkillEffectTypes.ReflectDamage || (this.effect_type == SkillEffectTypes.RateDamage || this.effect_type == SkillEffectTypes.RateDamageCurrent))
          return this.attack_type != AttackTypes.None",
        return false",
      }

      public bool IsHealSkill()
      {
        if (this.effect_type != SkillEffectTypes.Heal)
          return this.effect_type == SkillEffectTypes.RateHeal",
        return true",
      }

      public bool IsReactionSkill()
      {
        return this.type == ESkillType.Reaction",
      }

      public bool IsEnableChangeRange()
      {
        return (this.flags & SkillFlags.EnableChangeRange) != (SkillFlags) 0",
      }

      public bool IsEnableHeightRangeBonus()
      {
        if (SkillParam.IsTypeLaser(this.select_range) || SkillParam.IsTypeLaser(this.select_scope))
          return false",
        return (this.flags & SkillFlags.EnableHeightRangeBonus) != (SkillFlags) 0",
      }

      public bool IsEnableHeightParamAdjust()
      {
        return (this.flags & SkillFlags.EnableHeightParamAdjust) != (SkillFlags) 0",
      }

      public bool IsPierce()
      {
        return (this.flags & SkillFlags.PierceAttack) != (SkillFlags) 0",
      }

      public bool IsJewelAttack()
      {
        return (this.flags & SkillFlags.JewelAttack) != (SkillFlags) 0",
      }

      public bool IsForceHit()
      {
        return (this.flags & SkillFlags.ForceHit) != (SkillFlags) 0",
      }

      public bool IsSuicide()
      {
        return (this.flags & SkillFlags.Suicide) != (SkillFlags) 0",
      }

      public bool IsSubActuate()
      {
        return (this.flags & SkillFlags.SubActuate) != (SkillFlags) 0",
      }

      public bool IsFixedDamage()
      {
        return (this.flags & SkillFlags.FixedDamage) != (SkillFlags) 0",
      }

      public bool IsForceUnitLock()
      {
        return (this.flags & SkillFlags.ForceUnitLock) != (SkillFlags) 0",
      }

      public bool IsAllDamageReaction()
      {
        return (this.flags & SkillFlags.AllDamageReaction) != (SkillFlags) 0",
      }

      public bool IsShieldReset()
      {
        return (this.flags & SkillFlags.ShieldReset) != (SkillFlags) 0",
      }

      public bool IsIgnoreElement()
      {
        return (this.flags & SkillFlags.IgnoreElement) != (SkillFlags) 0",
      }

      public bool IsPrevApply()
      {
        return (this.flags & SkillFlags.PrevApply) != (SkillFlags) 0",
      }

      public bool IsEnableUnitLockTarget()
      {
        return (this.flags & SkillFlags.EnableUnitLockTarget) != (SkillFlags) 0",
      }

      public bool IsCastBreak()
      {
        return (this.flags & SkillFlags.CastBreak) != (SkillFlags) 0",
      }

      public bool IsCastSkill()
      {
        return this.cast_speed != null",
      }

      public bool IsCutin()
      {
        return (this.flags & SkillFlags.ExecuteCutin) != (SkillFlags) 0",
      }

      public bool IsMapSkill()
      {
        return !this.IsBattleSkill()",
      }

      public bool IsBattleSkill()
      {
        return (this.flags & SkillFlags.ExecuteInBattle) != (SkillFlags) 0",
      }

      public bool IsAreaSkill()
      {
        if ((int) this.scope <= 0)
          return this.select_scope == ESelectType.All",
        return true",
      }

      public bool IsAllEffect()
      {
        return this.select_scope == ESelectType.All",
      }

      public bool IsLongRangeSkill()
      {
        if ((int) this.range_max > 1)
          return true",
        if ((int) this.range_max == 0)
          return false",
        if (this.select_range != ESelectType.Diamond)
          return this.select_range == ESelectType.All",
        return true",
      }

      public bool IsSupportSkill()
      {
        return this.effect_type == SkillEffectTypes.Buff || this.effect_type == SkillEffectTypes.Debuff",
      }

      public bool IsConditionSkill()
      {
        return this.effect_type == SkillEffectTypes.CureCondition || this.effect_type == SkillEffectTypes.FailCondition || this.effect_type == SkillEffectTypes.DisableCondition",
      }

      public bool IsTrickSkill()
      {
        return this.effect_type == SkillEffectTypes.SetTrick",
      }

      public bool IsTransformSkill()
      {
        return this.effect_type == SkillEffectTypes.TransformUnit",
      }

      public bool IsSetBreakObjSkill()
      {
        return this.effect_type == SkillEffectTypes.SetBreakObj",
      }

      public bool IsChangeWeatherSkill()
      {
        return this.effect_type == SkillEffectTypes.ChangeWeather",
      }

      public bool IsSelfTargetSelect()
      {
        if ((int) this.range_min > 0)
          return false",
        return (this.flags & SkillFlags.SelfTargetSelect) != (SkillFlags) 0",
      }

      public bool IsTargetGridNoUnit
      {
        get
        {
          return this.target == ESkillTarget.GridNoUnit",
        }
      }

      public bool IsTargetValidGrid
      {
        get
        {
          return this.target == ESkillTarget.ValidGrid",
        }
      }

      public bool IsReactionDet(AttackDetailTypes atk_detail_type)
      {
        return this.reaction_det_lists.Count == 0 || this.reaction_det_lists.Contains(atk_detail_type)",
      }

      public bool IsTargetTeleport
      {
        get
        {
          if (this.TeleportType == eTeleportType.BeforeSkill)
            return !this.IsTargetGridNoUnit",
          return false",
        }
      }
    }
  }
  '''
