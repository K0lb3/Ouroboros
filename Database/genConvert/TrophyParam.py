def TrophyParam(json):
    this={}#TrophyParamjson)
    #if(json==null)
    #returnfalse
    #if(json.flg_quests==null)
        #this.RequiredTrophies=newstring[0]
    #else
        #for(intindex=0index<json.flg_quests.Length++index)
        if 'flg_quests' in json:
            this['RequiredTrophies'] = json['flg_quests']
    #this.Objectives=newTrophyObjective[1]
    #for(intindex=0index<1++index)
        #this.Objectives=newTrophyObjective()
        #this.Objectives.Param=this
        #this.Objectives.index=index
        this['']
        this['Objectives']
        if 'type' in json:
            this['Objectives']['type'] = ENUM['TrophyConditionTypes'][json['type']]
        this['Objectives']
        if 'ival' in json:
            this['Objectives']['ival'] = json['ival']
        #if(json.sval!=null)
        this['Objectives']
        if 'sval' in json:
            this['Objectives']['sval'] = newList<string>json['sval'])
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
        #this.is_none_category_hash=false
    if 'category' in json:
        this['Category'] = json['category']
    if 'disp' in json:
        this['DispType'] = ENUM['TrophyDispType'][json['disp']]
    #this.Items=TrophyParam.InitializeItems(json)
    #this.Artifacts=TrophyParam.InitializeArtifacts(json)
    #this.ConceptCards=TrophyParam.InitializeConceptCards(json)
    #returntrue
return this
