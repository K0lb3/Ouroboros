from ParamFunctions._variables import ENUM
def TrophyCategoryParam(json):
    this={}#TrophyCategoryParamjson)
    if 'iname' in json:
        this['iname'] = json['iname']
    if 'is_not_pull' in json:
        this['is_not_pull'] = json['is_not_pull']==1
    if 'day_reset' in json:
        this['days'] = json['day_reset']
    if 'bgnr' in json:
        this['beginner'] = json['bgnr']
    if 'begin_at' in json:
        this['begin_at'] = json['begin_at']
    if 'end_at' in json:
        this['end_at'] = json['end_at']
    this['category'] = ENUM['TrophyCategorys'][json['category']] if 'category' in json else 'Other'
    if 'linked_quest' in json:
        this['linked_quest'] = json['linked_quest']
    return this
