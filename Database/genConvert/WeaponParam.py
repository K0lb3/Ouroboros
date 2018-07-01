def WeaponParam(json):
    this={}#WeaponParamjson)
    #returnfalse
    if 'iname' in json:
        this['iname'] = json['iname']
    if 'atk' in json:
        this['atk'] = json['atk']
    if 'formula' in json:
        this['formula'] = json['formula']
    #returntrue
return this
