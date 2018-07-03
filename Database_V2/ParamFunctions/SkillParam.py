from ParamFunctions._variables import RAWELEMENT,ENUM,TRANSLATION
def SkillParam(json):
    this={}#SkillParamjson)
    if 'iname' in json:
        this['iname'] = json['iname']
    if 'name' in json:
        this['kanji'] = json['name']
    if json['iname'] in TRANSLATION:
        json.update(TRANSLATION[json['iname']])
    
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
        this['ComboDamageRate'] = (100-abs(json['combo_rate']))
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
    try:
        if('rp_tgt_ids' in json):
            this['ReplaceTargetIdLists']=json['rp_tgt_ids']
        if 'ab_rp_chg_ids' in json and len(this['ReplaceTargetIdLists'])>0:
            this['ReplaceChangeIdLists']=json['rp_chg_ids']
        if(this['ReplaceTargetIdLists'] and this['ReplaceChangeIdLists'] and len(this['ReplaceTargetIdLists']) != len(this['ReplaceChangeIdLists'])):
            this['ReplaceTargetIdLists']=[]
            this['ReplaceChangeIdLists']=[]
    except:
        pass
    try:
        if('ab_rp_tgt_ids' in json):
            this['AbilityReplaceTargetIdLists']=json['ab_rp_tgt_ids']
        if 'ab_rp_chg_ids' in json and len(this['AbilityReplaceTargetIdLists'])>0:
            this['AbilityReplaceChangeIdLists']=json['ab_rp_chg_ids']
        if(this['AbilityReplaceTargetIdLists'] and this['AbilityReplaceChangeIdLists'] and len(this['ReplaceTargetIdLists']) != len(this['ReplaceChangeIdLists'])):
            this['ReplaceTargetIdLists']=[]
            this['ReplaceChangeIdLists']=[]
    except:
        pass

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

    this['flags']=[]
    if 'cutin' in json and json['cutin']!=0:
        this['flags'].append('ExecuteCutin')
    if 'isbtl' in json and json['isbtl']!=0:
        this['flags'].append('ExecuteInBattle')
    if 'chran' in json and json['chran']!=0:
        this['flags'].append('EnableChangeRange')
    if 'sonoba' in json and json['sonoba']!=0:
        this['flags'].append('SelfTargetSelect')
    if 'pierce' in json and json['pierce']!=0:
        this['flags'].append('PierceAttack')
    if 'hbonus' in json and json['hbonus']!=0:
        this['flags'].append('EnableHeightRangeBonus')
    if 'ehpa' in json and json['ehpa']!=0:
        this['flags'].append('EnableHeightParamAdjust')
    if 'utgt' in json and json['utgt']!=0:
        this['flags'].append('EnableUnitLockTarget')
    if 'ctbreak' in json and json['ctbreak']!=0:
        this['flags'].append('CastBreak')
    if 'mpatk' in json and json['mpatk']!=0:
        this['flags'].append('JewelAttack')
    if 'fhit' in json and json['fhit']!=0:
        this['flags'].append('ForceHit')
    if 'suicide' in json and json['suicide']!=0:
        this['flags'].append('Suicide')
    if 'sub_actuate' in json and json['sub_actuate']!=0:
        this['flags'].append('SubActuate')
    if 'is_fixed' in json and json['is_fixed']!=0:
        this['flags'].append('FixedDamage')
    if 'f_ulock' in json and json['f_ulock']!=0:
        this['flags'].append('ForceUnitLock')
    if 'ad_react' in json and json['ad_react']!=0:
        this['flags'].append('AllDamageReaction')
    if 'ig_elem' in json and json['ig_elem']!=0:
        this['flags'].append('IgnoreElement')
    if 'is_pre_apply' in json and json['is_pre_apply']!=0:
        this['flags'].append('PrevApply')
    if 'jhp_over' in json and json['jhp_over']!=0:
        this['flags'].append('JudgeHpOver')
    if 'is_mhm_dmg' in json and json['is_mhm_dmg']!=0:
        this['flags'].append('MhmDamage')
    if 'ac_is_self' in json and json['ac_is_self']!=0:
        this['flags'].append('AcSelf')
    if 'ac_is_reset' in json and json['ac_is_reset']!=0:
        this['flags'].append('AcReset')
    if 'is_htndiv' in json and json['is_htndiv']!=0:
        this['flags'].append('HitTargetNumDiv')
    if 'is_no_ccc' in json and json['is_no_ccc']!=0:
        this['flags'].append('NoChargeCalcCT')
    if 'jmpbreak' in json and json['jmpbreak']!=0:
        this['flags'].append('JumpBreak')

    if 'hp_cost' in json:
        this['hp_cost'] = json['hp_cost']
    if 'hp_cost_rate' in json:
        this['hp_cost_rate'] = min(max(json['hp_cost_rate'],0),100)
    if 'rhit' in json:
        this['random_hit_rate'] = json['rhit']
    if 'eff_type' in json:
        this['effect_type'] = ENUM['SkillEffectTypes'][json['eff_type']]
    if 'eff_calc' in json:
        this['effect_calc'] = ENUM['SkillParamCalcTypes'][json['eff_calc']]

    this['effect_rate']={}
    if 'eff_rate_ini' in json:
        this['effect_rate']['ini'] = json['eff_rate_ini']
    if 'eff_rate_max' in json:
        this['effect_rate']['max'] = json['eff_rate_max']

    this['effect_value']={}
    if 'eff_val_ini' in json:
        this['effect_value']['ini'] = json['eff_val_ini']
    if 'eff_val_max' in json:
        this['effect_value']['max'] = json['eff_val_max']

    this['effect_range']={}
    if 'eff_range_ini' in json:
        this['effect_range']['ini'] = json['eff_range_ini']
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
        this['element_type'] = RAWELEMENT[json['elem']]
        if this['element_type'] != 'None':
            this['element_value']={}
            if 'elem_ini' in json:
                this['element_value']['ini'] = json['elem_ini']
            if 'elem_max' in json:
                this['element_value']['max'] = json['elem_max']

    if 'ct_type' in json:
        this['cast_type'] = ENUM['ECastTypes'][json['ct_type']]
    if ('type' in this and this['type'] == "Skill" and ('ct_spd_ini' in json and json['ct_spd_ini'] != 0) or ('ct_spd_max' in json and json['ct_spd_max'] != 0)):
        this['cast_speed']={}
        if 'ct_spd_ini' in json:
            this['cast_speed']['ini'] = json['ct_spd_ini']
        if 'ct_spd_max' in json:
            this['cast_speed']['max'] = json['ct_spd_max']

    if 'abs_d_rate' in json:
        this['absorb_damage_rate'] = json['abs_d_rate']
    if 'react_d_type' in json:
        this['reaction_damage_type'] = ENUM['DamageTypes'][json['react_d_type']]

    if('react_dets' in json and len(json['react_dets'])!=0):
        this['reaction_det_lists']=[
            ENUM['AttackDetailTypes'][react_dets]
            for react_dets in json['react_dets']
        ]

    if (('ct_val_ini' in json and json['ct_val_ini']!=0) or ('ct_val_max' in json and json['ct_val_max']!=0)):
        this['control_ct_rate']={}
        if 'ct_rate_ini' in json:
            this['control_ct_rate']['ini'] = json['ct_rate_ini']
        if 'ct_rate_max' in json:
            this['control_ct_rate']['max'] = json['ct_rate_max']
        this['control_ct_value']={}
        if 'ct_val_ini' in json:
            this['control_ct_value']['ini'] = json['ct_val_ini']
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

    if('shield_type' in this and this['shield_type']!='None' and this['shield_damage_type']!='None'):
        this['shield_turn']={}
        if 'shield_turn_ini' in json:
            this['shield_turn']['ini'] = json['shield_turn_ini']
        if 'shield_turn_max' in json:
            this['shield_turn']['max'] = json['shield_turn_max']

        this['shield_value']={}
        if 'shield_ini' in json:
            this['shield_value']['ini'] = json['shield_ini']
        if 'shield_max' in json:
            this['shield_value']['max'] = json['shield_max']
        if('shield_reset' in json and json['shield_reset']!=0):
            this['flags'].append('ShieldReset')
    if(('reaction_damage_type' in json and this['reaction_damage_type']!='None') or ('shield_damage_type' in this and this['shield_damage_type']!='None')):
        this['control_damage_rate']={}
        if 'ctrl_d_rate_ini' in json:
            this['control_damage_rate']['ini'] = json['ctrl_d_rate_ini']
        if 'ctrl_d_rate_max' in json:
            this['control_damage_rate']['max'] = json['ctrl_d_rate_max']

        this['control_damage_value']={}
        if 'ctrl_d_ini' in json:
            this['control_damage_value']['ini'] = json['ctrl_d_ini']
        if 'ctrl_d_max' in json:
            this['control_damage_value']['max'] = json['ctrl_d_max']
        if 'ctrl_d_calc' in json:
            this['control_damage_calc'] = ENUM['SkillParamCalcTypes'][json['ctrl_d_calc']]

    while 'effect_type' in this and this['effect_type']:
        effectType=this['effect_type']
        if effectType=='Teleport' or effectType=='Changing' or effectType=='Throw':
            this['scope']=0
            this['select_scope']='Cross'
            break

        if(effectType == 'RateDamage' or effectType=='Attack' or effectType=='ReflectDamage' or effectType=='RateDamageCurrent'):
            if('attack_type' not in this or this['attack_type']=='None'):
                this['attack_type']='PhyAttack'
            break
        else:
            break

    if 'select_range' in this:
        if this['select_range']=='Laser':
            this['select_scope']='Laser'
            this['scope']=max(this['scope'],1) if 'scope' in this else 1 
        else:
            while this['select_range']:
                sel_ran=this['select_range']
                if sel_ran=='LaserSpread':
                    this['select_scope']='LaserSpread'
                    break
                if sel_ran=='LaserWide':
                    this['select_scope']='LaserWide'
                    break
                if sel_ran=='LaserTwin':
                    this['select_scope']='LaserTwin'
                    break
                if sel_ran=='LaserTriple':
                    this['select_scope']='LaserTriple'
                    break
                break
            if 'select_scope' in this:
                t_s_s = this['select_scope']
                if t_s_s == 'LaserSpread' or t_s_s== 'LaserWide' or t_s_s=='LaserTwin' or t_s_s == 'LaserTriple':
                    this['scope'] =1

    if('TeleportType' in this and this['TeleportType']!='None'):
        if not('IsTargetGridNoUnit' in this and this['IsTargetGridNoUnit']) and ('TeleportType' in this and this['TeleportType']!='BeforeSkill'):
            this['target']='GridNoUnit'
        if('IsTargetTeleport' in this and this['IsTargetTeleport']):
            this['scope']=0
    if('IsTargetValidGrid' in this and this['IsTargetValidGrid'] and not (this['effect_type'] == 'SetTrick')):
        this['target']='GridNoUnit'
    if('timing' in this and this['timing']=='Auto' and this['effect_type']=='Attack'):
        this['effect_type']='Buff'
    #returntrue
    return this
