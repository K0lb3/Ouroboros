from ParamFunctions._variables import RAWBIRTH,RAWELEMENT,ENUM,TRANSLATION
def BuffEffectParam(json):
    this={}#BuffEffectParamjson)
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
    if 'un_group' in json:
        this['un_group'] = json['un_group']
    if 'elem' in json:
        this['elem'] = RAWELEMENT[json['elem']]
    if 'rate' in json:
        this['rate'] = json['rate']
    if 'turn' in json:
        this['turn'] = json['turn']
    if 'chktgt' in json:
        this['chk_target'] = ENUM['EffectCheckTargets'][json['chktgt']]
    if 'timing' in json:
        this['chk_timing'] = ENUM['EffectCheckTimings'][json['timing']]
    if 'cond' in json:
        this['cond'] = ENUM['ESkillCondition'][json['cond']]
    if 'up_timing' in json:
        this['mUpTiming'] = ENUM['EffectCheckTimings'][json['up_timing']]
    if 'app_type' in json:
        this['mAppType'] = ENUM['EAppType'][json['app_type']]
    if 'app_mct' in json:
        this['mAppMct'] = json['app_mct']
    if 'eff_range' in json:
        this['mEffRange'] = ENUM['EEffRange'][json['eff_range']]

    this['buffs']=[]
    keys = {
        'type':'type',
        'vini':'value_ini',
        'vmax':'value_max', 
        'vone':'value_one',
        'calc':'calc'
        }
    for i in range(1, 12):
        n=str(i)
        #Fix for false naming
        for k in keys:
            if ('{key}0{i}'.format(key=k,i=n)) in json:
                json[k+n] = json[k+'0'+n]

        if 'type'+n not in json or json['type'+n]==0:
            continue
        if 'vini'+n not in json:
            json['vini'+n]=0

        if 'vmax'+n not in json:
            json['vmax'+n]=0

        if json['vmax'+n] == 0 and json['vini'+n] == 0:
            continue

        this['buffs'].append({
            'type': TRANSLATION[ENUM['ParamTypes'][json['type'+n]]],
            'value_ini': json['vini'+n],
            'value_max': json['vmax'+n],
            'value_one': json['vone'+n] if 'vone'+n in json else 0,
            'calc':ENUM['SkillParamCalcTypes'][json['calc'+n]] if 'calc'+n in json else 'Add'
        })

    this['mIsUpBuff']=False
    for buff in this['buffs']:
        if buff['value_one'] !=0:
            this['mIsUpBuff']=True
            break


    this['mFlags'] = []
    if 'is_up_rep' in json and (json['is_up_rep'] != 0):
        this['mFlags'].append('UpReplenish')
    if 'is_no_dis' in json and (json['is_no_dis'] != 0):
        this['mFlags'].append('NoDisabled')
    if 'is_no_bt' in json and (json['is_no_bt'] != 0):
        this['mFlags'].append('NoBuffTurn')

    if 'custom_targets' in json:
        this['custom_targets'] = json['custom_targets']
    #returntrue
    return this
