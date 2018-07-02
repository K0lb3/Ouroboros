def UnitGroupParam(json):
    this={}#UnitGroupParamjson)
    if 'iname' in json:
        this['iname'] = json['iname']
    if 'name' in json:
        this['name'] = json['name']
    if 'units' in json:
        this['units'] = json['units']
    #returntrue
return this
