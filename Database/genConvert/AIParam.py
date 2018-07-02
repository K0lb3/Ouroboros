def AIParam(json):
    this={}#AIParamjson)
    #if(json==null)
    #returnfalse
    if 'iname' in json:
        this['iname'] = json['iname']
    if 'role' in json:
        this['role'] = ENUM['RoleTypes'][json['role']]
    if 'prm' in json:
        this['param'] = ENUM['ParamTypes'][json['prm']]
    if 'prmprio' in json:
        this['param_prio'] = ENUM['ParamPriorities'][json['prmprio']]
    #if(json.best!=0)
        #AIParamaiParam=this
        #aiParam.flags=(OLong)((long)aiParam.flags|1L)
    #if(json.sneak!=0)
        #AIParamaiParam=this
        #aiParam.flags=(OLong)((long)aiParam.flags|2L)
    #if(json.notmov!=0)
        #AIParamaiParam=this
        #aiParam.flags=(OLong)((long)aiParam.flags|4L)
    #if(json.notact!=0)
        #AIParamaiParam=this
        #aiParam.flags=(OLong)((long)aiParam.flags|8L)
    #if(json.notskl!=0)
        #AIParamaiParam=this
        #aiParam.flags=(OLong)((long)aiParam.flags|16L)
    #if(json.notavo!=0)
        #AIParamaiParam=this
        #aiParam.flags=(OLong)((long)aiParam.flags|32L)
    #if(json.csff!=0)
        #AIParamaiParam=this
        #aiParam.flags=(OLong)((long)aiParam.flags|64L)
    #if(json.notmpd!=0)
        #AIParamaiParam=this
        #aiParam.flags=(OLong)((long)aiParam.flags|128L)
    #if(json.buff_self!=0)
        #AIParamaiParam=this
        #aiParam.flags=(OLong)((long)aiParam.flags|256L)
    #if(json.notprio!=0)
        #AIParamaiParam=this
        #aiParam.flags=(OLong)((long)aiParam.flags|512L)
    #if(json.use_old_sort!=0)
        #AIParamaiParam=this
        #aiParam.flags=(OLong)((long)aiParam.flags|1024L)
    if 'sos' in json:
        this['escape_border'] = json['sos']
    if 'heal' in json:
        this['heal_border'] = json['heal']
    if 'gems' in json:
        this['gems_border'] = json['gems']
    if 'buff_border' in json:
        this['buff_border'] = json['buff_border']
    if 'cond_border' in json:
        this['cond_border'] = json['cond_border']
    if 'safe_border' in json:
        this['safe_border'] = json['safe_border']
    if 'gosa_border' in json:
        this['gosa_border'] = json['gosa_border']
    if 'notsup_hp' in json:
        this['DisableSupportActionHpBorder'] = json['notsup_hp']
    if 'notsup_num' in json:
        this['DisableSupportActionMemberBorder'] = json['notsup_num']
    #this.SkillCategoryPriorities=(SkillCategory)null
    #this.BuffPriorities=(ParamTypes)null
    #this.ConditionPriorities=(EUnitCondition)null
    #if(json.skil_prio!=null)
        #for(intindex=0index<json.skil_prio.Length++index)
        if 'skil_prio' in json:
            this['SkillCategoryPriorities'] = ENUM['SkillCategory'][json['skil_prio']]
    #if(json.buff_prio!=null)
        #for(intindex=0index<json.buff_prio.Length++index)
        if 'buff_prio' in json:
            this['BuffPriorities'] = ENUM['ParamTypes'][json['buff_prio']]
    #if(json.cond_prio!=null)
        #for(intindex=0index<json.cond_prio.Length++index)
        if 'cond_prio' in json:
            this['ConditionPriorities'] = ENUM['EUnitCondition'][json['cond_prio']]
    #returntrue
return this
