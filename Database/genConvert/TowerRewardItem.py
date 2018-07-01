def TowerRewardItem(json):
    this={}#TowerRewardItemjson)
    #thrownewInvalidJSONException()
    if 'iname' in json:
        this['iname'] = json['iname']
    if 'type' in json:
        this['type'] = json['type']
    if 'num' in json:
        this['num'] = json['num']
    if 'visible' in json:
        this['visible'] = json['visible']==1
return this
