def AchievementParam(json):
    this={}#AchievementParamjson)
    #returnfalse
    if 'fields' in json:
        this['id'] = json['fields'].id
    if 'fields' in json:
        this['iname'] = json['fields'].iname
    if 'fields' in json:
        this['ios'] = json['fields'].ios
    if 'fields' in json:
        this['googleplay'] = json['fields'].googleplay
    #returntrue
return this
