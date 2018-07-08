def SimpleLocalMapsParam(json):
    this={}#SimpleLocalMapsParamjson)
    if 'iname' in json:
        this['iname'] = json['iname']
    if 'droplist' in json:
        this['droplist'] = json['droplist']
    return this
