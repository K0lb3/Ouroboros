def ChatPlayerData(json):
    this={}#ChatPlayerDatajson)
    #return
    if 'name' in json:
        this['name'] = json['name']
    if 'exp' in json:
        this['exp'] = json['exp']
    if 'lastlogin' in json:
        this['lastlogin'] = json['lastlogin']
    if 'fuid' in json:
        this['fuid'] = json['fuid']
    if 'is_friend' in json:
        this['is_friend'] = json['is_friend']
    if 'is_favorite' in json:
        this['is_favorite'] = json['is_favorite']
    if 'award' in json:
        this['award'] = json['award']
    #return
    #unitData.Deserialize(json.unit)
return this
