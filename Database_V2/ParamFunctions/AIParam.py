from ParamFunctions._variables import ENUM
def AIParam(json):
    this={}#AIParamjson)
    if 'iname' in json:
        this['iname'] = json['iname']
    if 'role' in json:
        this['role'] = ENUM['RoleTypes'][json['role']]
    if 'prm' in json:
        this['param'] = ENUM['ParamTypes'][json['prm']]
    if 'prmprio' in json:
        this['param_prio'] = ENUM['ParamPriorities'][json['prmprio']]
    this['flags']=[]
    if 'best' in json and json['best'] != 0:
        this['flags'].append(ENUM['AIFlags'][1])
    if 'sneak' in json and json['sneak'] != 0:
        this['flags'].append(ENUM['AIFlags'][2])
    if 'notmov' in json and json['notmov'] != 0:
        this['flags'].append(ENUM['AIFlags'][4])
    if 'notact' in json and json['notact'] != 0:
        this['flags'].append(ENUM['AIFlags'][8])
    if 'notskl' in json and json['notskl'] != 0:
        this['flags'].append(ENUM['AIFlags'][16])
    if 'notavo' in json and json['notavo'] != 0:
        this['flags'].append(ENUM['AIFlags'][32])
    if 'csff' in json and json['csff'] != 0:
        this['flags'].append(ENUM['AIFlags'][64])
    if 'notmpd' in json and json['notmpd'] != 0:
        this['flags'].append(ENUM['AIFlags'][128])
    if 'buff_self' in json and json['buff_self'] != 0:
        this['flags'].append(ENUM['AIFlags'][256])
    if 'notprio' in json and json['notprio'] != 0:
        this['flags'].append(ENUM['AIFlags'][512])
    if 'use_old_sort' in json and json['use_old_sort'] != 0:
        this['flags'].append(ENUM['AIFlags'][1024])
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
        this['SkillCategoryPriorities'] = [
            ENUM['SkillCategory'][skl]
            for skl in json['skil_prio']
            ]
    if 'buff_prio' in json:
        this['BuffPriorities'] = [
            ENUM['ParamTypes'][buff]
            for buff in json['buff_prio']
            ]
    if 'cond_prio' in json:
        this['ConditionPriorities'] = [
            ENUM['EUnitCondition'][cond]
            for cond in json['cond_prio']
            ]
    #returntrue
    return this
