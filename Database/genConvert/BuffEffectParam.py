def BuffEffectParam(json):
    this={}#BuffEffectParamjson)
    #returnfalse
    if 'iname' in json:
        this['iname'] = json['iname']
    if 'job' in json:
        this['job'] = json['job']
    if 'buki' in json:
        this['buki'] = json['buki']
    if 'birth' in json:
        this['birth'] = json['birth']
    if 'sex' in json:
        this['sex'] = ENUM['ESex'][json['sex']]
    if 'elem' in json:
        this['elem'] = Convert.ToInt32,2)
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
    if 'app_type' in json:
        this['mAppType'] = ENUM['EAppType'][json['app_type']]
    if 'app_mct' in json:
        this['mAppMct'] = json['app_mct']
    if 'eff_range' in json:
        this['mEffRange'] = ENUM['EEffRange'][json['eff_range']]
    #++length
    #++length
    #++length
    #++length
    #++length
    #++length
    #++length
    #++length
    #++length
    #++length
    #if(length>0)
            this['']
            this['buffs[index]']
            if 'vini1' in json:
                this['buffs[index]']['value_ini'] = json['vini1']
            this['buffs[index]']
            if 'vmax1' in json:
                this['buffs[index]']['value_max'] = json['vmax1']
            this['buffs[index]']
            if 'calc1' in json:
                this['buffs[index]']['calc'] = ENUM['SkillParamCalcTypes'][json['calc1']]
            #++index
            if 'vini2' in json:
                this['buffs[index]']['value_ini'] = json['vini2']
            if 'vmax2' in json:
                this['buffs[index]']['value_max'] = json['vmax2']
            if 'calc2' in json:
                this['buffs[index]']['calc'] = ENUM['SkillParamCalcTypes'][json['calc2']]
            #++index
            if 'vini3' in json:
                this['buffs[index]']['value_ini'] = json['vini3']
            if 'vmax3' in json:
                this['buffs[index]']['value_max'] = json['vmax3']
            if 'calc3' in json:
                this['buffs[index]']['calc'] = ENUM['SkillParamCalcTypes'][json['calc3']]
            #++index
            if 'vini4' in json:
                this['buffs[index]']['value_ini'] = json['vini4']
            if 'vmax4' in json:
                this['buffs[index]']['value_max'] = json['vmax4']
            if 'calc4' in json:
                this['buffs[index]']['calc'] = ENUM['SkillParamCalcTypes'][json['calc4']]
            #++index
            if 'vini5' in json:
                this['buffs[index]']['value_ini'] = json['vini5']
            if 'vmax5' in json:
                this['buffs[index]']['value_max'] = json['vmax5']
            if 'calc5' in json:
                this['buffs[index]']['calc'] = ENUM['SkillParamCalcTypes'][json['calc5']]
            #++index
            if 'vini6' in json:
                this['buffs[index]']['value_ini'] = json['vini6']
            if 'vmax6' in json:
                this['buffs[index]']['value_max'] = json['vmax6']
            if 'calc6' in json:
                this['buffs[index]']['calc'] = ENUM['SkillParamCalcTypes'][json['calc6']]
            #++index
            if 'vini7' in json:
                this['buffs[index]']['value_ini'] = json['vini7']
            if 'vmax7' in json:
                this['buffs[index]']['value_max'] = json['vmax7']
            if 'calc7' in json:
                this['buffs[index]']['calc'] = ENUM['SkillParamCalcTypes'][json['calc7']]
            #++index
            if 'vini8' in json:
                this['buffs[index]']['value_ini'] = json['vini8']
            if 'vmax8' in json:
                this['buffs[index]']['value_max'] = json['vmax8']
            if 'calc8' in json:
                this['buffs[index]']['calc'] = ENUM['SkillParamCalcTypes'][json['calc8']]
            #++index
            if 'vini9' in json:
                this['buffs[index]']['value_ini'] = json['vini9']
            if 'vmax9' in json:
                this['buffs[index]']['value_max'] = json['vmax9']
            if 'calc9' in json:
                this['buffs[index]']['calc'] = ENUM['SkillParamCalcTypes'][json['calc9']]
            #++index
            if 'vini10' in json:
                this['buffs[index]']['value_ini'] = json['vini10']
            if 'vmax10' in json:
                this['buffs[index]']['value_max'] = json['vmax10']
            if 'calc10' in json:
                this['buffs[index]']['calc'] = ENUM['SkillParamCalcTypes'][json['calc10']]
    #returntrue
return this
