def AIParam(json):
    this={}#AIParamjson)
    #returnfalse
    if 'iname' in json:
        this['iname'] = json['iname']
    if 'role' in json:
        this['role'] = ENUM['RoleTypes'][json['role']]
    if 'prm' in json:
        this['param'] = ENUM['ParamTypes'][json['prm']]
    if 'prmprio' in json:
        this['param_prio'] = ENUM['ParamPriorities'][json['prmprio']]
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
        if 'skil_prio' in json:
            this['SkillCategoryPriorities'] = newSkillCategory[json['skil_prio'].Length]
        if 'skil_prio' in json:
            this['SkillCategoryPriorities[index]'] = ENUM['SkillCategory'][json['skil_prio']]
        if 'buff_prio' in json:
            this['BuffPriorities'] = newParamTypes[json['buff_prio'].Length]
        if 'buff_prio' in json:
            this['BuffPriorities[index]'] = ENUM['ParamTypes'][json['buff_prio']]
        if 'cond_prio' in json:
            this['ConditionPriorities'] = newEUnitCondition[json['cond_prio'].Length]
        if 'cond_prio' in json:
            this['ConditionPriorities[index]'] = ENUM['EUnitCondition'][json['cond_prio']]
    #returntrue
return this
