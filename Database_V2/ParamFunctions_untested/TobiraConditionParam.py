def TobiraConditionParam(json):
    this={}#TobiraConditionParamjson)
    #if(json==null)
    #return
    if 'conds_type' in json:
        this['mCondType'] = json['conds_type']
    if 'conds_iname' in json:
        this['mCondIname'] = json['conds_iname']
return this
