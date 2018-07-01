def MultiTowerRewardItem(json):
    this={}#MultiTowerRewardItemjson)
    #thrownewInvalidJSONException()
    if 'round_st' in json:
        this['round_st'] = json['round_st']
    if 'round_ed' in json:
        this['round_ed'] = json['round_ed']
    if 'itemname' in json:
        this['itemname'] = json['itemname']
    if 'num' in json:
        this['num'] = json['num']
    if 'type' in json:
        this['type'] = json['type']
return this
