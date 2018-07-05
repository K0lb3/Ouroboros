def ChallengeCategoryParam(json):
    this={}#ChallengeCategoryParamjson)
    if 'iname' in json:
        this['iname'] = json['iname']
    
    this['begin_at'] = json['begin_at'] if 'begin_at' in json else False
    this['end_at'] = json['end_at'] if 'end_at' in json else False
    if 'prio' in json:
        this['prio'] = json['prio']
    #returntrue
    return this
