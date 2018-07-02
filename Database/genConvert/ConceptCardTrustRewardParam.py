def ConceptCardTrustRewardParam(json):
    this={}#ConceptCardTrustRewardParamjson)
    if 'iname' in json:
        this['iname'] = json['iname']
    #if(json.rewards!=null)
        if 'rewards' in json:
            this['rewards'] = newConceptCardTrustRewardItemParam[json['rewards'].Length]
        #for(intindex=0index<json.rewards.Length++index)
            #ConceptCardTrustRewardItemParamtrustRewardItemParam=newConceptCardTrustRewardItemParam()
            #if(!trustRewardItemParam.Deserialize(json.rewards))
            #returnfalse
            #this.rewards=trustRewardItemParam
    #returntrue
return this
