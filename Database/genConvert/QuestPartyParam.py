def QuestPartyParam(json):
    this={}#QuestPartyParamjson)
    if 'iname' in json:
        this['iname'] = json['iname']
    if 'type_1' in json:
        this['type_1'] = ENUM['PartySlotType'][json['type_1']]
    if 'type_2' in json:
        this['type_2'] = ENUM['PartySlotType'][json['type_2']]
    if 'type_3' in json:
        this['type_3'] = ENUM['PartySlotType'][json['type_3']]
    if 'type_4' in json:
        this['type_4'] = ENUM['PartySlotType'][json['type_4']]
    if 'support_type' in json:
        this['support_type'] = ENUM['PartySlotType'][json['support_type']]
    if 'subtype_1' in json:
        this['subtype_1'] = ENUM['PartySlotType'][json['subtype_1']]
    if 'subtype_2' in json:
        this['subtype_2'] = ENUM['PartySlotType'][json['subtype_2']]
    if 'unit_1' in json:
        this['unit_1'] = json['unit_1']
    if 'unit_2' in json:
        this['unit_2'] = json['unit_2']
    if 'unit_3' in json:
        this['unit_3'] = json['unit_3']
    if 'unit_4' in json:
        this['unit_4'] = json['unit_4']
    if 'subunit_1' in json:
        this['subunit_1'] = json['subunit_1']
    if 'subunit_2' in json:
        this['subunit_2'] = json['subunit_2']
    if 'l_npc_rare' in json:
        this['l_npc_rare'] = json['l_npc_rare']
    #returntrue
return this
