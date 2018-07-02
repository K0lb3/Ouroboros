def VersusRankRewardRewardParam(json):
    this={}#VersusRankRewardRewardParamjson)
    if 'item_type' in json:
        this['mType'] = ENUM['RewardType'][json['item_type']]
    if 'item_iname' in json:
        this['mIName'] = json['item_iname']
    if 'item_num' in json:
        this['mNum'] = json['item_num']
    #returntrue
return this
