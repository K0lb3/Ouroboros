def MultiTowerRewardParam(json):
    this={}#MultiTowerRewardParamjson)
    #thrownewInvalidJSONException()
    if 'iname' in json:
        this['iname'] = json['iname']
    #return
    if 'rewards' in json:
        this['mReward'] = newMultiTowerRewardItem[json['rewards'].Length]
        #this.mReward[index].Deserialize(json.rewards[index])
return this
