def TrophyParam(json):
    this={}#TrophyParamjson)
    #returnfalse
    #else
        if 'flg_quests' in json:
            this['RequiredTrophies'] = json['flg_quests']
        this['']
        this['Objectives[index]']
        if 'type' in json:
            this['Objectives[index]']['type'] = ENUM['TrophyConditionTypes'][json['type']]
        this['Objectives[index]']
        if 'ival' in json:
            this['Objectives[index]']['ival'] = json['ival']
        this['Objectives[index]']
        if 'sval' in json:
            this['Objectives[index]']['sval'] = newList<string>json['sval'])
    if 'iname' in json:
        this['iname'] = json['iname']
    if 'name' in json:
        this['Name'] = json['name']
    if 'expr' in json:
        this['Expr'] = json['expr']
    if 'reward_gold' in json:
        this['Gold'] = json['reward_gold']
    if 'reward_coin' in json:
        this['Coin'] = json['reward_coin']
    if 'reward_exp' in json:
        this['Exp'] = json['reward_exp']
    if 'reward_stamina' in json:
        this['Stamina'] = json['reward_stamina']
    if 'parent_iname' in json:
        this['ParentTrophy'] = json['parent_iname']
    if 'help' in json:
        this['help'] = json['help']
    #if(!string.IsNullOrEmpty(json.category))
        if 'category' in json:
            this['category_hash_code'] = json['category'].GetHashCode
    if 'category' in json:
        this['Category'] = json['category']
    if 'disp' in json:
        this['DispType'] = ENUM['TrophyDispType'][json['disp']]
    #returntrue
return this
