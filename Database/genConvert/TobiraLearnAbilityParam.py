def TobiraLearnAbilityParam(json):
    this={}#TobiraLearnAbilityParamjson)
    #if(json==null)
    #return
    if 'abil_iname' in json:
        this['mAbilityIname'] = json['abil_iname']
    if 'learn_lv' in json:
        this['mLevel'] = json['learn_lv']
    if 'add_type' in json:
        this['mAddType'] = json['add_type']
    if 'abil_overwrite' in json:
        this['mAbilityOverwrite'] = json['abil_overwrite']
return this
