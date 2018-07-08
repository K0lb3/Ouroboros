def SimpleQuestDropParam(json):
    this={}#SimpleQuestDropParamjson)
    if 'iname' in json:
        this['item_iname'] = json['iname']
    if 'questlist' in json:
        this['questlist'] = json['questlist']
    return this
