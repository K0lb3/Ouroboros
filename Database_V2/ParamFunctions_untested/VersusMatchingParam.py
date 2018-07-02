def VersusMatchingParam(json):
    this={}#VersusMatchingParamjson)
    #if(json==null)
    #return
    if 'key' in json:
        this['MatchKey'] = json['key']
    if 'key' in json:
        this['MatchKeyHash'] = VersusMatchingParam.CalcHash
    if 'point' in json:
        this['MatchLinePoint'] = json['point']
return this
