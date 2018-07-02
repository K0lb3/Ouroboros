def ConceptCardTrustRewardItemParam(json):
    this={}#ConceptCardTrustRewardItemParamjson)
    if 'reward_type' in json:
        this['reward_type'] = ENUM['eRewardType'][json['reward_type']]
    if 'reward_iname' in json:
        this['iname'] = json['reward_iname']
    if 'reward_num' in json:
        this['reward_num'] = json['reward_num']
    #returntrue
return this
