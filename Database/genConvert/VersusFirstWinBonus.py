def VersusFirstWinBonus(json):
    this={}#VersusFirstWinBonusjson)
    #if(json==null)
    #returnfalse
    if 'id' in json:
        this['id'] = json['id']
    #if(json.rewards!=null)
        #intlength=json.rewards.Length
        #this.rewards=newVersusWinBonusRewardParam[length]
        #if(this.rewards!=null)
            #for(intindex=0index<length++index)
                #this.rewards=newVersusWinBonusRewardParam()
                this['']
                this['rewards']
                if 'rewards' in json:
                    this['rewards']['type'] = ENUM['VERSUS_REWARD_TYPE'][json['rewards']]
                this['rewards']
                if 'rewards' in json:
                    this['rewards']['iname'] = json['rewards'].item_iname
                this['rewards']
                if 'rewards' in json:
                    this['rewards']['num'] = json['rewards'].item_num
    #try
        #if(!string.IsNullOrEmpty(json.begin_at))
        if 'begin_at' in json:
            this['begin_at'] = DateTime.Parse
        #if(!string.IsNullOrEmpty(json.end_at))
        if 'end_at' in json:
            this['end_at'] = DateTime.Parse
    #catch(Exceptionex)
        #DebugUtility.LogError(ex.Message)
        #returnfalse
    #returntrue
return this
