def WeaponParam(json):
    this={}#WeaponParamjson)
    if 'iname' in json:
        this['iname'] = json['iname']
    if 'atk' in json:
        this['atk'] = json['atk']
    if 'formula' in json:
        this['formula'] = json['formula']
    return this
