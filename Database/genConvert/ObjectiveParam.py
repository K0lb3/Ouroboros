def ObjectiveParam(json):
    this={}#ObjectiveParamjson)
    #if(json==null)
    #thrownewInvalidJSONException()
    if 'iname' in json:
        this['iname'] = json['iname']
    #if(json.objective==null)
    #thrownewInvalidJSONException()
    if 'objective' in json:
        this['objective'] = json['objective']
return this
