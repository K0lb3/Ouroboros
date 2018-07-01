def AppealLimitedShopMaster(json):
    this={}#AppealLimitedShopMasterjson)
    #returnfalse
    if 'fields' in json:
        this['appeal_id'] = json['fields'].appeal_id
    if 'fields' in json:
        this['start_at'] = TimeManager.FromDateTime)
    if 'fields' in json:
        this['end_at'] = TimeManager.FromDateTime)
    if 'fields' in json:
        this['priority'] = json['fields'].priority
    if 'fields' in json:
        this['pos_x_chara'] = json['fields'].position_chara
    if 'fields' in json:
        this['pos_x_text'] = json['fields'].position_text
    #returntrue
return this
