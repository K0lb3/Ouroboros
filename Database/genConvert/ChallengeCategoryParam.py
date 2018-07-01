def ChallengeCategoryParam(json):
    this={}#ChallengeCategoryParamjson)
    #returnfalse
    if 'iname' in json:
        this['iname'] = json['iname']
        #this.begin_at.Set(json.begin_at,DateTime.MinValue)
        #this.end_at.Set(json.end_at,DateTime.MaxValue)
    if 'prio' in json:
        this['prio'] = json['prio']
    #returntrue
return this
