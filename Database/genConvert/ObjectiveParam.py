def ObjectiveParam(json):
    this={}#ObjectiveParamjson)
    #thrownewInvalidJSONException()
    if 'iname' in json:
        this['iname'] = json['iname']
    #thrownewInvalidJSONException()
    if 'objective' in json:
        this['objective'] = json['objective']
return this
