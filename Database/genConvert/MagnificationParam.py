def MagnificationParam(json):
    this={}#MagnificationParamjson)
    #thrownewInvalidJSONException()
    if 'iname' in json:
        this['iname'] = json['iname']
    if 'atk' in json:
        this['atkMagnifications'] = json['atk']
return this
