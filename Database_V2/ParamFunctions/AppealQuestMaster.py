def AppealQuestMaster(json):
    this={}#AppealQuestMasterjson)
    #if(json==null)
    #returnfalse
    if 'fields' in json:
        this['appeal_id'] = json['fields']['appeal_id']
    if 'fields' in json:
        this['start_at'] = json['start_at']
    if 'fields' in json:
        this['end_at'] = json['end_at']
    #returntrue
    return this
