def GeoParam(json):
    this={}#GeoParamjson)
    #returnfalse
    if 'iname' in json:
        this['iname'] = json['iname']
    if 'name' in json:
        this['name'] = json['name']
    if 'cost' in json:
        this['cost'] = Math.Max(json['cost'],1)
    if 'stop' in json:
        this['DisableStopped'] = (json['stop']!=0)
    #returntrue
return this
