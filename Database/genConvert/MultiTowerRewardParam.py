def MultiTowerRewardParam(json):
    this={}#MultiTowerRewardParamjson)
    #if(json==null)
    #thrownewInvalidJSONException()
    if 'iname' in json:
        this['iname'] = json['iname']
    #if(json.rewards==null)
    #return
    if 'rewards' in json:
        this['mReward'] = newMultiTowerRewardItem[json['rewards'].Length]
    #for(intindex=0index<json.rewards.Length++index)
        #this.mReward=newMultiTowerRewardItem()
        #this.mReward.Deserialize(json.rewards)
return this
