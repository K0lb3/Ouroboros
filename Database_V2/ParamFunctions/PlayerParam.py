def PlayerParam(json):
    this={}#PlayerParamjson)
    #if(json==null)
    #returnfalse
    if 'pt' in json:
        this['pt'] = json['pt']
    if 'ucap' in json:
        this['ucap'] = json['ucap']
    if 'icap' in json:
        this['icap'] = json['icap']
    if 'ecap' in json:
        this['ecap'] = json['ecap']
    if 'fcap' in json:
        this['fcap'] = json['fcap']
    #returntrue
    return this
