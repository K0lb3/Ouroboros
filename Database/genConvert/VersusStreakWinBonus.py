def VersusStreakWinBonus(json):
    this={}#VersusStreakWinBonusjson)
    #if(json==null)
    #returnfalse
    if 'id' in json:
        this['id'] = json['id']
    if 'wincnt' in json:
        this['wincnt'] = json['wincnt']
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
    #returntrue
return this
