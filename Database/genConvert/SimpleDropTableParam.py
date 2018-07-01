def SimpleDropTableParam(json):
    this={}#SimpleDropTableParamjson)
    if 'iname' in json:
        this['iname'] = json['iname']
    if 'droplist' in json:
        this['dropList'] = json['droplist']
    #if(!string.IsNullOrEmpty(json.begin_at))
    #DateTime.TryParse(json.begin_at,outthis.beginAt)
    #if(!string.IsNullOrEmpty(json.end_at))
    #DateTime.TryParse(json.end_at,outthis.endAt)
    #returntrue
return this
