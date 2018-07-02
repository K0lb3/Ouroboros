def SkillParam(json):
    this={}#SkillParamjson)
    #if(json==null)
    #returnfalse
    if 'iname' in json:
        this['iname'] = json['iname']
    if 'name' in json:
        this['name'] = json['name']
    if 'expr' in json:
        this['expr'] = json['expr']
    if 'motnm' in json:
        this['motion'] = json['motnm']
    if 'effnm' in json:
        this['effect'] = json['effnm']
    if 'effdef' in json:
        this['defend_effect'] = json['effdef']
    if 'weapon' in json:
        this['weapon'] = json['weapon']
    if 'tktag' in json:
        this['tokkou'] = json['tktag']
    if 'tkrate' in json:
        this['tk_rate'] = json['tkrate']
    if 'type' in json:
        this['type'] = ENUM['ESkillType'][json['type']]
    if 'timing' in json:
        this['timing'] = ENUM['ESkillTiming'][json['timing']]
    if 'cond' in json:
        this['condition'] = ENUM['ESkillCondition'][json['cond']]
    if 'target' in json:
        this['target'] = ENUM['ESkillTarget'][json['target']]
    if 'line' in json:
        this['line_type'] = ENUM['ELineType'][json['line']]
    if 'cap' in json:
        this['lvcap'] = json['cap']
    if 'cost' in json:
        this['cost'] = json['cost']
    if 'count' in json:
        this['count'] = json['count']
    if 'rate' in json:
        this['rate'] = json['rate']
    if 'sran' in json:
        this['select_range'] = ENUM['ESelectType'][json['sran']]
    if 'rangemin' in json:
        this['range_min'] = json['rangemin']
    if 'range' in json:
        this['range_max'] = json['range']
    if 'ssco' in json:
        this['select_scope'] = ENUM['ESelectType'][json['ssco']]
    if 'scope' in json:
        this['scope'] = json['scope']
    if 'eff_h' in json:
        this['effect_height'] = json['eff_h']
    if 'bdb' in json:
        this['back_defrate'] = json['bdb']
    if 'sdb' in json:
        this['side_defrate'] = json['sdb']
    if 'idr' in json:
        this['ignore_defense_rate'] = json['idr']
    if 'job' in json:
        this['job'] = json['job']
    if 'scn' in json:
        this['SceneName'] = json['scn']
    if 'combo_num' in json:
        this['ComboNum'] = json['combo_num']
    if 'combo_rate' in json:
        this['ComboDamageRate'] = (100-Math.Abs(json['combo_rate']))
    if 'is_cri' in json:
        this['IsCritical'] = (json['is_cri']!=0)
    if 'jdtype' in json:
        this['JewelDamageType'] = ENUM['JewelDamageTypes'][json['jdtype']]
    if 'jdv' in json:
        this['JewelDamageValue'] = json['jdv']
    if 'jdabs' in json:
        this['IsJewelAbsorb'] = (json['jdabs']!=0)
    if 'dupli' in json:
        this['DuplicateCount'] = json['dupli']
    if 'cs_main_id' in json:
        this['CollaboMainId'] = json['cs_main_id']
    if 'cs_height' in json:
        this['CollaboHeight'] = json['cs_height']
    if 'kb_rate' in json:
        this['KnockBackRate'] = json['kb_rate']
    if 'kb_val' in json:
        this['KnockBackVal'] = json['kb_val']
    if 'kb_dir' in json:
        this['KnockBackDir'] = ENUM['eKnockBackDir'][json['kb_dir']]
    if 'kb_ds' in json:
        this['KnockBackDs'] = ENUM['eKnockBackDs'][json['kb_ds']]
    if 'dmg_dt' in json:
        this['DamageDispType'] = ENUM['eDamageDispType'][json['dmg_dt']]
    #this.ReplaceTargetIdLists=(List<string>)null
    #if(json.rp_tgt_ids!=null)
        #this.ReplaceTargetIdLists=newList<string>()
        #foreach(stringrpTgtIdinjson.rp_tgt_ids)
        #this.ReplaceTargetIdLists.Add(rpTgtId)
    #this.ReplaceChangeIdLists=(List<string>)null
    #if(json.rp_chg_ids!=null&&this.ReplaceTargetIdLists!=null)
        #this.ReplaceChangeIdLists=newList<string>()
        #foreach(stringrpChgIdinjson.rp_chg_ids)
        #this.ReplaceChangeIdLists.Add(rpChgId)
    #if(this.ReplaceTargetIdLists!=null&&this.ReplaceChangeIdLists!=null&&this.ReplaceTargetIdLists.Count!=this.ReplaceChangeIdLists.Count)
        #this.ReplaceTargetIdLists.Clear()
        #this.ReplaceChangeIdLists.Clear()
    #this.AbilityReplaceTargetIdLists=(List<string>)null
    #if(json.ab_rp_tgt_ids!=null)
        #this.AbilityReplaceTargetIdLists=newList<string>()
        #foreach(stringabRpTgtIdinjson.ab_rp_tgt_ids)
        #this.AbilityReplaceTargetIdLists.Add(abRpTgtId)
    #this.AbilityReplaceChangeIdLists=(List<string>)null
    #if(json.ab_rp_chg_ids!=null&&this.AbilityReplaceTargetIdLists!=null)
        #this.AbilityReplaceChangeIdLists=newList<string>()
        #foreach(stringabRpChgIdinjson.ab_rp_chg_ids)
        #this.AbilityReplaceChangeIdLists.Add(abRpChgId)
    #if(this.AbilityReplaceTargetIdLists!=null&&this.AbilityReplaceChangeIdLists!=null&&this.AbilityReplaceTargetIdLists.Count!=this.AbilityReplaceChangeIdLists.Count)
        #this.AbilityReplaceTargetIdLists.Clear()
        #this.AbilityReplaceChangeIdLists.Clear()
    if 'cs_voice' in json:
        this['CollaboVoiceId'] = json['cs_voice']
    if 'cs_vp_df' in json:
        this['CollaboVoicePlayDelayFrame'] = json['cs_vp_df']
    if 'tl_type' in json:
        this['TeleportType'] = ENUM['eTeleportType'][json['tl_type']]
    if 'tl_target' in json:
        this['TeleportTarget'] = ENUM['ESkillTarget'][json['tl_target']]
    if 'tl_height' in json:
        this['TeleportHeight'] = json['tl_height']
    if 'tl_is_mov' in json:
        this['TeleportIsMove'] = json['tl_is_mov']!=0
    if 'tr_id' in json:
        this['TrickId'] = json['tr_id']
    if 'tr_set' in json:
        this['TrickSetType'] = ENUM['eTrickSetType'][json['tr_set']]
    if 'bo_id' in json:
        this['BreakObjId'] = json['bo_id']
    if 'me_desc' in json:
        this['MapEffectDesc'] = json['me_desc']
    if 'wth_rate' in json:
        this['WeatherRate'] = json['wth_rate']
    if 'wth_id' in json:
        this['WeatherId'] = json['wth_id']
    if 'elem_tk' in json:
        this['ElementSpcAtkRate'] = json['elem_tk']
    if 'max_dmg' in json:
        this['MaxDamageValue'] = json['max_dmg']
    if 'ci_cc_id' in json:
        this['CutInConceptCardId'] = json['ci_cc_id']
    if 'jhp_val' in json:
        this['JudgeHpVal'] = json['jhp_val']
    if 'jhp_calc' in json:
        this['JudgeHpCalc'] = ENUM['SkillParamCalcTypes'][json['jhp_calc']]
    if 'ac_fr_ab_id' in json:
        this['AcFromAbilId'] = json['ac_fr_ab_id']
    if 'ac_to_ab_id' in json:
        this['AcToAbilId'] = json['ac_to_ab_id']
    if 'ac_turn' in json:
        this['AcTurn'] = json['ac_turn']
    if 'eff_htnrate' in json:
        this['EffectHitTargetNumRate'] = json['eff_htnrate']
    if 'aag' in json:
        this['AbsorbAndGive'] = ENUM['eAbsorbAndGive'][json['aag']]
    if 'target_ex' in json:
        this['TargetEx'] = ENUM['eSkillTargetEx'][json['target_ex']]
    if 'jmp_tk' in json:
        this['JumpSpcAtkRate'] = json['jmp_tk']
    #this.flags=(SkillFlags)0
    #if(json.cutin!=0)
    #this.flags|=SkillFlags.ExecuteCutin
    #if(json.isbtl!=0)
    #this.flags|=SkillFlags.ExecuteInBattle
    #if(json.chran!=0)
    #this.flags|=SkillFlags.EnableChangeRange
    #if(json.sonoba!=0)
    #this.flags|=SkillFlags.SelfTargetSelect
    #if(json.pierce!=0)
    #this.flags|=SkillFlags.PierceAttack
    #if(json.hbonus!=0)
    #this.flags|=SkillFlags.EnableHeightRangeBonus
    #if(json.ehpa!=0)
    #this.flags|=SkillFlags.EnableHeightParamAdjust
    #if(json.utgt!=0)
    #this.flags|=SkillFlags.EnableUnitLockTarget
    #if(json.ctbreak!=0)
    #this.flags|=SkillFlags.CastBreak
    #if(json.mpatk!=0)
    #this.flags|=SkillFlags.JewelAttack
    #if(json.fhit!=0)
    #this.flags|=SkillFlags.ForceHit
    #if(json.suicide!=0)
    #this.flags|=SkillFlags.Suicide
    #if(json.sub_actuate!=0)
    #this.flags|=SkillFlags.SubActuate
    #if(json.is_fixed!=0)
    #this.flags|=SkillFlags.FixedDamage
    #if(json.f_ulock!=0)
    #this.flags|=SkillFlags.ForceUnitLock
    #if(json.ad_react!=0)
    #this.flags|=SkillFlags.AllDamageReaction
    #if(json.ig_elem!=0)
    #this.flags|=SkillFlags.IgnoreElement
    #if(json.is_pre_apply!=0)
    #this.flags|=SkillFlags.PrevApply
    #if(json.jhp_over!=0)
    #this.flags|=SkillFlags.JudgeHpOver
    #if(json.is_mhm_dmg!=0)
    #this.flags|=SkillFlags.MhmDamage
    #if(json.ac_is_self!=0)
    #this.flags|=SkillFlags.AcSelf
    #if(json.ac_is_reset!=0)
    #this.flags|=SkillFlags.AcReset
    #if(json.is_htndiv!=0)
    #this.flags|=SkillFlags.HitTargetNumDiv
    #if(json.is_no_ccc!=0)
    #this.flags|=SkillFlags.NoChargeCalcCT
    #if(json.jmpbreak!=0)
    #this.flags|=SkillFlags.JumpBreak
    if 'hp_cost' in json:
        this['hp_cost'] = json['hp_cost']
    if 'hp_cost_rate' in json:
        this['hp_cost_rate'] = Math.Min(Math.Max(json['hp_cost_rate'],0),100)
    if 'rhit' in json:
        this['random_hit_rate'] = json['rhit']
    if 'eff_type' in json:
        this['effect_type'] = ENUM['SkillEffectTypes'][json['eff_type']]
    if 'eff_calc' in json:
        this['effect_calc'] = ENUM['SkillParamCalcTypes'][json['eff_calc']]
    #this.effect_rate=newSkillRankUpValue()
    this['']
    this['effect_rate']
    if 'eff_rate_ini' in json:
        this['effect_rate']['ini'] = json['eff_rate_ini']
    this['effect_rate']
    if 'eff_rate_max' in json:
        this['effect_rate']['max'] = json['eff_rate_max']
    #this.effect_value=newSkillRankUpValue()
    this['']
    this['effect_value']
    if 'eff_val_ini' in json:
        this['effect_value']['ini'] = json['eff_val_ini']
    this['effect_value']
    if 'eff_val_max' in json:
        this['effect_value']['max'] = json['eff_val_max']
    #this.effect_range=newSkillRankUpValue()
    this['']
    this['effect_range']
    if 'eff_range_ini' in json:
        this['effect_range']['ini'] = json['eff_range_ini']
    this['effect_range']
    if 'eff_range_max' in json:
        this['effect_range']['max'] = json['eff_range_max']
    if 'eff_hprate' in json:
        this['effect_hprate'] = json['eff_hprate']
    if 'eff_mprate' in json:
        this['effect_mprate'] = json['eff_mprate']
    if 'eff_durate' in json:
        this['effect_dead_rate'] = json['eff_durate']
    if 'eff_lvrate' in json:
        this['effect_lvrate'] = json['eff_lvrate']
    if 'atk_type' in json:
        this['attack_type'] = ENUM['AttackTypes'][json['atk_type']]
    if 'atk_det' in json:
        this['attack_detail'] = ENUM['AttackDetailTypes'][json['atk_det']]
    if 'elem' in json:
        this['element_type'] = ENUM['EElement'][json['elem']]
    #this.element_value=(SkillRankUpValue)null
    #if(this.element_type!=EElement.None)
        #this.element_value=newSkillRankUpValue()
        this['']
        this['element_value']
        if 'elem_ini' in json:
            this['element_value']['ini'] = json['elem_ini']
        this['element_value']
        if 'elem_max' in json:
            this['element_value']['max'] = json['elem_max']
    if 'ct_type' in json:
        this['cast_type'] = ENUM['ECastTypes'][json['ct_type']]
    #this.cast_speed=(SkillRankUpValue)null
    if 'ct_spd_ini' in json:
        this['type'] = =ESkillType.Skill&&)
        #this.cast_speed=newSkillRankUpValue()
        this['']
        this['cast_speed']
        if 'ct_spd_ini' in json:
            this['cast_speed']['ini'] = json['ct_spd_ini']
        this['cast_speed']
        if 'ct_spd_max' in json:
            this['cast_speed']['max'] = json['ct_spd_max']
    if 'abs_d_rate' in json:
        this['absorb_damage_rate'] = json['abs_d_rate']
    if 'react_d_type' in json:
        this['reaction_damage_type'] = ENUM['DamageTypes'][json['react_d_type']]
    #this.reaction_det_lists=(List<AttackDetailTypes>)null
    #if(json.react_dets!=null)
        #this.reaction_det_lists=newList<AttackDetailTypes>()
        #foreach(AttackDetailTypesreactDetinjson.react_dets)
        #this.reaction_det_lists.Add(reactDet)
    #this.control_ct_rate=(SkillRankUpValue)null
    #this.control_ct_value=(SkillRankUpValue)null
    if 'ct_val_ini' in json:
        this['control_ct_calc'] = =SkillParamCalcTypes.Fixed||json['ct_val_ini']!=0||json.ct_val_max!=0)
        #this.control_ct_rate=newSkillRankUpValue()
        this['']
        this['control_ct_rate']
        if 'ct_rate_ini' in json:
            this['control_ct_rate']['ini'] = json['ct_rate_ini']
        this['control_ct_rate']
        if 'ct_rate_max' in json:
            this['control_ct_rate']['max'] = json['ct_rate_max']
        #this.control_ct_value=newSkillRankUpValue()
        this['']
        this['control_ct_value']
        if 'ct_val_ini' in json:
            this['control_ct_value']['ini'] = json['ct_val_ini']
        this['control_ct_value']
        if 'ct_val_max' in json:
            this['control_ct_value']['max'] = json['ct_val_max']
        if 'ct_calc' in json:
            this['control_ct_calc'] = ENUM['SkillParamCalcTypes'][json['ct_calc']]
    if 't_buff' in json:
        this['target_buff_iname'] = json['t_buff']
    if 't_cond' in json:
        this['target_cond_iname'] = json['t_cond']
    if 's_buff' in json:
        this['self_buff_iname'] = json['s_buff']
    if 's_cond' in json:
        this['self_cond_iname'] = json['s_cond']
    if 'shield_type' in json:
        this['shield_type'] = ENUM['ShieldTypes'][json['shield_type']]
    if 'shield_d_type' in json:
        this['shield_damage_type'] = ENUM['DamageTypes'][json['shield_d_type']]
    #this.shield_turn=(SkillRankUpValue)null
    #this.shield_value=(SkillRankUpValue)null
    #if(this.shield_type!=ShieldTypes.None&&this.shield_damage_type!=DamageTypes.None)
        #this.shield_turn=newSkillRankUpValue()
        this['']
        this['shield_turn']
        if 'shield_turn_ini' in json:
            this['shield_turn']['ini'] = json['shield_turn_ini']
        this['shield_turn']
        if 'shield_turn_max' in json:
            this['shield_turn']['max'] = json['shield_turn_max']
        #this.shield_value=newSkillRankUpValue()
        this['']
        this['shield_value']
        if 'shield_ini' in json:
            this['shield_value']['ini'] = json['shield_ini']
        this['shield_value']
        if 'shield_max' in json:
            this['shield_value']['max'] = json['shield_max']
        #if(json.shield_reset!=0)
        #this.flags|=SkillFlags.ShieldReset
    #if(this.reaction_damage_type!=DamageTypes.None||this.shield_damage_type!=DamageTypes.None)
        #this.control_damage_rate=newSkillRankUpValue()
        this['']
        this['control_damage_rate']
        if 'ctrl_d_rate_ini' in json:
            this['control_damage_rate']['ini'] = json['ctrl_d_rate_ini']
        this['control_damage_rate']
        if 'ctrl_d_rate_max' in json:
            this['control_damage_rate']['max'] = json['ctrl_d_rate_max']
        #this.control_damage_value=newSkillRankUpValue()
        this['']
        this['control_damage_value']
        if 'ctrl_d_ini' in json:
            this['control_damage_value']['ini'] = json['ctrl_d_ini']
        this['control_damage_value']
        if 'ctrl_d_max' in json:
            this['control_damage_value']['max'] = json['ctrl_d_max']
        if 'ctrl_d_calc' in json:
            this['control_damage_calc'] = ENUM['SkillParamCalcTypes'][json['ctrl_d_calc']]
    #SkillEffectTypeseffectType=this.effect_type
    #switch(effectType)
        #caseSkillEffectTypes.Teleport:
        #caseSkillEffectTypes.Changing:
        #caseSkillEffectTypes.Throw:
        #this.scope=(OInt)0
        #this.select_scope=ESelectType.Cross
        #break
        #caseSkillEffectTypes.RateDamage:
        #if(this.attack_type==AttackTypes.None)
            #this.attack_type=AttackTypes.PhyAttack
            #break
        #break
        #default:
        #if(effectType==SkillEffectTypes.Attack||effectType==SkillEffectTypes.ReflectDamage||effectType==SkillEffectTypes.RateDamageCurrent)
        #gotocaseSkillEffectTypes.RateDamage
        #else
        #break
    #if(this.select_range==ESelectType.Laser)
        #this.select_scope=ESelectType.Laser
        #this.scope=(OInt)Math.Max((int)this.scope,1)
    #else
        #switch(this.select_range)
            #caseESelectType.LaserSpread:
            #this.select_scope=ESelectType.LaserSpread
            #break
            #caseESelectType.LaserWide:
            #this.select_scope=ESelectType.LaserWide
            #break
            #caseESelectType.LaserTwin:
            #this.select_scope=ESelectType.LaserTwin
            #break
            #caseESelectType.LaserTriple:
            #this.select_scope=ESelectType.LaserTriple
            #break
        #switch(this.select_scope)
            #caseESelectType.LaserSpread:
            #caseESelectType.LaserWide:
            #caseESelectType.LaserTwin:
            #caseESelectType.LaserTriple:
            #this.scope=(OInt)1
            #break
    #if(this.TeleportType!=eTeleportType.None)
        #if(!this.IsTargetGridNoUnit&&this.TeleportType!=eTeleportType.BeforeSkill)
        #this.target=ESkillTarget.GridNoUnit
        #if(this.IsTargetTeleport)
            #if(this.IsCastSkill())
            #this.cast_speed=(SkillRankUpValue)null
            #if((int)this.scope!=0)
            #this.scope=(OInt)0
    #if(this.IsTargetValidGrid&&!this.IsTrickSkill())
    #this.target=ESkillTarget.GridNoUnit
    #if(this.timing==ESkillTiming.Auto&&this.effect_type==SkillEffectTypes.Attack)
    #this.effect_type=SkillEffectTypes.Buff
    #returntrue
return this
