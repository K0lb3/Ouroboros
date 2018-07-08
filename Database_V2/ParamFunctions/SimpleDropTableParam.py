def SimpleDropTableParam(json):
    this={}#SimpleDropTableParamjson)
    if 'iname' in json:
        this['iname'] = json['iname']
    if 'droplist' in json:
        this['dropList'] = json['droplist']
    if 'dropcards' in json:
        this['dropcards'] = json['dropcards']
    if 'begin_at' in json:
        this['beginAt'] = json['begin_at']
    if 'end_at' in json:
        this['endAt'] = json['end_at']
    return this
