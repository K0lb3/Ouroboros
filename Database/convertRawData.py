RawElement={
    0: 'Fire',
    1: 'Water',
    2: 'Wind',
    3: 'Thunder',
    4: 'Light',
    5: 'Dark',
    6: '',
    10:     'Fire',
    100:    'Water',
    1000:   'Thunder',
    10000:  'Wind',
    100000: 'Light',
    111111: 'Dark'
}


def convertRawBuff(buff,SYS,ENUM):
    b={}
    if 'iname' in buff:
        b['iname'] =buff['iname']
    if 'job' in buff:
        b['job'] = buff['job']
    if 'buff' in buff:
        b['buki'] = buff['buki']
    if 'birth' in buff:
        b['birth'] = buff['birth']
    if 'sex' in buff:
        b['sex'] =  ENUM['ESex'][buff['sex']]
    if 'elem' in buff:
        b['elem'] =  RawElement[buff['elem']]
    if 'rate' in buff:
        b['rate'] =  buff['rate']
    if 'turn' in buff:
        b['turn'] =  buff['turn']
    if 'chktgt' in buff:
        b['chk_target'] = ENUM['EffectCheckTargets'][buff['chktgt']]
    if 'timin' in buff:
        b['chk_timing'] = ENUM['EffectCheckTimings'][buff['timing']]
    if 'cond' in buff:
        b['cond'] = ENUM['ESkillCondition'][buff['cond']]
    if 'app_type' in buff:
        b['mAppType'] = ENUM['EAppType'][buff['app_type']]
    if 'app_mct' in buff:
        b['mAppMct'] = buff['app_mct']
    if 'eff_range' in buff:
        b['mEffRange'] = ENUM['EEffRange'][buff['eff_range']]

    b['buffs']=[]
    use = ['type', 'vmax', 'vini','calc']
    for i in range(1, 11):
        for u in use:
            if (u+'0'+str(i)) in buff:
                buff[u+str(i)] = buff[u+'0'+str(i)]
        try:
            b['buffs'].append({
                'type':ENUM['ParamTypes'][buff['type'+str(i)]],
                'value_ini':buff['vini'+str(i)],
                'value_max':buff['vmax'+str(i)],
                'calc':ENUM['SkillParamCalcTypes'][buff['calc'+str(i)]],
            })
        except:
            pass
    return b

def convertRawCondition(cond,SYS,ENUM):
    c={} 
    if 'iname' in cond:
        c['iname'] = cond['iname']
    if 'job' in cond:
        c['job'] = cond['job']
    if 'buki' in cond:
        c['buki'] = cond['buki']
    if 'birth' in cond:
        c['birth'] = cond['birth']
    if 'sex' in cond:
        c['sex'] = ENUM['ESex'][cond['sex']]
    if 'elem' in cond:
        c['elem'] = RawElement[cond['elem']]
    if 'cond' in cond:
        c['cond'] = ENUM['ESkillCondition'][cond['cond']]
    if 'type' in cond:
        c['type'] = ENUM['ConditionEffectTypes'][cond['type']]
    if 'chktgt' in cond:
        c['chk_target'] = ENUM['EffectCheckTargets'][cond['chktgt']]
    if 'timing' in cond:
        c['chk_timing'] = ENUM['EffectCheckTimings'][cond['timing']]
    if 'vini' in cond:
        c['value_ini'] = cond['vini']
    if 'vmax' in cond:
        c['value_max'] = cond['vmax']
    if 'rini' in cond:
        c['rate_ini'] = cond['rini']
    if 'rmax' in cond:
        c['rate_max'] = cond['rmax']
    if 'tini' in cond:
        c['turn_ini'] = cond['tini']
    if 'tmax' in cond:
        c['turn_max'] = cond['tmax']
    if 'turn_max' in cond:
        c['curse'] = cond['curse']
    if 'v_poi' in cond:
        c['v_poison_rate'] = cond['v_poi']
    if 'v_poifix' in cond:
        c['v_poison_fix'] = cond['v_poifix']
    if 'v_par' in cond:
        c['v_paralyse_rate'] = cond['v_par']
    if 'v_blihit' in cond:
        c['v_blink_hit'] = cond['v_blihit']
    if 'v_bliavo' in cond:
        c['v_blink_avo'] = cond['v_bliavo']
    if 'v_dea' in cond:
        c['v_death_count'] = cond['v_dea']
    if 'v_beratk' in cond:
        c['v_berserk_atk'] = cond['v_beratk']
    if 'v_berdef' in cond:
        c['v_berserk_def'] = cond['v_berdef']
    if 'v_fast' in cond:
        c['v_fast'] = cond['v_fast']
    if 'f_slow' in cond:
        c['v_slow'] = cond['v_slow']
    if 'v_don' in cond:
        c['v_donmov'] = cond['v_don']
    if 'v_ahp' in cond:
        c['v_auto_hp_heal'] = cond['v_ahp']
    if 'v_amp' in cond:
        c['v_auto_mp_heal'] = cond['v_amp']
    if 'v_ahpfix' in cond:
        c['v_auto_hp_heal_fix'] = cond['v_ahpfix']
    if 'v_ampfix' in cond:
        c['v_auto_mp_heal_fix'] = cond['v_ampfix']

    pre='Resist_' if cond['type']<2 else 'Assist_'
    EnchantTypes=ENUM['EnchantTypes']
    if 'conds' in cond:
        c['conds']=([
        SYS[pre+EnchantTypes[c]]
        for c in cond['conds']
    ])

    return c

def convertRawSkill(skl,loc,ENUM):

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
    skill["type"] = ENUM["ESkillType"][skl["type"]]
  if "timing" in skl:
    skill["timing"] = ENUM["ESkillTiming"][skl["timing"]]
  if "cond" in skl:
    skill["condition"] = ENUM["ESkillCondition"][skl["cond"]]
  if "target" in skl:
    skill["target"] = ENUM["ESkillTarget"][skl["target"]]
  if "line" in skl:
    skill["line_type"] = ENUM["ELineType"][skl["line"]]
  if "sran" in skl:
    skill["select_range"] = ENUM["ESelectType"][skl["sran"]]
  if"ssco" in skl:
    skill["select_scope"] = ENUM["ESelectType"][skl["ssco"]]
  if "combo_rate" in skl:
    skill["ComboDamageRate"] = 100 - skl['combo_rate']
  if "is_cri" in skl:
    skill["IsCritical"] = skl['is_cri']!=0
  if "jdtype" in skl:
    skill["JewelDamageType"] = ENUM["JewelDamageTypes"][skl["jdtype"]]
  if "jdabs" in skl:
    skill["IsJewelAbsorb"] = skl[jdabs] != 0
  if "kb_dir" in skl:
    skill["KnockBackDir"] = ENUM["eKnockBackDir"][skl["kb_dir"]]
  if "kb_ds" in skl:
    skill["KnockBackDs"] = ENUM["eKnockBackDs"][skl["kb_ds"]]
  if "tl_type" in skl:
    skill["TeleportType"] = ENUM["eTeleportType"][skl["tl_type"]]
  if "tl_target" in skl:
    skill["TeleportTarget"] = ENUM["ESkillTarget"][skl["tl_target"]]
  if "tr_set" in skl:
    skill["TrickSetType"] = ENUM["eTrickSetType"][skl["tr_set"]]
  if "hp_cost_rate" in skl:
    skill["hp_cost_rate"] = min(max(skl["hp_cost_rate"], 0), 100)
  if "eff_type" in skl:
    skill["effect_type"] = ENUM["SkillEffectTypes"][skl["eff_type"]]
  if "eff_calc" in skl:
    skill["effect_calc"] = ENUM["SkillParamCalcTypes"][skl["eff_calc"]]
  if "atk_type" in skl:
    skill["attack_type"] = ENUM["AttackTypes"][skl["atk_type"]]
  if "atk_det" in skl:
    skill["attack_detail"] = ENUM["AttackDetailTypes"][skl["atk_det"]]
  if "elem" in skl:
    skill["element_type"] = RawElement[skl["elem"]]
  if "ct_type" in skl:
    skill["cast_type"] = ENUM["ECastTypes"][skl["ct_type"]]
  if "react_d_type" in skl:
    skill["reaction_damage_type"] = ENUM["DamageTypes"][skl["react_d_type"]]
  if "ct_calc" in skl:
    skill["control_ct_calc"] = ENUM["SkillParamCalcTypes"][skl["ct_calc"]]
  if "shield_type" in skl:
    skill["shield_type"] = ENUM["ShieldTypes"][skl["shield_type"]]
  if "shield_d_type" in skl:
    skill["shield_damage_type"] = ENUM["DamageTypes"][skl["shield_d_type"]]
  if "ctrl_d_calc" in skl:
    skill["control_damage_calc"] = ENUM["SkillParamCalcTypes"][skl["ctrl_d_calc"]]

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

