def BuffEffectParam(json):
    this={}#BuffEffectParamjson)
    #if(json==null)
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
    if 'un_group' in json:
        this['un_group'] = json['un_group']
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
    #this.mIsUpBuff=(OBool)false
    if 'up_timing' in json:
        this['mUpTiming'] = ENUM['EffectCheckTimings'][json['up_timing']]
    if 'app_type' in json:
        this['mAppType'] = ENUM['EAppType'][json['app_type']]
    if 'app_mct' in json:
        this['mAppMct'] = json['app_mct']
    if 'eff_range' in json:
        this['mEffRange'] = ENUM['EEffRange'][json['eff_range']]
    #this.mFlags=(BuffFlags)0
    #if(json.is_up_rep!=0)
    #this.mFlags|=BuffFlags.UpReplenish
    #if(json.is_no_dis!=0)
    #this.mFlags|=BuffFlags.NoDisabled
    #if(json.is_no_bt!=0)
    #this.mFlags|=BuffFlags.NoBuffTurn
    #ParamTypestype1=(ParamTypes)json.type1
    #ParamTypestype2=(ParamTypes)json.type2
    #ParamTypestype3=(ParamTypes)json.type3
    #ParamTypestype4=(ParamTypes)json.type4
    #ParamTypestype5=(ParamTypes)json.type5
    #ParamTypestype6=(ParamTypes)json.type6
    #ParamTypestype7=(ParamTypes)json.type7
    #ParamTypestype8=(ParamTypes)json.type8
    #ParamTypestype9=(ParamTypes)json.type9
    #ParamTypestype10=(ParamTypes)json.type10
    #ParamTypestype11=(ParamTypes)json.type11
    #intlength=0
    #if(type1!=ParamTypes.None)
    #++length
    #if(type2!=ParamTypes.None)
    #++length
    #if(type3!=ParamTypes.None)
    #++length
    #if(type4!=ParamTypes.None)
    #++length
    #if(type5!=ParamTypes.None)
    #++length
    #if(type6!=ParamTypes.None)
    #++length
    #if(type7!=ParamTypes.None)
    #++length
    #if(type8!=ParamTypes.None)
    #++length
    #if(type9!=ParamTypes.None)
    #++length
    #if(type10!=ParamTypes.None)
    #++length
    #if(type11!=ParamTypes.None)
    #++length
    #if(length>0)
        #this.buffs=newBuffEffectParam.Buff[length]
        #intindex=0
        #if(type1!=ParamTypes.None)
            #this.buffs=newBuffEffectParam.Buff()
            #this.buffs.type=type1
            this['']
            this['buffs']
            if 'vini1' in json:
                this['buffs']['value_ini'] = json['vini1']
            this['buffs']
            if 'vmax1' in json:
                this['buffs']['value_max'] = json['vmax1']
            this['buffs']
            if 'vone1' in json:
                this['buffs']['value_one'] = json['vone1']
            this['buffs']
            if 'calc1' in json:
                this['buffs']['calc'] = ENUM['SkillParamCalcTypes'][json['calc1']]
            #++index
        #if(type2!=ParamTypes.None)
            #this.buffs=newBuffEffectParam.Buff()
            #this.buffs.type=type2
            if 'vini2' in json:
                this['buffs']['value_ini'] = json['vini2']
            if 'vmax2' in json:
                this['buffs']['value_max'] = json['vmax2']
            if 'vone2' in json:
                this['buffs']['value_one'] = json['vone2']
            if 'calc2' in json:
                this['buffs']['calc'] = ENUM['SkillParamCalcTypes'][json['calc2']]
            #++index
        #if(type3!=ParamTypes.None)
            #this.buffs=newBuffEffectParam.Buff()
            #this.buffs.type=type3
            if 'vini3' in json:
                this['buffs']['value_ini'] = json['vini3']
            if 'vmax3' in json:
                this['buffs']['value_max'] = json['vmax3']
            if 'vone3' in json:
                this['buffs']['value_one'] = json['vone3']
            if 'calc3' in json:
                this['buffs']['calc'] = ENUM['SkillParamCalcTypes'][json['calc3']]
            #++index
        #if(type4!=ParamTypes.None)
            #this.buffs=newBuffEffectParam.Buff()
            #this.buffs.type=type4
            if 'vini4' in json:
                this['buffs']['value_ini'] = json['vini4']
            if 'vmax4' in json:
                this['buffs']['value_max'] = json['vmax4']
            if 'vone4' in json:
                this['buffs']['value_one'] = json['vone4']
            if 'calc4' in json:
                this['buffs']['calc'] = ENUM['SkillParamCalcTypes'][json['calc4']]
            #++index
        #if(type5!=ParamTypes.None)
            #this.buffs=newBuffEffectParam.Buff()
            #this.buffs.type=type5
            if 'vini5' in json:
                this['buffs']['value_ini'] = json['vini5']
            if 'vmax5' in json:
                this['buffs']['value_max'] = json['vmax5']
            if 'vone5' in json:
                this['buffs']['value_one'] = json['vone5']
            if 'calc5' in json:
                this['buffs']['calc'] = ENUM['SkillParamCalcTypes'][json['calc5']]
            #++index
        #if(type6!=ParamTypes.None)
            #this.buffs=newBuffEffectParam.Buff()
            #this.buffs.type=type6
            if 'vini6' in json:
                this['buffs']['value_ini'] = json['vini6']
            if 'vmax6' in json:
                this['buffs']['value_max'] = json['vmax6']
            if 'vone6' in json:
                this['buffs']['value_one'] = json['vone6']
            if 'calc6' in json:
                this['buffs']['calc'] = ENUM['SkillParamCalcTypes'][json['calc6']]
            #++index
        #if(type7!=ParamTypes.None)
            #this.buffs=newBuffEffectParam.Buff()
            #this.buffs.type=type7
            if 'vini7' in json:
                this['buffs']['value_ini'] = json['vini7']
            if 'vmax7' in json:
                this['buffs']['value_max'] = json['vmax7']
            if 'vone7' in json:
                this['buffs']['value_one'] = json['vone7']
            if 'calc7' in json:
                this['buffs']['calc'] = ENUM['SkillParamCalcTypes'][json['calc7']]
            #++index
        #if(type8!=ParamTypes.None)
            #this.buffs=newBuffEffectParam.Buff()
            #this.buffs.type=type8
            if 'vini8' in json:
                this['buffs']['value_ini'] = json['vini8']
            if 'vmax8' in json:
                this['buffs']['value_max'] = json['vmax8']
            if 'vone8' in json:
                this['buffs']['value_one'] = json['vone8']
            if 'calc8' in json:
                this['buffs']['calc'] = ENUM['SkillParamCalcTypes'][json['calc8']]
            #++index
        #if(type9!=ParamTypes.None)
            #this.buffs=newBuffEffectParam.Buff()
            #this.buffs.type=type9
            if 'vini9' in json:
                this['buffs']['value_ini'] = json['vini9']
            if 'vmax9' in json:
                this['buffs']['value_max'] = json['vmax9']
            if 'vone9' in json:
                this['buffs']['value_one'] = json['vone9']
            if 'calc9' in json:
                this['buffs']['calc'] = ENUM['SkillParamCalcTypes'][json['calc9']]
            #++index
        #if(type10!=ParamTypes.None)
            #this.buffs=newBuffEffectParam.Buff()
            #this.buffs.type=type10
            if 'vini10' in json:
                this['buffs']['value_ini'] = json['vini10']
            if 'vmax10' in json:
                this['buffs']['value_max'] = json['vmax10']
            if 'vone10' in json:
                this['buffs']['value_one'] = json['vone10']
            if 'calc10' in json:
                this['buffs']['calc'] = ENUM['SkillParamCalcTypes'][json['calc10']]
            #++index
        #if(type11!=ParamTypes.None)
            #this.buffs=newBuffEffectParam.Buff()
            #this.buffs.type=type11
            if 'vini11' in json:
                this['buffs']['value_ini'] = json['vini11']
            if 'vmax11' in json:
                this['buffs']['value_max'] = json['vmax11']
            if 'vone11' in json:
                this['buffs']['value_one'] = json['vone11']
            if 'calc11' in json:
                this['buffs']['calc'] = ENUM['SkillParamCalcTypes'][json['calc11']]
            #intnum=index+1
        #foreach(BuffEffectParam.Buffbuffinthis.buffs)
            #if((int)buff.value_one!=0)
                #this.mIsUpBuff=(OBool)true
                #break
    #if(json.custom_targets!=null)
        #for(intindex=0index<json.custom_targets.Length++index)
        if 'custom_targets' in json:
            this['custom_targets'] = json['custom_targets']
    #returntrue
return this
