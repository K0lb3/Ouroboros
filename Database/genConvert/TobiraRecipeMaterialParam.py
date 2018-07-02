def TobiraRecipeMaterialParam(json):
    this={}#TobiraRecipeMaterialParamjson)
    #if(json==null)
    #return
    if 'iname' in json:
        this['mIname'] = json['iname']
    if 'num' in json:
        this['mNum'] = json['num']
return this
