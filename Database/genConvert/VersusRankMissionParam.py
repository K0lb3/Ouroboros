def VersusRankMissionParam(json):
    this={}#VersusRankMissionParamjson)
    #if(json==null)
    #returnfalse
    if 'iname' in json:
        this['mIName'] = json['iname']
    if 'name' in json:
        this['mName'] = json['name']
    if 'expr' in json:
        this['mExpire'] = json['expr']
    if 'type' in json:
        this['mType'] = ENUM['RankMatchMissionType'][json['type']]
    if 'sval' in json:
        this['mSVal'] = json['sval']
    if 'ival' in json:
        this['mIVal'] = json['ival']
    if 'reward_id' in json:
        this['mRewardId'] = json['reward_id']
    #returntrue
return this
