def UnlockParam(json):
    this={}#UnlockParamjson)
    #returnfalse
    if 'iname' in json:
        this['iname'] = json['iname']
    #try
        if 'iname' in json:
            this['UnlockTarget'] = ENUM['UnlockTargets'][json['iname']]
    #catch(Exceptionex)
        #returnfalse
    if 'lv' in json:
        this['PlayerLevel'] = json['lv']
    if 'vip' in json:
        this['VipRank'] = json['vip']
    #returntrue
return this
