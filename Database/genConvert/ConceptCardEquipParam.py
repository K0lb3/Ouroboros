def ConceptCardEquipParam(json):
    this={}#ConceptCardEquipParamjson)
    if 'cnds_iname' in json:
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
    #returntrue
return this
