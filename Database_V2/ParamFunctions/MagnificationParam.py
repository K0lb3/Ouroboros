from ParamFunctions._variables import ENUM
def MagnificationParam(json):
    this={}#MagnificationParamjson)
    if 'iname' in json:
        this['iname'] = json['iname']
    if 'atk' in json:
        this['atkMagnifications'] = {
            ENUM['AttackDetailTypes'][typ]:value
            for typ,value in enumerate(json['atk'])
        }
    return this
