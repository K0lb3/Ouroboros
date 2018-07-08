from ParamFunctions._variables import ENUM
def ObjectiveParam(json):
    this={}#ObjectiveParamjson)
    if 'iname' in json:
        this['iname'] = json['iname']
    this['objective'] =[]
    if 'objective' in json:
        for objective in json['objective']:
            obj={}
            if 'type' in objective:
                obj['Type'] : ENUM['EMissionType'][objective['type']]
            if 'val' in objective:
                obj['TypeParam'] : objective['val']
            if 'item' in objective:
                obj['item'] : objective['item']
            if 'num' in objective:
                obj['itemNum'] : objective['num']
            if 'item_type' in objective:
                obj['itemType'] : ENUM['RewardType'][objective['item_type']]
            if 'IsTakeoverProgress' in objective:
                obj['IsTakeoverProgress'] : objective['IsTakeoverProgress']
        this['objective'].append(obj)
    return this