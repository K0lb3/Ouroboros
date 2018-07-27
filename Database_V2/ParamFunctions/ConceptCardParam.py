from ParamFunctions._variables import ENUM,TRANSLATION
def ConceptCardParam(json):
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
        if (json['lvcap'] <= 0):        
            this['is_override_lvcap'] = False
            this['lvcap']=this['rare']*5+10

        if 'effects' in json:
            this['effects'] = [
                ConceptCardEffectsParam(effect)
                for effect in json['effects']
            ]

    if 'not_sale' in json:
        this['not_sale'] = json['not_sale']==1
    #returntrue
    return this


def ConceptCardEffectsParam(json):
    this={}
    return json
    
    this['cnds_iname'] = json['cnds_iname']
    if 'card_skill' in json:
        this['card_skill'] = json['card_skill']
    if 'add_card_skill_buff_awake' in json:
        this['add_card_skill_buff_awake'] = json['add_card_skill_buff_awake']
    if 'add_card_skill_buff_lvmax' in json:
        this['add_card_skill_buff_lvmax'] = json['add_card_skill_buff_lvmax']
    if 'abil_iname' in json:
        this['abil_iname'] = json['abil_iname']
    if 'abil_iname_lvmax' in json:
        this['abil_iname_lvmax'] = json['abil_iname_lvmax']
    if 'statusup_skill' in json:
        this['statusup_skill'] = json['statusup_skill']
    if 'skin' in json:
        this['skin'] = json['skin']
    return this