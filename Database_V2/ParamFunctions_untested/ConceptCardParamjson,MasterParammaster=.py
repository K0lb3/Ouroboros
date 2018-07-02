def ConceptCardParamjson,MasterParammaster=(json):
    this={}#ConceptCardParamjson,MasterParammaster=null)
    if 'iname' in json:
        this['iname'] = json['iname']
    if 'name' in json:
        this['name'] = json['name']
    if 'expr' in json:
        this['expr'] = json['expr']
    if 'type' in json:
        this['type'] = ENUM['eCardType'][json['type']]
    if 'icon' in json:
        this['icon'] = json['icon']
    if 'rare' in json:
        this['rare'] = json['rare']
    if 'sell' in json:
        this['sell'] = json['sell']
    if 'en_cost' in json:
        this['en_cost'] = json['en_cost']
    if 'en_exp' in json:
        this['en_exp'] = json['en_exp']
    if 'en_trust' in json:
        this['en_trust'] = json['en_trust']
    if 'trust_reward' in json:
        this['trust_reward'] = json['trust_reward']
    if 'first_get_unit' in json:
        this['first_get_unit'] = json['first_get_unit']
    #this.is_override_lvcap=true
    if 'lvcap' in json:
        this['lvcap'] = json['lvcap']
    #if(json.lvcap<=0)
        #this.is_override_lvcap=false
        #RarityParamrarityParam=MonoSingleton<GameManager>.Instance.MasterParam.GetRarityParam(this.rare)
        #if(rarityParam!=null)
        #this.lvcap=(int)rarityParam.ConceptCardLvCap
    #if(json.effects!=null)
        if 'effects' in json:
            this['effects'] = newConceptCardEffectsParam[json['effects'].Length]
        #for(intindex=0index<json.effects.Length++index)
            #ConceptCardEffectsParamcardEffectsParam=newConceptCardEffectsParam()
            #if(!cardEffectsParam.Deserialize(json.effects))
            #returnfalse
            #this.effects=cardEffectsParam
    if 'not_sale' in json:
        this['not_sale'] = json['not_sale']==1
    #returntrue
return this
