def LoginInfoParam(json):
    this={}#LoginInfoParamjson)
    #returnfalse
    if 'iname' in json:
        this['iname'] = json['iname']
    if 'path' in json:
        this['path'] = json['path']
    if 'scene' in json:
        this['scene'] = json['scene']
    #if(!string.IsNullOrEmpty(json.begin_at))
    #DateTime.TryParse(json.begin_at,outresult1)
    #if(!string.IsNullOrEmpty(json.end_at))
    #DateTime.TryParse(json.end_at,outresult2)
    if 'conditions' in json:
        this['conditions'] = json['conditions']
    if 'conditions_value' in json:
        this['conditions_value'] = json['conditions_value']
    #returntrue
return this
