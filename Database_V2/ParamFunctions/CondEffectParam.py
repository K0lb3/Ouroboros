from ParamFunctions._variables import RAWBIRTH,RAWELEMENT,ENUM,TRANSLATION
def CondEffectParam(json):
    this={}#CondEffectParamjson)
    #if(json==null)
    #returnfalse
    if 'iname' in json:
        this['iname'] = json['iname']
    if 'job' in json:
        this['job'] = json['job']
    if 'buki' in json:
        this['buki'] = json['buki']
    if 'birth' in json:
        this['birth'] = RAWBIRTH[json['birth']]
    if 'sex' in json:
        this['sex'] = ENUM['ESex'][json['sex']]
    if 'elem' in json:
        this['elem'] = RAWELEMENT[json['elem']]
    if 'cond' in json:
        this['cond'] = ENUM['ESkillCondition'][json['cond']]
    if 'type' in json:
        this['type'] = ENUM['ConditionEffectTypes'][json['type']]
    if 'chktgt' in json:
        this['chk_target'] = ENUM['EffectCheckTargets'][json['chktgt']]
    if 'timing' in json:
        this['chk_timing'] = ENUM['EffectCheckTimings'][json['timing']]
    if 'vini' in json:
        this['value_ini'] = json['vini']
    if 'vmax' in json:
        this['value_max'] = json['vmax']
    if 'rini' in json:
        this['rate_ini'] = json['rini']
    if 'rmax' in json:
        this['rate_max'] = json['rmax']
    if 'tini' in json:
        this['turn_ini'] = json['tini']
    if 'tmax' in json:
        this['turn_max'] = json['tmax']
    if 'curse' in json:
        this['curse'] = json['curse']
    
    pre='Resist_' if json['type']<2 else 'Assist_'
    EnchantTypes=ENUM['EnchantTypes']
    if 'conds' in json:
        this['conditions'] = [
            TRANSLATION[pre+EnchantTypes[cond]]
            #ENUM['EUnitCondition'][pow(2,cond)]
            for cond in json['conds']
        ]
    if 'buffs' in json:
        this['BuffIds'] = json['buffs']
    if 'v_poi' in json:
        this['v_poison_rate'] = json['v_poi']
    if 'v_poifix' in json:
        this['v_poison_fix'] = json['v_poifix']
    if 'v_par' in json:
        this['v_paralyse_rate'] = json['v_par']
    if 'v_blihit' in json:
        this['v_blink_hit'] = json['v_blihit']
    if 'v_bliavo' in json:
        this['v_blink_avo'] = json['v_bliavo']
    if 'v_dea' in json:
        this['v_death_count'] = json['v_dea']
    if 'v_beratk' in json:
        this['v_berserk_atk'] = json['v_beratk']
    if 'v_berdef' in json:
        this['v_berserk_def'] = json['v_berdef']
    if 'v_fast' in json:
        this['v_fast'] = json['v_fast']
    if 'v_slow' in json:
        this['v_slow'] = json['v_slow']
    if 'v_don' in json:
        this['v_donmov'] = json['v_don']
    if 'v_ahp' in json:
        this['v_auto_hp_heal'] = json['v_ahp']
    if 'v_amp' in json:
        this['v_auto_mp_heal'] = json['v_amp']
    if 'v_ahpfix' in json:
        this['v_auto_hp_heal_fix'] = json['v_ahpfix']
    if 'v_ampfix' in json:
        this['v_auto_mp_heal_fix'] = json['v_ampfix']
    return this
