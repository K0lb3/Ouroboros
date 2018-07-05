from ParamFunctions._variables import ENUM
def BannerParam(json):
    this={}#BannerParamjson)
    if 'iname' in json:
        this['iname'] = json['iname']
    if 'type' in json:
        this['type'] = json['type']
        #this['type'] = ENUM['BannerType'][json['type']]
    if 'sval' in json:
        this['sval'] = json['sval']
    if 'banr' in json:
        this['banner'] = json['banr']
    if 'banr_sprite' in json:
        this['banr_sprite'] = json['banr_sprite']
    if 'begin_at' in json:
        this['begin_at'] = json['begin_at']
    if 'end_at' in json:
        this['end_at'] = json['end_at']
    if 'priority' in json:
        this['priority'] = json['priority'] if json['priority']>0 else 62554#int.MaxValue
    if 'message' in json:
        this['message'] = json['message']
    if 'is_not_home' in json:
        this['is_not_home'] = json['is_not_home']==1
    return this