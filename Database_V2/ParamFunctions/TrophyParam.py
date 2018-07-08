from ParamFunctions._variables import ENUM
def TrophyParam(json):
    this={}#TrophyParamjson)
    if('flg_quests' not in json or len(json['flg_quests'])==0):
        this['RequiredTrophies']=[]
    else:
        this['RequiredTrophies'] = json['flg_quests']
    this['Objectives']={}
    if 'type' in json:
        this['Objectives']['type'] = ENUM['TrophyConditionTypes'][json['type']]
    if 'ival' in json:
        this['Objectives']['ival'] = json['ival']
    if 'sval' in json:
        this['Objectives']['sval'] = json['sval']
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
    if 'category' in json:
        this['Category'] = json['category']
    if 'disp' in json:
        this['DispType'] = ENUM['TrophyDispType'][json['disp']]
    
    this['Items']=[]
    this['Artifacts']=[]
    this['ConceptCards']=[]
    for i in range(1,4):
        find=[
            ('Items','reward_item{}_iname'.format(i),'reward_item{}_num'.format(i)),
            ('Artifacts','reward_artifact{}_iname'.format(i),'reward_artifact{}_num'.format(i)),
            ('ConceptCards','reward_cc{}_iname'.format(i),'reward_cc{}_num'.format(i)),
            ]
        for typ, iname, num in find: 
            if iname in json:
                this[typ].append({
                    'iname' : json[iname],
                    'num' : json[num]
                })
    return this
