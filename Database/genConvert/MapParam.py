def MapParam(json):
    this={}#MapParamjson)
    #thrownewInvalidJSONException()
    if 'scn' in json:
        this['mapSceneName'] = json['scn']
    if 'set' in json:
        this['mapSetName'] = json['set']
    if 'btl' in json:
        this['battleSceneName'] = json['btl']
    if 'ev' in json:
        this['eventSceneName'] = json['ev']
    if 'bgm' in json:
        this['bgmName'] = json['bgm']
return this
